<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPageIntern.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Produkte.aspx.cs" Inherits="HODOR.Produkte" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div id="test">
        <div id="produktinfo">
            <asp:Label ID="lb_programmname_text" runat="server" Text="Programm:" />
            <br />
            <asp:Label ID="lb_programmname" runat="server" Text="" />


            <br />
            <asp:Table ID="Table1" runat="server" BorderStyle="Dashed" BorderColor="Black" BorderWidth="1" CellPadding="10" GridLines="Both">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>

                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <asp:DropDownList ID="DDL_Release" runat="server" OnSelectedIndexChanged="SelectedChangeRelease" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>

                        <asp:DropDownList ID="DDL_SubRelease" runat="server" OnSelectedIndexChanged="SelectedChangeSubRelease" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
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
                        <asp:Label ID="l_Releasediscription" runat="server" Text="" />
                    </asp:TableCell>
                      <asp:TableCell>
                        <asp:Label ID="l_SubReleasediscription" runat="server" Text="" />
                    </asp:TableCell>
                      <asp:TableCell>
                        <asp:Label ID="l_Builddiscription" runat="server" Text="" />
                           <asp:Button ID="b_download" runat="server" OnClick="OnClick_b_download" Text="Download" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>






        </div>
    </div>
</asp:Content>
