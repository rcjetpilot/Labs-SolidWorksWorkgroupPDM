' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Namespace CommonPdmFramework

    ''' <summary>
    ''' Provides a contract for an object to handle exceptions which occur
    ''' in the common PDM framework.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IPdmExceptionHandler

        ''' <summary>
        ''' Handles the given exception.
        ''' </summary>
        ''' <param name="ex">The exception to handle.</param>
        ''' <remarks></remarks>
        Sub HandleException(ByVal ex As Exception)
    End Interface
End Namespace