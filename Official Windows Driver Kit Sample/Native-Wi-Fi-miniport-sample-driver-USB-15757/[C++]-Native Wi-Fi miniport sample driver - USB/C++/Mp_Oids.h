/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Mp_Oids.h

Abstract:
    OID processing code
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#ifndef _MP_OIDS_H_

#define _MP_OIDS_H_

VOID
MpQuerySupportedOidsList(
    _Inout_ PNDIS_OID *SupportedOidList,
    _Inout_ PULONG    SupportedOidListLength
    );

NDIS_STATUS
MpQuerySupportedPHYTypes(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpResetRequest(
    _In_  PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_  ULONG InputBufferLength,
    _In_  ULONG OutputBufferLength,
    _Out_ PULONG BytesRead,
    _Out_ PULONG BytesWritten,
    _Out_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetMulticastList(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryMPDUMaxLength(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryOperationModeCapability(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCurrentOperationMode(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryATIMWindow(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryIndicateTXStatus(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryNicPowerState(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryOptionalCapability(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCurrentOptionalCapability(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCFPollable(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryPowerMgmtMode(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryOperationalRateSet(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryBeaconPeriod(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryRTSThreshold(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryShortRetryLimit(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryLongRetryLimit(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryFragmentationThreshold(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryMaxTXMSDULifeTime(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryMaxReceiveLifeTime(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCurrentRegDomain(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryTempType(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCurrentTXAntenna(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryDiversitySupport(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCurrentRXAntenna(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQuerySupportedPowerLevels(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpPnPQueryPower(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength
    );

NDIS_STATUS
MpQueryCurrentTXPowerLevel(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCurrentChannel(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCCAModeSupported(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCurrentCCA(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryEDThreshold(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryRegDomainsSupportValue(
    _In_ PADAPTER pAdapter,
    _Out_writes_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQuerySupportedTXAntenna(
    _In_ PADAPTER pAdapter,
    _Out_writes_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQuerySupportedRXAntenna(
    _In_ PADAPTER pAdapter,
    _Out_writes_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryDiversitySelectionRX(
    _In_ PADAPTER pAdapter,
    _Out_writes_bytes_(InformationBufferLength) PVOID  InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Out_ PULONG                  BytesWritten,
    _Out_ PULONG                  BytesNeeded
    );

NDIS_STATUS
MpQuerySupportedDataRatesValue(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetATIMWindow(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetIndicateTXStatus(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetNicPowerState(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryStationId(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryPnPCapabilities(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetPower(
    _In_  PADAPTER        pAdapter,
    _In_  PVOID           InformationBuffer,
    _In_  ULONG           InformationBufferLength,
    _In_  PULONG          pulBytesNeeded,
    _In_  PULONG          pulBytesRead
    );

NDIS_STATUS
MpAddWakeUpPattern(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpRemoveWakeUpPattern(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetOperationalRateSet(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetBeaconPeriod(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetRTSThreshold(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetFragmentationThreshold(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetCurrentRegDomain(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );
    
NDIS_STATUS
MpSetCurrentChannel(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetCurrentOperationMode(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpScanRequest(
    PADAPTER pAdapter,
    _In_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Out_ PULONG BytesRead,
    _Out_ PULONG BytesNeeded
    );



NDIS_STATUS
MpSetPacketFilter(
    _In_ PADAPTER pAdapter,
    _In_ ULONG PacketFilter
    );

NDIS_STATUS
MpEnumerateBSSList(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InputBufferLength,
    _In_ ULONG OutputBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryDesiredSSIDList(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryExcludedMACAddressList(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Out_ PULONG BytesWritten,
    _Out_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryDesiredBSSIDList(
    _In_ PADAPTER pAdapter,
    _Out_writes_bytes_(InformationBufferLength) PVOID  InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Out_ PULONG                  BytesWritten,
    _Out_ PULONG                  BytesNeeded
    );

NDIS_STATUS
MpQueryDesiredBSSType(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryDot11Statistics(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryEnabledAuthenticationAlgorithm(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQuerySupportedUnicastAlgorithmPair(
    _In_ PADAPTER pAdapter,
    _Out_writes_bytes_(InformationBufferLength) PVOID  InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Out_ PULONG                  BytesWritten,
    _Out_ PULONG                  BytesNeeded
    );

NDIS_STATUS
MpQuerySupportedMulticastAlgorithmPair(
    _In_ PADAPTER pAdapter,
    _Out_writes_bytes_(InformationBufferLength) PVOID  InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Out_ PULONG                  BytesWritten,
    _Out_ PULONG                  BytesNeeded
    );

NDIS_STATUS
MpQueryEnabledUnicastCipherAlgorithm(
    _In_ PADAPTER pAdapter,
    _Out_writes_bytes_(InformationBufferLength) PVOID  InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Out_ PULONG                  BytesWritten,
    _Out_ PULONG                  BytesNeeded
    );

NDIS_STATUS
MpQueryEnabledMulticastCipherAlgorithm(
    _In_ PADAPTER pAdapter,
    _Out_writes_bytes_(InformationBufferLength) PVOID  InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Out_ PULONG                  BytesWritten,
    _Out_ PULONG                  BytesNeeded
    );

NDIS_STATUS
MpEnumerateAssociationInformation(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryHardwarePHYState(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryDesiredPHYList(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryCurrentPHYID(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryMediaSteamingOption(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryUnreachableDetectionThreshold(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryActivePHYList(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );
    
NDIS_STATUS
MpQueryDot11TableSize(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpQueryDataRateMappingTable(
    _In_ PADAPTER pAdapter,
    _Inout_updates_bytes_(InformationBufferLength) PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesWritten,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpFlushBSSList(
    _In_ PADAPTER pAdapter
    );

NDIS_STATUS
MpSetPowerMgmtMode(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetDesiredSSIDList(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetExcludedMACAddressList(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetDesiredBSSIDList(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetDesiredBSSType(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpConnectRequest(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );


NDIS_STATUS
MpDisconnectRequest(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetEnabledAuthenticationAlgorithm(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetDesiredPHYList(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );

NDIS_STATUS
MpSetCurrentPHYID(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );
    
NDIS_STATUS
MpSetMediaStreamingOption(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );
    
NDIS_STATUS
MpSetUnreachableDetectionThreshold(
    _In_ PADAPTER pAdapter,
    _Inout_ PVOID InformationBuffer,
    _In_ ULONG InformationBufferLength,
    _Inout_ PULONG BytesRead,
    _Inout_ PULONG BytesNeeded
    );


NDIS_STATUS
MpQueryInformation(
    _In_    NDIS_HANDLE               MiniportAdapterContext,
    _In_    NDIS_OID                  Oid,
    _In_    PVOID                     InformationBuffer,
    _In_    ULONG                     InformationBufferLength,
    _Out_   PULONG                    BytesWritten,
    _Out_   PULONG                    BytesNeeded
    );


NDIS_STATUS
MpSetInformation(
    _In_    NDIS_HANDLE               MiniportAdapterContext,
    _In_    NDIS_OID                  Oid,
    _In_    PVOID                     InformationBuffer,
    _In_    ULONG                     InformationBufferLength,
    _Out_   PULONG                    BytesRead,
    _Out_   PULONG                    BytesNeeded
    );

NDIS_STATUS
MpQuerySetInformation(
    _In_    PADAPTER                  pAdapter,
    _In_    NDIS_OID                  Oid,
    _In_    PVOID                     InformationBuffer,
    _In_    ULONG                     InputBufferLength,
    _In_    ULONG                     OutputBufferLength,
    _In_    ULONG                     MethodId,
    _Out_   PULONG                    BytesWritten,
    _Out_   PULONG                    BytesRead,
    _Out_   PULONG                    BytesNeeded
    );

#endif  // _MP_OIDS_H_
