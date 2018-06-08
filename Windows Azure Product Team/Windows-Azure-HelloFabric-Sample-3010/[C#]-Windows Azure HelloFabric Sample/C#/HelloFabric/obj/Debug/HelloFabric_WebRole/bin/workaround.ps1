[Reflection.Assembly]::LoadWithPartialName("Microsoft.WindowsAzure.ServiceRuntime") 

#Replace the WCFService.svclog resource name with your resource name found in your service config file.
#By default the Visual Studio template uses the resource name WCFService.svclog.
$WCFLogResourceName = "WCFService.svclog"

#Do no touch these variables. These are the enviroment variables.
$DiagnosticStore = "DiagnosticStore"
$WAContainerId = "WA_CONTAINER_SID"

#Set the correct ACL for Failed Request Logs Folder
Function Fix-FailedLogsACL {
    trap { 
      echo ""
      echo "Cannot set the ACL for Failed Request Logs Folder: $FailedRequestLogsPath"
      break 
    }

    $DiagStorePath = [Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment]::GetLocalResource($DiagnosticStore).RootPath
    $FailedRequestLogsPath = join-path -path $DiagStorePath -childpath FailedReqLogFiles
    $FailedRequestsACL = (Get-Item $FailedRequestLogsPath).GetAccessControl("Access")
    $AccessRule = New-Object System.Security.AccessControl.FileSystemAccessRule("IIS_IUSRS", "Read, Write, Traverse, TakeOwnership", "ContainerInherit, ObjectInherit",   "None", "Allow")
    $FailedRequestsACL.AddAccessRule($AccessRule)
    Set-Acl $FailedRequestLogsPath $FailedRequestsACL
        
    echo ""
    echo "ACL set successfully for failed request logs directory:  $FailedRequestLogsPath"
}

#Set the correct ACL for WCF Logs Folder
Function Fix-WCFLogsACL {
    trap {      
      echo ""
      echo "Cannot set the ACL for WCF Logs Folder: $WCFLogPath"
      break 
    }
    
    $WCFLogPath = [Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment]::GetLocalResource($WCFLogResourceName).RootPath
    $WAContainerUser = [environment]::GetEnvironmentVariable($WAContainerId)
    $WAContainerUserSID = New-Object System.Security.Principal.SecurityIdentifier($WAContainerUser)
    $WAContainerUserAccount = $WAContainerUserSID.Translate( [System.Security.Principal.NTAccount])

    $AccessRule = New-Object System.Security.AccessControl.FileSystemAccessRule($WAContainerUserAccount.Value, "Read, Traverse, Delete", "ContainerInherit, ObjectInherit",   "None", "Allow")
    $WcfLogACL = (Get-Item $WCFLogPath).GetAccessControl("Access")    
    $WcfLogACL.AddAccessRule($AccessRule)
    Set-Acl $WCFLogPath $WcfLogACL     
    
    echo ""
    echo "ACL set successfully for WCF logs directory: $WCFLogPath" 
}

#fix the ACL issues
Fix-FailedLogsACL
Fix-WCFLogsACL