Before building and running this sample application, do the following:

1. Make sure the Persistent Chat SDK is installed in the local computer.

2. Verify that the path of the assembly references to Microsoft.Rtc.Collaboration.Dll and Microsoft.Rtc.Collaboration.PersistentChat.DLL point to the correct location on the local computer. By default, this should be in "%proffile%\Microsoft Lync Server 2013\Persistent Chat SDK\"

3. Edit the SampleCommon.cs file to update the application configuration to your test or development environment. The to-be-updated application configuration include the following settings:
	OcsServer,
	UserSipUri,
	Username,
	Password,  if required
	UsingSso,
	MemberUserString1,
	MemberUserString2,
	ManagerUserString,
	PresenterUserString