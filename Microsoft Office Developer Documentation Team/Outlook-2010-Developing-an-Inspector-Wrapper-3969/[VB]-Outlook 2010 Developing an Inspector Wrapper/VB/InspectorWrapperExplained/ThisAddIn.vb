Public Class ThisAddIn

    ''' <summary>
    ''' Holds a reference to the Application.Inspectors collection
    ''' Required to get notifications for NewInspector events.
    ''' </summary>
    Private m_inspectors As Outlook.Inspectors

    ''' <summary>
    ''' A dictionary that holds a reference to the Inspectors handled by the add-in
    ''' </summary>
    Private m_wrappedInspectors As Dictionary(Of Guid, InspectorWrapper)

    ''' <summary>
    ''' Startup method is called when the add-in is loaded by Outlook
    ''' </summary>
    Private Sub ThisAddIn_Startup() Handles Me.Startup
        m_wrappedInspectors = New Dictionary(Of Guid, InspectorWrapper)
        m_inspectors = Globals.ThisAddIn.Application.Inspectors
        AddHandler m_inspectors.NewInspector, AddressOf WrapInspector

        ' Handle also already existing Inspectors
        ' (e.g. Double clicking a .msg file)
        For Each inspector As Outlook.Inspector In m_inspectors
            WrapInspector(inspector)
        Next
    End Sub

    ''' <summary>
    ''' Wraps an Inspector if required and remember it in memory to get events of the wrapped Inspector
    ''' </summary>
    ''' <param name="inspector">The Outlook Inspector instance</param>
    Private Sub WrapInspector(ByVal inspector As Outlook.Inspector)
        Dim wrapper As InspectorWrapper = InspectorWrapper.GetWrapperFor(inspector)
        If wrapper IsNot Nothing Then
            ' register for the closed event
            AddHandler wrapper.Closed, AddressOf wrapper_Closed
            ' remember the inspector in memory
            m_wrappedInspectors(wrapper.Id) = wrapper
        End If
    End Sub

    ''' <summary>
    ''' Method is called when an inspector has been closed
    ''' Removes reference from memory
    ''' </summary>
    ''' <param name="id">The unique id of the closed inspector</param>
    Private Sub wrapper_Closed(ByVal id As Guid)
        m_wrappedInspectors.Remove(id)
    End Sub

    ''' <summary>
    ''' Shutdown method is called when Outlook is unloading the add-in
    ''' </summary>
    Private Sub ThisAddIn_Shutdown() Handles Me.Shutdown
        ' do the homework and cleanup
        m_wrappedInspectors.Clear()
        RemoveHandler m_inspectors.NewInspector, AddressOf WrapInspector
        m_inspectors = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

End Class
