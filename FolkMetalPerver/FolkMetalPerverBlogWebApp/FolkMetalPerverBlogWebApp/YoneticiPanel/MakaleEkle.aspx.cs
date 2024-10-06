using System;
using System.IO;
using System.Web.UI;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.YoneticiPanel
{
    public partial class MakaleEkle : System.Web.UI.Page
    {
        VeriModel vm = new VeriModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kategorileri listele
                var kategoriler = vm.KategoriListele(false, true);
                ddl_kategoriler.DataSource = kategoriler;
                ddl_kategoriler.DataTextField = "Isim"; // Kategori ismini göster
                ddl_kategoriler.DataValueField = "ID";   // Kategori ID'sini değer olarak kullan
                ddl_kategoriler.DataBind();
            }
        }

        protected void lbtn_ekle_Click(object sender, EventArgs e)
        {
            // Başlık kontrolü
            if (string.IsNullOrEmpty(tb_baslik.Text))
            {
                lbl_mesaj.Text = "Başlıksız makale olmaz.";
                pnl_basarili.Visible = false;
                pnl_basarisiz.Visible = true;
                return;
            }

            // Makale nesnesi oluşturma
            Makale mak = new Makale
            {
                Baslik = tb_baslik.Text,
                KategoriID = Convert.ToInt32(ddl_kategoriler.SelectedItem.Value),
                YazarID = ((Yonetici)Session["GirisYapanYonetici"]).ID,
                Ozet = tb_ozet.Text,
                Icerik = tb_icerik.Text,
                EklemeTarihi = DateTime.Now,
                GoruntulemeSayisi = 0,
                Durum = cb_yayinla.Checked
            };

            // Resim yükleme işlemi
            if (fu_resim.HasFile)
            {
                try
                {
                    string isim = Guid.NewGuid().ToString();
                    string yol = fu_resim.FileName;
                    string uzanti = Path.GetExtension(yol);
                    string dosyatamisim = isim + uzanti;
                    fu_resim.SaveAs(Server.MapPath("../Resimler/MakaleResimleri/" + dosyatamisim));
                    mak.KapakResim = dosyatamisim;
                }
                catch (Exception ex)
                {
                    lbl_mesaj.Text = "Resim yüklenirken hata: " + ex.Message;
                    pnl_basarili.Visible = false;
                    pnl_basarisiz.Visible = true;
                    return;
                }
            }
            else
            {
                mak.KapakResim = "none.png"; // Varsayılan resim
            }

            // Makale ekleme işlemi
            try
            {
                if (vm.MakaleEkle(mak))
                {
                    pnl_basarili.Visible = true; // Başarılı mesajını göster
                    pnl_basarisiz.Visible = false; // Hata panelini gizle
                }
                else
                {
                    lbl_mesaj.Text = "Makale eklenirken bir hata oluştu. Lütfen tekrar deneyin."; // Daha açıklayıcı mesaj
                    pnl_basarili.Visible = false; // Başarı panelini gizle
                    pnl_basarisiz.Visible = true; // Hata panelini göster
                }
            }
            catch (Exception ex)
            {
                lbl_mesaj.Text = "Hata: " + ex.Message; // Hata mesajını göster
                pnl_basarili.Visible = false; // Başarı panelini gizle
                pnl_basarisiz.Visible = true; // Hata panelini göster
            }
        }

        
    }
}
