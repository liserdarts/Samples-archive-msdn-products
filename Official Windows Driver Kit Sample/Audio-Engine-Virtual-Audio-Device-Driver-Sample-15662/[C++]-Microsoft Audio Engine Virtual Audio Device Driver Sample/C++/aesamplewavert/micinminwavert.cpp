/*++

Copyright (c) 1997-2000  Microsoft Corporation All Rights Reserved

Module Name:

    minwavert.cpp

Abstract:

    Implementation of wavert miniport.

--*/

#pragma warning (disable : 4127)

#include <msvad.h>
#include <common.h>
#include <limits.h>
#include "simple.h"
#include "minwavert.h"
#include "minwavertstream.h"
#include "IHVPrivatePropertySet.h"
#include "wavtable.h"

//=============================================================================
#ifdef _SUPPORT_INDEPENDENT_MICIN
#pragma code_seg("PAGE")
NTSTATUS
CMiniportWaveRT::PropertyHandlerProposedFormat_MicIn
(
    IN PPCPROPERTY_REQUEST      PropertyRequest
)
/*++

Routine Description:

  Handles KSPROPERTY_PIN_PROPOSEDDATAFORMAT

Arguments:

  PropertyRequest - 

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    DPF_ENTER(("[PropertyHandlerProposedFormat]"));

    NTSTATUS ntStatus = STATUS_INVALID_DEVICE_REQUEST;

    if (PropertyRequest->Verb & KSPROPERTY_TYPE_BASICSUPPORT)
    {
        ntStatus = 
            PropertyHandler_BasicSupport
            (
                PropertyRequest,
                KSPROPERTY_TYPE_BASICSUPPORT | KSPROPERTY_TYPE_SET,
                VT_ILLEGAL
            );
    }
    else
    {
        ULONG cbMinSize = sizeof(KSDATAFORMAT_WAVEFORMATEX);

        if (PropertyRequest->ValueSize == 0)
        {
            PropertyRequest->ValueSize = cbMinSize;
            ntStatus = STATUS_BUFFER_OVERFLOW;
        }
        else if (PropertyRequest->ValueSize < cbMinSize)
        {
            ntStatus = STATUS_BUFFER_TOO_SMALL;
        }
        else
        {
            if (PropertyRequest->Verb & KSPROPERTY_TYPE_SET)
            {
                KSDATAFORMAT_WAVEFORMATEX* pKsFormat = (KSDATAFORMAT_WAVEFORMATEX*)PropertyRequest->Value;

                ntStatus = STATUS_NO_MATCH;

                if ((pKsFormat->DataFormat.MajorFormat == KSDATAFORMAT_TYPE_AUDIO) &&
                    (pKsFormat->DataFormat.SubFormat == KSDATAFORMAT_SUBTYPE_PCM) &&
                    (pKsFormat->DataFormat.Specifier == KSDATAFORMAT_SPECIFIER_WAVEFORMATEX))
                {
                    WAVEFORMATEX* pWfx = (WAVEFORMATEX*)&pKsFormat->WaveFormatEx;

                    // make sure the WAVEFORMATEX part of the format makes sense
                    if ((pWfx->wBitsPerSample == 16) &&
                        (pWfx->nChannels == 2) &&
                        ((pWfx->nSamplesPerSec == 16000) || (pWfx->nSamplesPerSec == 48000)) &&
                        (pWfx->nBlockAlign == (pWfx->nChannels * 2)) &&
                        (pWfx->nAvgBytesPerSec == (pWfx->nSamplesPerSec * pWfx->nBlockAlign)))
                    {
                        ntStatus = STATUS_SUCCESS;
                    }
                }
            }
            else
            {
                ntStatus = STATUS_INVALID_PARAMETER;
            }
        }
    }

    return ntStatus;
} // PropertyHandlerProposedFormat
#endif
//=============================================================================
#ifdef _SUPPORT_INDEPENDENT_MICIN
#pragma code_seg("PAGE")
NTSTATUS
PropertyHandler_WaveFilter_MicIn
( 
    IN PPCPROPERTY_REQUEST      PropertyRequest 
)
/*++

Routine Description:

  Redirects general property request to miniport object

Arguments:

  PropertyRequest - 

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    NTSTATUS            ntStatus = STATUS_INVALID_DEVICE_REQUEST;
    CMiniportWaveRT* pWaveHelper = reinterpret_cast<CMiniportWaveRT*>(PropertyRequest->MajorTarget);

    if (pWaveHelper == NULL)
    {
        ntStatus = STATUS_INVALID_PARAMETER;
    }
    if (!NT_SUCCESS(ntStatus))
    {
        return ntStatus;
    }
    pWaveHelper->AddRef();

    switch (PropertyRequest->PropertyItem->Id)
    {
        case KSPROPERTY_PIN_PROPOSEDATAFORMAT:
            ntStatus = 
                pWaveHelper->PropertyHandlerProposedFormat_MicIn
                (
                    PropertyRequest
                );
            break;
        
        default:
            DPF(D_TERSE, ("[PropertyHandler_WaveFilter_MicIn: Invalid Device Request]"));
    }

    pWaveHelper->Release();

    return ntStatus;
} // PropertyHandler_WaveFilter_MicIn
#endif







  
