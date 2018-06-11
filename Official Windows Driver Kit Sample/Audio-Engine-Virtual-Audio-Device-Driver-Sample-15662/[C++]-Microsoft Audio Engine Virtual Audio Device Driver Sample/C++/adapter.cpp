/*++

Copyright (c) 1997-2011  Microsoft Corporation All Rights Reserved

Module Name:

    adapter.cpp

Abstract:

    Setup and miniport installation.  No resources are used by msvad.
    This sample is to demonstrate how to develop an audio miniport driver supporting 
    hardware audio engine.
    
    An mic in device was added into this sample to mainly show how to expose independent filter set (wave+topology) for 
    each endpoint, which would be easier to maintain when comparing with using a single monolithic filter set for 
    all the audio endpoints. This sample allow the mic in endpoint to be exposed correctly in the control panel.
    But miniport driver would have to implement the actually data capture part to make it a fully 
    functioning captrue device.


The filter set for the sample render device

**********************************************************************
* Topology/Wave bridge connection for render                         *
*                                                                    *
*              +------+    +------+                                  *
*              | Wave |    | Topo |                                  *
*              |      |    |      |                                  *
* System   --->|0     |    |      |                                  *
* Offload  --->|1    3|--->|0    1|<--- Line Out                     *
* Loopback <---|2     |    |      |                                  *
*              |      |    |      |                                  *
*              +------+    +------+                                  *
**********************************************************************

The filter set for the sample mic-in device. The inclusion is optional
via _SUPPORT_INDEPENDENT_MICIN in the sources file

**********************************************************************
* Topology/Wave bridge connection for mic in                         *
*                                                                    *
*              +------+    +------+                                  *
*              | Topo |    | Wave |                                  *
*              |      |    |      |                                  *
*  Mic in  --->|0    1|===>|0    1|---> Capture Host Pin             *
*              |      |    |      |                                  *
*              +------+    +------+                                  *
**********************************************************************


--*/

#pragma warning (disable : 4127)

//
// All the GUIDS for all the miniports end up in this object.
//
#define PUT_GUIDS_HERE

#include <msvad.h>
#include "common.h"
#include "IHVPrivatePropertySet.h"

//-----------------------------------------------------------------------------
// Defines                                                                    
//-----------------------------------------------------------------------------

// Number of maximum miniport filters
#ifdef _SUPPORT_INDEPENDENT_MICIN
#define MAX_MINIPORTS 4     // two for the sample render endpoint 
                            // two for the sample capture endpoint 
#else
#define MAX_MINIPORTS 2     // two for the sample render endpoint 
#endif

#define CAPTURE_PORT TRUE
#define RENDER_PORT  FALSE

#include "simple.h"
#include "mintopo.h"
#include "minwavert.h"
#include "toptable.h"
#include "wavtable.h"
#include "micintoptable.h"
#include "micinwavtable.h"

NTSTATUS
CreateMiniportWaveRTMSVAD
( 
    OUT PUNKNOWN *,
    IN  REFCLSID,
    IN  PUNKNOWN,
    IN  POOL_TYPE,
    IN  eDeviceType,
    IN  PCFILTER_DESCRIPTOR *pDesc,
    IN  PUNKNOWN  UnknownAdapter
);

NTSTATUS
CreateMiniportTopologyMSVAD
( 
    OUT PUNKNOWN *,
    IN  REFCLSID,
    IN  PUNKNOWN,
    IN  POOL_TYPE,
    IN  eDeviceType,
    IN  PCFILTER_DESCRIPTOR *pDesc, 
    IN  PUNKNOWN UnknownAdapter
);


//-----------------------------------------------------------------------------
// Referenced forward.
//-----------------------------------------------------------------------------

DRIVER_ADD_DEVICE AddDevice;

NTSTATUS
StartDevice
( 
    IN  PDEVICE_OBJECT,      
    IN  PIRP,                
    IN  PRESOURCELIST        
); 

