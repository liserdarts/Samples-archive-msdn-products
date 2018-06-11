/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Mp_Main.h

Abstract:
    Mp layer major functions
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#ifndef _NATIVE_WIFI_MAIN_H_

#define _NATIVE_WIFI_MAIN_H_


//
// Driver Entry and Exit Points
//
DRIVER_INITIALIZE DriverEntry;

MINIPORT_UNLOAD DriverUnload;



//
// Handlers for Entry Points from NDIS
//

MINIPORT_INITIALIZE MPInitialize;

MINIPORT_RESTART MPRestart;

MINIPORT_PAUSE MPPause;

MINIPORT_HALT MPHalt;

MINIPORT_OID_REQUEST MPRequest;

MINIPORT_CANCEL_OID_REQUEST MPCancelRequest;

MINIPORT_SET_OPTIONS MPSetOptions;

MINIPORT_SEND_NET_BUFFER_LISTS MPSendNetBufferLists;

MINIPORT_RETURN_NET_BUFFER_LISTS MPReturnNetBufferLists;

MINIPORT_CHECK_FOR_HANG MPCheckForHang;

MINIPORT_RESET MPReset;

MINIPORT_CANCEL_SEND MPCancelSendNetBufferLists;

MINIPORT_DEVICE_PNP_EVENT_NOTIFY MPDevicePnPEvent;

MINIPORT_SHUTDOWN MPAdapterShutdown;


INLINE 
BOOLEAN
MpRemoveAdapter(
    _In_ PADAPTER pAdapter
    );





//
// Functions that will be used by Miniport internally.
// Mostly consist of helper API. Their implementation
// is spread around in other files.
//


NDIS_STATUS
MpInitializeSendEngine(
    _In_ PADAPTER            Adapter
    );

VOID
MpReinitializeSendEngine(
    _In_ PADAPTER     Adapter
    );

VOID
MpTerminateSendEngine(
    _In_ PADAPTER Adapter
    );

BOOL
MPCanTransmit(
    _In_ PADAPTER         Adapter
    );

PMP_TX_MSDU
MPReserveTxResources(
    _In_ PADAPTER                 Adapter,
    _In_ PNET_BUFFER_LIST         NetBufferList
    );

VOID
MpSendTxMSDUs(
    _In_ PADAPTER         Adapter,
    _In_ PMP_TX_MSDU      pTxd,
    _In_ ULONG            NumTxd,
    _In_ BOOLEAN          DispatchLevel
    );

NDIS_STATUS
MpSendSingleTxMSDU(
    _In_ PADAPTER         Adapter,
    _In_ PMP_TX_MSDU      pTxd,
    _In_ BOOLEAN          DispatchLevel
    );

NDIS_STATUS
MpTransmitTxMSDU(
    _In_ PADAPTER         Adapter,
    _In_ PMP_TX_MSDU      pTxd,
    _In_ BOOLEAN          DispatchLevel
    );

VOID
MpSendReadyTxMSDUs(
    _In_  PADAPTER        Adapter,
    _In_ BOOLEAN          DispatchLevel
    );

VOID
MpHandleSendCompleteInterrupt(
    _In_ PADAPTER         Adapter
    );

_IRQL_requires_(DISPATCH_LEVEL)
VOID
MpCompleteQueuedTxMSDUs(
    _In_ PADAPTER    Adapter
    );

_IRQL_requires_(DISPATCH_LEVEL)
VOID
MpCompleteQueuedNBL(
    _In_ PADAPTER    Adapter
    );

NDIS_STATUS
MpGetAdapterStatus(
    _In_ PADAPTER         Adapter
    );

 
 _IRQL_requires_(DISPATCH_LEVEL)
NDIS_STATUS
MpResetInternalRoutine(
    _In_ PADAPTER             Adapter,
    _In_ MP_RESET_TYPE        ResetType
    ) ;

EVT_WDF_WORKITEM MpDot11ResetWorkItem;

EVT_WDF_WORKITEM MpNdisResetWorkItem;

VOID
MpDot11ResetComplete(
    _In_  PADAPTER                Adapter,
    _In_  PDOT11_RESET_REQUEST    pDot11ResetRequest
    );


VOID
MpHandleDefaultReceiveInterrupt(
    _In_ PADAPTER         Adapter,
    PNIC_RX_FRAGMENT    NicFragment,
    _In_ size_t           Size  
    );

VOID
MpHandleRawReceiveInterrupt(
    _In_ PADAPTER         Adapter,
    PNIC_RX_FRAGMENT    NicFragment,
    _In_ size_t           Size
    ) ;

VOID
MpHandleSafeModeReceiveInterrupt(
    _In_ PADAPTER         Adapter,
    PNIC_RX_FRAGMENT    NicFragment,
    _In_ size_t           Size
    );

VOID
MpExpireReassemblingPackets(
    _In_  PADAPTER    Adapter,
    _In_  BOOLEAN     DispatchLevel
    );

VOID
MpPSPacketsManagerDeliverDTIMPackets(
    _In_  PADAPTER                    Adapter
    );

VOID
MpResetMacMIBs(
    _In_  PADAPTER    Adapter
    );

VOID
MpResetPhyMIBs(
    _In_  PADAPTER    Adapter
    );

VOID
MpAdjustReceiveHandler(
    _In_ PADAPTER         pAdapter
    );

#endif  // _NATIVE_WIFI_MAIN_H_
