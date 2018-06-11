
NDIS_STATUS
HwInitializeBus(
    _In_ PNIC Nic
    )  ;

VOID
HwDeInitBus(
    _In_ PNIC Nic
    );

NDIS_STATUS
HwReadAdapterInfo(
    _In_ PNIC Nic
    ) ;

NDIS_STATUS
HwInitializeAdapter(
    _In_ PNIC Nic
    );

NDIS_STATUS
ReadNicInfo8187(
    PNIC         Nic
    );

VOID
InitializeVariables8187(
    PNIC         Nic
    );
 
NDIS_STATUS
HwBusStartDevice(
                _In_ PNIC Nic
                 );


NDIS_STATUS
HwBusFreeRecvResources(
    _In_ PNIC Nic
    );


NDIS_STATUS
HwBusAllocateRecvResources(
    _In_ PNIC Nic
    );

NDIS_STATUS
HwBusAllocateXmitResources(
    _In_ PNIC Nic ,
    _In_  ULONG NumTxd
    )  ;

VOID
HwBusFreeXmitResources(
    _In_ PNIC Nic
    ) ;

NDIS_STATUS
HwBusFindAdapterAndResources(
    _In_ PNIC Nic
    ) ;


NDIS_STATUS
HwBusSendPacketAsync(
    _In_ PNIC             Nic,
    _In_ UCHAR            QueueType,
    _In_ ULONG            BufferLength,
    _In_ PVOID            Buffer,
    _In_opt_ PNIC_TX_MSDU     NicTxd 
    ) ;

VOID
HwBusStopNotification(
    _In_ PNIC Nic
    );

VOID
HwBusFreeRecvFragment(
                _In_  PNIC Nic,
                _In_  PNIC_RX_FRAGMENT NicFragment
                ) ;

