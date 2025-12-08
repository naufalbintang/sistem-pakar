<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormHasil
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        LabelHasil = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 18.0F)
        Label1.Location = New Point(87, 177)
        Label1.Name = "Label1"
        Label1.Size = New Size(640, 41)
        Label1.TabIndex = 0
        Label1.Text = "Rekomendasi Topik Skripsi Untuk Anda Adalah:"
        ' 
        ' LabelHasil
        ' 
        LabelHasil.AutoSize = True
        LabelHasil.Font = New Font("Segoe UI", 18.0F)
        LabelHasil.Location = New Point(348, 232)
        LabelHasil.Name = "LabelHasil"
        LabelHasil.Size = New Size(104, 41)
        LabelHasil.TabIndex = 1
        LabelHasil.Text = "(topik)"
        ' 
        ' FormHasil
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(LabelHasil)
        Controls.Add(Label1)
        Name = "FormHasil"
        Text = "FormHasil"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents LabelHasil As Label
End Class
