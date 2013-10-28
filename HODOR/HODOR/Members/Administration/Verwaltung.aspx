<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true"
    CodeBehind="Verwaltung.aspx.cs" Inherits="HODOR.Members.Administration.Verwaltung" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%--DataSourcen für die ListViews--%>
    <%-- Suche nach Produkten mit dem Programmnamen als Suchkriterium --%>
    <asp:EntityDataSource ID="ProductDataSource" runat="server"
        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
        EnableFlattening="False" EntitySetName="Releases" AutoGenerateWhereClause="True"
        Select="it.[Releasenummer], it.[Releasedatum], it.[Beschreibung]" EntityTypeFilter="" Where="">
        <WhereParameters>
            <asp:ControlParameter ControlID="l_ProgrammID" Name="ReleaseVonProgramm" PropertyName="Text" Type="Int32" />
        </WhereParameters>
    </asp:EntityDataSource>

    <%-- Suche nach Benutzern mit der NutzerNr als Suchkriterium --%>
    <asp:EntityDataSource ID="UserDataSourceByNutzerNr" runat="server"
        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
        EnableFlattening="False" EntitySetName="Benutzers" AutoGenerateWhereClause="false"
        Select="it.[NutzerNr], it.[Email], it.[Name], it.[RolleID]" EntityTypeFilter=""
        Where="it.[NutzerNr] LIKE '%' + @NutzerNr + '%'">
        <WhereParameters>
            <asp:ControlParameter ControlID="tb_SearchInput" Name="NutzerNr" PropertyName="Text" Type="String" />
        </WhereParameters>
    </asp:EntityDataSource>

    <%-- Suche nach Benutzern mit dem Namen als Suchkriterium --%>
    <asp:EntityDataSource ID="UserDataSourceByName" runat="server"
        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
        EnableFlattening="False" EntitySetName="Benutzers" AutoGenerateWhereClause="false"
        Select="it.[NutzerNr], it.[Email], it.[Name], it.[RolleID]" EntityTypeFilter=""
        Where="it.[NutzerNr] LIKE '%' + @Name + '%'">
        <WhereParameters>
            <asp:ControlParameter ControlID="tb_SearchInput" Name="Name" PropertyName="Text" Type="String" />
        </WhereParameters>
    </asp:EntityDataSource>

    <%-- Suche nach Benutzern mit dem Namen und der NutzerNr als Suchkriterien --%>
    <asp:EntityDataSource ID="UserDataSourceByNutzerNrAndName" runat="server"
        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
        EnableFlattening="False" EntitySetName="Benutzers" AutoGenerateWhereClause="false"
        Select="it.[NutzerNr], it.[Email], it.[Name], it.[RolleID]"
        Where="it.[NutzerNr] LIKE '%' + @NutzerNr + '%' OR it.[Name] LIKE '%' + @Name + '%'">
        <WhereParameters>
            <asp:ControlParameter ControlID="tb_SearchInput" Name="NutzerNr" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="tb_SearchInput" Name="Name" PropertyName="Text" Type="String" />
        </WhereParameters>
    </asp:EntityDataSource>

    <div>
        <p>
            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="l_SearchInput" runat="server" Text="Suchbegriff eingeben:    " /><br />
                        <asp:TextBox ID="tb_SearchInput" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:RadioButton ID="rb_UserSearch" runat="server" Text="Benutzer" GroupName="RadioButtonSearch" OnCheckedChanged="rb_UserSearch_CheckedChanged" AutoPostBack="true" /><br />
                        <asp:RadioButton ID="rb_ProductSearch" runat="server" Text="Produkt" GroupName="RadioButtonSearch" OnCheckedChanged="rb_ProductSearch_CheckedChanged" AutoPostBack="true" />
                    </asp:TableCell><asp:TableCell>
                        <asp:CheckBox ID="cb_NutzerNr" runat="server" Text="per NutzerNr" ValidationGroup="CheckBoxSearch" Visible="false" /><br />
                        <asp:CheckBox ID="cb_Name" runat="server" Text="per Firmenname" ValidationGroup="CheckBoxSearch" Visible="false" />
                    </asp:TableCell><asp:TableCell>
                        <asp:Button ID="b_Search" runat="server" OnClick="SearchButton_Click" Text="Suchen" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:RegularExpressionValidator ID="SearchInputValidator" runat="server"
                            ControlToValidate="tb_SearchInput" ErrorMessage="Bitte geben Sie einen Suchbegriff ein."
                            ValidationExpression="[a-zA-Z0-9]{1,254}"></asp:RegularExpressionValidator><br />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </p>
        <br />
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="PreResultView" runat="server">
                <p>
                    <asp:Label ID="l_noCatch" runat="server" Text="Kein Treffer." Visible="false" /><br />
                    <asp:ListBox ID="lb_Product" runat="server" OnSelectedIndexChanged="lb_SelectedIndexChanged"
                        AutoPostBack="true" Visible="false" /><br />
                </p>
            </asp:View>
            <asp:View ID="ResultView" runat="server">
                <p>
                    <asp:Label ID="l_ProgrammName" runat="server" Text="" />
                    <asp:Label ID="l_ProgrammID" runat="server" Text="" Visible="false" />><br />

                    <%-- ListView für die Ergebnisse der UserSuche --%>
                    <asp:ListView ID="lv_User" runat="server"
                        OnSelectedIndexChanging="lvwUsers_SelectedIndexChanging">
                        <LayoutTemplate>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ListView">
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <EmptyDataTemplate>
                            <table id="Table2" runat="server" style="">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <th class="NzNr_incon1">
                                    <li><%# Eval("nutzerNr") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                        CommandName="Select" CommandArgument='<%# Eval("nutzerNr") %>' />
                                </th>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <th class="NzNr_incon2">
                                    <li><%# Eval("nutzerNr") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                        CommandName="Select" CommandArgument='<%# Eval("nutzerNr") %>' />
                                </th>
                            </tr>
                        </AlternatingItemTemplate>
                        <SelectedItemTemplate>
                            <tr>
                                <th class="NzNr_incon3">
                                    <li><%# Eval("nutzerNr") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten"
                                        CommandName="Edit" />
                                </th>
                            </tr>
                            <tr class="sub">
                                <td colspan="4">
                                    <li><%# Eval("name") %></li>
                                </td>
                                <td colspan="4">
                                    <li><%# Eval("email") %></li>
                                </td>
                                <td colspan="4">
                                    <li><%# Eval("rolle") %></li>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                        <EditItemTemplate>
                            <tr>
                            </tr>
                        </EditItemTemplate>
                    </asp:ListView>

                    <%-- ListView für die Ergebnisse der Prduktsuche --%>
                    <asp:ListView ID="lv_Product" runat="server" DataSourceID=""
                        OnSelectedIndexChanging="lvwProducts_SelectedIndexChanging">
                        <LayoutTemplate>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ListView">
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <EmptyDataTemplate>
                            <table id="Table2" runat="server" style="">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr>
                                <th class="RlNr_incon1">
                                    <li><%# Eval("Releasenummer") %></li>
                                </th>
                                <th>
                                    <li><%# Eval("Releasedatum") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                        CommandName="Select" />
                                </th>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <th class="RlNr_incon2">
                                    <li><%# Eval("Releasenummer") %></li>
                                </th>
                                <th>
                                    <li><%# Eval("Releasedatum") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                        CommandName="Select" />
                                </th>
                            </tr>
                        </AlternatingItemTemplate>
                        <SelectedItemTemplate>
                            <tr>
                                <th class="RlNr_incon3">
                                    <li><%# Eval("Releasenummer") %></li>
                                </th>
                                <th>
                                    <li><%# Eval("Releasedatum") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten"
                                        CommandName="edit" />
                                </th>
                            </tr>
                            <tr class="sub">
                                <td colspan="4">
                                    <li><%# Eval("Beschreibung") %></li>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                        <EditItemTemplate>
                            <tr>
                            </tr>
                        </EditItemTemplate>
                    </asp:ListView>
                </p>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
