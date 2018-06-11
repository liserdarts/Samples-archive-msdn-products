/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Hw_Nic.c

Abstract:
    HW layer NIC specific function
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#define NIC_DELAY_POST_RESET            20

#define RX_DESC_SIZE    16
#define TX_DESC_SIZE    32

#define DoCoalesceThreshold     8

#define     NUM_11A_CHANNEL         46
#define     NUM_11G_CHANNEL         14

//   
//  regulatory domain
//
#define	DOMAIN_FROM_EEPROM				0x0100
#define	DOMAIN_FROM_COUNTRY				0x0200
#define	DOMAIN_FROM_UI					0x0400
#define	CHANNELPLAN_MASK                0x000f

// Regulatory domain values that are not defined yet in the windot11.h
#define HW_REG_DOMAIN_MKK1          0x00000041
#define HW_REG_DOMAIN_ISRAEL        0x00000042


#define MAX_BEACON_PERIOD       1024
#define LIMIT_BEACON_PERIOD(_Period)        \
    if((_Period)>=MAX_BEACON_PERIOD)        \
        (_Period)=MAX_BEACON_PERIOD-1;

#define	HWPERIOD_TIMER_IN_MS	2000 //ms

#define GetListHeadEntry(ListHead)  ((ListHead)->Flink)
#define GetListTailEntry(ListHead)  ((ListHead)->Blink)


// TODO: Testing Check For Hang hypothesis
/*
// N=1~...
#define HwIncrementNextDescToSend(  _pNic, _Queue, N)       \
    (_pNic)->TxInfo.TxNextDescToSend[_Queue]=((_pNic)->TxInfo.TxNextDescToSend[_Queue]+N)%(_pNic)->TxInfo.TxTotalDescNum[_Queue]

#define HwIncrementTxNextDescToCheck(_pNic, _Queue, N)      \
    (_pNic)->TxInfo.TxNextDescToCheck[_Queue]=((_pNic)->TxInfo.TxNextDescToCheck[_Queue]+N)%(_pNic)->TxInfo.TxTotalDescNum[_Queue]
*/

// N=1~...
#define HwIncrementNextDescToSend(_pNic, _Queue, N)     \
{                                                               \
    (_pNic)->TxInfo.TxNextDescToSend[_Queue]=((_pNic)->TxInfo.TxNextDescToSend[_Queue]+N)%(_pNic)->TxInfo.TxTotalDescNum[_Queue];   \
    InterlockedExchangeAdd(&(_pNic)->TxInfo.TxBusyDescCount[_Queue], (LONG)N);                 \
    MPVERIFY(HwGetTxBusyDescNum(_pNic, _Queue) == (ULONG)(_pNic)->TxInfo.TxBusyDescCount[_Queue]);   \
}

#define HwIncrementTxNextDescToCheck(_pNic, _Queue, N)      \
{                                                               \
    (_pNic)->TxInfo.TxNextDescToCheck[_Queue]=((_pNic)->TxInfo.TxNextDescToCheck[_Queue]+N)%(_pNic)->TxInfo.TxTotalDescNum[_Queue]; \
    InterlockedExchangeAdd(&(_pNic)->TxInfo.TxBusyDescCount[_Queue], (((LONG)N) * -1));                 \
    MPVERIFY(HwGetTxBusyDescNum(_pNic, _Queue) == (ULONG)(_pNic)->TxInfo.TxBusyDescCount[_Queue]);  \
}


// N=0~...
#define HwGetNextDescToSend(_pNic,_Queue, N)        \
    &(_pNic)->TxInfo.TxDesc[_Queue][((_pNic)->TxInfo.TxNextDescToSend[_Queue]+N)%(_pNic)->TxInfo.TxTotalDescNum[_Queue]]

#define HwGetNextDescToCheck(_pNic,_Queue, N)       \
    &(_pNic)->TxInfo.TxDesc[_Queue][((_pNic)->TxInfo.TxNextDescToCheck[_Queue]+N)%(_pNic)->TxInfo.TxTotalDescNum[_Queue]]

#define HwGetHWDESCAdjustedBuffer(Buf) \
           ( (PUCHAR)(Buf) + RTL8187_HWDESC_HEADER_LEN )

PVOID
HwNormalQGetNextToSendBuffer(
    _In_  PNIC       Nic
    );

PVOID
HwHighQGetNextToSendBuffer(
    _In_  PNIC       Nic
    );

#define HwIsAddr1Match(_TxBuf,_Addr)            \
    (BOOLEAN)(memcmp((PUCHAR)(_TxBuf)+4, _Addr, 6)==0)

