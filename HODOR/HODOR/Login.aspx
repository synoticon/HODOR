<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" Language="C#"
    MasterPageFile="~/SubPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="HODOR.Login" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="login">
        <form method="post" action="">
        <asp:Label ID="l_Kundennummer" runat="server" Text="Kundennummer:" />
        <br />
        <asp:TextBox ID="tb_Kundennummer" runat="server" CssClass="KdNrTB" MaxLength="15"
            TextMode="SingleLine" ToolTip="Bitte geben Sie Ihre 15 stellige Kundennummer ein" />
        <br />
        <asp:Label ID="l_Passwort" runat="server" Text="Passwort:" />
        <br />
        <asp:TextBox ID="tb_Passwort" runat="server" CssClass="PwTB" MaxLength="10" TextMode="Password" />
        <br />
        <asp:Button ID="b_Login" runat="server" CssClass="loginButton" Text="Login" OnClick="b_Login_Click" />
    </div>
    </form>
</asp:Content>
