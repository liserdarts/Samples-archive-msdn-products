// ***************************************************************
// <copyright file="VirusScanner.cpp" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// CVirusScanner receives messages from a managed Exchange agent
// adds a header, and sends the modified message back to Exchange.
// </summary>
// ***************************************************************

#include "stdafx.h"
#include "VirusScanner.h"
#include "Service.h"
#include "Service_i.c"
#include "ComInterop_i.c"
#include <CorError.h>
#include <stdio.h>
#include "StreamUtilities.h"

const char *szActionHeader = "X-VirusScanAction: RemoveVirus";
const char *szScannedHeaderName = "X-ExampleAntivirusResult: ";
const char *szScannedHeaderValueNoOp = "Clean; malware=none; ";
const char *szScannedHeaderValueRemoveVirus = "Disinfected; malware=Win32.Fictional; ";
const char *szScannedHeaderParameters = "PseudoScannerVersion=1.0\r\n";

typedef enum { NewLine, CarriageReturn, HeaderName, Colon, Space, HeaderValue } ParserState;

// This will be cleaned up by the OS when the service stops
HANDLE CVirusScanner::m_hFileLock = CreateEvent(
        NULL,  // no special security attributes
        FALSE, // FALSE = auto reset
        TRUE,  // TRUE = initially signaled
        NULL); // no name;

void OutputDebugErrorMessage ();
char* Append (char* pDestination, const char* pSource, int length);

///////////////////////////////////////////////////////////////////////////////
CVirusScanner::CVirusScanner ()
{
	OutputDebugString(L"[VirusScanner] CVirusScanner created.\r\n");
}

///////////////////////////////////////////////////////////////////////////////
CVirusScanner::~CVirusScanner ()
{
}

///////////////////////////////////////////////////////////////////////////////
// This is the function invoked by the Exchange agent, using ComProxy
STDMETHODIMP CVirusScanner::BeginVirusScan(IComCallback *callback)
{
    HRESULT hr = S_OK;
    m_callback = callback;
    OutputDebugString(L"[VirusScanner] BeginVirusScan invoked.\r\n");

    DWORD dwThreadId;
    char dbg[200];

    // AddRef on the 'this' and on the callback so that neither is cleaned up 
    // until the message has been processed.  The caller may release 'this'
    // and/or the callback before the write operation has completed.  The 
    // corresponding Release() calls are at the end of ScanMessage().
    m_callback->AddRef();
    this->AddRef();

    // Create a new thread to process this message using the Component tests.
    CreateThread(NULL,             // The handle cannot be inherited (LPSECURITY_ATTRIBUTES)
        0,                          // default stack size. (dwStackSize)
        CVirusScanner::ThreadProc,// LPTHREAD_START_ROUTINE,
        (LPVOID) this,              // LPVOID lpParameter,
        0,                          // DWORD dwCreationFlags,
        &dwThreadId);

    _snprintf_s(dbg, sizeof(dbg), "[VirusScanner] BeginVirusSCan: Exiting, created thread with Id = %d.  this = %p\r\n", dwThreadId, this);
    OutputDebugStringA(dbg);

    return hr;
}

///////////////////////////////////////////////////////////////////////////////
// This static method is a Win32 ThreadProc wrapper for the LogMessage method.
DWORD WINAPI CVirusScanner::ThreadProc (LPVOID lpThis)
{
    CVirusScanner * pThis = (CVirusScanner *) lpThis;
    return pThis->ScanMessage();
}

///////////////////////////////////////////////////////////////////////////////
// Read the message from the stream, modify it, return it to Exchange.
HRESULT CVirusScanner::ScanMessage ()
{
    HRESULT hr = S_OK;
    IStream *inputStream = NULL;
    IStream *outputStream = NULL;
    char *pInputBuffer = NULL;
    LARGE_INTEGER li;
    STATSTG statstg;
    char dbg[200];
    ULONG cbRead;

    // update the action property
    int action = Unknown;
    int value = 0;
    int cSubjectSize = 0;
    long size = sizeof(int);

    OutputDebugString(L"[VirusScanner] ScanMessage invoked.\r\n");

    __try
    {    
        // Get the read stream
        hr = m_callback->GetContentStream(&inputStream);
        if (FAILED(hr))
        {
            goto Fail;
        }

        // Seek to the beginning of stream
        li.QuadPart = 0;
        hr = inputStream->Seek(li, STREAM_SEEK_SET, NULL);
        if (FAILED(hr))
        {
            goto Fail;
        }

        hr = inputStream->Stat(&statstg, 0);      
        if (FAILED(hr)) 
        {
            goto Fail;
        }
        
        // Initialize a buffer to hold the message stream.  The size of the
        // szInputBuffer is number of bytes in the MessageStream + 1 to 
        // allow for the null terminator.
        int length = statstg.cbSize.LowPart + 1;
        pInputBuffer = new char[length];
        
        // Read the entire message stream
        hr = inputStream->Read(pInputBuffer, statstg.cbSize.LowPart, &cbRead);
        if (FAILED(hr))
        {
            goto Fail;
        }
        pInputBuffer[cbRead] = '\0';

        // Get the scan result and the end-of-header index
        ScanResult result = NoOp;
        int iEndOfHeaders = 0;
        ExamineMessage(pInputBuffer, &result, &iEndOfHeaders);

        // Rewrite the message into a buffer
        char *pOutputBuffer = RewriteMessage(pInputBuffer, cbRead, result, iEndOfHeaders);	

        // Wrap the buffer with a stream
        CComObject<CReadOnlyRgBytesStream> *pOutputStream = NULL;
        hr = CComObject<CReadOnlyRgBytesStream>::CreateInstance(&pOutputStream);
        if (FAILED(hr)) 
        {
            goto Fail;
        }

        pOutputStream->Init((BYTE*) pOutputBuffer, lstrlenA(pOutputBuffer));

        // Pass the stream back to the agent
        hr = m_callback->SetContentStream(pOutputStream);

        goto Exit;

    Fail:
        OutputDebugErrorMessage ();
    Exit:
        ;
    }
    __finally
    {
        // Notify the agent that the message has been processed
        HRESULT hr2 = m_callback->VirusScanCompleted();

        // An AsyncCompletion() error only counts if nothing went wrong beforehand
        if (S_OK == hr && FAILED(hr2))
        {
            hr = hr2;
        }

        _snprintf_s(dbg, sizeof(dbg), "[VirusScanner] ScanMessage result = 0x%x\r\n", hr);
        OutputDebugStringA(dbg);
        
        // Release all resources
        delete [] pInputBuffer;

        if (inputStream)
        {
            inputStream->Release();
            inputStream = NULL;
        }

        if (m_callback)
        {
            m_callback->Release();
            m_callback = NULL;
        }

        this->Release ();
    }

    return hr;
}

