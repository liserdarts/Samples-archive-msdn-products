<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="HelloWorld" generation="1" functional="0" release="0" Id="b366eee5-3ede-48c5-88b1-7c417fc7aa35" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="HelloWorldGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HelloWorld_WebRole:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/HelloWorld/HelloWorldGroup/LB:HelloWorld_WebRole:HttpIn" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="HelloWorld_WebRole:?IsSimulationEnvironment?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloWorld/HelloWorldGroup/MapHelloWorld_WebRole:?IsSimulationEnvironment?" />
          </maps>
        </aCS>
        <aCS name="HelloWorld_WebRole:?RoleHostDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloWorld/HelloWorldGroup/MapHelloWorld_WebRole:?RoleHostDebugger?" />
          </maps>
        </aCS>
        <aCS name="HelloWorld_WebRole:?StartupTaskDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/HelloWorld/HelloWorldGroup/MapHelloWorld_WebRole:?StartupTaskDebugger?" />
          </maps>
        </aCS>
        <aCS name="HelloWorld_WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/HelloWorld/HelloWorldGroup/MapHelloWorld_WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HelloWorld_WebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/HelloWorld/HelloWorldGroup/MapHelloWorld_WebRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:HelloWorld_WebRole:HttpIn">
          <toPorts>
            <inPortMoniker name="/HelloWorld/HelloWorldGroup/HelloWorld_WebRole/HttpIn" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapHelloWorld_WebRole:?IsSimulationEnvironment?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloWorld/HelloWorldGroup/HelloWorld_WebRole/?IsSimulationEnvironment?" />
          </setting>
        </map>
        <map name="MapHelloWorld_WebRole:?RoleHostDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloWorld/HelloWorldGroup/HelloWorld_WebRole/?RoleHostDebugger?" />
          </setting>
        </map>
        <map name="MapHelloWorld_WebRole:?StartupTaskDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloWorld/HelloWorldGroup/HelloWorld_WebRole/?StartupTaskDebugger?" />
          </setting>
        </map>
        <map name="MapHelloWorld_WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/HelloWorld/HelloWorldGroup/HelloWorld_WebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHelloWorld_WebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/HelloWorld/HelloWorldGroup/HelloWorld_WebRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="HelloWorld_WebRole" generation="1" functional="0" release="0" software="C:\Users\davidmu\Downloads\HelloWorld\HelloWorld\HelloWorld\bin\Debug\HelloWorld.csx\roles\HelloWorld_WebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="?IsSimulationEnvironment?" defaultValue="" />
              <aCS name="?RoleHostDebugger?" defaultValue="" />
              <aCS name="?StartupTaskDebugger?" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HelloWorld_WebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HelloWorld_WebRole&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/HelloWorld/HelloWorldGroup/HelloWorld_WebRoleInstances" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyID name="HelloWorld_WebRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="d3a6082e-34ff-455f-af14-8d545e723bbc" ref="Microsoft.RedDog.Contract\ServiceContract\HelloWorldContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="a0e538a9-4b2c-4329-b939-c5d22d8d255e" ref="Microsoft.RedDog.Contract\Interface\HelloWorld_WebRole:HttpIn@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/HelloWorld/HelloWorldGroup/HelloWorld_WebRole:HttpIn" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>