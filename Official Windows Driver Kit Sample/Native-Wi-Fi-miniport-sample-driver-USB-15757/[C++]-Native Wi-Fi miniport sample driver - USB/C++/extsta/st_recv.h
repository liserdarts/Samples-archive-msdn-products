/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Sta_Recv.h

Abstract:
    STA layer receive processing functions
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#ifndef _STATION_RECEIVE_H_
#define _STATION_RECEIVE_H_

#include <packon.h>
typedef struct {
    UCHAR sh_dsap;
    UCHAR sh_ssap;
    UCHAR sh_ctl;
    UCHAR sh_protid[3];
    unsigned short  sh_etype;
} IEEE_8022_LLC_SNAP, *PIEEE_8022_LLC_SNAP;
#include <packoff.h>

/**
 * 
 * 
 * \param pStation
 * \param pMpFragment
 * \param pNicFragment
 * \param pFragmentHdr
 * \param FragmentSize
 * \return 
 * \sa 
 */
NDIS_STATUS
StaReceiveMgmtPacket(
    _In_  PSTATION                        pStation,
    _In_  PMP_RX_FRAGMENT                 pMpFragment,
    _In_  PNIC_RX_FRAGMENT                pNicFragment,
    _In_  PDOT11_MAC_HEADER               pFragmentHdr,
    _In_  USHORT                          FragmentSize
    );

/**
 * 
 * 
 * \param pStation
 * \param pNicFragment
 * \param TotalLength
 * \return 
 * \sa 
 */
VOID 
StaReceiveBeacon(
    _In_  PSTATION                        pStation,
    _In_  PNIC_RX_FRAGMENT                pNicFragment,
    _In_  ULONG                           TotalLength
    );

/**
 * 
 * 
 * \param pStation
 * \param pNicFragment
 * \param TotalLength
 * \sa 
 */
VOID 
StaReceiveAuthentication(
    _In_  PSTATION                        pStation,
    _In_  PNIC_RX_FRAGMENT                pNicFragment,
    _In_  ULONG                           TotalLength
    );

/**
 * 
 * 
 * \param pStation
 * \param pNicFragment
 * \param TotalLength
 * \sa 
 */
VOID 
StaReceiveDeauthentication(
    _In_  PSTATION                        pStation,
    _In_  PNIC_RX_FRAGMENT                pNicFragment,
    _In_  ULONG                           TotalLength
    );

/**
 * 
 * 
 * \param pStation
 * \param pNicFragment
 * \param TotalLength
 * \return 
 * \sa 
 */
VOID 
StaReceiveAssociationResponse(
    _In_  PSTATION                        pStation,
    _In_  PNIC_RX_FRAGMENT                pNicFragment,
    _In_  ULONG                           TotalLength
    );


/**
 * 
 * 
 * \param pStation
 * \param pNicFragment
 * \param TotalLength
 * \return 
 * \sa 
 */
VOID 
StaReceiveDisassociation(
    _In_  PSTATION                        pStation,
    _In_  PNIC_RX_FRAGMENT                pNicFragment,
    _In_  ULONG                           TotalLength
    );

#endif // _STATION_RECEIVE_H_
