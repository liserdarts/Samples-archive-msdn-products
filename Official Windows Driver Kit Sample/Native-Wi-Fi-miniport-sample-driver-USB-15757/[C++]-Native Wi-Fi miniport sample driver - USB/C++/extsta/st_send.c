/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Sta_Send.c

Abstract:
    STA layer send processing functions
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#include "precomp.h"
#include "st_scan.h"

#if DOT11_TRACE_ENABLED
#include "Sta_Send.tmh"
#endif

BOOLEAN
Sta11CanTransmit(
    _In_  PSTATION        pStation
    )
{
    //
    // If we are in the middle of a scan, this send must be queued
    //
    if (STA_TEST_SCAN_FLAG(pStation, (STA_COMPLETE_PERIODIC_SCAN | STA_COMPLETE_EXTERNAL_SCAN)))
        return FALSE;

    return TRUE;
}

NDIS_STATUS
Sta11SendNetBufferList(
    _In_  PSTATION        pStation,
    _In_  PNET_BUFFER_LIST    pNetBufferList,
    _In_  ULONG           SendFlags,
    _In_  PDOT11_EXTSTA_SEND_CONTEXT  pSendContext
    )
{
    UNREFERENCED_PARAMETER(pStation);
    UNREFERENCED_PARAMETER(pNetBufferList);
    UNREFERENCED_PARAMETER(SendFlags);
    UNREFERENCED_PARAMETER(pSendContext);
    
    return NDIS_STATUS_NOT_RECOGNIZED;
}

VOID
Sta11ProcessSend(
    _In_  PSTATION        pStation,
    _In_  PMP_TX_MSDU     pMpTxd
    )
{
   UNREFERENCED_PARAMETER(pStation);
    UNREFERENCED_PARAMETER(pMpTxd);
}

VOID 
Sta11ProcessSendComplete(
    _In_  PSTATION        pStation,
    _In_  PMP_TX_MSDU     pMpTxd,
    _In_  NDIS_STATUS     ndisStatus
    )
{
    UNREFERENCED_PARAMETER(pStation);
    UNREFERENCED_PARAMETER(pMpTxd);
    UNREFERENCED_PARAMETER(ndisStatus);
}
