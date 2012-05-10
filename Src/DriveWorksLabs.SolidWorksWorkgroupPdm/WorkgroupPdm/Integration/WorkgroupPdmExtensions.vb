' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports PDMWorks.Interop.pdmworks

Module WorkgroupPdmExtensions

    ''' <summary>
    ''' Gets the project with the given name.
    ''' </summary>
    ''' <param name="projects"></param>
    ''' <param name="projectName"></param>
    ''' <param name="project"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function TryGetProject(ByVal projects As IPDMWProjects, ByVal projectName As String, ByRef project As IPDMWProject) As Boolean
        Const E_FAIL As Integer = &H80004005

        Try
            project = DirectCast(projects(projectName), IPDMWProject)
            Return True
        Catch ex As COMException When ex.ErrorCode = E_FAIL
            project = Nothing
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Determines whether a project with the specified name exists.
    ''' </summary>
    ''' <param name="projects"></param>
    ''' <param name="projectName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function Exists(ByVal projects As IPDMWProjects, ByVal projectName As String) As Boolean
        Dim project As IPDMWProject = Nothing

        If projects.TryGetProject(projectName, project) Then
            Marshal.ReleaseComObject(project)
            project = Nothing
            Return True
        Else
            Return False
        End If
    End Function
End Module
