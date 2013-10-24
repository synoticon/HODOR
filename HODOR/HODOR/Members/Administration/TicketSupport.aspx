<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true"
    CodeBehind="TicketSupport.aspx.cs" Inherits="HODOR.Members.Administration.TicketSupport" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
      <h2>Ticket Liste</h2>
        <asp:Table ID="Table1" runat="server" BorderStyle="Dashed" BorderColor="Black" BorderWidth="1" CellPadding="10" GridLines="Both">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>
                           Ticketnummer
                </asp:TableHeaderCell>
                  <asp:TableHeaderCell>
                           Eingestellt von
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                           Programm
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                           Release
                </asp:TableHeaderCell>
                <asp:TableHeaderCell>
                           Subrelease
                </asp:TableHeaderCell>
                   <asp:TableHeaderCell>
                           Build
                </asp:TableHeaderCell>
                   <asp:TableHeaderCell>
                           Beschreibung
                </asp:TableHeaderCell>
                 <asp:TableHeaderCell>
                           Status
                </asp:TableHeaderCell>
                  
            </asp:TableHeaderRow>

        </asp:Table>
    </div>
</asp:Content>
