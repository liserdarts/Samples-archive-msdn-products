/*++

Module Name:

    MiniportStreamAudioEngineNode.cpp

Abstract:

    Implementation of IMiniportstreamAudioEngineNode interface for wavert steam.

--*/

#include <msvad.h>
#include <common.h>
#include <intsafe.h>
#include <ks.h>
#include "simple.h"
#include "minwavert.h"
#include "minwavertstream.h"
#include "UnittestData.h"
#define MINWAVERTSTREAM_POOLTAG 'SRWM'
#define HNSTIME_PER_MILLISECOND 10000

#pragma warning (disable : 4127)

#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::GetLfxState 

Description:

    Portcls calls this method to get a offload stream's Lfx state 

Parameters
    
    _Out_ pbEnable: a pointer to a BOOL value for receieving the returned LFX state

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks
    The Lfx operations (on the offload stream) inside HW Audio Engine (such as src, dsp, and other special effects) are hidden from the software audio stack.
So, the driver should return TRUE if any one of the effects is on and returns FALSE when all the opertations are off.
-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::GetLfxState(_Out_ BOOL *_pbEnable)
{
    PAGED_CODE ();

    DPF_ENTER(("[CMiniportWaveRT::GetLfxState]"));

    *_pbEnable = m_bLfxEnabled;
    return STATUS_SUCCESS;
}
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::SetLfxState 
 
Decscription:

    Portcls calls this method to set a offload stream's Lfx state

Parameters:
    
    _In_ bEnable: a BOOL value. TRU: to enable Lfx, FALSE: to disable Lfx

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks
    The local operations (on the offload stream) inside HW Audio Engine (such as src, dsp, and other special effects) are hidden from the software audio stack.
So, when handling disabling Lfx, the driver should ALL the effects. When enabling Lfx, it's up the driver+HW Audio Engine to decide what opertations to turn on
when they see appropriate.
-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::SetLfxState(_In_ BOOL _bEnable)
{
    PAGED_CODE ();

    DPF_ENTER(("[CMiniportWaveRT::SetGfxState]"));

    UNREFERENCED_PARAMETER(_bEnable);
    m_bLfxEnabled = _bEnable;
    return STATUS_SUCCESS;
}
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::GetStreamChannelVolume 
 
Decscription:

    When handling GET volume KS property for the device, Portcls calls 
    this method, inside its property handlers, to get the current setting on the specific channel.

Parameters:

        _In_ _uiChannel:  the target channel for this GET volume operation
		_Out_ _pVolume: a pointer to a LONG variable for receiving returned information

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::GetStreamChannelVolume(_In_ UINT32 _uiChannel, _Out_ LONG *_pVolume)
{
    PAGED_CODE ();

    DPF_ENTER(("[CMiniportWaveRTStream::GetStreamChannelVolume]"));

    *_pVolume = m_plVolumeLevel[_uiChannel];

    return STATUS_SUCCESS;
}
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::GetStreamChannelMute 
 
Decscription:

    When handling GET mute KS property for the device, Portcls calls 
    this method, inside its property handlers, to get the current setting on the specific channel.

Parameters:

        _In_ _uiChannel:  the target channel for this GET volume operation
		_Out_ _pbMute: a pointer to a BOOL variable for receiving returned information

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

-------------------------------------------------------------------------------------------------------------------------*/
STDMETHODIMP_(NTSTATUS) CMiniportWaveRTStream::GetStreamChannelMute(_In_ UINT32 _uiChannel, _Out_  BOOL  *_pbMute)
{
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();
    ASSERT (_pbMute);

    DPF_ENTER(("[CMiniportWaveRT::GetStreamChannelMute]"));
    ntStatus = GetChannelMute(_uiChannel, _pbMute);

    return ntStatus;
}
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::GetStreamAttributeSteppings 
 
Decscription:

    When handling volume, mute, and meter related KS properties, Portcls calls 
    this method, inside its property handlers, to know the property stepping information for the 
    corresponding KS property.

