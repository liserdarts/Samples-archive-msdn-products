/*++

Copyright (c) Microsoft Corporation. All rights reserved.

Module Name:
    Hw_bus_interface.c

Abstract:
    
    
Revision History:
      When        What
    ----------    ----------------------------------------------
    08-01-2005    Created

Notes:

--*/

#include "hw_pcomp.h"

#pragma warning(disable:4200)  // nameless struct/union
#pragma warning(disable:4201)  // nameless struct/union
#pragma warning(disable:4214)  // bit field types other than int

#include "usb_main.h"
#include "bus_intf.h"
#include "8187_gen.h"

#include "usb_xmit.h"
#include "usb_recv.h"


#pragma warning(default:4200)
#pragma warning(default:4201)
#pragma warning(default:4214)


NDIS_STATUS
HwInitializeBus(
    _In_ PNIC Nic
    )
{
    NDIS_STATUS ndisStatus;

    ndisStatus =  HwUSBSpecificInit(Nic);
    //InitializeVariables here
    //tod do i need this here 
    //InitializeVariables(Nic);
    return ndisStatus ;
}

VOID
HwDeInitBus(
    _In_ PNIC Nic
    )
{
    HwUsbDeInit(Nic);
}

NDIS_STATUS
HwReadAdapterInfo(
    _In_ PNIC Nic
    )
{
    NDIS_STATUS  ndisStatus;
    
    ndisStatus = ReadNicInfo8187(Nic); 
    return ndisStatus;
}



NDIS_STATUS
HwInitializeAdapter(
    _In_ PNIC Nic
    )
{
    //
    //channel will come from reg stetings   . Initialize to channel 1
    //
    return InitializeNic8187(Nic, 1);
}

NDIS_STATUS
HwBusAllocateRecvResources(
    _In_ PNIC Nic
    )

{
    return HwUsbAllocateRecvResources(Nic);
}

NDIS_STATUS
HwBusAllocateXmitResources(
    _In_ PNIC Nic ,
    _In_  ULONG NumTxd
    )

{
    return HwUsbAllocateXmitResources(Nic, NumTxd);
}



VOID
HwBusFreeXmitResources(
    _In_ PNIC Nic
    )
{
    HwUsbFreeXmitMemory(Nic);
    return ;

}


NDIS_STATUS
HwBusFreeRecvResources(
    _In_ PNIC Nic
    )
{
    return HwUsbFreeRecvResources(Nic);
}

NDIS_STATUS
HwBusFindAdapterAndResources(
    _In_ PNIC Nic
    )
{
    UNREFERENCED_PARAMETER(Nic);
    
    return NDIS_STATUS_SUCCESS;
    //no op for usb
}

NDIS_STATUS
HwBusStartDevice(
                _In_ PNIC Nic
                 )
{
    NDIS_STATUS ndisStatus;
    
    ndisStatus = HwUsbRecvStart(Nic);
    if (ndisStatus != NDIS_STATUS_SUCCESS) {
        MpTrace(COMP_INIT_PNP, DBG_SERIOUS, ("Failed to start Recv IoTarget.\n"));
    }
    ndisStatus = HwUsbXmitStart(Nic);
    if (ndisStatus != NDIS_STATUS_SUCCESS) {
        MpTrace(COMP_INIT_PNP, DBG_SERIOUS, ("Failed to  start Transmit IoTarget.\n"));
    }
    return ndisStatus;
}

NDIS_STATUS
HwBusSendPacketAsync(
    _In_ PNIC Nic,
    _In_ UCHAR            QueueType,
    _In_ ULONG            BufferLength,
    _In_ PVOID            Buffer,
    _In_opt_ PNIC_TX_MSDU     NicTxd 
    )
{
    NTSTATUS ntStatus;
    NDIS_STATUS ndisStatus;

    ntStatus =  HwUsbSendPacketAsync(Nic,  QueueType, BufferLength, Buffer, NicTxd);    

    NT_STATUS_TO_NDIS_STATUS(ntStatus, &ndisStatus);
    return    ndisStatus;    
}

VOID
HwBusStopNotification(
    _In_ PNIC Nic
    )
{
    HwUsbStopAllPipes(Nic);
}


VOID
HwBusFreeRecvFragment(
                _In_  PNIC Nic,
                _In_  PNIC_RX_FRAGMENT NicFragment
                ) 
{
    HwUsbFreeRecvFragment(Nic,NicFragment );
}


