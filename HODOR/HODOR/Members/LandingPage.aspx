<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="HODOR.Members.LandingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form2" runat="server">
        <div id="ajaxTest">
            <asp:LinkButton ID="Product" runat="server" OnCommand="MenuLink_Command" CommandName="Product">Produkte</asp:LinkButton>
            &nbsp; |&nbsp;
        <asp:LinkButton ID="User" runat="server" OnCommand="MenuLink_Command" CommandName="User">Benutzer</asp:LinkButton>
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Product" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="User" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:View ID="ProfilView" runat="server">
                    </asp:View>
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="ProductView" runat="server">
                            <p>
                                <!--Hier kommt die Produktauflistung per ListView rein-->
                            </p>
                        </asp:View>
                        <br />
                        <asp:View ID="UserView" runat="server">
                            <p>
                                <!--
                                Hier noch eine Auflistung der User einbauen.
                                Eventuell die Anlage der User noch gesondert behandeln?
                             -->
                                <asp:Label ID="l_FirmenName" runat="server" Text="Name der Firma:" />
                                <br />
                                <asp:TextBox ID="tb_FirmenName" runat="server" MaxLength="255" TextMode="SingleLine" />
                                <br />
                                <asp:Label ID="l_KundenNummer" runat="server" Text="Kundennummer:" />
                                <br />
                                <asp:TextBox ID="tb_KundenNummer" runat="server" MaxLength="255" TextMode="SingleLine" />
                                <br />
                                <asp:Label ID="l_EMail" runat="server" Text="E-Mail:" />
                                <br />
                                <asp:TextBox ID="tb_EMail" runat="server" MaxLength="255" TextMode="SingleLine" />
                                <br />
                                <asp:Button ID="b_Register" runat="server" Text="Anlegen" OnClick="b_Register_Click" />
                            </p>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</asp:Content>
