''' <summary>
''' A more specialized wrapper class 
''' for Contact Items with a MessageClass of "IPM.Contact.X4U"
''' </summary>
Friend Class X4UContactItemWrapper
    Inherits ContactItemWrapper

    ''' <summary>
    ''' .ctor
    ''' </summary>
    ''' <param name="inspector">The Outlook Inspector instance that should be handled</param>
    Public Sub New(ByVal inspector As Outlook.Inspector)
        MyBase.New(inspector)
    End Sub

End Class
