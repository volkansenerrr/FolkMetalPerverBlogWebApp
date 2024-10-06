<%@ Page Title="" Language="C#" MasterPageFile="~/YoneticiPanel/YoneticiMaster.Master" AutoEventWireup="true" CodeBehind="MakaleEkle.aspx.cs" Inherits="FolkMetalPerverBlogWebApp.YoneticiPanel.MakaleEkle" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/MakaleFormSayfasi.css" rel="stylesheet" />
    <script src="ckeditor/ckeditor.js"></script>
    <!-- CKEditor Scripti Eklendi -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="formTasiyici">
        <div class="formBaslik">
            <h4 style="text-align: center;">Makale Ekle</h4>
        </div>
        <div class="formIcerik">
            <!-- Başarılı ve Başarısız durumlar için mesaj panelleri -->
            <asp:Panel ID="pnl_basarili" runat="server" CssClass="basariliPanel" Visible="false">
                <strong>Başarılı!</strong> Makale Başarıyla Eklenmiştir.
            </asp:Panel>
            <asp:Panel ID="pnl_basarisiz" runat="server" CssClass="basarisizPanel" Visible="false">
                <strong>Başarısız!</strong>
                <asp:Label ID="lbl_mesaj" runat="server"></asp:Label>
            </asp:Panel>

            <div class="satir">
                <label for="tb_baslik">Makale Başlığı</label><br />
                <asp:TextBox ID="tb_baslik" runat="server" CssClass="metinKutu"></asp:TextBox>
            </div>
            <div class="satir">
                <label for="ddl_kategoriler">Makale Kategorisi Seç</label><br />
                <asp:DropDownList ID="ddl_kategoriler" runat="server" CssClass="metinKutu"></asp:DropDownList>
            </div>
            <div class="satir">
                <label for="tb_ozet">Makale Özet</label><br />
                <asp:TextBox ID="tb_ozet" runat="server" CssClass="metinKutu"></asp:TextBox>
            </div>
            <div class="satir">
                <label for="tb_icerik">Makale İçeriği</label><br />
                <asp:TextBox ID="tb_icerik" runat="server" CssClass="metinKutu" TextMode="MultiLine" Rows="10"></asp:TextBox>
            </div>
            <div class="satir">
                <label for="fu_resim">Kapak Resmi</label><br />
                <asp:FileUpload ID="fu_resim" runat="server" />
            </div>
            <div class="satir">
                <asp:CheckBox ID="cb_yayinla" runat="server" Text="" />
                <label for="cb_yayinla">Makale Yayınla</label>
            </div>
            <div class="satir">
                <asp:LinkButton ID="lbtn_ekle" runat="server" CssClass="islemButton" OnClick="lbtn_ekle_Click">Makale Ekle</asp:LinkButton>
            </div>
        </div>
    </div>

    <script>
        CKEDITOR.replace('<%= tb_icerik.ClientID %>');
    </script>
</asp:Content>
