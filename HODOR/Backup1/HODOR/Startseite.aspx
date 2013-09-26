<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPage.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Startseite.aspx.cs" Inherits="HODOR.Startseite" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="test">
        <asp:MultiView ID="StartseiteMultiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="LoginView" runat="server">
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
                    <asp:Button ID="b_Login" runat="server" CssClass="loginButton" Text="Login" />
                    </form>
                </div>
            </asp:View>
            <asp:View ID="PwResetView" runat="server">
                <div id="reset">
                    <p>Passwort vergessen?<br />Einfach Kundennummer oder E-Mail Adresse eingeben.</p>
                    <br />
                    <asp:Label ID="l_KdNr" runat="server" CssClass="reset" Text="Kundennummer:" />
                    <br />
                    <asp:TextBox ID="tb_KdNr" runat="server" CssClass="reset" MaxLength="50" TextMode="SingleLine" />
                    <br />
                    <asp:Label ID="l_email" runat="server" CssClass="reset" Text="E-Mail:" />
                    <br />
                    <asp:TextBox ID="tb_email" runat="server" CssClass="reset" MaxLength="50" TextMode="SingleLine" />
                    <br />
                    <asp:Button ID="b_reset" runat="server" CssClass="reset" Text="Zurücksetzen" />
                </div>
            </asp:View>
        </asp:MultiView>
        <br />
        <div class="newOld">
            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="PwReset" OnCommand="MenuLink_Command">Reset</asp:LinkButton>
        </div>
    </div>
</asp:Content>
