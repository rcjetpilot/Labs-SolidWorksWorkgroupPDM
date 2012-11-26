<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PluginSettingsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PluginSettingsForm))
        Me.VaultNameLabel = New System.Windows.Forms.Label()
        Me.UserNameLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.VaultText = New System.Windows.Forms.TextBox()
        Me.UserNameText = New System.Windows.Forms.TextBox()
        Me.PasswordText = New System.Windows.Forms.TextBox()
        Me.BodyLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.SilentLabel = New System.Windows.Forms.Label()
        Me.RequestPortNum = New System.Windows.Forms.NumericUpDown()
        Me.SpecFilesCheck = New System.Windows.Forms.CheckBox()
        Me.ModelFilesCheck = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LoggingVerbosity = New System.Windows.Forms.ComboBox()
        Me.RequestPortLabel = New System.Windows.Forms.Label()
        Me.DataPortLabel = New System.Windows.Forms.Label()
        Me.TopLevelProjectLabel = New System.Windows.Forms.Label()
        Me.TopLevelProjectText = New System.Windows.Forms.TextBox()
        Me.DataPortNum = New System.Windows.Forms.NumericUpDown()
        Me.BodyPanel = New System.Windows.Forms.Panel()
        Me.FinishButton = New System.Windows.Forms.Button()
        Me.DlgCancelButton = New System.Windows.Forms.Button()
        Me.HeaderPanel = New DriveWorksLabs.SolidWorksWorkgroupPdm.HeaderPanel()
        Me.HeaderLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.DescriptionLabel = New System.Windows.Forms.Label()
        Me.HeaderLabel = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.FooterPanel = New DriveWorksLabs.SolidWorksWorkgroupPdm.FooterPanel()
        Me.FooterLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TestButton = New System.Windows.Forms.Button()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.BodyLayoutPanel.SuspendLayout()
        CType(Me.RequestPortNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataPortNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BodyPanel.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        Me.HeaderLayoutPanel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FooterPanel.SuspendLayout()
        Me.FooterLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'VaultNameLabel
        '
        Me.VaultNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.VaultNameLabel.AutoSize = True
        Me.VaultNameLabel.Location = New System.Drawing.Point(5, 8)
        Me.VaultNameLabel.Name = "VaultNameLabel"
        Me.VaultNameLabel.Size = New System.Drawing.Size(65, 13)
        Me.VaultNameLabel.TabIndex = 2
        Me.VaultNameLabel.Text = "Vault Name:"
        '
        'UserNameLabel
        '
        Me.UserNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.UserNameLabel.AutoSize = True
        Me.UserNameLabel.Location = New System.Drawing.Point(5, 34)
        Me.UserNameLabel.Name = "UserNameLabel"
        Me.UserNameLabel.Size = New System.Drawing.Size(63, 13)
        Me.UserNameLabel.TabIndex = 3
        Me.UserNameLabel.Text = "User Name:"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.Location = New System.Drawing.Point(5, 60)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(56, 13)
        Me.PasswordLabel.TabIndex = 4
        Me.PasswordLabel.Text = "Password:"
        '
        'VaultText
        '
        Me.VaultText.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.VaultText.Location = New System.Drawing.Point(136, 5)
        Me.VaultText.Name = "VaultText"
        Me.VaultText.Size = New System.Drawing.Size(214, 20)
        Me.VaultText.TabIndex = 0
        '
        'UserNameText
        '
        Me.UserNameText.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.UserNameText.Location = New System.Drawing.Point(136, 31)
        Me.UserNameText.Name = "UserNameText"
        Me.UserNameText.Size = New System.Drawing.Size(214, 20)
        Me.UserNameText.TabIndex = 1
        '
        'PasswordText
        '
        Me.PasswordText.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.PasswordText.Location = New System.Drawing.Point(136, 57)
        Me.PasswordText.Name = "PasswordText"
        Me.PasswordText.Size = New System.Drawing.Size(214, 20)
        Me.PasswordText.TabIndex = 2
        Me.PasswordText.UseSystemPasswordChar = True
        '
        'BodyLayoutPanel
        '
        Me.BodyLayoutPanel.ColumnCount = 2
        Me.BodyLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.BodyLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.BodyLayoutPanel.Controls.Add(Me.SilentLabel, 0, 5)
        Me.BodyLayoutPanel.Controls.Add(Me.VaultText, 1, 0)
        Me.BodyLayoutPanel.Controls.Add(Me.PasswordLabel, 0, 2)
        Me.BodyLayoutPanel.Controls.Add(Me.RequestPortNum, 1, 9)
        Me.BodyLayoutPanel.Controls.Add(Me.PasswordText, 1, 2)
        Me.BodyLayoutPanel.Controls.Add(Me.UserNameLabel, 0, 1)
        Me.BodyLayoutPanel.Controls.Add(Me.UserNameText, 1, 1)
        Me.BodyLayoutPanel.Controls.Add(Me.VaultNameLabel, 0, 0)
        Me.BodyLayoutPanel.Controls.Add(Me.SpecFilesCheck, 1, 5)
        Me.BodyLayoutPanel.Controls.Add(Me.ModelFilesCheck, 1, 4)
        Me.BodyLayoutPanel.Controls.Add(Me.Label1, 0, 4)
        Me.BodyLayoutPanel.Controls.Add(Me.Label2, 0, 7)
        Me.BodyLayoutPanel.Controls.Add(Me.LoggingVerbosity, 1, 7)
        Me.BodyLayoutPanel.Controls.Add(Me.RequestPortLabel, 0, 9)
        Me.BodyLayoutPanel.Controls.Add(Me.DataPortLabel, 0, 10)
        Me.BodyLayoutPanel.Controls.Add(Me.TopLevelProjectLabel, 0, 3)
        Me.BodyLayoutPanel.Controls.Add(Me.TopLevelProjectText, 1, 3)
        Me.BodyLayoutPanel.Controls.Add(Me.DataPortNum, 1, 10)
        Me.BodyLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BodyLayoutPanel.Location = New System.Drawing.Point(8, 8)
        Me.BodyLayoutPanel.Name = "BodyLayoutPanel"
        Me.BodyLayoutPanel.Padding = New System.Windows.Forms.Padding(2)
        Me.BodyLayoutPanel.RowCount = 11
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12.0!))
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12.0!))
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.BodyLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.BodyLayoutPanel.Size = New System.Drawing.Size(575, 252)
        Me.BodyLayoutPanel.TabIndex = 8
        '
        'SilentLabel
        '
        Me.SilentLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.SilentLabel.AutoSize = True
        Me.SilentLabel.Location = New System.Drawing.Point(5, 129)
        Me.SilentLabel.Name = "SilentLabel"
        Me.SilentLabel.Size = New System.Drawing.Size(106, 13)
        Me.SilentLabel.TabIndex = 8
        Me.SilentLabel.Text = "Check Documents in"
        '
        'RequestPortNum
        '
        Me.RequestPortNum.Location = New System.Drawing.Point(136, 200)
        Me.RequestPortNum.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.RequestPortNum.Name = "RequestPortNum"
        Me.RequestPortNum.Size = New System.Drawing.Size(164, 20)
        Me.RequestPortNum.TabIndex = 3
        Me.RequestPortNum.ThousandsSeparator = True
        '
        'SpecFilesCheck
        '
        Me.SpecFilesCheck.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.SpecFilesCheck.AutoSize = True
        Me.SpecFilesCheck.Location = New System.Drawing.Point(136, 129)
        Me.SpecFilesCheck.Name = "SpecFilesCheck"
        Me.SpecFilesCheck.Size = New System.Drawing.Size(15, 14)
        Me.SpecFilesCheck.TabIndex = 3
        Me.SpecFilesCheck.UseVisualStyleBackColor = True
        '
        'ModelFilesCheck
        '
        Me.ModelFilesCheck.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ModelFilesCheck.AutoSize = True
        Me.ModelFilesCheck.Checked = True
        Me.ModelFilesCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ModelFilesCheck.Location = New System.Drawing.Point(136, 109)
        Me.ModelFilesCheck.Name = "ModelFilesCheck"
        Me.ModelFilesCheck.Size = New System.Drawing.Size(15, 14)
        Me.ModelFilesCheck.TabIndex = 3
        Me.ModelFilesCheck.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Check Models in"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 165)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Logging Verbosity"
        '
        'LoggingVerbosity
        '
        Me.LoggingVerbosity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LoggingVerbosity.FormattingEnabled = True
        Me.LoggingVerbosity.Items.AddRange(New Object() {"None", "General", "Diagnostic"})
        Me.LoggingVerbosity.Location = New System.Drawing.Point(136, 161)
        Me.LoggingVerbosity.Name = "LoggingVerbosity"
        Me.LoggingVerbosity.Size = New System.Drawing.Size(214, 21)
        Me.LoggingVerbosity.TabIndex = 9
        '
        'RequestPortLabel
        '
        Me.RequestPortLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RequestPortLabel.AutoSize = True
        Me.RequestPortLabel.Location = New System.Drawing.Point(5, 203)
        Me.RequestPortLabel.Name = "RequestPortLabel"
        Me.RequestPortLabel.Size = New System.Drawing.Size(72, 13)
        Me.RequestPortLabel.TabIndex = 4
        Me.RequestPortLabel.Text = "Request Port:"
        '
        'DataPortLabel
        '
        Me.DataPortLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DataPortLabel.AutoSize = True
        Me.DataPortLabel.Location = New System.Drawing.Point(5, 230)
        Me.DataPortLabel.Name = "DataPortLabel"
        Me.DataPortLabel.Size = New System.Drawing.Size(55, 13)
        Me.DataPortLabel.TabIndex = 4
        Me.DataPortLabel.Text = "Data Port:"
        '
        'TopLevelProjectLabel
        '
        Me.TopLevelProjectLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TopLevelProjectLabel.AutoSize = True
        Me.TopLevelProjectLabel.Location = New System.Drawing.Point(5, 86)
        Me.TopLevelProjectLabel.Name = "TopLevelProjectLabel"
        Me.TopLevelProjectLabel.Size = New System.Drawing.Size(125, 13)
        Me.TopLevelProjectLabel.TabIndex = 4
        Me.TopLevelProjectLabel.Text = "Top Level Project Name:"
        '
        'TopLevelProjectText
        '
        Me.TopLevelProjectText.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TopLevelProjectText.Location = New System.Drawing.Point(136, 83)
        Me.TopLevelProjectText.Name = "TopLevelProjectText"
        Me.TopLevelProjectText.Size = New System.Drawing.Size(214, 20)
        Me.TopLevelProjectText.TabIndex = 2
        '
        'DataPortNum
        '
        Me.DataPortNum.Location = New System.Drawing.Point(136, 226)
        Me.DataPortNum.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DataPortNum.Name = "DataPortNum"
        Me.DataPortNum.Size = New System.Drawing.Size(164, 20)
        Me.DataPortNum.TabIndex = 3
        Me.DataPortNum.ThousandsSeparator = True
        '
        'BodyPanel
        '
        Me.BodyPanel.Controls.Add(Me.BodyLayoutPanel)
        Me.BodyPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BodyPanel.Location = New System.Drawing.Point(0, 84)
        Me.BodyPanel.Name = "BodyPanel"
        Me.BodyPanel.Padding = New System.Windows.Forms.Padding(8)
        Me.BodyPanel.Size = New System.Drawing.Size(591, 268)
        Me.BodyPanel.TabIndex = 8
        '
        'FinishButton
        '
        Me.FinishButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.FinishButton.Location = New System.Drawing.Point(513, 3)
        Me.FinishButton.Name = "FinishButton"
        Me.FinishButton.Size = New System.Drawing.Size(75, 25)
        Me.FinishButton.TabIndex = 1
        Me.FinishButton.Text = "&Finish"
        Me.FinishButton.UseVisualStyleBackColor = True
        '
        'DlgCancelButton
        '
        Me.DlgCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.DlgCancelButton.Location = New System.Drawing.Point(432, 3)
        Me.DlgCancelButton.Name = "DlgCancelButton"
        Me.DlgCancelButton.Size = New System.Drawing.Size(75, 25)
        Me.DlgCancelButton.TabIndex = 2
        Me.DlgCancelButton.Text = "&Cancel"
        Me.DlgCancelButton.UseVisualStyleBackColor = True
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackColor = System.Drawing.Color.White
        Me.HeaderPanel.Controls.Add(Me.HeaderLayoutPanel)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Padding = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.HeaderPanel.Size = New System.Drawing.Size(591, 84)
        Me.HeaderPanel.TabIndex = 1
        '
        'HeaderLayoutPanel
        '
        Me.HeaderLayoutPanel.ColumnCount = 2
        Me.HeaderLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HeaderLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.HeaderLayoutPanel.Controls.Add(Me.DescriptionLabel, 0, 1)
        Me.HeaderLayoutPanel.Controls.Add(Me.HeaderLabel, 0, 0)
        Me.HeaderLayoutPanel.Controls.Add(Me.PictureBox1, 1, 0)
        Me.HeaderLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HeaderLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderLayoutPanel.Name = "HeaderLayoutPanel"
        Me.HeaderLayoutPanel.RowCount = 2
        Me.HeaderLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.HeaderLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.HeaderLayoutPanel.Size = New System.Drawing.Size(591, 82)
        Me.HeaderLayoutPanel.TabIndex = 2
        '
        'DescriptionLabel
        '
        Me.DescriptionLabel.AutoSize = True
        Me.DescriptionLabel.Location = New System.Drawing.Point(3, 29)
        Me.DescriptionLabel.Name = "DescriptionLabel"
        Me.DescriptionLabel.Padding = New System.Windows.Forms.Padding(8)
        Me.DescriptionLabel.Size = New System.Drawing.Size(380, 29)
        Me.DescriptionLabel.TabIndex = 2
        Me.DescriptionLabel.Text = "Configure and test the settings used by DriveWorks to connect to your vault"
        '
        'HeaderLabel
        '
        Me.HeaderLabel.AutoSize = True
        Me.HeaderLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HeaderLabel.Location = New System.Drawing.Point(3, 0)
        Me.HeaderLabel.Name = "HeaderLabel"
        Me.HeaderLabel.Padding = New System.Windows.Forms.Padding(8)
        Me.HeaderLabel.Size = New System.Drawing.Size(328, 29)
        Me.HeaderLabel.TabIndex = 1
        Me.HeaderLabel.Text = "SolidWorks Workgroup PDM Integration Configuration"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(519, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.HeaderLayoutPanel.SetRowSpan(Me.PictureBox1, 2)
        Me.PictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'FooterPanel
        '
        Me.FooterPanel.Controls.Add(Me.FooterLayoutPanel)
        Me.FooterPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.FooterPanel.Location = New System.Drawing.Point(0, 352)
        Me.FooterPanel.Name = "FooterPanel"
        Me.FooterPanel.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.FooterPanel.Size = New System.Drawing.Size(591, 33)
        Me.FooterPanel.TabIndex = 0
        '
        'FooterLayoutPanel
        '
        Me.FooterLayoutPanel.ColumnCount = 4
        Me.FooterLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.FooterLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.FooterLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.FooterLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.FooterLayoutPanel.Controls.Add(Me.TestButton, 0, 0)
        Me.FooterLayoutPanel.Controls.Add(Me.FinishButton, 3, 0)
        Me.FooterLayoutPanel.Controls.Add(Me.DlgCancelButton, 2, 0)
        Me.FooterLayoutPanel.Controls.Add(Me.LabelVersion, 1, 0)
        Me.FooterLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FooterLayoutPanel.Location = New System.Drawing.Point(0, 2)
        Me.FooterLayoutPanel.Name = "FooterLayoutPanel"
        Me.FooterLayoutPanel.RowCount = 1
        Me.FooterLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.FooterLayoutPanel.Size = New System.Drawing.Size(591, 31)
        Me.FooterLayoutPanel.TabIndex = 8
        '
        'TestButton
        '
        Me.TestButton.Location = New System.Drawing.Point(3, 3)
        Me.TestButton.Name = "TestButton"
        Me.TestButton.Size = New System.Drawing.Size(75, 25)
        Me.TestButton.TabIndex = 0
        Me.TestButton.Text = "&Test"
        Me.TestButton.UseVisualStyleBackColor = True
        '
        'LabelVersion
        '
        Me.LabelVersion.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Location = New System.Drawing.Point(199, 9)
        Me.LabelVersion.Margin = New System.Windows.Forms.Padding(3)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(227, 13)
        Me.LabelVersion.TabIndex = 3
        Me.LabelVersion.Text = "_VERSION_NUMBER_AUTO_POPULATED_"
        '
        'PluginSettingsForm
        '
        Me.AcceptButton = Me.FinishButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.DlgCancelButton
        Me.ClientSize = New System.Drawing.Size(591, 385)
        Me.Controls.Add(Me.BodyPanel)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Controls.Add(Me.FooterPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PluginSettingsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SolidWorks Workgroup PDM Integration Configuration"
        Me.BodyLayoutPanel.ResumeLayout(False)
        Me.BodyLayoutPanel.PerformLayout()
        CType(Me.RequestPortNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataPortNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BodyPanel.ResumeLayout(False)
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderLayoutPanel.ResumeLayout(False)
        Me.HeaderLayoutPanel.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FooterPanel.ResumeLayout(False)
        Me.FooterLayoutPanel.ResumeLayout(False)
        Me.FooterLayoutPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FooterPanel As FooterPanel
    Friend WithEvents HeaderPanel As HeaderPanel
    Friend WithEvents FinishButton As System.Windows.Forms.Button
    Friend WithEvents DlgCancelButton As System.Windows.Forms.Button
    Friend WithEvents TestButton As System.Windows.Forms.Button
    Friend WithEvents HeaderLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents HeaderLabel As System.Windows.Forms.Label
    Friend WithEvents VaultNameLabel As System.Windows.Forms.Label
    Friend WithEvents UserNameLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents VaultText As System.Windows.Forms.TextBox
    Friend WithEvents UserNameText As System.Windows.Forms.TextBox
    Friend WithEvents PasswordText As System.Windows.Forms.TextBox
    Friend WithEvents BodyLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BodyPanel As System.Windows.Forms.Panel
    Friend WithEvents FooterLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents SilentLabel As System.Windows.Forms.Label
    Friend WithEvents SpecFilesCheck As System.Windows.Forms.CheckBox
    Friend WithEvents ModelFilesCheck As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LoggingVerbosity As System.Windows.Forms.ComboBox
    Friend WithEvents RequestPortLabel As System.Windows.Forms.Label
    Friend WithEvents DataPortLabel As System.Windows.Forms.Label
    Friend WithEvents TopLevelProjectLabel As System.Windows.Forms.Label
    Friend WithEvents TopLevelProjectText As System.Windows.Forms.TextBox
    Friend WithEvents RequestPortNum As System.Windows.Forms.NumericUpDown
    Friend WithEvents DataPortNum As System.Windows.Forms.NumericUpDown
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
End Class
