Imports System
Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports OutlookSocialProvider

Namespace OSCProvider_VB
    Public Class HelperMethods

        'OSC Errors
        Public Const OSC_E_FAIL As Integer = &H80004005 'General failure error
        Public Const OSC_E_INTERNAL_ERROR As Integer = &H80041400 'An internal error has occurred due to an invalid operation
        Public Const OSC_E_INVALIDARG As Integer = &H80070057 'Invalid argument error
        Public Const OSC_E_AUTH_ERROR As Integer = &H80041404 'Authentication has failed on the network of the social network
        Public Const OSC_E_NO_CHANGES As Integer = &H80041406 'No changes have occurred since the last synchronization
        Public Const OSC_E_COULDNOTCONNECT As Integer = &H80041402 'No connection is available to connect to the social network
        Public Const OSC_E_NOT_FOUND As Integer = &H80041405 'A resource cannot be found
        Public Const OSC_E_NOT_IMPLEMENTED As Integer = &H80004001 'Not yet implemented
        Public Const OSC_E_OUT_OF_MEMORY As Integer = &H8007000E 'Out of memory error
        Public Const OSC_E_PERMISSION_DENIED As Integer = &H80041403 'Permission for the resource is denied by the OSC provider
        Public Const OSC_E_VERSION As Integer = &H80041401 'The provider does not support this version of OSC provider extensibility


        Public Shared Function SerializeObjectToString(ByVal o As Object)
            Dim writer As System.IO.StringWriter = New System.IO.StringWriter()
            Dim t As Type = o.GetType()
            Dim serializer As XmlSerializer = New XmlSerializer(t)
            serializer.Serialize(writer, o)
            Return writer.ToString()
        End Function

        Public Shared Function GetProviderJpeg() As Byte()
            Dim img As Image = My.Resources.Energy_Bliss
            Dim imgMemoryStream As MemoryStream = New MemoryStream()
            img.Save(imgMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim imgByteArray As Byte() = imgMemoryStream.GetBuffer()
            Return imgByteArray
        End Function

    End Class
End Namespace

