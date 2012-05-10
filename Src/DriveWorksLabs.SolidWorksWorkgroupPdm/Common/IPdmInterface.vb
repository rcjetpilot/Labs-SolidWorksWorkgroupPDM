' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Namespace CommonPdmFramework

    ''' <summary>
    ''' Provides a contract for the PDM functionality required by 
    ''' the common PDM framework.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IPdmInterface

        ''' <summary>
        ''' Notifies the PDM system that a batch is starting.
        ''' </summary>
        ''' <remarks></remarks>
        Sub BeginBatch()

        ''' <summary>
        ''' Notifies the PDM system that a batch is ending.
        ''' </summary>
        ''' <remarks></remarks>
        Sub EndBatch()

        ''' <summary>
        ''' Adds the specified folder to PDM.
        ''' </summary>
        ''' <param name="folderPath">The absolute or vault-relative path to the folder to be added.</param>
        ''' <returns>True if the folder path was in a controlled location and added, otherwise false.</returns>
        ''' <remarks></remarks>
        Function AddFolder(ByVal folderPath As String) As Boolean

        ''' <summary>
        ''' Adds the specified file to PDM.
        ''' </summary>
        ''' <param name="filePath">The absolute or vault-relative path to the file to be added.</param>
        ''' <returns>True if the file path was in a controlled location and added, otherwise false.</returns>
        ''' <remarks></remarks>
        Function AddFile(ByVal filePath As String) As Boolean

        ''' <summary>
        ''' Checks the specified file out of source control.
        ''' </summary>
        ''' <param name="filePath">The path to the file to check out.</param>
        ''' <remarks></remarks>
        Sub CheckOutFile(ByVal filePath As String)
    End Interface
End Namespace