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

## Create a trusted application pool, if needed, 
## register trusted app in the pool, 
## and assigns it a trusted application endpoint:

##if (!(Get-CsTrustedApplicationPool -Identity $trustedAppPoolFqdn))
	New-CsTrustedApplicationPool -identity $trustedAppPoolFqdn -site $trustedAppPoolSite -registrar $registrarFqdn

##if (!(New-CsTrustedApplication -identity $trustedAppPoolFqdn/@trustedAppName))
	New-CsTrustedApplication -identity $trustedAppPoolFqdn/$trustedAppName -port $trustedAppPort

Enable-cstopology

New-CsTrustedApplicationEndpoint -sipaddress $trustedAppSipAddress `
	-applicationId $trustedAppName `
	-trustedapplicationpoolfqdn $trustedAppPoolFqdn

## Register MSPL-script:
New-CsServerApplication -Uri $msplAppUri `
   -Priority 4  `
   -Identity $msplAppId  `
   -Critical $False 

Set-CsServerApplication -Identity $msplAppId -enabled $true

Get-CsServerApplication -Identity $msplAppId   

## Added administrator through console->configurationmgr->groups->RTCserverapplications
## requires logging off after that

## open cmd as admin
