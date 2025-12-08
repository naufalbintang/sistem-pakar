Imports System.Net.Mail

Module ValidasiEmail
    Public Function cekEmail(email As String) As Boolean
        Try
            Dim mail = New MailAddress(email)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Module
