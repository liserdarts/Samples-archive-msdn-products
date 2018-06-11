$EXDIR="C:\TransportAgentSamples\Antivirus"

Net Stop MSExchangeTransport
Net Stop Antivirus

Write-Output "Unregistering Antivirus service..."
AntivirusService.exe -unregserver
Sleep 2

Write-Output "Disabling agent..."
Disable-TransportAgent -Identity "Antivirus Sample"

Write-Output "Uninstalling agent..."
Uninstall-TransportAgent -Identity "Antivirus Sample"

Write-Output "Deleting directories and files..."
Remove-Item $EXDIR\* -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item $EXDIR -Recurse -Force -ErrorAction SilentlyContinue

Write-Output "Starting Transport..."
Net Start MSExchangeTransport

Write-Output "Uninstallation complete."
