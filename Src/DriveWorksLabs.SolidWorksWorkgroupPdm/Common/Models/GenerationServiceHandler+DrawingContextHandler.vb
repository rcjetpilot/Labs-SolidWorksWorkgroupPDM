' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System.IO
Imports DriveWorks.SolidWorks.Generation

Namespace CommonPdmFramework
    Partial Class GenerationServiceHandler

        ''' <summary>
        ''' Handles events for a drawing context 
        ''' and checks relevant files in/out of PDM.
        ''' </summary>
        ''' <remarks></remarks>
        Private Class DrawingContextHandler
            Private mExceptionHandler As IPdmExceptionHandler
            Private mPdm As IPdmInterface
            Private WithEvents mContext As IDrawingGenerationContext

            Public Sub New(ByVal pdm As IPdmInterface, ByVal exceptionHandler As IPdmExceptionHandler, ByVal context As IDrawingGenerationContext)
                mPdm = pdm
                mExceptionHandler = exceptionHandler
                mContext = context
            End Sub

            Private Sub HandlePreparing(ByVal sender As Object, ByVal e As System.EventArgs) Handles mContext.Preparing
                Try

                    ' In order for DriveWorks to overwrite the model, as we may be regenerating a model, we need to ensure we've got exclusive access it
                    If IO.File.Exists(mContext.Drawing.TargetPath) Then
                        mPdm.CheckOutFile(mContext.Drawing.TargetPath)
                    End If

                Catch ex As Exception

                    ' Make sure we don't crash DriveWorks
                    mExceptionHandler.HandleException(ex)
                End Try
            End Sub

            Private Sub HandleGenerated(ByVal sender As Object, ByVal e As System.EventArgs) Handles mContext.Generated
                Try

                    ' Add it to the vault
                    mPdm.AddFile(mContext.Drawing.TargetPath)

                Catch ex As Exception

                    ' Make sure we don't crash DriveWorks
                    mExceptionHandler.HandleException(ex)
                End Try
            End Sub

            Private Sub HandleAdditionalFileFormatPreparing(ByVal sender As Object, ByVal e As FileFormatGenerationEventArgs) Handles mContext.AdditionalFileFormatPreparing
                Try

                    ' In order for DriveWorks to overwrite the format, as we may be regenerating a model, we need to ensure we've got exclusive access it
                    If IO.File.Exists(e.TargetPath) Then
                        mPdm.CheckOutFile(e.TargetPath)
                    End If

                Catch ex As Exception

                    ' Make sure we don't crash DriveWorks
                    mExceptionHandler.HandleException(ex)
                End Try
            End Sub

            Private Sub HandleAdditionalFileFormatCreated(ByVal sender As Object, ByVal e As FileFormatGenerationEventArgs) Handles mContext.AdditionalFileFormatGenerated
                Try

                    ' Add it to the vault
                    mPdm.AddFile(e.TargetPath)

                Catch ex As Exception

                    ' Make sure we don't crash DriveWorks
                    mExceptionHandler.HandleException(ex)
                End Try
            End Sub
        End Class
    End Class
End Namespace