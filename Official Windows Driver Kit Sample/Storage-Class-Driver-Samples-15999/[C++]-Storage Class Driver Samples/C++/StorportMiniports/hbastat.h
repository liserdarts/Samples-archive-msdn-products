/*++

Copyright (C) Microsoft Corporation, 2009

Module Name:

    hbastat.h

Abstract:

    

Notes:

Revision History:

        Nathan Obr (natobr),  February 2005
        Michael Xing (xiaoxing),  December 2009
--*/

#pragma once

BOOLEAN
AhciAdapterReset( 
    _In_ PAHCI_ADAPTER_EXTENSION AdapterExtension
    );

VOID
AhciCOMRESET(
    PAHCI_CHANNEL_EXTENSION ChannelExtension,
    PAHCI_PORT Px
    );

BOOLEAN
P_NotRunning(
    PAHCI_CHANNEL_EXTENSION ChannelExtension,
    PAHCI_PORT Px
    );

VOID
AhciAdapterRunAllPorts(
    _In_ PAHCI_ADAPTER_EXTENSION AdapterExtension
    );

VOID
RunNextPort(
    _In_ PAHCI_CHANNEL_EXTENSION ChannelExtension,
    _In_ BOOLEAN AtDIRQL
    );

VOID
P_Running_StartAttempt(
    _In_ PAHCI_CHANNEL_EXTENSION ChannelExtension,
    _In_ BOOLEAN AtDIRQL
    );

BOOLEAN
P_Running(
    _In_ PAHCI_CHANNEL_EXTENSION ChannelExtension
    );

HW_TIMER_EX P_Running_Callback;

VOID
P_Running_WaitOnDET(
    PAHCI_CHANNEL_EXTENSION ChannelExtension
    );

VOID
P_Running_WaitWhileDET1(
    PAHCI_CHANNEL_EXTENSION ChannelExtension
    );

VOID
P_Running_WaitOnDET3(
    PAHCI_CHANNEL_EXTENSION ChannelExtension
    );

VOID
P_Running_WaitOnFRE(
    PAHCI_CHANNEL_EXTENSION ChannelExtension
    );

VOID
P_Running_WaitOnBSYDRQ(
    PAHCI_CHANNEL_EXTENSION ChannelExtension
    );

VOID
P_Running_StartFailed(
    PAHCI_CHANNEL_EXTENSION ChannelExtension
    );

BOOLEAN
AhciPortReset (
    _In_ PAHCI_CHANNEL_EXTENSION ChannelExtension,
    _In_ BOOLEAN CompleteAllRequests
    );

VOID
AhciPortErrorRecovery(
    _In_ PAHCI_CHANNEL_EXTENSION ChannelExtension
    );