//-----------------------------------------------------------------------------
// Functions
//-----------------------------------------------------------------------------

//=============================================================================
#pragma code_seg("INIT")
extern "C" DRIVER_INITIALIZE DriverEntry;
extern "C" NTSTATUS
DriverEntry
( 
    IN  PDRIVER_OBJECT          DriverObject,
    IN  PUNICODE_STRING         RegistryPathName
)
{
/*++

Routine Description:

  Installable driver initialization entry point.
  This entry point is called directly by the I/O system.

  All audio adapter drivers can use this code without change.

Arguments:

  DriverObject - pointer to the driver object

  RegistryPath - pointer to a unicode string representing the path,
                   to driver-specific key in the registry.

Return Value:

  STATUS_SUCCESS if successful,
  STATUS_UNSUCCESSFUL otherwise.

--*/
    NTSTATUS                    ntStatus;

    DPF(D_TERSE, ("[DriverEntry]"));

    // Tell the class driver to initialize the driver.
    //
    ntStatus =  
        PcInitializeAdapterDriver
        ( 
            DriverObject,
            RegistryPathName,
            (PDRIVER_ADD_DEVICE)AddDevice
        );

    return ntStatus;
} // DriverEntry

#pragma code_seg()
// disable prefast warning 28152 because 
// DO_DEVICE_INITIALIZING is cleared in PcAddAdapterDevice
#pragma warning(disable:28152)
#pragma code_seg("PAGE")
//=============================================================================
NTSTATUS AddDevice
( 
    IN  PDRIVER_OBJECT          DriverObject,
    IN  PDEVICE_OBJECT          PhysicalDeviceObject 
)
/*++

Routine Description:

  The Plug & Play subsystem is handing us a brand new PDO, for which we
  (by means of INF registration) have been asked to provide a driver.

  We need to determine if we need to be in the driver stack for the device.
  Create a function device object to attach to the stack
  Initialize that device object
  Return status success.

  All audio adapter drivers can use this code without change.
  Set MAX_MINIPORTS depending on the number of miniports that the driver
  uses.

Arguments:

  DriverObject - pointer to a driver object

  PhysicalDeviceObject -  pointer to a device object created by the
                            underlying bus driver.

Return Value:

  NT status code.

--*/
{
    PAGED_CODE();

    NTSTATUS                    ntStatus;

    DPF(D_TERSE, ("[AddDevice]"));

    // Tell the class driver to add the device.
    //
    ntStatus = 
        PcAddAdapterDevice
        ( 
            DriverObject,
            PhysicalDeviceObject,
            PCPFNSTARTDEVICE(StartDevice),
            MAX_MINIPORTS,
            0
        );

    return ntStatus;
} // AddDevice

