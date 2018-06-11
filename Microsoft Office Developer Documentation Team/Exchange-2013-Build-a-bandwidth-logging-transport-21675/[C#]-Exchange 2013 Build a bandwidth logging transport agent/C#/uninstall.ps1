$EXDIR="C:\TransportAgentSamples\BandwidthLogging"
Net Stop MSExchangeTransport

Write-Output "Disabling agent"
Disable-TransportAgent -Identity "Bandwidth Logging Sample" -Confirm:$false

Write-Output "Uninstalling agent..."
Uninstall-TransportAgent -Identity "Bandwidth Logging Sample" -Confirm:$false

Write-Output "Deleting directories and files..."
Remove-Item $EXDIR\* -Recurse -ErrorAction SilentlyContinue
Remove-Item $EXDIR -Recurse -ErrorAction SilentlyContinue

Write-Output "Starting Transport..."
Net Start MSExchangeTransport

Write-Output "Uninstallation complete."


