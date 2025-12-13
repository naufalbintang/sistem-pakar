Imports System.Security.Cryptography.X509Certificates
Imports Microsoft.Data.SqlClient

Public Class FormRegister
    'variabel ui
    Private labelJudul As Label
    Private labelSubJudul As Label

    Private labelNIM As Label
    Private WithEvents textBoxNIM As TextBox

    Private labelNama As Label
    Private WithEvents textBoxNama As TextBox

    Private labelEmail As Label
    Private WithEvents textBoxEmail As TextBox

    Private labelPassword As Label
    Private WithEvents textBoxPassword As TextBox

    Private WithEvents buttonRegister As Button
    Private labelInfo As Label
    Private WithEvents labelLogin As Label

    Private Sub FormRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'setup form
        Me.Text = "Registrasi User Baru"
        Me.Size = New Size(450, 680)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.WhiteSmoke

        buatTampilan()
    End Sub

    Private Sub buatTampilan()
        Me.Controls.Clear()

        Dim formWidth As Integer = Me.ClientSize.Width
        Dim centerX As Integer = formWidth / 2

        'judul
        labelJudul = New Label()
        labelJudul.Text = "REGISTRASI"
        labelJudul.Font = New Font("Segoe UI", 16, FontStyle.Bold)
        labelJudul.ForeColor = Color.FromArgb(64, 64, 64)
        labelJudul.AutoSize = False
        labelJudul.Size = New Size(formWidth, 40)
        labelJudul.TextAlign = ContentAlignment.MiddleCenter
        labelJudul.Location = New Point(0, 30)
        Me.Controls.Add(labelJudul)

        labelSubJudul = New Label()
        labelSubJudul.Text = "Silakan Lengkapi Data Diri Anda"
        labelSubJudul.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        labelSubJudul.ForeColor = Color.Gray
        labelSubJudul.AutoSize = False
        labelSubJudul.Size = New Size(formWidth, 30)
        labelSubJudul.TextAlign = ContentAlignment.MiddleCenter
        labelSubJudul.Location = New Point(0, 65)
        Me.Controls.Add(labelSubJudul)

        'input field nim
        labelNIM = New Label()
        labelNIM.Text = "NIM / ID User"
        labelNIM.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        labelNIM.AutoSize = True
        labelNIM.Location = New Point(centerX - 165, 120)
        Me.Controls.Add(labelNIM)

        textBoxNIM = New TextBox()
        textBoxNIM.Font = New Font("Segoe UI", 11)
        textBoxNIM.Size = New Size(330, 30)
        textBoxNIM.Location = New Point(centerX - 165, 145)
        Me.Controls.Add(textBoxNIM)

        'input field nama
        labelNama = New Label()
        labelNama.Text = "Nama Lengkap"
        labelNama.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        labelNama.AutoSize = True
        labelNama.Location = New Point(centerX - 165, 190)
        Me.Controls.Add(labelNama)

        textBoxNama = New TextBox()
        textBoxNama.Font = New Font("Segoe UI", 11)
        textBoxNama.Size = New Size(330, 30)
        textBoxNama.Location = New Point(centerX - 165, 215)
        Me.Controls.Add(textBoxNama)

        'input field email
        labelEmail = New Label()
        labelEmail.Text = "Alamat Email"
        labelEmail.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        labelEmail.AutoSize = True
        labelEmail.Location = New Point(centerX - 165, 260)
        Me.Controls.Add(labelEmail)

        textBoxEmail = New TextBox()
        textBoxEmail.Font = New Font("Segoe UI", 11)
        textBoxEmail.Size = New Size(330, 30)
        textBoxEmail.Location = New Point(centerX - 165, 285)
        Me.Controls.Add(textBoxEmail)

        'input field password
        labelPassword = New Label()
        labelPassword.Text = "Password"
        labelPassword.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        labelPassword.AutoSize = True
        labelPassword.Location = New Point(centerX - 165, 330)
        Me.Controls.Add(labelPassword)

        textBoxPassword = New TextBox()
        textBoxPassword.Font = New Font("Segoe UI", 11)
        textBoxPassword.Size = New Size(330, 30)
        textBoxPassword.Location = New Point(centerX - 165, 355)
        textBoxPassword.UseSystemPasswordChar = True
        Me.Controls.Add(textBoxPassword)

        'tombol daftar
        buttonRegister = New Button()
        buttonRegister.Text = "DAFTAR SEKARANG"
        buttonRegister.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        buttonRegister.BackColor = Color.DodgerBlue
        buttonRegister.ForeColor = Color.White
        buttonRegister.FlatStyle = FlatStyle.Flat
        buttonRegister.FlatAppearance.BorderSize = 0
        buttonRegister.Size = New Size(330, 45)
        buttonRegister.Location = New Point(centerX - 165, 420)
        buttonRegister.Cursor = Cursors.Hand
        Me.Controls.Add(buttonRegister)

        'link login
        Dim panelLink As New Panel()
        panelLink.AutoSize = True
        panelLink.Location = New Point(0, 490)

        labelInfo = New Label()
        labelInfo.Text = "Sudah punya akun?"
        labelInfo.Font = New Font("Segoe UI", 9)
        labelInfo.AutoSize = True
        labelInfo.Location = New Point(0, 0)

        labelLogin = New Label()
        labelLogin.Text = "Login di sini"
        labelLogin.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        labelLogin.ForeColor = Color.DodgerBlue
        labelLogin.Cursor = Cursors.Hand
        labelLogin.AutoSize = True
        labelLogin.Location = New Point(labelInfo.Width + 5, 0)
        AddHandler labelLogin.Click, AddressOf labelLogin_Click

        panelLink.Controls.Add(labelInfo)
        panelLink.Controls.Add(labelLogin)
        Me.Controls.Add(panelLink)
        panelLink.Left = centerX - (panelLink.PreferredSize.Width / 2)
    End Sub

    'logika registrasi
    Private Sub buttonRegister_Click(sender As Object, e As EventArgs) Handles buttonRegister.Click
        Dim idUser As String = textBoxNIM.Text
        Dim nama As String = textBoxNama.Text
        Dim email As String = textBoxEmail.Text
        Dim passwordMentah As String = textBoxPassword.Text

        'validasi
        If idUser = "" Or nama = "" Or email = "" Or passwordMentah = "" Then
            MsgBox("Mohon lengkapi semua data.", MsgBoxStyle.Exclamation, "Peringatan")
            Return
        End If

        If Not ValidasiEmail.cekEmail(email) Then
            MsgBox("Format email tidak sesuai.", MsgBoxStyle.Exclamation, "Peringatan")
            Return
        End If

        Try
            'hash password
            Dim passwordHash As String = HashPassword.HashPassword(passwordMentah)

            Using conn = ModuleDB.getConnection()
                conn.Open()
                Dim query As String = "INSERT INTO Akun(Id_user, nama, email, password) VALUES(@idUser, @nama, @email, @password)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@idUser", idUser)
                    cmd.Parameters.AddWithValue("@nama", nama)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@password", passwordHash)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MsgBox("Registrasi berhasil! Silakan login.", MsgBoxStyle.Information, "Sukses")
            Me.Close()
        Catch ex As Exception
            MsgBox("Gagal menyimpan data: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub labelLogin_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class