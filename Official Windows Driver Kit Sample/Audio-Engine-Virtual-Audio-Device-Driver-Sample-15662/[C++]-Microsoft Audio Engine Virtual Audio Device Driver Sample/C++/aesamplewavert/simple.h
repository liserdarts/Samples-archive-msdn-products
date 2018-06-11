/*++

Copyright (c) 1997-2000  Microsoft Corporation All Rights Reserved

Module Name:

    simple.h

Abstract:

    Node and Pin numbers for simple sample.

--*/

#ifndef _MSVAD_SIMPLE_H_
#define _MSVAD_SIMPLE_H_

// Name Guid
// {946A7B1A-EBBC-422a-A81F-F07C8D40D3B4}
#define STATIC_NAME_MSVAD_SIMPLE\
    0x946a7b1a, 0xebbc, 0x422a, 0xa8, 0x1f, 0xf0, 0x7c, 0x8d, 0x40, 0xd3, 0xb4
DEFINE_GUIDSTRUCT("946A7B1A-EBBC-422a-A81F-F07C8D40D3B4", NAME_MSVAD_SIMPLE);
#define NAME_MSVAD_SIMPLE DEFINE_GUIDNAMED(NAME_MSVAD_SIMPLE)

// Pin properties.
#define MAX_INPUT_SYSTEM_STREAMS   1
#define MAX_INPUT_OFFLOAD_STREAMS  3
#define MAX_OUTPUT_LOOPBACK_STREAMS  1
#define MAX_TOTAL_STREAMS           MAX_INPUT_SYSTEM_STREAMS + MAX_INPUT_OFFLOAD_STREAMS + MAX_OUTPUT_LOOPBACK_STREAMS                 

// Wave pins
enum 
{
    KSPIN_WAVE_RENDER_SINK_SYSTEM = 0, 
    KSPIN_WAVE_RENDER_SINK_OFFLOAD, 
    KSPIN_WAVE_RENDER_SINK_LOOPBACK, 
    KSPIN_WAVE_RENDER_SOURCE
};

// Wave Topology nodes.
enum 
{
    KSNODE_WAVE_AUDIO_ENGINE = 0, 
    KSNODE_WAVE_DAC
};

// topology pins.
enum
{
    KSPIN_TOPO_WAVEOUT_SOURCE = 0,
    KSPIN_TOPO_LINEOUT_DEST,
};

// topology nodes.
enum
{
    KSNODE_TOPO_WAVEOUT_VOLUME = 0,
    KSNODE_TOPO_WAVEOUT_MUTE
};

#ifdef _SUPPORT_INDEPENDENT_MICIN
//----------------------------------------------------
// New defines for the mic in
//----------------------------------------------------
// Pin properties.
#define MAX_INPUT_STREAMS           1       // Number of capture streams.

// PCM Info
#define MIN_CHANNELS                2       // Min Channels.
#define MAX_CHANNELS_PCM            2       // Max Channels.
#define MIN_BITS_PER_SAMPLE_PCM     16      // Min Bits Per Sample
#define MAX_BITS_PER_SAMPLE_PCM     16      // Max Bits Per Sample
#define MIN_SAMPLE_RATE             8000    // Min Sample Rate
#define MAX_SAMPLE_RATE             48000   // Max Sample Rate
// Wave pins
enum 
{
    KSPIN_WAVE_BRIDGE = 0,
    KSPIN_WAVEIN_HOST
};

// Wave Topology nodes.
enum 
{
    KSNODE_WAVE_ADC = 0
};

// topology pins.
enum
{
    KSPIN_TOPO_MIC_ELEMENTS,
    KSPIN_TOPO_BRIDGE
};

// topology nodes.
enum
{
    KSNODE_TOPO_VOLUME,
    KSNODE_TOPO_MUTE
};

#endif

#endif

