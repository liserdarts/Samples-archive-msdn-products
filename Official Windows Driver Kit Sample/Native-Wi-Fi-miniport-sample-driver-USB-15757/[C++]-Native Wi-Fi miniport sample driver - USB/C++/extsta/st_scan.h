/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Sta_Scan.h

Abstract:
    STA layer OS/internal scan functions
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#ifndef _STATION_SCAN_H_
#define _STATION_SCAN_H_


/** OS initiated scan is in progress (running) */
#define STA_EXTERNAL_SCAN_IN_PROGRESS           0x00000001

/** We are completing an external scan */
#define STA_COMPLETE_EXTERNAL_SCAN              0x00000002

/** Internal perdiodic scan is in progress (running) */
#define STA_PERIODIC_SCAN_IN_PROGRESS           0x00000010

/** We are completing a periodic scan */
#define STA_COMPLETE_PERIODIC_SCAN              0x00000020

/** Internal perdiodic scan has been queued to run (queued but not yet run) */
#define STA_PERIODIC_SCAN_QUEUED                0x00000040

/** Stop periodic scanning (dont requeue) */
#define STA_STOP_PERIODIC_SCAN                  0x00000100

/* Macros to change scanning flags */
#define STA_SET_SCAN_FLAG(_S, _F)       MP_SET_FLAG(&((_S)->ScanContext), (_F))
#define STA_TEST_SCAN_FLAG(_S, _F)      MP_TEST_FLAG(&((_S)->ScanContext), (_F))
#define STA_CLEAR_SCAN_FLAG(_S, _F)     MP_CLEAR_FLAG(&((_S)->ScanContext), (_F))

NDIS_STATUS
StaInitializeScanContext(
    _In_  PSTATION            pStation
    );

VOID
StaFreeScanContext(
    _In_  PSTATION            pStation
    );

VOID
StaStopPeriodicScan(
    _In_  PSTATION            pStation
    );

VOID
StaStartPeriodicScan(
    _In_  PSTATION            pStation
    );

VOID
StaWaitForPeriodicScan(
    _In_  PSTATION            pStation
    );

VOID
StaForceInternalScan(
    _In_  PSTATION            pStation,
    _In_  BOOLEAN             bScanToRoam
    );

_IRQL_requires_(DISPATCH_LEVEL)
VOID
StaPeriodicScanComplete(
    _In_  PSTATION            pStation,
    _In_  NDIS_STATUS         ndisStatus
    );

#endif
