<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FolkMetalPerverBlogWebApp.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="lv_makaleler" runat="server">
        <itemtemplate>
            <div class="makale">
                <a href='MakaleDetay.aspx?makale=<%# Eval("ID") %>'>
                    <h2 style="margin: 10px 0;"><%# Eval("Baslik") %></h2>
                    <img src='Resimler/MakaleResimleri/<%# Eval("KapakResim") %>' style="width: 100%" /></a>
                <div class="ayrac"></div>
                <div class="altbilgi">
                    Yazar : <%# Eval("Yazar") %> | Kategori : <%# Eval("Kategori") %>
                    Yayınlama Tarihi: <%# Eval("EklemeTarihi") %> | Görüntüleme Sayısı :
                    
                    <%# Eval("GoruntulemeSayisi") %>
                </div>
                <div class="ayrac"></div>
                <div style="margin: 10px 0;">
                    <%# Eval("Ozet") %>
                     &nbsp;&nbsp;&nbsp;
                   
                    <a href="MakaleDetay.aspx?makale=<%# Eval("ID") %>'">Makalenin Devamı => </a>
                </div>
            </div>
        </itemtemplate>
    </asp:Repeater>

    <div class="spotify-container" style="margin: 20px 0;">
        <h2>Folk Metalperver Müzik Listesi</h2>
       <iframe style="border-radius:12px" src="https://open.spotify.com/embed/playlist/61aAayVP2CkBlgkLnNI2UF?utm_source=generator" width="100%" height="352" frameBorder="0" allowfullscreen="" allow="autoplay; clipboard-write; encrypted-media; fullscreen; picture-in-picture" loading="lazy"></iframe>
    </div>
</asp:Content>
