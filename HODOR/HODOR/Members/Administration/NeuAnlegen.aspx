<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true" CodeBehind="NeuAnlegen.aspx.cs" Inherits="HODOR.Members.Administration.NeuAnlegen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <asp:Label ID="l_Build" runat="server" Text="Was möchten Sie anlegen: " Font-Bold="true" Font-Size="Larger" /><br />
    <asp:LinkButton ID="lb_user" runat="server" OnCommand="MenuLink_Command" CommandName="User">Benutzer</asp:LinkButton><br />
    <asp:LinkButton ID="lv_product" runat="server" OnCommand="MenuLink_Command" CommandName="Product">Produkt</asp:LinkButton>
    <br />
    <br />
    <asp:Label ID="is_registered" runat="server" Visible="false" Text="Erstellung erfolgreich." />

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="-1">
        <asp:View ID="UserView" runat="server">
            <asp:Label ID="l_KdNr" runat="server" Text="Kundennummer:" /><br />
            <asp:TextBox ID="tb_KdNr" runat="server" /><br />
            <asp:Label ID="l_FirmenName" runat="server" Text="Firmenname:" /><br />
            <asp:TextBox ID="tb_Firmenname" runat="server" /><br />
            <asp:Label ID="l_EMail" runat="server" Text="E-Mail:" /><br />
            <asp:TextBox ID="tb_EMail" runat="server" /><br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="tb_EMail" ErrorMessage="Bitte geben Sie eine valide E-Mail-Adresse ein (bsp.: Member@Domain.de)."
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" /><br />
            <asp:Label ID="l_PW" runat="server" Text="Passwort :" /><br />
            <asp:TextBox ID="tb_PW" runat="server" /><br />
            <asp:Label ID="l_role" runat="server" Text="Berechtigungsrolle:" /><br />
            <asp:DropDownList ID="ddl_roles" runat="server" /><br />
            <asp:Button ID="b_register" runat="server" Text="Registrieren"
                OnClick="b_Register_Click" /><br />
        </asp:View>
        <asp:View ID="ProductView" runat="server">

            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <asp:RegularExpressionValidator ID="regexpName" runat="server"
                ErrorMessage="This expression does not validate."
                ControlToValidate="FileUpload1"
                ValidationExpression=".*[a-zA-Z0-9]{1,254}_\d{1,10}_\d{1,10}_\d{1,10}..*" />
            <br />
            <br />
            <asp:Button ID="b_upload" runat="server" OnClick="OnClick_b_upload" Text="Upload File" /><br />
            <asp:Label ID="l_message" runat="server"></asp:Label>
            <asp:Table ID="Table1" runat="server" BorderStyle="Dashed" BorderColor="Black" BorderWidth="1" CellPadding="10" GridLines="Both">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <asp:DropDownList ID="DDL_Programm" runat="server" OnSelectedIndexChanged="SelectedChangeProgramm" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        Release
                        <asp:DropDownList ID="DDL_Release" runat="server" OnSelectedIndexChanged="SelectedChangeRelease" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        Sub-Release
                        <asp:DropDownList ID="DDL_SubRelease" runat="server" OnSelectedIndexChanged="SelectedChangeSubRelease" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        Build
                        <asp:DropDownList ID="DDL_Build" runat="server" OnSelectedIndexChanged="SelectedChangeBuild" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Beschreibung:
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="ta_Programmdiscription" TextMode="multiline" Columns="20" Rows="15" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="ta_Releasediscription" TextMode="multiline" Columns="20" Rows="15" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="ta_SubReleasediscription" TextMode="multiline" Columns="20" Rows="15" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="ta_Builddiscription" TextMode="multiline" Columns="20" Rows="15" runat="server" />

                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="b_save" OnClick="OnClick_b_save" Text="Save" runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
