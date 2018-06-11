/*++

Copyright (c) 1997-2000  Microsoft Corporation All Rights Reserved

Module Name:

    common.cpp

Abstract:

    Implementation of the AdapterCommon class. 

--*/

#pragma warning (disable : 4127)

#include <msvad.h>
#include "common.h"
#include "hw.h"

//=============================================================================
// Classes
//=============================================================================

///////////////////////////////////////////////////////////////////////////////
// CAdapterCommon
//   

class CAdapterCommon : 
    public IAdapterCommon,
    public IAdapterPowerManagement,
    public CUnknown    
{
    private:
        PPORTWAVERT             m_pPortWave;           // Port interface
        PPORTWAVERT             m_pPortWaveCapture;    // Port interface
        PSERVICEGROUP           m_pServiceGroupWave;
        PDEVICE_OBJECT          m_pDeviceObject;      
        DEVICE_POWER_STATE      m_PowerState;        

        PCMSVADHW               m_pHW;          // Virtual MSVAD HW object
        PPORTCLSETWHELPER       m_pPortClsEtwHelper;
    public:
        //=====================================================================
        // Default CUnknown
        DECLARE_STD_UNKNOWN();
        DEFINE_STD_CONSTRUCTOR(CAdapterCommon);
        ~CAdapterCommon();

        //=====================================================================
        // Default IAdapterPowerManagement
        IMP_IAdapterPowerManagement;

        //=====================================================================
        // IAdapterCommon methods      

        STDMETHODIMP_(NTSTATUS) Init
        (   
            IN  PDEVICE_OBJECT  DeviceObject
        );

        STDMETHODIMP_(PDEVICE_OBJECT)   GetDeviceObject(void);

        STDMETHODIMP_(PUNKNOWN *)       WavePortDriverDest(BOOL _bIsCapture);

        STDMETHODIMP_(void)     SetWaveServiceGroup
        (   
            IN  PSERVICEGROUP   ServiceGroup
        );

        STDMETHODIMP_(BOOL)     bDevSpecificRead();

        STDMETHODIMP_(void)     bDevSpecificWrite
        (
            IN  BOOL            bDevSpecific
        );
        STDMETHODIMP_(INT)      iDevSpecificRead();

        STDMETHODIMP_(void)     iDevSpecificWrite
        (
            IN  INT             iDevSpecific
        );
        STDMETHODIMP_(UINT)     uiDevSpecificRead();

        STDMETHODIMP_(void)     uiDevSpecificWrite
        (
            IN  UINT            uiDevSpecific
        );

        STDMETHODIMP_(BOOL)     MixerMuteRead
        (
            IN  ULONG           Index
        );

        STDMETHODIMP_(void)     MixerMuteWrite
        (
            IN  ULONG           Index,
            IN  BOOL            Value
        );

        STDMETHODIMP_(ULONG)    MixerMuxRead(void);

        STDMETHODIMP_(void)     MixerMuxWrite
        (
            IN  ULONG           Index
        );

        STDMETHODIMP_(void)     MixerReset(void);

        STDMETHODIMP_(LONG)     MixerVolumeRead
        ( 
            IN  ULONG           Index,
            IN  LONG            Channel
        );

        STDMETHODIMP_(void)     MixerVolumeWrite
        ( 
            IN  ULONG           Index,
            IN  LONG            Channel,
            IN  LONG            Value 
        );
        STDMETHOD_(NTSTATUS,        WriteEtwEvent) 
        ( 
            IN EPcMiniportEngineEvent    miniportEventType,
            IN ULONGLONG  ullData1,
            IN ULONGLONG  ullData2,
            IN ULONGLONG  ullData3,
            IN ULONGLONG  ullData4
        );

        STDMETHOD_(VOID,            SetEtwHelper) 
        ( 
            PPORTCLSETWHELPER _pPortClsEtwHelper
        );


        //=====================================================================
        // friends

        friend NTSTATUS         NewAdapterCommon
        ( 
            OUT PADAPTERCOMMON * OutAdapterCommon, 
            IN  PRESOURCELIST   ResourceList 
        );
};

