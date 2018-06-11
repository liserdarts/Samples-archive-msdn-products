/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Sta_misc.h

Abstract:
    STA layer utility functions
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#ifndef _STATION_MISC_H
#define _STATION_MISC_H

#define STA_INVALID_PHY_ID          0x80000000U

#define RSNA_OUI_PREFIX             0xac0f00
#define WPA_OUI_PREFIX              0xf25000

#define RSNA_CIPHER_WEP40           0x01000000
#define RSNA_CIPHER_TKIP            0x02000000
#define RSNA_CIPHER_CCMP            0x04000000
#define RSNA_CIPHER_WEP104          0x05000000

#define RSNA_AKM_RSNA               0x01000000
#define RSNA_AKM_RSNA_PSK           0x02000000

#define RSNA_CAPABILITY_PRE_AUTH    0x01
#define RSNA_CAPABILITY_NO_PAIRWISE 0x02

//
// This structure is used for extracting RSN IE in beacon or probe response frame.
// Both RSN (WPA2) and WPA share the same structure. However, for WPA, PMKIDCount
// is also 0 and PKMID NULL.
// 
typedef struct _RSN_IE_INFO
{
    ULONG   OUIPrefix;
    USHORT  Version;
    USHORT  GroupCipherCount;
    USHORT  PairwiseCipherCount;
    USHORT  AKMSuiteCount;
    USHORT  Capability;
    USHORT  PMKIDCount;
    PUCHAR  GroupCipher;
    PUCHAR  PairwiseCipher;
    PUCHAR  AKMSuite;
    PUCHAR  PMKID;
} RSN_IE_INFO, *PRSN_IE_INFO;

NDIS_STATUS
StaGetRateSetFromInfoEle(
    _In_  PUCHAR InfoElemBlobPtr,
    _In_  ULONG InfoElemBlobSize,
    _In_  BOOLEAN basicRateOnly,
    _Out_ PDOT11_RATE_SET rateSet
    );

NDIS_STATUS
StaFilterUnsupportedRates(
    _In_ PSTA_BSS_ENTRY pAPEntry, 
    _In_ PDOT11_RATE_SET rateSet
    );

VOID
StaGenerateRandomBSSID(
    _In_  DOT11_MAC_ADDRESS MACAddr,
    _Out_ DOT11_MAC_ADDRESS BSSID
    );

ULONG
StaGetPhyIdByType(
    _In_ PNIC pNic,
    _In_ DOT11_PHY_TYPE PhyType
    );

DOT11_PHY_TYPE
StaGetPhyTypeById(
    _In_ PNIC pNic,
    _In_ ULONG PhyId
    );

NDIS_STATUS
StaParseRNSIE(
    _In_ PUCHAR RSNIEData,
    _In_ ULONG OUIPrefix,
    _In_ UCHAR RSNIELength,
    _Out_ PRSN_IE_INFO RSNIEInfo
    );

DOT11_CIPHER_ALGORITHM
StaGetGroupCipherFromRSNIEInfo(
    _In_ PRSN_IE_INFO RSNIEInfo
    );

DOT11_CIPHER_ALGORITHM
StaGetPairwiseCipherFromRSNIEInfo(
    _In_ PRSN_IE_INFO RSNIEInfo,
    _In_ USHORT index
    );

DOT11_AUTH_ALGORITHM
StaGetAKMSuiteFromRSNIEInfo(
    _In_ PRSN_IE_INFO RSNIEInfo,
    _In_ USHORT index
    );

NDIS_STATUS
StaAttachInfraRSNIE(
    _In_ PSTATION pStation, 
    _In_ PSTA_BSS_ENTRY pAPEntry, 
    _Inout_ PUCHAR *ppCurrentIE,
    _Inout_ PUSHORT pIESize
    );

NDIS_STATUS
StaAttachAdHocRSNIE(
    _In_ PSTATION pStation, 
    _Inout_ PUCHAR *ppCurrentIE,
    _Inout_ PUSHORT pIESize
    );

BOOLEAN
StaMatchRSNInfo(
    _In_ PSTATION        pStation,
    _In_ PRSN_IE_INFO    RSNIEInfo
    );

#endif // _STATION_MAIN_H