Parameters:

        _In_ _targetType:  the query target (volume. mute, or peak meter)
		_Out_ _pKsPropMembHead: a pointer to a PKSPROPERTY_STEPPING_LONG variable for receiving returned channel count information
        _In_ ulBufferSize: a pointer to a ULONG variable that has the size of the buffer pointed by _pKsPropMembHead

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::GetStreamAttributeSteppings(_In_  eChannelTargetType _targetType, _Out_ PKSPROPERTY_STEPPING_LONG _pKsPropMembHead, _In_  UINT32 _ui32DataSize)
{
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();

    DPF_ENTER(("[CMiniportWaveRTStream::GetDeviceAttributeSteppings]"));

    switch (_targetType)
    {
        case eVolumeAttribute:
             ntStatus = GetVolumeSteppings(_pKsPropMembHead, _ui32DataSize);;
             break;
        case eMuteAttribute:
             ntStatus = GetMuteSteppings(_pKsPropMembHead, _ui32DataSize);;
             break;
        case ePeakMeterAttribute:
             ntStatus = GetPeakMeterSteppings(_pKsPropMembHead, _ui32DataSize);;
             break;
        default:
            ntStatus = STATUS_INVALID_DEVICE_REQUEST;
            break;
    }
     return ntStatus;

}
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::SetStreamChannelVolume 
 
Decscription:

    When handling SET volume KS property for the device, Portcls calls 
    this method, inside its property handlers, to set the current setting on the specific channel.

Parameters:

        _In_ Channel:  the target channel for this GET volume operation
        _In_ TargetVolume: volume value to set 
        _In_ CurveType: type of curve to apply to the ramp
        _In_ CurveDuration: amount of time in hns over which to ramp the volume

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

-------------------------------------------------------------------------------------------------------------------------*/
STDMETHODIMP_(NTSTATUS) CMiniportWaveRTStream::SetStreamChannelVolume
(
    _In_ UINT32             Channel,
    _In_ LONG	            TargetVolume,
    _In_ AUDIO_CURVE_TYPE   CurveType,
    _In_ ULONGLONG          CurveDuration
)
{
    UNREFERENCED_PARAMETER(CurveType);
    UNREFERENCED_PARAMETER(CurveDuration);
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();

    DPF_ENTER(("[CMiniportWaveRTStream::SetStreamChannelVolume]"));

    // Snap the volume level to our range of steppings.
    LONG lVolume = TargetVolume < MIN_VOLUME_STEPPINGS ? MIN_VOLUME_STEPPINGS :
                   TargetVolume > MAX_VOLUME_STEPPINGS ? MAX_VOLUME_STEPPINGS : (TargetVolume / VOLUME_STEPPING_DELTA);

    // If Channel is UINT32_MAX, then set the level on all channels
    if ( UINT32_MAX == Channel )
    {
        for (UINT32 i = 0; i < m_pWfExt->Format.nChannels; i++)
        {
            ntStatus = SetChannelVolume(i, lVolume);
        }
    }
    else
    {
        ntStatus = SetChannelVolume(Channel, lVolume);
    }

    return ntStatus;
}

#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::GetStreamChannelPeakMeter 
 
Decscription:

    When handling GET peak meter KS property for the device, Portcls calls 
    this method, inside its property handlers, to get the current setting on the specific channel.

Parameters:

        _In_ _uiChannel:  the target channel for this GET peak meter operation
	_Out_ _pPeakMeterValue: a pointer to a LONG variable for receiving returned information

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

-------------------------------------------------------------------------------------------------------------------------*/
STDMETHODIMP_(NTSTATUS) CMiniportWaveRTStream::GetStreamChannelPeakMeter(_In_ UINT32 _uiChannel, _Out_  LONG  *_pPeakMeterValue)
{
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();

    DPF_ENTER(("[CMiniportWaveRTStream::GetStreamChannelPeakMeter]"));

    ntStatus = GetChannelPeakMeter(_uiChannel, _pPeakMeterValue);

    return ntStatus;
}

