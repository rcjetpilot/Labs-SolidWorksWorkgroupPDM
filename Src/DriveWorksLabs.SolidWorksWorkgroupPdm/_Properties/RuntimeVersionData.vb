' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System
Imports System.Reflection

Friend Class RuntimeVersionData

    ' Update this version number if we're releasing a new version 
    ' of the plugin, reset to 1 if the SDK version changes
    Public Const VersionPlugin As String = "1"

    ' Update these versions to correspond to the SDK against which
    ' we are building
    Public Const VersionSdkMajor As String = "9"
    Public Const VersionSdkMinor As String = "3"
    Public Const VersionSdkRevision As String = "0"

#Region " Computed Values - Do Not Change "

    ''' <summary>
    ''' The version number of the SDK, i.e. the first three components of the version number.
    ''' </summary>
    Public Const VersionSdkCombined As String = VersionSdkMajor & "." & VersionSdkMinor & "." & VersionSdkRevision

    ''' <summary>
    ''' The version number of the SDK as it is displayed in the user interface.
    ''' </summary>
    Public Const VersionSdkDisplay As String = VersionSdkMajor & " SP" & VersionSdkMinor & "." & VersionSdkRevision

    ''' <summary>
    ''' The file version number of the plugin.
    ''' </summary>
    Public Const VersionCombined As String = VersionSdkCombined & "." & VersionPlugin

    ''' <summary>
    ''' The assembly version of the plugin, which is the same as the as the file version number.
    ''' </summary>
    ''' <remarks></remarks>
    Public Const VersionAssembly As String = VersionCombined

    ''' <summary>
    ''' The product version, this is for file properties and user interface only.
    ''' </summary>
    ''' <remarks></remarks>
    Public Const VersionProduct As String = "Version " & VersionAssembly & " (DriveWorks " & VersionSdkDisplay & " or later)"

#If DEBUG Then
    Public Const VersionConfiguration As String = "Debug"
#Else
    Public Const VersionConfiguration As String = "Release"
#End If

#End Region

End Class
