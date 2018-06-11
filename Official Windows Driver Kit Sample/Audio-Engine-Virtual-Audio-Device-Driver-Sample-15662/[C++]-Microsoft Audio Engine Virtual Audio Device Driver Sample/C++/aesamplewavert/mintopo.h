
/*++

Copyright (c) 1997-2000  Microsoft Corporation All Rights Reserved

Module Name:

    minitopo.h

Abstract:

    Declaration of topology miniport.

--*/

#ifndef _MSVAD_MINTOPO_H_
#define _MSVAD_MINTOPO_H_

#include "basetopo.h"

//=============================================================================
// Classes
//=============================================================================

///////////////////////////////////////////////////////////////////////////////
// CMiniportTopology 
//   

class CMiniportTopology : 
    public CMiniportTopologyMSVAD,
    public IMiniportTopology,
    public CUnknown
{
  private:
    PCFILTER_DESCRIPTOR m_FilterDesc;
    eDeviceType m_DeviceType;

public:
    DECLARE_STD_UNKNOWN();
    CMiniportTopology(eDeviceType deviceType, PCFILTER_DESCRIPTOR *_Desc):CUnknown(0), m_DeviceType(deviceType) 
    {
        if (_Desc)
        {
            RtlCopyMemory(&m_FilterDesc, _Desc, sizeof(m_FilterDesc));
        }
    }

    ~CMiniportTopology();

    IMP_IMiniportTopology;

    NTSTATUS                    PropertyHandlerJackDescription
    (
        IN PPCPROPERTY_REQUEST  PropertyRequest
    );

    NTSTATUS                    PropertyHandlerJackDescriptionMicIn
    (
        IN PPCPROPERTY_REQUEST  PropertyRequest
    );
};
typedef CMiniportTopology *PCMiniportTopology;

extern NTSTATUS PropertyHandler_TopoFilter(IN PPCPROPERTY_REQUEST PropertyRequest);
extern NTSTATUS PropertyHandler_MicInTopoFilter(IN PPCPROPERTY_REQUEST PropertyRequest);

#endif

