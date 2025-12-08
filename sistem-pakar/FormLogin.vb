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

                Dim query = "SELECT password FROM Akun WHERE Id_user = @nim"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@nim", TextBoxNIM.Text)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim passwordDB As String = reader("password").ToString()

                            If FormPassword = passwordDB Then
                                MsgBox("Login berhasil!", MsgBoxStyle.Information, "Sukses")
                                Dim FormPertanyaan As New FormPertanyaan()
                                FormPertanyaan.Show()
                                Me.Close()
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
End Class
