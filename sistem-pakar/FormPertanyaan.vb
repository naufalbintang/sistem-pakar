Public Class FormPertanyaan
    'variabel global
    Dim dtPertanyaan As DataTable
    Dim halamanSaatIni As Integer = 0
    Dim jumlahPerHalaman As Integer = 4
    Dim totalHalaman As Integer = 5

    'array untuk menyimpan jawaban sementara (soal 0-19)
    Dim jawabanUser(19) As String

    Private Sub FormPertanyaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ambil 20 data dari database
        dtPertanyaan = ModuleDB.AmbilSemuaPertanyaan()

        If dtPertanyaan.Rows.Count = 0 Then
            MsgBox("Data pertanyaan kosong!")
            Me.Close()
            Return
        End If

        'siapkan array jawaban
        For i As Integer = 0 To 19
            jawabanUser(i) = ""
        Next

        'tampilkan halaman pertama
        tampilkanHalaman(0)
    End Sub

    Sub tampilkanHalaman(halaman As Integer)
        Dim indexMulai As Integer = halaman * jumlahPerHalaman

        'loop untuk mengisi 4 slot
        For i As Integer = 1 To 4
            Dim indexData As Integer = indexMulai + (i - 1)

            'cari kontrol berdasarkan nama string
            Dim label As Label = Me.Controls.Find("LabelTanya" & i, True).FirstOrDefault()
            Dim radioButtonYa As RadioButton = Me.Controls.Find("RadioButtonYa" & i, True).FirstOrDefault()
            Dim radioButtonTidak As RadioButton = Me.Controls.Find("RadioButtonTidak" & i, True).FirstOrDefault()
            Dim groupBox As GroupBox = label.Parent

            'cek apakah datanya ada?
            If indexData < dtPertanyaan.Rows.Count Then
                groupBox.Visible = True

                'memasukkan teks soal dari database
                label.Text = dtPertanyaan.Rows(indexData)("teks_pertanyaan").ToString()

                'reset radio button
                radioButtonYa.Checked = False
                radioButtonTidak.Checked = False

                'load jawaban lama kalo user udah pernah jawab
                If jawabanUser(indexData) = "Y" Then
                    radioButtonYa.Checked = True
                ElseIf jawabanUser(indexData) = "T" Then
                    radioButtonTidak.Checked = True
                End If
            Else
                groupBox.Visible = False
            End If
        Next

        'atur status tombol
        ButtonSebelumnya.Enabled = (halaman > 0)

        If (indexMulai + jumlahPerHalaman) >= dtPertanyaan.Rows.Count Then
            ButtonSelanjutnya.Text = "Selesai"
        Else
            ButtonSelanjutnya.Text = "Selanjutnya"
        End If
    End Sub

    'tombol selanjutnya
    Private Sub ButtonSelanjutnya_Click(sender As Object, e As EventArgs) Handles ButtonSelanjutnya.Click
        SimpanJawabanSementara()

        'cek apakah sudah halaman terakhir
        If ButtonSelanjutnya.Text = "Selesai " Then
            'proses hitung skor / simpan ke db
            MsgBox("Jawaban berhasil disimpan!")
            'FormHasil.Show()
        Else
            halamanSaatIni += 1
            tampilkanHalaman(halamanSaatIni)
        End If

    End Sub

    'tombol sebelumnya
    Private Sub ButtonSebelumnya_Click(sender As Object, e As EventArgs) Handles ButtonSebelumnya.Click
        SimpanJawabanSementara()

        halamanSaatIni -= 1
        tampilkanHalaman(halamanSaatIni)
    End Sub

    'logika menyimpan jawaban ke array
    Sub SimpanJawabanSementara()
        Dim indexMulai As Integer = halamanSaatIni * jumlahPerHalaman

        For i As Integer = 1 To 4
            Dim indexData As Integer = indexMulai + (i - 1)

            'cek batasan data
            If indexData >= dtPertanyaan.Rows.Count Then Exit For

            Dim radioButtonYa As RadioButton = Me.Controls.Find("RadioButtonYa" & i, True).FirstOrDefault()
            Dim radioButtonTidak As RadioButton = Me.Controls.Find("RadioButtonTidak" & i, True).FirstOrDefault()

            If radioButtonYa.Checked Then
                jawabanUser(indexData) = "Y"
            ElseIf radioButtonTidak.Checked Then
                jawabanUser(indexData) = "T"
            End If
        Next
    End Sub


End Class