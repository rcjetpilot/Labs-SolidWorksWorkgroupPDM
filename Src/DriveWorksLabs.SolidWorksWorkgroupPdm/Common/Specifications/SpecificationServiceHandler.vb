' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports DriveWorks.Applications
Imports DriveWorks.Specification

Namespace CommonPdmFramework

    ''' <summary>
    ''' Handles the interaction between the specification process and a PDM system.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SpecificationServiceHandler
        Private mExceptionHandler As IPdmExceptionHandler
        Private mPdm As IPdmInterface
        Private mOptions As SpecificationOptions
        Private mEnabled As Boolean
        Private WithEvents mSpecificationService As ISpecificationService

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GenerationServiceHandler" /> class.
        ''' </summary>
        ''' <param name="application">The hosting application.</param>
        ''' <param name="pdm">The PDM system.</param>
        ''' <param name="exceptionHandler">The exception handler.</param>
        ''' <param name="options">Any specific behavioral options which should be applied.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal application As IApplication, ByVal pdm As IPdmInterface, ByVal exceptionHandler As IPdmExceptionHandler, ByVal options As SpecificationOptions)
            If application Is Nothing Then Throw New ArgumentNullException("application")
            If pdm Is Nothing Then Throw New ArgumentNullException("pdm")
            If exceptionHandler Is Nothing Then Throw New ArgumentNullException("exceptionHandler")

            mPdm = pdm
            mExceptionHandler = exceptionHandler
            mOptions = options

            ' Get hold of the specification service - if the application doesn't have one, Nothing is returned
            mSpecificationService = application.ServiceManager.GetService(Of ISpecificationService)()
        End Sub

        ''' <summary>
        ''' Gets or sets whether the generation service is enabled/disabled.
        ''' </summary>
        Public Property Enabled() As Boolean
            Get
                Return mEnabled
            End Get
            Set(ByVal value As Boolean)
                mEnabled = value
            End Set
        End Property

        Private Sub HandleSpecificationContextCreated(ByVal sender As Object, ByVal e As SpecificationContextEventArgs) Handles mSpecificationService.SpecificationContextCreated
            If Not mEnabled Then
                Return
            End If

            ' Create a new object to handle the specification context's events
            Dim handler As SpecificationContextHandler
            handler = New SpecificationContextHandler(mPdm, mExceptionHandler, mOptions, e.Context)
        End Sub
    End Class
End Namespace