$EXDIR="C:\TransportAgentSamples\SmtpLogging"
Net Stop MSExchangeTransport

Write-Output "Creating directories"
New-Item -Type Directory -path $EXDIR\Log -ErrorAction SilentlyContinue

Write-Output "Copying files"
Copy-Item SmtpLogging.dll $EXDIR -force

Write-Output "Registering Agent"
Install-TransportAgent -Name "SMTP Logging Sample" -AssemblyPath $EXDIR\SmtpLogging.dll -TransportAgentFactory Microsoft.Exchange.Samples.Agents.SmtpLogging.MessageLoggerFactory 

Write-Output "Enabling Agent"
Enable-TransportAgent -Identity "SMTP Logging Sample"
Get-Transportagent -Identity "SMTP Logging Sample"

Net Start MSExchangeTransport

Write-Output "Install Complete."