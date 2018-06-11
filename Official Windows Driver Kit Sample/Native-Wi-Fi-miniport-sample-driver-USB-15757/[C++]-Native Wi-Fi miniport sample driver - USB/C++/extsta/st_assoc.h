/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Sta_assoc.h

Abstract:
    STA layer association frame processing functions
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/
#ifndef _STATION_ASSOCIATION_H_
#define _STATION_ASSOCIATION_H_

/**
 * Allocates and populate the association request packet. 
 * 
 * \param pStation              STATION structure
 * \param pAPEntry              The APEntry this association request packet is for
 * \param ppAssocPacket         Returns pointer to association request packet
 * \param pAssocPacketLength    Association request packet length
 * \return NDIS_STATUS
 * \sa StaFreeAssociateRequestPacket
 */
NDIS_STATUS
StaCreateAssociateRequestPacket(
    _In_  PSTATION        pStation,
    _In_  PSTA_BSS_ENTRY  pAPEntry,
    _Outptr_result_maybenull_ PUCHAR*         ppAssocPacket,
    _Out_ PUSHORT         pAssocPacketLength
    );

/**
 * Frees a previously allocate association request packet
 * 
 * \param pStation              STATION structure
 * \param pAPEntry              The APEntry this association request packet was for
 * \param pAssocPacket          Pointer to association request packet
 * \param AssocPacketLength     Association request packet's data length. The memory
 *                              allocated may be larger than this length
 * \sa StaCreateAssociateRequestPacket
 */
VOID
StaFreeAssociateRequestPacket(
    _In_  PSTATION        pStation,
    _In_  PSTA_BSS_ENTRY  pAPEntry,
    _Out_ PUCHAR          pAssocPacket,
    _Out_ USHORT          AssocPacketLength
    );


/**
 * Starts exchanging association packets with the access point. 
 * 
 * \param pStation          STATION structure
 * \param pAPEntry          APEntry to associate with
 * \param AssociateTimeout  Max time to wait for association completion, expressed 
 *                          in 100 nanoseconds
 * \return NDIS_STATUS  If the function fails, it must complete association process
 *                      before returning.
 * \sa StaAssociationComplete
 */
NDIS_STATUS
StaAssociate(
    _In_  PSTATION        pStation,
    _In_  PSTA_BSS_ENTRY  pAPEntry,
    _In_  ULONG           AssociateTimeout
    );

/**
 * Called to complete an association attempt. This is generally called on receiving
 * association response or on timeout
 * 
 * \param pStation          STATION structure
 * \param Status            Status code for the association attempt. If the
 *                          status is zero, the association is considered a 
 *                          success
 * \param ErrorSource       Source of error in case Status was non successful
 * \sa 
 */
VOID
StaAssociateComplete(
    _In_  PSTATION        pStation,
    _In_  ULONG           Status
    );

/**
 * Timeout call back in case association has timed out. This can also be called
 * from the reset routine to expedite association process completion. This means
 * it may be called at IRQL_PASSIVE or IRQL_DISPATCH
 *
 * \param param             STATION structure
 */
EVT_WDF_TIMER StaAssociateTimeoutCallback;

/**
 * Sends a disassociation packet to an access point. The caller is responsible
 * for determining if the packet must be sent or not, changing states and
 * indicating status
 * 
 * \param pStation          STATION structure
 * \param pAPEntry          APEntry to disassociate with
 * \param AssociateTimeout  Reason to use in the disassociation request
 * \return NDIS_STATUS  NDIS_STATUS from the send attempt
 */
NDIS_STATUS
StaSendDisassociation(
    _In_  PSTATION        pStation,
    _In_  PSTA_BSS_ENTRY  pAPEntry,
    _In_  USHORT          ReasonCode
    );



#endif // _STATION_ASSOCIATION_H_
