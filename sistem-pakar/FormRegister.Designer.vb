<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRegister
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
        TextBoxNIM = New TextBox()
        LabelNIM = New Label()
        TextBoxNama = New TextBox()
        LabelNama = New Label()
        TextBoxEmail = New TextBox()
        LabelEmail = New Label()
        TextBoxPassword = New TextBox()
        LabelPassword = New Label()
        ButtonRegister = New Button()
        SuspendLayout()
        ' 
        ' TextBoxNIM
        ' 
        TextBoxNIM.Location = New Point(209, 76)
        TextBoxNIM.Name = "TextBoxNIM"
        TextBoxNIM.Size = New Size(358, 27)
        TextBoxNIM.TabIndex = 3
        ' 
        ' LabelNIM
        ' 
        LabelNIM.AutoSize = True
        LabelNIM.Location = New Point(209, 39)
        LabelNIM.Name = "LabelNIM"
        LabelNIM.Size = New Size(37, 20)
        LabelNIM.TabIndex = 2
        LabelNIM.Text = "NIM"
        ' 
        ' TextBoxNama
        ' 
        TextBoxNama.Location = New Point(209, 157)
        TextBoxNama.Name = "TextBoxNama"
        TextBoxNama.Size = New Size(358, 27)
        TextBoxNama.TabIndex = 5
        ' 
        ' LabelNama
        ' 
        LabelNama.AutoSize = True
        LabelNama.Location = New Point(209, 120)
        LabelNama.Name = "LabelNama"
        LabelNama.Size = New Size(49, 20)
        LabelNama.TabIndex = 4
        LabelNama.Text = "Nama"
        ' 
        ' TextBoxEmail
        ' 
        TextBoxEmail.Location = New Point(209, 238)
        TextBoxEmail.Name = "TextBoxEmail"
        TextBoxEmail.Size = New Size(358, 27)
        TextBoxEmail.TabIndex = 7
        ' 
        ' LabelEmail
        ' 
        LabelEmail.AutoSize = True
        LabelEmail.Location = New Point(209, 201)
        LabelEmail.Name = "LabelEmail"
        LabelEmail.Size = New Size(46, 20)
        LabelEmail.TabIndex = 6
        LabelEmail.Text = "Email"
        ' 
        ' TextBoxPassword
        ' 
        TextBoxPassword.Location = New Point(209, 319)
        TextBoxPassword.Name = "TextBoxPassword"
        TextBoxPassword.Size = New Size(358, 27)
        TextBoxPassword.TabIndex = 9
        TextBoxPassword.UseSystemPasswordChar = True
        ' 
        ' LabelPassword
        ' 
        LabelPassword.AutoSize = True
        LabelPassword.Location = New Point(209, 282)
        LabelPassword.Name = "LabelPassword"
        LabelPassword.Size = New Size(70, 20)
        LabelPassword.TabIndex = 8
        LabelPassword.Text = "Password"
        ' 
        ' ButtonRegister
        ' 
        ButtonRegister.Location = New Point(353, 371)
        ButtonRegister.Name = "ButtonRegister"
        ButtonRegister.Size = New Size(94, 29)
        ButtonRegister.TabIndex = 10
        ButtonRegister.Text = "Register"
        ButtonRegister.UseVisualStyleBackColor = True
        ' 
        ' FormRegister
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(ButtonRegister)
        Controls.Add(TextBoxPassword)
        Controls.Add(LabelPassword)
        Controls.Add(TextBoxEmail)
        Controls.Add(LabelEmail)
        Controls.Add(TextBoxNama)
        Controls.Add(LabelNama)
        Controls.Add(TextBoxNIM)
        Controls.Add(LabelNIM)
        Name = "FormRegister"
        Text = "FormRegister"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TextBoxNIM As TextBox
    Friend WithEvents LabelNIM As Label
    Friend WithEvents TextBoxNama As TextBox
    Friend WithEvents LabelNama As Label
    Friend WithEvents TextBoxEmail As TextBox
    Friend WithEvents LabelEmail As Label
    Friend WithEvents TextBoxPassword As TextBox
    Friend WithEvents LabelPassword As Label
    Friend WithEvents ButtonRegister As Button
End Class