#define HwIsBroadcast(_TxBuf)                                       \
    ((BOOLEAN)(                                                 \
                    ((((UCHAR *)(_TxBuf))[4])==(UCHAR)0xff) &&      \
                    ((((UCHAR *)(_TxBuf))[5])==(UCHAR)0xff) &&      \
                    ((((UCHAR *)(_TxBuf))[6])==(UCHAR)0xff) &&      \
                    ((((UCHAR *)(_TxBuf))[7])==(UCHAR)0xff) &&      \
                    ((((UCHAR *)(_TxBuf))[8])==(UCHAR)0xff) &&      \
                    ((((UCHAR *)(_TxBuf))[9])==(UCHAR)0xff)         \
                                                                ))

#define HwIsMulticast(_TxBuf)                   \
    ((BOOLEAN)(((_TxBuf)[4]&0x01)!=0))


typedef struct _TIMER_NIC_CONTEXT {
    PNIC        Nic;
}   TIMER_NIC_CONTEXT, *PTIMER_NIC_CONTEXT;

//
// Specify an accessor method for the WMI_SAMPLE_DEVICE_DATA structure.
//
WDF_DECLARE_CONTEXT_TYPE_WITH_NAME(TIMER_NIC_CONTEXT, GetTimerContext)

typedef struct _WORKITEM_CONTEXT {
    PNIC        Nic;
}   WORKITEM_CONTEXT, *PWORKITEM_CONTEXT;

//
// Specify an accessor method for the WMI_SAMPLE_DEVICE_DATA structure.
//
WDF_DECLARE_CONTEXT_TYPE_WITH_NAME(WORKITEM_CONTEXT, GetWorkItemContext)



void 
HwInitializeVariable(
    _In_ PNIC     Nic
    );

void 
HwResetVariable(
    _In_ PNIC     Nic
    );

VOID
HwSetHardwareID(
    _In_ PNIC     Nic
    );

NDIS_STATUS 
HwSetNICAddress(
    _In_  PNIC Nic
    );

BOOLEAN
HwSetRFState(
    _In_  PNIC                Nic,
    _In_  RT_RF_POWER_STATE   RFPowerState
    );

//
// Called at passive
//
VOID
HwSetRadioState(
    _In_  PNIC    Nic,
    _In_  BOOLEAN bPowerOn
    );


USHORT 
HwGenerateBeacon(
    _In_ PNIC        Nic,
    _In_ PUCHAR      BeaconBuf,
    _In_ BOOLEAN     APModeFlag
    );

void 
HwSetupBeacon(
    _In_  PNIC        Nic,
    _In_  PUCHAR      BeaconBuf,
    _In_  USHORT      BeaconBufLen,
    _In_  ULONG       BeaconBufPaLow,
    _In_  USHORT      Rate
    );

UCHAR
HwMRateToHwRate(
    _In_  UCHAR   Rate
    );

UCHAR
HwHwRateToMRate(
    _In_  UCHAR   Rate
    );

ULONG64
HwDataRateToLinkSpeed(
    UCHAR  Rate
    );

NDIS_STATUS
HwAllocateAlignPhyMemory(
    PNIC                    Nic,
    ULONG                   Length,
    PUCHAR                  *VirtualAddr,
    NDIS_PHYSICAL_ADDRESS   *PhysicalAddr,
    PUCHAR                  *AlignVirtualAddr,
    NDIS_PHYSICAL_ADDRESS   *AlignPhysicalAddr
    );

void
HwLinkTxDesc(
    PUCHAR                  VirtualAddr,
    NDIS_PHYSICAL_ADDRESS   PhysicalAddr,
    USHORT                  DescSize,
    USHORT                  DescNum
    );

ULONG
HwGetTxBusyDescNum(
    _In_  PNIC                    Nic,
    _In_  int                     QueueIndex
    );

ULONG
HwGetTxFreeDescNum(
    _In_  PNIC                    Nic,
    _In_  int                     QueueIndex
    );

void 
HwTransmitNextPacketWithLocalBuffer(
    _In_ PNIC     Nic,
    _In_ UCHAR    QueueIndex,
    _In_ USHORT   PktSize,
    _In_ USHORT   Rate,
    _In_ BOOLEAN  bAIDContained,
    _In_ PUCHAR   Desc
    );

void
HwOnProbe(
    _In_ PNIC     Nic,
    _In_ PUCHAR   ProbePktBuf,
    _In_ USHORT   ProbePktLen,
    _In_ USHORT   ProbePktRate
    );

void
HwSendProbe(
    _In_ PNIC                 Nic,
    _In_ PDOT11_MAC_ADDRESS   BSSID,
    _In_ PDOT11_SSID          SSID,
    _In_ PUCHAR               ScanAppendIEByteArray,
    _In_ USHORT               ScanAppendIEByteArrayLength
    );

USHORT
HwGetHighestRate(
    _In_ PDOT11_RATE_SET  RateSet
    );

LONG
HwGetPositionInBuffer(
    _In_  ULONG   PriorLength,
    _In_  ULONG   BufferLength,
    _In_  ULONG   TotalOffset
    );

