Imports Microsoft.Data.SqlClient

Public Class FormRegister
    Private Sub ButtonRegister_Click(sender As Object, e As EventArgs) Handles ButtonRegister.Click

        Dim Id_User As String = TextBoxNIM.Text
        Dim nama As String = TextBoxNama.Text
        Dim email As String = TextBoxEmail.Text
        Dim password As String = HashPassword.HashPassword(TextBoxPassword.Text)

        If TextBoxNIM.Text = "" Or TextBoxNama.Text = "" Or TextBoxEmail.Text = "" Or TextBoxPassword.Text = "" Then
            MsgBox("Silakan lengkapi form terlebih dahulu!", MsgBoxStyle.Exclamation, "Warning")
            Return
        End If

        If Not ValidasiEmail.cekEmail(email) Then
            MsgBox("Email tidak valid!", MsgBoxStyle.Exclamation, "Warning")
            Return
        End If



        Try
            Using conn = ModuleDB.getConnection()
                conn.Open()
                Dim query As String = "INSERT INTO Akun (Id_User, nama, email, password) VALUES (@Id_User, @nama, @email, @password)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Id_user", Id_User)
                    cmd.Parameters.AddWithValue("@nama", nama)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@password", password)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MsgBox("Berhasil melakukan registrasi. Silakan login.")
            Me.Close()
            Dim FormLogin As New FormLogin()
            FormLogin.Show()
        Catch ex As Exception
            MsgBox("Gagal memasukkan data ke database " & ex.Message)
        End Try
    End Sub

    Private Sub FormRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
    End Sub
End Class