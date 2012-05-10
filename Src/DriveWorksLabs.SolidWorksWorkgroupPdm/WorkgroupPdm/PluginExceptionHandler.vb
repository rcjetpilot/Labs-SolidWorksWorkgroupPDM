' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports DriveWorksLabs.SolidWorksWorkgroupPdm.CommonPdmFramework

Class PluginExceptionHandler

    ' This is part of the Common PDM Framework to allow
    ' the various generation/specification handlers to notify the plugin
    ' of exceptions
    Implements IPdmExceptionHandler

    ''' <summary>
    ''' Implements <see cref="IPdmExceptionHandler.HandleException" />.
    ''' </summary>
    Public Overridable Sub HandleException(ByVal ex As Exception) Implements IPdmExceptionHandler.HandleException
        Debug.Fail(ex.Message, ex.ToString())
    End Sub
End Class
