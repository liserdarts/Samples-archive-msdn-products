/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Sta_Adhoc.h

Abstract:
    STA layer adhoc functions
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#ifndef _STATION_ADHOC_H
#define _STATION_ADHOC_H

#define STA11_ADHOC_JOIN_TIMEOUT                2000            /* 2 seconds */
#define STA_ADHOC_MIN_UNREACHABLE_THRESHOLD     5000            /* 5 seconds */
#define STA_ADHOC_MAX_UNREACHABLE_THRESHOLD     30000           /* 30 seconds */
#define STA_DEAUTH_WAITING_THRESHOLD            3               /* 3 ticks, or 6 seconds */
#define STA_PROBE_REQUEST_LIMIT                 2               /* 2 probe requests */

/**
 * Information Element to be put in beacon for FHSS phy
 */
typedef struct STA_FHSS_IE {
    USHORT dot11DwellTime;
    UCHAR dot11HopSet;
    UCHAR dot11HopPattern;
    UCHAR dot11HopIndex;
} STA_FHSS_IE, *PSTA_FHSS_IE;

/**
 * Initializes the AdHoc station info
 * 
 * \param pStation  station pointer
 */
NDIS_STATUS
StaInitializeAdHocStaInfo(
    _In_  PSTATION        pStation
    );

/**
 * Free the AdHoc station info
 * 
 * \param pStation  station pointer
 */
VOID
StaFreeAdHocStaInfo(
    _In_  PSTATION        pStation
    );

NDIS_STATUS 
StaSaveAdHocStaInfo(
    _In_  PSTATION        pStation,
    _In_  PNIC_RX_FRAGMENT    pNicFragment,
    _In_  PDOT11_BEACON_FRAME pDot11BeaconFrame,
    _In_  ULONG           InfoElemBlobSize
    );

NDIS_STATUS
StaResetAdHocStaInfo(
    _In_  PSTATION       pStation,
    _In_  BOOLEAN        flushStaList
    );

void
StaClearStaListAssocState(
    _In_  PSTATION    pStation,
    _In_  BOOLEAN     SendDeauth
    );

EVT_WDF_WORKITEM StaConnectAdHoc;

void 
StaAdHocReceiveDirectData(
    _In_  PSTATION                    pStation,
    _In_  PDOT11_DATA_SHORT_HEADER    pDataHdr
    );

void 
StaAdHocIndicateAssociation(
    _In_  PSTATION pStation,
    _In_  PSTA_ADHOC_STA_ENTRY StaEntry
    );

NDIS_STATUS
StaFlushAdHocStaList(
    _In_  PSTATION         pStation
    );

EVT_WDF_TIMER StaAdHocWatchdogTimerRoutine;

VOID
StaAdhocProcessMgmtPacket(
    _In_  PSTATION                        pStation,
    _In_  PDOT11_MGMT_HEADER              MgmtPacket,
    _In_  ULONG                           PacketLength
    );

VOID
StaResumeAdHoc(
    _In_  PSTATION                        pStation
    );

#endif
