## $msplAppUri in the New-CsServerApplication cmdlet must match the appUri attribute of the <applicationManifest> element in the .am file.

$edgeServerFqdn = "w15-lync-edge.metio.ms";
$registrarFqdn = "w15-lync-se1.metio.ms";
$trustedAppPoolSite = "Metio.ms"
$trustedAppPort = 7061;

$trustedAppPoolFqdn = $registrarFqdn;
##$trustedAppPoolFqdn = $edgeServerFqdn;

$msplAppName = "MsplPresenceSubInterceptor";
$mspLAppUri = "http://www.microsoft.com/lyncServer/sdk/samples/presencesubinterceptorforbot";

$msplAppId ="Service:Registrar:$registrarFqdn/$msplAppName";  
##$msplAppId ="Service:EdgeServer:$edgeServerFqdn/$msplAppName";  

$trustedAppName = "UcmaPresenceSubInterceptor";
$trustedAppSipAddress = "sip:PresenceSubInterceptor@metio.ms";
$trustedAppId = "urn:application:UcmaPresenceSubInterceptor";

##if (Get-CsServerApplication -Identity @msplAppId)
	Remove-CsServerApplication -Identity $msplAppId

##if (Get-CsTrustedApplicationEndpoint -Identity $trustedAppSipAddress)
	Remove-CsTrustedApplicationEndpoint -Identity $trustedAppSipAddress 

##if (Get-CsTrustedApplication -Identity $trustedAppPoolFqdn/$trustedAppName)
	Remove-CsTrustedApplication -Identity $trustedAppPoolFqdn/$trustedAppName

##if (Get-CsTrustedApplicationPool -Identity $trustedAppPoolFqdn)
	Remove-CsTrustedApplicationPool -Identity $trustedAppPoolFqdn