/*++

Copyright (c) 1997-2000  Microsoft Corporation All Rights Reserved

Module Name:

    wavtable.h

Abstract:

    Declaration of wave miniport tables.

--*/

#ifndef _MSVAD_WAVTABLE_H_
#define _MSVAD_WAVTABLE_H_


// To keep the code simple assume device supports only 48KHz, 16-bit, stereo (PCM and NON-PCM)

#define DEVICE_MAX_CHANNELS                 2       // Max Channels.
#define DEVICE_MIN_BITS_PER_SAMPLE          8       // Min Bits Per Sample
#define DEVICE_MAX_BITS_PER_SAMPLE          16      // Max Bits Per Sample
#define DEVICE_MIN_SAMPLE_RATE              16      // Max Bits Per Sample
#define DEVICE_MAX_SAMPLE_RATE              16      // Max Bits Per Sample

#define HOST_MAX_CHANNELS                   2       // Max Channels.
#define HOST_MIN_BITS_PER_SAMPLE            16      // Min Bits Per Sample
#define HOST_MAX_BITS_PER_SAMPLE            16      // Max Bits Per Sample
#define HOST_MIN_SAMPLE_RATE                44100   // Min Sample Rate
#define HOST_MAX_SAMPLE_RATE                96000   // Max Sample Rate

#define OFFLOAD_MAX_CHANNELS                2       // Max Channels.
#define OFFLOAD_MIN_BITS_PER_SAMPLE         16      // Min Bits Per Sample
#define OFFLOAD_MAX_BITS_PER_SAMPLE         16      // Max Bits Per Sample
#define OFFLOAD_MIN_SAMPLE_RATE             44100   // Min Sample Rate
#define OFFLOAD_MAX_SAMPLE_RATE             48000   // Max Sample Rate

#define LOOPBACK_MAX_CHANNELS               2       // Max Channels.
#define LOOPBACK_MIN_BITS_PER_SAMPLE        16      // Min Bits Per Sample
#define LOOPBACK_MAX_BITS_PER_SAMPLE        16      // Max Bits Per Sample
#define LOOPBACK_MIN_SAMPLE_RATE            44100   // Min Sample Rate
#define LOOPBACK_MAX_SAMPLE_RATE            48000   // Max Sample Rate

#define DOLBY_DIGITAL_MAX_CHANNELS          2       // Max Channels.
#define DOLBY_DIGITAL_MIN_BITS_PER_SAMPLE   16      // Min Bits Per Sample
#define DOLBY_DIGITAL_MAX_BITS_PER_SAMPLE   16      // Max Bits Per Sample
#define DOLBY_DIGITAL_MIN_SAMPLE_RATE       44100   // Min Sample Rate
#define DOLBY_DIGITAL_MAX_SAMPLE_RATE       44100   // Max Sample Rate

//=============================================================================
static 
KSDATAFORMAT_WAVEFORMATEXTENSIBLE AudioEngineSupportedDeviceFormats[] =
{
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                44100,
                176400,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                48000,
                192000,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    }
};

static 
KSDATAFORMAT_WAVEFORMATEXTENSIBLE HostPinSupportedDeviceFormats[] =
{
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                44100,
                176400,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                48000,
                192000,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                88200,
                352800,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                96000,
                384000,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    }
};

