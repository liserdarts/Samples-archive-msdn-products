$EXDIR="C:\TransportAgentSamples\BandwidthLogging"
Net Stop MSExchangeTransport

Write-Output "Creating directories..."
New-Item -Type Directory -path $EXDIR -ErrorAction SilentlyContinue
New-Item -Type Directory -path $EXDIR\Log -ErrorAction SilentlyContinue

Write-Output "Copying files..."
Copy-Item BandwidthLogging.dll $EXDIR -force

Write-Output "Registering agent..."
Install-TransportAgent -Name "Bandwidth Logging Sample" -AssemblyPath $EXDIR\BandwidthLogging.dll -TransportAgentFactory Microsoft.Exchange.Samples.Agents.BandwidthLogging.BandwidthLoggerFactory

Write-Output "Enabling agent"
Enable-TransportAgent -Identity "Bandwidth Logging Sample"
Get-TransportAgent -Identity "Bandwidth Logging Sample"

Net Start MSExchangeTransport

Write-Output "Installation complete. Please exit the Exchange Management Shell."


