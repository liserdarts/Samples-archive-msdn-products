<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="HelloFabric" generation="1" functional="0" release="0" Id="a075a559-32c1-44cf-a95a-fb229aeb1c25" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="HelloFabricGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HelloFabric_WebRole:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/HelloFabric/HelloFabricGroup/LB:HelloFabric_WebRole:HttpIn" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="HelloFabric_WebRole:?IsSimulationEnvironment?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WebRole:?IsSimulationEnvironment?" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WebRole:?RoleHostDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WebRole:?RoleHostDebugger?" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WebRole:?StartupTaskDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WebRole:?StartupTaskDebugger?" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WebRole:BannerText" defaultValue="">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WebRole:BannerText" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WorkerRole:?IsSimulationEnvironment?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WorkerRole:?IsSimulationEnvironment?" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WorkerRole:?RoleHostDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WorkerRole:?RoleHostDebugger?" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WorkerRole:?StartupTaskDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WorkerRole:?StartupTaskDebugger?" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HelloFabric_WorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/HelloFabric/HelloFabricGroup/MapHelloFabric_WorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:HelloFabric_WebRole:HttpIn">
          <toPorts>
            <inPortMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRole/HttpIn" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:HelloFabric_WorkerRole:DayTime">
          <toPorts>
            <inPortMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WorkerRole/DayTime" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapHelloFabric_WebRole:?IsSimulationEnvironment?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRole/?IsSimulationEnvironment?" />
          </setting>
        </map>
        <map name="MapHelloFabric_WebRole:?RoleHostDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRole/?RoleHostDebugger?" />
          </setting>
        </map>
        <map name="MapHelloFabric_WebRole:?StartupTaskDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRole/?StartupTaskDebugger?" />
          </setting>
        </map>
        <map name="MapHelloFabric_WebRole:BannerText" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRole/BannerText" />
          </setting>
        </map>
        <map name="MapHelloFabric_WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHelloFabric_WebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRoleInstances" />
          </setting>
        </map>
        <map name="MapHelloFabric_WorkerRole:?IsSimulationEnvironment?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WorkerRole/?IsSimulationEnvironment?" />
          </setting>
        </map>
        <map name="MapHelloFabric_WorkerRole:?RoleHostDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WorkerRole/?RoleHostDebugger?" />
          </setting>
        </map>
        <map name="MapHelloFabric_WorkerRole:?StartupTaskDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WorkerRole/?StartupTaskDebugger?" />
          </setting>
        </map>
        <map name="MapHelloFabric_WorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHelloFabric_WorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="HelloFabric_WebRole" generation="1" functional="0" release="0" software="C:\Users\davidmu\Documents\Visual Studio 2010\Projects\Windows_Azure_HelloFabric_Sample\C#\HelloFabric\bin\Debug\HelloFabric.csx\roles\HelloFabric_WebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="3584" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" portRanges="80" />
              <outPort name="HelloFabric_WorkerRole:DayTime" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/HelloFabric/HelloFabricGroup/SW:HelloFabric_WorkerRole:DayTime" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="?IsSimulationEnvironment?" defaultValue="" />
              <aCS name="?RoleHostDebugger?" defaultValue="" />
              <aCS name="?StartupTaskDebugger?" defaultValue="" />
              <aCS name="BannerText" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HelloFabric_WebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HelloFabric_WebRole&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HelloFabric_WorkerRole&quot;&gt;&lt;e name=&quot;DayTime&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="localStoreOne" defaultAmount="[10,10,10]" defaultSticky="false" kind="Directory" />
              <resourceReference name="localStoreTwo" defaultAmount="[10,10,10]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRoleInstances" />
            <sCSPolicyFaultDomainMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="HelloFabric_WorkerRole" generation="1" functional="0" release="0" software="C:\Users\davidmu\Documents\Visual Studio 2010\Projects\Windows_Azure_HelloFabric_Sample\C#\HelloFabric\bin\Debug\HelloFabric.csx\roles\HelloFabric_WorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="DayTime" protocol="tcp" />
              <outPort name="HelloFabric_WorkerRole:DayTime" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/HelloFabric/HelloFabricGroup/SW:HelloFabric_WorkerRole:DayTime" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="?IsSimulationEnvironment?" defaultValue="" />
              <aCS name="?RoleHostDebugger?" defaultValue="" />
              <aCS name="?StartupTaskDebugger?" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HelloFabric_WorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HelloFabric_WebRole&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;HelloFabric_WorkerRole&quot;&gt;&lt;e name=&quot;DayTime&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WorkerRoleInstances" />
            <sCSPolicyFaultDomainMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="HelloFabric_WebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="HelloFabric_WorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="HelloFabric_WebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="HelloFabric_WorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="a850997c-f678-4268-af37-46ba0578fc0b" ref="Microsoft.RedDog.Contract\ServiceContract\HelloFabricContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="b93e8240-9b8a-4a7c-a192-4174b6597ec1" ref="Microsoft.RedDog.Contract\Interface\HelloFabric_WebRole:HttpIn@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/HelloFabric/HelloFabricGroup/HelloFabric_WebRole:HttpIn" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>