ULONG
HwGetPhysicalAddressFromScatterGatherList(
    _In_ SCATTER_GATHER_LIST  *FragList,
    _In_ ULONG                Offset
    );

//
// Hardware receive related function
//

VOID
HwReturnFragment(
    _In_ PNIC             Nic,
    _In_ PNIC_RX_FRAGMENT NicFragment
    );

VOID
HwResetReceive(
    _In_  PNIC    Nic
    );

BOOLEAN
HwFragmentIsDuplicate(
                     _In_  PNIC              Nic,
                     _In_  PNIC_RX_FRAGMENT  NicFragment
                     );

void
HwHandleTimingCriticalPacket(
    _In_ PNIC         Nic
    );


VOID
HwResponseToPacket(
    _In_ PNIC             Nic,
    _In_ PNIC_RX_FRAGMENT NicFragment
    );

BOOLEAN
HwFilterPacket(
    _In_ PNIC             Nic,
    _In_ PNIC_RX_FRAGMENT NicFragment
    );

EVT_WDF_TIMER HwScanCallback;

EVT_WDF_TIMER HwAwakeCallback;

EVT_WDF_WORKITEM HwSwChnlWorkItem;

VOID
HwSwChnlWorker(
    PNIC    Nic 
    );

EVT_WDF_TIMER HwJoinTimeoutCallback;

EVT_WDF_TIMER HwPeriodTimerCallback;

NDIS_STATUS
HwUpdateInfoFromBSSInformation(
    _In_ PNIC             Nic,
    _In_ PSTA_BSS_ENTRY   BSSEntry
    );

NDIS_STATUS
HwSetPacketFilter(
    PNIC    Nic,
    ULONG   PacketFilter
    );
    
BOOLEAN
HwAddIE(
    _In_ PNIC     Nic,
    _In_ UCHAR    ID,
    _In_ UCHAR    Size,
    _In_ PUCHAR   Data
    );

VOID
HwRemoveIE(
    _In_ PNIC     Nic,
    _In_ UCHAR    ID,
    _In_ PUCHAR   Data
    );

PUCHAR
HwGetIE(
    _In_ PNIC     Nic,
    _In_ UCHAR    ID,
    _In_ UCHAR    Index,
    _Out_ PUCHAR  Size
    );

VOID
HwClearAllIE(
    _In_ PNIC     Nic
    );

BOOLEAN
HwSendNullPkt(
    _In_ PNIC     Nic
    );          

BOOLEAN
HwSendPSPoll(
    _In_ PNIC     Nic
    );

ULONG
HwComputeCrc(
    _In_ PUCHAR   Buffer,
    _In_ UINT     Length
    );

VOID
HwGetMulticastBit(
    _In_ UCHAR    Address[ETH_LENGTH_OF_ADDRESS],
    _Out_ UCHAR * Byte,
    _Out_ UCHAR * Value
    );

NDIS_STATUS
HwSetMulticastMask(
    _In_ PNIC     Nic,
    _In_ BOOLEAN  bAcceptAll
    );

VOID
HwDoze(
    _In_ PNIC     Nic,
    _In_ ULONG    ulDozeTime
    );

BOOLEAN
HwAwake(
    _In_ PNIC     Nic
    );

void
HwRemoveKeyEntry(
    _In_ PNIC Nic,
    _In_ UCHAR Index,
    _In_  DOT11_CIPHER_ALGORITHM  AlgoId
    );

void
HwAddKeyEntry(
    _In_ PNIC Nic,
    _In_ PNICKEY NicKey,
    _In_ UCHAR Index);

void
HwDeleteAllKeys(
    _In_  PNIC Nic
    );

NDIS_STATUS
HwUpdateInfoFromdot11BSSDescription(
    _In_ PNIC     Nic,
    _In_ PDOT11_BSS_DESCRIPTION   Dot11BSSDescription
    );

NDIS_STATUS
HwGetInfoIEWithOUI(
    _In_ PUCHAR pucInfoBlob,
    _In_ ULONG uSizeOfBlob,
    _In_ UCHAR ucInfoId,
    _Out_ PUCHAR pucLength,
    _Outptr_result_maybenull_ PVOID * ppvInfoEle,
    _Inout_ PULONG pOffset
    );

UCHAR
HwTxRetryLimit(
    _In_  PNIC    Nic,
    _In_  USHORT  DataRate    
    );

USHORT
GetSequenceNumber(
    _In_ PNIC        Nic
    );

VOID
HwUpdateTxRetryStat(
    _In_ PNIC                 pNic,
    _In_ PMP_TX_MSDU          pMpTxd,
    _In_ USHORT               retryCount
    );

VOID
HwUpdateTxDataRate(
    _In_ PNIC                     pNic
    );


