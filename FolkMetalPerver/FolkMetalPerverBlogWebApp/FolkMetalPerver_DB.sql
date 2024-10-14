CREATE DATABASE FolkMetalPerver_DB
GO
USE FolkMetalPerver_DB
GO
CREATE TABLE YoneticiTurleri 
(
  ID INT IDENTITY (1,1),
  Isim NVARCHAR (50),
  CONSTRAINT PK_YoneticiTurleri PRIMARY KEY (ID)
)
GO
INSERT INTO YoneticiTurleri(Isim) VALUES ('Admin')
INSERT INTO YoneticiTurleri(Isim) VALUES ('Moderatör')
GO
CREATE TABLE Yoneticiler 
(
  ID INT IDENTITY (1,1),
  YoneticiTurID INT,
  Isim NVARCHAR (50),
  Soyisim NVARCHAR (50),
  KullaniciAdi NVARCHAR (50),
  Mail NVARCHAR (120),
  Sifre NVARCHAR (20),
  Durum BIT,
  Silinmis BIT, 
  CONSTRAINT PK_Yonetici PRIMARY KEY (ID),
  CONSTRAINT FK_Yonetici_YoneticiTur FOREIGN KEY (YoneticiTurID) REFERENCES YoneticiTurleri (ID) 
)
GO
INSERT INTO Yoneticiler(YoneticiTurID, Isim, Soyisim, KullaniciAdi, Mail, Sifre, Durum, Silinmis) VALUES (1, 'Volkan', 'Þener', 'YaþlýGenç', 'volkanvssener@hotmail.com', '1234', 1, 0)
GO
CREATE TABLE Kategoriler 
(
  ID INT IDENTITY (1,1),
  Isim NVARCHAR (50),
  Aciklama NVARCHAR (500),
  Durum BIT,
  Silinmis BIT,
  CONSTRAINT PK_Kategori PRIMARY KEY (ID) 
)
GO
CREATE TABLE Makaleler 
(
  ID INT IDENTITY (1,1),
  KategoriID INT,
  YazarID INT,
  Baslik NVARCHAR (250),
  Ozet NVARCHAR (500),
  Icerik NVARCHAR (MAX),
  EklemeTarihi DATETIME,
  GoruntulemeSayisi BIGINT,
  KapakResim NVARCHAR (255),
  Durum BIT,
  CONSTRAINT PK_Makale PRIMARY KEY (ID),
  CONSTRAINT FK_Makale_Kategori FOREIGN KEY (KategoriID) REFERENCES Kategoriler (ID),
  CONSTRAINT FK_Makale_Yazar FOREIGN KEY (YazarID) REFERENCES Yoneticiler (ID) 
)
GO
CREATE TABLE Uyeler 
(
  ID INT IDENTITY (1,1),
  Isim NVARCHAR (50),
  Soyisim NVARCHAR (50),
  KullaniciAdi NVARCHAR (50),
  Mail NVARCHAR (120),
  Sifre NVARCHAR (20),
  Durum BIT,
  Silinmis BIT,
  CONSTRAINT PK_Uye PRIMARY KEY (ID)
)
GO
CREATE TABLE Yorumlar 
(
  ID INT IDENTITY (1,1),
  MakaleID INT,
  UyeID INT,
  Icerik NVARCHAR (500),
  EklemeTarihi DATETIME,
  Durum BIT,
  CONSTRAINT PK_Yorum PRIMARY KEY (ID),
  CONSTRAINT FK_Yorum_Makale FOREIGN KEY (MakaleID) REFERENCES Makaleler (ID),
  CONSTRAINT FK_Yorum_Uye FOREIGN KEY (UyeID) REFERENCES Uyeler (ID)
)



