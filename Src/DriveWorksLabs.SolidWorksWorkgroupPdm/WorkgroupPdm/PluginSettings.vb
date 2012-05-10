' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports DriveWorks.Applications

''' <summary>
''' Provides access to the plugins settings.
''' </summary>
''' <remarks></remarks>
Friend NotInheritable Class PluginSettings
    Private Const SETTING_BASE As String = "Common\Libraries\DriveWorksLabs.SolidWorksWorkgroupPdm\Settings\"
    Private Const SETTING_NAME_VAULT As String = SETTING_BASE & "VaultName"
    Private Const SETTING_NAME_VAULT_UN As String = SETTING_BASE & "VaultUserName"
    Private Const SETTING_NAME_VAULT_PW As String = SETTING_BASE & "VaultPassword"
    Private Const SETTING_NAME_VAULT_TOPLEVEL_PROJECTNAME As String = SETTING_BASE & "VaultTopLevelProjectName"
    Private Const SETTING_NAME_LOGGING_VERBOSITY As String = SETTING_BASE & "LoggingVerbosity"
    Private Const SETTING_NAME_VAULT_REQUESTPORT As String = SETTING_BASE & "VaultRequestPort"
    Private Const SETTING_NAME_VAULT_DATAPORT As String = SETTING_BASE & "VaultDataPort"
    Private Const SETTING_NAME_CHECKIN_MODELS As String = SETTING_BASE & "CheckInModels"
    Private Const SETTING_NAME_CHECKIN_SPECIFICATIONS As String = SETTING_BASE & "CheckInSpecifications"

    Private Const SETTING_DEFAULT_REQUESTPORT As Integer = 40000
    Private Const SETTING_DEFAULT_DATAPORT As Integer = 30000

    Private mSettingsManager As ISettingsManager

    ''' <summary>
    ''' Initializes a new instance of the <see cref="PluginSettings" /> class.
    ''' </summary>
    ''' <param name="settingsManager">The settings manager to wrap.</param>
    Public Sub New(ByVal settingsManager As ISettingsManager)
        mSettingsManager = settingsManager
    End Sub

    ''' <summary>
    ''' Gets or sets the name of the vault to which to connect.
    ''' </summary>
    Public Property VaultName() As String
        Get
            Return mSettingsManager.GetSettingAsString(SettingLevel.User, SETTING_NAME_VAULT, False)
        End Get
        Set(ByVal value As String)
            mSettingsManager.SetSetting(SettingLevel.User, SETTING_NAME_VAULT, value, False)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the user name to use to connect to the vault.
    ''' </summary>
    Public Property VaultUserName() As String
        Get
            Return mSettingsManager.GetSettingAsString(SettingLevel.User, SETTING_NAME_VAULT_UN, False)
        End Get
        Set(ByVal value As String)
            mSettingsManager.SetSetting(SettingLevel.User, SETTING_NAME_VAULT_UN, value, False)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the password to use to connect to the vault.
    ''' </summary>
    Public Property VaultPassword() As String
        Get
            Return Crypt.WeakDecrypt(mSettingsManager.GetSettingAsString(SettingLevel.User, SETTING_NAME_VAULT_PW, False))
        End Get
        Set(ByVal value As String)
            mSettingsManager.SetSetting(SettingLevel.User, SETTING_NAME_VAULT_PW, Crypt.WeakEncrypt(value), False)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the logging verbosity.
    ''' </summary>
    Public Property LoggingVerbosity() As LoggingLevel
        Get
            Dim verbosity As Integer

            If mSettingsManager.TryGetSettingAsInteger(SettingLevel.User, SETTING_NAME_LOGGING_VERBOSITY, False, verbosity) Then
                Return DirectCast(verbosity, LoggingLevel)
            Else
                Return LoggingLevel.General
            End If
        End Get
        Set(ByVal value As LoggingLevel)
            mSettingsManager.SetSetting(SettingLevel.User, SETTING_NAME_LOGGING_VERBOSITY, CInt(value), False)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the top level project name for adding new files to the vault.
    ''' </summary>
    Public Property TopLevelProjectName() As String
        Get
            Return mSettingsManager.GetSettingAsString(SettingLevel.User, SETTING_NAME_VAULT_TOPLEVEL_PROJECTNAME, False)
        End Get
        Set(ByVal value As String)
            mSettingsManager.SetSetting(SettingLevel.User, SETTING_NAME_VAULT_TOPLEVEL_PROJECTNAME, value, False)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the request port to use to connect to the vault.
    ''' </summary>
    Public Property RequestPort() As Integer
        Get
            Dim value As Integer

            If mSettingsManager.TryGetSettingAsInteger(SettingLevel.User, SETTING_NAME_VAULT_REQUESTPORT, False, value) AndAlso IsValidPortNumber(value) Then
                Return value
            Else
                Return SETTING_DEFAULT_REQUESTPORT
            End If
        End Get
        Set(ByVal value As Integer)
            mSettingsManager.SetSetting(SettingLevel.User, SETTING_NAME_VAULT_REQUESTPORT, value, False)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the data port to use to connect to the vault.
    ''' </summary>
    Public Property DataPort() As Integer
        Get
            Dim value As Integer

            If mSettingsManager.TryGetSettingAsInteger(SettingLevel.User, SETTING_NAME_VAULT_DATAPORT, False, value) AndAlso IsValidPortNumber(value) Then
                Return value
            Else
                Return SETTING_DEFAULT_DATAPORT
            End If
        End Get
        Set(ByVal value As Integer)
            mSettingsManager.SetSetting(SettingLevel.User, SETTING_NAME_VAULT_DATAPORT, value, False)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether models are checked in.
    ''' </summary>
    Public Property CheckInModels() As Boolean
        Get
            Dim value As Boolean

            If mSettingsManager.TryGetSettingAsBoolean(SettingLevel.User, SETTING_NAME_CHECKIN_MODELS, False, value) Then
                Return value
            Else
                Return True
            End If
        End Get
        Set(ByVal value As Boolean)
            mSettingsManager.SetSetting(SettingLevel.User, SETTING_NAME_CHECKIN_MODELS, value, False)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether specifications are checked in.
    ''' </summary>
    Public Property CheckInSpecifications() As Boolean
        Get
            Dim value As Boolean

            If mSettingsManager.TryGetSettingAsBoolean(SettingLevel.User, SETTING_NAME_CHECKIN_SPECIFICATIONS, False, value) Then
                Return value
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            mSettingsManager.SetSetting(SettingLevel.User, SETTING_NAME_CHECKIN_SPECIFICATIONS, value, False)
        End Set
    End Property

    Private Shared Function IsValidPortNumber(ByVal value As Integer) As Boolean
        Return value >= 0 AndAlso value <= 65535
    End Function
End Class