#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::SetStreamChannelMute 
 
Decscription:

    When handling SET mute KS property for the device, Portcls calls 
    this method, inside its property handlers, to set the current setting on the specific channel.

Parameters:

        _In_ _uiChannel:  the target channel for this Set mute operation
		_In_ _bMute: mute value to set 

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::SetStreamChannelMute(_In_ UINT32 _uiChannel, _In_  BOOL  _bMute)
{
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();

    DPF_ENTER(("[CMiniportWaveRTStream::SetStreamChannelMute]"));

    ntStatus = SetChannelMute(_uiChannel, _bMute);
 
    return ntStatus;
}
//presentation
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::GetStreamPresentationPosition 
 
Decscription:

    Portcls calls this method, inside its property handlers, to get a stream's presentation 
    postion.

Parameters:

		_Out_ pPresentationPosition: a pointer to a KSAUDIO_PRESENTATION_POSITION variable for receiving returned information

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::GetStreamPresentationPosition(_Out_ KSAUDIO_PRESENTATION_POSITION *_pPresentationPosition)
{
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();
    ASSERT(_pPresentationPosition);
    DPF_ENTER(("[CMiniportWaveRTStream::GetStreamPresentationPosition]"));

    ntStatus = GetPresentationPosition(_pPresentationPosition);
 
    return ntStatus;
}
//StreamCurrentWritePosition
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::SetStreamCurrentWritePosition 
 
Decscription:

    Portcls calls this method, inside its property handlers, to set a stream's write position 
    postion.

Parameters:

		_In_ ulCurrentWritePosition: a position value indicating the last valid byte in the buffer

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

_ulCurrentWritePosition specifies the current write position in bytes, which an offload driver can use to know how much valid data in the WaveRT buffer.
Let say, BufferByteSize is the WaveRT buffer size,  the valid value range for _ulCurrentWritePosition be 0... BufferByteSize (inclusive)

0: no valid data in the buffer
n: the last valid data byte is at the offset n from the beginning of the buffer. If this is a value smaller than a previous write position, 
it means a wrap-around has happened, in the case, the driver would need to take that in to consider when calculating how many bytes are valid for fetching.
BufferByteSize: the last valid data byte is at the end of the buffer

After a pin is instantiated and before any KSPROPERTY_AUDIO_WAVERT_CURRENT_WRITE_POSITION is received, a driver should assume its current write position is zero.

-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::SetStreamCurrentWritePosition(_In_ ULONG _ulCurrentWritePosition)
{
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();
    DPF_ENTER(("[CMiniportWaveRT::SetStreamCurrentWritePosition]"));

    ntStatus = SetCurrentWritePosition(_ulCurrentWritePosition);

    return ntStatus;
}
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::GetStreamLinearBufferPosition 
 
Decscription:

    Portcls calls this method, inside its property handlers, to set a stream's write position 
    postion.

Parameters:

		_Out_ pullLinearBufferPosition: a pointer to a ULONGLONG variable for receiving returned information


Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks
The returned  value is the number of bytes that the DMA has fetched from the audio buffer since the beginning of the stream
-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::GetStreamLinearBufferPosition(_Out_ ULONGLONG *_pullLinearBufferPosition)
{
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();
    ASSERT(_pullLinearBufferPosition);
    DPF_ENTER(("[CMiniportWaveRTStream::GetStreamLinearBufferPosition]"));

    ntStatus = GetLinearBufferPosition(_pullLinearBufferPosition, NULL);
 
    return ntStatus;
}
//SetStreamLoopbackProtection
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::SetStreamLoopbackProtection 
 
Decscription:

    Portcls calls this method, inside its property handlers, to set the loopback
    protection option

