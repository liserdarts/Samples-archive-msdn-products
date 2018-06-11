
if($args.length -ne 1)
{
    write-host "You need to specify the site url" -Fore Red
}
else
{
	$processor = "33B06720-F08A-4070-8A47-760BBB7234F6"
	write-host "Switching Dialog Processor...$processor" -Fore Yellow
	$url = $args[0]
	$rootWeb = Get-SPWeb $url
	$rootWeb.FileDialogPostProcessorId = $processor
	$rootWeb.Update()
}