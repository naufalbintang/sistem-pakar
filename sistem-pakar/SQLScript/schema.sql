-- ========================================================
-- 1. DROP TABLES (Menghapus tabel jika sudah ada)
--    Dilakukan untuk memastikan script berjalan mulus jika dijalankan berulang.
-- ========================================================

-- Urutan penghapusan harus memperhatikan Foreign Key
IF OBJECT_ID('[dbo].[Jawaban_Mhs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Jawaban_Mhs];

IF OBJECT_ID('[dbo].[Pertanyaan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pertanyaan];

IF OBJECT_ID('[dbo].[Konsultasi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Konsultasi];

IF OBJECT_ID('[dbo].[Akun]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Akun];

IF OBJECT_ID('[dbo].[Topik]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Topik];

IF OBJECT_ID('[dbo].[Mahasiswa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mahasiswa];


-----------------------------------------------------------

-- ========================================================
-- 2. CREATE TABLES (Membuat ulang semua tabel)
-- ========================================================

-- =======================
-- 2.1 TABLE: Topik
-- =======================
CREATE TABLE [dbo].[Topik] (
    [Id_topik]   CHAR (3)     NOT NULL, -- Contoh: T01
    [nama_topik] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_topik] ASC)
);


-- =======================
-- 2.2 TABLE: Akun
-- =======================
CREATE TABLE [dbo].[Akun] (
    [Id_user]  VARCHAR (18)  NOT NULL, -- Panjang 18 (e.g., NIM)
    [nama]     VARCHAR (100) NOT NULL,
    [email]    VARCHAR (100) NOT NULL,
    [password] VARCHAR (255) NOT NULL,
    [role]     VARCHAR (10)  NOT NULL DEFAULT 'mahasiswa',
    PRIMARY KEY CLUSTERED ([Id_user] ASC)
);


-- =======================
-- 2.3 TABLE: Konsultasi
-- =======================
CREATE TABLE [dbo].[Konsultasi] (
    [Id_konsultasi] INT IDENTITY (1, 1) NOT NULL,
    [Id_topik]      CHAR (3)            NULL,       -- Topik rekomendasi (bisa NULL di awal)
    [Id_user]       VARCHAR (18)        NOT NULL,   -- DIPERBAIKI: Harus 18, sama dengan tabel Akun
    PRIMARY KEY CLUSTERED ([Id_konsultasi] ASC),

    FOREIGN KEY ([Id_topik]) REFERENCES [dbo].[Topik] ([Id_topik]),
    FOREIGN KEY ([Id_user])  REFERENCES [dbo].[Akun] ([Id_user])
);


-- =======================
-- 2.4 TABLE: Pertanyaan
-- =======================
CREATE TABLE [dbo].[Pertanyaan] (
    [Id_pertanyaan]    CHAR(4)      NOT NULL, -- Contoh: P001
    [teks_pertanyaan]  TEXT         NOT NULL,
    [bobot_pertanyaan] FLOAT (53)   NOT NULL,
    [Id_topik]         CHAR (3)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_pertanyaan] ASC),

    FOREIGN KEY ([Id_topik]) REFERENCES [dbo].[Topik] ([Id_topik])
);


-- =======================
-- 2.5 TABLE: Jawaban_Mhs
-- =======================
CREATE TABLE [dbo].[Jawaban_Mhs] (
    [Id_konsultasi] INT          NOT NULL,
    [Id_pertanyaan] CHAR (4)     NOT NULL,
    [jawaban]       VARCHAR(5)   NOT NULL, -- Jawaban: 'Ya' atau 'Tidak'

    CONSTRAINT [PK_SesiJawaban]
        PRIMARY KEY CLUSTERED ([Id_pertanyaan] ASC, [Id_konsultasi] ASC),

    FOREIGN KEY ([Id_pertanyaan]) REFERENCES [dbo].[Pertanyaan] ([Id_pertanyaan]),
    FOREIGN KEY ([Id_konsultasi]) REFERENCES [dbo].[Konsultasi] ([Id_konsultasi])
);


-----------------------------------------------------------

-- ========================================================
-- 3. INSERT DATA (Mengisi data Topik dan Pertanyaan)
-- ========================================================

-- Hapus data lama agar tidak terjadi duplikasi saat insert ulang
DELETE FROM [dbo].[Jawaban_Mhs];
DELETE FROM [dbo].[Konsultasi];
DELETE FROM [dbo].[Pertanyaan];
DELETE FROM [dbo].[Topik];
-- Data Akun biasanya tidak dihapus pada skrip setup awal, tapi jika perlu, tambahkan: DELETE FROM [dbo].[Akun];


-- =======================
-- 3.1 Insert Tabel Topik
-- =======================
INSERT INTO Topik(Id_topik, nama_topik) VALUES ('T01', 'Rekayasa Perangkat Lunak');
INSERT INTO Topik(Id_topik, nama_topik) VALUES ('T02', 'AI dan Data Science');
INSERT INTO Topik(Id_topik, nama_topik) VALUES ('T03', 'Network dan Security');
INSERT INTO Topik(Id_topik, nama_topik) VALUES ('T04', 'Internet Of Things dan Robotika');
INSERT INTO Topik(Id_topik, nama_topik) VALUES ('T05', 'Multimedia dan Game Development');


-- ==========================
-- 3.2 Insert Tabel Pertanyaan
-- ==========================
-- === 1. TOPIK: REKAYASA PERANGKAT LUNAK (T01) ===
INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P001', 'Apakah Anda menikmati proses merancang alur sistem (flowchart/UML) dan desain database (ERD) sebelum memulai coding?', 20, 'T01');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P002', 'Apakah Anda tertarik membangun aplikasi yang memiliki user interface visual (Web/Mobile)?', 10, 'T01');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P003', 'Apakah Anda menguasai konsep CRUD (Create, Read, Update, Delete) dalam coding?', 60, 'T01');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P004', 'Apakah Anda lebih suka melihat hasil coding yang langsung terlihat bentuk fisiknya (tombol/form)?', 10, 'T01');


