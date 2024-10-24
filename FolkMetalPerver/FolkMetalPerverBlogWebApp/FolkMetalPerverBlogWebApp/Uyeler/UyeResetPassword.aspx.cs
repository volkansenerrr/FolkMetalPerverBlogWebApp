using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.Uyeler
{
    public partial class UyeResetPassword : System.Web.UI.Page
    {
        VeriModel vm = new VeriModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Sayfa yüklendiğinde mesaj panelini kontrol et
            if (Session["Message"] != null)
            {
                pnl_mesaj.Visible = true; // Mesaj panelini görünür yap
                lbl_mesaj.Text = Session["Message"].ToString(); // Mesajı ata
                lbl_mesaj.CssClass = Session["CssClass"].ToString(); // CSS sınıfını ata
                Session.Remove("Message"); // Mesajı temizle
                Session.Remove("CssClass"); // CSS sınıfını temizle
            }
            else
            {
                pnl_mesaj.Visible = false; // Mesaj yoksa gizle
            }
        }

        protected void btn_UyeSifreSifirla_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan verileri değişkenlere ata
            string email = tb_mail.Text.Trim();
            string eskiSifre = tb_oldPassword.Text.Trim();
            string yeniSifre = tb_newPassword.Text.Trim();

            // Kontrol et, eğer yeni şifre ve eski şifre alanları boşsa uyarı göster
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(eskiSifre) || string.IsNullOrEmpty(yeniSifre))
            {
                SetMessage("Mail, eski şifre ve yeni şifre alanlarını doldurmalısınız!", "error-message");
                return;
            }

            // Kullanıcı bilgilerini kontrol et
            var kullanici = vm.UyeEmailveSifreGetir(email, eskiSifre);

            if (kullanici == null)
            {
                SetMessage("Girdiğiniz eski şifre yanlış veya e-posta adresi mevcut değil!", "error-message");
                return;
            }

            // Eski şifre doğruysa, yeni şifreyi güncelle
            bool guncellendiMi = vm.UyeSifreGuncelle(email, yeniSifre);

            if (guncellendiMi)
            {
                SetMessage("Şifre sıfırlama işlemi başarılı!", "success-message");
            }
            else
            {
                SetMessage("Şifre sıfırlama işlemi sırasında bir hata oluştu.", "error-message");
            }
        }

        private void SetMessage(string message, string cssClass)
        {
            Session["Message"] = message; // Mesajı session'a ata
            Session["CssClass"] = cssClass; // CSS sınıfını session'a ata
            Response.Redirect(Request.RawUrl); // Sayfayı yenile
        }
    }
}