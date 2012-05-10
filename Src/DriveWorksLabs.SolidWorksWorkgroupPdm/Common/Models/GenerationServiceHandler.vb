' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports DriveWorks.Applications
Imports DriveWorks.SolidWorks.Generation

Namespace CommonPdmFramework

    ''' <summary>
    ''' Handles the interaction between the model generation process and a PDM system.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GenerationServiceHandler
        Private mExceptionHandler As IPdmExceptionHandler
        Private mPdm As IPdmInterface
        Private mEnabled As Boolean
        Private WithEvents mGenerationService As IGenerationService

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GenerationServiceHandler" /> class.
        ''' </summary>
        ''' <param name="application">The hosting application.</param>
        ''' <param name="pdm">The PDM system.</param>
        ''' <param name="exceptionHandler">The exception handler.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal application As IApplication, ByVal pdm As IPdmInterface, ByVal exceptionHandler As IPdmExceptionHandler)
            If application Is Nothing Then Throw New ArgumentNullException("application")
            If pdm Is Nothing Then Throw New ArgumentNullException("pdm")
            If exceptionHandler Is Nothing Then Throw New ArgumentNullException("exceptionHandler")

            ' Store required services
            mPdm = pdm
            mExceptionHandler = exceptionHandler

            ' Get hold of the generation service - if the application doesn't have one, Nothing is returned
            mGenerationService = application.ServiceManager.GetService(Of IGenerationService)()
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

        Private Sub HandleBatchStarted(ByVal sender As Object, ByVal e As System.EventArgs) Handles mGenerationService.BatchStarted
            If Not mEnabled Then
                Return
            End If

            mPdm.BeginBatch()
        End Sub

        Private Sub HandleGeneratingModel(ByVal sender As Object, ByVal e As ModelGenerationContextEventArgs) Handles mGenerationService.ModelGenerationContextCreated
            If Not mEnabled Then
                Return
            End If

            ' Create a new handler to handle the generation of the specified model
            Dim handler As ModelContextHandler
            handler = New ModelContextHandler(mPdm, mExceptionHandler, e.Context)
        End Sub

        Private Sub HandleBatchFinished(ByVal sender As Object, ByVal e As System.EventArgs) Handles mGenerationService.BatchFinished
            If Not mEnabled Then
                Return
            End If

            mPdm.EndBatch()
        End Sub
    End Class
End Namespace