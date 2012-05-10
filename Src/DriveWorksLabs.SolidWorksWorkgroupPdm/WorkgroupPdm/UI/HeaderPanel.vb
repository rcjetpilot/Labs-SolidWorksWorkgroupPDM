' Copyright (c) 2012 DriveWorks Ltd. All rights reserved. See LICENSE.md in the repository root for license information.

Imports System.Drawing

Friend Class HeaderPanel
    Inherits System.Windows.Forms.Panel

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.DoubleBuffered = True
        Me.ResizeRedraw = True
    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'HeaderPanel
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Name = "HeaderPanel"
        Me.Size = New System.Drawing.Size(408, 88)

    End Sub

#End Region

#Region " Painting Code "
    Private Sub pnlHeader_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        With e.Graphics
            .DrawLine(SystemPens.ControlLightLight, 0, Height - 1, Width, Height - 1)
            .DrawLine(SystemPens.ControlDark, 0, Height - 2, Width, Height - 2)
        End With
    End Sub
#End Region

End Class