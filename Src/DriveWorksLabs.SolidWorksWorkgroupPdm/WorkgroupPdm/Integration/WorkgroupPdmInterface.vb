' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DriveWorks.Applications
Imports DriveWorksLabs.SolidWorksWorkgroupPdm.CommonPdmFramework
Imports PDMWorks.Interop.pdmworks

''' <summary>
''' Provides the interface between the common PDM framework and SolidWorks Workgroup PDM
''' </summary>
''' <remarks></remarks>
Friend Class WorkgroupPdmInterface
    Implements IPdmInterface

    Private mLog As Logger
    Private mGroupService As IGroupService
    Private mManager As WorkgroupVaultManager

    Private mInBatch As Integer

    Private mDefaultSpecificationFolder As String  ' this will be set to be the default spec folder from the DriveWorks Group

    Private mTopLevelProjectName As String
    Private mCheckInModels As Boolean
    Private mCheckInSpecifications As Boolean

    Private mFileRegistry As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
    Private mDirectoryRegistry As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

    Private mPartsList As New List(Of String)
    Private mAssemblyList As New List(Of String)
    Private mDrawingList As New List(Of String)
    Private mOtherList As New List(Of String)
    Private mFailedList As New List(Of String)

    Public Sub New(ByVal groupservice As IGroupService, ByVal log As Logger)
        mLog = log
        mGroupService = groupservice
        mManager = New WorkgroupVaultManager(log)
    End Sub

    Public Sub LoadSettings(ByVal settings As PluginSettings)

        ' Store the settings
        mTopLevelProjectName = settings.TopLevelProjectName
        mCheckInModels = settings.CheckInModels
        mCheckInSpecifications = settings.CheckInSpecifications

        ' Update the vault manager
        mManager.VaultName = settings.VaultName
        mManager.VaultUserName = settings.VaultUserName
        mManager.VaultPassword = settings.VaultPassword
        mManager.RequestPort = settings.RequestPort
        mManager.DataPort = settings.DataPort
        mManager.Close()
    End Sub

    Public Sub CheckOutFile(ByVal filePath As String) Implements IPdmInterface.CheckOutFile

        ' Not currently implemented for workgroup PDM
    End Sub

    Public Function AddFolder(ByVal folderPath As String) As Boolean Implements IPdmInterface.AddFolder

        ' Nothing we can do with that
        If folderPath Is Nothing Then
            Return False
        End If

        ' Are we enabled?
        If Not mManager.IsEnabled() Then
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(folderPath), String.Format(LocalizedResources.LogDescQueueFolderAborted, folderPath), LocalizedResources.LogTitleAddFolder, Nothing)
            Return False
        End If

        ' Normalize the path
        If Not folderPath.EndsWith(Path.DirectorySeparatorChar) Then
            folderPath &= Path.DirectorySeparatorChar
        End If

        folderPath = New DirectoryInfo(folderPath).FullName

        ' Ensure it's below the vault path
        If Not folderPath.StartsWith(Me.DefaultSpecificationFolder, StringComparison.OrdinalIgnoreCase) Then
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(folderPath), String.Format(LocalizedResources.LogDescQueueFolderAbortedNotInVault, folderPath), LocalizedResources.LogTitleAddFolder, Nothing)
            Return False
        End If

        ' It's valid - add it
        SyncLock Me
            If mInBatch = 0 Then

                ' Add immediately
                mManager.RunWithEnvironment(Sub() Me.AddFolderCore(folderPath))
            Else

                ' Queue it
                mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(folderPath), String.Format(LocalizedResources.LogDescAboutToQueueFolder, folderPath), LocalizedResources.LogTitleAddFolder, Nothing)
                mDirectoryRegistry.Add(folderPath)
            End If
        End SyncLock

        ' Let the caller know it was a controlled folder
        Return True
    End Function

    Public Function AddFile(ByVal filePath As String) As Boolean Implements IPdmInterface.AddFile

        ' Nothing we can do with that
        If filePath Is Nothing Then
            Return False
        End If

        ' Are we enabled?
        If Not mManager.IsEnabled() Then
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescQueueFileAborted, filePath), LocalizedResources.LogTitleAddFile, Nothing)
            Return False
        End If

        ' Normalize the path
        filePath = New FileInfo(filePath).FullName

        ' Ensure it's below the vault path
        If Not filePath.StartsWith(Me.DefaultSpecificationFolder, StringComparison.OrdinalIgnoreCase) Then
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescQueueFileAbortedNotInVault, filePath), LocalizedResources.LogTitleAddFile, Nothing)
            Return False
        End If

        ' It's valid - add it
        SyncLock Me
            If mInBatch = 0 Then

                ' Add immediately
                mManager.RunWithEnvironment(Sub() Me.AddFileCore(filePath, False))
            Else

                ' Queue for later add
                If mFileRegistry.Add(filePath) Then

                    ' Now check its extension, to add it to the correct list
                    ' We need to add the parts, assemblies and drawings in the correct order
                    Select Case IO.Path.GetExtension(filePath).ToLower()
                        Case ".sldprt"
                            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescFileQueued_Parts, filePath), LocalizedResources.LogTitleAddFile, Nothing)
                            mPartsList.Add(filePath)

                        Case ".sldasm"
                            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescFileQueued_Assemblies, filePath), LocalizedResources.LogTitleAddFile, Nothing)
                            mAssemblyList.Add(filePath)

                        Case ".slddrw"
                            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescFileQueued_Drawings, filePath), LocalizedResources.LogTitleAddFile, Nothing)
                            mDrawingList.Add(filePath)

                        Case Else
                            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescFileQueued_General, filePath), LocalizedResources.LogTitleAddFile, Nothing)
                            mOtherList.Add(filePath)
                    End Select
                End If
            End If
        End SyncLock

        ' Let the caller know it was a controlled file
        Return True
    End Function

    Public Sub BeginBatch() Implements CommonPdmFramework.IPdmInterface.BeginBatch

        ' Are we enabled?
        If Not mManager.IsEnabled() Then
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, LocalizedResources.LogDescBeginBatchAborted, LocalizedResources.LogTitleBatch, Nothing)
            Return
        End If

        ' seems like a great time to get the default spec folder, so that we can ensure the files go in the correct project in workgroupPDM
        mDefaultSpecificationFolder = mGroupService.ActiveGroup.DefaultSpecificationFolder

        SyncLock Me
            mInBatch += 1
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogDescBeginBatchIncrement, mInBatch), LocalizedResources.LogTitleBatch, Nothing)
        End SyncLock
    End Sub

    Public Sub EndBatch() Implements CommonPdmFramework.IPdmInterface.EndBatch

        ' Are we enabled?
        If Not mManager.IsEnabled() Then
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, LocalizedResources.LogDescEndBatchAborted, LocalizedResources.LogTitleBatch, Nothing)
            Return
        End If

        SyncLock Me
            If mInBatch = 0 Then
                mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, LocalizedResources.LogDescEndBatchAborted_AlreadyZero, LocalizedResources.LogTitleBatch, Nothing)
                Return
            Else
                mInBatch -= 1
                mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogDescEndBatchDecrement, mInBatch), LocalizedResources.LogTitleBatch, Nothing)
            End If

            If mInBatch = 0 Then
                mManager.RunWithEnvironment(Sub() Me.Commit())
            End If
        End SyncLock
    End Sub

