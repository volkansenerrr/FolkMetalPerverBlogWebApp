<%@ Page Title="" Language="C#" MasterPageFile="~/YoneticiPanel/YoneticiMaster.Master" AutoEventWireup="true" CodeBehind="KategoriListele.aspx.cs" Inherits="FolkMetalPerverBlogWebApp.YoneticiPanel.KategoriListele" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/ListeSayfasi.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sayfaBaslik">
        <h3>Kategoriler</h3>
    </div>
    <div class="tabloTasiyici">
        <asp:ListView ID="lv_Kategoriler" runat="server" OnItemCommand="lv_Kategoriler_ItemCommand">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" class="tablo">
                    <tr>
                        <th>ID</th>
                        <th>İsim</th>
                        <th>Açıklama</th>
                        <th>Durum</th>
                        <th>Seçenekler</th>
                    </tr>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("ID") %></td>
                    <td><%# Eval("Isim") %></td>
                    <td><%# Eval("Aciklama") %></td>
                    <td><%# Eval("Durum") %></td>
                    <td>
                        <div style="display: flex; justify-content: center; gap: 5px;"> <!-- Boşluk ve hizalama -->
                            <a href='KategoriDuzenle.aspx?kategoriId=<%# Eval("ID") %>' class="tablobutton duzenle" style="display: inline-flex; align-items: center; padding: 10px; line-height: 1.5; width: 100px;">
                                <img src="Resimler/Duzenle_Icon.png" alt="Düzenle" style="margin-right: 5px;" /> Düzenle
                            </a>
                            <asp:LinkButton ID="lbtn_durum" runat="server" class="tablobutton durum" CommandArgument='<%# Eval("ID") %>' CommandName="durum" style="display: inline-flex; align-items: center; padding: 10px; line-height: 1.5; width: 100px;">
                                <img src="Resimler/Durum_Icon.png" alt="Durum" style="margin-right: 5px;" /> Durum
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtn_sil" runat="server" class="tablobutton sil" CommandArgument='<%# Eval("ID") %>' CommandName="sil" style="display: inline-flex; align-items: center; padding: 10px; line-height: 1.5; width: 100px;">
                                <img src="Resimler/Sil_Icon.png" alt="Sil" style="margin-right: 5px;" /> Sil
                            </asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