Parameters:
		_In_ protectionOption: protection option
                                 CONSTRICTOR_OPTION_DISABLE: Turn protection  off.
                                 CONSTRICTOR_OPTION_MUTE: Mute the loopback stream
Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::SetStreamLoopbackProtection(_In_ CONSTRICTOR_OPTION protectionOption)
{
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();
    DPF_ENTER(("[CMiniportWaveRT::SetStreamLoopbackProtection]"));

    ntStatus = SetLoopbackProtection(protectionOption);

    return ntStatus;
}
#pragma code_seg("PAGE")
/*-----------------------------------------------------------------------------
IMiniportStreamAudioEngineNode::GetStreamChannelCount 
 
Decscription:

    When handling volume, mute, and meter related KS properties, Portcls calls 
    this method, inside its property handlers, to know the number of channels for the 
    corresponding KS property.

Parameters:

        _In_ _targetType:  the query target (volume, mute, or peak meter)
		_Out_ _pulChannelCount: a pointer to a UINT32 variable for receiving returned channel count information

Return Value:

   Appropriate NTSTATUS code

Called at PASSIVE_LEVEL

Remarks

-------------------------------------------------------------------------------------------------------------------------*/
NTSTATUS CMiniportWaveRTStream::GetStreamChannelCount(_In_  eChannelTargetType	_targetType,_Out_  UINT32 *_pulChannelCount)
{
    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    PAGED_CODE ();

    DPF_ENTER(("[CMiniportWaveRTStream::GetStreamChannelCount]"));

    switch (_targetType)
    {
        case eVolumeAttribute:
             ntStatus = GetVolumeChannelCount(_pulChannelCount);
             break;
        case eMuteAttribute:
             ntStatus = GetMuteChannelCount(_pulChannelCount);
             break;
        case ePeakMeterAttribute:
             ntStatus = GetPeakMeterChannelCount(_pulChannelCount);
             break;
        default:
            ntStatus = STATUS_INVALID_DEVICE_REQUEST;
            break;
    }

    return ntStatus;
}


//------------------------------------------------------------------------------------------------------------------------
//CMiniportWaveRTStream private supporting functions
//------------------------------------------------------------------------------------------------------------------------
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetVolumeChannelCount(_Out_ UINT32 *_puiChannelCount)
{
    PAGED_CODE();

    DPF_ENTER(("[CMiniportWaveRTStream::GetVolumeChannelCount]"));

    ASSERT(_puiChannelCount);
    ASSERT(m_pWfExt);

    NTSTATUS ntStatus = STATUS_SUCCESS;
    *_puiChannelCount = m_pWfExt->Format.nChannels;
    return ntStatus;
}

#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetVolumeSteppings(_Out_writes_bytes_(_ui32DataSize)  PKSPROPERTY_STEPPING_LONG _pKsPropStepLong, _In_  UINT32 _ui32DataSize)
{
    PAGED_CODE ();
    UINT32 ulChannelCount = _ui32DataSize / sizeof(KSPROPERTY_STEPPING_LONG);
    ASSERT (_pKsPropStepLong);
    DPF_ENTER(("[CMiniportWaveRTStream::GetVolumeSteppings]"));

    if (ulChannelCount != m_pWfExt->Format.nChannels)
    {
        return STATUS_INVALID_PARAMETER;
    }

    for(UINT i = 0; i < ulChannelCount; i++)
    {
        _pKsPropStepLong[i].SteppingDelta = VOLUME_STEPPING_DELTA;
        _pKsPropStepLong[i].Bounds.SignedMaximum = MIN_VOLUME_STEPPINGS;
        _pKsPropStepLong[i].Bounds.SignedMinimum = MAX_VOLUME_STEPPINGS;
    }

    return STATUS_SUCCESS;
}
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetChannelVolume(_In_  UINT32 _uiChannel, _Out_  LONG *_pVolume)
{
    PAGED_CODE ();
    ASSERT (_pVolume);
    DPF_ENTER(("[CMiniportWaveRTStream::GetChannelVolume]"));

    *_pVolume = m_plVolumeLevel[_uiChannel];

    return STATUS_SUCCESS;
}
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::SetChannelVolume(_In_  UINT32 _uiChannel, _In_  LONG _Volume)
{
    PAGED_CODE ();
    DPF_ENTER(("[CMiniportWaveRTStream::SetChannelVolume]"));

    m_plVolumeLevel[_uiChannel] = _Volume;

    return STATUS_SUCCESS;
}
///- metering
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetPeakMeterChannelCount(_Out_ UINT32 *puiChannelCount)
{
    PAGED_CODE();

    DPF_ENTER(("[CMiniportWaveRTStream::GetPeakMeterChannelCount]"));

    ASSERT(puiChannelCount);
    ASSERT(m_pWfExt);

    NTSTATUS ntStatus = STATUS_SUCCESS;
    *puiChannelCount = m_pWfExt->Format.nChannels;
    return ntStatus;
}

