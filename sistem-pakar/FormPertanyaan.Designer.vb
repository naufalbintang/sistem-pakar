<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPertanyaan
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
        PanelPertanyaan = New FlowLayoutPanel()
        ButtonSebelumnya = New Button()
        ButtonSelanjutnya = New Button()
        LabelHalaman = New Label()
        SuspendLayout()
        ' 
        ' PanelPertanyaan
        ' 
        PanelPertanyaan.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PanelPertanyaan.AutoScroll = True
        PanelPertanyaan.FlowDirection = FlowDirection.TopDown
        PanelPertanyaan.Location = New Point(12, 32)
        PanelPertanyaan.Name = "PanelPertanyaan"
        PanelPertanyaan.Size = New Size(776, 354)
        PanelPertanyaan.TabIndex = 0
        PanelPertanyaan.WrapContents = False
        ' 
        ' ButtonSebelumnya
        ' 
        ButtonSebelumnya.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ButtonSebelumnya.Location = New Point(15, 392)
        ButtonSebelumnya.Name = "ButtonSebelumnya"
        ButtonSebelumnya.Size = New Size(105, 29)
        ButtonSebelumnya.TabIndex = 1
        ButtonSebelumnya.Text = "Sebelumnya"
        ButtonSebelumnya.UseVisualStyleBackColor = True
        ' 
        ' ButtonSelanjutnya
        ' 
        ButtonSelanjutnya.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ButtonSelanjutnya.Location = New Point(688, 392)
        ButtonSelanjutnya.Name = "ButtonSelanjutnya"
        ButtonSelanjutnya.Size = New Size(100, 29)
        ButtonSelanjutnya.TabIndex = 2
        ButtonSelanjutnya.Text = "Selanjutnya"
        ButtonSelanjutnya.UseVisualStyleBackColor = True
        ' 
        ' LabelHalaman
        ' 
        LabelHalaman.AutoSize = True
        LabelHalaman.Location = New Point(374, 9)
        LabelHalaman.Name = "LabelHalaman"
        LabelHalaman.Size = New Size(53, 20)
        LabelHalaman.TabIndex = 0
        LabelHalaman.Text = "Label1"
        ' 
        ' FormPertanyaan
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 433)
        Controls.Add(LabelHalaman)
        Controls.Add(ButtonSelanjutnya)
        Controls.Add(ButtonSebelumnya)
        Controls.Add(PanelPertanyaan)
        MaximizeBox = False
        Name = "FormPertanyaan"
        Text = "FormPertanyaan"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PanelPertanyaan As FlowLayoutPanel
    Friend WithEvents ButtonSebelumnya As Button
    Friend WithEvents ButtonSelanjutnya As Button
    Friend WithEvents LabelHalaman As Label
End Class