//-----------------------------------------------------------------------------
// Functions
//-----------------------------------------------------------------------------

//=============================================================================
#pragma code_seg("PAGE")
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
)
/*++

Routine Description:

  Creates a new CAdapterCommon

Arguments:

  Unknown - 

  UnknownOuter -

  PoolType

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    ASSERT(Unknown);

    STD_CREATE_BODY_
    ( 
        CAdapterCommon, 
        Unknown, 
        UnknownOuter, 
        PoolType,      
        PADAPTERCOMMON 
    );
} // NewAdapterCommon

//=============================================================================
CAdapterCommon::~CAdapterCommon
( 
    void 
)
/*++

Routine Description:

  Destructor for CAdapterCommon.

Arguments:

Return Value:

  void

--*/
{
    PAGED_CODE();

    DPF_ENTER(("[CAdapterCommon::~CAdapterCommon]"));

    if (m_pHW)
    {
        delete m_pHW;
    }

    if (m_pPortClsEtwHelper)
    {
        m_pPortClsEtwHelper->Release();
    }

    if (m_pPortWave)
    {
        m_pPortWave->Release();
    }

    if (m_pPortWaveCapture)
    {
        m_pPortWaveCapture->Release();
    }

    if (m_pServiceGroupWave)
    {
        m_pServiceGroupWave->Release();
    }
} // ~CAdapterCommon  

//=============================================================================
STDMETHODIMP_(PDEVICE_OBJECT)   
CAdapterCommon::GetDeviceObject
(
    void
)
/*++

Routine Description:

  Returns the deviceobject

Arguments:

Return Value:

  PDEVICE_OBJECT

--*/
{
    PAGED_CODE();
    
    return m_pDeviceObject;
} // GetDeviceObject

//=============================================================================
NTSTATUS
CAdapterCommon::Init
( 
    IN  PDEVICE_OBJECT          DeviceObject 
)
/*++

Routine Description:

    Initialize adapter common object.

Arguments:

    DeviceObject - pointer to the device object

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    ASSERT(DeviceObject);

    NTSTATUS                    ntStatus = STATUS_SUCCESS;

    DPF_ENTER(("[CAdapterCommon::Init]"));

    m_pDeviceObject = DeviceObject;
    m_PowerState    = PowerDeviceD0;

    // Initialize HW.
    // 
    m_pHW = new (NonPagedPool, MSVAD_POOLTAG)  CMSVADHW;
    if (!m_pHW)
    {
        DPF(D_TERSE, ("Insufficient memory for MSVAD HW"));
        ntStatus = STATUS_INSUFFICIENT_RESOURCES;
    }
    else
    {
        m_pHW->MixerReset();
    }

    return ntStatus;
} // Init

//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::MixerReset
( 
    void 
)
/*++

Routine Description:

  Reset mixer registers from registry.

Arguments:

Return Value:

  void

--*/
{
    PAGED_CODE();
    
    if (m_pHW)
    {
        m_pHW->MixerReset();
    }
} // MixerReset

