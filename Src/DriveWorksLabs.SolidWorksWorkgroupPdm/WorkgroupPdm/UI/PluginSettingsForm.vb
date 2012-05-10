' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports PDMWorks.Interop.pdmworks

Friend Class PluginSettingsForm

    Public Sub LoadSettings(ByVal settings As PluginSettings)
        Me.VaultText.Text = settings.VaultName
        Me.UserNameText.Text = settings.VaultUserName
        Me.PasswordText.Text = settings.VaultPassword
        Me.TopLevelProjectText.Text = settings.TopLevelProjectName
        Me.LoggingVerbosity.SelectedIndex = CInt(settings.LoggingVerbosity)
        Me.ModelFilesCheck.Checked = settings.CheckInModels
        Me.SpecFilesCheck.Checked = settings.CheckInSpecifications
        Me.RequestPortNum.Value = settings.RequestPort
        Me.DataPortNum.Value = settings.DataPort
    End Sub

    Public Sub SaveSettings(ByVal settings As PluginSettings)
        settings.VaultName = Me.VaultText.Text
        settings.VaultUserName = Me.UserNameText.Text
        settings.VaultPassword = Me.PasswordText.Text
        settings.TopLevelProjectName = Me.TopLevelProjectText.Text
        settings.LoggingVerbosity = DirectCast(Me.LoggingVerbosity.SelectedIndex, LoggingLevel)

        settings.RequestPort = CInt(Me.RequestPortNum.Value)
        settings.DataPort = CInt(Me.DataPortNum.Value)

        settings.CheckInModels = Me.ModelFilesCheck.Checked
        settings.CheckInSpecifications = Me.SpecFilesCheck.Checked
    End Sub

    Private Sub TestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestButton.Click

        ' Attempt a connection to the vault
        Dim vault As PDMWConnection

        Try
            vault = New PDMWConnection()
        Catch ex As COMException
            MessageBox.Show(LocalizedResources.LogPDMCannotCreateInterface, LocalizedResources.TestTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            TestCore(vault)
        Finally
            Marshal.FinalReleaseComObject(vault)
            vault = Nothing
        End Try
    End Sub

    Private Sub TestCore(ByVal vault As PDMWConnection)
        Dim loginResult As Integer

        ' Logon attempt 1 - using the user-selected port information
        Try

            loginResult = vault.Login(Me.UserNameText.Text, Me.PasswordText.Text, Me.VaultText.Text, CInt(Me.RequestPortNum.Value), CInt(Me.DataPortNum.Value))

        Catch loginAttempt1Ex As COMException

            ' Let the user know in failed on the first attempt using their port numbers
            MessageBox.Show(String.Format(LocalizedResources.LogVaultLoginAttempt1Failed, loginAttempt1Ex.Message), LocalizedResources.TestTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Try

                ' Try logging on again with the default ports
                loginResult = vault.Login(Me.UserNameText.Text, Me.PasswordText.Text, Me.VaultText.Text)

            Catch loginAttempt2Ex As COMException

                ' Let the user know that the second attempt using default ports didn't work
                MessageBox.Show(String.Format(LocalizedResources.LogVaultLoginAttempt2Failed, loginAttempt1Ex.Message), LocalizedResources.TestTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
        End Try

        ' Login failed?
        If loginResult <> 0 Then
            MessageBox.Show(String.Format(LocalizedResources.LogvaultLoginNonZeroErrorCode, loginResult), LocalizedResources.TestTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        ' Success!
        vault.Logout()
        MessageBox.Show(LocalizedResources.TestSucceeded, LocalizedResources.TestTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class