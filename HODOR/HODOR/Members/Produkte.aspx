<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPageIntern.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Produkte.aspx.cs" Inherits="HODOR.Produkte" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div id="test">
        <div id="produktinfo">
            <h1><asp:Label ID="lb_programmname_text" runat="server" Text="Programm:" /></h1>
            <br />
            <h2><asp:Label ID="lb_programmname" runat="server" Text="" /></h2>
               <asp:DropDownList ID="DDL_Programm" runat="server" OnSelectedIndexChanged="SelectedChangeProgramm" AutoPostBack="true" Visible ="false">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
              <br />
            Beschreibung:
             <br />
              <asp:Label ID="l_Programmdiscripion" runat="server" Text="" />
            <br />
            <asp:Table ID="Table1" runat="server" BorderStyle="Dashed" BorderColor="Black" BorderWidth="1" CellPadding="10" GridLines="Both">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>

                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <p>Release</p>
                        <asp:DropDownList ID="DDL_Release" runat="server" OnSelectedIndexChanged="SelectedChangeRelease" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <p>Sub-Release</p>
                        <asp:DropDownList ID="DDL_SubRelease" runat="server" OnSelectedIndexChanged="SelectedChangeSubRelease" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <p>Build</p>
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
                        <asp:Label ID="l_Releasedescription" runat="server" Text="" />
                    </asp:TableCell>
                      <asp:TableCell>
                        <asp:Label ID="l_SubReleasedescription" runat="server" Text="" />
                    </asp:TableCell>
                      <asp:TableCell>
                        <asp:Label ID="l_Builddescription" runat="server" Text="" />
                           <br />
                           <asp:Button ID="b_download" runat="server" OnClick="OnClick_b_download" Text="Download" Visible="false"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>






        </div>
    </div>
</asp:Content>