//=============================================================================
/* Here are the definitions of the standard miniport events.

Event type	: eMINIPORT_IHV_DEFINED 
Parameter 1	: Defined and used by IHVs
Parameter 2	: Defined and used by IHVs
Parameter 3	:Defined and used by IHVs
Parameter 4 :Defined and used by IHVs

Event type: eMINIPORT_BUFFER_COMPLETE
Parameter 1: Current linear buffer position	
Parameter 2: the previous WaveRtBufferWritePosition that the drive received	
Parameter 3: Data length completed
Parameter 4:0

Event type: eMINIPORT_PIN_STATE
Parameter 1: Current linear buffer position	
Parameter 2: the previous WaveRtBufferWritePosition that the drive received		
Parameter 3: Pin State 0->KS_STOP, 1->KS_ACQUIRE, 2->KS_PAUSE, 3->KS_RUN 
Parameter 4:0

Event type: eMINIPORT_GET_STREAM_POS
Parameter 1: Current linear buffer position	
Parameter 2: the previous WaveRtBufferWritePosition that the drive received	
Parameter 3: 0
Parameter 4:0


Event type: eMINIPORT_SET_WAVERT_BUFFER_WRITE_POS
Parameter 1: Current linear buffer position	
Parameter 2: the previous WaveRtBufferWritePosition that the drive received	
Parameter 3: the arget WaveRtBufferWritePosition received from portcls
Parameter 4:0

Event type: eMINIPORT_GET_PRESENTATION_POS
Parameter 1: Current linear buffer position	
Parameter 2: the previous WaveRtBufferWritePosition that the drive received	
Parameter 3: Presentation position
Parameter 4:0

Event type: eMINIPORT_PROGRAM_DMA 
Parameter 1: Current linear buffer position	
Parameter 2: the previous WaveRtBufferWritePosition that the drive received	
Parameter 3: Starting  WaveRt buffer offset
Parameter 4: Data length

Event type: eMINIPORT_GLITCH_REPORT
Parameter 1: Current linear buffer position	
Parameter 2: the previous WaveRtBufferWritePosition that the drive received	
Parameter 3: major glitch code: 1:WaveRT buffer is underrun, 
                                2:decoder errors, 
                                3:receive the same wavert buffer two in a row in event driven mode
Parameter 4: minor code for the glitch cause

*/
#pragma code_seg()
STDMETHODIMP
CAdapterCommon::WriteEtwEvent
( 
    IN EPcMiniportEngineEvent    miniportEventType,
    IN ULONGLONG  ullData1,
    IN ULONGLONG  ullData2,
    IN ULONGLONG  ullData3,
    IN ULONGLONG  ullData4
)
{
    NTSTATUS ntStatus = STATUS_SUCCESS;

    if (m_pPortClsEtwHelper)
    {
        ntStatus = m_pPortClsEtwHelper->MiniportWriteEtwEvent( miniportEventType, ullData1, ullData2, ullData3, ullData4) ;
    }
    return ntStatus;
} // WriteEtwEvent

#pragma code_seg("PAGE")
//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::SetEtwHelper
( 
    PPORTCLSETWHELPER _pPortClsEtwHelper
)
{
    PAGED_CODE();

    m_pPortClsEtwHelper = _pPortClsEtwHelper;
} // SetEtwHelper

//=============================================================================
STDMETHODIMP
CAdapterCommon::NonDelegatingQueryInterface
( 
    _In_ REFIID                      Interface,
    _COM_Outptr_ PVOID *        Object 
)
/*++

Routine Description:

  QueryInterface routine for AdapterCommon

Arguments:

  Interface - 

  Object -

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    ASSERT(Object);

    if (IsEqualGUIDAligned(Interface, IID_IUnknown))
    {
        *Object = PVOID(PUNKNOWN(PADAPTERCOMMON(this)));
    }
    else if (IsEqualGUIDAligned(Interface, IID_IAdapterCommon))
    {
        *Object = PVOID(PADAPTERCOMMON(this));
    }
    else if (IsEqualGUIDAligned(Interface, IID_IAdapterPowerManagement))
    {
        *Object = PVOID(PADAPTERPOWERMANAGEMENT(this));
    }
    else
    {
        *Object = NULL;
    }

    if (*Object)
    {
        PUNKNOWN(*Object)->AddRef();
        return STATUS_SUCCESS;
    }

    return STATUS_INVALID_PARAMETER;
} // NonDelegatingQueryInterface

//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::SetWaveServiceGroup
( 
    IN PSERVICEGROUP            ServiceGroup 
)
/*++

Routine Description:


Arguments:

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();
    
    DPF_ENTER(("[CAdapterCommon::SetWaveServiceGroup]"));
    
    if (m_pServiceGroupWave)
    {
        m_pServiceGroupWave->Release();
    }

    m_pServiceGroupWave = ServiceGroup;

    if (m_pServiceGroupWave)
    {
        m_pServiceGroupWave->AddRef();
    }
} // SetWaveServiceGroup

//=============================================================================
STDMETHODIMP_(PUNKNOWN *)
CAdapterCommon::WavePortDriverDest
( 
    BOOL _bIsCapture 
)
/*++

Routine Description:

  Returns the wave port.

Arguments:

Return Value:

  PUNKNOWN : pointer to waveport

--*/
{
    PAGED_CODE();

    if (_bIsCapture)
    {
        return (PUNKNOWN *)&m_pPortWaveCapture;
    }
    else
    {
        return (PUNKNOWN *)&m_pPortWave;
    }

} // WavePortDriverDest
#pragma code_seg()

