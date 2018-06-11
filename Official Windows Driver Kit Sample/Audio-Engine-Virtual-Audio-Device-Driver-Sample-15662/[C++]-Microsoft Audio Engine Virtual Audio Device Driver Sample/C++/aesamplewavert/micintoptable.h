/*++

Copyright (c) 1997-2011  Microsoft Corporation All Rights Reserved

Module Name:

    micintoptable.h

Abstract:

    Declaration of topology table for mic in device.

--*/

#ifndef _MSVAD_MICIN_TOPTABLE_H_
#define _MSVAD_MICIN_TOPTABLE_H_

#ifdef _SUPPORT_INDEPENDENT_MICIN


//=============================================================================
static
KSDATARANGE MicInTopoPinDataRangesBridge[] =
{
 {
   sizeof(KSDATARANGE),
   0,
   0,
   0,
   STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
   STATICGUIDOF(KSDATAFORMAT_SUBTYPE_ANALOG),
   STATICGUIDOF(KSDATAFORMAT_SPECIFIER_NONE)
 }
};

//=============================================================================
static
PKSDATARANGE MicInTopoPinDataRangePointersBridge[] =
{
  &MicInTopoPinDataRangesBridge[0]
};

//=============================================================================
static
PCPIN_DESCRIPTOR MicInTopoMiniportPins[] =
{
  // KSPIN_TOPO_MIC_ELEMENTS
  {
    0,
    0,
    0,                                              // InstanceCount
    NULL,                                           // AutomationTable
    {                                               // KsPinDescriptor
      0,                                            // InterfacesCount
      NULL,                                         // Interfaces
      0,                                            // MediumsCount
      NULL,                                         // Mediums
      SIZEOF_ARRAY(MicInTopoPinDataRangePointersBridge),     // DataRangesCount
      MicInTopoPinDataRangePointersBridge,                   // DataRanges
      KSPIN_DATAFLOW_IN,                            // DataFlow
      KSPIN_COMMUNICATION_NONE,                     // Communication
      &KSNODETYPE_MICROPHONE,                       // Category
      NULL,                                         // Name
      0                                             // Reserved
    }
  },

  // KSPIN_TOPO_BRIDGE
  {
    0,
    0,
    0,                                              // InstanceCount
    NULL,                                           // AutomationTable
    {                                               // KsPinDescriptor
      0,                                            // InterfacesCount
      NULL,                                         // Interfaces
      0,                                            // MediumsCount
      NULL,                                         // Mediums
      SIZEOF_ARRAY(MicInTopoPinDataRangePointersBridge),     // DataRangesCount
      MicInTopoPinDataRangePointersBridge,                   // DataRanges
      KSPIN_DATAFLOW_OUT,                           // DataFlow
      KSPIN_COMMUNICATION_NONE,                     // Communication
      &KSCATEGORY_AUDIO,                            // Category
      NULL,                                         // Name
      0                                             // Reserved
    }
  }
};

//=============================================================================
static
KSJACK_DESCRIPTION JackDescMics =
{
    KSAUDIO_SPEAKER_STEREO,
    0xB3C98C,               // Color spec for green
    eConnType3Point5mm,
    eGeoLocRear,
    eGenLocPrimaryBox,
    ePortConnJack,
    TRUE
};

// Only return a KSJACK_DESCRIPTION for the physical bridge pin.
static 
PKSJACK_DESCRIPTION MicJackDescriptions[] =
{
    &JackDescMics,
    NULL
};

//=============================================================================
static
PCPROPERTY_ITEM PropertiesVolume[] =
{
    {
    &KSPROPSETID_Audio,
    KSPROPERTY_AUDIO_VOLUMELEVEL,
    KSPROPERTY_TYPE_GET | KSPROPERTY_TYPE_SET | KSPROPERTY_TYPE_BASICSUPPORT,
    PropertyHandler_Topology
    },
    {
    &KSPROPSETID_Audio,
    KSPROPERTY_AUDIO_CPU_RESOURCES,
    KSPROPERTY_TYPE_GET | KSPROPERTY_TYPE_BASICSUPPORT,
    PropertyHandler_Topology
  }
};

DEFINE_PCAUTOMATION_TABLE_PROP(AutomationVolume, PropertiesVolume);

//=============================================================================
static
PCPROPERTY_ITEM PropertiesMute[] =
{
  {
    &KSPROPSETID_Audio,
    KSPROPERTY_AUDIO_MUTE,
    KSPROPERTY_TYPE_GET | KSPROPERTY_TYPE_SET | KSPROPERTY_TYPE_BASICSUPPORT,
    PropertyHandler_Topology
  },
  {
    &KSPROPSETID_Audio,
    KSPROPERTY_AUDIO_CPU_RESOURCES,
    KSPROPERTY_TYPE_GET | KSPROPERTY_TYPE_BASICSUPPORT,
    PropertyHandler_Topology
  }
};

DEFINE_PCAUTOMATION_TABLE_PROP(AutomationMute, PropertiesMute);

//=============================================================================
static
PCNODE_DESCRIPTOR MicInTopologyNodes[] =
{
  // KSNODE_TOPO_VOLUME
  {
    0,                      // Flags
    &AutomationVolume,      // AutomationTable
    &KSNODETYPE_VOLUME,     // Type
    &KSAUDFNAME_MIC_VOLUME  // Name
  },
  // KSNODE_TOPO_MUTE
  {
    0,                      // Flags
    &AutomationMute,        // AutomationTable
    &KSNODETYPE_MUTE,       // Type
    &KSAUDFNAME_MIC_MUTE    // Name
  },
};

C_ASSERT( KSNODE_TOPO_VOLUME  == 0 );
C_ASSERT( KSNODE_TOPO_MUTE    == 1 );

//=============================================================================
static
PCCONNECTION_DESCRIPTOR MicInMiniportConnections[] =
{
  //  FromNode,             FromPin,                    ToNode,                 ToPin
  {   PCFILTER_NODE,        KSPIN_TOPO_MIC_ELEMENTS,    KSNODE_TOPO_VOLUME,     1 },
  {   KSNODE_TOPO_VOLUME,   0,                          KSNODE_TOPO_MUTE,       1 },
  {   KSNODE_TOPO_MUTE,     0,                          PCFILTER_NODE,          KSPIN_TOPO_BRIDGE }
};


//=============================================================================
static
PCPROPERTY_ITEM PropertiesTopoFilter[] =
{
    {
        &KSPROPSETID_Jack,
        KSPROPERTY_JACK_DESCRIPTION,
        KSPROPERTY_TYPE_GET | KSPROPERTY_TYPE_BASICSUPPORT,
        PropertyHandler_MicInTopoFilter
    }
};

DEFINE_PCAUTOMATION_TABLE_PROP(AutomationMicInTopoFilter, PropertiesTopoFilter);

//=============================================================================
static
PCFILTER_DESCRIPTOR MicInTopoMiniportFilterDescriptor =
{
  0,                                  // Version
  &AutomationMicInTopoFilter,         // AutomationTable
  sizeof(PCPIN_DESCRIPTOR),           // PinSize
  SIZEOF_ARRAY(MicInTopoMiniportPins),// PinCount
  MicInTopoMiniportPins,              // Pins
  sizeof(PCNODE_DESCRIPTOR),          // NodeSize
  SIZEOF_ARRAY(MicInTopologyNodes),   // NodeCount
  MicInTopologyNodes,                 // Nodes
  SIZEOF_ARRAY(MicInMiniportConnections),  // ConnectionCount
  MicInMiniportConnections,           // Connections
  0,                                  // CategoryCount
  NULL                                // Categories
};

#endif
#endif
