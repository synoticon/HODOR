<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPageIntern.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Produkte.aspx.cs" Inherits="HODOR.Produkte" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form1" runat="server">
        <asp:View ID="SelectView" runat="server">

        </asp:View>
        <asp:View ID="InfoView" runat="server">

        </asp:View>
    </form>




    <!--Falls noch zum Vergleich benötigt

        <div id="test"> 
        <div id="produktinfo">
            <asp:Label ID="lb_programmname_text" runat="server" Text="Programm:" />
            <br />
            <asp:Label ID="lb_programmname" runat="server" Text="" />
            <br />
            <asp:DropDownList ID="DDL_Release" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                DataTextField="Name" DataValueField="id" OnSelectedIndexChanged="SelectedChange">
                <asp:ListItem Text="---Select---" Value="0" />
            </asp:DropDownList>
            <asp:DropDownList ID="DDL_Build" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                DataTextField="Name" DataValueField="id" OnSelectedIndexChanged="itemSelectedBuild">
                <asp:ListItem Text="---Select---" Value="0" />
            </asp:DropDownList>
        </div>
    </div>-->
</asp:Content>
