#pragma once 

BOOLEAN
SetRFPowerState(
    PNIC                Nic, 
    RT_RF_POWER_STATE   eRFPowerState
    );


// Phy functions
void SwChnlPhy(
    PNIC    pNic,
    UCHAR   channel
    );

VOID
HwInternalStartReqComplete(
    _In_  PNIC        pNic
    );

ULONG
HwRSSI(
    PNIC        pNic,
    RX_STATUS_DESC_8187     RxDesc
    );

void
HwRestoreToBeforeScan(
    _In_ PNIC pNic
    );    

void
HwPeriodTimerHandler(
	_In_ PNIC pNic
	);	

void
HwPostSendHandler(
    _In_ PNIC     pNic,
    _In_ ULONG    sentBufferLength
    );