#Region " PDMWorks Workgroup Specific Helpers "

    Private Function AddFileCore(ByVal filePath As String, Optional ByVal addToFailedList As Boolean = True) As Boolean
        mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescAboutToCheckInFile, filePath), LocalizedResources.LogTitleAddFile, Nothing)

        ' Figure out the vault relative path which is also the path relative to the default specification folder
        ' NOTE: All paths that get to AddFileCore/AddFolderCore are already known to be under the DSF.
        Dim vaultRelativePathToProject As String = Path.GetDirectoryName(filePath).Substring(Me.DefaultSpecificationFolder.Length)

        ' Ensure the projects in the chain exist and get the last project name
        Dim projectName As String = EnsureProjects(mTopLevelProjectName, vaultRelativePathToProject)

        ' Did ensuring the project structure fail?
        If projectName Is Nothing Then

            ' Failed - add to fail list so we can try again
            If addToFailedList Then
                mFailedList.Add(filePath)
            End If

            mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Warning, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescCheckingInFileIntoProjectFailedEnsureFailed, filePath, vaultRelativePathToProject), LocalizedResources.LogTitleAddFile, Nothing)
            Return False
        End If

        ' Check-in - first attempt
        mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescCheckingInFileIntoProject, filePath, projectName), LocalizedResources.LogTitleAddFile, Nothing)

        Try
            CheckFileIntoVault(filePath, projectName)
            Return True
        Catch ex As Exception
            ' If we fail for the first time, the session could have timed out - lets try logging back in again
        End Try

        ' Check-in - final attempt
        mManager.Close()

        Try

            CheckFileIntoVault(filePath, projectName)
            Return True

        Catch ex As Exception

            ' We failed pretty badly, lets inform
            mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Error, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescAddFileEx, filePath, ex.Message), LocalizedResources.LogTitleAddFile, ex)
        End Try

        ' Failed - add to fail list so we can try again
        If addToFailedList Then
            mFailedList.Add(filePath)
        End If

        Return False
    End Function

    Private Sub CheckFileIntoVault(ByVal filePath As String, ByVal fileProject As String)
        Dim document = mManager.GetVault.CheckIn(filePath, fileProject, "", "", "", PDMWRevisionOptionType.Default, "", "", True, Nothing)

        If document IsNot Nothing Then
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Success, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescCheckingInFileIntoProjectSuccess, filePath, fileProject, document.Name), LocalizedResources.LogTitleAddFile, Nothing)
        Else
            mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Warning, Path.GetFileName(filePath), String.Format(LocalizedResources.LogDescCheckingInFileIntoProjectFailed, filePath, fileProject), LocalizedResources.LogTitleAddFile, Nothing)
        End If
    End Sub

    Public Function AddFolderCore(ByVal folderPath As String) As Boolean

        ' Figure out the vault relative path which is also the path relative to the default specification folder
        ' NOTE: All paths that get to AddFileCore/AddFolderCore are already known to be under the DSF.
        Dim vaultRelativePathToProject As String = folderPath.Substring(Me.DefaultSpecificationFolder.Length)

        ' Got a vault relative path - make sure it exists in WPDM
        If EnsureProjects(mTopLevelProjectName, vaultRelativePathToProject) Is Nothing Then
            mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Warning, Path.GetFileName(folderPath), String.Format(LocalizedResources.LogDescCheckingInDirIntoProjectFailedEnsureFailed, folderPath, vaultRelativePathToProject), LocalizedResources.LogTitleAddFile, Nothing)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Ensures that all of the projects represented by the path exist.
    ''' </summary>
    ''' <param name="rootProject">The root project to which the projects will be added.</param>
    ''' <param name="vaultRelativePath">The names of the projects to create separated by directory-separator characters.</param>
    ''' <returns>The name of the last project in the project path, or a null reference if the project creation failed.</returns>
    Private Function EnsureProjects(ByVal rootProject As String, ByVal vaultRelativePath As String) As String

        If String.IsNullOrEmpty(vaultRelativePath) Then
            Return rootProject
        End If

        Dim projectNames = vaultRelativePath.Split(New Char() {IO.Path.DirectorySeparatorChar}, StringSplitOptions.RemoveEmptyEntries)
        Dim parentProject As String = rootProject

        For Each projectName In projectNames

            If Not EnsureProject(parentProject, projectName) Then

                ' It didn't work, lets try logging in to the vault again, in case it timed out
                mManager.Logout()

                If Not EnsureProject(parentProject, projectName) Then

                    ' Failed twice, that's all the chance we're giving it this round
                    Return Nothing
                End If
            End If

            parentProject = projectName
        Next

        ' Return the last project name
        Return parentProject
    End Function

    Private Function EnsureProject(ByVal parentProjectName As String, ByVal childProjectName As String) As Boolean
        Try
            mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, childProjectName, String.Format(LocalizedResources.LogDescAboutToAddProjectToParent, childProjectName, parentProjectName), LocalizedResources.LogTitleAddFolder, Nothing)

            If mManager.GetVault Is Nothing Then
                mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Error, Nothing, String.Format(LocalizedResources.LogDescProjectCreationFailedCompletelyNoVault, childProjectName), LocalizedResources.LogTitleAddFolder, Nothing)
                Return False
            End If

            ' Refresh any cached vault data
            Dim vault = mManager.GetVault
            vault.Refresh()

            ' Get all of the existing projects from the vault
            Dim projects As IPDMWProjects = vault.Projects

            ' Does the child already exist?
            Dim childProject As IPDMWProject = Nothing

            If projects.TryGetProject(childProjectName, childProject) Then
                If childProject.Parent Is Nothing Then
                    mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogDescProjectAlreadyInVaultAndIsRoot, childProjectName), LocalizedResources.LogTitleAddFolder, Nothing)
                Else
                    mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogDescProjectAlreadyInVaultWithParent, childProjectName, childProject.Parent.Name), LocalizedResources.LogTitleAddFolder, Nothing)
                End If

                Marshal.ReleaseComObject(childProject)
                Return True
            End If

            ' No parent = no joy
            Dim parentProject As IPDMWProject = Nothing

            If Not projects.TryGetProject(parentProjectName, parentProject) Then
                mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Error, Nothing, String.Format(LocalizedResources.LogDescCannotFindParent, childProjectName, parentProjectName), LocalizedResources.LogTitleAddFolder, Nothing)
                Return False
            End If

            Try

                ' First attempt!
                mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogDescAboutToAddProjectToParent, childProjectName, parentProjectName), LocalizedResources.LogTitleAddFolder, Nothing)

                Try
                    parentProject.CreateSubProject(childProjectName, childProjectName)
                    mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogDescAboutToAddProjectToParent, childProjectName, parentProjectName), LocalizedResources.LogTitleAddFolder, Nothing)
                    Return True
                Catch ex As Exception
                End Try

                ' OK - failed. That could be because the login timed out.  Lets try again
                mManager.Close()
                vault = mManager.GetVault

                ' Final attempt
                Try
                    vault.CreateProject(childProjectName, childProjectName, parentProjectName)
                    mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogDescAboutToAddProjectToParent, childProjectName, parentProjectName), LocalizedResources.LogTitleAddFolder, Nothing)
                    Return True
                Catch ex As Exception
                    mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogDescProjectCreationFailed, childProjectName, parentProjectName, ex.Message), LocalizedResources.LogTitleAddFolder, Nothing)
                End Try

            Finally
                Marshal.ReleaseComObject(parentProject)
                parentProject = Nothing
            End Try

        Catch ex As Exception

            ' Don't crash DriveWorks, let the user know
            mLog.LogMessage(LoggingLevel.General, ApplicationEventType.Error, childProjectName, String.Format(LocalizedResources.LogDescAddFolderEx, childProjectName, ex.Message), LocalizedResources.LogTitleAddFile, ex)
        End Try

        Return False
    End Function

    Private Sub Commit()
        SyncLock Me

            ' Add folders
            For Each folder As String In mDirectoryRegistry
                Me.AddFolderCore(folder)
            Next

            ' Add files
            For Each file As String In mPartsList
                Me.AddFileCore(file)
            Next

            For Each file As String In mAssemblyList
                Me.AddFileCore(file)
            Next

            For Each file As String In mDrawingList
                Me.AddFileCore(file)
            Next

            For Each file As String In mOtherList
                Me.AddFileCore(file)
            Next

            ' finally run the failed list again.  EPDM might have caught up by now
            ' second attempts almost always work
            For Each file As String In mFailedList
                Me.AddFileCore(file, False)
            Next

            ' Make sure we don't try to commit the stuff again
            mDirectoryRegistry.Clear()
            mFileRegistry.Clear()
            mPartsList.Clear()
            mAssemblyList.Clear()
            mDrawingList.Clear()
            mOtherList.Clear()
            mFailedList.Clear()

            ' all done, so lets log out
            mManager.Logout()
        End SyncLock
    End Sub

    ''' <summary>
    ''' Returns the default specification folder for the active group, including a trailing directory-separator character, e.g. "&lt;".
    ''' </summary>
    Private ReadOnly Property DefaultSpecificationFolder() As String
        Get

            ' Let's try to get the default spec folder from the group if we don't have it yet
            If mDefaultSpecificationFolder Is Nothing Then

                mDefaultSpecificationFolder = mGroupService.ActiveGroup.DefaultSpecificationFolder

                ' Ensure we have a trailing slash
                If Not mDefaultSpecificationFolder.EndsWith(Path.DirectorySeparatorChar) Then
                    mDefaultSpecificationFolder &= Path.DirectorySeparatorChar
                End If

                ' Log the vault root path
                mLog.LogMessage(LoggingLevel.Diagnostic, ApplicationEventType.Information, Nothing, String.Format(LocalizedResources.LogDescVaultRootPath, mDefaultSpecificationFolder, mManager.VaultName), LocalizedResources.LogTitleVaultRootPath, Nothing)
            End If

            ' Return whatever we've got
            Return mDefaultSpecificationFolder
        End Get
    End Property

#End Region

End Class
