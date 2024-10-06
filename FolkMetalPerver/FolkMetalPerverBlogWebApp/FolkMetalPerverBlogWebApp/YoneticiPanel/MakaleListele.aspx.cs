using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using VeriErisimKatmani;

namespace FolkMetalPerverBlogWebApp.YoneticiPanel
{
    public partial class MakaleListele : System.Web.UI.Page
    {
        VeriModel vm = new VeriModel();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) // Eğer sayfa ilk kez yükleniyorsa
            {
                lv_Makaleler.DataSource = vm.MakaleListele();
                lv_Makaleler.DataBind();
            }
        }

        
        protected void lv_Makaleler_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int makid = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "sil")
            {
                vm.MakaleSil(makid);
            }
            lv_Makaleler.DataSource = vm.MakaleListele();
            lv_Makaleler.DataBind();
        }
    }
}
