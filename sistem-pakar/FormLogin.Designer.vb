<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        LabelNIM = New Label()
        TextBoxNIM = New TextBox()
        TextBoxPassword = New TextBox()
        LabelPassword = New Label()
        Label1 = New Label()
        LabelRegistrasi = New Label()
        ButtonLogin = New Button()
        SuspendLayout()
        ' 
        ' LabelNIM
        ' 
        LabelNIM.AutoSize = True
        LabelNIM.Location = New Point(221, 86)
        LabelNIM.Name = "LabelNIM"
        LabelNIM.Size = New Size(37, 20)
        LabelNIM.TabIndex = 0
        LabelNIM.Text = "NIM"
        ' 
        ' TextBoxNIM
        ' 
        TextBoxNIM.Location = New Point(221, 118)
        TextBoxNIM.Name = "TextBoxNIM"
        TextBoxNIM.Size = New Size(358, 27)
        TextBoxNIM.TabIndex = 1
        ' 
        ' TextBoxPassword
        ' 
        TextBoxPassword.Location = New Point(221, 212)
        TextBoxPassword.Name = "TextBoxPassword"
        TextBoxPassword.Size = New Size(358, 27)
        TextBoxPassword.TabIndex = 3
        TextBoxPassword.UseSystemPasswordChar = True
        ' 
        ' LabelPassword
        ' 
        LabelPassword.AutoSize = True
        LabelPassword.Location = New Point(221, 180)
        LabelPassword.Name = "LabelPassword"
        LabelPassword.Size = New Size(70, 20)
        LabelPassword.TabIndex = 2
        LabelPassword.Text = "Password"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(283, 286)
        Label1.Name = "Label1"
        Label1.Size = New Size(156, 20)
        Label1.TabIndex = 4
        Label1.Text = "Belum Memiliki Akun?"
        ' 
        ' LabelRegistrasi
        ' 
        LabelRegistrasi.AutoSize = True
        LabelRegistrasi.ForeColor = SystemColors.Highlight
        LabelRegistrasi.Location = New Point(435, 286)
        LabelRegistrasi.Name = "LabelRegistrasi"
        LabelRegistrasi.Size = New Size(73, 20)
        LabelRegistrasi.TabIndex = 5
        LabelRegistrasi.Text = "Registrasi"
        ' 
        ' ButtonLogin
        ' 
        ButtonLogin.Location = New Point(304, 346)
        ButtonLogin.Name = "ButtonLogin"
        ButtonLogin.Size = New Size(192, 29)
        ButtonLogin.TabIndex = 6
        ButtonLogin.Text = "Login"
        ButtonLogin.UseVisualStyleBackColor = True
        ' 
        ' FormLogin
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(ButtonLogin)
        Controls.Add(LabelRegistrasi)
        Controls.Add(Label1)
        Controls.Add(TextBoxPassword)
        Controls.Add(LabelPassword)
        Controls.Add(TextBoxNIM)
        Controls.Add(LabelNIM)
        MaximizeBox = False
        Name = "FormLogin"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LabelNIM As Label
    Friend WithEvents TextBoxNIM As TextBox
    Friend WithEvents TextBoxPassword As TextBox
    Friend WithEvents LabelPassword As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LabelRegistrasi As Label
    Friend WithEvents ButtonLogin As Button

End Class
