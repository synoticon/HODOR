<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="HODOR.Members.LandingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        <div id="ajaxTest">
              <p>
                                <asp:Label ID="l_benutzer" runat="server" Text="Name:" />
                                <br />
                                <asp:TextBox ID="tb_benutzer" runat="server" MaxLength="255" TextMode="SingleLine" />
                                <br />
                                <asp:Label ID="l_kundenNummer" runat="server" Text="Kundennummer:" />
                                <br />
                                <asp:TextBox ID="tb_kundenNummer" runat="server" MaxLength="255" TextMode="SingleLine" />
                                <br />
                                <asp:Label ID="l_eMail" runat="server" Text="E-Mail:" />
                                <br />
                                <asp:TextBox ID="tb_eMail" runat="server" MaxLength="255" TextMode="SingleLine" />
                                <br />
                                <asp:Label ID="l_rolle" runat="server" Text="rolle" />
                                <br />
                                <asp:TextBox ID="tb_rolle" runat="server" MaxLength="255" TextMode="SingleLine" />
                                <br />
                            </p>
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
                                <asp:ListBox runat="server" ID="listbox_user" Height="108px" OnSelectedIndexChanged="listbox_user_SelectedIndexChanged" Width="404px"></asp:ListBox>                  

                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:Content>