#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetPeakMeterSteppings(_Out_writes_bytes_(_ui32DataSize) PKSPROPERTY_STEPPING_LONG _pKsPropStepLong, _In_  UINT32 _ui32DataSize)
{
    PAGED_CODE ();
    UINT32 ulChannelCount = _ui32DataSize / sizeof(KSPROPERTY_STEPPING_LONG);

    ASSERT (_pKsPropStepLong);
    DPF_ENTER(("[CMiniportWaveRTStream::GetPeakMeterSteppings]"));

    if (ulChannelCount != m_pWfExt->Format.nChannels)
    {
        return STATUS_INVALID_PARAMETER;
    }

    for(UINT i = 0; i < ulChannelCount; i++)
    {
        _pKsPropStepLong[i].SteppingDelta = 0x1000;
        _pKsPropStepLong[i].Bounds.SignedMaximum = LONG_MAX;
        _pKsPropStepLong[i].Bounds.SignedMinimum = LONG_MIN;
    }

    return STATUS_SUCCESS;
}
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetChannelPeakMeter(_In_  UINT32 _uiChannel, _Out_  LONG *_plPeakMeter)
{
    PAGED_CODE ();
    ASSERT (_plPeakMeter);
    UNREFERENCED_PARAMETER(_uiChannel);
    DPF_ENTER(("[CMiniportWaveRTStream::GetChannelPeakMeter]"));

    *_plPeakMeter = LONG_MAX / 2;

    return STATUS_SUCCESS;
}

 //=============================================================================
// stream Mute
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetMuteChannelCount(_Out_ UINT32 *puiChannelCount)
{
    PAGED_CODE();

    DPF_ENTER(("[CMiniportWaveRTStream::GetMuteChannelCount]"));

    ASSERT(puiChannelCount);
    ASSERT(m_pWfExt);

    NTSTATUS ntStatus = STATUS_SUCCESS;
    *puiChannelCount = m_pWfExt->Format.nChannels;
    return ntStatus;
}

