Imports System.Windows.Forms

Public Class DisplayAccountInfo
    Public Sub AddAccount(ByVal acct As Outlook.Account)
        Dim node As TreeNode
        node = treeAccounts.Nodes.Add(acct.DisplayName)
        node.Tag = acct
    End Sub

    Private Sub treeAccounts_AfterSelect(
      ByVal sender As System.Object,
      ByVal e As System.Windows.Forms.TreeViewEventArgs) _
      Handles treeAccounts.AfterSelect

        listProperties.Items.Clear()
        Dim exAcct As Outlook.Account =
            TryCast(e.Node.Tag, Outlook.Account)
        If exAcct Is Nothing Then
            listProperties.Items.Add("Account Object Unavailable.")

        Else
            AddNewItem("AccountName",
              System.Enum.GetName(GetType(Outlook.OlAccountType),
                                  exAcct.AccountType))
            AddNewItem("AutoDiscoverConnectionMode",
              System.Enum.GetName(GetType(Outlook.OlAutoDiscoverConnectionMode),
                                  exAcct.AutoDiscoverConnectionMode))
            AddNewItem("AutoDiscoverXml", exAcct.AutoDiscoverXml)
            AddNewItem("CurrentUser.Name", exAcct.CurrentUser.Name)
            AddNewItem("DeliveryStore.DisplayName",
                       exAcct.DeliveryStore.DisplayName)
            AddNewItem("ExchangeConnectionMode",
              System.Enum.GetName(GetType(Outlook.OlExchangeConnectionMode),
                                  exAcct.ExchangeConnectionMode))
            AddNewItem("ExchangeMailboxServerName",
                       exAcct.ExchangeMailboxServerName)
            AddNewItem("ExchangeMailboxServerVersion",
                       exAcct.ExchangeMailboxServerVersion)
        End If
    End Sub
    Private Sub AddNewItem(ByVal PropName As String,
                           ByVal PropValue As String)
        Dim lvItem As ListViewItem =
            listProperties.Items.Add(PropName)
        lvItem.SubItems.Add(PropValue)
    End Sub
End Class