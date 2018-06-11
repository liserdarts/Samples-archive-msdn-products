/*++

Copyright (c) 1997-2000  Microsoft Corporation All Rights Reserved

Module Name:

    Common.h

Abstract:
    
    CAdapterCommon class declaration.

--*/

#ifndef _MSVAD_COMMON_H_
#define _MSVAD_COMMON_H_

//=============================================================================
// Macros
//=============================================================================

//-------------------------------------------------------------------------
// Description:
//
// If the condition evaluates to TRUE, perform the given statement
// then jump to the given label.
//
// Parameters:
//
//      condition - [in] Code that fits in if statement
//      action - [in] action to perform in body of if statement
//      label - [in] label to jump if condition is met
//
#define IF_TRUE_ACTION_JUMP(condition, action, label)           \
    if(condition)                                               \
    {                                                           \
        action;                                                 \
        goto label;                                             \
    }
//-------------------------------------------------------------------------
// Description:
//
// If the hresult passed FAILED, jump to the given label.
//
// Parameters:
//
//      _hresult - [in] Value to check
//      label - [in] label to jump if condition is met
//
#define IF_FAILED_JUMP(ntStatus, label)                         \
    if(!NT_SUCCESS(ntStatus))                                        \
    {                                                           \
        goto label;                                             \
    }


#define MINWAVERT_POOLTAG 'RWNM'

typedef enum
{
    eSpeakersDevice,
    eMicInDevice
} eDeviceType;

// both wave & topology miniport create function prototypes have this form:
typedef HRESULT (*PFNCREATEMINIPORT)(
    OUT PUNKNOWN *  Unknown,
    IN  REFCLSID    ClassId,
    IN  PUNKNOWN    OuterUnknown,
    IN  POOL_TYPE   PoolType,
    IN  eDeviceType     deviceType,
    IN  PCFILTER_DESCRIPTOR *pDesc, 
    IN  PUNKNOWN  UnknownAdapter
);
//=============================================================================
// Defines
//=============================================================================

DEFINE_GUID(IID_IAdapterCommon,
0x7eda2950, 0xbf9f, 0x11d0, 0x87, 0x1f, 0x0, 0xa0, 0xc9, 0x11, 0xb5, 0x44);

//=============================================================================
// Interfaces
//=============================================================================

///////////////////////////////////////////////////////////////////////////////
// IAdapterCommon
//
DECLARE_INTERFACE_(IAdapterCommon, IUnknown)
{
    STDMETHOD_(NTSTATUS,        Init) 
    ( 
        THIS_
        IN  PDEVICE_OBJECT      DeviceObject 
    ) PURE;

    STDMETHOD_(PDEVICE_OBJECT,  GetDeviceObject)
    (
        THIS
    ) PURE;

    STDMETHOD_(VOID,            SetWaveServiceGroup) 
    ( 
        THIS_
        IN PSERVICEGROUP        ServiceGroup 
    ) PURE;

    STDMETHOD_(PUNKNOWN *,      WavePortDriverDest) 
    ( 
        THIS_ 
        IN  BOOL                bIsCapture
    ) PURE;

    STDMETHOD_(BOOL,            bDevSpecificRead)
    (
        THIS_
    ) PURE;

    STDMETHOD_(VOID,            bDevSpecificWrite)
    (
        THIS_
        IN  BOOL                bDevSpecific
    );

    STDMETHOD_(INT,             iDevSpecificRead)
    (
        THIS_
    ) PURE;

    STDMETHOD_(VOID,            iDevSpecificWrite)
    (
        THIS_
        IN  INT                 iDevSpecific
    );

    STDMETHOD_(UINT,            uiDevSpecificRead)
    (
        THIS_
    ) PURE;

    STDMETHOD_(VOID,            uiDevSpecificWrite)
    (
        THIS_
        IN  UINT                uiDevSpecific
    );

    STDMETHOD_(BOOL,            MixerMuteRead)
    (
        THIS_
        IN  ULONG               Index
    ) PURE;

    STDMETHOD_(VOID,            MixerMuteWrite)
    (
        THIS_
        IN  ULONG               Index,
        IN  BOOL                Value
    );

    STDMETHOD_(ULONG,           MixerMuxRead)
    (
        THIS
    );

    STDMETHOD_(VOID,            MixerMuxWrite)
    (
        THIS_
        IN  ULONG               Index
    );

    STDMETHOD_(LONG,            MixerVolumeRead) 
    ( 
        THIS_
        IN  ULONG               Index,
        IN  LONG                Channel
    ) PURE;

    STDMETHOD_(VOID,            MixerVolumeWrite) 
    ( 
        THIS_
        IN  ULONG               Index,
        IN  LONG                Channel,
        IN  LONG                Value 
    ) PURE;

    STDMETHOD_(VOID,            MixerReset) 
    ( 
        THIS 
    ) PURE;

    STDMETHOD_(NTSTATUS,        WriteEtwEvent) 
    ( 
        THIS_ 
        IN EPcMiniportEngineEvent    miniportEventType,
        IN ULONGLONG  ullData1,
        IN ULONGLONG  ullData2,
        IN ULONGLONG  ullData3,
        IN ULONGLONG  ullData4
    ) PURE;

    STDMETHOD_(VOID,            SetEtwHelper) 
    ( 
        THIS_
        PPORTCLSETWHELPER _pPortClsEtwHelper
    ) PURE;
};

typedef IAdapterCommon *PADAPTERCOMMON;

//=============================================================================
// Function Prototypes
//=============================================================================
NTSTATUS
NewAdapterCommon
( 
    OUT PUNKNOWN *              Unknown,
    IN  REFCLSID,
    IN  PUNKNOWN                UnknownOuter OPTIONAL,
    _When_((PoolType & NonPagedPoolMustSucceed) != 0,
        __drv_reportError("Must succeed pool allocations are forbidden. "
			    "Allocation failures cause a system crash"))
    IN  POOL_TYPE               PoolType 
);

#endif  //_COMMON_H_

