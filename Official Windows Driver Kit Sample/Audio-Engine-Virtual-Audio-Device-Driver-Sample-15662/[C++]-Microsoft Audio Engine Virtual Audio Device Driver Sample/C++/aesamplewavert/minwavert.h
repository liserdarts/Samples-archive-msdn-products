/*++

Copyright (c) 1997-2010  Microsoft Corporation All Rights Reserved

Module Name:

    minwavert.h

Abstract:

    Definition of wavert miniport class.

--*/

#ifndef _MSVAD_MINWAVERT_H_
#define _MSVAD_MINWAVERT_H_

#define VOLUME_STEPPING_DELTA   0x8000
#define MIN_VOLUME_STEPPINGS    0x00000000
#define MAX_VOLUME_STEPPINGS    (-96 * 0x10000)

//=============================================================================
// Referenced Forward
//=============================================================================
class CMiniportWaveRTStream;
typedef CMiniportWaveRTStream *PCMiniportWaveRTStream;

//=============================================================================
// Classes
//=============================================================================
///////////////////////////////////////////////////////////////////////////////
// CMiniportWaveRT
//   
class CMiniportWaveRT : 
    public IMiniportWaveRT,
    public IMiniportAudioEngineNode,
    public CUnknown
{
private:
    ULONG                               m_ulLoopbackAllocated;
    ULONG                               m_ulSystemAllocated;
    ULONG                               m_ulOffloadAllocated;
    
    ULONG                               m_ulMaxSystemStreams;
    ULONG                               m_ulMaxOffloadStreams;
    ULONG                               m_ulMaxLoopbackStreams;

    LONGLONG                            m_llProcessingPeriod;
    BOOL                                m_bGfxEnabled;
    PBOOL                               m_pbMuted;
    PLONG                               m_plVolumeLevel;
    PLONG                               m_plPeakMeter;
    PKSDATAFORMAT_WAVEFORMATEXTENSIBLE  m_pMixFormat;
    PKSDATAFORMAT_WAVEFORMATEXTENSIBLE  m_pDeviceFormat;
    PCFILTER_DESCRIPTOR                 m_FilterDesc;
    eDeviceType                         m_DeviceType;
    PADAPTERCOMMON                      m_pAdapterCommon;

public:
    NTSTATUS                            ValidateStreamCreate(_In_ ULONG _Pin, _In_ BOOLEAN _Capture);
    NTSTATUS                            StreamCreated(ULONG _Pin);
    NTSTATUS                            StreamClosed(ULONG _Pin);
    NTSTATUS                            IsFormatSupported(ULONG _ulPin, BOOLEAN _bCapture,PKSDATAFORMAT _pDataFormat);

public:
    DECLARE_STD_UNKNOWN();

    CMiniportWaveRT(eDeviceType deviceType, PCFILTER_DESCRIPTOR *_Desc, PUNKNOWN UnknownAdapter):CUnknown(0), m_DeviceType(deviceType) 
    {
        m_pAdapterCommon = (PADAPTERCOMMON)UnknownAdapter;
        if (_Desc)
        {
            RtlCopyMemory(&m_FilterDesc, _Desc, sizeof(m_FilterDesc));
        }
    }

    ~CMiniportWaveRT();

    IMP_IMiniportWaveRT;
    IMP_IMiniportAudioEngineNode;
    // Friends
    friend class                CMiniportWaveRTStream;
    friend class                CMiniportTopologySimple;


public:
    NTSTATUS PropertyHandlerProposedFormat(IN PPCPROPERTY_REQUEST PropertyRequest);
    PADAPTERCOMMON GetAdapterCommObj() {return m_pAdapterCommon; };

#ifdef _SUPPORT_INDEPENDENT_MICIN
    NTSTATUS PropertyHandlerProposedFormat_MicIn(IN PPCPROPERTY_REQUEST PropertyRequest);
#endif

    //---------------------------------------------------------------------------------------------------------
    // volume
    //---------------------------------------------------------------------------------------------------------

    NTSTATUS GetVolumeChannelCount(_Out_  UINT32 *pulChannelCount);
    NTSTATUS GetVolumeSteppings(_Out_writes_bytes_(_ui32DataSize)  PKSPROPERTY_STEPPING_LONG _pKsPropStepLong, _In_  UINT32 _ui32DataSize);
    NTSTATUS GetChannelVolume(_In_  UINT32 _uiChannel, _Out_  LONG *_pVolume);
    NTSTATUS SetChannelVolume(_In_  UINT32 _uiChannel, _In_  LONG _Volume);

    //-----------------------------------------------------------------------------
    // metering 
    //-----------------------------------------------------------------------------
    NTSTATUS GetPeakMeterChannelCount(_Out_  UINT32 *pulChannelCount);
    NTSTATUS GetPeakMeterSteppings(_Out_writes_bytes_(_ui32DataSize)  PKSPROPERTY_STEPPING_LONG _pKsPropStepLong, _In_  UINT32 _ui32DataSize);
    NTSTATUS GetChannelPeakMeter(_In_  UINT32 _uiChannel, _Out_  LONG *_plPeakMeter);

    // mute
    NTSTATUS GetMuteChannelCount(_Out_  UINT32 *pulChannelCount);
    NTSTATUS GetMuteSteppings(_Out_writes_bytes_(_ui32DataSize)  PKSPROPERTY_STEPPING_LONG _pKsPropStepLong, _In_  UINT32 _ui32DataSize);
    NTSTATUS GetChannelMute(_In_  UINT32 _uiChannel, _Out_  BOOL *_pbMute);
    NTSTATUS SetChannelMute(_In_  UINT32 _uiChannel, _In_  BOOL _bMute);

};
typedef CMiniportWaveRT *PCMiniportWaveRT;
#endif