//=============================================================================
NTSTATUS
InstallSubdevice
( 
    _In_        PDEVICE_OBJECT          DeviceObject,
    _In_        PIRP                    Irp,
    _In_        PWSTR                   Name,
    _In_        REFGUID                 PortClassId,
    _In_        REFGUID                 MiniportClassId,
    _In_opt_    PFNCREATEMINIPORT       MiniportCreate,
    _In_        eDeviceType             deviceType,
    _In_        PCFILTER_DESCRIPTOR     *pDesc,
    _In_opt_    PUNKNOWN                UnknownAdapter,
    _In_opt_    PRESOURCELIST           ResourceList,
    _In_        REFGUID                 PortInterfaceId,
    _Out_opt_   PUNKNOWN *              OutPortInterface,
    _Out_opt_   PUNKNOWN *              OutPortUnknown
)
{
/*++

Routine Description:

    This function creates and registers a subdevice consisting of a port       
    driver, a minport driver and a set of resources bound together.  It will   
    also optionally place a pointer to an interface on the port driver in a    
    specified location before initializing the port driver.  This is done so   
    that a common ISR can have access to the port driver during 
    initialization, when the ISR might fire.                                   

Arguments:

    DeviceObject - pointer to the driver object

    Irp - pointer to the irp object.

    Name - name of the miniport. Passes to PcRegisterSubDevice
 
    PortClassId - port class id. Passed to PcNewPort.

    MiniportClassId - miniport class id. Passed to PcNewMiniport.

    MiniportCreate - pointer to a miniport creation function. If NULL, 
                     PcNewMiniport is used.

    UnknownAdapter - pointer to the adapter object. 
                     Used for initializing the port.

    ResourceList - pointer to the resource list.

    PortInterfaceId - GUID that represents the port interface.
       
    OutPortInterface - pointer to store the port interface

    OutPortUnknown - pointer to store the unknown port interface.

Return Value:

    NT status code.

--*/
    PAGED_CODE();

    ASSERT(DeviceObject);
    ASSERT(Irp);
    ASSERT(Name);

    NTSTATUS                    ntStatus;
    PPORT                       port = NULL;
    PUNKNOWN                    miniport = NULL;
     
    DPF_ENTER(("[InstallSubDevice %S]", Name));

    // Create the port driver object
    //
    ntStatus = PcNewPort(&port, PortClassId);

    // Create the miniport object
    //
    if (NT_SUCCESS(ntStatus))
    {
        if (MiniportCreate)
        {
            ntStatus = 
                MiniportCreate
                ( 
                    &miniport,
                    MiniportClassId,
                    NULL,
                    NonPagedPool,
                    deviceType,
                    pDesc,
                    UnknownAdapter
                );
        }
        else
        {
            ntStatus = 
                PcNewMiniport
                (
                    (PMINIPORT *) &miniport, 
                    MiniportClassId
                );
        }
    }

    // Init the port driver and miniport in one go.
    //
    if (NT_SUCCESS(ntStatus))
    {
#pragma warning(push)
        // IPort::Init's annotation on ResourceList requires it to be non-NULL.  However,
        // for dynamic devices, we may no longer have the resource list and this should
        // still succeed.
        //
#pragma warning(disable:6387)
        ntStatus = 
            port->Init
            ( 
                DeviceObject,
                Irp,
                miniport,
                UnknownAdapter,
                ResourceList 
            );
#pragma warning (pop)

        if (NT_SUCCESS(ntStatus))
        {
            // Register the subdevice (port/miniport combination).
            //
            ntStatus = 
                PcRegisterSubdevice
                ( 
                    DeviceObject,
                    Name,
                    port 
                );
        }

        // We don't need the miniport any more.  Either the port has it,
        // or we've failed, and it should be deleted.
        //
        miniport->Release();
    }

    // Deposit the port interfaces if it's needed.
    //
    if (NT_SUCCESS(ntStatus))
    {
        if (OutPortUnknown)
        {
            ntStatus = 
                port->QueryInterface
                ( 
                    IID_IUnknown,
                    (PVOID *)OutPortUnknown 
                );
        }

        if (OutPortInterface)
        {
            ntStatus = 
                port->QueryInterface
                ( 
                    PortInterfaceId,
                    (PVOID *) OutPortInterface 
                );
        }
    }

    if (port)
    {
        port->Release();
    }

    return ntStatus;
} // InstallSubDevice

#pragma code_seg()
NTSTATUS
_IRQL_requires_max_(DISPATCH_LEVEL)
PowerControlCallback
(
    _In_        LPCGUID PowerControlCode,
    _In_opt_    PVOID   InBuffer,
    _In_        SIZE_T  InBufferSize,
    _Out_writes_bytes_to_(OutBufferSize, *BytesReturned) PVOID OutBuffer,
    _In_        SIZE_T  OutBufferSize,
    _Out_opt_   PSIZE_T BytesReturned,
    _In_opt_    PVOID   Context
)
{
    UNREFERENCED_PARAMETER(PowerControlCode);
    UNREFERENCED_PARAMETER(BytesReturned);
    UNREFERENCED_PARAMETER(InBuffer);
    UNREFERENCED_PARAMETER(OutBuffer);
    UNREFERENCED_PARAMETER(OutBufferSize);
    UNREFERENCED_PARAMETER(InBufferSize);
    UNREFERENCED_PARAMETER(Context);
    
    return STATUS_NOT_IMPLEMENTED;
}