-- === 2. TOPIK: AI & DATA SCIENCE (T02) ===
INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P005', 'Apakah Anda menyukai mata kuliah yang melibatkan banyak logika matematika dan statistika?', 20, 'T02');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P006', 'Apakah Anda tertarik membuat sistem yang bisa belajar sendiri (Machine Learning) dari data masa lalu?', 10, 'T02');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P007', 'Apakah Anda lebih menikmati mengolah data mentah mencari pola daripada mendesain tampilan?', 10, 'T02');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P008', 'Apakah Anda familiar dengan library pengolahan data seperti Pandas, Scikit-Learn, atau TensorFlow?', 60, 'T02');


-- === 3. TOPIK: NETWORK & SECURITY (T03) ===
INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P009', 'Apakah Anda lebih suka mengkonfigurasi server, router, dan IP address daripada coding aplikasi?', 60, 'T03');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P010', 'Apakah Anda tertarik dengan isu keamanan siber, hacking, dan cara memproteksi sistem?', 10, 'T03');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P011', 'Apakah Anda nyaman bekerja menggunakan layar hitam (Terminal/CLI) tanpa tampilan grafis?', 20, 'T03');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P012', 'Apakah Anda penasaran bagaimana protokol data dikirimkan melalui jaringan internet?', 10, 'T03');


-- === 4. TOPIK: IOT & ROBOTIKA (T04) ===
INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P013', 'Apakah Anda tertarik menggabungkan kode program dengan perangkat keras fisik (sensor/lampu)?', 10, 'T04');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P014', 'Apakah Anda tidak keberatan berurusan dengan kabel, komponen elektronik, atau menyolder?', 20, 'T04');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P015', 'Apakah Anda ingin membuat sistem kendali peralatan rumah tangga jarak jauh (Smart Home)?', 10, 'T04');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P016', 'Apakah Anda pernah/mau belajar memprogram mikrokontroler (Arduino, ESP32, Raspberry Pi)?', 60, 'T04');


-- === 5. TOPIK: MULTIMEDIA & GAME DEV (T05) ===
INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P017', 'Apakah Anda memiliki ketertarikan tinggi pada desain grafis, animasi, atau aset visual 3D?', 10, 'T05');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P018', 'Apakah Anda tertarik logika fisika komputer (gravitasi/tumbukan) untuk simulasi game?', 60, 'T05');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P019', 'Apakah Anda ingin membuat aplikasi Augmented Reality (AR) atau Virtual Reality (VR)?', 10, 'T05');

INSERT INTO Pertanyaan (Id_pertanyaan, teks_pertanyaan, bobot_pertanyaan, Id_topik) 
VALUES ('P020', 'Apakah Anda tertarik mengutak-atik Game Engine (Unity/Unreal) dan membuat script gameplay?', 20, 'T05');