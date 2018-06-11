Option Explicit On
Option Strict On

Imports System
Imports System.Security.Permissions
Imports Microsoft.SharePoint
Imports Microsoft.SharePoint.Security
Imports Microsoft.SharePoint.Utilities
Imports Microsoft.SharePoint.Workflow

Public Class EventReceiver1 
    Inherits SPItemEventReceiver

	''' <summary>
	''' An item is being added.
	''' </summary>
    Public Overrides Sub ItemAdding(ByVal properties As SPItemEventProperties)
        Try
            Dim allowed As Boolean = True

            If properties.ListTitle = "Open Positions" Then
                allowed = CheckItem(properties)
            End If

            If allowed = False Then
                properties.Status = SPEventReceiverStatus.CancelWithError
                properties.ErrorMessage = "The job you have entered is not defined in the Job Definitions List"
                properties.Cancel = True
            End If

        Catch ex As Exception
            properties.Status = SPEventReceiverStatus.CancelWithError
            properties.ErrorMessage = ex.Message
            properties.Cancel = True
        End Try
    End Sub


	''' <summary>
	''' An item is being updated.
	''' </summary>
    Public Overrides Sub ItemUpdating(ByVal properties As SPItemEventProperties)
        Try
            Dim allowed As Boolean = True

            If properties.ListTitle = "Open Positions" Then
                allowed = CheckItem(properties)
            End If

            If allowed = False Then
                properties.Status = SPEventReceiverStatus.CancelWithError
                properties.ErrorMessage = "The job you have entered is not defined in the Job Definitions List"
                properties.Cancel = True
            End If

        Catch ex As Exception
            properties.Status = SPEventReceiverStatus.CancelWithError
            properties.ErrorMessage = ex.Message
            properties.Cancel = True

        End Try
    End Sub


    Public Function CheckItem(ByVal properties As SPItemEventProperties) As Boolean

        Dim jobTitle As String = properties.AfterProperties("Title").ToString()
        Dim allowed As Boolean = False
        Dim jobDefWeb As SPWeb = Nothing
        Dim jobDefList As SPList
        Dim privilegedAccount As SPUser = properties.Web.AllUsers("SHAREPOINT\SYSTEM")
        Dim privilegedToken As SPUserToken = privilegedAccount.UserToken

        Try
            Using elevatedSite As New SPSite(properties.Web.Url, privilegedToken)
                Using elevatedWeb As SPWeb = elevatedSite.OpenWeb()
                    jobDefWeb = elevatedWeb.Webs("JobDefinitions")
                    jobDefList = jobDefWeb.Lists("Job Definitions")

                    For Each item As SPListItem In jobDefList.Items
                        If item("Title").ToString() = jobTitle Then
                            allowed = True
                            Exit For
                        End If
                    Next

                End Using
            End Using

            Return (allowed)

        Finally
            jobDefWeb.Dispose()
        End Try
    End Function

End Class
