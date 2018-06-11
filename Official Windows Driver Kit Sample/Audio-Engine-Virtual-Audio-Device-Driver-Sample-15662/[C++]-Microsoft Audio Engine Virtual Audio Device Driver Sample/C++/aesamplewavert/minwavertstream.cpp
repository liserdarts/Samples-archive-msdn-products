#include <msvad.h>
#include <common.h>
#include <limits.h>
#include <ks.h>
#include "simple.h"
#include "minwavert.h"
#include "minwavertstream.h"
#include "UnittestData.h"
#define MINWAVERTSTREAM_POOLTAG 'SRWM'
#define HNSTIME_PER_MILLISECOND 10000

#pragma warning (disable : 4127)

//=============================================================================
// CMiniportWaveRTStream
//=============================================================================

//=============================================================================
#pragma code_seg("PAGE")
CMiniportWaveRTStream::~CMiniportWaveRTStream
( 
    void 
)
/*++

Routine Description:

  Destructor for wavertstream 

Arguments:

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();
    if (NULL != m_pMiniport)
    {
        m_pMiniport->StreamClosed(m_ulPin);
        m_pMiniport->Release();
        m_pMiniport = NULL;
    }

    if (m_pDpc)
    {
        ExFreePoolWithTag( m_pDpc, MINWAVERTSTREAM_POOLTAG );
        m_pDpc = NULL;
    }

    if (m_pTimer)
    {
        ExFreePoolWithTag( m_pTimer, MINWAVERTSTREAM_POOLTAG );
        m_pTimer = NULL;
    }

    if (m_pbMuted)
    {
        ExFreePoolWithTag( m_pbMuted, MINWAVERTSTREAM_POOLTAG );
        m_pbMuted = NULL;
    }

    if (m_plVolumeLevel)
    {
        ExFreePoolWithTag( m_plVolumeLevel, MINWAVERTSTREAM_POOLTAG );
        m_plVolumeLevel = NULL;
    }

    if (m_plPeakMeter)
    {
        ExFreePoolWithTag( m_plPeakMeter, MINWAVERTSTREAM_POOLTAG );
        m_plPeakMeter = NULL;
    }

    if (m_pWfExt)
    {
        ExFreePoolWithTag( m_pWfExt, MINWAVERTSTREAM_POOLTAG );
        m_pWfExt = NULL;
    }
    if (m_pNotificationTimer)
    {
        KeCancelTimer(m_pNotificationTimer);
        ExFreePoolWithTag(m_pNotificationTimer, MINWAVERTSTREAM_POOLTAG);
    }

    // Since we just cancelled the notification timer, wait for all queued 
    // DPCs to complete before we free the notification DPC.
    //
    KeFlushQueuedDpcs();

    if (m_pNotificationDpc)
    {
        ExFreePoolWithTag( m_pNotificationDpc, MINWAVERTSTREAM_POOLTAG );
    }

    DPF_ENTER(("[CMiniportWaveRTStream::~CMiniportWaveRTStream]"));
} // ~CMiniportWaveRTStream

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS
CMiniportWaveRTStream::Init
( 
    IN PCMiniportWaveRT             Miniport_,
    IN PPORTWAVERTSTREAM            PortStream_,
    IN ULONG                        Pin_,
    IN BOOLEAN                      Capture_,
    IN PKSDATAFORMAT                DataFormat_
)
/*++

Routine Description:

  Initializes the stream object.

Arguments:

  Miniport_ -

  Pin_ -

  Capture_ -

  DataFormat -

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    PWAVEFORMATEX pWfEx = NULL;
    NTSTATUS ntStatus = STATUS_SUCCESS;

    m_pMiniport = NULL;
    m_ulPin = 0;
    m_bCapture = FALSE;
    m_ulDmaBufferSize = 0;
    m_KsState = KSSTATE_STOP;
    m_pTimer = NULL;
    m_pDpc = NULL;
    m_ullPlayPosition = 0;
    m_ullWritePosition = 0;
    m_ullDmaTimeStamp = 0;
    m_hnsElapsedTimeCarryForward = 0;
    m_ulDmaMovementRate = 0;
    m_hnsDisplacementCarryForward = 0;
    m_bLfxEnabled = FALSE;
    m_pbMuted = NULL;
    m_plVolumeLevel = NULL;
    m_plPeakMeter = NULL;
    m_pWfExt = NULL;
    m_ullLinearPosition = 0;

    m_pPortStream = PortStream_;
    InitializeListHead(&m_NotificationList);
    m_ulNotificationIntervalMs = 0;

    m_pNotificationDpc = (PRKDPC)ExAllocatePoolWithTag(
        NonPagedPool,
        sizeof(KDPC),
        MINWAVERTSTREAM_POOLTAG);
    if (!m_pNotificationDpc)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }

    m_pNotificationTimer = (PKTIMER)ExAllocatePoolWithTag(
        NonPagedPool,
        sizeof(KTIMER),
        MINWAVERTSTREAM_POOLTAG);
    if (!m_pNotificationTimer)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }

    KeInitializeDpc(m_pNotificationDpc, TimerNotifyRT, this);
    KeInitializeTimerEx(m_pNotificationTimer, NotificationTimer);

    pWfEx = GetWaveFormatEx(DataFormat_);
    if (NULL == pWfEx) 
    { 
        return STATUS_UNSUCCESSFUL; 
    }

    m_pMiniport = reinterpret_cast<CMiniportWaveRT*>(Miniport_);
    if (m_pMiniport == NULL)
    {
        return STATUS_INVALID_PARAMETER;
    }
    m_pMiniport->AddRef();
    if (!NT_SUCCESS(ntStatus))
    {
        return ntStatus;
    }
    m_ulPin = Pin_;
    m_bCapture = Capture_;
    m_ulDmaMovementRate = pWfEx->nAvgBytesPerSec;

    m_pDpc = (PRKDPC)ExAllocatePoolWithTag(NonPagedPool, sizeof(KDPC), MINWAVERTSTREAM_POOLTAG);
    if (!m_pDpc)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }

    m_pWfExt = (PWAVEFORMATEXTENSIBLE)ExAllocatePoolWithTag(NonPagedPool, sizeof(WAVEFORMATEX) + pWfEx->cbSize, MINWAVERTSTREAM_POOLTAG);
    if (m_pWfExt == NULL)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    RtlCopyMemory(m_pWfExt, pWfEx, sizeof(WAVEFORMATEX) + pWfEx->cbSize);

    m_pbMuted = (PBOOL)ExAllocatePoolWithTag(NonPagedPool, m_pWfExt->Format.nChannels * sizeof(BOOL), MINWAVERTSTREAM_POOLTAG);
    if (m_pbMuted == NULL)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    RtlZeroMemory(m_pbMuted, m_pWfExt->Format.nChannels * sizeof(BOOL));

    m_plVolumeLevel = (PLONG)ExAllocatePoolWithTag(NonPagedPool, m_pWfExt->Format.nChannels * sizeof(LONG), MINWAVERTSTREAM_POOLTAG);
    if (m_plVolumeLevel == NULL)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    RtlZeroMemory(m_plVolumeLevel, m_pWfExt->Format.nChannels * sizeof(LONG));

    m_plPeakMeter = (PLONG)ExAllocatePoolWithTag(NonPagedPool, m_pWfExt->Format.nChannels * sizeof(LONG), MINWAVERTSTREAM_POOLTAG);
    if (m_plPeakMeter == NULL)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }
    RtlZeroMemory(m_plPeakMeter, m_pWfExt->Format.nChannels * sizeof(LONG));


    return STATUS_SUCCESS;
} // Init

//=============================================================================
#pragma code_seg("PAGE")
STDMETHODIMP_(NTSTATUS)
CMiniportWaveRTStream::NonDelegatingQueryInterface
( 
    _In_ REFIID  Interface,
    _COM_Outptr_ PVOID * Object 
)
/*++

Routine Description:

  QueryInterface

Arguments:

  Interface - GUID

  Object - interface pointer to be returned

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    ASSERT(Object);

    if (IsEqualGUIDAligned(Interface, IID_IUnknown))
    {
        *Object = PVOID(PUNKNOWN(PMINIPORTWAVERTSTREAM(this)));
    }
    else if (IsEqualGUIDAligned(Interface, IID_IMiniportWaveRTStream))
    {
        *Object = PVOID(PMINIPORTWAVERTSTREAM(this));
    }
    else if (IsEqualGUIDAligned(Interface, IID_IMiniportWaveRTStreamNotification))
    {
        *Object = PVOID(PMINIPORTWAVERTSTREAMNOTIFICATION(this));
    }
    else if (IsEqualGUIDAligned(Interface, IID_IMiniportStreamAudioEngineNode))
    {
        *Object = (PVOID)(IMiniportStreamAudioEngineNode*)this;
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
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::AllocateBufferWithNotification
(
    _In_    ULONG               NotificationCount_,
    _In_    ULONG               RequestedSize_,
    _Out_   PMDL                *AudioBufferMdl_,
    _Out_   ULONG               *ActualSize_,
    _Out_   ULONG               *OffsetFromFirstPage_,
    _Out_   MEMORY_CACHING_TYPE *CacheType_
)
{
    UNREFERENCED_PARAMETER(NotificationCount_);

    PAGED_CODE();

    ULONG ulBufferDurationMs = 0;

    if ( (0 == RequestedSize_) || (RequestedSize_ < m_pWfExt->Format.nBlockAlign) )
    { 
        return STATUS_UNSUCCESSFUL; 
    }

    RequestedSize_ -= RequestedSize_ % (m_pWfExt->Format.nBlockAlign);

    PHYSICAL_ADDRESS highAddress;
    highAddress.HighPart = 0;
    highAddress.LowPart = MAXULONG;

    PMDL pBufferMdl = m_pPortStream->AllocatePagesForMdl (highAddress, RequestedSize_);

    if (NULL == pBufferMdl)
    {
        return STATUS_UNSUCCESSFUL;
    }

    m_ulDmaBufferSize = RequestedSize_;
    ulBufferDurationMs = (RequestedSize_ * 1000) / m_ulDmaMovementRate;
    m_ulNotificationIntervalMs = ulBufferDurationMs / NotificationCount_;

    *AudioBufferMdl_ = pBufferMdl;
    *ActualSize_ = RequestedSize_;
    *OffsetFromFirstPage_ = 0;
    *CacheType_ = MmCached;

    return STATUS_SUCCESS;
}

//=============================================================================
#pragma code_seg("PAGE")
VOID CMiniportWaveRTStream::FreeBufferWithNotification
(
    _In_        PMDL    Mdl_,
    _In_        ULONG   Size_
)
{
    UNREFERENCED_PARAMETER(Size_);

    PAGED_CODE();

    m_pPortStream->FreePagesFromMdl(Mdl_);
    m_ulDmaBufferSize = 0;

    return;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::RegisterNotificationEvent
(
    _In_ PKEVENT NotificationEvent_
)
{
    UNREFERENCED_PARAMETER(NotificationEvent_);

    PAGED_CODE();

    NotificationListEntry *nleNew = (NotificationListEntry*)ExAllocatePoolWithTag( 
        NonPagedPool,
        sizeof(NotificationListEntry),
        MINWAVERTSTREAM_POOLTAG);
    if (NULL == nleNew)
    {
        return STATUS_INSUFFICIENT_RESOURCES;
    }

    nleNew->NotificationEvent = NotificationEvent_;

    if (!IsListEmpty(&m_NotificationList))
    {
        PLIST_ENTRY leCurrent = m_NotificationList.Flink;
        while (leCurrent != &m_NotificationList)
        {
            NotificationListEntry* nleCurrent = CONTAINING_RECORD( leCurrent, NotificationListEntry, ListEntry);
            if (nleCurrent->NotificationEvent == NotificationEvent_)
            {
                RemoveEntryList( leCurrent );
                ExFreePoolWithTag( nleNew, MINWAVERTSTREAM_POOLTAG );
                return STATUS_UNSUCCESSFUL;
            }

            leCurrent = leCurrent->Flink;
        }
    }

    InsertTailList(&m_NotificationList, &(nleNew->ListEntry));
    
    return STATUS_SUCCESS;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::UnregisterNotificationEvent
(
    _In_ PKEVENT NotificationEvent_
)
{
    UNREFERENCED_PARAMETER(NotificationEvent_);

    PAGED_CODE();

    if (!IsListEmpty(&m_NotificationList))
    {
        PLIST_ENTRY leCurrent = m_NotificationList.Flink;
        while (leCurrent != &m_NotificationList)
        {
            NotificationListEntry* nleCurrent = CONTAINING_RECORD( leCurrent, NotificationListEntry, ListEntry);
            if (nleCurrent->NotificationEvent == NotificationEvent_)
            {
                RemoveEntryList( leCurrent );
                ExFreePoolWithTag( nleCurrent, MINWAVERTSTREAM_POOLTAG );
                return STATUS_SUCCESS;
            }

            leCurrent = leCurrent->Flink;
        }
    }

    return STATUS_NOT_FOUND;
}


//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetClockRegister
(
    _Out_ PKSRTAUDIO_HWREGISTER Register_
)
{
    UNREFERENCED_PARAMETER(Register_);

    PAGED_CODE();

    return STATUS_NOT_IMPLEMENTED;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::GetPositionRegister
(
    _Out_ PKSRTAUDIO_HWREGISTER Register_
)
{
    UNREFERENCED_PARAMETER(Register_);

    PAGED_CODE();

    return STATUS_NOT_IMPLEMENTED;
}

//=============================================================================
#pragma code_seg("PAGE")
VOID CMiniportWaveRTStream::GetHWLatency
(
    _Out_ PKSRTAUDIO_HWLATENCY  Latency_
)
{
    PAGED_CODE();

    ASSERT(Latency_);

    Latency_->ChipsetDelay = 0;
    Latency_->CodecDelay = 0;
    Latency_->FifoSize = 0;
}

//=============================================================================
#pragma code_seg("PAGE")
VOID CMiniportWaveRTStream::FreeAudioBuffer
(
    _In_opt_    PMDL    Mdl_,
    _In_        ULONG   Size_
)
{
    UNREFERENCED_PARAMETER(Size_);

    PAGED_CODE();
    if (Mdl_ != NULL)
    {
        m_pPortStream->FreePagesFromMdl(Mdl_);
    }
    m_ulDmaBufferSize = 0;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::AllocateAudioBuffer
(
    _In_    ULONG               RequestedSize_,
    _Out_   PMDL                *AudioBufferMdl_,
    _Out_   ULONG               *ActualSize_,
    _Out_   ULONG               *OffsetFromFirstPage_,
    _Out_   MEMORY_CACHING_TYPE *CacheType_
)
{
    PAGED_CODE();

    if ( (0 == RequestedSize_) || (RequestedSize_ < m_pWfExt->Format.nBlockAlign) )
    { 
        return STATUS_UNSUCCESSFUL; 
    }

    RequestedSize_ -= RequestedSize_ % (m_pWfExt->Format.nBlockAlign);

    PHYSICAL_ADDRESS highAddress;
    highAddress.HighPart = 0;
    highAddress.LowPart = MAXULONG;

    PMDL pBufferMdl = m_pPortStream->AllocatePagesForMdl (highAddress, RequestedSize_);

    if (NULL == pBufferMdl)
    {
        return STATUS_UNSUCCESSFUL;
    }

    m_ulDmaBufferSize = RequestedSize_;

    *AudioBufferMdl_ = pBufferMdl;
    *ActualSize_ = RequestedSize_;
    *OffsetFromFirstPage_ = 0;
    *CacheType_ = MmCached;

    return STATUS_SUCCESS;
}

//=============================================================================
#pragma code_seg("CODE")
NTSTATUS CMiniportWaveRTStream::GetPosition
(
    _Out_   KSAUDIO_POSITION    *Position_
)
{
    if (m_KsState == KSSTATE_RUN)
    {
        // Get the current time
        //
        LARGE_INTEGER ilQPC = KeQueryPerformanceCounter(NULL);

        LONGLONG  hnsCurrentTime = KSCONVERT_PERFORMANCE_TIME(m_ullPerformanceCounterFrequency.QuadPart, ilQPC);


        // Calculate the time elapsed since the last call to GetPosition() or since the
        // DMA engine started.  Note that the division by 10000 to convert to milliseconds
        // may cause us to lose some of the time, so we will carry the remainder forward 
        // to the next GetPosition() call.
        //
        ULONG TimeElapsedInMS = (ULONG)(hnsCurrentTime - m_ullDmaTimeStamp + m_hnsDisplacementCarryForward)/1000;


        // Calculate how many bytes in the DMA buffer would have been processed in the elapsed
        // time.  Note that the division by 1000 to convert to milliseconds may cause us to 
        // lose some bytes, so we will carry the remainder forward to the next GetPosition() call.
        //
        ULONG ByteDisplacement = (m_ulDmaMovementRate * TimeElapsedInMS) / 1000;

        // Carry forward the remainder of this division so we don't fall behind with our position.
        //
        m_hnsDisplacementCarryForward = (hnsCurrentTime - m_ullDmaTimeStamp + m_hnsDisplacementCarryForward) % 1000;

        // Increment the DMA position by the number of bytes displaced since the last
        // call to GetPosition() and ensure we properly wrap at buffer length.
        //
        m_ullPlayPosition = m_ullWritePosition =
            (m_ullWritePosition + ByteDisplacement) % m_ulDmaBufferSize;

        // m_ullDmaTimeStamp is updated in both GetPostion and GetLinearPosition calls
        // so m_ullLinearPosition needs to be updated accordingly here
        //
        m_ullLinearPosition += ByteDisplacement;
        // Update the DMA time stamp for the next call to GetPosition()
        //
        m_ullDmaTimeStamp = hnsCurrentTime;
    }

    Position_->PlayOffset = m_ullPlayPosition;
    Position_->WriteOffset = m_ullWritePosition;

    return STATUS_SUCCESS;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::SetState
(
    _In_    KSSTATE State_
)
{
    PAGED_CODE();

    PADAPTERCOMMON pAdapterComm = m_pMiniport->GetAdapterCommObj();

    // Spew an event for a pin state change request from portcls
    //Event type: eMINIPORT_PIN_STATE
    //Parameter 1: Current linear buffer position	
    //Parameter 2: Current WaveRtBufferWritePosition	
    //Parameter 3: Pin State 0->KS_STOP, 1->KS_ACQUIRE, 2->KS_PAUSE, 3->KS_RUN 
    //Parameter 4:0
    pAdapterComm->WriteEtwEvent(eMINIPORT_PIN_STATE, 
                                100, // replace with the correct "Current linear buffer position"	
                                m_ulCurrentWritePosition, // replace with the previous WaveRtBufferWritePosition that the drive received	
                                State_, // repalce with the correct "Data length completed"
                                0);  // always zero

    switch (State_)
    {
        case KSSTATE_STOP:
            // Reset DMA
            m_ullPlayPosition = 0;
            m_ullWritePosition = 0;
            m_ullLinearPosition = 0;
            break;

        case KSSTATE_ACQUIRE:
            break;

        case KSSTATE_PAUSE:
            // Pause DMA
            if (m_ulNotificationIntervalMs > 0)
                KeCancelTimer( m_pNotificationTimer );
            break;

        case KSSTATE_RUN:
            // Start DMA
            LARGE_INTEGER ullTemp;
            ullTemp = KeQueryPerformanceCounter(&m_ullPerformanceCounterFrequency);
            m_ullDmaTimeStamp = KSCONVERT_PERFORMANCE_TIME(m_ullPerformanceCounterFrequency.QuadPart, ullTemp);
            m_hnsElapsedTimeCarryForward  = 0;
            m_hnsDisplacementCarryForward = 0;

            if (m_ulNotificationIntervalMs > 0)
            {
                LARGE_INTEGER   delay;
                delay.HighPart  = 0;
                delay.LowPart   = m_ulNotificationIntervalMs * HNSTIME_PER_MILLISECOND * -1;

                KeSetTimerEx
                (
                    m_pNotificationTimer,
                    delay,
                    m_ulNotificationIntervalMs,
                    m_pNotificationDpc
                );
            }

            break;
    }

    m_KsState = State_;

    return STATUS_SUCCESS;
}

//=============================================================================
#pragma code_seg("PAGE")
NTSTATUS CMiniportWaveRTStream::SetFormat
(
    _In_    KSDATAFORMAT    *DataFormat_
)
{
    UNREFERENCED_PARAMETER(DataFormat_);

    PAGED_CODE();

    return STATUS_NOT_SUPPORTED;
}

#pragma code_seg()
//=============================================================================
void
TimerNotifyRT
(
    IN  PKDPC                   Dpc,
    IN  PVOID                   DeferredContext,
    IN  PVOID                   SA1,
    IN  PVOID                   SA2
)
{
    UNREFERENCED_PARAMETER(Dpc);
    UNREFERENCED_PARAMETER(SA1);
    UNREFERENCED_PARAMETER(SA2);

    CMiniportWaveRTStream* _this = (CMiniportWaveRTStream*)DeferredContext;

    if (NULL == _this)
        return;

    PADAPTERCOMMON pAdapterComm = _this->m_pMiniport->GetAdapterCommObj();


    if (!IsListEmpty(&_this->m_NotificationList))
    {
        PLIST_ENTRY leCurrent = _this->m_NotificationList.Flink;
        while (leCurrent != &_this->m_NotificationList)
        {
            NotificationListEntry* nleCurrent = CONTAINING_RECORD( leCurrent, NotificationListEntry, ListEntry);
            //Event type: eMINIPORT_BUFFER_COMPLETE
            //Parameter 1: Current linear buffer position	
            //Parameter 2: the previous WaveRtBufferWritePosition that the drive received
            //Parameter 3: Data length completed	
            //Parameter 4:0
            pAdapterComm->WriteEtwEvent(eMINIPORT_BUFFER_COMPLETE, 
                                        100, // replace with the correct "Current linear buffer position"	
                                        _this->GetCurrentWaveRTWrtiePosition(), 	
                                        300, // repalce with the correct "Data length completed"
                                        0);  // always zero

            KeSetEvent(nleCurrent->NotificationEvent, 0, 0);

            leCurrent = leCurrent->Flink;
        }
    }
}
//=============================================================================

