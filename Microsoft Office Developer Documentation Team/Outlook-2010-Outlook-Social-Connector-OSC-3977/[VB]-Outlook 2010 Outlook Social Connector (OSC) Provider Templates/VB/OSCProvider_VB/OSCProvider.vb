Imports System
Imports System.Data
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports OutlookSocialProvider

#Region "Readme for OSC Sample Provider "
' VB Provider Template for the Microsoft Outlook Social Connector (OSC) 
' Copyright © 2009-2010 Microsoft Corporation
' Use this template to develop a provider for the OSC
'
' Instructions:
'   1. Change the project name and namespace to your project name and namespace identifiers.
'   2. Modify AssemblyInfo to use the correct assembly information.
'   2. Implement the interface members marked as To-Do and add additional dependencies/references as required.
'   3. Build the Project.
'   4. The provider assembly ProgID must be listed as a key under
'   HKEY_CURRENT_USER\Software\Microsoft\Office\Outlook\SocialConnector\SocialProviders
'   5. To distribute the setup project, create a setup project in Visual Studio or the setup tool of your choice.
'   6. Your setup project should COM register your assembly and also create the ProgID key listed in step 4.
'
' THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
' KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
' 
#End Region

Namespace OSCProvider_VB

    Public Class OSCProvider
        Implements OutlookSocialProvider.ISocialProvider

        Const PROVIDER_VERSION As String = "1.1"
        Const SOCIAL_NETWORK_NAME As String = "Your Social Network Name"
        ''To-do: Use Tools > Create Guid to create an immutable Guid for your provider
        Const networkGuidString As String = "{HHHHHHHH-HHHH-HHHH-HHHH-HHHHHHHHHHHH}"

#Region "ISocialProvider Members"
        Public ReadOnly Property DefaultSiteUrls() As String() Implements OutlookSocialProvider.ISocialProvider.DefaultSiteUrls
            Get
                'To-Do: Implement DefaultSiteUrls
                Dim urls As String() = {"http://www.contoso.com"}
                Return urls
            End Get
        End Property

        Public Function GetAutoConfiguredSession() As OutlookSocialProvider.ISocialSession Implements OutlookSocialProvider.ISocialProvider.GetAutoConfiguredSession
            Return New OSCSession()
        End Function

        Public Function GetCapabilities() As String Implements OutlookSocialProvider.ISocialProvider.GetCapabilities
            'To-Do: Implement GetCapabilities

            'If you implement capabilities using class defined by xsd tool,
            'you should observe the correct schema sequence.
            'Dim capabilities As OSCSchema.capabilities = New OSCSchema.capabilities()
            'capabilities.getFriends = True
            'capabilities.cacheFriends = True
            'Additional schema values in sequence
            'Return(HelperMethods.SerializeObjectToString(capabilities))
            Return String.Empty
        End Function

        Public Function GetSession() As OutlookSocialProvider.ISocialSession Implements OutlookSocialProvider.ISocialProvider.GetSession
            Return New OSCSession()
        End Function

        Public Sub GetStatusSettings(ByRef statusDefault As String, ByRef maxStatusLength As Integer) Implements OutlookSocialProvider.ISocialProvider.GetStatusSettings
            'Not supported in OSC version 1.0 and version 1.1
        End Sub

        Public Sub Load(ByVal socialProviderInterfaceVersion As String, ByVal languageTag As String) Implements OutlookSocialProvider.ISocialProvider.Load
            'To-Do: Implement Load
        End Sub

        Public ReadOnly Property SocialNetworkGuid() As System.Guid Implements OutlookSocialProvider.ISocialProvider.SocialNetworkGuid
            Get
                'To-Do: Create a unique Guid for your provider
                'Return the immutable Guid for your provider
                Return New Guid(networkGuidString)
            End Get
        End Property

        Public ReadOnly Property SocialNetworkIcon() As Byte() Implements OutlookSocialProvider.ISocialProvider.SocialNetworkIcon
            Get
                'To-Do: Implement SocialNetworkIcon 
                'GetProviderJpeg returns byte array for static resource
                Return (HelperMethods.GetProviderJpeg())
            End Get
        End Property

        Public ReadOnly Property SocialNetworkName() As String Implements OutlookSocialProvider.ISocialProvider.SocialNetworkName
            Get
                Return SOCIAL_NETWORK_NAME
            End Get
        End Property

        Public ReadOnly Property Version() As String Implements OutlookSocialProvider.ISocialProvider.Version
            Get
                Return PROVIDER_VERSION
            End Get
        End Property
#End Region
    End Class
End Namespace

