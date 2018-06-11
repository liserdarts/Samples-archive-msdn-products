/*++

Copyright (c) 1997-2000  Microsoft Corporation All Rights Reserved

Module Name:

    Msvad.h

Abstract:

    Header file for common stuff.

--*/

#ifndef _MSVAD_H_
#define _MSVAD_H_

#include <portcls.h>
#include <stdunk.h>
#include <ksdebug.h>
#include "kshelper.h"

//=============================================================================
// Defines
//=============================================================================

// Version number. Revision numbers are specified for each sample.
#define MSVAD_VERSION               1

// Revision number.
#define MSVAD_REVISION              0

// Product Id
// {5B722BF8-F0AB-47ee-B9C8-8D61D31375A1}
#define STATIC_PID_MSVAD\
    0x5b722bf8, 0xf0ab, 0x47ee, 0xb9, 0xc8, 0x8d, 0x61, 0xd3, 0x13, 0x75, 0xa1
DEFINE_GUIDSTRUCT("5B722BF8-F0AB-47ee-B9C8-8D61D31375A1", PID_MSVAD);
#define PID_MSVAD DEFINE_GUIDNAMED(PID_MSVAD)

// Pool tag used for MSVAD allocations
#define MSVAD_POOLTAG               'DVSM'  

// Debug module name
#define STR_MODULENAME              "MSVAD: "

// Debug utility macros
#define D_FUNC                      4
#define D_BLAB                      DEBUGLVL_BLAB
#define D_VERBOSE                   DEBUGLVL_VERBOSE        
#define D_TERSE                     DEBUGLVL_TERSE          
#define D_ERROR                     DEBUGLVL_ERROR          
#define DPF                         _DbgPrintF
#define DPF_ENTER(x)                DPF(D_FUNC, x)

// Channel orientation
#define CHAN_LEFT                   0
#define CHAN_RIGHT                  1
#define CHAN_MASTER                 (-1)

// Dma Settings.
#define DMA_BUFFER_SIZE             0x16000

#define KSPROPERTY_TYPE_ALL         KSPROPERTY_TYPE_BASICSUPPORT | \
                                    KSPROPERTY_TYPE_GET | \
                                    KSPROPERTY_TYPE_SET
                                    
// Specific node numbers for vadsampl.sys
#define DEV_SPECIFIC_VT_BOOL 9
#define DEV_SPECIFIC_VT_I4   10
#define DEV_SPECIFIC_VT_UI4  11

#define _100NS_PER_MILLISECOND      10000                       // number of 100ns units per millisecond
#define DEFAULT_PROCESSING_PERIOD  _100NS_PER_MILLISECOND * 10 // 10 millisecond processing period

//=============================================================================
// Typedefs
//=============================================================================

// Connection table for registering topology/wave bridge connection
typedef struct _PHYSICALCONNECTIONTABLE
{
    ULONG       ulTopologyIn;
    ULONG       ulTopologyOut;
    ULONG       ulWaveIn;
    ULONG       ulWaveOut;
} PHYSICALCONNECTIONTABLE, *PPHYSICALCONNECTIONTABLE;

//=============================================================================
// Externs
//=============================================================================

// Physical connection table. Defined in mintopo.cpp for each sample
extern PHYSICALCONNECTIONTABLE TopologyPhysicalConnections;

#ifdef _SUPPORT_INDEPENDENT_MICIN
extern PHYSICALCONNECTIONTABLE MicInTopologyPhysicalConnections;
#endif

// Generic topology handler
extern NTSTATUS PropertyHandler_Topology
( 
    IN  PPCPROPERTY_REQUEST     PropertyRequest 
);

// Generic wave port handler
extern NTSTATUS PropertyHandler_Wave
(
    IN  PPCPROPERTY_REQUEST     PropertyRequest
);

// Default WaveFilter automation table.
// Handles the GeneralComponentId request.
extern NTSTATUS PropertyHandler_WaveFilter
(
    IN PPCPROPERTY_REQUEST PropertyRequest
);

#ifdef _SUPPORT_INDEPENDENT_MICIN
extern NTSTATUS PropertyHandler_WaveFilter_MicIn
(
    IN PPCPROPERTY_REQUEST PropertyRequest
);
#endif

extern NTSTATUS PropertyHandler_AudioEngine
(
    IN PPCPROPERTY_REQUEST PropertyRequest
);
extern NTSTATUS PropertyHandler_OffloadPin
(
    IN PPCPROPERTY_REQUEST PropertyRequest
);

#endif
