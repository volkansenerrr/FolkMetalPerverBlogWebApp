using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.Uyeler
{
    public partial class UyeOl : System.Web.UI.Page
    {
        VeriModel vm = new VeriModel();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_UyeOlButonu_Click(object sender, EventArgs e)
        {
            // Kullanıcının girdiği verileri al
            string isim = tb_isim.Text.Trim();
            string soyisim = tb_soyad.Text.Trim();
            string kullaniciAdi = tb_kullaniciAdi.Text.Trim();
            string mail = tb_mail.Text.Trim();
            string sifre = tb_sifre.Text.Trim();

            // Boş alan kontrolü
            if (string.IsNullOrEmpty(isim) || string.IsNullOrEmpty(soyisim) ||
                string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(mail) ||
                string.IsNullOrEmpty(sifre))
            {
                lbl_mesaj.Text = "Tüm alanlar doldurulmalıdır!";
                lbl_mesaj.CssClass = "error-message";
                pnl_mesaj.Visible = true; // Mesaj panelini göster
                return; // Fonksiyondan çık
            }

            // E-posta adresi kontrolü
            if (!IsValidEmail(mail))
            {
                lbl_mesaj.Text = "Geçersiz e-posta adresi! Lütfen doğru bir format girin.";
                lbl_mesaj.CssClass = "error-message";
                pnl_mesaj.Visible = true; // Mesaj panelini göster
                return; // Fonksiyondan çık
            }

            // Üyeyi veritabanına ekle
            VeriErisimKatmani.Uye yeniUye = new VeriErisimKatmani.Uye
            {
                Isim = isim,
                Soyisim = soyisim,
                KullaniciAdi = kullaniciAdi,
                Mail = mail,
                Sifre = sifre,
                Durum = true, // Varsayılan durum
                Silinmis = false // Varsayılan silinmiş durumu
            };

            if (vm.UyeEkle(yeniUye))
            {
                // Başarı mesajı
                lbl_mesaj.Text = "Üyelik işlemi başarılı!";
                lbl_mesaj.CssClass = "success-message";
                pnl_mesaj.Visible = true; // Mesaj panelini göster
            }
            else
            {
                // Hata mesajı
                lbl_mesaj.Text = "Üyelik işlemi başarısız! Lütfen tekrar deneyin.";
                lbl_mesaj.CssClass = "error-message";
                pnl_mesaj.Visible = true; // Mesaj panelini göster
            }
        }

        private bool IsValidEmail(string email)
        {
            // E-posta adresi için regex deseni
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
