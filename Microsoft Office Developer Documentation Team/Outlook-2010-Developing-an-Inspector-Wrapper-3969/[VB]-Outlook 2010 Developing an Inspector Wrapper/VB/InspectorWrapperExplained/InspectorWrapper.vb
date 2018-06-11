''' <summary>
''' Eventhandler used to correctly clean up resources
''' </summary>
''' <param name="id">The unique id of the Inspector instance</param>
Friend Delegate Sub InspectorWrapperClosedEventHandler(ByVal id As Guid)

''' <summary>
''' The base class for all InspectorWrappers
''' </summary>
Friend MustInherit Class InspectorWrapper

    ''' <summary>
    ''' Event notifier for the InspectorWrapper.Closed event.
    ''' Is raised when an Inspector has been closed.
    ''' </summary>
    Public Event Closed As InspectorWrapperClosedEventHandler

    ''' <summary>
    ''' The unique Id the identifies the Inspector Window
    ''' </summary>
    Private m_Id As Guid
    Public Property Id As Guid
        Get
            Return m_Id
        End Get
        Private Set(ByVal value As Guid)
            m_Id = value
        End Set
    End Property

    ''' <summary>
    ''' The Outlook Inspector Instance
    ''' </summary>
    Private m_Inspector As Outlook.Inspector
    Public Property Inspector As Outlook.Inspector
        Get
            Return m_Inspector
        End Get
        Private Set(ByVal value As Outlook.Inspector)
            m_Inspector = value
        End Set
    End Property

    ''' <summary>
    ''' .ctor
    ''' </summary>
    ''' <param name="inspector">The Outlook Inspector instance that should be handled</param>
    Public Sub New(ByVal inspector As Outlook.Inspector)
        Me.Id = Guid.NewGuid()
        Me.Inspector = inspector
        ' register for Inspector events here
        AddHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).Close, AddressOf Inspector_Close
        AddHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).Activate, AddressOf Activate
        AddHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).Deactivate, AddressOf Deactivate
        AddHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).BeforeMaximize, AddressOf BeforeMaximize
        AddHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).BeforeMinimize, AddressOf BeforeMinimize
        AddHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).BeforeMove, AddressOf BeforeMove
        AddHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).BeforeSize, AddressOf BeforeSize
        AddHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).PageChange, AddressOf PageChange

        ' Initialize is called to give the derived Wrappers a chance to do initialization
        Initialize()
    End Sub

    ''' <summary>
    ''' Eventhandler for the Inspector close event
    ''' </summary>
    Private Sub Inspector_Close()
        ' call the Close Method - the derived classes can implement cleanup code
        ' by overriding the Close method
        Close()
        ' unregister Inspector events
        RemoveHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).Close, AddressOf Inspector_Close
        RemoveHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).Activate, AddressOf Activate
        RemoveHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).Deactivate, AddressOf Deactivate
        RemoveHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).BeforeMaximize, AddressOf BeforeMaximize
        RemoveHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).BeforeMinimize, AddressOf BeforeMinimize
        RemoveHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).BeforeMove, AddressOf BeforeMove
        RemoveHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).BeforeSize, AddressOf BeforeSize
        RemoveHandler DirectCast(Inspector, Outlook.InspectorEvents_10_Event).PageChange, AddressOf PageChange
        ' clean up resources and do a GC.Collect();
        Inspector = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
        ' raise the Close event.
        RaiseEvent Closed(Id)
    End Sub

    ''' <summary>
    ''' Method is called after the internal initialization of the Wrapper
    ''' </summary>
    Protected Overridable Sub Initialize()
    End Sub

    ''' <summary>
    ''' Method gets called when another Page of the Inspector has been selected
    ''' </summary>
    ''' <param name="ActivePageName">The active page name by reference</param>
    Protected Overridable Sub PageChange(ByRef ActivePageName As String)
    End Sub

    ''' <summary>
    ''' Method gets called before the Inspector is resized
    ''' </summary>
    ''' <param name="Cancel">To prevent resizing set Cancel to true</param>
    Protected Overridable Sub BeforeSize(ByRef Cancel As Boolean)
    End Sub

    ''' <summary>
    ''' Method gets called before the Inspector is moved around
    ''' </summary>
    ''' <param name="Cancel">To prevent moving set Cancel to true</param>
    Protected Overridable Sub BeforeMove(ByRef Cancel As Boolean)
    End Sub

    ''' <summary>
    ''' Method gets called before the Inspector is minimized
    ''' </summary>
    ''' <param name="Cancel">To prevent minimizing set Cancel to true</param>
    Protected Overridable Sub BeforeMinimize(ByRef Cancel As Boolean)
    End Sub

    ''' <summary>
    ''' Method gets called before the Inspector is maximized
    ''' </summary>
    ''' <param name="Cancel">To prevent maximizing set Cancel to true</param>
    Protected Overridable Sub BeforeMaximize(ByRef Cancel As Boolean)
    End Sub

    ''' <summary>
    ''' Method gets called when the Inspector is deactivated
    ''' </summary>
    Protected Overridable Sub Deactivate()
    End Sub

    ''' <summary>
    ''' Method gets called when the Inspector is activated
    ''' </summary>
    Protected Overridable Sub Activate()
    End Sub

    ''' <summary>
    ''' Derived classes can do a cleanup by overriding this method.
    ''' </summary>
    Protected Overridable Sub Close()
    End Sub

    ''' <summary>
    ''' This Fabric method returns a specific InspectorWrapper or null if not handled.
    ''' </summary>
    ''' <param name="inspector">The Outlook Inspector instance</param>
    ''' <returns>Returns the specific Wrapper or null</returns>
    Public Shared Function GetWrapperFor(ByVal inspector As Outlook.Inspector) As InspectorWrapper

        ' retrieve the message class using late binding
        Dim messageClass As String = inspector.CurrentItem.MessageClass

        ' depending on a messageclass you can instantiate different Wrappers
        ' explicitely for a given MessageClass
        ' using a switch statement
        Select Case messageClass
            Case "IPM.Contact"
                Return New ContactItemWrapper(inspector)
            Case "IPM.Journal"
                Return New ContactItemWrapper(inspector)
            Case "IPM.Note"
                Return New MailItemWrapper(inspector)
            Case "IPM.Post"
                Return New PostItemWrapper(inspector)
            Case "IPM.Task"
                Return New TaskItemWrapper(inspector)
        End Select

        ' or check if the messageclass begins with a specific fragment
        If messageClass.StartsWith("IPM.Contact.X4U") Then
            Return New X4UContactItemWrapper(inspector)
        End If

        ' or check the interface type of the Item
        If TypeOf inspector.CurrentItem Is Outlook.AppointmentItem Then
            Return New AppointmentItemWrapper(inspector)
        End If

        ' no wrapper found
        Return Nothing
    End Function
End Class
