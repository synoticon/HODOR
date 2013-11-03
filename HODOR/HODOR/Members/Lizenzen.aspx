<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true"
    CodeBehind="Lizenzen.aspx.cs" Inherits="HODOR.Members.Lizenzen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <asp:Label ID="Header" runat="server" Text="Lizenzen von " />
    <asp:Label ID="l_KdNr" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <br />
    <br />

    <asp:ListView ID="lv_User" runat="server" DataKeyNames=""
        OnSelectedIndexChanging="lv_User_SelectedIndexChanging"
        OnItemEditing="lv_User_ItemEditing">
        <LayoutTemplate>
            <table id="Table3" class="ExampleView" runat="server" width="100%">
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" class="userTable">
                            <tr id="Tr2" runat="server" style="">
                                <th id="Th1" class="emptyTH" runat="server"></th>
                                <th id="Th2" class="KdNrTH" runat="server">Lizenz Nr.</th>
                                <th id="Th3" class="NameTH" runat="server">Lizenzbeginn</th>
                                <th id="Th4" class="EMailTH" runat="server">Lizenzende</th>
                                <th id="Th5" class="ActionTH" runat="server">&bull;</th>
                            </tr>
                            <tr id="itemPlaceHolder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <table id="Table2" runat="server" style="">
                <tr>
                    <td>Kein Treffer.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr class="itemTemplate">
                <td>
                    <asp:Image ID="pictureControlID" runat="server" AlternateText="ArrowItem"
                        ImageUrl="~/images/ListView/ArrowItem.png" />
                </td>
                <td>
                    <asp:Label ID="l_lizenzID" runat="server" Text='<%# Eval("LizenzID") %>' />
                </td>
                <td>
                    <%# Eval("StartDatum", "{0:dd.MM.yy}") %>
                </td>
                <td>
                    <%# Eval("EndDatum", "{0:dd.MM.yy}") %>
                </td>
                <td class="action">
                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten"
                        CommandName="Edit" /><br />
                    <asp:LinkButton ID="lb_delete" runat="server" Text="Löschen"
                        CommandName="Delete" OnClientClick="return confirm('Sind Sie sicher, dass Sie diese Lizenz löschen wolle?');" />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="alternativItemTemplate">
                <td>
                    <asp:Image ID="pictureControlID" runat="server" AlternateText="ArrowAlternativ"
                        ImageUrl="~/images/ListView/ArrowAlternativ.png" />
                </td>
                <td>
                    <%# Eval("LizenzID") %>
                </td>
                <td>
                    <%# Eval("StartDatum", "{0:dd.MM.yy}") %>
                </td>
                <td>
                    <%# Eval("EndDatum", "{0:dd.MM.yy}") %>
                </td>
                <td class="action">
                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten"
                        CommandName="Edit" /><br />
                    <asp:LinkButton ID="lb_delete" runat="server" Text="Löschen"
                        CommandName="Delete" OnClientClick="return confirm('Sind Sie sicher, dass Sie diese Lizenz löschen wolle?');" />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr class="editItemTemplate">
                <td>
                    <asp:Image ID="pictureControlID" runat="server" AlternateText="Edit"
                        ImageUrl="~/images/ListView/Edit.png" />
                </td>
                <td>
                    <asp:TextBox ID="EditNutzerNr" runat="server" Text='<%# Bind("LizenzID") %>' TextMode="SingleLine" MaxLength="255" />
                </td>
                <td>
                    <asp:TextBox ID="tb_startDate" runat="server" Text='<%# Bind("StartDatum") %>' />
                    <asp:CalendarExtender ID="ce_startDate" runat="server" TargetControlID="tb_startDate" />
                </td>
                <td>
                    <asp:TextBox ID="tb_endDate" runat="server" Text='<%# Bind("EndDatum") %>' />
                    <asp:CalendarExtender ID="ce_endDate" runat="server" TargetControlID="tb_endDate" />
                </td>
                <td>
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update"
                        CausesValidation="true" Text="Ändern" />
                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel"
                        CausesValidation="false" Text="Abbrechen" /><%-- Muss noch richtig eingerichtet werden, da der Abbruch noch nicht richtig durchgeführt wird --%>
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>

    <br />
    <asp:LinkButton ID="lb_Build" runat="server" OnClick="lb_Build_Click" >Neu anlegen</asp:LinkButton><br />

</asp:Content>
