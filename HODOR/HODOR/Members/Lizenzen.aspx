<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true"
    CodeBehind="Lizenzen.aspx.cs" Inherits="HODOR.Members.Lizenzen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <asp:Label ID="Header" runat="server" Text="Lizenzen für " />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <asp:TabContainer runat="server"
        OnClientActiveTabChanged="ClientFunction"
        Height="150px">
        <asp:TabPanel runat="server" HeaderText="Signatur" CssClass="CustomTabStyle">
            <HeaderTemplate>
                sadfdsfsadf
            </HeaderTemplate>
            <ContentTemplate>
                fkajsdfljdfkj
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>

    <asp:Table ID="t_user" runat="server">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell CssClass="kdNrHeader">KdNr</asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="nameHeader">Firmenname</asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="eMailHeader">E-Mail-Adresse</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow CssClass="contentRow">
            <asp:TableCell>
                <asp:Label ID="l_KdNr" runat="server" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="l_FirmenName" runat="server" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="l_EMail" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:ListView ID="lv_User" runat="server" DataKeyNames=""
        OnSelectedIndexChanging="lv_User_SelectedIndexChanging">
        <LayoutTemplate>
            <table id="Table3" class="ExampleView" runat="server" width="100%">
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" class="userTable">
                            <tr id="Tr2" runat="server" style="">
                                <th id="Th1" class="emptyTH" runat="server"></th>
                                <th id="Th2" class="KdNrTH" runat="server">Lizenz Nr.</th>
                                <th id="Th3" class="NameTH" runat="server">Typ</th>
                                <th id="Th4" class="EMailTH" runat="server"></th>
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
                    <%# Eval("LizenzID") %>
                </td>
                <td>
                    <asp:Label ID="l_typ" runat="server" />
                </td>
                <td></td>
                <td class="action">
                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten" CommandName="Edit" />
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
                    <asp:Label ID="l_typ" runat="server" />
                </td>
                <td></td>
                <td class="action">
                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten" CommandName="Edit" />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <SelectedItemTemplate>
            <tr class="selectedItemTemplate">
                <td>
                    <asp:Image ID="pictureControlID" runat="server" AlternateText="ArrowDetails"
                        ImageUrl="~/images/ListView/ArrowDetails.png" />
                </td>
                <td>
                    <%# Eval("LizenzID") %>
                </td>
                <td>
                    <asp:Label ID="l_typ" runat="server" />
                </td>
                <td>
                    <asp:Label ID="l_dauer" runat="server" />
                </td>
                <td class="action">
                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten"
                        CommandName="Edit" /><br />
                    <asp:LinkButton ID="lb_delete" runat="server" Text="Löschen"
                        CommandName="Delete" OnClientClick="return confirm('Sind Sie sicher, dass Sie diesen Benutzer löschen wolle?');" />
                </td>
            </tr>
        </SelectedItemTemplate>
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
                    <asp:DropDownList ID="ddl_typ" runat="server" />
                </td>
                <td>
                    <asp:DropDownList ID="ddl_release" runat="server" />

                    <asp:ToolkitScriptManager ID="tksm_date" runat="server" />

                    <asp:TextBox ID="tb_startDate" runat="server" Text="" /><br />
                    <asp:CalendarExtender ID="ce_startDate" runat="server" TargetControlID="tb_startDate" />

                    <asp:TextBox ID="tb_endDate" runat="server" Text="" />
                    <asp:CalendarExtender ID="ce_endDate" runat="server" TargetControlID="tb_endDate" />
                </td>
                <td>
                    <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Ändern"
                        CausesValidation="true" Text="Ändern" />
                    <asp:LinkButton ID="CancelButton" runat="server" CommandName="Abbrechen"
                        CausesValidation="false" Text="Cancel" /><%-- Muss noch richtig eingerichtet werden, da der Abbruch noch nicht richtig durchgeführt wird --%>
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>

</asp:Content>
