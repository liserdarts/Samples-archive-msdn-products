<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DisplayAccountInfo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.scAccountInfo = New System.Windows.Forms.SplitContainer()
        Me.treeAccounts = New System.Windows.Forms.TreeView()
        Me.listProperties = New System.Windows.Forms.ListView()
        Me.chProp = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.scAccountInfo.Panel1.SuspendLayout()
        Me.scAccountInfo.Panel2.SuspendLayout()
        Me.scAccountInfo.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'scAccountInfo
        '
        Me.scAccountInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scAccountInfo.Location = New System.Drawing.Point(0, 0)
        Me.scAccountInfo.Name = "scAccountInfo"
        '
        'scAccountInfo.Panel1
        '
        Me.scAccountInfo.Panel1.Controls.Add(Me.treeAccounts)
        '
        'scAccountInfo.Panel2
        '
        Me.scAccountInfo.Panel2.Controls.Add(Me.listProperties)
        Me.scAccountInfo.Size = New System.Drawing.Size(502, 339)
        Me.scAccountInfo.SplitterDistance = 167
        Me.scAccountInfo.TabIndex = 0
        '
        'treeAccounts
        '
        Me.treeAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeAccounts.Location = New System.Drawing.Point(0, 0)
        Me.treeAccounts.Name = "treeAccounts"
        Me.treeAccounts.Size = New System.Drawing.Size(167, 339)
        Me.treeAccounts.TabIndex = 0
        '
        'listProperties
        '
        Me.listProperties.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chProp, Me.chValue})
        Me.listProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listProperties.Location = New System.Drawing.Point(0, 0)
        Me.listProperties.Name = "listProperties"
        Me.listProperties.Size = New System.Drawing.Size(331, 339)
        Me.listProperties.TabIndex = 0
        Me.listProperties.UseCompatibleStateImageBehavior = False
        Me.listProperties.View = System.Windows.Forms.View.Details
        '
        'chProp
        '
        Me.chProp.Text = "Property Name"
        '
        'chValue
        '
        Me.chValue.Text = "Property Value"
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.Location = New System.Drawing.Point(0, 302)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(502, 37)
        Me.pnlButtons.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnClose.Location = New System.Drawing.Point(424, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'DisplayAccountInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 339)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.scAccountInfo)
        Me.Name = "DisplayAccountInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DisplayAccountInfo"
        Me.scAccountInfo.Panel1.ResumeLayout(False)
        Me.scAccountInfo.Panel2.ResumeLayout(False)
        Me.scAccountInfo.ResumeLayout(False)
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents scAccountInfo As System.Windows.Forms.SplitContainer
    Friend WithEvents treeAccounts As System.Windows.Forms.TreeView
    Friend WithEvents listProperties As System.Windows.Forms.ListView
    Friend WithEvents chProp As System.Windows.Forms.ColumnHeader
    Friend WithEvents chValue As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
