Imports Microsoft.Data.SqlClient

Public Class FormLogin

    Private labelJudul As Label
    Private labelSubJudul As Label

    Private labelNIM As Label
    Private WithEvents textBoxNIM As TextBox

    Private labelPassword As Label
    Private WithEvents textBoxPassword As TextBox

    Private WithEvents buttonLogin As Button
    Private labelInfo As Label
    Private WithEvents labelRegistrasi As Label

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'setup form
        Me.Text = "Login Sistem Pakar"
        Me.Size = New Size(450, 550)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.WhiteSmoke

        buatTampilan()
    End Sub

    Private Sub buatTampilan()
        Me.Controls.Clear()
        Dim centerX As Integer = Me.ClientSize.Width / 2

        'judul
        labelJudul = New Label()
        labelJudul.Text = "SELAMAT DATANG"
        labelJudul.Font = New Font("Segoe UI", 16, FontStyle.Bold)
        labelJudul.ForeColor = Color.FromArgb(64, 64, 64)
        labelJudul.AutoSize = False
        labelJudul.Size = New Size(Me.ClientSize.Width, 40)
        labelJudul.TextAlign = ContentAlignment.MiddleCenter
        labelJudul.Location = New Point(0, 50)
        Me.Controls.Add(labelJudul)

        labelSubJudul = New Label()
        labelSubJudul.Text = "Sistem Pakar Penentuan Topik Skripsi"
        labelSubJudul.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        labelSubJudul.AutoSize = False
        labelSubJudul.Size = New Size(Me.ClientSize.Width, 30)
        labelSubJudul.TextAlign = ContentAlignment.MiddleCenter
        labelSubJudul.Location = New Point(0, 85)
        Me.Controls.Add(labelSubJudul)

        'input NIM
        labelNIM = New Label()
        labelNIM.Text = "NIM / ID User"
        labelNIM.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        labelNIM.Location = New Point(50, 140)
        labelNIM.AutoSize = True
        Me.Controls.Add(labelNIM)

        textBoxNIM = New TextBox()
        textBoxNIM.Font = New Font("Segoe UI", 11)
        textBoxNIM.Location = New Point(50, 165)
        textBoxNIM.Size = New Size(330, 30)
        Me.Controls.Add(textBoxNIM)

        'input password
        labelPassword = New Label()
        labelPassword.Text = "Password"
        labelPassword.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        labelPassword.Location = New Point(50, 210)
        labelPassword.AutoSize = True
        Me.Controls.Add(labelPassword)

        textBoxPassword = New TextBox()
        textBoxPassword.Font = New Font("Segoe UI", 11)
        textBoxPassword.Location = New Point(50, 235)
        textBoxPassword.Size = New Size(330, 30)
        textBoxPassword.UseSystemPasswordChar = True
        Me.Controls.Add(textBoxPassword)

        'tombol login
        buttonLogin = New Button()
        buttonLogin.Text = "Login"
        buttonLogin.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        buttonLogin.BackColor = Color.DodgerBlue
        buttonLogin.ForeColor = Color.White
        buttonLogin.FlatStyle = FlatStyle.Flat
        buttonLogin.FlatAppearance.BorderSize = 0
        buttonLogin.Size = New Size(330, 45)
        buttonLogin.Location = New Point(50, 300)
        buttonLogin.Cursor = Cursors.Hand
        Me.Controls.Add(buttonLogin)

        'tombol register
        labelInfo = New Label()
        labelInfo.Text = "Belum punya akun?"
        labelInfo.Font = New Font("Segoe UI", 9)
        labelInfo.AutoSize = True
        labelInfo.Location = New Point(110, 370)
        Me.Controls.Add(labelInfo)

        labelRegistrasi = New Label()
        labelRegistrasi.Text = "Daftar di Sini"
        labelRegistrasi.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        labelRegistrasi.ForeColor = Color.DodgerBlue
        labelRegistrasi.Cursor = Cursors.Hand
        labelRegistrasi.AutoSize = True
        labelRegistrasi.Location = New Point(labelInfo.Right + 5, 370)
        Me.Controls.Add(labelRegistrasi)
    End Sub

    Private Sub buttonLogin_Click(sender As Object, e As EventArgs) Handles buttonLogin.Click
        Dim formPassword As String = HashPassword.HashPassword(textBoxPassword.Text)

        Try
            Using conn = ModuleDB.getConnection()
                conn.Open()

                Dim query As String = "SELECT Id_user, password, role FROM Akun WHERE Id_user=@nim"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@nim", textBoxNIM.Text)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim NIMDB As String = reader("Id_user").ToString()
                            Dim passwordDB As String = reader("password").ToString()
                            Dim roleDB As String = reader("role").ToString()

                            If formPassword = passwordDB Then
                                MsgBox("Login berhasil!", MsgBoxStyle.Information, "Sukses")
                                ModuleDB.NIMSekarang = NIMDB

                                'routing halaman berdasarkan role
                                If roleDB = "admin" Then
                                    Dim formAdmin As New FormAdmin()
                                    formAdmin.Show()
                                Else
                                    Dim formPertanyaan As New FormPertanyaan()
                                    formPertanyaan.Show()
                                End If
                                Me.Hide()
                            Else
                                MsgBox("Password salah!", MsgBoxStyle.Critical, "Gagal")
                            End If
                        Else
                            MsgBox("NIM tidak terdaftar.", MsgBoxStyle.Exclamation, "Gagal")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub labelRegistrasi_Click(sender As Object, e As EventArgs) Handles labelRegistrasi.Click
        Dim formRegistrasi As New FormRegister()
        formRegistrasi.ShowDialog()
    End Sub
End Class