#pragma code_seg("PAGE")
NTSTATUS InstallRenderFilters(PDEVICE_OBJECT _pDeviceObject, PIRP _pIrp, PADAPTERCOMMON _pAdapterCommon)
{
    NTSTATUS  ntStatus = STATUS_SUCCESS;
    PUNKNOWN                    unknownTopology = NULL;
    PUNKNOWN                    unknownWave     = NULL;
    PPORTCLSETWHELPER pPortClsEtwHelper = NULL;
    PPORTCLSRUNTIMEPOWER        pPortClsRuntimePower = NULL;
    PAGED_CODE();

    // Install MSVAD topology miniport for the render endpoint.
    //
    ntStatus = InstallSubdevice(_pDeviceObject,
                                _pIrp,
                                L"Topology",    // make sure this name matches with MSVAD.Wave.szPname in the inf's [Strings] section
                                CLSID_PortTopology,
                                CLSID_PortTopology, 
                                CreateMiniportTopologyMSVAD,
                                eSpeakersDevice,
                                &RenderTopoMiniportFilterDescriptor,
                                _pAdapterCommon,
                                NULL,
                                IID_IPortTopology,
                                NULL,
                                &unknownTopology 
                                );
  
    // Install MSVAD wave miniport for the render endpoint.
    //
    if (NT_SUCCESS(ntStatus))
    {
        ntStatus = InstallSubdevice(_pDeviceObject,
                                    _pIrp,
                                    L"Wave",// make sure this name matches with MSVAD.Wave.szPname in the inf's [Strings] section
                                    CLSID_PortWaveRT,
                                    CLSID_PortWaveRT,   
                                    CreateMiniportWaveRTMSVAD,
                                    eSpeakersDevice,
                                    &RenderWaveMiniportFilterDescriptor,
                                    _pAdapterCommon,
                                    NULL,
                                    IID_IPortWaveRT,
                                    _pAdapterCommon->WavePortDriverDest(RENDER_PORT),
                                    &unknownWave 
                                    );
    }

    if (unknownTopology && unknownWave)
    {
        // register wave <=> topology connections
        // This will connect bridge pins of wave and topology
        // miniports.
        //
        if ((TopologyPhysicalConnections.ulTopologyOut != (ULONG)-1) &&
            (TopologyPhysicalConnections.ulWaveIn != (ULONG)-1))
        {
            ntStatus =
                PcRegisterPhysicalConnection
                ( 
                    _pDeviceObject,
                    unknownTopology,
                    TopologyPhysicalConnections.ulTopologyOut,
                    unknownWave,
                    TopologyPhysicalConnections.ulWaveIn
                );
        }

        if (NT_SUCCESS(ntStatus))
        {
            if ((TopologyPhysicalConnections.ulWaveOut != (ULONG)-1) &&
                (TopologyPhysicalConnections.ulTopologyIn != (ULONG)-1))
            {
                ntStatus =
                    PcRegisterPhysicalConnection
                    ( 
                        _pDeviceObject,
                        unknownWave,
                        TopologyPhysicalConnections.ulWaveOut,
                        unknownTopology,
                        TopologyPhysicalConnections.ulTopologyIn
                    );
            }
        }

    }
    if (unknownWave) // IID_IPortClsEtwHelper and IID_IPortClsRuntimePower interfaces are only exposed on the WaveRT port.
    {
        ntStatus =unknownWave->QueryInterface (IID_IPortClsEtwHelper, (PVOID *)&pPortClsEtwHelper);
        if (NT_SUCCESS(ntStatus))
        {
            _pAdapterCommon->SetEtwHelper(pPortClsEtwHelper);
        }

        // Let's get the runtime power interface on PortCls.  
        ntStatus = unknownWave->QueryInterface(IID_IPortClsRuntimePower, (PVOID *)&pPortClsRuntimePower);
        if (NT_SUCCESS(ntStatus))
        {
            // This interface would typically be stashed away for later use.  Instead,
            // let's just send an empty control with GUID_NULL.
            NTSTATUS ntStatusTest =
                pPortClsRuntimePower->SendPowerControl
                (
                    _pDeviceObject,
                    &GUID_NULL,
                    NULL,
                    0,
                    NULL,
                    0,
                    NULL
                );

            if (NT_SUCCESS(ntStatusTest) || STATUS_NOT_IMPLEMENTED == ntStatusTest || STATUS_NOT_SUPPORTED == ntStatusTest)
            {
                ntStatus = pPortClsRuntimePower->RegisterPowerControlCallback(_pDeviceObject, &PowerControlCallback, NULL);
                if (NT_SUCCESS(ntStatus))
                {
                    ntStatus = pPortClsRuntimePower->UnregisterPowerControlCallback(_pDeviceObject);
                }
            }
            else
            {
                ntStatus = ntStatusTest;
            }

            pPortClsRuntimePower->Release();
        }
    }

    if (unknownTopology)
    {
        unknownTopology->Release();
    }
    if (unknownWave)
    {
        unknownWave->Release();
    }
    return ntStatus;
}
#ifdef _SUPPORT_INDEPENDENT_MICIN
NTSTATUS InstallCaptureFilters(PDEVICE_OBJECT _pDeviceObject, PIRP _pIrp, PADAPTERCOMMON _pAdapterCommon)
{
    NTSTATUS  ntStatus = STATUS_SUCCESS;
    PUNKNOWN                    unknownTopology = NULL;
    PUNKNOWN                    unknownWave     = NULL;
    PAGED_CODE();
    // Install MSVAD topology miniport for the render endpoint.
    //
    ntStatus = InstallSubdevice(_pDeviceObject,
                                _pIrp,
                                L"TopologyMicIn", // make sure this name matches with MSVAD.Wave.szPname in the inf's [Strings] section
                                CLSID_PortTopology,
                                CLSID_PortTopology, 
                                CreateMiniportTopologyMSVAD,
                                eMicInDevice,
                                &MicInTopoMiniportFilterDescriptor,
                                _pAdapterCommon,
                                NULL,
                                IID_IPortTopology,
                                NULL,
                                &unknownTopology 
                                );
  
    // Install MSVAD wave miniport for the render endpoint.
    //
    if (NT_SUCCESS(ntStatus))
    {
        ntStatus = InstallSubdevice(_pDeviceObject,
                                    _pIrp,
                                    L"WaveMicIn", // make sure this name matches with MSVAD.TopoMicIn.szPname in the inf's [Strings] section
                                    CLSID_PortWaveRT,
                                    CLSID_PortWaveRT,   
                                    CreateMiniportWaveRTMSVAD,
                                    eMicInDevice,
                                    &MicInWaveMiniportFilterDescriptor,
                                    _pAdapterCommon,
                                    NULL,
                                    IID_IPortWaveRT,
                                    _pAdapterCommon->WavePortDriverDest(CAPTURE_PORT), 
                                    &unknownWave 
                                    );
    }

    if (unknownTopology && unknownWave)
    {
        // register wave <=> topology connections
        // This will connect bridge pins of wave and topology
        // miniports.
        //
        if ((MicInTopologyPhysicalConnections.ulTopologyOut != (ULONG)-1) &&
            (MicInTopologyPhysicalConnections.ulWaveIn != (ULONG)-1))
        {
            ntStatus =
                PcRegisterPhysicalConnection
                ( 
                    _pDeviceObject,
                    unknownTopology,
                    MicInTopologyPhysicalConnections.ulTopologyOut,
                    unknownWave,
                    MicInTopologyPhysicalConnections.ulWaveIn
                );
        }

        if (NT_SUCCESS(ntStatus))
        {
            if ((MicInTopologyPhysicalConnections.ulWaveOut != (ULONG)-1) &&
                (MicInTopologyPhysicalConnections.ulTopologyIn != (ULONG)-1))
            {
                ntStatus =
                    PcRegisterPhysicalConnection
                    ( 
                        _pDeviceObject,
                        unknownWave,
                        MicInTopologyPhysicalConnections.ulWaveOut,
                        unknownTopology,
                        MicInTopologyPhysicalConnections.ulTopologyIn
                    );
            }
        }
    }
    if (unknownTopology)
    {
        unknownTopology->Release();
    }
    if (unknownWave)
    {
        unknownWave->Release();
    }
    return ntStatus;
}
#endif
//=============================================================================
NTSTATUS
StartDevice
( 
    IN  PDEVICE_OBJECT          DeviceObject,     
    IN  PIRP                    Irp,              
    IN  PRESOURCELIST           ResourceList      
)  
{
/*++

Routine Description:

  This function is called by the operating system when the device is 
  started.
  It is responsible for starting the miniports.  This code is specific to    
  the adapter because it calls out miniports for functions that are specific 
  to the adapter.                                                            

Arguments:

  DeviceObject - pointer to the driver object

  Irp - pointer to the irp 

  ResourceList - pointer to the resource list assigned by PnP manager

Return Value:

  NT status code.

--*/
    UNREFERENCED_PARAMETER(ResourceList);

    PAGED_CODE();

    ASSERT(DeviceObject);
    ASSERT(Irp);
    ASSERT(ResourceList);

    NTSTATUS                    ntStatus        = STATUS_SUCCESS;

    PADAPTERCOMMON              pAdapterCommon  = NULL;
    PUNKNOWN                    pUnknownCommon  = NULL;

    DPF_ENTER(("[StartDevice]"));

    // create a new adapter common object
    //
    ntStatus = NewAdapterCommon( 
                                &pUnknownCommon,
                                IID_IAdapterCommon,
                                NULL,
                                NonPagedPool 
                                );
    IF_FAILED_JUMP(ntStatus, Exit);

    ntStatus = pUnknownCommon->QueryInterface( IID_IAdapterCommon,(PVOID *) &pAdapterCommon);
    IF_FAILED_JUMP(ntStatus, Exit);

    ntStatus = pAdapterCommon->Init(DeviceObject);
    IF_FAILED_JUMP(ntStatus, Exit);

    // register with PortCls for power-management services
    ntStatus = PcRegisterAdapterPowerManagement( PUNKNOWN(pAdapterCommon), DeviceObject);
    IF_FAILED_JUMP(ntStatus, Exit);

    //
    // Install wave+topology filters for render devices
    //
    ntStatus = InstallRenderFilters(DeviceObject, Irp, pAdapterCommon);
    IF_FAILED_JUMP(ntStatus, Exit);

#ifdef _SUPPORT_INDEPENDENT_MICIN
    //
    // Install wave+topology filters for capture devices
    //
    ntStatus = InstallCaptureFilters(DeviceObject, Irp, pAdapterCommon);
    IF_FAILED_JUMP(ntStatus, Exit);
#endif

Exit:
    // Release the adapter common object.  It either has other references,
    // or we need to delete it anyway.
    //
    if (pAdapterCommon)
    {
        pAdapterCommon->Release();
    }

    if (pUnknownCommon)
    {
        pUnknownCommon->Release();
    }
    
    return ntStatus;
} // StartDevice
#pragma code_seg()

