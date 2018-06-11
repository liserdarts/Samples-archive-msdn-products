Imports Microsoft.Office.Tools.Ribbon
Imports System.Windows.Forms

Public Class MainRibbon

  Private Sub Main_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load

  End Sub

  Private Sub btnGetInfo_Click(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles btnGetInfo.Click
    Try
      Dim host As Outlook.Application = Globals.ThisAddIn.Application
      ' Check for Outlook 2010
      If host.Version.Substring(0, 2) = "14" Then
        Dim results = From exAccts As Outlook.Account In host.Session.Accounts
                  Where exAccts.AccountType = Outlook.OlAccountType.olExchange
                  Select exAccts

        If results.Count > 0 Then
          Using frm As New DisplayAccountInfo
            For Each exAcct As Outlook.Account In results
              frm.AddAccount(exAcct)
            Next
            frm.ShowDialog()
          End Using
        Else
          MessageBox.Show(
              "No Exchange Accounts found in current profile.",
              "Information",
              MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
      Else
        MessageBox.Show(
              "You need Outlook 2010 to run this add-in.",
              "Warning",
              MessageBoxButtons.OK, MessageBoxIcon.Warning)
      End If
    Catch ex As Exception
      Dim errorText As String =
          String.Format(
              "An unexpected error occurred.{0}Error details:{0}{1}",
              Environment.NewLine, ex.Message)
      MessageBox.Show(
          errorText,
          ex.Source,
          MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

  Private Sub btnCreateMail_Click(ByVal sender As System.Object, ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) Handles btnCreateMail.Click
    Try
      Dim host As Outlook.Application = Globals.ThisAddIn.Application
      Dim fld As Outlook.Folder =
        TryCast(host.ActiveExplorer.CurrentFolder, Outlook.Folder)
      Dim str As Outlook.Store = fld.Store

      Dim results = From exAccts As Outlook.Account
        In host.Session.Accounts
        Where exAccts.DeliveryStore.StoreID = str.StoreID
        Select exAccts

      If results.Count > 0 Then
        Dim ae As Outlook.AddressEntry =
          results(0).CurrentUser.AddressEntry
        Dim mail As Outlook.MailItem =
        host.CreateItem(Outlook.OlItemType.olMailItem)

        If ae IsNot Nothing Then
          mail.Sender = ae
        End If
        mail.Subject = "E-mail created by add-in"
        mail.BodyFormat = Outlook.OlBodyFormat.olFormatPlain
        mail.Body = "Having multiple Exchange accounts is great!"
        mail.Display(False)
      Else
        MessageBox.Show(
        "Could not match account to active session.",
        "Information",
        MessageBoxButtons.OK, MessageBoxIcon.Information)
      End If

    Catch ex As Exception
      Dim errorText As String =
          String.Format(
              "An unexpected error occurred.{0}Error details:{0}{1}",
              Environment.NewLine, ex.Message)
      MessageBox.Show(
          errorText,
          ex.Source,
          MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub
  Sub foo()
    Try

    Catch ex As Exception
      Dim errorText As String =
          String.Format(
              "An unexpected error occurred.{0}Error details:{0}{1}",
              Environment.NewLine, ex.Message)
      MessageBox.Show(
          errorText,
          ex.Source,
          MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Try
  End Sub
End Class
