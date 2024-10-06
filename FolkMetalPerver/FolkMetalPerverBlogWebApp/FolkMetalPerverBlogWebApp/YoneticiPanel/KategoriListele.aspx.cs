using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.YoneticiPanel
{
    public partial class KategoriListele : System.Web.UI.Page
    {
        // 'vm' değişkeni burada tanımlanmalı
        VeriModel vm;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 'vm' değişkenini burada initialize ediyoruz
            vm = new VeriModel();

            // Verileri listeleme
            if (!IsPostBack) // Sayfa ilk kez yüklendiğinde verileri yüklemek için
            {
                lv_Kategoriler.DataSource = vm.KategoriListele(false);
                lv_Kategoriler.DataBind();
            }
        }

        protected void lv_Kategoriler_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "sil")
            {
                vm.KategoriSil(id);
            }
            if (e.CommandName == "durum")
            {
                vm.KategoriDurumDegistir(id);
            }

            // Güncel verileri tekrar yükle
            lv_Kategoriler.DataSource = vm.KategoriListele(false);
            lv_Kategoriler.DataBind();
        }
    }
}
