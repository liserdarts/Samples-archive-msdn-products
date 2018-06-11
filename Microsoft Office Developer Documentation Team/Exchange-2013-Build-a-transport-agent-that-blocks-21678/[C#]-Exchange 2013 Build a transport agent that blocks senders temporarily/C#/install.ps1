$EXDIR="C:\TransportAgentSamples\MailFilter"
net stop MSExchangeTransport

Write-Output "Creating directories"
new-item -Type Directory -path $EXDIR -ErrorAction SilentlyContinue
new-item -Type Directory -path $EXDIR\Data -ErrorAction SilentlyContinue

write-output "Copying files"
copy-item MailFilterAgent.dll $EXDIR -force
copy-item MailFilterConfig.xml $EXDIR\Data -force

write-output "Registering agent"
install-transportagent -Name "Mail Filter Sample" -AssemblyPath $EXDIR\MailFilterAgent.dll -TransportAgentFactory Microsoft.Exchange.Samples.Agents.MailFilterAgent.MailFilterAgentFactory

write-output "Enabling agent"
enable-transportagent -Identity "Mail Filter Sample"
get-transportagent -Identity "Mail Filter Sample"

write-output "Starting Edge Transport"
net start MSExchangeTransport


