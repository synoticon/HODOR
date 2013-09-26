<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPage.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Startseite.aspx.cs" Inherits="HODOR.Startseite" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="test">
        <asp:MultiView ID="StartseiteMultiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="LoginView" runat="server">
                <div id="login">
                    <form method="post">
                    <asp:Label ID="l_Kundennummer" runat="server" Text="Kundennummer:" />
                    <br />
                    <asp:TextBox ID="tb_Kundennummer" runat="server" CssClass="KdNrTB" MaxLength="15"
                        TextMode="SingleLine" ToolTip="Bitte geben Sie Ihre 15 stellige Kundennummer ein" />
                    <br />
                    <asp:Label ID="l_Passwort" runat="server" Text="Passwort:" />
                    <br />
                    <asp:TextBox ID="tb_Passwort" runat="server" CssClass="PwTB" MaxLength="10" TextMode="Password" />
                    <br />
                    <asp:Button ID="b_Login" runat="server" CssClass="loginButton" Text="Login" />
                </div>
                </form>
            </asp:View>
            <br />
            <asp:View ID="RegisterView" runat="server">
                <form method="post">
                <div id="register">
                    
                </div>
                </form>
            </asp:View>
            <br />
            <asp:View ID="PwResetView" runat="server">
            </asp:View>
            <br />
        </asp:MultiView>
        <br />
        <div class="newOld">
            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Register" OnCommand="MenuLink_Command">Registrieren</asp:LinkButton>
            &nbsp;•
            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="PwReset" OnCommand="MenuLink_Command">Reset</asp:LinkButton>
        </div>
    </div>
</asp:Content>
