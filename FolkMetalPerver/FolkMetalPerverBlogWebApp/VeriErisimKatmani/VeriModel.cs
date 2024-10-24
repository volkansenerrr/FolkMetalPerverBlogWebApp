using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriErisimKatmani
{
    public class VeriModel
    {
        SqlConnection baglanti; SqlCommand komut;

        public VeriModel()
        {
            baglanti = new SqlConnection(BaglantiYollari.baglantiYolu);
            komut = baglanti.CreateCommand();
        }

        #region Yönetici Metotları

        public Yonetici YoneticiGiris(string mail, string sifre)
        {
            SqlCommand komut = baglanti.CreateCommand();
            try
            {
                komut.CommandText = "SELECT COUNT(*) FROM Yoneticiler WHERE Mail = @mail AND Sifre = @sifre";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@mail", mail);
                komut.Parameters.AddWithValue("@sifre", sifre);
                baglanti.Open();

                int sayi = Convert.ToInt32(komut.ExecuteScalar());
                if (sayi == 1)
                {
                    komut.CommandText = "SELECT Y.ID, Y.YoneticiTurID, YT.Isim, Y.Isim, Y.Soyisim, Y.KullaniciAdi, Y.Mail, Y.Sifre, Y.Durum, Y.Silinmis FROM Yoneticiler AS Y JOIN YoneticiTurleri AS YT ON Y.YoneticiTurID = YT.ID WHERE Y.Mail = @mail AND Y.Sifre = @sifre";
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@mail", mail);
                    komut.Parameters.AddWithValue("@sifre", sifre);
                    SqlDataReader okuyucu = komut.ExecuteReader();
                    Yonetici y = new Yonetici();
                    while (okuyucu.Read())
                    {
                        y.ID = okuyucu.GetInt32(0);
                        y.YoneticiTurID = okuyucu.GetInt32(1);
                        y.YoneticiTur = okuyucu.GetString(2);
                        y.Isim = okuyucu.GetString(3);
                        y.Soyisim = okuyucu.GetString(4);
                        y.KullaniciAdi = okuyucu.GetString(5);
                        y.Mail = okuyucu.GetString(6);
                        y.Sifre = okuyucu.GetString(7);
                        y.Durum = okuyucu.GetBoolean(8);
                        y.Silinmis = okuyucu.GetBoolean(9);
                    }
                    return y;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Yonetici YoneticiEmailveSifreGetir(string email, string sifre)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM Yoneticiler WHERE Mail = @mail AND Sifre = @sifre", baglanti);
            komut.Parameters.AddWithValue("@mail", email);
            komut.Parameters.AddWithValue("@sifre", sifre);

            try
            {
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();

                if (okuyucu.Read())
                {
                    return new Yonetici
                    {
                        ID = (int)okuyucu["ID"],
                        Isim = (string)okuyucu["Isim"],
                        Soyisim = (string)okuyucu["Soyisim"],
                        KullaniciAdi = (string)okuyucu["KullaniciAdi"],
                        Mail = (string)okuyucu["Mail"],
                        Sifre = (string)okuyucu["Sifre"],
                        Durum = (bool)okuyucu["Durum"],
                        Silinmis = (bool)okuyucu["Silinmis"]
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
            return null; // Kullanıcı bulunamazsa null döndür
        }

        public bool YoneticiSifreGuncelle(string email, string yeniSifre)
        {
            SqlCommand komut = new SqlCommand("UPDATE Yoneticiler SET Sifre = @yeniSifre WHERE Mail = @mail", baglanti);
            komut.Parameters.AddWithValue("@mail", email);
            komut.Parameters.AddWithValue("@yeniSifre", yeniSifre);

            try
            {
                baglanti.Open();
                int sonuc = komut.ExecuteNonQuery();
                return sonuc > 0; // Güncelleme başarılıysa true döndür
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        #endregion

        #region Kategori Metotları

        public bool KategoriEkle(Kategori kat)
        {
            try
            {
                komut.CommandText = "INSERT INTO Kategoriler(Isim,Aciklama,Durum,Silinmis) VALUES(@isim,@aciklama,@durum,@silinmis)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", kat.Isim);
                komut.Parameters.AddWithValue("@aciklama", kat.Aciklama);
                komut.Parameters.AddWithValue("@durum", kat.Durum);
                komut.Parameters.AddWithValue("@silinmis", kat.Silinmis);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public List<Kategori> KategoriListele()
        {
            List<Kategori> kategoriler = new List<Kategori>();

            try
            {
                komut.CommandText = "SELECT ID, Isim, Aciklama, Durum, Silinmis FROM Kategoriler ";
                komut.Parameters.Clear();
                baglanti.Open();
                SqlDataReader reader = komut.ExecuteReader();
                while (reader.Read())
                {
                    Kategori kat = new Kategori();
                    kat.ID = reader.GetInt32(0);
                    kat.Isim = reader.GetString(1);
                    kat.Aciklama = reader.GetString(2);
                    kat.Durum = reader.GetBoolean(3);
                    kat.Silinmis = reader.GetBoolean(4);
                    kategoriler.Add(kat);
                }
                return kategoriler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public List<Kategori> KategoriListele(bool silinmis)
        {
            List<Kategori> kategoriler = new List<Kategori>();

            try
            {
                komut.CommandText = "SELECT ID, Isim, Aciklama, Durum, Silinmis FROM Kategoriler WHERE Silinmis=@silinmis";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@silinmis", silinmis);
                baglanti.Open();
                SqlDataReader reader = komut.ExecuteReader();
                while (reader.Read())
                {
                    Kategori kat = new Kategori();
                    kat.ID = reader.GetInt32(0);
                    kat.Isim = reader.GetString(1);
                    kat.Aciklama = reader.GetString(2);
                    kat.Durum = reader.GetBoolean(3);
                    kat.Silinmis = reader.GetBoolean(4);
                    kategoriler.Add(kat);
                }
                return kategoriler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }
        public List<Kategori> KategoriListele(bool silinmis, bool durum)
        {
            List<Kategori> kategoriler = new List<Kategori>();

            try
            {
                komut.CommandText = "SELECT ID, Isim, Aciklama, Durum, Silinmis FROM Kategoriler WHERE Silinmis=@silinmis AND Durum =@durum";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@silinmis", silinmis);
                komut.Parameters.AddWithValue("@durum", durum);
                baglanti.Open();
                SqlDataReader reader = komut.ExecuteReader();
                while (reader.Read())
                {
                    Kategori kat = new Kategori();
                    kat.ID = reader.GetInt32(0);
                    kat.Isim = reader.GetString(1);
                    kat.Aciklama = reader.GetString(2);
                    kat.Durum = reader.GetBoolean(3);
                    kat.Silinmis = reader.GetBoolean(4);
                    kategoriler.Add(kat);
                }
                return kategoriler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void KategoriSilHardDelete(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Kategoriler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void KategoriSil(int id)
        {
            try
            {
                komut.CommandText = "UPDATE Kategoriler SET Silinmis = 1 WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void KategoriDurumDegistir(int id)
        {
            try
            {
                komut.CommandText = "SELECT Durum FROM Kategoriler WHERE ID = @id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                bool durum = Convert.ToBoolean(komut.ExecuteScalar());
                komut.CommandText = "UPDATE Kategoriler SET Durum=@durum WHERE ID = @id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@durum", !durum);
                komut.Parameters.AddWithValue("@id", id);
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Kategori KategoriGetir(int id)
        {
            try
            {
                komut.CommandText = "SELECT ID, Isim, Aciklama, Durum, Silinmis FROM Kategoriler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                Kategori kat = new Kategori();

                while (okuyucu.Read())
                {
                    kat.ID = okuyucu.GetInt32(0);
                    kat.Isim = okuyucu.GetString(1);
                    kat.Aciklama = okuyucu.GetString(2);
                    kat.Durum = okuyucu.GetBoolean(3);
                    kat.Silinmis = okuyucu.GetBoolean(4);
                }
                return kat;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool KategoriGuncelle(Kategori k)
        {
            try
            {
                komut.CommandText = "UPDATE Kategoriler SET Isim=@isim, Aciklama=@aciklama, Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", k.ID);
                komut.Parameters.AddWithValue("@isim", k.Isim);
                komut.Parameters.AddWithValue("@aciklama", k.Aciklama);
                komut.Parameters.AddWithValue("@durum", k.Durum);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        #endregion

        #region Makale Metotları

        public bool MakaleEkle(Makale mak)
        {
            try
            {
                Debug.WriteLine("Makale ekleme işlemi başlatıldı."); // İlk mesaj
                komut.CommandText = "INSERT INTO Makaleler(KategoriID, YazarID, Baslik, Ozet, Icerik, EklemeTarihi, GoruntulemeSayisi, KapakResim, Durum) VALUES(@kategoriID, @yazarID, @baslik, @ozet, @icerik, @eklemeTarihi, @goruntulemeSayisi, @kapakResim, @durum)";

                // Parametreleri ekleme
                Debug.WriteLine("Parametreler ekleniyor...");

                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@kategoriID", mak.KategoriID);
                Debug.WriteLine("Kategori ID: " + mak.KategoriID);
                komut.Parameters.AddWithValue("@yazarID", mak.YazarID);
                Debug.WriteLine("Yazar ID: " + mak.YazarID);
                komut.Parameters.AddWithValue("@baslik", mak.Baslik);
                Debug.WriteLine("Başlık: " + mak.Baslik);
                komut.Parameters.AddWithValue("@ozet", mak.Ozet);
                komut.Parameters.AddWithValue("@icerik", mak.Icerik);
                komut.Parameters.AddWithValue("@eklemeTarihi", mak.EklemeTarihi);
                komut.Parameters.AddWithValue("@goruntulemeSayisi", mak.GoruntulemeSayisi);
                komut.Parameters.AddWithValue("@kapakResim", mak.KapakResim);
                komut.Parameters.AddWithValue("@durum", mak.Durum);

                baglanti.Open();
                Debug.WriteLine("Veritabanına bağlandı.");

                int result = komut.ExecuteNonQuery();
                if (result > 0)
                {
                    Debug.WriteLine("Makale başarıyla eklendi.");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Makale eklenemedi.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hata: {ex.Message}"); // Hata mesajını debug konsoluna yazdır
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}"); // Hatanın yığıt izini (stack trace) yazdır
                return false;
            }
            finally
            {
                baglanti.Close();
                Debug.WriteLine("Veritabanı bağlantısı kapatıldı.");
            }
        }

        public List<Makale> MakaleListele()
        {
            List<Makale> makaleler = new List<Makale>();
            try
            {
                komut.CommandText = "SELECT M.ID, M.KategoriID, K.Isim, M.YazarID, Y.KullaniciAdi, M.Ozet, M.Icerik, M.Baslik, M.EklemeTarihi, M.GoruntulemeSayisi, M.KapakResim, M.Durum FROM Makaleler AS M JOIN Kategoriler AS K ON M.KategoriID = K.ID JOIN Yoneticiler AS Y ON M.YazarID = Y.ID";
                komut.Parameters.Clear();

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();

                while (okuyucu.Read())
                {
                    Makale mak = new Makale();
                    mak.ID = okuyucu.GetInt32(0);
                    mak.KategoriID = okuyucu.GetInt32(1);
                    mak.Kategori = okuyucu.GetString(2);
                    mak.YazarID = okuyucu.GetInt32(3);
                    mak.Yazar = okuyucu.GetString(4);
                    mak.Ozet = okuyucu.GetString(5);
                    mak.Icerik = okuyucu.GetString(6);
                    mak.Baslik = okuyucu.GetString(7);
                    mak.EklemeTarihi = okuyucu.GetDateTime(8);
                    mak.GoruntulemeSayisi = okuyucu.GetInt64(9);
                    mak.KapakResim = okuyucu.GetString(10);
                    mak.Durum = okuyucu.GetBoolean(11);
                    makaleler.Add(mak);
                }
            }
            catch (Exception ex)
            {
                // Hata günlüğü veya loglama yap
                // Örneğin: LogHata(ex);
                throw new Exception("Veritabanından makaleleri yüklerken bir hata oluştu.", ex);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }

            return makaleler;
        }

        public List<Makale> MakaleListele(int kid)
        {
            List<Makale> makaleler = new List<Makale>();
            try
            {
                komut.CommandText = "SELECT M.ID, M.KategoriID, K.Isim, M.YazarID, Y.KullaniciAdi, M.Ozet, M.Icerik, M.Baslik, M.EklemeTarihi, M.GoruntulemeSayisi, M.KapakResim, M.Durum FROM Makaleler AS M JOIN Kategoriler AS K ON M.KategoriID = K.ID JOIN Yoneticiler AS Y ON M.YazarID = Y.ID WHERE M.KategoriID = @kid";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@kid", kid);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();
                while (okuyucu.Read())
                {
                    Makale mak = new Makale();
                    mak.ID = okuyucu.GetInt32(0);
                    mak.KategoriID = okuyucu.GetInt32(1);
                    mak.Kategori = okuyucu.GetString(2);
                    mak.YazarID = okuyucu.GetInt32(3);
                    mak.Yazar = okuyucu.GetString(4);
                    mak.Ozet = okuyucu.GetString(5);
                    mak.Icerik = okuyucu.GetString(6);
                    mak.Baslik = okuyucu.GetString(7);
                    mak.EklemeTarihi = okuyucu.GetDateTime(8);
                    mak.GoruntulemeSayisi = okuyucu.GetInt64(9);
                    mak.KapakResim = okuyucu.GetString(10);
                    mak.Durum = okuyucu.GetBoolean(11);
                    makaleler.Add(mak);
                }
                return makaleler;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Makale MakaleGetir(int id)
        {
            try
            {
                komut.CommandText = "SELECT M.ID, M.KategoriID, K.Isim, M.YazarID, Y.KullaniciAdi, M.Ozet, M.Icerik, M.Baslik, M.EklemeTarihi, M.GoruntulemeSayisi, M.KapakResim, M.Durum FROM Makaleler AS M JOIN Kategoriler AS K ON M.KategoriID = K.ID JOIN Yoneticiler AS Y ON M.YazarID = Y.ID WHERE M.ID = @id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();

                SqlDataReader okuyucu = komut.ExecuteReader();
                Makale mak = new Makale();
                while (okuyucu.Read())
                {
                    mak.ID = okuyucu.GetInt32(0);
                    mak.KategoriID = okuyucu.GetInt32(1);
                    mak.Kategori = okuyucu.GetString(2);
                    mak.YazarID = okuyucu.GetInt32(3);
                    mak.Yazar = okuyucu.GetString(4);
                    mak.Ozet = okuyucu.GetString(5);
                    mak.Icerik = okuyucu.GetString(6);
                    mak.Baslik = okuyucu.GetString(7);
                    mak.EklemeTarihi = okuyucu.GetDateTime(8);
                    mak.GoruntulemeSayisi = okuyucu.GetInt64(9);
                    mak.KapakResim = okuyucu.GetString(10);
                    mak.Durum = okuyucu.GetBoolean(11);
                }
                return mak;
            }
            catch
            {
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public bool MakaleDuzenle(Makale mak)
        {
            try
            {
                komut.CommandText = "UPDATE Makaleler SET KategoriID=@kategoriId, Baslik=@baslik, Icerik=@icerik, Ozet=@ozet, KapakResim=@kapakresim, Durum=@durum WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@kategoriId", mak.KategoriID);
                komut.Parameters.AddWithValue("@baslik", mak.Baslik);
                komut.Parameters.AddWithValue("@icerik", mak.Icerik);
                komut.Parameters.AddWithValue("@ozet", mak.Ozet);
                komut.Parameters.AddWithValue("@kapakresim", mak.KapakResim);
                komut.Parameters.AddWithValue("@durum", mak.Durum);
                komut.Parameters.AddWithValue("@id", mak.ID);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void MakaleSil(int id)
        {
            try
            {
                komut.CommandText = "DELETE FROM Yorumlar WHERE MakaleID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
                komut.CommandText = "DELETE FROM Makaleler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }

        public void MakaleGoruntulemeArttir(int id)
        {
            try
            {
                komut.CommandText = "SELECT GoruntulemeSayisi FROM Makaleler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                int sayi = Convert.ToInt32(komut.ExecuteScalar());
                komut.CommandText = "UPDATE Makaleler SET GoruntulemeSayisi=@gs WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                komut.Parameters.AddWithValue("@gs", sayi + 1);
                komut.ExecuteNonQuery();
            }
            finally
            {
                baglanti.Close();
            }
        }
        #endregion

        #region Üye Metotları

        public bool UyeEkle(Uye uye)
        {
            try
            {
                komut.CommandText = "INSERT INTO Uyeler(Isim,Soyisim,KullaniciAdi,Mail,Sifre,Durum,Silinmis) VALUES(@isim,@soyisim,@kullaniciadi,@mail,@sifre,@durum,@silinmis)";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@isim", uye.Isim);
                komut.Parameters.AddWithValue("@soyisim", uye.Soyisim);
                komut.Parameters.AddWithValue("@kullaniciadi", uye.KullaniciAdi);
                komut.Parameters.AddWithValue("@mail", uye.Mail);
                komut.Parameters.AddWithValue("@sifre", uye.Sifre);
                komut.Parameters.AddWithValue("@durum", uye.Durum);
                komut.Parameters.AddWithValue("@silinmis", uye.Silinmis);
                baglanti.Open();
                komut.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public Uye UyeGiris(string mail, string sifre)
        {
            SqlCommand komut = baglanti.CreateCommand();
            try
            {
                // Kullanıcıyı sorgularken parametreleri yeniden ekleyelim
                komut.CommandText = "SELECT ID, Isim, Soyisim, KullaniciAdi, Mail, Sifre, Durum, Silinmis FROM Uyeler WHERE Mail = @mail AND Sifre = @sifre";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@mail", mail.ToLower()); // E-posta küçük harfe çevrildi
                komut.Parameters.AddWithValue("@sifre", sifre); // Şifreyi hashlemiyorsan bu düz şekilde tutulacak

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();

                if (okuyucu.Read()) // Eğer kullanıcı bulunduysa
                {
                    Uye u = new Uye
                    {
                        ID = okuyucu.GetInt32(0),
                        Isim = okuyucu.GetString(1),
                        Soyisim = okuyucu.GetString(2),
                        KullaniciAdi = okuyucu.GetString(3),
                        Mail = okuyucu.GetString(4),
                        Sifre = okuyucu.GetString(5),
                        Durum = okuyucu.GetBoolean(6),
                        Silinmis = okuyucu.GetBoolean(7)
                    };

                    return u; // Kullanıcı bulundu, geri dön
                }
                else
                {
                    return null; // Kullanıcı bulunamadı
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return null;
            }
            finally
            {
                baglanti.Close();
            }
        }


        public Uye UyeEmailveSifreGetir(string email, string sifre)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM Uyeler WHERE Mail = @mail AND Sifre = @sifre", baglanti);
            komut.Parameters.AddWithValue("@mail", email);
            komut.Parameters.AddWithValue("@sifre", sifre);

            try
            {
                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();

                if (okuyucu.Read())
                {
                    return new Uye
                    {
                        ID = (int)okuyucu["ID"],
                        Isim = (string)okuyucu["Isim"],
                        Soyisim = (string)okuyucu["Soyisim"],
                        KullaniciAdi = (string)okuyucu["KullaniciAdi"],
                        Mail = (string)okuyucu["Mail"],
                        Sifre = (string)okuyucu["Sifre"],
                        Durum = (bool)okuyucu["Durum"],
                        Silinmis = (bool)okuyucu["Silinmis"]
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
            return null; // Kullanıcı bulunamazsa null döndür
        }

        public bool UyeSifreGuncelle(string email, string yeniSifre)
        {
            SqlCommand komut = new SqlCommand("UPDATE Uyeler SET Sifre = @yeniSifre WHERE Mail = @mail", baglanti);

            // E-posta adresini küçük harfe çevir
            komut.Parameters.AddWithValue("@mail", email.ToLower());
            komut.Parameters.AddWithValue("@yeniSifre", yeniSifre);

            try
            {
                baglanti.Open();
                int sonuc = komut.ExecuteNonQuery();
                return sonuc > 0; // Güncelleme başarılıysa true döndür
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                baglanti.Close();
            }
        }

        public List<Uye> UyeListele()
        {
            List<Uye> uyeler = new List<Uye>();
            try
            {
                komut.CommandText = "SELECT ID, Isim, Soyisim, KullaniciAdi, Mail, Sifre, Durum, Silinmis FROM Uyeler";
                komut.Parameters.Clear();

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();

                while (okuyucu.Read())
                {
                    Uye u = new Uye
                    {
                        ID = okuyucu.GetInt32(0),
                        Isim = okuyucu.GetString(1),
                        Soyisim = okuyucu.GetString(2),
                        KullaniciAdi = okuyucu.GetString(3),
                        Mail = okuyucu.GetString(4),
                        Sifre = okuyucu.GetString(5),
                        Durum = okuyucu.GetBoolean(6),
                        Silinmis = okuyucu.GetBoolean(7)
                    };
                    uyeler.Add(u);
                }
            }
            catch (Exception ex)
            {
                // Hata günlüğü veya loglama yap
                throw new Exception("Veritabanından üyeleri yüklerken bir hata oluştu.", ex);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }

            return uyeler;
        }

        public List<Uye> UyeListele(int uyeid)
        {
            List<Uye> uyeler = new List<Uye>();
            try
            {
                // Burada doğru sorgu kullanılmış.
                komut.CommandText = "SELECT ID, Isim, Soyisim, KullaniciAdi, Mail, Sifre, Durum, Silinmis FROM Uyeler WHERE ID = @uyeid";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@uyeid", uyeid);

                baglanti.Open();
                SqlDataReader okuyucu = komut.ExecuteReader();

                while (okuyucu.Read())
                {
                    Uye u = new Uye
                    {
                        ID = okuyucu.GetInt32(0),
                        Isim = okuyucu.GetString(1),
                        Soyisim = okuyucu.GetString(2),
                        KullaniciAdi = okuyucu.GetString(3),
                        Mail = okuyucu.GetString(4),
                        Sifre = okuyucu.GetString(5),
                        Durum = okuyucu.GetBoolean(6),
                        Silinmis = okuyucu.GetBoolean(7)
                    };
                    uyeler.Add(u);
                }

                return uyeler;
            }
            catch (Exception ex) // Hata durumunda hata mesajını göster
            {
                // Hata mesajını güncelledim
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                return null;
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        public void UyeSil(int id)
        {
            try
            {
                komut.CommandText = "UPDATE Uyeler SET Silinmis = 1 WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                int rowsAffected = komut.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("Silme işlemi sırasında bir sorun oluştu, ID bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Silme işlemi başarısız: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }


        // Eğer hard delete istiyorsan
        public void UyeSilHardDelete(int id)
        {
            try
            {
                // Hard delete: Üyeyi veritabanından tamamen sil
                komut.CommandText = "DELETE FROM Uyeler WHERE ID=@id";
                komut.Parameters.Clear();
                komut.Parameters.AddWithValue("@id", id);
                baglanti.Open();
                komut.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Hata durumunda, burada uygun bir hata mesajı dönebilirsiniz
                throw new Exception("Silme işlemi başarısız: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        #endregion

    }
}