static 
KSDATAFORMAT_WAVEFORMATEXTENSIBLE OffloadPinSupportedDeviceFormats[] =
{
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                44100,
                176400,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                48000,
                192000,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_IEC61937_DOLBY_DIGITAL),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                48000,
                192000,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_IEC61937_DOLBY_DIGITAL)
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),           // MajorFormat
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEGLAYER3),   // SubFormat
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)// Specifier
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                44100,
                24000,
                1,
                0,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            1152, // wSamplesPerBlock
            0, // dwChannelMask
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEGLAYER3) // SubFormat
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),           // MajorFormat
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEG_HEAAC),   // SubFormat
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)// Specifier
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                44100,
                0,
                1,
                0,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            0, // wSamplesPerBlock
            0, // dwChannelMask
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEG_HEAAC) // SubFormat
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),           // MajorFormat
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEG_HEAAC),   // SubFormat
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)// Specifier
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                44100,
                0,
                1,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            0, // wSamplesPerBlock
            0, // dwChannelMask
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEG_HEAAC) // SubFormat
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),           // MajorFormat
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEG_HEAAC),   // SubFormat
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)// Specifier
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                48000,
                0,
                1,
                0,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            0, // wSamplesPerBlock
            0, // dwChannelMask
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEG_HEAAC) // SubFormat
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),           // MajorFormat
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEG_HEAAC),   // SubFormat
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)// Specifier
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                48000,
                0,
                1,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            0, // wSamplesPerBlock
            0, // dwChannelMask
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEG_HEAAC) // SubFormat
        }
    }

};

static 
KSDATAFORMAT_WAVEFORMATEXTENSIBLE LoopbackPinSupportedDeviceFormats[] =
{
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                44100,
                176400,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    },
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                48000,
                192000,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    }
};

