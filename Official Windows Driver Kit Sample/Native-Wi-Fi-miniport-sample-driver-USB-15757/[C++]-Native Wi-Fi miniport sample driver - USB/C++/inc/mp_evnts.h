/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Mp_Events.h

Abstract:
    Prototypes for MP layer hooking functions
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#ifndef _MP_CUSTOM_INTERFACES_H_

#define _MP_CUSTOM_INTERFACES_H_


#define OID_RTL_SAMPLE_OID       0xDEEF1201
#define OID_RTL_SAMPLE2_OID      0xDEEF1202


/*
 * Any OIDs that are private to this this miniport should be added to the
 * macro below. At compile time this macro will aggregate these OIDs into
 * the supported OID list and automatically report them to the OS.
 *
 * If there is at least one OID in the list, please remember to include the
 * comma after the last OID. You will see compiler errors otherwise
 */
#define MP_PRIVATE_OID_LIST         \
    OID_RTL_SAMPLE_OID,               \
    OID_RTL_SAMPLE2_OID,
    


NDIS_STATUS
MpEventInitialize(
    _In_ PADAPTER    pAdapter
    );


VOID
MpEventTerminate(
    _In_ PADAPTER    pAdapter
    );


NDIS_STATUS
MpEventQueryInformation(
    _In_   PADAPTER                  pAdapter,
    _In_   NDIS_OID                  Oid,
    _In_   PVOID                     InformationBuffer,
    _In_   ULONG                     InformationBufferLength,
    _Out_   PULONG                    BytesWritten,
    _Out_   PULONG                    BytesNeeded
    );


NDIS_STATUS
MpEventSetInformation(
    _In_   PADAPTER                  pAdapter,
    _In_   NDIS_OID                  Oid,
    _In_   PVOID                     InformationBuffer,
    _In_   ULONG                     InformationBufferLength,
    _Out_   PULONG                    BytesRead,
    _Out_   PULONG                    BytesNeeded
    );

NDIS_STATUS
MpEventQuerySetInformation(
    _In_   PADAPTER                  pAdapter,
    _In_   NDIS_OID                  Oid,
    _In_   PVOID                     InformationBuffer,
    _In_   ULONG                     InputBufferLength,
    _In_   ULONG                     OutputBufferLength,
    _In_   ULONG                     MethodId,
    _Out_   PULONG                    BytesWritten,
    _Out_   PULONG                    BytesRead,
    _Out_   PULONG                    BytesNeeded
    );

BOOLEAN
MpEventCheckForHang(
    _In_ PADAPTER    pAdapter
    );


VOID
MpEventDot11Reset(
    _In_ PADAPTER            pAdapter,
    _In_ DOT11_RESET_TYPE    ResetType
    );


VOID
MpEventMiniportReset(
    _In_ PADAPTER    pAdapter
    );


NDIS_STATUS
MpEventSendNetBufferList(
    _In_ PADAPTER                        pAdapter,
    _In_ PNET_BUFFER_LIST                NetBufferList,
    _In_ ULONG                           SendFlags,
    _In_ PDOT11_EXTSTA_SEND_CONTEXT      pDot11SendContext,
    _In_ BOOLEAN                         DispatchLevel
    );

BOOL
MpEventShouldBufferPacket(
    _In_ PADAPTER                        pAdapter,
    _In_ PDOT11_EXTSTA_SEND_CONTEXT      pDot11SendContext
    );

VOID
MpEventSendComplete(
    _In_ PADAPTER        pAdapter,
    _In_ PMP_TX_MSDU     pTxd,
    _In_ NDIS_STATUS     ndisStatus,
    _In_ BOOLEAN         DispatchLevel
    );


NDIS_STATUS
MpEventPacketReceived(
    _In_ PADAPTER                        pAdapter,
    _In_ PMP_RX_FRAGMENT                 pMpFragment,
    _In_ PNIC_RX_FRAGMENT                pNicFragment,
    _In_ PDOT11_MAC_HEADER               pFragmentHdr,
    _In_ USHORT                          uFragmentSize
    );

VOID
MpEventHandleInterrupt(
    _In_ PADAPTER        pAdapter
    );

#endif  // _MP_CUSTOM_INTERFACES_H_
