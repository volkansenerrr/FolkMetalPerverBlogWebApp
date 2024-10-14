using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.YoneticiPanel
{
    public partial class MakaleDuzenle : System.Web.UI.Page
    {
        VeriModel vm = new VeriModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0)
            {
                if (!IsPostBack)
                {
                    ddl_kategoriler.DataSource = vm.KategoriListele(false, true);
                    ddl_kategoriler.DataBind();

                    int id;
                    if (int.TryParse(Request.QueryString["makaleId"], out id)) // ID kontrolü
                    {
                        Makale mak = vm.MakaleGetir(id);
                        if (mak != null) // Makale kontrolü
                        {
                            tb_baslik.Text = mak.Baslik;
                            ddl_kategoriler.SelectedValue = Convert.ToString(mak.KategoriID);
                            tb_icerik.Text = mak.Icerik;
                            tb_ozet.Text = mak.Ozet;
                            cb_yayinla.Checked = mak.Durum;
                            img_resim.ImageUrl = "../Resimler/MakaleResimleri/" + mak.KapakResim;
                        }
                        else
                        {
                            lbl_mesaj.Text = "Geçersiz makale ID.";
                            pnl_basarisiz.Visible = true;
                        }
                    }
                    else
                    {
                        lbl_mesaj.Text = "Makale ID'si geçersiz.";
                        pnl_basarisiz.Visible = true;
                    }
                }
            }
            else
            {
                Response.Redirect("MakaleListele.aspx");
            }
        }

        protected void lbtn_makaleduzenle_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(Request.QueryString["makaleId"], out id))
            {
                lbl_mesaj.Text = "Geçersiz makale ID.";
                pnl_basarisiz.Visible = true;
                return;
            }

            Makale mak = vm.MakaleGetir(id);
            if (mak == null)
            {
                lbl_mesaj.Text = "Makale bulunamadı.";
                pnl_basarisiz.Visible = true;
                return;
            }

            mak.Baslik = tb_baslik.Text;
            mak.Icerik = tb_icerik.Text;
            mak.Ozet = tb_ozet.Text;
            mak.Durum = cb_yayinla.Checked;
            mak.KategoriID = Convert.ToInt32(ddl_kategoriler.SelectedItem.Value);

            // Dosya uzantısı kontrolü
            if (fu_resim.HasFile)
            {
                string uzanti = Path.GetExtension(fu_resim.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(uzanti))
                {
                    lbl_mesaj.Text = "Yalnızca JPG ve PNG dosyaları yüklenebilir.";
                    pnl_basarisiz.Visible = true;
                    return;
                }

                string dosyaisim = Convert.ToString(Guid.NewGuid());
                fu_resim.SaveAs(Server.MapPath("../Resimler/MakaleResimleri/" + dosyaisim + uzanti));
                mak.KapakResim = dosyaisim + uzanti;
            }

            // Makale güncelleme
            if (vm.MakaleDuzenle(mak))
            {
                pnl_basarili.Visible = true;
                pnl_basarisiz.Visible = false;
            }
            else
            {
                pnl_basarili.Visible = false;
                pnl_basarisiz.Visible = true;
                lbl_mesaj.Text = "Makale düzenleme başarısız";
            }
        }
    }
}
