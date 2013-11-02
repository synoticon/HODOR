<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true"
    CodeBehind="TicketSupport.aspx.cs" Inherits="HODOR.Members.Administration.TicketSupport" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <asp:Label ID="Header" runat="server" Text="Support-Tickets" />
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <h2>
            <asp:LinkButton ID="Ticket_Display" runat="server" OnCommand="MenuLink_Command" CommandName="Display" Visible="false">Tickets Anzeigen</asp:LinkButton>
            &nbsp; |&nbsp;
        <asp:LinkButton ID="Ticket_Create" runat="server" OnCommand="MenuLink_Command" CommandName="Create">Tickets Erstellen</asp:LinkButton></h2>
        <br />
        <br />

        <triggers>
                <asp:AsyncPostBackTrigger ControlID="Ticket_Display" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="Ticket_Create" EventName="Click" />
            </triggers>
        <contenttemplate>
                <asp:View ID="ProfilView" runat="server">
                </asp:View>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="DisplayView" runat="server">
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
                           Eingestellt am
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
                    </asp:View>
                    <br />
              
                    <asp:View ID="CreateView" runat="server">
                         
                    <asp:Table ID="Table2" runat="server" BorderStyle="Dashed" BorderColor="Black" BorderWidth="1" CellPadding="10" GridLines="Both">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>
                           <p>Programm</p>
                        <asp:DropDownList ID="DDL_Programm" runat="server" OnSelectedIndexChanged="SelectedChangeProgramm" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
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
                              </asp:Table>
                        <asp:TextBox ID="ta_Fallbeispiel" TextMode="multiline" Columns="20" Rows="15" runat="server" />
                        </br>
                        <asp:Button ID="b_erstell" runat="server" OnClick="OnClick_b_erstell" Text="Erstell"/>
                        <asp:Label  ID="l_error" runat="server" Text=""></asp:Label>
                    </asp:View>
                </asp:MultiView>
            </contenttemplate>


    </div>
</asp:Content>
