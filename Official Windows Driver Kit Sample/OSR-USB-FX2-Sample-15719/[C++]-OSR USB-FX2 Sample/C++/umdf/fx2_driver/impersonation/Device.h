/*++

Copyright (C) Microsoft Corporation, All Rights Reserved

Module Name:

    Device.h

Abstract:

    This module contains the type definitions for the UMDF Skeleton sample
    driver's device callback class.

Environment:

    Windows User-Mode Driver Framework (WUDF)

--*/

#pragma once
#include "internal.h"

#define ENDPOINT_TIMEOUT        10000
#define NUM_OSRUSB_ENDPOINTS    3

//
// Define the vendor commands supported by our device
//
#define USBFX2LK_READ_7SEGMENT_DISPLAY      0xD4
#define USBFX2LK_READ_SWITCHES              0xD6
#define USBFX2LK_READ_BARGRAPH_DISPLAY      0xD7
#define USBFX2LK_SET_BARGRAPH_DISPLAY       0xD8
#define USBFX2LK_IS_HIGH_SPEED              0xD9
#define USBFX2LK_REENUMERATE                0xDA
#define USBFX2LK_SET_7SEGMENT_DISPLAY       0xDB

typedef struct {
    UCHAR Segments;
} SEVEN_SEGMENT, *PSEVEN_SEGMENT;

//
// Context for the impersonation callback.
//

typedef struct 
{
    PFILE_PLAYBACK PlaybackInfo;

    HANDLE FileHandle;

    HRESULT Hr;
} PLAYBACK_IMPERSONATION_CONTEXT, *PPLAYBACK_IMPERSONATION_CONTEXT;

//
// Class for the iotrace driver.
//

