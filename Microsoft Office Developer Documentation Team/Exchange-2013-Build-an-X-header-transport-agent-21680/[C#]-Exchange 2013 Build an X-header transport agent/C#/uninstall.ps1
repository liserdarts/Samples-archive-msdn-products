$EXDIR="C:\TransportAgentSamples\XHeader"
Net Stop MSExchangeTransport

Write-Output "Disabling Agent..."
Disable-Transportagent -Identity "XHeader Agent Sample" -Confirm:$false

Write-Output "De-registering agent..."
Uninstall-TransportAgent -Identity "XHeader Agent Sample" -Confirm:$false

Write-Output "Deleting files and folders..."
Remove-Item $EXDIR\* -Recurse -ErrorAction SilentlyContinue
Remove-Item $EXDIR -Recurse -ErrorAction SilentlyContinue

Net Start MSExchangeTransport

Write-Output "Uninstall Complete."