//=============================================================================
STDMETHODIMP_(BOOL)
CAdapterCommon::bDevSpecificRead()
/*++

Routine Description:

  Fetch Device Specific information.

Arguments:

  N/A

Return Value:

    BOOL - Device Specific info

--*/
{
    if (m_pHW)
    {
        return m_pHW->bGetDevSpecific();
    }

    return FALSE;
} // bDevSpecificRead

//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::bDevSpecificWrite
(
    IN  BOOL                    bDevSpecific
)
/*++

Routine Description:

  Store the new value in the Device Specific location.

Arguments:

  bDevSpecific - Value to store

Return Value:

  N/A.

--*/
{
    if (m_pHW)
    {
        m_pHW->bSetDevSpecific(bDevSpecific);
    }
} // DevSpecificWrite

//=============================================================================
STDMETHODIMP_(INT)
CAdapterCommon::iDevSpecificRead()
/*++

Routine Description:

  Fetch Device Specific information.

Arguments:

  N/A

Return Value:

    INT - Device Specific info

--*/
{
    if (m_pHW)
    {
        return m_pHW->iGetDevSpecific();
    }

    return 0;
} // iDevSpecificRead

//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::iDevSpecificWrite
(
    IN  INT                    iDevSpecific
)
/*++

Routine Description:

  Store the new value in the Device Specific location.

Arguments:

  iDevSpecific - Value to store

Return Value:

  N/A.

--*/
{
    if (m_pHW)
    {
        m_pHW->iSetDevSpecific(iDevSpecific);
    }
} // iDevSpecificWrite

//=============================================================================
STDMETHODIMP_(UINT)
CAdapterCommon::uiDevSpecificRead()
/*++

Routine Description:

  Fetch Device Specific information.

Arguments:

  N/A

Return Value:

    UINT - Device Specific info

--*/
{
    if (m_pHW)
    {
        return m_pHW->uiGetDevSpecific();
    }

    return 0;
} // uiDevSpecificRead

//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::uiDevSpecificWrite
(
    IN  UINT                    uiDevSpecific
)
/*++

Routine Description:

  Store the new value in the Device Specific location.

Arguments:

  uiDevSpecific - Value to store

Return Value:

  N/A.

--*/
{
    if (m_pHW)
    {
        m_pHW->uiSetDevSpecific(uiDevSpecific);
    }
} // uiDevSpecificWrite

//=============================================================================
STDMETHODIMP_(BOOL)
CAdapterCommon::MixerMuteRead
(
    IN  ULONG                   Index
)
/*++

Routine Description:

  Store the new value in mixer register array.

Arguments:

  Index - node id

Return Value:

    BOOL - mixer mute setting for this node

--*/
{
    if (m_pHW)
    {
        return m_pHW->GetMixerMute(Index);
    }

    return 0;
} // MixerMuteRead

//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::MixerMuteWrite
(
    IN  ULONG                   Index,
    IN  BOOL                    Value
)
/*++

Routine Description:

  Store the new value in mixer register array.

Arguments:

  Index - node id

  Value - new mute settings

Return Value:

  NT status code.

--*/
{
    if (m_pHW)
    {
        m_pHW->SetMixerMute(Index, Value);
    }
} // MixerMuteWrite

