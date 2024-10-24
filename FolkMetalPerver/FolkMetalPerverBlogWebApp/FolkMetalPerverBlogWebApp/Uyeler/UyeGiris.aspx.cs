using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.Uyeler
{
    public partial class UyeGiris : System.Web.UI.Page
    {
        VeriModel vm = new VeriModel();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_UyeGirisiYap_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_mail.Text))
            {
                if (!string.IsNullOrEmpty(tb_sifre.Text))
                {
                    Uye u = vm.UyeGiris(tb_mail.Text, tb_sifre.Text);
                    if (u != null)
                    {
                        Session["GirisYapanUye"] = u;
                        //Giriş Yapan Yöneticinin nesnesi client tarafında tutuldu
                        Response.Redirect("UyeDefault.aspx");
                    }
                    else
                    {
                        pnl_basarisiz.Visible = true;
                        lbl_mesaj.Text = "Kullanıcı Bulunamadı";
                    }
                }
                else
                {
                    pnl_basarisiz.Visible = true;
                    lbl_mesaj.Text = "Şifre adresi boş bırakılamaz";
                }
            }
            else
            {
                pnl_basarisiz.Visible = true;
                lbl_mesaj.Text = "Mail adresi boş bırakılamaz";
            }
        }

        protected void btn_UyeOl_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Uyeler/UyeOl.aspx");
        }
    }
}