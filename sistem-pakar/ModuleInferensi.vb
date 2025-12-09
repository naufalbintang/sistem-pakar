Module ModuleInferensi
    'fungsi inferensi menerima input berupa array dan mengeluarkan output berupa string (topik)
    Public Function cekRekomendasiTopik(jawabanUser As Integer(), dtPertanyaan As DataTable) As List(Of String)

        'validasi input
        If jawabanUser Is Nothing OrElse jawabanUser.Length < 20 Then
            Return New List(Of String) From {"Error: Data tidak lengkap"}
        End If

        'ambil data topik dari database
        Dim dtTopik As DataTable = ModuleDB.ambilDaftarTopik()

        'dictionary untuk skor topik (key, value)
        'contoh = {"T01" : (skor)}
        Dim skorTopik As New Dictionary(Of String, Integer)

        'dictionary untuk menyimpan nama topik (key, value)
        'contoh = {"T01":"Rekayasa Perangkat Lunak"} 
        Dim namaTopik As New Dictionary(Of String, String)

        'inisialisasi skor 0 untuk semua topik
        For Each row As DataRow In dtTopik.Rows
            Dim id As String = row("Id_topik").ToString()
            Dim nama As String = row("nama_topik").ToString()

            skorTopik.Add(id, 0)
            namaTopik.Add(id, nama)
        Next

        'loop berdasarkan jawaban user
        For i As Integer = 0 To jawabanUser.Length - 1
            'jika user jawab "YA"
            If jawabanUser(i) = 1 Then
                Dim idTopikSoal As String = dtPertanyaan.Rows(i)("Id_topik").ToString()
                Dim bobotSoal As Integer = Convert.ToInt32(dtPertanyaan.Rows(i)("bobot_pertanyaan"))

                'tambahkan bobot ke topik yang sesuai
                If skorTopik.ContainsKey(idTopikSoal) Then
                    skorTopik(idTopikSoal) += bobotSoal
                End If
            End If
        Next

        'cari skor tertinggi
        Dim maxSkor As Integer = skorTopik.Values.Max()

        'jika user tidak menjawab "Ya" satupun
        If maxSkor = 0 Then
            Return New List(Of String) From {"Minat tidak teridentifikasi (Anda menjawab Tidak pada semua pertanyaan)."}
        End If

        'cari topik dengan skor tertinggi (bisa seri)
        Dim pemenang As New List(Of String)
        For Each keyValue In skorTopik
            If keyValue.Value = maxSkor Then
                pemenang.Add(namaTopik(keyValue.Key))
            End If
        Next

        Return pemenang
    End Function
End Module
