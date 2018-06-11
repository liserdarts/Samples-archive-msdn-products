/*++

Copyright (c) 1997-2000  Microsoft Corporation All Rights Reserved

Module Name:

    minwavert.cpp

Abstract:

    Implementation of wavert miniport.

--*/

#pragma warning (disable : 4127)

#include <msvad.h>
#include <common.h>
#include <limits.h>
#include "simple.h"
#include "minwavert.h"
#include "minwavertstream.h"
#include "IHVPrivatePropertySet.h"
#include "wavtable.h"


//=============================================================================
// CMiniportWaveRT
//=============================================================================

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS
CreateMiniportWaveRTMSVAD
( 
    OUT PUNKNOWN *              Unknown,
    IN  REFCLSID,
    IN  PUNKNOWN                UnknownOuter OPTIONAL,
    _When_((PoolType & NonPagedPoolMustSucceed) != 0,
       __drv_reportError("Must succeed pool allocations are forbidden. "
			 "Allocation failures cause a system crash"))
    IN  POOL_TYPE               PoolType,
    IN  eDeviceType             deviceType,
    IN  PCFILTER_DESCRIPTOR     *pDesc,
    IN  PUNKNOWN                UnknownAdapter
)
/*++

Routine Description:

  Create the wavert miniport.

Arguments:

  Unknown - 

  RefClsId -

  UnknownOuter -

  PoolType -

Return Value:

  NT status code.

--*/
{
    UNREFERENCED_PARAMETER(UnknownOuter);

    PAGED_CODE();

    ASSERT(Unknown);

    CMiniportWaveRT *obj = new (PoolType, MINWAVERT_POOLTAG) CMiniportWaveRT(deviceType, pDesc, UnknownAdapter);
    if (NULL == obj)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    obj->AddRef();
    *Unknown = reinterpret_cast<IUnknown*>(obj);

    return STATUS_SUCCESS;
}

//=============================================================================
#pragma code_seg("PAGE")
CMiniportWaveRT::~CMiniportWaveRT
( 
    void 
)
/*++

Routine Description:

  Destructor for wavert miniport

Arguments:

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    DPF_ENTER(("[CMiniportWaveRT::~CMiniportWaveRT]"));

    if (m_pDeviceFormat)
    {
        ExFreePoolWithTag( m_pDeviceFormat, MINWAVERT_POOLTAG );
        m_pDeviceFormat = NULL;
    }

    if (m_pMixFormat)
    {
        ExFreePoolWithTag( m_pMixFormat, MINWAVERT_POOLTAG );
        m_pMixFormat = NULL;
    }

    if (m_pbMuted)
    {
        ExFreePoolWithTag( m_pbMuted, MINWAVERT_POOLTAG );
        m_pbMuted = NULL;
    }

    if (m_plVolumeLevel)
    {
        ExFreePoolWithTag( m_plVolumeLevel, MINWAVERT_POOLTAG );
        m_plVolumeLevel = NULL;
    }

    if (m_plPeakMeter)
    {
        ExFreePoolWithTag( m_plPeakMeter, MINWAVERT_POOLTAG );
        m_plPeakMeter = NULL;
    }
} // ~CMiniportWaveRT

//=============================================================================
#pragma code_seg("PAGE")
STDMETHODIMP_(NTSTATUS)
CMiniportWaveRT::DataRangeIntersection
( 
    _In_        ULONG                       PinId,
    _In_        PKSDATARANGE                ClientDataRange,
    _In_        PKSDATARANGE                MyDataRange,
    _In_        ULONG                       OutputBufferLength,
    _Out_writes_bytes_to_opt_(OutputBufferLength, *ResultantFormatLength)
                PVOID                       ResultantFormat,
    _Out_       PULONG                      ResultantFormatLength 
)
/*++

Routine Description:

  The DataRangeIntersection function determines the highest quality 
  intersection of two data ranges.

Arguments:

  PinId -           Pin for which data intersection is being determined. 

  ClientDataRange - Pointer to KSDATARANGE structure which contains the data 
                    range submitted by client in the data range intersection 
                    property request. 

  MyDataRange -         Pin's data range to be compared with client's data 
                        range. In this case we actually ignore our own data 
                        range, because we know that we only support one range.

  OutputBufferLength -  Size of the buffer pointed to by the resultant format 
                        parameter. 

  ResultantFormat -     Pointer to value where the resultant format should be 
                        returned. 

  ResultantFormatLength -   Actual length of the resultant format placed in 
                            ResultantFormat. This should be less than or equal 
                            to OutputBufferLength. 

  Return Value:

    NT status code.

--*/
{
    UNREFERENCED_PARAMETER(PinId);
    UNREFERENCED_PARAMETER(ClientDataRange);
    UNREFERENCED_PARAMETER(MyDataRange);
    UNREFERENCED_PARAMETER(OutputBufferLength);
    UNREFERENCED_PARAMETER(ResultantFormat);
    UNREFERENCED_PARAMETER(ResultantFormatLength);

    PAGED_CODE();


    return STATUS_NOT_IMPLEMENTED;
} // DataRangeIntersection

