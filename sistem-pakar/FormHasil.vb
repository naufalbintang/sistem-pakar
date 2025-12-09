Public Class FormHasil
    'variabel penampung data skor
    Public dataSkor As Dictionary(Of String, Integer)

    'variabel kontrol ui
    Private labelHeaderNIM As Label
    Private labelJudulUtama As Label
    Private labelRekomendasi As Label
    Private gridSkor As DataGridView
    Private buttonTutup As Button

    Private Sub FormHasil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'setup form
        Me.Text = "Hasil Analisis Sistem Pakar"
        Me.Size = New Size(600, 700)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.WhiteSmoke

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
        labelRekomendasi.AutoSize = False
        labelRekomendasi.Size = New Size(Me.ClientSize.Width, 80)
        labelRekomendasi.TextAlign = ContentAlignment.MiddleCenter
        labelRekomendasi.Location = New Point(0, 90)
        Me.Controls.Add(labelRekomendasi)

        'label detail skor
        gridSkor = New DataGridView()
        gridSkor.Location = New Point(50, 190)
        gridSkor.Size = New Size(Me.ClientSize.Width - 100, 300)
        gridSkor.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        gridSkor.BackgroundColor = Color.White
        gridSkor.ReadOnly = True
        gridSkor.AllowUserToAddRows = False
        gridSkor.RowHeadersVisible = False
        gridSkor.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gridSkor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill

        'tambah kolom ke tabel
        gridSkor.Columns.Add("colTopik", "Bidang Minat")
        gridSkor.Columns.Add("colTopik", "Total Skor")

        Me.Controls.Add(gridSkor)

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

        'urutkan skor
        Dim skorUrut = From entry In dataSkor Order By entry.Value Descending Select entry

        'ambil skor tertinggi
        Dim maxScore As Integer = skorUrut.First().Value

        'tampilkan pemenang
        If maxScore = 0 Then
            labelRekomendasi.Text = "Sepertinya minat anda belum ada."
            labelRekomendasi.ForeColor = Color.Gray
        Else
            'cari topik apa saja yang memiliki skor = maxScore
            Dim daftarPemenang As New List(Of String)

            For Each item In skorUrut
                If item.Value = maxScore Then
                    daftarPemenang.Add(item.Key.ToUpper())
                End If
            Next

            'gabungkan nama-nama pemenang
            labelRekomendasi.Text = String.Join(vbCrLf & "&" & vbCrLf, daftarPemenang)

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
            gridSkor.Rows.Add(item.Key, item.Value & " Poin")
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
    End Sub

    'membuat layout responsive saat resize
    Private Sub FormHasil_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If labelJudulUtama IsNot Nothing Then
            labelJudulUtama.Width = Me.ClientSize.Width
            labelRekomendasi.Width = Me.ClientSize.Width
            buttonTutup.Location = New Point((Me.ClientSize.Width - buttonTutup.Width) / 2, Me.ClientSize.Height - 80)
        End If
    End Sub

    Private Sub TombolTutup_Click(sender As Object, e As EventArgs)
        Application.Exit() ' Keluar dari aplikasi
    End Sub
End Class