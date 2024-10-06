using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.YoneticiPanel
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["GirisYapanYonetici"] != null)
            {
                Yonetici y = (Yonetici)Session["GirisYapanYonetici"];
                lbl_kullanici.Text = y.KullaniciAdi + " (" + y.YoneticiTur + ")";
            }
            else
            {
                Response.Redirect("Giris.aspx");
            }
        }

        protected void lbtn_cikis_Click(object sender, EventArgs e)
        {
            Session["GirisYapanYonetici"] = null;
            Response.Redirect("Giris.aspx");
        }
    }
}