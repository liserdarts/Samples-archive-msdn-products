$EXDIR="C:\TransportAgentSamples\XHeader"
Net Stop MSExchangeTransport

Write-Output "Creating directories"
New-Item -Type Directory -path $EXDIR -ErrorAction SilentlyContinue

Write-Output "Copying files"
Copy-Item XHeader.dll $EXDIR -force
Copy-Item configuration.xml $EXDIR -force

Write-Output "Registering agent"
Install-TransportAgent -Name "XHeader Agent Sample" -AssemblyPath $EXDIR\XHeader.dll -TransportAgentFactory Microsoft.Exchange.Samples.Agents.XHeader.XHeaderAgentFactory

Write-Output "Enabling Agent"
Enable-Transportagent -Identity "XHeader Agent Sample" 
Get-TransportAgent -Identity "XHeader Agent Sample" 

Net Start MSExchangeTransport

Write-Output "Agent installation complete"
Write-Output "Please exit the Exchange Management Shell."