' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports DriveWorks.Applications
Imports DriveWorks.Applications.Extensibility
Imports DriveWorks.Extensibility
Imports DriveWorks.SolidWorks.Generation
Imports DriveworksLabs.SolidWorksWorkgroupPdm.CommonPdmFramework

' DriveWorks needs three things for the ApplicationPlugin attribute:
'   1. The invariant name of the plugin - this never changes and isn't localized
'   2. The display name of the plugin - this can change and may be localized (the resx syntax makes this pretty easy)
'   2. The description of the plugin - this can change and may be localized
<ApplicationPlugin( _
    "DriveWorksSolidWorksWorkgroupPDM", _
    "resx://DriveWorksLabs.SolidWorksWorkgroupPdm.LocalizedResources,PluginName", _
    "resx://DriveWorksLabs.SolidWorksWorkgroupPdm.LocalizedResources,PluginDescription")> _
Public NotInheritable Class Plugin

    ' This is the main interface a standard application plugin MUST implement
    Implements IApplicationPlugin

    ' This optional interface lets the plugin screen in DriveWorks applications know
    ' to show a settings button
    Implements IHasConfiguration

    Private mExceptionHandler As New PluginExceptionHandler()
    Private mSettings As PluginSettings

    Private mPdm As WorkgroupPdmInterface
    Private mLog As Logger

    Private mGenerationHandler As GenerationServiceHandler
    Private mSpecificationHandler As SpecificationServiceHandler

#Region " IApplicationPlugin Members "

    ' This method is called by DriveWorks when the plugin is initialized so it has an opportunity
    ' to request services from the application
    Public Sub Initialize(ByVal application As IApplication) Implements Extensibility.IApplicationPlugin.Initialize

        ' We need access to the group mostly for information about the default specification folder
        Dim groupService = application.ServiceManager.GetService(Of IGroupService)()

        ' The application event service gives us access to the event log shown in the settings screen
        ' in DriveWorks applications, and on the Autopilot screen in DriveWorks Autopilot.
        Dim eventService = application.ServiceManager.GetService(Of IApplicationEventService)()
        mLog = New Logger(eventService)

        ' Setup for the Common PDM Framework (see README.md for more details)
        '
        ' NOTE: For the specification handler, we disable specification
        '       file checkin because Workgroup PDM doesn't support checking
        '       in more than one file with the same name even if the path 
        '       information is unique.
        mPdm = New WorkgroupPdmInterface(groupService, mLog)
        mGenerationHandler = New GenerationServiceHandler(application, mPdm, mExceptionHandler)  'TODO add in the argument for mEventService when the new generation servive handler is used from SP4
        mSpecificationHandler = New SpecificationServiceHandler(application, mPdm, mExceptionHandler, SpecificationOptions.DisableSpecificationFileCheckin) 'TODO add in the argument for mEventService when the new generation servive handler is used from SP4

        ' Create a wrapper around the application's settings manager
        ' which we'll use to read/write our own settings
        mSettings = New PluginSettings(application.SettingsManager)

        ' lets make sure we load the previously saved settings for this session
        Me.LoadSettings()
    End Sub

#End Region

#Region " IHasConfiguration Members "

    ' This method is called by the DriveWorks settings screen when the settings button is clicked
    ' for the plugin
    Public Sub ShowConfiguration(ByVal owner As System.Windows.Forms.IWin32Window) Implements Extensibility.IHasConfiguration.ShowConfiguration

        Using configForm As New PluginSettingsForm()

            configForm.LoadSettings(mSettings)

            If configForm.ShowDialog(owner) <> Windows.Forms.DialogResult.OK Then
                Return
            End If

            configForm.SaveSettings(mSettings)

            Me.LoadSettings()
        End Using
    End Sub

#End Region

#Region " Helpers "

    Private Sub LoadSettings()

        mPdm.LoadSettings(mSettings)
        mLog.LoggingLevel = mSettings.LoggingVerbosity
        mGenerationHandler.Enabled = mSettings.CheckInModels
        mSpecificationHandler.Enabled = mSettings.CheckInSpecifications

        Dim verbosity = mSettings.LoggingVerbosity

        If verbosity = LoggingLevel.Diagnostic Then
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogTitleSettingLoaded, LocalizedResources.LogSettingNameVaultName, mSettings.VaultName), Nothing, Nothing)
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogTitleSettingLoaded, LocalizedResources.LogSettingNameVaultUserName, mSettings.VaultUserName), Nothing, Nothing)
        End If
    End Sub

#End Region

End Class
