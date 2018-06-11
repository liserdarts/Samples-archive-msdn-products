$EXDIR="C:\TransportAgentSamples\MailFilter"
net stop MSExchangeTransport

Write-Output "Disabling Agent..."
Disable-Transportagent -Identity "Mail Filter Sample" -Confirm:$False

Write-Output "Uninstalling Agent..."
Uninstall-TransportAgent -Identity "Mail Filter Sample" -Confirm:$False

Write-Output "Deleting Files and Folders..."
remove-item -path $EXDIR\* -Recurse -Force -ErrorAction SilentlyContinue
remove-item -path $EXDIR -Recurse -Force -ErrorAction SilentlyContinue

Net Start MSExchangeTransport

Write-Output "Uninstall Complete."
