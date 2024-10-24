<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UyeOl.aspx.cs" Inherits="FolkMetalPerverBlogWebApp.Uyeler.UyeOl" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Üye Olma Paneli</title>
    <link href="UyeCss/UyeOl.css" rel="stylesheet" />
    <style>
        /* CSS ile mesaj paneli için stil */
        .message-panel {
            position: relative; /* Diğer öğeleri kaydırmaması için */
            margin-top: 10px; /* Mesaj paneli ile buton arasında boşluk bırakır */
            display: none; /* Başlangıçta gizli olacak */
            text-align: center; /* Metni ortalar */
        }

        .success-message {
            color: green; /* Başarılı mesaj için yeşil */
        }

        .error-message {
            color: red; /* Hata mesajı için kırmızı */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="tasiyici">
            <h3 class="baslik">Üye Ol</h3>
            <div class="sol">
                <!-- İsim Girişi -->
                <div class="satir">
                    <label class="IsimLabel">İsim</label>
                    <asp:TextBox ID="tb_isim" runat="server" CssClass="metinKutu"></asp:TextBox>
                </div>

                <!-- Soyad Girişi -->
                <div class="satir">
                    <label class="SoyadLabel">Soyad</label>
                    <asp:TextBox ID="tb_soyad" runat="server" CssClass="metinKutu"></asp:TextBox>
                </div>

                <!-- Kullanıcı Adı -->
                <div class="satir">
                    <label class="KullaniciAdiLabel">Kullanıcı Adı</label>
                    <asp:TextBox ID="tb_kullaniciAdi" runat="server" CssClass="metinKutu"></asp:TextBox>
                </div>

                <!-- Mail -->
                <div class="satir">
                    <label class="MailLabel">Mail</label>
                    <asp:TextBox ID="tb_mail" runat="server" CssClass="metinKutu" placeholder="ornek@ornek.com"></asp:TextBox>
                </div>

                <!-- Şifre -->
                <div class="satir">
                    <label class="SifreLabel">Şifre</label>
                    <asp:TextBox ID="tb_sifre" runat="server" CssClass="metinKutu" placeholder="15?***/caT?" TextMode="Password"></asp:TextBox>
                </div>

                <!-- Üye Ol Butonu -->
                <div class="satir">
                    <asp:Button ID="btn_UyeOlButonu" runat="server" OnClick="btn_UyeOlButonu_Click" Text="Üye Ol" CssClass="uyeOlButon" />
                </div>

                <!-- Mesaj Paneli -->
                <asp:Panel ID="pnl_mesaj" runat="server" CssClass="message-panel" Visible="false">
                    <asp:Label ID="lbl_mesaj" runat="server"></asp:Label>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
