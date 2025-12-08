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
End Module