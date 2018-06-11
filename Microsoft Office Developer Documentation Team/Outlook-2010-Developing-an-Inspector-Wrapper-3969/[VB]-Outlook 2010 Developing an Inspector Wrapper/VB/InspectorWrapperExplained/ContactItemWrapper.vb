''' <summary>
''' We derive a Wrapper for each MessageClass / ItemType
''' </summary>
Friend Class ContactItemWrapper
    Inherits InspectorWrapper

    ''' <summary>
    ''' .ctor
    ''' </summary>
    ''' <param name="inspector">The Outlook Inspector instance that should be handled</param>
    Public Sub New(ByVal inspector As Outlook.Inspector)
        MyBase.New(inspector)
    End Sub

    ''' <summary>
    ''' The Object instance behind the Inspector (CurrentItem)
    ''' </summary>
    Private m_Item As Outlook.ContactItem
    Public Property Item As Outlook.ContactItem
        Get
            Return m_Item
        End Get
        Private Set(ByVal value As Outlook.ContactItem)
            m_Item = value
        End Set
    End Property

    ''' <summary>
    ''' Method is called when the Wrapper has been initialized
    ''' </summary>
    Protected Overloads Overrides Sub Initialize()
        ' Get the Item of the current Inspector
        Item = DirectCast(Inspector.CurrentItem, Outlook.ContactItem)

        ' Register for the Item events
        AddHandler Item.Open, AddressOf Item_Open
        AddHandler Item.Write, AddressOf Item_Write

        ' Set the initial size of the Inspector
        Inspector.Width = 800
        Inspector.Height = 600
    End Sub

    ''' <summary>
    ''' This Method is called when the Item is saved.
    ''' </summary>
    ''' <param name="Cancel">When set to true, the save operation is cancelled</param>
    Private Sub Item_Write(ByRef Cancel As Boolean)
        'TODO: Implement something 
    End Sub

    ''' <summary>
    ''' This Method is called when the Item is visible and the UI is initialized.
    ''' </summary>
    ''' <param name="Cancel">When you set this property to true, the Inspector is closed.</param>
    Private Sub Item_Open(ByRef Cancel As Boolean)
        'TODO: Implement something 
    End Sub


    ''' <summary>
    ''' The Close Method is called when the Inspector has been closed.
    ''' Do your cleanup tasks here.
    ''' The UI is gone, can't access it here.
    ''' </summary>
    Protected Overloads Overrides Sub Close()
        ' unregister events
        RemoveHandler Item.Write, AddressOf Item_Write
        RemoveHandler Item.Open, AddressOf Item_Open

        ' required, just stting to NULL may keep a reference in memory of the Garbage Collector.
        Item = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    ''' <summary>
    ''' Prevent resizing of the Inspector by setting Cancel to true.
    ''' </summary>
    ''' <param name="Cancel">The Cancel parameter is passed as reference</param>
    Protected Overloads Overrides Sub BeforeSize(ByRef Cancel As Boolean)
        Cancel = True
    End Sub
End Class