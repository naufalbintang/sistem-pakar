Imports System.Drawing.Printing

Public Class FormHasil
    'variabel penampung data skor
    Public dataSkor As Dictionary(Of String, Double)

    'variabel kontrol ui
    Private labelHeaderNIM As Label
    Private labelJudulUtama As Label
    Private labelRekomendasi As Label
    Private panelGrafik As Panel
    Private gridSkor As DataGridView
    Private buttonTutup As Button

    ' Variabel Print
    Private buttonPrint As Button
    Private WithEvents printDoc As New PrintDocument
    Private printPreview As New PrintPreviewDialog

    'variabel untuk grafik
    Private maxSkorGrafik As Double = 100.0

    Private Sub FormHasil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'setup form
        Me.Text = "Hasil Analisis Sistem Pakar"
        Me.Size = New Size(600, 900)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.WhiteSmoke
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        'render ui
        buatTampilan()

        'isi data
        tampilkanData()
    End Sub

    'desain ui
    Sub buatTampilan()
        'label nim (header)
        labelHeaderNIM = New Label()
        labelHeaderNIM.Text = "NIM User: -"
        labelHeaderNIM.Font = New Font("Segoe UI", 10, FontStyle.Italic)
        labelHeaderNIM.ForeColor = Color.Gray
        labelHeaderNIM.AutoSize = True
        labelHeaderNIM.Location = New Point(20, 20)
        Me.Controls.Add(labelHeaderNIM)

        'label judul
        labelJudulUtama = New Label()
        labelJudulUtama.Text = "Rekomendasi Topik Skripsi Anda: "
        labelJudulUtama.Font = New Font("Segoe UI", 12, FontStyle.Regular)
        labelJudulUtama.AutoSize = False
        labelJudulUtama.Size = New Size(Me.ClientSize.Width, 30)
        labelJudulUtama.TextAlign = ContentAlignment.MiddleCenter
        labelJudulUtama.Location = New Point(0, 60)
        Me.Controls.Add(labelJudulUtama)

        'label hasil pemenang
        labelRekomendasi = New Label()
        labelRekomendasi.Text = "..."
        labelRekomendasi.Font = New Font("Segoe UI", 20, FontStyle.Bold)
        labelRekomendasi.ForeColor = Color.DodgerBlue
        labelRekomendasi.AutoSize = True
        labelRekomendasi.MaximumSize = New Size(Me.ClientSize.Width - 40, 0)
        labelRekomendasi.TextAlign = ContentAlignment.MiddleCenter
        labelRekomendasi.Location = New Point(20, 90)
        Me.Controls.Add(labelRekomendasi)

        'panel grafik
        panelGrafik = New Panel()
        panelGrafik.Location = New Point(50, 200)
        panelGrafik.Size = New Size(Me.ClientSize.Width - 100, 200)
        panelGrafik.BackColor = Color.White
        panelGrafik.BorderStyle = BorderStyle.FixedSingle
        AddHandler panelGrafik.Paint, AddressOf panelGrafik_Paint
        AddHandler panelGrafik.Resize, Sub() panelGrafik.Invalidate()
        Me.Controls.Add(panelGrafik)

        'label detail skor
        gridSkor = New DataGridView()
        gridSkor.Location = New Point(50, 250)
        gridSkor.Size = New Size(Me.ClientSize.Width - 100, 300)
        gridSkor.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gridSkor.BackgroundColor = Color.White
        gridSkor.ReadOnly = True
        gridSkor.AllowUserToAddRows = False
        gridSkor.RowHeadersVisible = False
        gridSkor.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gridSkor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        gridSkor.ScrollBars = ScrollBars.Vertical

        'tambah kolom ke tabel
        gridSkor.Columns.Add("colTopik", "Bidang Minat")
        gridSkor.Columns.Add("colSkor", "Total Skor")
        Me.Controls.Add(gridSkor)

        ' Tombol Print (BARU)
        buttonPrint = New Button()
        buttonPrint.Text = "Cetak / Print"
        buttonPrint.Size = New Size(150, 45)
        buttonPrint.BackColor = Color.DodgerBlue
        buttonPrint.ForeColor = Color.White
        buttonPrint.FlatStyle = FlatStyle.Flat
        buttonPrint.FlatAppearance.BorderSize = 0
        buttonPrint.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        ' Posisi di kiri bawah
        buttonPrint.Location = New Point(50, 0)
        AddHandler buttonPrint.Click, AddressOf ButtonPrint_Click
        Me.Controls.Add(buttonPrint)

        'tombol tutup
        buttonTutup = New Button()
        buttonTutup.Text = "Tutup/Selesai"
        buttonTutup.Size = New Size(200, 40)
        buttonTutup.Location = New Point((Me.ClientSize.Width - 200) / 2, 520)
        buttonTutup.BackColor = Color.IndianRed
        buttonTutup.ForeColor = Color.White
        buttonTutup.FlatStyle = FlatStyle.Flat
        buttonTutup.FlatAppearance.BorderSize = 0

        AddHandler buttonTutup.Click, AddressOf TombolTutup_Click
        Me.Controls.Add(buttonTutup)
    End Sub

    Sub tampilkanData()
        'tampilkan NIM dari session
        labelHeaderNIM.Text = "Hasil Analisis untuk NIM: " & ModuleDB.NIMSekarang

        'validasi data
        If dataSkor Is Nothing OrElse dataSkor.Count = 0 Then
            labelRekomendasi.Text = "Data tidak tersedia"
            Return
        End If

        'cari max skor untuk grafik
        maxSkorGrafik = dataSkor.Values.Max()
        If maxSkorGrafik = 0 Then maxSkorGrafik = 100.0

        'urutkan skor
        Dim skorUrut = From entry In dataSkor Order By entry.Value Descending Select entry

        'ambil skor tertinggi
        Dim maxScore As Double = skorUrut.First().Value

        'tampilkan pemenang
        If maxScore = 0 Then
            labelRekomendasi.Text = "Sepertinya minat anda belum ada."
            labelRekomendasi.ForeColor = Color.Gray
        Else
            'cari topik apa saja yang memiliki skor = maxScore
            Dim daftarPemenang As New List(Of String)

            For Each item In skorUrut
                If item.Value = maxScore Then
                    daftarPemenang.Add("- " & item.Key.ToUpper())
                End If
            Next

            'gabungkan nama-nama pemenang
            labelRekomendasi.Text = String.Join(vbCrLf, daftarPemenang)

            'kecilkan font jika pemenangnya banyak
            If daftarPemenang.Count > 1 Then
                labelRekomendasi.Font = New Font("Segoe UI", 16, FontStyle.Bold)
            Else
                labelRekomendasi.Font = New Font("Segoe UI", 20, FontStyle.Bold)
            End If

            labelRekomendasi.ForeColor = Color.DodgerBlue
        End If

        'masukkan ke tabel
        gridSkor.Rows.Clear()
        For Each item In skorUrut
            gridSkor.Rows.Add(item.Key, item.Value.ToString("F1") & " Poin")
        Next

        'highlight baris pertama (juara)
        If gridSkor.Rows.Count > 0 AndAlso maxScore > 0 Then
            For Each row As DataGridViewRow In gridSkor.Rows
                Dim teksSkor As String = row.Cells(1).Value.ToString().Split(" "c)(0)
                Dim nilaiSkor As Integer = CInt(teksSkor)

                If nilaiSkor = maxScore Then
                    row.DefaultCellStyle.BackColor = Color.LightCyan
                    row.DefaultCellStyle.Font = New Font(gridSkor.Font, FontStyle.Bold)
                End If
            Next
        End If

        'gambar ulang grafik
        panelGrafik.Invalidate()
        aturPosisi()
    End Sub

    Sub aturPosisi()
        If labelRekomendasi Is Nothing Then Exit Sub

        ' 1. Posisi Elemen Atas
        labelRekomendasi.Left = (Me.ClientSize.Width - labelRekomendasi.Width) / 2
        panelGrafik.Location = New Point(50, labelRekomendasi.Bottom + 20)
        gridSkor.Location = New Point(50, panelGrafik.Bottom + 20)

        ' 2. Atur Posisi Tombol Berdampingan
        Dim jarakTombol As Integer = 20
        Dim yTombol As Integer = gridSkor.Bottom + 30

        ' Hitung total lebar kedua tombol + jarak
        Dim totalLebar As Integer = buttonPrint.Width + jarakTombol + buttonTutup.Width

        ' Cari titik mulai (X) agar grup tombol ini ada di tengah form
        Dim startX As Integer = (Me.ClientSize.Width - totalLebar) / 2

        ' Set posisi
        buttonPrint.Location = New Point(startX, yTombol)
        buttonTutup.Location = New Point(startX + buttonPrint.Width + jarakTombol, yTombol)

        ' 3. Auto Resize Tinggi Form jika kurang muat
        If buttonTutup.Bottom + 50 > Me.ClientSize.Height Then
            Me.ClientSize = New Size(Me.ClientSize.Width, buttonTutup.Bottom + 50)
        End If
    End Sub

    Private Sub panelGrafik_Paint(sender As Object, e As PaintEventArgs)
        If dataSkor Is Nothing OrElse dataSkor.Count = 0 Then Exit Sub

        Dim grafik As Graphics = e.Graphics
        grafik.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        'konfigurasi grafik
        Dim marginBawah As Integer = 70
        Dim marginAtas As Integer = 30
        Dim marginKiri As Integer = 10
        Dim marginKanan As Integer = 10

        Dim tinggiGrafik As Integer = panelGrafik.Height - marginBawah - marginAtas
        Dim lebarGrafik As Integer = panelGrafik.Width - marginKiri - marginKanan

        'lebar batang
        Dim lebarBatang As Integer = CInt(lebarGrafik / dataSkor.Count) - 20
        If lebarBatang < 10 Then lebarBatang = 10

        Dim xPos As Integer = marginKiri + 10
        Dim index As Integer = 0

        'loop gambar batang
        For Each keyValue In dataSkor
            Dim skor As Double = keyValue.Value
            'hitung tinggi batang berdasarkan persentase terhadap skor tertinggi
            Dim faktorSkala As Double = maxSkorGrafik
            If faktorSkala = 0 Then faktorSkala = 100

            Dim tinggiBatang As Integer = CInt((skor / faktorSkala) * tinggiGrafik)

            Dim rectX As Integer = xPos
            Dim rectY As Integer = (panelGrafik.Height - marginBawah) - tinggiBatang

            'warna batang (biru untuk skor tinggi, abu untuk 0)
            Dim brushBatang As Brush = Brushes.CornflowerBlue
            If skor = maxSkorGrafik AndAlso skor > 0 Then brushBatang = Brushes.DodgerBlue
            If skor = 0 Then
                tinggiBatang = 2
                rectY = (panelGrafik.Height - marginBawah) - tinggiBatang
                brushBatang = Brushes.LightGray
            End If

            'gambar batang
            grafik.FillRectangle(brushBatang, rectX, rectY, lebarBatang, tinggiBatang)
            grafik.DrawRectangle(Pens.DimGray, rectX, rectY, lebarBatang, tinggiBatang)


            'gambar skor di atas batang
            Dim textSkor As String = skor.ToString("F1")
            Dim fontSkor As New Font("Segoe UI", 9, FontStyle.Bold)
            Dim sizeSkor As SizeF = grafik.MeasureString(textSkor, fontSkor)
            grafik.DrawString(textSkor, fontSkor, Brushes.Black, rectX + (lebarBatang - sizeSkor.Width) / 2, rectY - 20)

            Dim namaTopik As String = keyValue.Key

            Dim fontLabel As New Font("Segoe UI", 7)
            Dim formatLabel As New StringFormat()
            formatLabel.Alignment = StringAlignment.Center
            formatLabel.LineAlignment = StringAlignment.Near

            Dim rectLabel As New RectangleF(rectX - 5, panelGrafik.Height - marginBawah + 5, lebarBatang + 10, marginBawah - 5)
            grafik.DrawString(namaTopik, fontLabel, Brushes.Black, rectLabel, formatLabel)

            ' geser posisi X untuk batang berikutnya
            xPos += lebarBatang + 20
            index += 1
        Next
    End Sub

    Private Sub TombolTutup_Click(sender As Object, e As EventArgs)
        Application.Exit() ' Keluar dari aplikasi
    End Sub

    Private Sub ButtonPrint_Click(sender As Object, e As EventArgs)
        printDoc.DocumentName = "Laporan_Sistem_Pakar_" & ModuleDB.NIMSekarang
        printPreview.Document = printDoc

        ' Maksimalkan jendela preview agar jelas
        CType(printPreview, Form).WindowState = FormWindowState.Maximized
        printPreview.ShowDialog()
    End Sub

    Private Sub printDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        Dim grafik As Graphics = e.Graphics
        Dim fontHeader As New Font("Segoe UI", 16, FontStyle.Bold)
        Dim fontSubHeader As New Font("Segoe UI", 12, FontStyle.Regular)
        Dim fontBold As New Font("Segoe UI", 11, FontStyle.Bold)
        Dim fontBody As New Font("Segoe UI", 10, FontStyle.Regular)

        Dim marginKiri As Integer = 50
        Dim yPos As Integer = 50
        Dim lebarKonten As Integer = e.PageBounds.Width - 100

        'header laporan
        grafik.DrawString("LAPORAN HASIL ANALISIS MINAT SKRIPSI", fontHeader, Brushes.DarkBlue, marginKiri, yPos)
        yPos += 40
        grafik.DrawString("Tanggal Cetak: " & DateTime.Now.ToString("dd MMMM yyyy"), fontBody, Brushes.Black, marginKiri, yPos)
        yPos += 20
        grafik.DrawString("NIM Mahasiswa: " & ModuleDB.NIMSekarang, fontSubHeader, Brushes.Black, marginKiri, yPos)
        yPos += 40
        grafik.DrawLine(Pens.Black, marginKiri, yPos, e.PageBounds.Width - 50, yPos)
        yPos += 30

        'hasil rekomendasi
        grafik.DrawString("Rekomendasi Utama:", fontSubHeader, Brushes.DarkBlue, marginKiri, yPos)
        yPos += 30

        'gambar teks rekomendasi
        Dim kotakRekomendasi As New RectangleF(marginKiri, yPos, lebarKonten, 150)
        grafik.DrawString(labelRekomendasi.Text, fontHeader, Brushes.DodgerBlue, kotakRekomendasi)
        Dim ukuranTeks As SizeF = grafik.MeasureString(labelRekomendasi.Text, fontHeader, lebarKonten)
        yPos += CInt(ukuranTeks.Height) + 40

        'gambar grafik
        grafik.DrawString("Grafik Perbandingan Skor:", fontSubHeader, Brushes.DarkBlue, marginKiri, yPos)
        yPos += 30

        'buat bitmap dari panelGrafik
        Dim bitmapGrafik As New Bitmap(panelGrafik.Width, panelGrafik.Height)
        panelGrafik.DrawToBitmap(bitmapGrafik, New Rectangle(0, 0, panelGrafik.Width, panelGrafik.Height))

        'gambar bitmap ke kertas
        Dim xGrafik As Integer = marginKiri + (lebarKonten - panelGrafik.Width) / 2
        grafik.DrawImage(bitmapGrafik, xGrafik, yPos)
        yPos += panelGrafik.Height + 40

        'tabel detail skor
        grafik.DrawString("Rincian Skor: ", fontSubHeader, Brushes.DarkBlue, marginKiri, yPos)
        yPos += 30

        'header tabel
        grafik.FillRectangle(Brushes.LightGray, marginKiri, yPos, lebarKonten, 30)
        grafik.DrawRectangle(Pens.Black, marginKiri, yPos, lebarKonten, 30)
        grafik.DrawString("Bidang Minat", fontBold, Brushes.Black, marginKiri + 10, yPos + 5)
        grafik.DrawString("Skor", fontBold, Brushes.Black, marginKiri + 400, yPos + 5)
        yPos += 30

        ' isi tabel
        For Each row As DataGridViewRow In gridSkor.Rows
            'garis kotak
            grafik.DrawRectangle(Pens.Black, marginKiri, yPos, lebarKonten, 30)

            'teks
            Dim topik As String = row.Cells(0).Value.ToString()
            Dim skor As String = row.Cells(1).Value.ToString()

            grafik.DrawString(topik, fontBody, Brushes.Black, marginKiri + 10, yPos + 5)
            grafik.DrawString(skor, fontBody, Brushes.Black, marginKiri + 400, yPos + 5)

            yPos += 30
        Next
    End Sub

    Private Sub FormHasil_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
End Class