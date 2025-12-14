Public Class FormPertanyaan
    'variabel ui
    Private panelHeader As Panel
    Private labelJudul As Label
    Private labelHalaman As Label

    Private panelKonten As FlowLayoutPanel

    Private panelFooter As Panel
    Private WithEvents buttonSebelumnya As Button
    Private WithEvents buttonSelanjutnya As Button

    'variabel logika
    Dim dtPertanyaan As DataTable
    Dim halamanSaatIni As Integer = 0
    Dim jumlahPerHalaman As Integer = 4
    Dim jawabanUser(19) As Integer

    Private Sub FormPertanyaan_Loac(sender As Object, e As EventArgs) Handles MyBase.Load
        'setup form
        Me.Text = "Sistem Pakar Menentukan Topik Skripsi"
        Me.Size = New Size(850, 600)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.WhiteSmoke

        buatTampilan()

        loadDataPertanyaan()
    End Sub

    'membuat ui
    Private Sub buatTampilan()
        Me.Controls.Clear()

        'header panel
        panelHeader = New Panel()
        panelHeader.Dock = DockStyle.Top
        panelHeader.Height = 60
        panelHeader.BackColor = Color.DodgerBlue
        Me.Controls.Add(panelHeader)

        labelJudul = New Label()
        labelJudul.Text = "DAFTAR PERTANYAAN"
        labelJudul.ForeColor = Color.White
        labelJudul.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        labelJudul.AutoSize = True
        labelJudul.Location = New Point(20, 18)
        panelHeader.Controls.Add(labelJudul)

        labelHalaman = New Label()
        labelHalaman.Text = "Halaman 1 dari X"
        labelHalaman.ForeColor = Color.White
        labelHalaman.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        labelHalaman.AutoSize = False
        labelHalaman.TextAlign = ContentAlignment.MiddleRight
        labelHalaman.Size = New Size(200, 30)
        labelHalaman.Location = New Point(Me.ClientSize.Width - 220, 15)
        panelHeader.Controls.Add(labelHalaman)

        'footer (navigasi)
        panelFooter = New Panel()
        panelFooter.Dock = DockStyle.Bottom
        panelFooter.Height = 70
        panelFooter.BackColor = Color.White
        Me.Controls.Add(panelFooter)

        'garis pemisah halus di atas footer
        Dim panelGaris As New Panel()
        panelGaris.Dock = DockStyle.Top
        panelGaris.Height = 1
        panelGaris.BackColor = Color.LightGray
        panelFooter.Controls.Add(panelGaris)

        'button sebelumnya
        buttonSebelumnya = New Button()
        buttonSebelumnya.Text = "<< Sebelumnya"
        buttonSebelumnya.Size = New Size(150, 40)
        buttonSebelumnya.Location = New Point(20, 15)
        buttonSebelumnya.FlatStyle = FlatStyle.Flat
        buttonSebelumnya.BackColor = Color.WhiteSmoke
        buttonSebelumnya.ForeColor = Color.DimGray
        buttonSebelumnya.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        buttonSebelumnya.Cursor = Cursors.Hand
        panelFooter.Controls.Add(buttonSebelumnya)

        'button selanjutnya
        buttonSelanjutnya = New Button()
        buttonSelanjutnya.Text = "Selanjutnya >>"
        buttonSelanjutnya.Size = New Size(150, 40)
        buttonSelanjutnya.Location = New Point(Me.ClientSize.Width - 190, 15)
        buttonSelanjutnya.FlatStyle = FlatStyle.Flat
        buttonSelanjutnya.BackColor = Color.DodgerBlue
        buttonSelanjutnya.ForeColor = Color.White
        buttonSelanjutnya.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        buttonSelanjutnya.Cursor = Cursors.Hand
        buttonSelanjutnya.FlatAppearance.BorderSize = 0
        panelFooter.Controls.Add(buttonSelanjutnya)

        'panel konten (scrollable)
        panelKonten = New FlowLayoutPanel()
        panelKonten.Dock = DockStyle.Fill
        panelKonten.AutoScroll = True
        panelKonten.FlowDirection = FlowDirection.TopDown
        panelKonten.WrapContents = False
        panelKonten.BackColor = Color.WhiteSmoke
        panelKonten.Padding = New Padding(20, 20, 0, 20)
        Me.Controls.Add(panelKonten)
        panelKonten.BringToFront()
    End Sub

    'logika utama
    Private Sub loadDataPertanyaan()
        'ambil pertanyaan dari databse
        dtPertanyaan = ModuleDB.AmbilSemuaPertanyaan()

        If dtPertanyaan.Rows.Count = 0 Then
            MsgBox("Data pertanyaan kosong! Silakan cek database.", MsgBoxStyle.Critical, "Error")
            Me.Close()
            Return
        End If

        'reset jawaban user (-1 berarti belum terjawab)
        For i As Integer = 0 To 19
            jawabanUser(i) = -1
        Next

        'tampilkan halaman pertama
        tampilkanHalaman(0)
    End Sub

    Private Sub tampilkanHalaman(halaman As Integer)
        'hapus card pertanyaan lama
        panelKonten.Controls.Clear()

        Dim indexMulai As Integer = halaman * jumlahPerHalaman
        Dim indexAkhir As Integer = Math.Min(indexMulai + jumlahPerHalaman - 1, dtPertanyaan.Rows.Count - 1)
        Dim totalHalaman As Integer = Math.Ceiling(dtPertanyaan.Rows.Count / jumlahPerHalaman)

        'loop card pertanyaan
        For i As Integer = indexMulai To indexAkhir
            'wadah card
            Dim cardPanel As New Panel()
            cardPanel.Name = "Card" & i
            cardPanel.Width = panelKonten.ClientSize.Width - 60
            cardPanel.Height = 110
            cardPanel.BackColor = Color.White
            cardPanel.Margin = New Padding(0, 0, 0, 15) 'jarak antar card
            cardPanel.BorderStyle = BorderStyle.FixedSingle

            'label soal
            Dim labelSoal As New Label()
            labelSoal.Text = (i + 1) & ". " & dtPertanyaan.Rows(i)("teks_pertanyaan").ToString()
            labelSoal.Font = New Font("Segoe UI", 11, FontStyle.Regular)
            labelSoal.Location = New Point(15, 15)
            labelSoal.AutoSize = False
            labelSoal.Size = New Size(cardPanel.Width - 30, 50)
            cardPanel.Controls.Add(labelSoal)

            'radio button YA
            Dim rbYa As New RadioButton()
            rbYa.Text = "Ya"
            rbYa.Font = New Font("Segoe UI", 10)
            rbYa.Location = New Point(20, 70)
            rbYa.AutoSize = True
            rbYa.Cursor = Cursors.Hand
            If jawabanUser(i) = 1 Then rbYa.Checked = True
            cardPanel.Controls.Add(rbYa)

            'radio button TIDAK
            Dim rbTidak As New RadioButton()
            rbTidak.Text = "Tidak"
            rbTidak.Font = New Font("Segoe UI", 10)
            rbTidak.Location = New Point(100, 70)
            rbTidak.AutoSize = True
            rbTidak.Cursor = Cursors.Hand
            If jawabanUser(i) = 0 Then rbTidak.Checked = True
            cardPanel.Controls.Add(rbTidak)

            panelKonten.Controls.Add(cardPanel)
        Next

        'update info halaman
        labelHalaman.Text = "Halaman " & (halaman + 1) & " dari " & totalHalaman

        'update status tombol
        buttonSebelumnya.Enabled = (halaman > 0)

        If (indexMulai + jumlahPerHalaman) >= dtPertanyaan.Rows.Count Then
            buttonSelanjutnya.Text = "SELESAI"
            buttonSelanjutnya.BackColor = Color.DodgerBlue
        Else
            buttonSelanjutnya.Text = "Selanjutnya >>"
            buttonSelanjutnya.BackColor = Color.DodgerBlue
        End If
    End Sub

    Private Sub simpanJawabanSementara()
        'loop cari panel card di dalam FlowLayout
        For Each ctrl As Control In panelKonten.Controls
            If TypeOf ctrl Is Panel AndAlso ctrl.Name.StartsWith("Card") Then
                Dim indexSoal As Integer = CInt(ctrl.Name.Replace("Card", ""))

                Dim rbYa As RadioButton = Nothing
                Dim rbTidak As RadioButton = Nothing

                'cari radio button di dalam card
                For Each c As Control In ctrl.Controls
                    If TypeOf c Is RadioButton Then
                        If c.Text = "Ya" Then rbYa = c
                        If c.Text = "Tidak" Then rbTidak = c
                    End If
                Next

                'simpan ke array
                If rbYa IsNot Nothing AndAlso rbYa.Checked Then
                    jawabanUser(indexSoal) = 1
                ElseIf rbTidak IsNot Nothing AndAlso rbTidak.Checked Then
                    jawabanUser(indexSoal) = 0
                End If
            End If
        Next
    End Sub

    'event handlers
    Private Sub buttonSelanjutnya_Click(sender As Object, e As EventArgs) Handles buttonSelanjutnya.Click
        simpanJawabanSementara()

        'jika tombol tulisannya SELESAI
        If buttonSelanjutnya.Text = "SELESAI" Then
            'validasi jawaban kosong
            Dim adaKosong As Boolean = False
            For i As Integer = 0 To dtPertanyaan.Rows.Count - 1
                If jawabanUser(i) = -1 Then
                    adaKosong = True
                    Exit For
                End If
            Next

            If adaKosong Then
                MsgBox("Masih ada soal yang belum dijawab! Mohon periksa kembali.", MsgBoxStyle.Exclamation, "Belum Selesai")
                Return
            End If

            'hitung hasil
            Dim hasilRekomendasi As Dictionary(Of String, Double) = ModuleInferensi.cekRekomendasiTopik(jawabanUser, dtPertanyaan)

            'cari juara 1 untuk disimpan ke db
            Dim juaraPertama As String = ""
            If hasilRekomendasi.Count > 0 Then
                'mengurutkan dan mengambil yang terbesar
                Dim sorted = From entry In hasilRekomendasi Order By entry.Value Descending Select entry
                juaraPertama = sorted.First().Key

                'jika skor 0 semua
                If hasilRekomendasi(juaraPertama) = 0 Then juaraPertama = ""
            End If

            'simpan ke database
            Try
                ModuleDB.simpanHasilKonsultasi(ModuleDB.NIMSekarang, jawabanUser, dtPertanyaan, juaraPertama)
                MsgBox("Terima kasih! Jawaban Anda telah disimpan.", MsgBoxStyle.Information, "Sukses")
            Catch ex As Exception
                MsgBox("Gagal menyimpan ke database." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try

            'buka form hasil
            Dim formHasil As New FormHasil()
            formHasil.dataSkor = hasilRekomendasi
            Me.Hide()
            formHasil.Show()
        Else
            'pindah halaman next
            halamanSaatIni += 1
            tampilkanHalaman(halamanSaatIni)
        End If
    End Sub

    Private Sub buttonSebelumnya_Click(sender As Object, e As EventArgs) Handles buttonSebelumnya.Click
        simpanJawabanSementara()
        halamanSaatIni -= 1
        tampilkanHalaman(halamanSaatIni)
    End Sub

    ' matikan aplikasi saat diclose
    Private Sub FormPertanyaan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
End Class