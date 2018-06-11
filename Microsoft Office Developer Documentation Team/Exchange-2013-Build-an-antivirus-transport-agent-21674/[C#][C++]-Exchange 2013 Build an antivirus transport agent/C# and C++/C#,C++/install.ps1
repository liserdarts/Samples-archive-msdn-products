$EXDIR="C:\TransportAgentSamples\Antivirus"

net stop MSExchangeTransport
net stop Antivirus

write-output "Copying files"
new-item -Type Directory -path $EXDIR -ErrorAction SilentlyContinue

write-output "Copying files"
copy-item AntivirusAgent.dll         $EXDIR -force
copy-item ComInterop.dll             $EXDIR -force

write-output "Registering agent"
install-transportagent -Name "Antivirus Sample" -AssemblyPath $EXDIR\AntivirusAgent.dll -TransportAgentFactory Microsoft.Exchange.Samples.Antivirus.AntivirusAgentFactory

write-output "Enabling agent"
enable-transportagent -Identity "Antivirus Sample"
get-transportagent -Identity "Antivirus Sample"

write-output "Registering Antivirus service"
.\AntivirusService.exe -service
sleep 2

write-output "Starting COM server"
net start Antivirus

write-output "Starting Edge Transport"
net start MSExchangeTransport

Write-Output "Installation complete. Please exit the Exchange Management Shell."

