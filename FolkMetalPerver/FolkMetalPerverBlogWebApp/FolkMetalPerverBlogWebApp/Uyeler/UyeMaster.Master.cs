using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.Uyeler
{
    public partial class UyeMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kullanıcının oturum açıp açmadığını kontrol et
            if (Session["GirisYapanUye"] != null)
            {
                // Session'dan Kullanıcı bilgilerini al
                Uye u = (Uye)Session["GirisYapanUye"]; // Uye sınıfını burada kullanın
                lbl_uye.Text = u.KullaniciAdi + " (" + u.Durum + ")"; // Kullanıcı adı ve durumu göster
            }
            else
            {
                // Oturum açmamışsa giriş sayfasına yönlendir
                Response.Redirect("UyeGiris.aspx");
            }
        }

        protected void lbtn_uyecikis_Click(object sender, EventArgs e)
        {
            // Oturumu kapatma işlemini burada gerçekleştirin
            Session["GirisYapanUye"] = null; // Oturumu sıfırla
            Response.Redirect("UyeGiris.aspx"); // Giriş sayfasına yönlendir
        }
    }
}
