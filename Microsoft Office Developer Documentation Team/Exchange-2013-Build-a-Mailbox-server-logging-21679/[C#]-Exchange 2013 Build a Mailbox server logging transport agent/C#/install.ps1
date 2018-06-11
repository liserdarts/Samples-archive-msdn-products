$EXDIR="C:\TransportAgentSamples\MailboxServerLogging"
Net Stop MSExchangeTransport

Write-Output "Creating directories"
New-Item -Type Directory -path $EXDIR\Log     -ErrorAction SilentlyContinue

Write-Output "Copying files"
Copy-Item MailboxServerLogging.dll $EXDIR -force

Write-Output "Registering agent"
Install-TransportAgent -Name "Mailbox Server Logging Sample" -AssemblyPath $EXDIR\MailboxServerLogging.dll -TransportAgentFactory Microsoft.Exchange.Samples.Agents.MailboxServerLogging.MessageLoggerFactory

Write-Output "Enabling agent"
Enable-TransportAgent -Identity "Mailbox Server Logging Sample"
Get-TransportAgent -Identity "Mailbox Server Logging Sample"

Net Start MSExchangeTransport

Write-Output "Install Complete. Please exit the Exchange Management Shell."