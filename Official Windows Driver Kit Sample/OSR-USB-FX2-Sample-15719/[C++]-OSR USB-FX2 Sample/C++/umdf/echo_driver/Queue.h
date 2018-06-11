/*++

Copyright (c) Microsoft Corporation, All Rights Reserved

Module Name:

    queue.h

Abstract:

    This file defines the queue callback interface.

Environment:

    Windows User-Mode Driver Framework (WUDF)

--*/

#pragma once


#define MAX_TRANSFER_SIZE(x)   64*1024*1024

//
// Queue Callback Object.
//

class CMyQueue : 
    public IQueueCallbackCreate,
    public IQueueCallbackRead,
    public IQueueCallbackWrite,
    public IQueueCallbackDeviceIoControl,
    public IRequestCallbackRequestCompletion,
    public CUnknown
{
    //
    // Unreferenced pointer to the partner Fx device.
    //

    IWDFIoQueue *m_FxQueue;

    //
    // Unreferenced pointer to the parent device.
    //

    PCMyDevice m_Parent;

    HRESULT
    Initialize(
        VOID
        );

    void
    ForwardFormattedRequest(
        _In_ IWDFIoRequest*                         pRequest,
        _In_ IWDFIoTarget*                          pIoTarget
        );
    
public:

    CMyQueue(
        _In_ PCMyDevice Device
        );

    virtual ~CMyQueue();

    static 
    HRESULT 
    CreateInstance( 
        _In_ PCMyDevice Device,
        _Out_ PCMyQueue *Queue
        );

    HRESULT
    Configure(
        VOID
        );

    IQueueCallbackCreate *
    QueryIQueueCallbackCreate(
        VOID
        )
    {
        AddRef();
        return static_cast<IQueueCallbackCreate *>(this);
    }

    IQueueCallbackWrite *
    QueryIQueueCallbackWrite(
        VOID
        )
    {
        AddRef();
        return static_cast<IQueueCallbackWrite *>(this);
    }

    IQueueCallbackRead *
    QueryIQueueCallbackRead(
        VOID
        )
    {
        AddRef();
        return static_cast<IQueueCallbackRead *>(this);
    }

    IQueueCallbackDeviceIoControl *
    QueryIQueueCallbackDeviceIoControl(
        VOID
        )
    {
        AddRef();
        return static_cast<IQueueCallbackDeviceIoControl *>(this);
    }

    IRequestCallbackRequestCompletion *
    QueryIRequestCallbackRequestCompletion(
        VOID
        )
    {
        AddRef();
        return static_cast<IRequestCallbackRequestCompletion *>(this);
    }

    //
    // IUnknown
    //

    STDMETHOD_(ULONG,AddRef) (VOID) {return CUnknown::AddRef();}

    _At_(this, __drv_freesMem(object))
    STDMETHOD_(ULONG,Release) (VOID) {return CUnknown::Release();}

    STDMETHOD_(HRESULT, QueryInterface)(
        _In_ REFIID InterfaceId, 
        _Outptr_ PVOID *Object
        );


    //
    // Helper routine to do the cleanup. 
    // 
    //

    void OnCleanupFile( 
        _In_ IWDFFile *pWdfFileObject
        ); 


    //
    // Wdf Callbacks
    //

    //
    // IQueueCallbackCreate
    //    
    STDMETHOD_ (void, OnCreateFile)(
        _In_ IWDFIoQueue *   pWdfQueue,
        _In_ IWDFIoRequest * pWdfRequest,
        _In_ IWDFFile *      pWdfFileObject
        );

    //
    // IQueueCallbackDeviceIoControl
    //
    STDMETHOD_ (void, OnDeviceIoControl)( 
        _In_ IWDFIoQueue *pWdfQueue,
        _In_ IWDFIoRequest *pWdfRequest,
        _In_ ULONG ControlCode,
        _In_ SIZE_T InputBufferSizeInBytes,
        _In_ SIZE_T OutputBufferSizeInBytes
           );

    //
    // IQueueCallbackWrite
    //
    STDMETHOD_ (void, OnWrite)(
        _In_ IWDFIoQueue *pWdfQueue,
        _In_ IWDFIoRequest *pWdfRequest,
        _In_ SIZE_T NumOfBytesToWrite
        );

    //
    // IQueueCallbackRead
    //
    STDMETHOD_ (void, OnRead)(
        _In_ IWDFIoQueue *pWdfQueue,
        _In_ IWDFIoRequest *pWdfRequest,
        _In_ SIZE_T NumOfBytesToRead
        );

    //
    //IRequestCallbackRequestCompletion
    //
    
    STDMETHOD_ (void, OnCompletion)(
        IWDFIoRequest*                 pWdfRequest,
        IWDFIoTarget*                  pIoTarget,
        IWDFRequestCompletionParams*   pParams,
        PVOID                          pContext
        );    
};
