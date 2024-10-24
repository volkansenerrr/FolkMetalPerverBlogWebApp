<%@ Page Title="" Language="C#" MasterPageFile="~/YoneticiPanel/YoneticiMaster.Master" AutoEventWireup="true" CodeBehind="Uyeler.aspx.cs" Inherits="FolkMetalPerverBlogWebApp.YoneticiPanel.Uyeler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/UyeListeStil.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sayfaBaslik">
        <h3>Üyeler</h3>
    </div>
    <asp:Label ID="lbl_Hata" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    <div class="tabloTasiyici">
        <asp:ListView ID="lv_Uyeler" runat="server" OnItemCommand="lv_Uyeler_ItemCommand">
            <LayoutTemplate>
                <table cellspacing="0" cellpadding="0" class="tablo">
                    <tr>
                        <th>ID</th>
                        <th>Isim</th>
                        <th>Soyisim</th>
                        <th>KullaniciAdi</th>
                        <th>Mail</th>
                        <th>Sifre</th>
                        <th>Durum</th>
                        <th>Silinmis</th>
                        <th>Islem</th>
                    </tr>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("ID") %></td>
                    <td><%# Eval("Isim") %></td>
                    <td><%# Eval("Soyisim") %></td>
                    <td><%# Eval("KullaniciAdi") %></td>
                    <td><%# Eval("Mail") %></td>
                    <td><%# Eval("Sifre") %></td>
                    <td><%# Eval("Durum") %></td>
                    <td><%# Eval("Silinmis") %></td>
                    <td>
                        <div class="butonlar">
                            <!-- Butonları bu div içine alalım -->
                            <asp:LinkButton ID="lbtn_sil" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="sil" class="tablobutton sil">
    <img src="Resimler/Sil_Icon.png" alt="Sil" />
    <span>Sil</span>
                            </asp:LinkButton>

                            <asp:LinkButton ID="lbtn_hardDelete" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="hardDelete" class="tablobutton sil">
    <img src="Resimler/Sil_Icon.png" alt="Kalıcı Sil" />
    <span>Kalıcı Sil</span>
                            </asp:LinkButton>

                        </div>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
