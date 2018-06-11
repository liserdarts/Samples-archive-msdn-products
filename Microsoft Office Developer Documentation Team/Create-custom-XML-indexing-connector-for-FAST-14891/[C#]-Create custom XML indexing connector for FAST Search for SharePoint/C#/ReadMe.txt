Description:
The MyFileConnector Custom Indexing Connector sample is a basic indexing connector that crawls all files and folders within a file share on a Windows file system.

Prerequisites:
Microsoft SharePoint Server 2010
Microsoft Visual Studio

Files:

MyFileConnector.sln - MyFileConnector solution file.

MyFileConnector.csproj - MyFileConnector project file.

Test.snk - MyFileConnector key file.

MyFile.cs - Defines the file and folder external content types.  Provides the method implementations for the Finder and SpecificFinder operations.

MyFileConnector.cs - Derives from the StructuredRepositorySystemUtility(Of T) class, which implements the ISystemUtility interface.

MyFileLobUri.cs - Derives from the LobUri class, which maps the URLs as they are passed from the Search service application to Microsoft Business Connectivity Services (BCS). 

MyFileNamingContainer.cs - Implements the INamingContainer interface, and maps the URLs as they are passed from the Business Connectivity Services to the Search service application.

MyFileModel.xml - MyFileConnector BCS model file.


Setup:
See Code Sample: MyFileConnector Custom Indexing Connector (http://msdn.microsoft.com/en-us/library/ff625800.aspx) for step-by-step instructions on setting up the sample.