//=============================================================================
STDMETHODIMP_(ULONG)
CAdapterCommon::MixerMuxRead() 
/*++

Routine Description:

  Return the mux selection

Arguments:

  Index - node id

  Value - new mute settings

Return Value:

  NT status code.

--*/
{
    if (m_pHW)
    {
        return m_pHW->GetMixerMux();
    }

    return 0;
} // MixerMuxRead

//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::MixerMuxWrite
(
    IN  ULONG                   Index
)
/*++

Routine Description:

  Store the new mux selection

Arguments:

  Index - node id

  Value - new mute settings

Return Value:

  NT status code.

--*/
{
    if (m_pHW)
    {
        m_pHW->SetMixerMux(Index);
    }
} // MixerMuxWrite

//=============================================================================
STDMETHODIMP_(LONG)
CAdapterCommon::MixerVolumeRead
( 
    IN  ULONG                   Index,
    IN  LONG                    Channel
)
/*++

Routine Description:

  Return the value in mixer register array.

Arguments:

  Index - node id

  Channel = which channel

Return Value:

    Byte - mixer volume settings for this line

--*/
{
    if (m_pHW)
    {
        return m_pHW->GetMixerVolume(Index, Channel);
    }

    return 0;
} // MixerVolumeRead

//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::MixerVolumeWrite
( 
    IN  ULONG                   Index,
    IN  LONG                    Channel,
    IN  LONG                    Value
)
/*++

Routine Description:

  Store the new value in mixer register array.

Arguments:

  Index - node id

  Channel - which channel

  Value - new volume level

Return Value:

    void

--*/
{
    if (m_pHW)
    {
        m_pHW->SetMixerVolume(Index, Channel, Value);
    }
} // MixerVolumeWrite

//=============================================================================
STDMETHODIMP_(void)
CAdapterCommon::PowerChangeState
( 
    _In_  POWER_STATE             NewState 
)
/*++

Routine Description:


Arguments:

  NewState - The requested, new power state for the device. 

Return Value:

    void

--*/
{
    DPF_ENTER(("[CAdapterCommon::PowerChangeState]"));

    // is this actually a state change??
    //
    if (NewState.DeviceState != m_PowerState)
    {
        // switch on new state
        //
        switch (NewState.DeviceState)
        {
            case PowerDeviceD0:
            case PowerDeviceD1:
            case PowerDeviceD2:
            case PowerDeviceD3:
                m_PowerState = NewState.DeviceState;

                DPF
                ( 
                    D_VERBOSE, 
                    ("Entering D%d", ULONG(m_PowerState) - ULONG(PowerDeviceD0)) 
                );

                break;
    
            default:
            
                DPF(D_VERBOSE, ("Unknown Device Power State"));
                break;
        }
    }
} // PowerStateChange

//=============================================================================
STDMETHODIMP_(NTSTATUS)
CAdapterCommon::QueryDeviceCapabilities
( 
    _Inout_updates_bytes_(sizeof(DEVICE_CAPABILITIES)) PDEVICE_CAPABILITIES    PowerDeviceCaps 
)
/*++

Routine Description:

    Called at startup to get the caps for the device.  This structure provides 
    the system with the mappings between system power state and device power 
    state.  This typically will not need modification by the driver.         

Arguments:

  PowerDeviceCaps - The device's capabilities. 

Return Value:

  NT status code.

--*/
{
    UNREFERENCED_PARAMETER(PowerDeviceCaps);

    DPF_ENTER(("[CAdapterCommon::QueryDeviceCapabilities]"));

    return (STATUS_SUCCESS);
} // QueryDeviceCapabilities

//=============================================================================
STDMETHODIMP_(NTSTATUS)
CAdapterCommon::QueryPowerChangeState
( 
    _In_  POWER_STATE             NewStateQuery 
)
/*++

Routine Description:

  Query to see if the device can change to this power state 

Arguments:

  NewStateQuery - The requested, new power state for the device

Return Value:

  NT status code.

--*/
{
    UNREFERENCED_PARAMETER(NewStateQuery);

    DPF_ENTER(("[CAdapterCommon::QueryPowerChangeState]"));

    return STATUS_SUCCESS;
} // QueryPowerChangeState

