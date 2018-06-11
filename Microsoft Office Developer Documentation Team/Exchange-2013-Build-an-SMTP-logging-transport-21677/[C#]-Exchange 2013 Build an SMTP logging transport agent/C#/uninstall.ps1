$EXDIR="C:\TransportAgentSamples\SmtpLogging"
Net Stop MSExchangeTransport

Write-Output "Disabling Agent..."
Disable-Transportagent -Identity "SMTP Logging Sample" -Confirm:$False

Write-Output "Uninstalling Agent..."
Uninstall-TransportAgent -Identity "SMTP Logging Sample" -Confirm:$False

Write-Output "Deleting Files and Folders..."
remove-item $EXDIR\* -Recurse -Force -ErrorAction SilentlyContinue
remove-item $EXDIR -Recurse -Force -ErrorAction SilentlyContinue

Net Start MSExchangeTransport

Write-Output "Uninstall Complete."
