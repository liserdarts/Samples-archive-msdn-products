<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="ContosoEventsWeb.Azure" generation="1" functional="0" release="0" Id="642f5ce8-52f1-489c-960b-4bd9785c0445" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="ContosoEventsWeb.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="ContosoEventsWeb:AppEndpoint" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/LB:ContosoEventsWeb:AppEndpoint" />
          </inToChannel>
        </inPort>
        <inPort name="ContosoEventsWeb:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/LB:ContosoEventsWeb:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="ContosoEventsWeb:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/MapContosoEventsWeb:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ContosoEventsWebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/MapContosoEventsWebInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:ContosoEventsWeb:AppEndpoint">
          <toPorts>
            <inPortMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/ContosoEventsWeb/AppEndpoint" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:ContosoEventsWeb:Endpoint1">
          <toPorts>
            <inPortMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/ContosoEventsWeb/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapContosoEventsWeb:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/ContosoEventsWeb/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapContosoEventsWebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/ContosoEventsWebInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="ContosoEventsWeb" generation="1" functional="0" release="0" software="C:\MSDNApps\ContosoEvents\ContosoEventsWeb.Azure\csx\Debug\roles\ContosoEventsWeb" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="AppEndpoint" protocol="https" portRanges="443" />
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ContosoEventsWeb&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;ContosoEventsWeb&quot;&gt;&lt;e name=&quot;AppEndpoint&quot; /&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/ContosoEventsWebInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/ContosoEventsWebUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/ContosoEventsWebFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="ContosoEventsWebUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="ContosoEventsWebFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="ContosoEventsWebInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="7f20cb9b-0654-45fd-b317-b7b059f07462" ref="Microsoft.RedDog.Contract\ServiceContract\ContosoEventsWeb.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="f7bfab7d-5604-452c-90e1-e3e89eee9324" ref="Microsoft.RedDog.Contract\Interface\ContosoEventsWeb:AppEndpoint@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/ContosoEventsWeb:AppEndpoint" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="49f7763b-edb9-4f01-a24e-e0f7b1b0405f" ref="Microsoft.RedDog.Contract\Interface\ContosoEventsWeb:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ContosoEventsWeb.Azure/ContosoEventsWeb.AzureGroup/ContosoEventsWeb:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>