///////////////////////////////////////////////////////////////////////////////
// Primitive message parser (not suitable for production use)
void CVirusScanner::ExamineMessage(char* szInputBuffer, ScanResult *pResult, int *piEndOfHeaderIndex)
{
    const char *szBodySeparator = "\r\n\r\n";
    const char *szValueSeparator = ": ";

    *pResult = NoOp;
    *piEndOfHeaderIndex = 0;

    // Figure out where the headers end
    int iBufferLength = lstrlenA(szInputBuffer);
    char *szSeparatorStart = strstr(szInputBuffer, szBodySeparator);
    if (NULL == szSeparatorStart)
    {
        // All headers, no body
        *piEndOfHeaderIndex = iBufferLength;
    }
    else
    {
        // This is where the headers end
        *piEndOfHeaderIndex = (int) (((intptr_t) szSeparatorStart) - ((intptr_t) szInputBuffer)) + 2;
    }

    // Look for the 'action' header
    char* szHeaderNameStart = strstr(szInputBuffer, szActionHeader);
    if (NULL != szHeaderNameStart)
    {
        if ((intptr_t) szHeaderNameStart < (intptr_t) szSeparatorStart)
        {
            *pResult = RemoveVirus;
        }
    }
}

///////////////////////////////////////////////////////////////////////////////
// Build the output message, given the input and the scan operation
char *CVirusScanner::RewriteMessage(char* pInputBuffer, int cbInputBuffer, ScanResult result, int iEndOfHeaderIndex)
{
    // Allocate output buffer
    int cbScannedHeaderName = lstrlenA(szScannedHeaderName);
    int cbScannedHeaderParameters = lstrlenA(szScannedHeaderParameters);
    int cbScannedHeaderValue = lstrlenA(szScannedHeaderValueNoOp);
    int cbScannedHeaderValueRemoveVirus = lstrlenA(szScannedHeaderValueRemoveVirus);
    int cbMessageBody = cbInputBuffer - iEndOfHeaderIndex;

    int cbScannedHeader = cbScannedHeaderName + cbScannedHeaderParameters;
    const char *szScannedHeaderValue = NULL;
    
    if (NoOp == result)
    {
        szScannedHeaderValue = szScannedHeaderValueNoOp;
        cbScannedHeaderValue = lstrlenA(szScannedHeaderValueNoOp);
    }
    else
    {
        szScannedHeaderValue = szScannedHeaderValueRemoveVirus;
        cbScannedHeaderValue = lstrlenA(szScannedHeaderValueRemoveVirus);		
    }
    cbScannedHeader += cbScannedHeaderValue;

    int iOutputLength = cbInputBuffer + cbScannedHeader + 1;

    char *pOutputBuffer = new char[iOutputLength];
    char *pDestination = pOutputBuffer;
        
    pDestination = Append(pDestination, pInputBuffer, iEndOfHeaderIndex);
    pDestination = Append(pDestination, szScannedHeaderName, cbScannedHeaderName);
    pDestination = Append(pDestination, szScannedHeaderValue, cbScannedHeaderValue);
    pDestination = Append(pDestination, szScannedHeaderParameters, cbScannedHeaderParameters);
    pDestination = Append(pDestination, pInputBuffer + iEndOfHeaderIndex, cbMessageBody);
    pDestination = Append(pDestination, "\0", 1);

    return pOutputBuffer;
}

///////////////////////////////////////////////////////////////////////////////
// Simple wrapper facilitates appending with memcpy
char* Append (char* pDestination, const char* pSource, int length)
{
    memcpy(pDestination, pSource, length);
    return pDestination + length;
}

///////////////////////////////////////////////////////////////////////////////
// Make GetLastError() into a readable message and send it to the debug console
void OutputDebugErrorMessage ()
{
    LPWSTR lpMessage;
    DWORD dwError = GetLastError();

    ::FormatMessage( 
        FORMAT_MESSAGE_ALLOCATE_BUFFER | 
        FORMAT_MESSAGE_FROM_SYSTEM | 
        FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL,
        dwError,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        (LPWSTR) &lpMessage,
        0,
        NULL);

    OutputDebugString(lpMessage);
    OutputDebugString(L"\r\n");
    LocalFree(lpMessage);
}