//=============================================================================
#pragma code_seg("PAGE")
STDMETHODIMP_(NTSTATUS)
CMiniportWaveRT::GetDescription
( 
    _Out_ PPCFILTER_DESCRIPTOR * OutFilterDescriptor 
)
/*++

Routine Description:

  The GetDescription function gets a pointer to a filter description. 
  It provides a location to deposit a pointer in miniport's description 
  structure.

Arguments:

  OutFilterDescriptor - Pointer to the filter description. 

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    ASSERT(OutFilterDescriptor);

    *OutFilterDescriptor = &m_FilterDesc;

    return STATUS_SUCCESS;
} // GetDescription

//=============================================================================
#pragma code_seg("PAGE")
STDMETHODIMP_(NTSTATUS)
CMiniportWaveRT::Init
( 
    _In_  PUNKNOWN                UnknownAdapter_,
    _In_  PRESOURCELIST           ResourceList_,
    _In_  PPORTWAVERT             Port_ 
)
/*++

Routine Description:

  The Init function initializes the miniport. Callers of this function 
  should run at IRQL PASSIVE_LEVEL

Arguments:

  UnknownAdapter - A pointer to the Iuknown interface of the adapter object. 

  ResourceList - Pointer to the resource list to be supplied to the miniport 
                 during initialization. The port driver is free to examine the 
                 contents of the ResourceList. The port driver will not be 
                 modify the ResourceList contents. 

  Port - Pointer to the topology port object that is linked with this miniport. 

Return Value:

  NT status code.

--*/
{
    UNREFERENCED_PARAMETER(UnknownAdapter_);
    UNREFERENCED_PARAMETER(ResourceList_);
    UNREFERENCED_PARAMETER(Port_);
    PAGED_CODE();

    ASSERT(UnknownAdapter_);
    ASSERT(Port_);

    DPF_ENTER(("[CMiniportWaveRT::Init]"));

    NTSTATUS ntStatus = STATUS_SUCCESS;

    // init class data members
    m_ulLoopbackAllocated = 0;
    m_ulSystemAllocated = 0;
    m_ulOffloadAllocated = 0;
    m_ulMaxSystemStreams = MAX_INPUT_SYSTEM_STREAMS;
    m_ulMaxOffloadStreams = MAX_INPUT_OFFLOAD_STREAMS;
    m_ulMaxLoopbackStreams = MAX_OUTPUT_LOOPBACK_STREAMS;
    m_llProcessingPeriod = 0;
    m_bGfxEnabled = FALSE;
    m_pbMuted = NULL;
    m_plVolumeLevel = NULL;
    m_plPeakMeter = NULL;
    m_pMixFormat= NULL;
    m_pDeviceFormat= NULL;

    m_pDeviceFormat = (PKSDATAFORMAT_WAVEFORMATEXTENSIBLE)ExAllocatePoolWithTag(NonPagedPool, sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE), MINWAVERT_POOLTAG);
    if (m_pDeviceFormat == NULL)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    if (SIZEOF_ARRAY(AudioEngineSupportedDeviceFormats) < 1)
    {
        return STATUS_UNSUCCESSFUL;
    }
    RtlCopyMemory((PVOID)(m_pDeviceFormat), (PVOID)(AudioEngineSupportedDeviceFormats), sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE));

    m_pMixFormat = (PKSDATAFORMAT_WAVEFORMATEXTENSIBLE)ExAllocatePoolWithTag(NonPagedPool, sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE), MINWAVERT_POOLTAG);
    if (m_pMixFormat == NULL)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    m_pMixFormat->DataFormat.FormatSize = sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE);
    m_pMixFormat->DataFormat.Flags = 0;
    m_pMixFormat->DataFormat.Reserved = 0;
    m_pMixFormat->DataFormat.SampleSize = 0;
    m_pMixFormat->DataFormat.MajorFormat = KSDATAFORMAT_TYPE_AUDIO;
    m_pMixFormat->DataFormat.SubFormat = KSDATAFORMAT_SUBTYPE_PCM;
    m_pMixFormat->DataFormat.Specifier = KSDATAFORMAT_SPECIFIER_WAVEFORMATEX;
    m_pMixFormat->WaveFormatExt.Format.wFormatTag = WAVE_FORMAT_EXTENSIBLE;
    m_pMixFormat->WaveFormatExt.Format.nChannels = 2;
    m_pMixFormat->WaveFormatExt.Format.nSamplesPerSec = 48000;
    m_pMixFormat->WaveFormatExt.Format.nBlockAlign = 4;
    m_pMixFormat->WaveFormatExt.Format.nAvgBytesPerSec = 192000;
    m_pMixFormat->WaveFormatExt.Format.wBitsPerSample = 16;
    m_pMixFormat->WaveFormatExt.Format.cbSize = sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX);
    m_pMixFormat->WaveFormatExt.SubFormat = KSDATAFORMAT_SUBTYPE_PCM;
    m_pMixFormat->WaveFormatExt.Samples.wValidBitsPerSample = 16;
    m_pMixFormat->WaveFormatExt.dwChannelMask = KSAUDIO_SPEAKER_STEREO;

    m_llProcessingPeriod = DEFAULT_PROCESSING_PERIOD;

    m_bGfxEnabled = FALSE;

    m_pbMuted = (PBOOL)ExAllocatePoolWithTag(NonPagedPool, DEVICE_MAX_CHANNELS * sizeof(BOOL), MINWAVERT_POOLTAG);
    if (m_pbMuted == NULL)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    RtlZeroMemory(m_pbMuted, DEVICE_MAX_CHANNELS * sizeof(BOOL));

    m_plVolumeLevel = (PLONG)ExAllocatePoolWithTag(NonPagedPool, DEVICE_MAX_CHANNELS * sizeof(LONG), MINWAVERT_POOLTAG);
    if (m_plVolumeLevel == NULL)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    RtlZeroMemory(m_plVolumeLevel, DEVICE_MAX_CHANNELS * sizeof(LONG));

    m_plPeakMeter = (PLONG)ExAllocatePoolWithTag(NonPagedPool, DEVICE_MAX_CHANNELS * sizeof(LONG), MINWAVERT_POOLTAG);
    if (m_plPeakMeter == NULL)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    RtlZeroMemory(m_plPeakMeter, DEVICE_MAX_CHANNELS * sizeof(LONG));

    return ntStatus;

} // Init

