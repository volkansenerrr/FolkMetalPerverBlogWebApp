<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YoneticiResetPassword.aspx.cs" Inherits="FolkMetalPerverBlogWebApp.YoneticiPanel.ResetPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Yönetici Şifre Sıfırlama</title>
    <link href="CSS/YoneticiResetPassword.css" rel="stylesheet" />
    <style>
        /* CSS ile mesaj paneli için stil */
        .message-panel {
            position: relative; /* Diğer öğeleri kaydırmaması için */
            margin-top: 10px;  /* Mesaj paneli ile buton arasında boşluk bırakır */
            display: none;      /* Başlangıçta gizli olacak */
            text-align: center; /* Metni ortalar */
        }
        .success-message {
            color: green; /* Başarılı mesaj için yeşil */
        }
        .error-message {
            color: red;   /* Hata mesajı için kırmızı */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="tasiyici">
            <h3 class="baslik">Yönetici Şifre Sıfırlama</h3>
            <div class="sol">
                <!-- Mail Girişi -->
                <div class="satir">
                    <label class="mailLabel">Mail</label>
                    <asp:TextBox ID="tb_mail" runat="server" CssClass="metinKutu" placeholder="ornek@ornek.com"></asp:TextBox>
                </div>

                <!-- Eski Şifre Girişi -->
                <div class="satir">
                    <label class="sifreLabel">Eski Şifre</label>
                    <asp:TextBox ID="tb_oldPassword" runat="server" CssClass="metinKutu" TextMode="Password" placeholder="Eski Şifrenizi Girin"></asp:TextBox>
                </div>

                <!-- Yeni Şifre Girişi -->
                <div class="satir">
                    <label class="sifreLabel">Yeni Şifre</label>
                    <asp:TextBox ID="tb_newPassword" runat="server" CssClass="metinKutu" TextMode="Password" placeholder="Yeni Şifrenizi Girin"></asp:TextBox>
                </div>

                <!-- Şifreyi Sıfırlama Butonu -->
                <div class="satir">
                    <asp:Button ID="btn_Sifirla" runat="server" OnClick="btn_Sifirla_Click" Text="Şifreyi Sıfırla" CssClass="sifirlaButon" />
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
