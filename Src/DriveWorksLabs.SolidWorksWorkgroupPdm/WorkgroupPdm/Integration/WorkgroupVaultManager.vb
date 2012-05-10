' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System.IO
Imports System.Runtime.InteropServices
Imports DriveWorks.Applications
Imports PDMWorks.Interop.pdmworks

Friend Class WorkgroupVaultManager
    Private mLog As Logger
    Private mVault As PDMWConnection
    Private mLoggedOn As Boolean = False

    Public Sub New(ByVal log As Logger)
        mLog = log
    End Sub

    ''' <summary>
    ''' Gets or sets the name of the user to log in to the vault as.
    ''' </summary>
    Public Property VaultUserName As String

    ''' <summary>
    ''' Gets or sets the password of the user to log in to the vault as.
    ''' </summary>
    Public Property VaultPassword As String

    ''' <summary>
    ''' Gets or sets the name of the vault to log in to.
    ''' </summary>
    Public Property VaultName As String

    ''' <summary>
    ''' Gets or sets the number of the request port to use to connect to the vault.
    ''' </summary>
    Public Property RequestPort As Integer

    ''' <summary>
    ''' Gets or sets the number of the data port to use to connect to the vault.
    ''' </summary>
    Public Property DataPort As Integer

    ''' <summary>
    ''' Determines whether the vault manager is enabled.
    ''' </summary>
    Public Function IsEnabled() As Boolean
        Return Not String.IsNullOrEmpty(Me.VaultName)
    End Function

    ''' <summary>
    ''' Gets a logged-in connection to the vault, otherwise if WPDM isn't installed,
    ''' or log in fails, returns a null reference.
    ''' </summary>
    Public Function GetVault() As PDMWConnection
        EnsureVault()
        Return mVault
    End Function

    ''' <summary>
    ''' Closes the connection to the vault - does not log off first.
    ''' </summary>
    Public Sub Close()

        ' If we are already connected to WorkgroupPDM, ditch
        ' our connection now
        If mVault IsNot Nothing Then
            Marshal.FinalReleaseComObject(mVault)
            mVault = Nothing
        End If

        ' Create a new vault object to interact with 
        mLoggedOn = False
    End Sub

    ''' <summary>
    ''' Logs out of the vault.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Logout()
        If Not mLoggedOn Then
            Return
        End If

        Try
            mVault.Logout()
        Catch
            ' May fail if the vault is already logged
            ' out but we're unaware
        End Try

        mLoggedOn = False
    End Sub

    ''' <summary>
    ''' Updates the PATH environment variable to include the path to the PDMW client folder
    ''' to work around an issue working with PDMW.
    ''' </summary>
    ''' <param name="action">The delegate to invoke between setting and restoring the environment.</param>
    ''' <remarks></remarks>
    Public Sub RunWithEnvironment(ByVal action As Action)

        ' Update the environment variable to include the path to the PDMW client folder
        Dim sharedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles), "SolidWorks Shared")
        Dim originalEnvironment As String = Nothing

        If Directory.Exists(sharedPath) Then
            Dim pathVariable = Environment.GetEnvironmentVariable("Path")
            Dim pathVariables() As String = {}

            If pathVariable IsNot Nothing Then
                pathVariables = pathVariable.Split(";"c)
            End If

            Dim alreadyAdded = Array.FindIndex(pathVariables, Function(v1) v1.Equals(sharedPath, StringComparison.OrdinalIgnoreCase)) >= 0

            If Not alreadyAdded Then
                originalEnvironment = pathVariable
                Environment.SetEnvironmentVariable("Path", String.Concat(pathVariable, ";", sharedPath), EnvironmentVariableTarget.Process)
                mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.EnvironmentVariableSet, sharedPath), LocalizedResources.EnvironmentVariableTitle, Nothing)

            Else
                mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.EnvironmentVariableExists, sharedPath), LocalizedResources.EnvironmentVariableTitle, Nothing)
            End If
        End If

        ' Do the processing
        Try

            ' Run the provided delegate
            action()

        Finally

            ' Restore the environment variable
            If originalEnvironment IsNot Nothing Then
                Environment.SetEnvironmentVariable("Path", originalEnvironment, EnvironmentVariableTarget.Process)
                mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.EnvironmentVariableRemoved, sharedPath), LocalizedResources.EnvironmentVariableTitle, Nothing)
            End If
        End Try
    End Sub

    Private Sub EnsureVault()
        If mVault Is Nothing Then
            Try
                mVault = New PDMWConnection()
            Catch ex As COMException
                mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Error, Nothing, String.Format(LocalizedResources.LogPDMCannotCreateInterface), LocalizedResources.SourceTitle, Nothing)
                Return
            End Try
        End If

        If mLoggedOn Then
            Return
        End If

        ' Logon attempt 1 - using the user-selected port information
        Dim loginResult As Integer

        Try

            loginResult = mVault.Login(Me.VaultUserName, Me.VaultPassword, Me.VaultName, Me.RequestPort, Me.DataPort)

        Catch loginAttempt1Ex As COMException

            ' Let the user know login failed on the first attempt using their port numbers
            mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Warning, Nothing, String.Format(LocalizedResources.LogVaultLoginAttempt1Failed), LocalizedResources.SourceTitle, Nothing)

            Try

                ' Try logging on again with the default ports
                loginResult = mVault.Login(Me.VaultUserName, Me.VaultPassword, Me.VaultName)

            Catch loginAttempt2Ex As COMException

                ' Let the user know that the second attempt using default ports didn't work
                mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Warning, Nothing, String.Format(LocalizedResources.LogVaultLoginAttempt2Failed), LocalizedResources.SourceTitle, Nothing)
                Return
            End Try
        End Try

        If loginResult <> 0 Then
            mVault = Nothing
            mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Error, Nothing, String.Format(LocalizedResources.LogvaultLoginNonZeroErrorCode, loginResult), LocalizedResources.SourceTitle, Nothing)
            Return
        End If

        ' Successfully logged in
        mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.TestSucceeded), LocalizedResources.SourceTitle, Nothing)
        mLoggedOn = True
    End Sub
End Class
