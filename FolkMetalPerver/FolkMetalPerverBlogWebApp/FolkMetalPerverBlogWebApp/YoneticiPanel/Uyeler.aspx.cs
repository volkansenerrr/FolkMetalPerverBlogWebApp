using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.YoneticiPanel
{
    public partial class Uyeler : System.Web.UI.Page
    {
        VeriModel vm = new VeriModel(); // VeriModel sınıfı doğru şekilde tanımlandığından emin ol

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Sayfa ilk kez yükleniyorsa
            {
                try
                {
                    var uyeler = vm.UyeListele(); // Üye listesini çek
                    if (uyeler != null && uyeler.Count > 0)
                    {
                        lv_Uyeler.DataSource = uyeler; // DataSource'a ata
                        lv_Uyeler.DataBind(); // DataBind yap
                    }
                    else
                    {
                        lbl_Hata.Text = "Üye bulunamadı."; // Üye yoksa mesaj yaz
                        lbl_Hata.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lbl_Hata.Text = $"Hata: {ex.Message}"; // Hata mesajını göster
                    lbl_Hata.Visible = true;
                }
            }
        }

        protected void lv_Uyeler_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument); // Komut argümanını integer'a çevir
            if (e.CommandName == "sil")
            {
                try
                {
                    // Soft delete işlemi
                    vm.UyeSil(id); // UyeSil metodu kullanılmalı
                    lbl_Hata.Text = "Üye başarıyla silindi."; // Başarı mesajı
                    lbl_Hata.Visible = true;
                }
                catch (Exception ex)
                {
                    // Hata mesajını göster
                    lbl_Hata.Text = $"Hata: {ex.Message}"; // Kullanıcıya hata mesajı göster
                    lbl_Hata.Visible = true;
                }
            }
            else if (e.CommandName == "hardDelete")
            {
                try
                {
                    // Hard delete işlemi
                    vm.UyeSilHardDelete(id); // UyeSilHardDelete metodu kullanılmalı
                    lbl_Hata.Text = "Üye başarıyla kalıcı olarak silindi."; // Başarı mesajı
                    lbl_Hata.Visible = true;
                }
                catch (Exception ex)
                {
                    // Hata mesajını göster
                    lbl_Hata.Text = $"Hata: {ex.Message}"; // Kullanıcıya hata mesajı göster
                    lbl_Hata.Visible = true;
                }
            }

            // Listeyi güncelle
            lv_Uyeler.DataSource = vm.UyeListele(); // Listeyi güncelle
            lv_Uyeler.DataBind();
        }
    }
}
