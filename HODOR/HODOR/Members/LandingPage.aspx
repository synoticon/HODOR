﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="HODOR.Members.LandingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div id="ajaxTest">
        <h1>Benutzer Profil</h1>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lStatic_benutzer" runat="server" Text="Name:" /></td>

                <td>
                    <asp:Label ID="l_benutzer" runat="server" MaxLength="255" TextMode="SingleLine" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lStatic_kundenNummer" runat="server" Text="Kundennummer:" /></td>

                <td>
                    <asp:Label ID="l_kundenNummer" runat="server" MaxLength="255" TextMode="SingleLine" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lStatic_eMail" runat="server" Text="E-Mail:" /></td>

                <td>
                    <asp:Label ID="l_eMail" runat="server" MaxLength="255" TextMode="SingleLine" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lStatic_rolle" runat="server" Text="Rolle:" /></td>

                <td>
                    <asp:Label ID="l_rolle" runat="server" MaxLength="255" TextMode="SingleLine" /></td>

        </table>
        <br></br>
        <asp:Button ID="b_edit" runat="server" OnClick="b_edit_Click" Text="Bearbeiten"  Visible="false" />
        <br></br>
        <asp:Button ID="b_licenz" runat="server" Text="Lizenzen Anzeigen" OnClick="b_licenz_Click"/>
        <br></br>

      <h2><asp:LinkButton ID="Product" runat="server" OnCommand="MenuLink_Command" CommandName="Product">Produkte</asp:LinkButton>
        &nbsp; |&nbsp;
        <asp:LinkButton ID="User" runat="server" OnCommand="MenuLink_Command" CommandName="User" Visible="false">Benutzer</asp:LinkButton></h2>  
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
                            <asp:ListBox runat="server" ID="listbox_prog" Height="108px" Width="404px"></asp:ListBox>
                        </p>
                        <p>
                            <asp:Button ID="b_prog_display" runat="server" Text="Programm Anzeigen" OnClick="OnClick_b_prog_display" />
                        </p>
                    </asp:View>
                    <br />
                    <asp:View ID="UserView" runat="server">
                        <p>
                            <asp:ListBox runat="server" ID="listbox_user" Height="108px" Width="404px"></asp:ListBox>
                        </p>
                        <p>
                            <asp:Button ID="b_user_display" runat="server" Text="User Anzeigen" OnClick="OnClick_b_user_display"/>
                        </p>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br></br>
        <br></br>
        <br></br>

        <h2>Download History</h2>
        <asp:Table ID="Table1" runat="server" BorderStyle="Dashed" BorderColor="Black" BorderWidth="1" CellPadding="10" GridLines="Both">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                            Benutzer Nummer
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                            Programm
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                            Version
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                            Datum
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>

        </asp:Table>

    </div>

</asp:Content>
