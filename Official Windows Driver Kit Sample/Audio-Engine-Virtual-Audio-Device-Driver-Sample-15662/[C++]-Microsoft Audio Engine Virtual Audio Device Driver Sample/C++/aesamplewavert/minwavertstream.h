/*++

Copyright (c) 1997-2010  Microsoft Corporation All Rights Reserved

Module Name:

    minwavert.h

Abstract:

    Definition of wavert miniport class.

--*/

#ifndef _MSVAD_MINWAVERTSTREAM_H_
#define _MSVAD_MINWAVERTSTREAM_H_

//
// Structure to store notifications events in a protected list
//
typedef struct _NotificationListEntry
{
    LIST_ENTRY  ListEntry;
    PKEVENT     NotificationEvent;
} NotificationListEntry;

KDEFERRED_ROUTINE TimerNotifyRT;

//=============================================================================
// Referenced Forward
//=============================================================================
class CMiniportWaveRT;
typedef CMiniportWaveRT *PCMiniportWaveRT;

//=============================================================================
// Classes
//=============================================================================
///////////////////////////////////////////////////////////////////////////////
// CMiniportWaveRTStream 
// 
class CMiniportWaveRTStream : 
    public IMiniportWaveRTStreamNotification,
    public IMiniportStreamAudioEngineNode,
    public CUnknown
{
protected:
    PPORTWAVERTSTREAM           m_pPortStream;
    LIST_ENTRY                  m_NotificationList;
    PKTIMER                     m_pNotificationTimer;
    PRKDPC                      m_pNotificationDpc;
    ULONG                       m_ulNotificationIntervalMs;
    BOOL                        m_IsCapture;
    ULONG                       m_ulCurrentWritePosition;
public:
    DECLARE_STD_UNKNOWN();
    DEFINE_STD_CONSTRUCTOR(CMiniportWaveRTStream);
    ~CMiniportWaveRTStream();

    IMP_IMiniportWaveRTStream;
    IMP_IMiniportWaveRTStreamNotification;
    IMP_IMiniportStreamAudioEngineNode;

    NTSTATUS                    Init
    ( 
        IN  PCMiniportWaveRT    Miniport,
        IN  PPORTWAVERTSTREAM   Stream,
        IN  ULONG               Channel,
        IN  BOOLEAN             Capture,
        IN  PKSDATAFORMAT       DataFormat
    );

    // Friends
    friend class                CMiniportWaveRT;
    friend KDEFERRED_ROUTINE    TimerNotifyRT;
protected:
    CMiniportWaveRT*        m_pMiniport;
    ULONG                   m_ulPin;
    BOOLEAN                 m_bCapture;
    ULONG                   m_ulDmaBufferSize;
    KSSTATE                 m_KsState;
    PKTIMER                 m_pTimer;
    PRKDPC                  m_pDpc;
    ULONGLONG               m_ullPlayPosition;
    ULONGLONG               m_ullWritePosition;
    ULONGLONG               m_ullLinearPosition;
    ULONGLONG               m_ullDmaTimeStamp;
    LARGE_INTEGER           m_ullPerformanceCounterFrequency;
    ULONGLONG               m_hnsElapsedTimeCarryForward;
    ULONG                   m_ulDmaMovementRate;
    ULONG                   m_hnsDisplacementCarryForward;
    BOOL                    m_bLfxEnabled;
    PBOOL                   m_pbMuted;
    PLONG                   m_plVolumeLevel;
    PLONG                   m_plPeakMeter;
    PWAVEFORMATEXTENSIBLE   m_pWfExt;

public:


    NTSTATUS GetVolumeChannelCount(_Out_ UINT32 *puiChannelCount);
    NTSTATUS GetVolumeSteppings(_Out_writes_bytes_(_ui32DataSize) PKSPROPERTY_STEPPING_LONG _pKsPropStepLong, _In_  UINT32 _ui32DataSize);
    NTSTATUS GetChannelVolume(_In_  UINT32 _uiChannel, _Out_  LONG *_pVolume);
    NTSTATUS SetChannelVolume(_In_  UINT32 _uiChannel, _In_  LONG _Volume);

    NTSTATUS GetPeakMeterChannelCount(_Out_ UINT32 *puiChannelCount);
    NTSTATUS GetPeakMeterSteppings(_Out_writes_bytes_(_ui32DataSize)  PKSPROPERTY_STEPPING_LONG _pKsPropStepLong, _In_  UINT32 _ui32DataSize);
    NTSTATUS GetChannelPeakMeter(_In_  UINT32 _uiChannel, _Out_  LONG *_plPeakMeter);

    NTSTATUS GetMuteChannelCount(_Out_ UINT32 *puiChannelCount);
    NTSTATUS GetMuteSteppings(_Out_writes_bytes_(_ui32DataSize)  PKSPROPERTY_STEPPING_LONG _pKsPropStepLong, _In_  UINT32 _ui32DataSize);
    NTSTATUS GetChannelMute(_In_  UINT32 _uiChannel, _Out_  BOOL *_pbMute);
    NTSTATUS SetChannelMute(_In_  UINT32 _uiChannel, _In_  BOOL _bMute);

    //presentation
    NTSTATUS GetPresentationPosition(_Out_  KSAUDIO_PRESENTATION_POSITION *_pPresentationPosition);
    NTSTATUS SetCurrentWritePosition(_In_  ULONG ulCurrentWritePosition);
    NTSTATUS GetLinearBufferPosition(_Out_  ULONGLONG *pullLinearBufferPosition, LARGE_INTEGER *_pliQPCTime);
    NTSTATUS SetLoopbackProtection(_In_ CONSTRICTOR_OPTION ulProtectionOption);


    ULONG GetCurrentWaveRTWrtiePosition() {return m_ulCurrentWritePosition;};
};
typedef CMiniportWaveRTStream *PCMiniportWaveRTStream;
#endif

