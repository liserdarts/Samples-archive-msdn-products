$EXDIR="C:\TransportAgentSamples\MailboxServerLogging"
Net Stop MSExchangeTransport

Write-Output "Disabling Agent..."
Disable-TransportAgent -Identity "Mailbox Server Logging Sample" -Confirm:$false

Write-Output "Uninstalling Agent.."
Uninstall-TransportAgent -Identity "Mailbox Server Logging Sample" -Confirm:$false

Write-Output "Deleteing Files and Folders..."
Remove-Item $EXDIR\* -Recurse -ErrorAction SilentlyContinue
Remove-Item $EXDIR -Recurse -ErrorAction SilentlyContinue

Net Start MsExchangeTransport

Write-Output "Uninstall Complete."
