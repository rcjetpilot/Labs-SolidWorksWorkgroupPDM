' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System.Windows.Forms
Imports DriveWorks.Applications

Class Logger
    Private Const SOURCE_INVARIANT_NAME As String = "urn://driveworks/plugins/core/pdm/swwpdm"

    Private mLoggingService As IApplicationEventService

    Public Sub New(ByVal eventService As IApplicationEventService)
        mLoggingService = eventService
    End Sub

    Public Property LoggingLevel As LoggingLevel

    Public Sub LogMessage(ByVal minimumLoggingLevel As LoggingLevel, ByVal eventType As ApplicationEventType, ByVal target As String, ByVal description As String, ByVal task As String, ByVal ex As Exception)
        If CInt(minimumLoggingLevel) > CInt(Me.LoggingLevel) Then
            Return
        End If

        Dim sourceDisplayName As String = LocalizedResources.SourceTitle

        If Not String.IsNullOrEmpty(task) Then
            sourceDisplayName &= ": " & task
        End If

        If mLoggingService Is Nothing Then
            MessageBox.Show(description, String.Format("{0} ({1})", sourceDisplayName, eventType), MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            mLoggingService.AddEvent(eventType, SOURCE_INVARIANT_NAME, sourceDisplayName, description, Nothing, target, Nothing)
        End If
    End Sub
End Class
