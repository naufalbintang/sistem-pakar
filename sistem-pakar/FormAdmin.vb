Imports System.Reflection.Metadata
Imports Microsoft.Data.SqlClient
Imports Microsoft.Extensions.Logging
Imports Microsoft.VisualBasic.ApplicationServices

Public Class FormAdmin

    'variabel global
    Private panelSideBar As Panel
    Private panelHeader As Panel
    Private panelKonten As Panel
    Private panelFormInput As Panel

    'tombol menu
    Private WithEvents buttonMenuUser As Button
    Private WithEvents buttonMenuPertanyaan As Button
    Private WithEvents buttonMenuHasil As Button
    Private WithEvents buttonLogout As Button

    'grid
    Private WithEvents dgv As DataGridView

    'input field
    Private textId As TextBox
    Private textNama As TextBox
    Private textEmail As TextBox
    Private textPassword As TextBox
    Private comboRole As ComboBox

    'tombol CRUD
    Private WithEvents buttonAdd As Button
    Private WithEvents buttonUpdate As Button
    Private WithEvents buttonDelete As Button
    Private WithEvents buttonClear As Button

    'state
    Private menuAktif As String = "USER"

    Private Sub FormAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'setup form dasar
        Me.Text = "Dashboard Admin - Sistem Pakar Menentukan Topik Skripsi"
        Me.Size = New Size(1000, 650)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.WhiteSmoke

        'render ui
        buatTampilan()

        'load data awal
        loadDataUser()
    End Sub

    Private Sub buatTampilan()
        'header
        panelHeader = New Panel()
        panelHeader.Dock = DockStyle.Top
        panelHeader.Height = 50
        panelHeader.BackColor = Color.MediumVioletRed

        Dim titleApp As New Label()
        titleApp.Text = "FormAdmin"
        titleApp.ForeColor = Color.White
        titleApp.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        titleApp.AutoSize = True
        titleApp.Location = New Point(15, 13)
        panelHeader.Controls.Add(titleApp)
        Me.Controls.Add(panelHeader)

        'sidebar
        panelSideBar = New Panel()
        panelSideBar.Dock = DockStyle.Left
        panelSideBar.Width = 200
        panelSideBar.BackColor = Color.WhiteSmoke

        Dim labelAdmin As New Label()
        labelAdmin.Text = "ADMIN"
        labelAdmin.Font = New Font("Segoe UI", 16, FontStyle.Bold)
        labelAdmin.TextAlign = ContentAlignment.MiddleCenter
        labelAdmin.AutoSize = False
        labelAdmin.Size = New Size(200, 50)
        labelAdmin.Location = New Point(0, 20)
        panelSideBar.Controls.Add(labelAdmin)

        'tombol menu
        buttonMenuUser = buatTombolMenu("Akun User", 100)
        buttonMenuPertanyaan = buatTombolMenu("Pertanyaan", 160)
        buttonMenuHasil = buatTombolMenu("Hasil", 220)

        buttonLogout = New Button()
        buttonLogout.Text = "Logout"
        buttonLogout.Size = New Size(150, 40)
        buttonLogout.Location = New Point(25, 550)
        buttonLogout.BackColor = Color.White
        buttonLogout.FlatStyle = FlatStyle.Flat
        panelSideBar.Controls.Add(buttonLogout)

        panelSideBar.Controls.Add(buttonMenuUser)
        panelSideBar.Controls.Add(buttonMenuPertanyaan)
        panelSideBar.Controls.Add(buttonMenuHasil)
        Me.Controls.Add(panelSideBar)

        'panel konten
        panelKonten = New Panel()
        panelKonten.Dock = DockStyle.Fill
        panelKonten.BackColor = Color.White
        Me.Controls.Add(panelKonten)
        panelKonten.BringToFront()

        'datagridview
        dgv = New DataGridView()
        dgv.Location = New Point(20, 20)
        dgv.Size = New Size(740, 250)
        dgv.BackgroundColor = Color.White
        dgv.AllowUserToAddRows = False
        dgv.ReadOnly = True
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        panelKonten.Controls.Add(dgv)

        'form input
        panelFormInput = New Panel()
        panelFormInput.Location = New Point(20, 290)
        panelFormInput.Size = New Size(740, 250)

        buatInputField("ID / NIM", textId, 0)
        buatInputField("Nama", textNama, 50)
        buatInputField("Email", textEmail, 100)

        Dim labelRole As New Label()
        labelRole.Text = "Role"
        labelRole.Location = New Point(0, 150)
        comboRole = New ComboBox()
        comboRole.Items.AddRange({"mahasiswa", "admin"})
        comboRole.Location = New Point(0, 175)
        comboRole.Size = New Size(250, 30)
        comboRole.DropDownStyle = ComboBoxStyle.DropDownList
        panelFormInput.Controls.Add(labelRole)
        panelFormInput.Controls.Add(comboRole)

        buatInputField("Password", textPassword, 200)
        textPassword.UseSystemPasswordChar = True
        textPassword.PlaceholderText = "(kosongkan jika tidak ingin diubah)"

        buttonAdd = buatTombolCRUD("ADD NEW", New Point(350, 150))
        buttonUpdate = buatTombolCRUD("UPDATE", New Point(480, 150))
        buttonDelete = buatTombolCRUD("DELETE", New Point(350, 200))
        buttonClear = buatTombolCRUD("CLEAR", New Point(480, 200))

        panelKonten.Controls.Add(panelFormInput)
    End Sub

    Private Function buatTombolMenu(teks As String, yPos As Integer) As Button
        Dim button As New Button()
        button.Text = teks
        button.Size = New Size(160, 40)
        button.Location = New Point(20, yPos)
        button.FlatStyle = FlatStyle.Flat
        button.BackColor = Color.WhiteSmoke
        button.TextAlign = ContentAlignment.MiddleLeft
        Return button
    End Function

    Private Sub buatInputField(labelTeks As String, ByRef textBox As TextBox, yPos As Integer)
        Dim label As New Label()
        label.Text = labelTeks
        label.Location = New Point(0, yPos)
        label.AutoSize = True

        textBox = New TextBox
        textBox.Location = New Point(0, yPos + 25)
        textBox.Size = New Size(300, 30)

        panelFormInput.Controls.Add(label)
        panelFormInput.Controls.Add(textBox)
    End Sub

    Private Function buatTombolCRUD(teks As String, lokasi As Point) As Button
        Dim button As New Button()
        button.Text = teks
        button.Location = lokasi
        button.Size = New Size(100, 35)
        button.BackColor = Color.White
        button.FlatStyle = FlatStyle.Standard
        panelFormInput.Controls.Add(button)

        Return button
    End Function

    Private Sub loadDataUser()
        menuAktif = "USER"
        resetTombolMenu(buttonMenuUser)
        panelFormInput.Visible = True

        Try
            Dim dt As New DataTable()
            Using conn = ModuleDB.getConnection()
                conn.Open()
                'tampilkan semua kolom akun
                Dim cmd As New SqlCommand("SELECT * FROM Akun", conn)
                Dim adapter As New SqlDataAdapter(cmd)
                adapter.Fill(dt)
            End Using
            dgv.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub loadDataPertanyaan()
        menuAktif = "TANYA"
        resetTombolMenu(buttonMenuPertanyaan)
        panelFormInput.Visible = False 'menyembunyikan inputan (readonly)
        dgv.DataSource = ModuleDB.AmbilSemuaPertanyaan()
    End Sub

    Private Sub loadDataHasil()
        menuAktif = "HASIL"
        resetTombolMenu(buttonMenuHasil)
        panelFormInput.Visible = False 'menyembunyikan inputan (readonly)

        Try
            Dim dt As New DataTable()
            Using conn = ModuleDB.getConnection()
                conn.Open()
                Dim query As String = "SELECT K.Id_konsultasi, A.nama AS Mahasiswa, T.nama_topik Hasil_Rekomendasi FROM Konsultasi K Join Akun A ON K.Id_user = A.Id_user LEFT JOIN Topik T ON K.Id_topik = T.Id_topik"
                Dim adapter As New SqlDataAdapter(New SqlCommand(query, conn))
                adapter.Fill(dt)
            End Using
            dgv.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub resetTombolMenu(buttonAktif As Button)
        buttonMenuUser.BackColor = Color.WhiteSmoke
        buttonMenuPertanyaan.BackColor = Color.WhiteSmoke
        buttonMenuHasil.BackColor = Color.WhiteSmoke
        buttonAktif.BackColor = Color.LightBlue
    End Sub

    'logika CRUD
    Private Sub buttonAdd_Click(sender As Object, e As EventArgs) Handles buttonAdd.Click
        If menuAktif <> "USER" Then Return
        If textId.Text = "" Or textNama.Text = "" Or textEmail.Text = "" Or textPassword.Text = "" Or comboRole.Text = "" Then
            MsgBox("Mohon lengkapi semua data!", MsgBoxStyle.Exclamation)
            Return
        End If

        Try
            Using conn = ModuleDB.getConnection()
                conn.Open()

                'cek apakah id sudah terpakai
                Dim queryCek As String = "SELECT COUNT(*) FROM Akun WHERE Id_user = @id"
                Using cmdCek As New SqlCommand(queryCek, conn)
                    cmdCek.Parameters.AddWithValue("@id", textId.Text)
                    Dim jumlah As Integer = Convert.ToInt32(cmdCek.ExecuteScalar())

                    If jumlah > 0 Then
                        MsgBox("NIM / ID User ini sudah terdaftar! Silakan gunakan ID lain.", MsgBoxStyle.Critical, "Gagal Daftar")
                        Return
                    End If
                End Using

                Dim query As String = "INSERT INTO Akun (Id_user, nama, email, password, role) VALUES (@idUser, @nama, @email, @password, @role)"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@idUser", textId.Text)
                    cmd.Parameters.AddWithValue("@nama", textNama.Text)
                    cmd.Parameters.AddWithValue("@email", textEmail.Text)
                    If textPassword.Text = "" Then
                        MsgBox("Password harus diisi untuk user baru!", MsgBoxStyle.Exclamation)
                        Return
                    End If
                    cmd.Parameters.AddWithValue("@password", HashPassword.HashPassword(textPassword.Text))
                    cmd.Parameters.AddWithValue("@role", comboRole.Text)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MsgBox("Data user berhasil ditambahkan!", MsgBoxStyle.Information)
            loadDataUser()
            clearInput()
        Catch ex As Exception
            MsgBox("Gagal menambahkan data: " & ex.Message)
        End Try
    End Sub

    Private Sub buttonnUpdate_Click(sender As Object, e As EventArgs) Handles buttonUpdate.Click
        If menuAktif <> "USER" Or textId.Text = "" Then Return

        Try
            Using conn = ModuleDB.getConnection()
                conn.Open()
                Dim query As String = ""
                If textPassword.Text = "" Then
                    query = "UPDATE Akun SET nama=@nama, email=@email, role=@role WHERE Id_user=@idUser"
                Else
                    query = "UPDATE Akun SET nama=@nama, email=@email, role=@role, password=@password WHERE Id_user=@idUser"
                End If

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@idUser", textId.Text)
                    cmd.Parameters.AddWithValue("@nama", textNama.Text)
                    cmd.Parameters.AddWithValue("@email", textEmail.Text)
                    cmd.Parameters.AddWithValue("@role", comboRole.Text)
                    If textPassword.Text <> "" Then
                        cmd.Parameters.AddWithValue("@password", HashPassword.HashPassword(textPassword.Text))
                    End If
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MsgBox("Data user berhasil diupdate", MsgBoxStyle.Information)
            loadDataUser()
            clearInput()
        Catch ex As Exception
            MsgBox("Gagal update: " & ex.Message)
        End Try
    End Sub

    Private Sub buttonDelete_Click(sender As Object, e As EventArgs) Handles buttonDelete.Click
        If menuAktif <> "USER" Or textId.Text = "" Then Return
        If MsgBox("Yakin ingin menghapus user " & textNama.Text & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical) = MsgBoxResult.Yes Then
            Try
                Using conn = ModuleDB.getConnection()
                    conn.Open()
                    'hapus data di tabel Jawaban_Mhs
                    Dim queryJawaban As String = "DELETE FROM Jawaban_Mhs WHERE Id_konsultasi IN (SELECT Id_konsultasi FROM Konsultasi WHERE Id_user=@idUser)"
                    Using cmd As New SqlCommand(queryJawaban, conn)
                        cmd.Parameters.AddWithValue("@idUser", textId.Text)
                        cmd.ExecuteNonQuery()
                    End Using

                    'hapus data terkait konsultasi
                    Dim queryKonsultasi As String = "DELETE FROM Konsultasi WHERE Id_user=@idUser"
                    Using cmd As New SqlCommand(queryKonsultasi, conn)
                        cmd.Parameters.AddWithValue("@idUser", textId.Text)
                        cmd.ExecuteNonQuery()
                    End Using

                    'hapus akun
                    Dim queryAkun As String = "DELETE FROM akun WHERE Id_user=@idUser"
                    Using cmd As New SqlCommand(queryAkun, conn)
                        cmd.Parameters.AddWithValue("@idUser", textId.Text)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                MsgBox("User berhasil dihapus", MsgBoxStyle.Information)
                loadDataUser()
                clearInput()
            Catch ex As Exception
                MsgBox("Gagal hapus: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub buttonClear_Click(sender As Object, e As EventArgs) Handles buttonClear.Click
        clearInput()
    End Sub

    Private Sub clearInput()
        textId.Clear()
        textNama.Clear()
        textEmail.Clear()
        textPassword.Clear()
        comboRole.SelectedIndex = -1
        textId.Enabled = True
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex >= 0 AndAlso menuAktif = "USER" Then
            Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
            textId.Text = row.Cells("Id_user").Value.ToString()
            textNama.Text = row.Cells("nama").Value.ToString()
            textEmail.Text = row.Cells("email").Value.ToString()
            comboRole.Text = row.Cells("role").Value.ToString()

            textId.Enabled = False
            textPassword.Clear()
        End If
    End Sub

    Private Sub buttonMenuUser_Click(sender As Object, e As EventArgs) Handles buttonMenuUser.Click
        loadDataUser()
    End Sub

    Private Sub buttonMenuPertanyaan_Click(sender As Object, e As EventArgs) Handles buttonMenuPertanyaan.Click
        loadDataPertanyaan()
    End Sub

    Private Sub buttonMenuHasil_Click(sender As Object, e As EventArgs) Handles buttonMenuHasil.Click
        loadDataHasil()
    End Sub

    Private Sub buttonLogout_Click(sender As Object, e As EventArgs) Handles buttonLogout.Click
        Dim login As New FormLogin()
        login.Show()
        Me.Hide()
    End Sub

    Private Sub FormAdmin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
End Class