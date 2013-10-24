<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true"
    CodeBehind="Verwaltung.aspx.cs" Inherits="HODOR.Members.Administration.ProduktVerwaltung" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <p>
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="l_SearchInput" runat="server" Text="Suchbegriff eingeben:    " /><br />
                        <asp:TextBox ID="tb_SearchInput" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:RadioButton ID="rb_UserSearch" runat="server" Text="Benutzer" GroupName="RadioButtonSearch" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:RadioButton ID="rb_ProductSearch" runat="server" Text="Produkt" GroupName="RadioButtonSearch" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:RegularExpressionValidator ID="SearchInputValidator" runat="server"
                            ControlToValidate="tb_SearchInput" ErrorMessage="Bitte geben Sie einen Suchbegriff ein."
                            ValidationExpression="[a-zA-Z0-9]{1,254}"></asp:RegularExpressionValidator><br />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="b_Search" runat="server" OnClick="SearchButton_Click" Text="Suchen" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </p>
        <br />
          <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="UserView" runat="server">
                <p>
                    <asp:ListBox ID="lb_User" runat="server" OnSelectedIndexChanged="lb_SelectedIndexChanged_Main" AutoPostBack="true" /><br />
                    <asp:Label ID="l_Name" runat="server" Text="" /><br />
                    <asp:Label ID="l_NutzerNr" runat="server" Text="" /><br />
                    <asp:Label ID="l_EMail" runat="server" Text="" /><br />
                    <asp:Label ID="l_Rolle" runat="server" Text="" /><br />
                    <asp:Label ID="l_LizenzZaehler" runat="server" Text="Der Kunde hat " Visible="false" />
                    <asp:Label ID="l_LizenzAnzahl" runat="server" Text="" />
                    <asp:Label ID="l_ende" runat="server" Text=" Lizenzen" Visible="false" />
                </p>
            </asp:View>
            <asp:View ID="ProductView" runat="server">
                <p>Bitte geben sie den genauen Produktnamen ein</p>
                <p>
                    <asp:ListBox ID="lb_Product" runat="server" OnSelectedIndexChanged="lb_SelectedIndexChanged_Main" /><br />
                    <asp:Label ID="l_ProgrammName" runat="server" Text="" /><br />
                    <asp:ListBox ID="lb_Release" runat="server" OnSelectedIndexChanged="lb_SelectedIndexChanged_Sub" Visible="false" /><br />
                    <asp:Label ID="l_ReleaseNr" runat="server" Text="" /><br />
                    <asp:Label ID="l_ReleaseDatum" runat="server" Text="" /><br />
                    <asp:Label ID="l_Beschreibung" runat="server" Text="" />
                </p>
            </asp:View>
          
        </asp:MultiView>&nbsp;<br />
    </div>
</asp:Content>
