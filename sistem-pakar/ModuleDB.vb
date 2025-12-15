Imports Microsoft.Data.SqlClient
Imports Microsoft.IdentityModel.Protocols.OpenIdConnect

Module ModuleDB
    Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;" &
                "AttachDbFilename=|DataDirectory|\sistem-pakar.mdf;" &
                "Integrated Security=True;" &
                "Connect Timeout=30"

    Public Function getConnection() As SqlConnection
        Return New SqlConnection(connectionString)
    End Function

    'nim dari akun yang sedang aktif
    Public NIMSekarang As String = ""

    Public Function AmbilSemuaPertanyaan() As DataTable
        'siapkan tabel kosong
        Dim tabelPertanyaan As New DataTable()

        Try
            Using conn As SqlConnection = getConnection()
                'query
                Dim query As String = "Select * FROM Pertanyaan ORDER BY Id_pertanyaan ASC"

                Using cmd As New SqlCommand(query, conn)
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(tabelPertanyaan)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Gagal mengambil petanyaan: " & ex.Message)
        End Try

        Return tabelPertanyaan
    End Function

    Public Function ambilDaftarTopik() As DataTable
        Dim tabelTopik As New DataTable()
        Try
            Using conn As SqlConnection = getConnection()
                conn.Open()
                Dim query As String = "Select * From Topik"
                Using cmd As New SqlCommand(query, conn)
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(tabelTopik)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Gagal mengambil data: " & ex.Message)
        End Try
        Return tabelTopik
    End Function

    Public Sub simpanHasilKonsultasi(idUser As String, jawabanUser As Integer(), dtPertanyaan As DataTable, namaPemenang As String)
        Using conn As SqlConnection = getConnection()
            conn.Open()
            Dim trans As SqlTransaction = conn.BeginTransaction()

            Try
                'cari id topik pemenang
                Dim idTopik As String = DBNull.Value.ToString()

                If namaPemenang <> "" AndAlso namaPemenang <> "Minat belum terlihat" Then
                    Dim queryGetId As String = "SELECT Id_topik FROM Topik WHERE nama_topik = @namaPemenang"
                    Using cmdId As New SqlCommand(queryGetId, conn, trans)
                        cmdId.Parameters.AddWithValue("@namaPemenang", namaPemenang)
                        Dim result = cmdId.ExecuteScalar()
                        If result IsNot Nothing Then idTopik = result.ToString()
                    End Using
                End If

                'insert ke tabel konsultasi
                Dim queryKonsul As String = "INSERT INTO Konsultasi(Id_topik, Id_user) OUTPUT INSERTED.Id_konsultasi VALUES (@idTopik, @idUser)"

                Dim idKonsultasiBaru As Integer = 0
                Using cmdKonsul As New SqlCommand(queryKonsul, conn, trans)
                    If String.IsNullOrEmpty(idTopik) Then
                        cmdKonsul.Parameters.AddWithValue("@idTopik", DBNull.Value)
                    Else
                        cmdKonsul.Parameters.AddWithValue("@idTopik", idTopik)
                    End If
                    cmdKonsul.Parameters.AddWithValue("@idUser", idUser)

                    'eksekusi dan ambil id baru
                    idKonsultasiBaru = Convert.ToInt32(cmdKonsul.ExecuteScalar())
                End Using

                Dim queryJawab As String = "INSERT INTO Jawaban_Mhs(Id_konsultasi, Id_pertanyaan, jawaban) VALUES(@idKonsultasi, @idPertanyaan, @jawaban)"

                Using cmdJawab As New SqlCommand(queryJawab, conn, trans)
                    cmdJawab.Parameters.Add(New SqlParameter("@idKonsultasi", SqlDbType.Int))
                    cmdJawab.Parameters.Add(New SqlParameter("@idPertanyaan", SqlDbType.Char, 4))
                    cmdJawab.Parameters.Add(New SqlParameter("@jawaban", SqlDbType.VarChar, 10))

                    'loop semua jawaban user
                    For i As Integer = 0 To jawabanUser.Length - 1
                        'ambil id pertanyaan dari tabel
                        Dim idPertanyaan As String = dtPertanyaan.Rows(i)("Id_pertanyaan").ToString()
                        Dim teksJawaban As String = If(jawabanUser(i) = 1, "Ya", "Tidak")

                        'set nilai parameter
                        cmdJawab.Parameters("@idKonsultasi").Value = idKonsultasiBaru
                        cmdJawab.Parameters("@idPertanyaan").Value = idPertanyaan
                        cmdJawab.Parameters("@jawaban").Value = teksJawaban

                        'eksekusi per baris
                        cmdJawab.ExecuteNonQuery()
                    Next
                End Using

                'simpan permanen
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception("Gagal menyimpan ke database: " & ex.Message)
            End Try
        End Using
    End Sub
End Module