//=============================================================================
#pragma code_seg("PAGE")
STDMETHODIMP_(NTSTATUS)
CMiniportWaveRT::NewStream
( 
    _Out_ PMINIPORTWAVERTSTREAM * OutStream,
    _In_  PPORTWAVERTSTREAM       OuterUnknown,
    _In_  ULONG                   Pin,
    _In_  BOOLEAN                 Capture,
    _In_  PKSDATAFORMAT           DataFormat
)
/*++

Routine Description:

  The NewStream function creates a new instance of a logical stream 
  associated with a specified physical channel. Callers of NewStream should 
  run at IRQL PASSIVE_LEVEL.

Arguments:

  OutStream -

  OuterUnknown -

  Pin - 

  Capture - 

  DataFormat -

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    ASSERT(OutStream);
    ASSERT(DataFormat);

    DPF_ENTER(("[CMiniportWaveRT::NewStream]"));

    NTSTATUS                    ntStatus = STATUS_SUCCESS;
    PCMiniportWaveRTStream      stream = NULL;

    // Check if we have enough streams.
    ntStatus = ValidateStreamCreate(Pin, Capture);

    // Determine if the format is valid.
    //
    if (NT_SUCCESS(ntStatus))
    {
        ntStatus = IsFormatSupported(Pin, Capture, DataFormat);
    }

    // Instantiate a stream. Stream must be in
    // NonPagedPool because of file saving.
    //
    if (NT_SUCCESS(ntStatus))
    {
        stream = new (NonPagedPool, MINWAVERT_POOLTAG) 
            CMiniportWaveRTStream(NULL);

        if (stream)
        {
            stream->AddRef();

            ntStatus = 
                stream->Init
                ( 
                    this,
                    OuterUnknown,
                    Pin,
                    Capture,
                    DataFormat
                );
        }
        else
        {
            ntStatus = STATUS_INSUFFICIENT_RESOURCES;
        }
    }

    if (NT_SUCCESS(ntStatus))
    {
        ntStatus = StreamCreated(Pin);

        *OutStream = PMINIPORTWAVERTSTREAM(stream);
        (*OutStream)->AddRef();

        // The stream has references now for the caller.  The caller expects these
        // references to be there.
    }

    // This is our private reference to the stream.  The caller has
    // its own, so we can release in any case.
    //
    if (stream)
    {
        stream->Release();
    }
    
    return ntStatus;
} // NewStream

//=============================================================================
#pragma code_seg("PAGE")
STDMETHODIMP_(NTSTATUS)
CMiniportWaveRT::NonDelegatingQueryInterface
( 
    _In_ REFIID  Interface,
    _COM_Outptr_ PVOID * Object 
)
/*++

Routine Description:

  QueryInterface

Arguments:

  Interface - GUID

  Object - interface pointer to be returned.

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    ASSERT(Object);

    if (IsEqualGUIDAligned(Interface, IID_IUnknown))
    {
        *Object = PVOID(PUNKNOWN(PMINIPORTWAVERT(this)));
    }
    else if (IsEqualGUIDAligned(Interface, IID_IMiniport))
    {
        *Object = PVOID(PMINIPORT(this));
    }
    else if (IsEqualGUIDAligned(Interface, IID_IMiniportWaveRT))
    {
        *Object = PVOID(PMINIPORTWAVERT(this));
    }
#ifdef _SUPPORT_INDEPENDENT_MICIN
    // In this sample, IMiniportAudioEngineNode is supported only for offloading endpoints.
    // at thso moment, offload could only be enabled for  render endpoints not capture.
    // Incorrectly support IMiniportAudioEngineNode interface by the miniport without underlying 
    // HWAudioEngine node will cause miniport::Init to fail.
    else if (IsEqualGUIDAligned(Interface, IID_IMiniportAudioEngineNode) &&
             (m_DeviceType == eSpeakersDevice)) 
#else
    else if (IsEqualGUIDAligned(Interface, IID_IMiniportAudioEngineNode))
#endif
    {
         *Object = (PVOID)(IMiniportAudioEngineNode*)this;
    }
    else
    {
        *Object = NULL;
    }

    if (*Object)
    {
        // We reference the interface for the caller.

        PUNKNOWN(*Object)->AddRef();
        return STATUS_SUCCESS;
    }

    return STATUS_INVALID_PARAMETER;
} // NonDelegatingQueryInterface

//=============================================================================
#pragma code_seg("PAGE")
STDMETHODIMP_(NTSTATUS) CMiniportWaveRT::GetDeviceDescription(_Out_ PDEVICE_DESCRIPTION DmaDeviceDescription)
{
    PAGED_CODE ();

    ASSERT (DmaDeviceDescription);

    DPF_ENTER(("[CMiniportWaveRT::GetDeviceDescription]"));

    RtlZeroMemory (DmaDeviceDescription, sizeof (DEVICE_DESCRIPTION));
    DmaDeviceDescription->Master = TRUE;
    DmaDeviceDescription->ScatterGather = TRUE;
    DmaDeviceDescription->Dma32BitAddresses = TRUE;
    DmaDeviceDescription->InterfaceType = PCIBus;
    DmaDeviceDescription->MaximumLength = 0xFFFFFFFF;

    return STATUS_SUCCESS;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS
CMiniportWaveRT::ValidateStreamCreate
(
    _In_    ULONG   _Pin,
    _In_    BOOLEAN _Capture
)
{
    PAGED_CODE();

    DPF_ENTER(("[CMiniportWaveHelper::ValidateStreamCreate]"));

    NTSTATUS ntStatus = STATUS_SUCCESS;

    if (_Capture)
    {
#ifdef _SUPPORT_INDEPENDENT_MICIN

        if (KSPIN_WAVE_RENDER_SINK_LOOPBACK == _Pin)
        {
            if (m_ulLoopbackAllocated >= m_ulMaxLoopbackStreams)
            {
                ntStatus = STATUS_INSUFFICIENT_RESOURCES;
            }
        }
        else if (KSPIN_WAVEIN_HOST != _Pin)
        {
            ntStatus = STATUS_NOT_SUPPORTED;
        }
#else
        if (KSPIN_WAVE_RENDER_SINK_LOOPBACK != _Pin)
        {
            ntStatus = STATUS_NOT_SUPPORTED;
        }
        else
        {
            if (m_ulLoopbackAllocated >= m_ulMaxLoopbackStreams)
            {
                ntStatus = STATUS_INSUFFICIENT_RESOURCES;
            }
        }
#endif
    }
    else
    {
        if (KSPIN_WAVE_RENDER_SINK_SYSTEM == _Pin)
        {
            if (m_ulSystemAllocated >= m_ulMaxSystemStreams)
            {
                ntStatus = STATUS_INSUFFICIENT_RESOURCES;
            }
        }
        else if (KSPIN_WAVE_RENDER_SINK_OFFLOAD == _Pin)
        {
            if (m_ulOffloadAllocated >= m_ulMaxOffloadStreams)
            {
                ntStatus = STATUS_INSUFFICIENT_RESOURCES;
            }
        }
        else
        {
            ntStatus = STATUS_NOT_SUPPORTED;
        }
    }

    return ntStatus;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS
CMiniportWaveRT::StreamCreated
(
    IN ULONG    _Pin
)
{
    PAGED_CODE();

    DPF_ENTER(("[CMiniportWaveHelper::StreamOpened]"));

    switch(_Pin)
    {
        case KSPIN_WAVE_RENDER_SINK_LOOPBACK:
            m_ulLoopbackAllocated++;
            break;
        case KSPIN_WAVE_RENDER_SINK_SYSTEM:
            m_ulSystemAllocated++;
            break;
        case KSPIN_WAVE_RENDER_SINK_OFFLOAD:
            m_ulOffloadAllocated++;
            break;
    }

    return STATUS_SUCCESS;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS
CMiniportWaveRT::StreamClosed
(
    IN ULONG    _Pin
)
{
    PAGED_CODE();

    DPF_ENTER(("[CMiniportWaveHelper::StreamClosed]"));

    switch(_Pin)
    {
        case KSPIN_WAVE_RENDER_SINK_LOOPBACK:
            m_ulLoopbackAllocated--;
            break;
        case KSPIN_WAVE_RENDER_SINK_SYSTEM:
            m_ulSystemAllocated--;
            break;
        case KSPIN_WAVE_RENDER_SINK_OFFLOAD:
            m_ulOffloadAllocated--;
            break;
    }

    return STATUS_SUCCESS;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS
CMiniportWaveRT::IsFormatSupported
(
    IN ULONG            _ulPin,
    IN BOOLEAN          _bCapture,
    IN PKSDATAFORMAT    _pDataFormat
)
{
    PAGED_CODE();

    DPF_ENTER(("[CMiniportWaveHelper::IsFormatSupported]"));

    NTSTATUS                            ntStatus = STATUS_NO_MATCH;
    PKSDATAFORMAT_WAVEFORMATEXTENSIBLE  pPinFormats = NULL;
    ULONG                               cPinFormats = 0;

#ifdef _SUPPORT_INDEPENDENT_MICIN
    if (_bCapture)
    {
        switch(_ulPin)
        {
            case KSPIN_WAVE_RENDER_SINK_LOOPBACK:
                pPinFormats = LoopbackPinSupportedDeviceFormats;
                cPinFormats = SIZEOF_ARRAY(LoopbackPinSupportedDeviceFormats);
                break;
            case KSPIN_WAVEIN_HOST: // for mic in 
                pPinFormats = MicInPinSupportedDeviceFormats;
                cPinFormats = SIZEOF_ARRAY(MicInPinSupportedDeviceFormats);
                break;
        }
    }
    else
#else
    UNREFERENCED_PARAMETER(_bCapture);
#endif
    {
        switch(_ulPin)
        {
            case KSPIN_WAVE_RENDER_SINK_LOOPBACK:
                pPinFormats = LoopbackPinSupportedDeviceFormats;
                cPinFormats = SIZEOF_ARRAY(LoopbackPinSupportedDeviceFormats);
                break;
            case KSPIN_WAVE_RENDER_SINK_SYSTEM:
                pPinFormats = HostPinSupportedDeviceFormats;
                cPinFormats = SIZEOF_ARRAY(HostPinSupportedDeviceFormats);
                break;
            case KSPIN_WAVE_RENDER_SINK_OFFLOAD:
                pPinFormats = OffloadPinSupportedDeviceFormats;
                cPinFormats = SIZEOF_ARRAY(OffloadPinSupportedDeviceFormats);
                break;
        }
    }

    for (UINT iFormat = 0; iFormat < cPinFormats; iFormat++)
    {
        PKSDATAFORMAT_WAVEFORMATEXTENSIBLE pFormat = &pPinFormats[iFormat];
        // KSDATAFORMAT VALIDATION
        if (!IsEqualGUIDAligned(pFormat->DataFormat.MajorFormat, _pDataFormat->MajorFormat)) { continue; }
        if (!IsEqualGUIDAligned(pFormat->DataFormat.SubFormat, _pDataFormat->SubFormat)) { continue; }
        if (!IsEqualGUIDAligned(pFormat->DataFormat.Specifier, _pDataFormat->Specifier)) { continue; }
        if (pFormat->DataFormat.FormatSize < sizeof(KSDATAFORMAT_WAVEFORMATEX)) { continue; }

        // WAVEFORMATEX VALIDATION
        PWAVEFORMATEX pWaveFormat = reinterpret_cast<PWAVEFORMATEX>(_pDataFormat + 1);
        
        if (pWaveFormat->wFormatTag != WAVE_FORMAT_EXTENSIBLE)
        {
            if (pWaveFormat->wFormatTag != EXTRACT_WAVEFORMATEX_ID(&(pFormat->WaveFormatExt.SubFormat))) { continue; }
        }
        if (pWaveFormat->nChannels  != pFormat->WaveFormatExt.Format.nChannels) { continue; }
        if (pWaveFormat->nSamplesPerSec != pFormat->WaveFormatExt.Format.nSamplesPerSec) { continue; }
        if (pWaveFormat->nBlockAlign != pFormat->WaveFormatExt.Format.nBlockAlign) { continue; }
        if (pWaveFormat->wBitsPerSample != pFormat->WaveFormatExt.Format.wBitsPerSample) { continue; }

        if (pWaveFormat->wFormatTag != WAVE_FORMAT_EXTENSIBLE)
        {
            ntStatus = STATUS_SUCCESS;
            break;
        }

        // WAVEFORMATEXTENSIBLE VALIDATION
        if (pWaveFormat->cbSize < sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)) { continue; }

        PWAVEFORMATEXTENSIBLE pWaveFormatExt = reinterpret_cast<PWAVEFORMATEXTENSIBLE>(pWaveFormat);
        if (pWaveFormatExt->Samples.wValidBitsPerSample != pFormat->WaveFormatExt.Samples.wValidBitsPerSample) { continue; }
        if (pWaveFormatExt->dwChannelMask != pFormat->WaveFormatExt.dwChannelMask) { continue; }
        if (!IsEqualGUIDAligned(pWaveFormatExt->SubFormat, pFormat->WaveFormatExt.SubFormat)) { continue; }

        ntStatus = STATUS_SUCCESS;
        break;
    }

    return ntStatus;
}    

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS
CMiniportWaveRT::PropertyHandlerProposedFormat
(
    IN PPCPROPERTY_REQUEST      PropertyRequest
)
{
    PAGED_CODE();

    DPF_ENTER(("[CMiniportWaveHelper::PropertyHandlerProposedFormat]"));

    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    if (PropertyRequest->Verb & KSPROPERTY_TYPE_BASICSUPPORT)
    {
        ntStatus = 
            PropertyHandler_BasicSupport
            (
                PropertyRequest,
                KSPROPERTY_TYPE_BASICSUPPORT | KSPROPERTY_TYPE_SET,
                VT_ILLEGAL
            );
    }
    else
    {
        ULONG cbMinSize = sizeof(KSDATAFORMAT_WAVEFORMATEX);

        if (PropertyRequest->ValueSize == 0)
        {
            PropertyRequest->ValueSize = cbMinSize;
            ntStatus = STATUS_BUFFER_OVERFLOW;
        }
        else if (PropertyRequest->ValueSize < cbMinSize)
        {
            ntStatus = STATUS_BUFFER_TOO_SMALL;
        }
        else
        {
            if (PropertyRequest->Verb & KSPROPERTY_TYPE_SET)
            {
                if (PropertyRequest->InstanceSize < sizeof(ULONG))
                {
                    return STATUS_INVALID_PARAMETER;
                }

                PULONG pulPinID = static_cast<PULONG>(PropertyRequest->Instance);

                PKSDATAFORMAT pKsFormat = (PKSDATAFORMAT)PropertyRequest->Value;

                ntStatus = IsFormatSupported(*pulPinID, FALSE, pKsFormat);
            }
            else //if (PropertyRequest->Verb & KSPROPERTY_TYPE_GET)
            {
                // KSPROPERTY_PIN_PROPOSEDATAFORMAT GET is current not implemented
                // Implementing it would  allows the audio driver to provide input on selection   
                // of the default data format
                ntStatus = STATUS_INVALID_PARAMETER;
            }
        }
    }

    return ntStatus;
} // PropertyHandlerProposedFormat

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS
PropertyHandler_WaveFilter
( 
    IN PPCPROPERTY_REQUEST      PropertyRequest 
)
/*++

Routine Description:

  Redirects general property request to miniport object

Arguments:

  PropertyRequest - 

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    NTSTATUS            ntStatus = STATUS_INVALID_DEVICE_REQUEST;
    CMiniportWaveRT* pWaveHelper = reinterpret_cast<CMiniportWaveRT*>(PropertyRequest->MajorTarget);

    if (pWaveHelper == NULL)
    {
        return STATUS_INVALID_PARAMETER;
    }

    pWaveHelper->AddRef();

    switch (PropertyRequest->PropertyItem->Id)
    {
        case KSPROPERTY_PIN_PROPOSEDATAFORMAT:
            ntStatus = 
                pWaveHelper->PropertyHandlerProposedFormat
                (
                    PropertyRequest
                );
            break;
        
        default:
            DPF(D_TERSE, ("[PropertyHandler_WaveFilter: Invalid Device Request]"));
    }

    pWaveHelper->Release();

    return ntStatus;
} // PropertyHandler_WaveFilter
//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS
PropertyHandler_OffloadPin
( 
    IN PPCPROPERTY_REQUEST      PropertyRequest 
)
{
    PAGED_CODE();

    NTSTATUS                    ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    if (IsEqualGUIDAligned(*PropertyRequest->PropertyItem->Set, KSPROPSETID_OffloadPin))
    {
        switch (PropertyRequest->PropertyItem->Id)
        {
            //KSPROPERTY_OFFLOAD_PIN_VERIFY_STREAM_OBJECT_POINTER

            case KSPROPERTY_OFFLOAD_PIN_GET_STREAM_OBJECT_POINTER:
            {
                if (PropertyRequest->Verb & KSPROPERTY_TYPE_GET)
                {
                    ULONG cbMinSize = sizeof(ULONG_PTR);

                    if (PropertyRequest->ValueSize == 0)
                    {
                        PropertyRequest->ValueSize = cbMinSize;
                        ntStatus = STATUS_BUFFER_OVERFLOW;
                    }
                    else if (PropertyRequest->ValueSize < cbMinSize)
                    {
                        ntStatus = STATUS_BUFFER_TOO_SMALL;
                    }
                    else
                    {
                        ULONG_PTR *streamObjectPtr = static_cast<ULONG_PTR*>(PropertyRequest->Value);
                        *streamObjectPtr = (ULONG_PTR)(PropertyRequest->MinorTarget);
                        ntStatus = STATUS_SUCCESS;
                    }
                }

                else
                {
                    ntStatus = STATUS_INVALID_PARAMETER;
                }
            }
            break;
            case KSPROPERTY_OFFLOAD_PIN_VERIFY_STREAM_OBJECT_POINTER:
            {
                if (PropertyRequest->Verb & KSPROPERTY_TYPE_SET)
                {
                    ULONG cbMinSize = sizeof(ULONG_PTR);
                    if (PropertyRequest->InstanceSize < cbMinSize)
                    {
                        return STATUS_INVALID_PARAMETER;
                    }
                    else
                    {
                        ULONG_PTR *streamObjectPtr = static_cast<ULONG_PTR*>(PropertyRequest->Instance);
                        if (*streamObjectPtr == (ULONG_PTR)(PropertyRequest->MinorTarget))
                        {
                            ntStatus = STATUS_SUCCESS;
                        }
                        else
                        {
                            ntStatus = STATUS_UNSUCCESSFUL;
                        }
                    }
                }
                else
                {
                    ntStatus = STATUS_INVALID_PARAMETER;
                }
            }
            break;
            default:
                DPF(D_TERSE, ("[PropertyHandler_OffloadPin: Invalid Request]"));
        }
    }
    return ntStatus;
}







  