class CMyDevice : 
    public CUnknown,
    public IPnpCallbackHardware, 
    public IImpersonateCallback
{

//
// Private data members.
//
private:
    //
    // Weak reference to framework device
    //    
    IWDFDevice *m_FxDevice;

    // 
    // Weak reference to the control queue
    //
    PCMyReadWriteQueue      m_ReadWriteQueue;

    // 
    // Weak reference to the control queue
    //
    PCMyControlQueue        m_ControlQueue;

    //
    // The bus type for this device (used here for
    // illustrative purposes only)
    //

    GUID m_BusTypeGuid;

    //
    // USB Device I/O Target
    //
    IWDFUsbTargetDevice *   m_pIUsbTargetDevice;

    //
    // USB Interface
    //
    IWDFUsbInterface *      m_pIUsbInterface;

    //
    // USB Input pipe for Reads
    //
    IWDFUsbTargetPipe *     m_pIUsbInputPipe;

    //
    // USB Output pipe for writes
    //
    IWDFUsbTargetPipe *     m_pIUsbOutputPipe;

    //
    // Device Speed (Low, Full, High)
    //
    UCHAR                   m_Speed;    

//
// Private methods.
//

private:

    CMyDevice(
        VOID
        ) :
        m_FxDevice(NULL),
        m_ControlQueue(NULL),            
        m_ReadWriteQueue(NULL),
        m_pIUsbTargetDevice(NULL),
        m_pIUsbInterface(NULL),
        m_pIUsbInputPipe(NULL),
        m_pIUsbOutputPipe(NULL),
        m_Speed(0)
    {
        ZeroMemory(&m_BusTypeGuid, sizeof(m_BusTypeGuid));
    }

    ~CMyDevice(
        );

    HRESULT
    Initialize(
        _In_ IWDFDriver *FxDriver,
        _In_ IWDFDeviceInitialize *FxDeviceInit
        );

    //
    // Helper methods
    //

    HRESULT
    GetBusTypeGuid(
        VOID
        );

    HRESULT
    CreateUsbIoTargets(
        VOID
        );

    HRESULT
    ConfigureUsbPipes(
        );

    HRESULT
    IndicateDeviceReady(
        VOID
        );

    //
    // Helper functions
    //
    
    HRESULT
    SendControlTransferSynchronously(
        _In_ PWINUSB_SETUP_PACKET SetupPacket,
        _Inout_updates_(BufferLength) PBYTE Buffer,
        _In_ ULONG BufferLength,
        _Out_ PULONG LengthTransferred
        );

    static
    WDF_IO_TARGET_STATE
    GetTargetState(
        IWDFIoTarget * pTarget
        );

//
// Public methods
//
public:

    //
    // The factory method used to create an instance of this driver.
    //
    
    static
    HRESULT
    CreateInstance(
        _In_ IWDFDriver *FxDriver,
        _In_ IWDFDeviceInitialize *FxDeviceInit,
        _Out_ PCMyDevice *Device
        );

    IWDFDevice *
    GetFxDevice(
        VOID
        )
    {
        return m_FxDevice;
    }

    HRESULT
    Configure(
        VOID
        );

    IPnpCallbackHardware *
    QueryIPnpCallbackHardware(
        VOID
        )
    {
        AddRef();
        return static_cast<IPnpCallbackHardware *>(this);
    }

    IImpersonateCallback *
    QueryIImpersonateCallback(
        VOID
        )
    {
        AddRef();
        return static_cast<IImpersonateCallback *>(this);
   }

    HRESULT
    GetBarGraphDisplay(
        _In_ PBAR_GRAPH_STATE BarGraphState
        );

    HRESULT
    SetBarGraphDisplay(
        _In_ PBAR_GRAPH_STATE BarGraphState
        );

    HRESULT
    GetSevenSegmentDisplay(
        _In_ PSEVEN_SEGMENT SevenSegment
        );

    HRESULT
    SetSevenSegmentDisplay(
        _In_ PSEVEN_SEGMENT SevenSegment
        );

    bool
    EncodeSegmentValue(
        _In_ UCHAR Character,
        _Out_ SEVEN_SEGMENT *SevenSegment
        );

    HRESULT
    ReadSwitchState(
        _In_ PSWITCH_STATE SwitchState
        );

    HRESULT
    PlaybackFile(
        _In_ PFILE_PLAYBACK PlaybackInfo,
        _In_ IWDFIoRequest *FxRequest
        );

    //
    //returns a weak reference to the target USB device
    //DO NOT release it
    //
    IWDFUsbTargetDevice *
    GetUsbTargetDevice(
        )
    {
        return m_pIUsbTargetDevice;
    }

    //
    //returns a weak reference to input pipe
    //DO NOT release it
    //
    IWDFUsbTargetPipe *
    GetInputPipe(
        )
    {
        return m_pIUsbInputPipe;
    }

    //
    //returns a weak reference to output pipe
    //DO NOT release it
    //
    IWDFUsbTargetPipe *
    GetOutputPipe(
        )
    {
        return m_pIUsbOutputPipe;
    }


//
// COM methods
//
public:

    //
    // IUnknown methods.
    //

    virtual
    ULONG
    STDMETHODCALLTYPE
    AddRef(
        VOID
        )
    {
        return __super::AddRef();
    }

    _At_(this, __drv_freesMem(object))
    virtual
    ULONG
    STDMETHODCALLTYPE
    Release(
        VOID
       )
    {
        return __super::Release();
    }

    virtual
    HRESULT
    STDMETHODCALLTYPE
    QueryInterface(
        _In_ REFIID InterfaceId,
        _Outptr_ PVOID *Object
        );

    //
    // IPnpCallbackHardware
    //

    virtual
    HRESULT
    STDMETHODCALLTYPE
    OnPrepareHardware(
            _In_ IWDFDevice *FxDevice
            );

    virtual
    HRESULT
    STDMETHODCALLTYPE
    OnReleaseHardware(
        _In_ IWDFDevice *FxDevice
        );

    //
    // IImpersonateCallback
    //

    virtual
    VOID
    STDMETHODCALLTYPE
    OnImpersonate(
        _In_ PVOID Context
        );
};

