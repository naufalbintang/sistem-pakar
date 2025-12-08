Public Class FormPertanyaan
    'variabel global
    Dim dtPertanyaan As DataTable
    Dim halamanSaatIni As Integer = 0
    Dim jumlahPerHalaman As Integer = 4

    'array untuk menyimpan jawaban user
    Dim jawabanUser(19) As String

    'saat form dibuka
    Private Sub FormPertanyaan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ambil pertanyaan dari database
        dtPertanyaan = ModuleDB.AmbilSemuaPertanyaan()

        'validasi jika kosong
        If dtPertanyaan.Rows.Count = 0 Then
            MsgBox("Data tidak ditemukan! Silakan cek database.", MsgBoxStyle.Critical, "Gagal")
            Me.Close()
            Return
        End If

        'bersihkan array jawaban
        For i As Integer = 0 To 19
            jawabanUser(i) = ""
        Next

        'tampilkan halaman
        tampilkanHalaman(0)
    End Sub

    'render halaman
    Sub tampilkanHalaman(halaman As Integer)
        'hapus semua kontrol dalam panel
        PanelPertanyaan.Controls.Clear()

        'menentukan index start dan end halaman ini
        Dim indexMulai As Integer = halaman * jumlahPerHalaman
        Dim indexAkhir As Integer = Math.Min(indexMulai + jumlahPerHalaman - 1, dtPertanyaan.Rows.Count - 1)
        Dim totalHalaman As Integer = Math.Ceiling(dtPertanyaan.Rows.Count / jumlahPerHalaman)

        'loop untuk membuat card pertanyaan
        For i As Integer = indexMulai To indexAkhir
            'buat wadah (groupbox)
            Dim groupBox As New GroupBox()
            groupBox.Name = "GroupBox" & i
            groupBox.Text = ""
            groupBox.Width = PanelPertanyaan.Width - 40
            groupBox.Height = 130
            groupBox.BackColor = Color.White
            groupBox.Margin = New Padding(0, 0, 0, 15)

            'buat label soal
            Dim label As New Label()
            label.Text = (i + 1) & ". " & dtPertanyaan.Rows(i)("teks_pertanyaan").ToString()
            label.Location = New Point(15, 20)
            label.AutoSize = False
            label.Size = New Size(groupBox.Width - 30, 50)
            label.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            groupBox.Controls.Add(label)


            'buat radio button "YA"
            Dim radioButtonYa As New RadioButton()
            radioButtonYa.Text = "Ya"
            radioButtonYa.Location = New Point(20, 80)
            radioButtonYa.Font = New Font("Segoe UI", 9)
            radioButtonYa.AutoSize = True
            groupBox.Controls.Add(radioButtonYa)

            'buat radio button "TIDAK"
            Dim radioButtonTidak As New RadioButton()
            radioButtonTidak.Text = "Tidak"
            radioButtonTidak.Location = New Point(100, 80)
            radioButtonTidak.Font = New Font("Segoe UI", 9)
            radioButtonTidak.AutoSize = True
            groupBox.Controls.Add(radioButtonTidak)

            'masukkan kotak ke panel
            PanelPertanyaan.Controls.Add(groupBox)
        Next

        'update info halaman
        If LabelHalaman IsNot Nothing Then
            LabelHalaman.Text = "Halaman " & (halaman + 1) & " dari " & totalHalaman
        End If

        'atur status tombol
        ButtonSebelumnya.Enabled = (halaman > 0)

        If (indexMulai + jumlahPerHalaman) >= dtPertanyaan.Rows.Count Then
            ButtonSelanjutnya.Text = "Selesai"
            ButtonSelanjutnya.BackColor = Color.ForestGreen
        Else
            ButtonSelanjutnya.Text = "Selanjutnya"
            ButtonSelanjutnya.BackColor = Color.DodgerBlue
        End If
    End Sub

    'simpan jawaban sementara
    Sub simpanJawabanSementara()
        'cek satu per satu group box yang tampil
        For Each groupBox As Control In PanelPertanyaan.Controls
            If TypeOf groupBox Is GroupBox Then
                'ambil id soal dari nama group box (misal GroupBox5 -> ambil angka 5)
                Dim indexSoal As Integer = CInt(groupBox.Name.Replace("GroupBox", ""))

                'cari radio button di dalam group box tersebut
                Dim radioButtonYa As RadioButton = Nothing
                Dim radioButtonTidak As RadioButton = Nothing

                For Each c As Control In groupBox.Controls
                    If TypeOf c Is RadioButton Then
                        If c.Text = "Ya" Then radioButtonYa = c
                        If c.Text = "Tidak" Then radioButtonTidak = c
                    End If
                Next

                'simpan ke array global
                If radioButtonYa IsNot Nothing AndAlso radioButtonYa.Checked Then
                    jawabanUser(indexSoal) = "Y"
                ElseIf radioButtonTidak IsNot Nothing AndAlso radioButtonTidak.checked Then
                    jawabanUser(indexSoal) = "T"
                End If
            End If
        Next
    End Sub

    'tombol selanjutnya
    Private Sub ButtonSelanjutnya_Click(sender As Object, e As EventArgs) Handles ButtonSelanjutnya.Click
        simpanJawabanSementara()

        If ButtonSelanjutnya.Text = "Selesai" Then
            'cek apakah semua soal sudah dijawab?
            Dim adaYangKosong As Boolean = False
            For i As Integer = 0 To dtPertanyaan.Rows.Count - 1
                If jawabanUser(i) = "" Then
                    adaYangKosong = True
                    Exit For
                End If
            Next

            If adaYangKosong Then
                Dim response = MsgBox("Masih ada soal yang belum dijawab. Yakin mau selesai?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation)
                If response = MsgBoxResult.No Then Return
            End If

            MsgBox("Jawaban anda telah disimpan!", MsgBoxStyle.Information)
            Dim FormHasil As New FormHasil()
            Me.Hide()
            FormHasil.Show()
        Else
            halamanSaatIni += 1
            tampilkanHalaman(halamanSaatIni)
        End If
    End Sub

    'tombol sebelumnya
    Private Sub ButtonSebelumnya_Click(sender As Object, e As EventArgs) Handles ButtonSebelumnya.Click
        simpanJawabanSementara()
        halamanSaatIni -= 1
        tampilkanHalaman(halamanSaatIni)
    End Sub
End Class