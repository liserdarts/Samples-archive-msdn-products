#ifndef __INC_RECEIVE_H
#define __INC_RECEIVE_H

#define MAX_RECEIVE_BUFFER_SIZE 2500

NDIS_STATUS
HwUsbRecvStop(
    PNIC Nic
    );
       
NDIS_STATUS
HwUsbRecvStart(
    PNIC Nic
    );

NDIS_STATUS
HwUsbAllocateRecvResources(
    _In_ PNIC Nic
    );

NDIS_STATUS
HwUsbFreeRecvResources(
    _In_ PNIC Nic
    );

VOID
HwUsbFreeRecvFragment(
                _In_  PNIC Nic,
                _In_  PNIC_RX_FRAGMENT NicFragment
                ) ;

VOID
HwUsbProcessReceivedPacket(
    PNIC    Nic,
    PNIC_RX_FRAGMENT     Rfd
    );


#endif // #ifndef __INC_RECEIVE_H