#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetMuteSteppings(_Out_writes_bytes_(_ui32DataSize) PKSPROPERTY_STEPPING_LONG _pKsPropStepLong, _In_  UINT32 _ui32DataSize)
{
    PAGED_CODE ();
    UINT32 ulChannelCount = _ui32DataSize / sizeof(KSPROPERTY_STEPPING_LONG);

    ASSERT (_pKsPropStepLong);
    DPF_ENTER(("[CMiniportWaveRTStream::GetMuteSteppings]"));

    if (ulChannelCount != m_pWfExt->Format.nChannels)
    {
        return STATUS_INVALID_PARAMETER;
    }

    for(UINT i = 0; i < ulChannelCount; i++)
    {
        _pKsPropStepLong[i].SteppingDelta = 1;
        _pKsPropStepLong[i].Bounds.SignedMaximum = TRUE;
        _pKsPropStepLong[i].Bounds.SignedMinimum = FALSE;
    }

    return STATUS_SUCCESS;
}
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetChannelMute(_In_  UINT32 _uiChannel, _Out_  BOOL *_pbMute)
{
    PAGED_CODE ();
    ASSERT (_pbMute);
    DPF_ENTER(("[CMiniportWaveRTStream::GetChannelMute]"));

    *_pbMute = m_pbMuted[_uiChannel];
    return STATUS_SUCCESS;
}
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::SetChannelMute(_In_  UINT32 _uiChannel, _In_  BOOL _bMute)
{
    PAGED_CODE ();
    DPF_ENTER(("[CMiniportWaveRTStream::SetChannelMute]"));

    m_plVolumeLevel[_uiChannel] = _bMute;

    return STATUS_SUCCESS;
}
//presentation
#pragma code_seg("PAGE")
//
//  UINT64 u64PositionInBlocks; // The block offset from the start of the stream to the current post-decoded uncompressed 
//                              // position in the stream, where a block is the group of channels in the same sample; for a PCM stream, 
//  			      // a block is same as a frame. For compressed formats, a block is a single sample within a frame 
//                              // (eg. each MP3 frame has 1152 samples or 1152 blocks) 
//  UINT64 u64QPCPosition;      // The value of the performance counter at the time that the audio endpoint device read the device 
//                              // position (*pu64Position) in response to the KSAUDIO_PRESENTATION_POSITION call.

