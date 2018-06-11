Imports Microsoft.Office.Tools.Ribbon
Imports System.Drawing

Public Class SolutionModuleRibbon
  Private Sub SolutionModuleRibbon_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load

  End Sub

  Private Sub btnCreateSM_Click(
    ByVal sender As System.Object,
    ByVal e As Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs) _
    Handles btnCreateSM.Click

    Try
      Const DISPLAY_POS As Integer = 2

      Dim app As Outlook.Application =
        Globals.ThisAddIn.Application
      Dim exp As Outlook.Explorer =
        app.ActiveExplorer()
      Dim np As Outlook.NavigationPane =
        exp.NavigationPane
      Dim sm As Outlook.SolutionsModule =
        TryCast(np.Modules.GetNavigationModule(
            Outlook.OlNavigationModuleType.olModuleSolutions), 
          Outlook.SolutionsModule)

      Dim solutionRoot As Outlook.Folder = Nothing
      Dim solutionCalendar As Outlook.Folder = Nothing
      Dim solutionTasks As Outlook.Folder = Nothing

      Dim rootStoreFolder As Outlook.Folder =
        app.Session.DefaultStore.GetRootFolder()

      Dim results = From aFolder As Outlook.Folder
                    In rootStoreFolder.Folders
                    Where aFolder.Name = "Custom App"
                    Select aFolder

      If results.Count > 0 Then
        solutionRoot = results(0)

        solutionCalendar = TryCast(
          solutionRoot.Folders("Custom Calendar"), 
          Outlook.Folder)
        solutionTasks = TryCast(
          solutionRoot.Folders("Custom Tasks"), 
          Outlook.Folder)

      Else
        solutionRoot = TryCast(rootStoreFolder.Folders.Add(
            "Custom App",
            Outlook.OlDefaultFolders.olFolderInbox), 
          Outlook.Folder)

        solutionCalendar = TryCast(solutionRoot.Folders.Add(
            "Custom Calendar",
            Outlook.OlDefaultFolders.olFolderCalendar), 
          Outlook.Folder)
        solutionTasks = TryCast(solutionRoot.Folders.Add(
            "Custom Tasks",
            Outlook.OlDefaultFolders.olFolderTasks), 
          Outlook.Folder)
      End If

      sm.AddSolution(solutionRoot,
        Outlook.OlSolutionScope.olHideInDefaultModules)

      Dim rootIcon As Icon =
        New Icon([GetType](), "Globe.ico")
      Dim calIcon As Icon =
        New Icon([GetType](), "005_Task.ico")
      Dim taskIcon As Icon =
        New Icon([GetType](), "008_Reminder.ico")

      solutionRoot.SetCustomIcon(
          PictureDispConverter.ToIPictureDisp(rootIcon))
      solutionCalendar.SetCustomIcon(
        PictureDispConverter.ToIPictureDisp(calIcon))
      solutionTasks.SetCustomIcon(
        PictureDispConverter.ToIPictureDisp(taskIcon))

      If Not sm.Visible Then
        sm.Visible = True
      End If
      If Not sm.Position = DISPLAY_POS Then
        sm.Position = DISPLAY_POS
      End If
      If Not np.DisplayedModuleCount = DISPLAY_POS Then
        np.DisplayedModuleCount = DISPLAY_POS
      End If
    Catch ex As Exception
      MsgBox(ex.Message, MsgBoxStyle.Critical, ex.Source)
    End Try
  End Sub
End Class