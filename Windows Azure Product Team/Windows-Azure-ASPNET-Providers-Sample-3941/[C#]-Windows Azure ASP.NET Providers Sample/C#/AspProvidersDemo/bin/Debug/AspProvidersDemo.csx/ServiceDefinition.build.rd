<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AspProvidersDemo" generation="1" functional="0" release="0" Id="ec927976-5509-4652-9b22-bee1c83bef19" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AspProvidersDemoGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="AspProvidersDemoWebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/LB:AspProvidersDemoWebRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="AspProvidersDemoWebRole:?IsSimulationEnvironment?" defaultValue="">
          <maps>
            <mapMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/MapAspProvidersDemoWebRole:?IsSimulationEnvironment?" />
          </maps>
        </aCS>
        <aCS name="AspProvidersDemoWebRole:?RoleHostDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/MapAspProvidersDemoWebRole:?RoleHostDebugger?" />
          </maps>
        </aCS>
        <aCS name="AspProvidersDemoWebRole:?StartupTaskDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/MapAspProvidersDemoWebRole:?StartupTaskDebugger?" />
          </maps>
        </aCS>
        <aCS name="AspProvidersDemoWebRole:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/MapAspProvidersDemoWebRole:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="AspProvidersDemoWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/MapAspProvidersDemoWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AspProvidersDemoWebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/MapAspProvidersDemoWebRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:AspProvidersDemoWebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/AspProvidersDemoWebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapAspProvidersDemoWebRole:?IsSimulationEnvironment?" kind="Identity">
          <setting>
            <aCSMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/AspProvidersDemoWebRole/?IsSimulationEnvironment?" />
          </setting>
        </map>
        <map name="MapAspProvidersDemoWebRole:?RoleHostDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/AspProvidersDemoWebRole/?RoleHostDebugger?" />
          </setting>
        </map>
        <map name="MapAspProvidersDemoWebRole:?StartupTaskDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/AspProvidersDemoWebRole/?StartupTaskDebugger?" />
          </setting>
        </map>
        <map name="MapAspProvidersDemoWebRole:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/AspProvidersDemoWebRole/DataConnectionString" />
          </setting>
        </map>
        <map name="MapAspProvidersDemoWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/AspProvidersDemoWebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAspProvidersDemoWebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/AspProvidersDemoWebRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="AspProvidersDemoWebRole" generation="1" functional="0" release="0" software="C:\Users\davidmu\Documents\Visual Studio 2010\Projects\ASPProviders\AspProvidersDemo\bin\Debug\AspProvidersDemo.csx\roles\AspProvidersDemoWebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="768" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="?IsSimulationEnvironment?" defaultValue="" />
              <aCS name="?RoleHostDebugger?" defaultValue="" />
              <aCS name="?StartupTaskDebugger?" defaultValue="" />
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AspProvidersDemoWebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AspProvidersDemoWebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/AspProvidersDemoWebRoleInstances" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyID name="AspProvidersDemoWebRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="977b27b5-1190-4677-ac4a-fc8b6dab0800" ref="Microsoft.RedDog.Contract\ServiceContract\AspProvidersDemoContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="2597dd16-aef8-4f68-90a7-7c9551b17649" ref="Microsoft.RedDog.Contract\Interface\AspProvidersDemoWebRole:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/AspProvidersDemo/AspProvidersDemoGroup/AspProvidersDemoWebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>