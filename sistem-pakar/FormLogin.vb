Imports Microsoft.Data.SqlClient

Public Class FormLogin
    Private Sub LabelRegistrasi_Click(sender As Object, e As EventArgs) Handles LabelRegistrasi.Click
        Dim FormRegister As New FormRegister()
        FormRegister.ShowDialog()
    End Sub

    Private Sub ButtonLogin_Click(sender As Object, e As EventArgs) Handles ButtonLogin.Click

        Dim FormPassword As String = HashPassword.HashPassword(TextBoxPassword.Text)

        Try
            Using conn = ModuleDB.getConnection()
                conn.Open()

                Dim query = "SELECT Id_user, password, role FROM Akun WHERE Id_user = @nim"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@nim", TextBoxNIM.Text)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim NIMDB As String = reader("Id_user").ToString()
                            Dim passwordDB As String = reader("password").ToString()
                            Dim roleDB As String = reader("role").ToString()

                            If FormPassword = passwordDB Then
                                MsgBox("Login berhasil!", MsgBoxStyle.Information, "Sukses")
                                ModuleDB.NIMSekarang = NIMDB
                                If roleDB = "admin" Then
                                    Dim formAdmin As New FormAdmin()
                                    formAdmin.Show()
                                    Me.Hide()
                                Else
                                    Dim FormPertanyaan As New FormPertanyaan()
                                    FormPertanyaan.Show()
                                    Me.Hide()
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

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
    End Sub
End Class
