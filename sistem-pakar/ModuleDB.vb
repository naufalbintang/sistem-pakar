Imports Microsoft.Data.SqlClient

Module ModuleDB
    Public ReadOnly Property connectionString As String
        Get
            Return "Data Source=(LocalDB)\MSSQLLocalDB;" &
                "AttachDbFilename=|DataDirectory|\sistem-pakar.mdf;" &
                "Integrated Security=True;" &
                "Connect Timeout=30"
        End Get
    End Property

    Public Function getConnection() As SqlConnection
        Return New SqlConnection(connectionString)
    End Function

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
End Module