/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Mp_Element.h

Abstract:
    Prototypes of information element processing functions
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#ifndef _NATIVE_WIFI_UTIL_H_

#define _NATIVE_WIFI_UTIL_H_

#define WPA_IE_TAG      0x01f25000

//
// Dot11ValidateInfoBlob and Dot11ValidatePacketInfoBlob
//  Validate the information blob. Dot11ValidateInfoBlob works
//  on a contiguous buffer while Dot11ValidatePacketInfoBlob
//  works on a MDL chain.
//
NDIS_STATUS
Dot11ValidateInfoBlob(
    _In_ PUCHAR pucInfoBlob,
    _In_ ULONG uOffsetOfInfoEleBlob
    );

NDIS_STATUS
Dot11GetInfoBlobSize(
    _In_  PUCHAR                  pPacketBuffer,
    _In_  ULONG                   PacketLength,
    _In_  ULONG                   OffsetOfInfoElemBlob,
    _Out_ PULONG                  pInfoElemBlobSize
    );

NDIS_STATUS
Dot11ValidatePacketInfoBlob(
    _In_ PNDIS_BUFFER pMdlHead,
    _In_ ULONG uOffsetOfInfoEleBlob
    );

//
// Dot11GetInfoEle and Dot11GetInfoEleFromPacket
//      Look up a particular information element in the
//      information blob.
//
// Dot11GetInfoEle works on a contiguous buffer while
// Dot11GetInfoEleFromPacket works on a MDL chain
//
NDIS_STATUS
Dot11GetInfoEle(
    _In_ PUCHAR pucInfoBlob,
    _In_ ULONG uSizeOfBlob,
    _In_ UCHAR ucInfoId,
    _Out_ PUCHAR pucLength,
    _Outptr_result_maybenull_ PVOID * ppvInfoEle
    );

NDIS_STATUS
Dot11GetInfoEleFromPacket(
    _In_ PNDIS_BUFFER pMdlHead,
    _In_ ULONG uOffsetOfInfoEleBlob,
    _In_ UCHAR ucInfoId,
    _In_ UCHAR ucMaxLength,
    _Out_ PUCHAR pucLength,
    _Inout_ PVOID * ppvInfoEle
    );

NDIS_STATUS
Dot11CopySSIDFromPacket(
    _In_ PNDIS_BUFFER pMdlHead,
    _In_ ULONG uOffsetOfInfoEleBlob,
    _In_ PDOT11_SSID pDot11SSID
    );

NDIS_STATUS
Dot11GetChannelForDSPhy(
    _In_ PUCHAR pucInfoBlob,
    _In_ ULONG uSizeOfBlob,
    _In_ PUCHAR pucChannel
    );

NDIS_STATUS
Dot11CopySSIDFromMemoryBlob(
    _In_ PUCHAR pucInfoBlob,
    _In_ ULONG uSizeOfBlob,
    _In_ PDOT11_SSID pDot11SSID
    );

NDIS_STATUS
Dot11AttachElement(
    _Inout_ PUCHAR *ppucBlob,
    _Inout_ USHORT *pusBlobSize,
    _In_ UCHAR ucElementId,
    _In_ UCHAR ucElementLength,
    _In_ PUCHAR pucElementInfo
    );

NDIS_STATUS
Dot11CopyInfoEle(
    _In_ PUCHAR pucInfoBlob,
    _In_ ULONG uSizeOfBlob,
    _In_ UCHAR ucInfoId,
    _Out_ PUCHAR pucLength,
    _In_ ULONG uBufSize,
    _Inout_ PVOID pvInfoEle
    );

NDIS_STATUS
Dot11GetWPAIE(
    _In_ PUCHAR pucInfoBlob,
    _In_ ULONG uSizeOfBlob,
    _Out_ PUCHAR pucLength,
    _Inout_ PVOID * ppvInfoEle
    );

#endif  // _NATIVE_WIFI_UTIL_H_
