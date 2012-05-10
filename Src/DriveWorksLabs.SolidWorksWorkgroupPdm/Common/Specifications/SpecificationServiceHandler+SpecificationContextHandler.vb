' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System.IO
Imports DriveWorks.Applications
Imports DriveWorks.Specification

Namespace CommonPdmFramework
    Partial Class SpecificationServiceHandler

        ''' <summary>
        ''' Handles the events for a specification context
        ''' and checks relevant files in/out of PDM.
        ''' </summary>
        ''' <remarks></remarks>
        Private Class SpecificationContextHandler
            Private mExceptionHandler As IPdmExceptionHandler
            Private mPdm As IPdmInterface
            Private mOptions As SpecificationOptions
            Private WithEvents mContext As SpecificationContext

            Public Sub New(ByVal pdm As IPdmInterface, ByVal exceptionHandler As IPdmExceptionHandler, ByVal options As SpecificationOptions, ByVal context As SpecificationContext)
                mPdm = pdm
                mExceptionHandler = exceptionHandler
                mOptions = options
                mContext = context
            End Sub

            Private Sub HandleChildContextCreated(ByVal sender As Object, ByVal e As SpecificationContextEventArgs) Handles mContext.ChildContextCreated

                ' Create a new handle for the child specification
                Dim childHandler As SpecificationContextHandler
                childHandler = New SpecificationContextHandler(mPdm, mExceptionHandler, mOptions, e.Context)
            End Sub

            Private Sub HandleAdditionalFoldersCreated(ByVal sender As Object, ByVal e As AdditionalFoldersCreatedEventArgs) Handles mContext.AdditionalFoldersCreated
                Try

                    ' Process each additional folder
                    For Each fullAdditionalFolderPath As String In e.FullPaths
                        mPdm.AddFolder(fullAdditionalFolderPath)
                    Next

                Catch ex As Exception

                    ' Make sure we don't crash DriveWorks
                    mExceptionHandler.HandleException(ex)
                End Try
            End Sub

            Private Sub HandleDocumentRegistered(ByVal sender As Object, ByVal e As SpecificationDocumentEventArgs) Handles mContext.DocumentRegistered
                Try

                    ' Get the new file name of the model in its normalized form
                    Dim newFilePath As String = New FileInfo(e.DocumentDetails.Path).FullName

                    ' Ensure the document is checked in 
                    If Not mPdm.AddFile(newFilePath) Then

                        ' If it wasn't added (because it wasn't in a controlled location)
                        ' there's no point continuing
                        Return
                    End If

                    ' If it's an HTML document, there may be an associated {0}_files directory, grab that too
                    Dim ext As String = Path.GetExtension(newFilePath)

                    If ext.Equals(".htm", StringComparison.OrdinalIgnoreCase) OrElse
                       ext.Equals(".html", StringComparison.OrdinalIgnoreCase) Then

                        ' Get the path to the {0}_files directory
                        Dim filesDirPath As String = Path.ChangeExtension(newFilePath, Nothing) & "_files"

                        ' If the directory exists, add it to the vault
                        If Directory.Exists(filesDirPath) Then
                            AddFolderRecursive(filesDirPath)
                        End If
                    End If

                Catch ex As Exception

                    ' Make sure we don't crash DriveWorks
                    mExceptionHandler.HandleException(ex)
                End Try
            End Sub

            Private Sub HandleTransitionInvoked(ByVal sender As Object, ByVal e As TransitionEventArgs) Handles mContext.TransitionInvoked
                Try

                    ' Make sure this behavior hasn't been disabled
                    If (mOptions And SpecificationOptions.DisableSpecificationFileCheckin) = SpecificationOptions.DisableSpecificationFileCheckin Then
                        Return
                    End If

                    ' If we're transitioning to a paused or automatic state
                    ' then ensure we check the files in/back in
                    If e.Transition.TargetState.Type <> StateType.Running Then
                        Me.CheckinSpecification()
                    End If

                Catch ex As Exception

                    ' Make sure we don't crash DriveWorks
                    mExceptionHandler.HandleException(ex)
                End Try
            End Sub

            Private Sub HandleTransitionInvoking(ByVal sender As Object, ByVal e As TransitionEventArgs) Handles mContext.TransitionInvoking
                Try

                    ' If we're transitioning to a paused or automatic state
                    ' then ensure the files we need are checked out so we can overwrite them
                    If mContext.Type = SpecificationType.Existing AndAlso e.Transition.TargetState.Type <> StateType.Running Then
                        Me.CheckoutSpecification()
                    End If

                Catch ex As Exception

                    ' Make sure we don't crash DriveWorks
                    mExceptionHandler.HandleException(ex)
                End Try
            End Sub

            Private Sub CheckoutSpecification()

                ' There are three files we're interested in:
                ' 1) The specification file (<spec name>.drivespec)
                ' 2) The specification project file (<project name>.driveproj)
                ' 3) The specification design master (<project name>.drivemaster or <project name>.xls) - this might be null if the project
                '        has an embedded design master (introduced in DriveWorks 9)
                Dim specificationFile = mContext.SpecificationFilePath
                Dim projectFile = mContext.ProjectFilePath
                Dim designMasterFile = mContext.DesignMasterPath

                mPdm.CheckOutFile(specificationFile)
                mPdm.CheckOutFile(projectFile)

                If String.IsNullOrEmpty(designMasterFile) Then
                    mPdm.CheckOutFile(designMasterFile)
                End If
            End Sub

            Private Sub CheckinSpecification()

                ' There are three files we're interested in:
                ' 1) The specification file (<spec name>.drivespec)
                ' 2) The specification project file (<project name>.driveproj)
                ' 3) The specification design master (<project name>.drivemaster or <project name>.xls) - this might be null if the project
                '        has an embedded design master (introduced in DriveWorks 9)
                Dim specificationFile = mContext.SpecificationFilePath
                Dim projectFile = mContext.ProjectFilePath
                Dim designMasterFile = mContext.DesignMasterPath

                mPdm.AddFile(specificationFile)
                mPdm.AddFile(projectFile)

                If String.IsNullOrEmpty(designMasterFile) Then
                    mPdm.CheckOutFile(designMasterFile)
                End If
            End Sub

            Private Sub AddFolderRecursive(ByVal directoryPath As String)

                ' Add the folder itself
                mPdm.AddFolder(directoryPath)

                ' Add files
                For Each fp As String In Directory.GetFiles(directoryPath)
                    mPdm.AddFile(fp)
                Next

                ' Add folders
                For Each dp As String In Directory.GetDirectories(directoryPath)
                    AddFolderRecursive(dp)
                Next
            End Sub
        End Class
    End Class
End Namespace