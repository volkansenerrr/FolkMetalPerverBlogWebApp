<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UyeGiris.aspx.cs" Inherits="FolkMetalPerverBlogWebApp.Uyeler.UyeGiris" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Üye Giriş</title>
    <link href="UyeCss/UyeGirisStil.css" rel="stylesheet" />
    <!-- Font Awesome Kütüphanesi -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
    <style>
        .gizliGoz {
            cursor: pointer;
            position: absolute;
            right: 1px; /* Göz simgesinin konumu */
            top: 52%; /* Dikey ortalama için */
            transform: translateY(-50%); /* Dikey olarak ortalar */
            background: none; /* Arka plan yok */
            border: none; /* Kenar yok */
            color: #28a745; /* Göz simgesinin rengi yeşil */
            font-size: 18px; /* İkon boyutu */
            z-index: 10; /* Üstte görünmesi için */
        }
        .inputWrapper {
            position: relative; /* Göz simgesi için konumlandırma */
            display: flex; /* Flexbox kullanarak içeriği hizala */
            align-items: center; /* Dikey ortalama için */
        }
        .metinKutu {
            padding-right: 40px; /* Sağda simge için alan bırak */
            width: 100%; /* Genişliği korumak için */
            box-sizing: border-box; /* Padding içermesi için */
        }
    </style>
    <script>
        function togglePasswordVisibility() {
            var passwordField = document.getElementById('<%= tb_sifre.ClientID %>');
            var toggleButton = document.getElementById('gizliGoz');

            if (passwordField.type === 'password') {
                passwordField.type = 'text'; // Şifreyi görünür yap
                toggleButton.innerHTML = '<i class="fas fa-eye-slash"></i>'; // Göz kapalı simgesi
            } else {
                passwordField.type = 'password'; // Şifreyi gizle
                toggleButton.innerHTML = '<i class="fas fa-eye"></i>'; // Göz açık simgesi
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="tasiyici">
            <div class="sol">
                <div class="solBaslik">
                    <h3>Üye Girişi</h3>
                </div>
                <div class="satir">
                    <label>Mail</label><br />
                    <asp:TextBox ID="tb_mail" CssClass="metinKutu" runat="server" placeholder="ornek@ornek.com"></asp:TextBox>
                </div>
                <div class="satir">
                    <label>Şifre</label><br />
                    <div class="inputWrapper">
                        <asp:TextBox ID="tb_sifre" CssClass="metinKutu" runat="server" placeholder="15?***/caT?" TextMode="Password"></asp:TextBox>
                        <button type="button" id="gizliGoz" class="gizliGoz" onclick="togglePasswordVisibility()">
                            <i class="fas fa-eye"></i> <!-- Başlangıçta göz simgesi -->
                        </button>
                    </div>
                </div>
                <div class="satir">
                    <asp:Button ID="btn_UyeGirisiYap" runat="server" CssClass="UyeGirisButon" OnClick="btn_UyeGirisiYap_Click" Text="Üye Giriş" />
                </div>
                <asp:Panel ID="pnl_basarisiz" CssClass="panelBasarisiz" Visible="false" runat="server">
                    <asp:Label ID="lbl_mesaj" runat="server">Kullanıcı Bulunamadı</asp:Label>
                </asp:Panel>
                <div class="satir">
                    <a href="UyeResetPassword.aspx" class="unuttum">Şifremi unuttum?</a>
                </div>
            </div>
            <div class="sag">
                <h2 class="sagbaslik">HOŞ GELDİN, CESUR SAVAŞÇI!</h2>
                Burası üye giriş kapısı, Folk Metal tınılarının yankılandığı, savaşçı ruhların canlandığı bu diyarak giriş yapmak istiyorsan üye ol 
                <br />
                <br />
                <asp:Button ID="btn_UyeOl" runat="server" CssClass="UyeOlButon" OnClick="btn_UyeOl_Click" Text="Üye Ol" />
            </div>
            <div style="clear: both"></div>
        </div>
    </form>
</body>
</html>
