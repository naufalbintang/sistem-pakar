Module ModuleInferensi
    'fungsi inferensi menerima input berupa array dan mengeluarkan output berupa string (topik)
    Public Function cekRekomendasiTopik(jawabanUser As Integer(), dtPertanyaan As DataTable) As Dictionary(Of String, Double)

        'validasi input
        If jawabanUser Is Nothing OrElse jawabanUser.Length < 20 Then
            Return Nothing
        End If

        'ambil data topik dari database
        Dim dtTopik As DataTable = ModuleDB.ambilDaftarTopik()

        'dictionary untuk simpan CF Combine tiap topik
        'key = ID topik, value = nilai keyakinan (0.0 - 1.0)
        Dim cfTopik As New Dictionary(Of String, Double)
        Dim namaTopik As New Dictionary(Of String, String)

        'inisialisasi skor 0 untuk semua topik
        For Each row As DataRow In dtTopik.Rows
            Dim id As String = row("Id_topik").ToString()
            Dim nama As String = row("nama_topik").ToString()
            cfTopik.Add(id, 0.0)
            namaTopik.Add(id, nama)
        Next

        'loop berdasarkan jawaban user
        For i As Integer = 0 To jawabanUser.Length - 1
            'jika user jawab "YA"
            If jawabanUser(i) = 1 Then
                Dim idTopikSoal As String = dtPertanyaan.Rows(i)("Id_topik").ToString()
                Dim bobotSoal As Double = Convert.ToDouble(dtPertanyaan.Rows(i)("bobot_pertanyaan"))

                'ambil nilai CF pakar dari database
                Dim cfPakar As Double = Convert.ToDouble(dtPertanyaan.Rows(i)("bobot_pertanyaan"))

                'mencari CF Gejala
                Dim cfUser As Double = 1.0 'user yakin menjawab YA
                Dim cfGejala As Double = cfPakar * cfUser

                'rumus kombinasi CF -> cfBaru = cfLama + cfGejala * (1 - cfLama)
                If cfTopik.ContainsKey(idTopikSoal) Then
                    Dim cfLama As Double = cfTopik(idTopikSoal)
                    Dim cfBaru As Double = cfLama + cfGejala * (1 - cfLama)

                    'update nilai
                    cfTopik(idTopikSoal) = cfBaru
                End If
            End If
        Next

        Dim hasilAkhir As New Dictionary(Of String, Double)

        For Each item In cfTopik
            Dim id As String = item.Key
            Dim skor As Double = item.Value

            If namaTopik.ContainsKey(id) Then
                Dim namaAsli As String = namaTopik(id)
                hasilAkhir.Add(namaAsli, skor)
            End If
        Next

        Return hasilAkhir
    End Function
End Module
