// ***************************************************************
// <copyright file="ComMessageLogger.cpp" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// CComMessageLogger receives messages from a managed Exchange
// agent, and writes them to disk asynchronously.
// </summary>
// ***************************************************************

#include "stdafx.h"
#include "ComMessageLogger.h"
#include "ComMessageLoggingService.h"
#include "ComMessageLoggingService_i.c"
#include "ComInterop_i.c"
#include <CorError.h>
#include <stdio.h>

extern CServiceModule _AtlModule;

// This will be cleaned up by the OS when the service stops
HANDLE CComMessageLogger::m_hFileLock = CreateEvent(
        NULL,  // no special security attributes
        FALSE, // FALSE = auto reset
        TRUE,  // TRUE = initially signaled
        NULL); // no name;

void OutputDebugErrorMessage ();

///////////////////////////////////////////////////////////////////////////////
CComMessageLogger::CComMessageLogger ()
{
}

///////////////////////////////////////////////////////////////////////////////
CComMessageLogger::~CComMessageLogger ()
{
}

///////////////////////////////////////////////////////////////////////////////
// This is the function invoked by the Exchange agent, using ComProxy
STDMETHODIMP CComMessageLogger::ComAsyncInvoke(IProxyCallback *callback)
{
    HRESULT hr = S_OK;
    m_callback = callback;
    OutputDebugString(L"[ComMessageLogger] ComAsyncInvoke: Entering.\r\n");

    DWORD dwThreadId;
    char dbg[100];

    // AddRef on the 'this' and on the callback so that neither is cleaned up 
    // until the message has been processed.  The caller may release 'this'
    // and/or the callback before the write operation has completed.  The 
    // corresponding Release() calls are at the end of LogMessage().
    m_callback->AddRef();
    this->AddRef();

    // Create a new thread to process this message using the Component tests.
    CreateThread(NULL,             // The handle cannot be inherited (LPSECURITY_ATTRIBUTES)
        0,                          // default stack size. (dwStackSize)
        CComMessageLogger::ThreadProc,// LPTHREAD_START_ROUTINE,
        (LPVOID) this,              // LPVOID lpParameter,
        0,                          // DWORD dwCreationFlags,
        &dwThreadId);

    _snprintf_s(dbg, 100, "[ComMessageLogger] ComAsyncInvoke: Exiting, created thread with Id = %d.  this = %p\r\n", dwThreadId, this);
    OutputDebugStringA(dbg);

    return hr;
}

///////////////////////////////////////////////////////////////////////////////
// This static method is a Win32 ThreadProc wrapper for the LogMessage method.
DWORD WINAPI CComMessageLogger::ThreadProc (LPVOID lpThis)
{
    CComMessageLogger * pThis =(CComMessageLogger *) lpThis;
    return pThis->LogMessage();
}

///////////////////////////////////////////////////////////////////////////////
// Read the message from the stream and write it to disk
HRESULT CComMessageLogger::LogMessage ()
{
    HRESULT hr = S_OK;
    IStream *stream = NULL;
    char *buffer = NULL;
    LARGE_INTEGER li;

    STATSTG statstg;

    char dbg[200];
    ULONG cbRead;

    // update the action property
    int action = Unknown;
    int value = 0;
    int cSubjectSize = 0;
    int iReturnCode = 1;

    long size = sizeof(int);

    OutputDebugString(L"[ComMessageLogger] LogMessage: Entering\r\n");
    
    // Query for the IStream Interface
    hr = m_callback->QueryInterface(IID_IStream, (LPVOID *)&stream);
    if (FAILED(hr)) goto Exit;

    // Seek to the beginning of stream
    li.QuadPart = 0;
    hr = stream->Seek(li, STREAM_SEEK_SET, NULL);
    if (FAILED(hr)) goto Exit;

    hr = stream->Stat(&statstg, 0);      
    if (FAILED(hr)) goto Fail;
    
    // Initialize a Char* Buffer to hold the message stream.  The size of the
    // buffer is number of bytes in the MessageStream + 1 to allow from the 
    // null terminator.
    int length = statstg.cbSize.LowPart + 1;
    buffer = new char[length];
    if (NULL == buffer)
    {
        hr = E_OUTOFMEMORY;
        goto Exit;
    }
    
    // Read the entire message stream
    hr = stream->Read(buffer, statstg.cbSize.LowPart, &cbRead);
    if (FAILED(hr)) goto Fail;
    buffer[cbRead]='\0';
    
    if (WAIT_OBJECT_0 == WaitForSingleObject (m_hFileLock, 5000))
    {
        // Write the message stream to a file, asynchronously
        HANDLE hFile = CreateFile(L"C:\\MessageLog.txt", GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_ALWAYS, NULL, NULL);
        if (hFile != INVALID_HANDLE_VALUE)
        {
            SetFilePointer(hFile, 0, NULL, FILE_END);

            DWORD dwBytesWritten = 0;            
            const char szSeparator[] = "\r\n-------------------------------------------------------------------------------\r\n";

            if (!WriteFile(hFile, szSeparator, lstrlenA(szSeparator), &dwBytesWritten, NULL))
            {
                OutputDebugString(L"[ComMessageLogger::LogMessage: WriteFile failed.\r\n");
            }

            if (!WriteFile(hFile, buffer, length, &dwBytesWritten, NULL))
            {
                OutputDebugString(L"[ComMessageLogger::LogMessage: WriteFile failed.\r\n");
            }

            CloseHandle(hFile);
        }

        SetEvent(m_hFileLock);
    }

    goto Exit;

 Fail:
    OutputDebugErrorMessage ();
    iReturnCode = 0;

 Exit:

    // Notify the agent that the message has been processed
    HRESULT hr2 = m_callback->AsyncCompletion();

    // An AsyncCompletion() error only counts if nothing went wrong beforehand
    if (S_OK == hr && FAILED(hr2)) hr=hr2;    

    _snprintf_s(dbg, sizeof(dbg), "[ComMessageLogger] LogMessage: result = 0x%x, ciReturnCode = %d\r\n", hr, iReturnCode);
    OutputDebugStringA(dbg);
    
    // Release all resources
    delete [] buffer;

    if (stream)
    {
        stream->Release();
        stream = NULL;
    }

    if (m_callback)
    {
        m_callback->Release();
        m_callback = NULL;
    }

    this->Release ();
    return hr;
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