NTSTATUS CMiniportWaveRTStream::GetPresentationPosition(_Out_  KSAUDIO_PRESENTATION_POSITION *_pPresentationPosition)
{
    PAGED_CODE ();
    ASSERT (_pPresentationPosition);
    LARGE_INTEGER timeStamp;
    PADAPTERCOMMON pAdapterComm = m_pMiniport->GetAdapterCommObj();

    DPF_ENTER(("[CMiniportWaveRT::GetPresentationPosition]"));

    ULONGLONG ullLinearPosition = {0};
    NTSTATUS status = STATUS_SUCCESS;
    
    status = GetLinearBufferPosition(&ullLinearPosition, &timeStamp);
    if (!NT_SUCCESS(status)) 
    { 
        return status; 
    }

    _pPresentationPosition->u64PositionInBlocks = ullLinearPosition * m_pWfExt->Format.nSamplesPerSec / m_pWfExt->Format.nAvgBytesPerSec;

    _pPresentationPosition->u64QPCPosition = (UINT64)timeStamp.QuadPart;


    //Event type: eMINIPORT_GET_PRESENTATION_POSITION
    //Parameter 1: Current linear buffer position	
    //Parameter 2: the previous WaveRtBufferWritePosition that the drive received	
    //Parameter 3: Presentation position
    //Parameter 4:0
    pAdapterComm->WriteEtwEvent(eMINIPORT_GET_PRESENTATION_POSITION, 
                                ullLinearPosition, // replace with the correct "Current linear buffer position"	
                                m_ulCurrentWritePosition,
                                _pPresentationPosition->u64PositionInBlocks, 
                                0);  // always zero
     
    return STATUS_SUCCESS;
}
NTSTATUS CMiniportWaveRTStream::SetCurrentWritePosition(_In_  ULONG _ulCurrentWritePosition)
{
    PAGED_CODE ();
    DPF_ENTER(("[CMiniportWaveRT::SetCurrentWritePosition]"));

    UNREFERENCED_PARAMETER(_ulCurrentWritePosition);
    PADAPTERCOMMON pAdapterComm = m_pMiniport->GetAdapterCommObj();

    //Event type: eMINIPORT_SET_WAVERT_BUFFER_WRITE_POSITION
    //Parameter 1: Current linear buffer position	
    //Parameter 2: the previous WaveRtBufferWritePosition that the drive received	
    //Parameter 3: Target WaveRtBufferWritePosition received from portcls
    //Parameter 4:0
    pAdapterComm->WriteEtwEvent(eMINIPORT_SET_WAVERT_BUFFER_WRITE_POSITION, 
                                100, // replace with the correct "Current linear buffer position"	
                                m_ulCurrentWritePosition,
                                _ulCurrentWritePosition, // this is the passed in parameter
                                0);  // always zero

    if (WAVERT_CURRENT_WRITE_POSITION_DATA != _ulCurrentWritePosition)
    {
        return STATUS_UNSUCCESSFUL;
    }
    m_ulCurrentWritePosition = _ulCurrentWritePosition;

    return STATUS_SUCCESS;
}
//linear position
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetLinearBufferPosition(_Out_  ULONGLONG *_pullLinearBufferPosition, LARGE_INTEGER *_pliQPCTime)
{
    PAGED_CODE ();
    ASSERT (_pullLinearBufferPosition);
    DPF_ENTER(("[CMiniportWaveRTStream::GetLinearBufferPosition]"));

    // Update *_pullLinearBufferPosition with the the number of bytes fetched from waveRT ever since a stream got set into RUN
    // state.
    // Once the stream is set to STOP state, any further read on this call would return zero.
    
    // Get the current time
    //
    LARGE_INTEGER ilQPC = KeQueryPerformanceCounter(NULL);
    if (m_KsState == KSSTATE_RUN)
    {

        ULONGLONG  hnsCurrentTime = KSCONVERT_PERFORMANCE_TIME(m_ullPerformanceCounterFrequency.QuadPart, ilQPC);

        // Calculate the time elapsed since the last call to GetPosition() or since the
        // DMA engine started.  Note that the division by 10000 to convert to milliseconds
        // may cause us to lose some of the time, so we will carry the remainder forward 
        // to the next GetPosition() call.

        // convert from hns to ms
        ULONG  TimeElapsedInMS = (ULONG)(hnsCurrentTime - m_ullDmaTimeStamp + m_hnsElapsedTimeCarryForward)/10000;

        // Carry forward the remainder of this division so we don't fall behind with our position.
        // too much
        m_hnsElapsedTimeCarryForward = (hnsCurrentTime - m_ullDmaTimeStamp + m_hnsElapsedTimeCarryForward) % 10000;

        // Calculate how many bytes in the DMA buffer would have been processed in the elapsed
        // time.  Note that the division by 1000 to convert to milliseconds may cause us to 
        // lose some bytes, so we will carry the remainder forward to the next GetPosition() call.
        //
        // need to divide by 1000 because m_ulDmaMovementRate is average bytes per sec.
        ULONG ByteDisplacement = (m_ulDmaMovementRate * TimeElapsedInMS)/ 1000;

        // Increment the DMA position by the number of bytes displaced since the last
        // call to GetPosition() and ensure we properly wrap at buffer length.
        //
        m_ullLinearPosition += ByteDisplacement;

        // Update the DMA time stamp for the next call to GetPosition()
        //
        m_ullDmaTimeStamp = hnsCurrentTime;

    }
    if (_pliQPCTime)
    {
        *_pliQPCTime = ilQPC;
    }
    *_pullLinearBufferPosition = m_ullLinearPosition;

    return STATUS_SUCCESS;
}
#pragma code_seg("PAGE")
//----------------------------------------------------------------------------------
// Description:
//         Set the loopback stream's content protection options
//
// Parameters:
// 
//		_In_ protectionOption: protection option
//                                 CONSTRICTOR_OPTION_DISABLE: Turn protection  off.
//                                 CONSTRICTOR_OPTION_MUTE: Mute the loopback stream
//
// Remark:
//     This content protection settings requestion could come in from the host process
//     pin or offload pin. The miniport needs to mute its loopback stream contents for 
//     for this request from either host process pin or offload pin.
//----------------------------------------------------------------------------------
NTSTATUS CMiniportWaveRTStream::SetLoopbackProtection(_In_  CONSTRICTOR_OPTION protectionOption)
{
    PAGED_CODE ();
    DPF_ENTER(("[CMiniportWaveRT::SetLoopbackProtection]"));

    UNREFERENCED_PARAMETER(protectionOption);

    // Miniport driver mutes/unmutes the loopback here


    return STATUS_SUCCESS;
}