#ifdef _SUPPORT_INDEPENDENT_MICIN
static 
KSDATAFORMAT_WAVEFORMATEXTENSIBLE MicInPinSupportedDeviceFormats[] =
{
    {
        {
            sizeof(KSDATAFORMAT_WAVEFORMATEXTENSIBLE),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        {
            {
                WAVE_FORMAT_EXTENSIBLE,
                2,
                48000,
                192000,
                4,
                16,
                sizeof(WAVEFORMATEXTENSIBLE) - sizeof(WAVEFORMATEX)
            },
            16,
            KSAUDIO_SPEAKER_STEREO,
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM)
        }
    }
};
#endif
static
KSDATARANGE_AUDIO PinDataRangesStream[] =
{
    {
        {
            sizeof(KSDATARANGE_AUDIO),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        HOST_MAX_CHANNELS,           
        HOST_MIN_BITS_PER_SAMPLE,    
        HOST_MAX_BITS_PER_SAMPLE,    
        HOST_MIN_SAMPLE_RATE,            
        HOST_MAX_SAMPLE_RATE             
    },
    {
        {
            sizeof(KSDATARANGE_AUDIO),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        OFFLOAD_MAX_CHANNELS,           
        OFFLOAD_MIN_BITS_PER_SAMPLE,    
        OFFLOAD_MAX_BITS_PER_SAMPLE,    
        OFFLOAD_MIN_SAMPLE_RATE,
        OFFLOAD_MAX_SAMPLE_RATE
    },
    {
        {
            sizeof(KSDATARANGE_AUDIO),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_PCM),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        LOOPBACK_MAX_CHANNELS,           
        LOOPBACK_MIN_BITS_PER_SAMPLE,    
        LOOPBACK_MAX_BITS_PER_SAMPLE,    
        LOOPBACK_MIN_SAMPLE_RATE,
        LOOPBACK_MAX_SAMPLE_RATE
    },
    {
        {
            sizeof(KSDATARANGE_AUDIO),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_IEC61937_DOLBY_DIGITAL),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        DOLBY_DIGITAL_MAX_CHANNELS,           
        DOLBY_DIGITAL_MIN_BITS_PER_SAMPLE,    
        DOLBY_DIGITAL_MAX_BITS_PER_SAMPLE,    
        DOLBY_DIGITAL_MIN_SAMPLE_RATE,
        DOLBY_DIGITAL_MAX_SAMPLE_RATE
    },
    {
        {
            sizeof(KSDATARANGE_AUDIO),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEGLAYER3),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        2,           
        0,    
        32,    
        44100,
        48000
    },
    {
        {
            sizeof(KSDATARANGE_AUDIO),
            0,
            0,
            0,
            STATICGUIDOF(KSDATAFORMAT_TYPE_AUDIO),
            STATICGUIDOF(KSDATAFORMAT_SUBTYPE_MPEG_HEAAC),
            STATICGUIDOF(KSDATAFORMAT_SPECIFIER_WAVEFORMATEX)
        },
        2,           
        0,    
        32,    
        8000,
        96000
    }
};

static
PKSDATARANGE PinDataRangePointersStream[] =
{
    PKSDATARANGE(&PinDataRangesStream[0])
};
static
PKSDATARANGE PinDataRangePointersOffloadStream[] =
{
    PKSDATARANGE(&PinDataRangesStream[1]),
    PKSDATARANGE(&PinDataRangesStream[3]),
    PKSDATARANGE(&PinDataRangesStream[4]),
    PKSDATARANGE(&PinDataRangesStream[5])
};
static
PKSDATARANGE PinDataRangePointersLoopbackStream[] =
{
    PKSDATARANGE(&PinDataRangesStream[2])
};
//=============================================================================
static
KSDATARANGE RenderPinDataRangesBridge[] =
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

static
PKSDATARANGE RenderPinDataRangePointersBridge[] =
{
    &RenderPinDataRangesBridge[0]
};

//=============================================================================

static
PCPROPERTY_ITEM PropertiesOffloadPin[] =
{
    {
        &KSPROPSETID_OffloadPin,  // define new property set
        KSPROPERTY_OFFLOAD_PIN_GET_STREAM_OBJECT_POINTER, // define properties
        KSPROPERTY_TYPE_GET | KSPROPERTY_TYPE_BASICSUPPORT,
        PropertyHandler_OffloadPin
    },
    {
        &KSPROPSETID_OffloadPin,  // define new property set
        KSPROPERTY_OFFLOAD_PIN_VERIFY_STREAM_OBJECT_POINTER, // define properties
        KSPROPERTY_TYPE_SET | KSPROPERTY_TYPE_BASICSUPPORT,
        PropertyHandler_OffloadPin
    }
};

DEFINE_PCAUTOMATION_TABLE_PROP(AutomationOffloadPin, PropertiesOffloadPin);

static
PCPIN_DESCRIPTOR RenderWaveMiniportPins[] =
{
    // Wave Out Streaming Pin (Renderer) KSPIN_WAVE_RENDER_SINK_SYSTEM
    {
        MAX_INPUT_SYSTEM_STREAMS,
        MAX_INPUT_SYSTEM_STREAMS, 
        0,
        NULL,
        {
            0,
            NULL,
            0,
            NULL,
            SIZEOF_ARRAY(PinDataRangePointersStream),
            PinDataRangePointersStream,
            KSPIN_DATAFLOW_IN,
            KSPIN_COMMUNICATION_SINK,
            &KSCATEGORY_AUDIO,
            NULL,
            0
        }
    },
    // Wave Out Streaming Pin (Renderer) KSPIN_WAVE_RENDER_SINK_OFFLOAD
    {
        MAX_INPUT_OFFLOAD_STREAMS,
        MAX_INPUT_OFFLOAD_STREAMS, 
        0,
        &AutomationOffloadPin,     // AutomationTable
        {
            0,
            NULL,
            0,
            NULL,
            SIZEOF_ARRAY(PinDataRangePointersOffloadStream),
            PinDataRangePointersOffloadStream,
            KSPIN_DATAFLOW_IN,
            KSPIN_COMMUNICATION_SINK,
            &KSCATEGORY_AUDIO,
            NULL,
            0
        }
    },
    // Wave Out Streaming Pin (Renderer) KSPIN_WAVE_RENDER_SINK_LOOPBACK
    {
        MAX_OUTPUT_LOOPBACK_STREAMS,
        MAX_OUTPUT_LOOPBACK_STREAMS, 
        0,
        NULL,
        {
            0,
            NULL,
            0,
            NULL,
            SIZEOF_ARRAY(PinDataRangePointersStream),
            PinDataRangePointersStream,
            KSPIN_DATAFLOW_OUT,              
            KSPIN_COMMUNICATION_SINK,
            &KSCATEGORY_AUDIO,
            NULL,
            0
        }
    },
    // Wave Out Bridge Pin (Renderer) KSPIN_WAVE_RENDER_SOURCE
    {
        0,
        0,
        0,
        NULL,
        {
            0,
            NULL,
            0,
            NULL,
            SIZEOF_ARRAY(RenderPinDataRangePointersBridge),
            RenderPinDataRangePointersBridge,
            KSPIN_DATAFLOW_OUT,
            KSPIN_COMMUNICATION_NONE,
            &KSCATEGORY_AUDIO,
            NULL,
            0
        }
    },
};

//=============================================================================
static
PCNODE_DESCRIPTOR RenderWaveMiniportNodes[] =
{
    // KSNODE_WAVE_AUDIO_ENGINE
    {
        0,                          // Flags
        NULL,                       // AutomationTable
        &KSNODETYPE_AUDIO_ENGINE,   // Type  KSNODETYPE_AUDIO_ENGINE
        NULL                        // Name
    }
};
//=============================================================================
//
//                   ----------------------------      
//                   |                          |      
//  System Pin   0-->|                          |      
//                   |                          |      
//  Offload Pin  1-->|    HW Audio Engine node  |--> KSPIN_WAVE_RENDER_SOURCE (3)
//                   |                          |      
//  Loopback Pin 2<--|                          |      
//                   |                          |      
//                   ---------------------------       
static
PCCONNECTION_DESCRIPTOR RenderWaveMiniportConnections[] =
{
    { PCFILTER_NODE,            KSPIN_WAVE_RENDER_SINK_SYSTEM,     KSNODE_WAVE_AUDIO_ENGINE,   1 },
    { PCFILTER_NODE,            KSPIN_WAVE_RENDER_SINK_OFFLOAD,    KSNODE_WAVE_AUDIO_ENGINE,   2 },
    { KSNODE_WAVE_AUDIO_ENGINE, 3,                                 PCFILTER_NODE,              KSPIN_WAVE_RENDER_SINK_LOOPBACK },
    { KSNODE_WAVE_AUDIO_ENGINE, 0,                                 PCFILTER_NODE,              KSPIN_WAVE_RENDER_SOURCE },
};

//=============================================================================
static
PCPROPERTY_ITEM PropertiesRenderWaveFilter[] =
{
    {
        &KSPROPSETID_Pin,
        KSPROPERTY_PIN_PROPOSEDATAFORMAT,
        KSPROPERTY_TYPE_SET | KSPROPERTY_TYPE_BASICSUPPORT,
        PropertyHandler_WaveFilter
    },
};

DEFINE_PCAUTOMATION_TABLE_PROP(AutomationRenderWaveFilter, PropertiesRenderWaveFilter);

//=============================================================================
static
PCFILTER_DESCRIPTOR RenderWaveMiniportFilterDescriptor =
{
    0,                                  // Version
    &AutomationRenderWaveFilter,              // AutomationTable
    sizeof(PCPIN_DESCRIPTOR),           // PinSize
    SIZEOF_ARRAY(RenderWaveMiniportPins),         // PinCount
    RenderWaveMiniportPins,                       // Pins
    sizeof(PCNODE_DESCRIPTOR),          // NodeSize
    SIZEOF_ARRAY(RenderWaveMiniportNodes),        // NodeCount
    RenderWaveMiniportNodes,                      // Nodes
    SIZEOF_ARRAY(RenderWaveMiniportConnections),  // ConnectionCount
    RenderWaveMiniportConnections,                // Connections
    0,                                  // CategoryCount
    NULL                                // Categories  - use defaults (audio, render, capture)
};

#endif
