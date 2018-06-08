
The LogDrive sample demonstrates how to write services that support VM Role.  It also shows how to use the Azure Storage Drive feature.

For simplicity, each role instance uses its own storage blob as a drive - no sharing between instances is shown.  The sample mounts a drive at system startup then configures IIS to write HTTP logfiles to the drive.  


This sample shows the following features of the platform:

- Azure Storage Drive
- Application Configuration Settings
- Local Resources
- Role Instance Status Check
- Windows Service support

Image Configuration Steps
-------------------------

1. Enable Windows features.  From the command-line:

DISM /Online /Enable-Feature /FeatureName:NetFx3 /FeatureName:IIS-WebServerRole /FeatureName:IIS-WebServer /FeatureName:IIS-CommonHttpFeatures /FeatureName:IIS-HttpErrors /FeatureName:IIS-ApplicationDevelopment /FeatureName:IIS-HealthAndDiagnostics /FeatureName:IIS-HttpLogging /FeatureName:IIS-RequestMonitor /FeatureName:IIS-Security /FeatureName:IIS-RequestFiltering /FeatureName:IIS-Performance /FeatureName:IIS-WebServerManagementTools /FeatureName:IIS-StaticContent /FeatureName:IIS-DefaultDocument /FeatureName:IIS-DirectoryBrowsing /FeatureName:IIS-HttpCompressionStatic /FeatureName:IIS-ManagementConsole

2. Install the Windows Azure Integration Components.

3. Install the sample. 

4. Run the System Preparation Tool.
	
5. Upload the image.

6. Configure the service model configuration file.
	a. Set the OsImage to the name of the uploaded image.
	b. Set the "LogDriveService.BlobPath" setting to refer to an existing storage account.  Note that the value is a format string; the sample code replaces the {0} field with the role instance ID.
	c. Set the "LogDriveService.AccountName" setting to the name of the existing storage account.
	d. Set the "LogDriveService.AccountKey" setting to the key of the existing storage account.

7. Package and deploy the service.

Notes
-----

1. Service Dependencies:  The sample declares a dependency on the Windows Azure Cloud Drive service.  It also declares a dependency on the 'VSS' service as a workaround to ensure that the Cloud Drive API initializes without error.
	
2. Setup Project issue: Visual Studio has an incompatibility with 64-bit executables that contain managed installers.  A workaround is implemented by calling a post-build script entitled "FixMSI.js".