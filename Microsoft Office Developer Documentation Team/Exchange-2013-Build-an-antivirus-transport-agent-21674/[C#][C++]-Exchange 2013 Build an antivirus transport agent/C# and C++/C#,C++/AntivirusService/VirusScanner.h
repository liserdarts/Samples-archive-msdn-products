// ***************************************************************
// <copyright file="VirusScanner.h" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// CVirusScanner receives messages from a managed Exchange agent
// adds a header, and sends the modified message back to Exchange.
// </summary>
// ***************************************************************

#pragma once

#include "resource.h"
#include "Service.h"
#include "ComInterop.h"

typedef enum { NoOp, RemoveVirus } ScanResult;

// Class CVirusScanner, an IComInvoke implementation for the COM service 
// sample.  The object will be created out-of-process by ComProxy.  

class ATL_NO_VTABLE CVirusScanner:
    public IComInvoke, 
    public CComObjectRootEx<CComMultiThreadModel>,
    public CComCoClass<CVirusScanner, &CLSID_CVirusScanner>
{
private:
    int m_refCount;
    IComCallback *m_callback;
    static HANDLE m_hFileLock;

public:
    CVirusScanner (void);

    ~CVirusScanner (void);
    

    DECLARE_REGISTRY_RESOURCEID(IDR_Class)

    BEGIN_COM_MAP(CVirusScanner)
        COM_INTERFACE_ENTRY(IComInvoke)
    END_COM_MAP()

    STDMETHOD(BeginVirusScan) (IComCallback *callback);

    DECLARE_PROTECT_FINAL_CONSTRUCT()

    HRESULT FinalConstruct() {return S_OK;}
    void FinalRelease() {};

	HRESULT ScanMessage (void);
	static DWORD WINAPI ThreadProc (LPVOID lpThis);
	static void ExamineMessage(char* buffer, ScanResult *result, int *iEndOfHeaderIndex);
	static char *RewriteMessage(char* szInputBuffer, int cbInputBuffer, ScanResult result, int iEndOfHeaderIndex);
};

OBJECT_ENTRY_AUTO(__uuidof(CVirusScanner), CVirusScanner)

