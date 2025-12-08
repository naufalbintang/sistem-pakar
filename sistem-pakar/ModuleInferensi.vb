Module ModuleInferensi
    'fungsi inferensi menerima input berupa array dan mengeluarkan output berupa string (topik)
    Public Function cekRekomendasiTopik(jawabanUser As Integer()) As String
        'validasi input
        If jawabanUser Is Nothing OrElse jawabanUser.Length < 20 Then
            Return "Error: data tidak lengkap"
        End If

        'penampung skor
        Dim skorTopik As New Dictionary(Of String, Integer) From {
            {"Rekayasa Perangkat Lunak", 0},
            {"AI dan Data Science", 0},
            {"Network dan Security", 0},
            {"Internet Of Things dan Robotika", 0},
            {"Multimedia dan Game Development", 0}
        }

        'penerapan rules mesin inferensi
        For i As Integer = 0 To 19
            If jawabanUser(i) = 1 Then
                Select Case i
                    ' === REKAYASA PERANGKAT LUNAK (0-3) ===
                    Case 0 : skorTopik("Rekayasa Perangkat Lunak") += 20
                    Case 1 : skorTopik("Rekayasa Perangkat Lunak") += 10
                    Case 2 : skorTopik("Rekayasa Perangkat Lunak") += 60
                    Case 3 : skorTopik("Rekayasa Perangkat Lunak") += 10

                    ' === AI & DATA SCIENCE (4-7) ===
                    Case 4 : skorTopik("AI dan Data Science") += 20
                    Case 5 : skorTopik("AI dan Data Science") += 10
                    Case 6 : skorTopik("AI dan Data Science") += 10
                    Case 7 : skorTopik("AI dan Data Science") += 60

                    ' === NETWORK & SECURITY (8-11) ===
                    Case 8 : skorTopik("Network dan Security") += 60
                    Case 9 : skorTopik("Network dan Security") += 10
                    Case 10 : skorTopik("Network dan Security") += 20
                    Case 11 : skorTopik("Network dan Security") += 10

                    ' === IOT & ROBOTIKA (12-15) ===
                    Case 12 : skorTopik("Internet Of Things dan Robotika") += 10
                    Case 13 : skorTopik("Internet Of Things dan Robotika") += 20
                    Case 14 : skorTopik("Internet Of Things dan Robotika") += 10
                    Case 15 : skorTopik("Internet Of Things dan Robotika") += 60

                    ' === MULTIMEDIA & GAME DEV (16-19) ===
                    Case 16 : skorTopik("Multimedia dan Game Development") += 10
                    Case 17 : skorTopik("Multimedia dan Game Development") += 60
                    Case 18 : skorTopik("Multimedia dan Game Development") += 10
                    Case 19 : skorTopik("Multimedia dan Game Development") += 20

                End Select
            End If
        Next

        'cari skor tertinggi
        Dim maxSkor As Integer = skorTopik.Values.Max()

        'jika user tidak menjawab "Ya" satupun
        If maxSkor = 0 Then
            Return "Minat tidak teridentifikasi (Anda menjawab Tidak pada semua pertanyaan)."
        End If

        'cari topik dengan skor tertinggi (bisa seri)
        Dim pemenang As New List(Of String)
        For Each topik In skorTopik
            If topik.Value = maxSkor Then
                pemenang.Add(topik.Key)
            End If
        Next

        'format output string
        If pemenang.Count = 1 Then
            Return pemenang(0)
        Else
            'jika ada 2 topik dengan skor sama
            Return String.Join(" & ", pemenang)
        End If
    End Function
End Module
