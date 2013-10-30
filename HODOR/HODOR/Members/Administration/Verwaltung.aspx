<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true"
    CodeBehind="Verwaltung.aspx.cs" Inherits="HODOR.Members.Administration.Verwaltung" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    <asp:Label ID="Header" runat="server" Text="Verwaltung " />
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%--DataSourcen für die ListViews--%>
    <%-- Suche nach Produkten mit dem Programmnamen als Suchkriterium --%>
    <asp:EntityDataSource ID="ProductDataSource" runat="server"
        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
        EnableFlattening="False" EntitySetName="Releases" AutoGenerateWhereClause="True"
        EnableUpdate="true" EnableDelete="true" EntityTypeFilter="Release">
        <WhereParameters>
            <asp:ControlParameter ControlID="l_ProgrammID" Name="ReleaseVonProgramm" PropertyName="Text" Type="Int32" />
        </WhereParameters>
    </asp:EntityDataSource>

    <%-- Suche nach Benutzern mit der NutzerNr als Suchkriterium --%>
    <asp:EntityDataSource ID="UserDataSourceByNutzerNr" runat="server"
        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
        EnableFlattening="False" EntitySetName="Benutzers" AutoGenerateWhereClause="false"
        EnableUpdate="true" EnableDelete="true" EntityTypeFilter=""
        Where="it.[NutzerNr] LIKE '%' + @NutzerNr + '%'">
        <WhereParameters>
            <asp:ControlParameter ControlID="tb_SearchInput" Name="NutzerNr" PropertyName="Text" Type="String" />
        </WhereParameters>
    </asp:EntityDataSource>

    <%-- Suche nach Benutzern mit dem Namen als Suchkriterium --%>
    <asp:EntityDataSource ID="UserDataSourceByName" runat="server"
        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
        EnableFlattening="False" EntitySetName="Benutzers" AutoGenerateWhereClause="false"
        EnableUpdate="true" EnableDelete="true" EntityTypeFilter=""
        Where="it.[NutzerNr] LIKE '%' + @Name + '%'">
        <WhereParameters>
            <asp:ControlParameter ControlID="tb_SearchInput" Name="Name" PropertyName="Text" Type="String" />
        </WhereParameters>
    </asp:EntityDataSource>

    <%-- Suche nach Benutzern mit dem Namen und der NutzerNr als Suchkriterien --%>
    <asp:EntityDataSource ID="UserDataSourceByNutzerNrAndName" runat="server"
        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
        EnableFlattening="False" EntitySetName="Benutzers" AutoGenerateWhereClause="false"
        EnableUpdate="true" EnableDelete="true" EntityTypeFilter=""
        Where="it.[NutzerNr] LIKE '%' + @NutzerNr + '%' OR it.[Name] LIKE '%' + @Name + '%'">
        <WhereParameters>
            <asp:ControlParameter ControlID="tb_SearchInput" Name="NutzerNr" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="tb_SearchInput" Name="Name" PropertyName="Text" Type="String" />
        </WhereParameters>
    </asp:EntityDataSource>

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
                <asp:Label ID="is_registered" runat="server" Text="Benutzer konnte nicht angelegt werden!" Visible="false" />
                <!-- Baustelle! -->
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableHeaderRow>
            <asp:TableCell>
                <asp:LinkButton ID="lb_Build" runat="server" PostBackUrl="~/Members/Administration/NeuAnlegen.aspx">Neu anlegen</asp:LinkButton><br />
            </asp:TableCell>
        </asp:TableHeaderRow>
    </asp:Table>
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
                <asp:Label ID="l_BuildVon" runat="server" Text="Builds von Sub-Release Nummer " Visible="false" Font-Bold="true" Font-Size="Larger" />
                <asp:Label ID="l_SubReleaseNr" runat="server" Text="" Visible="false" Font-Bold="true" Font-Size="Larger" />
                <asp:Label ID="l_SubReleaseVon" runat="server" Text="Sub-Releases von " Visible="false" Font-Bold="true" Font-Size="Larger" />
                <asp:Label ID="l_vonXY" runat="server" Text=" von " Visible="false" Font-Bold="true" Font-Size="Larger" />
                <asp:Label ID="l_ProgrammName" runat="server" Text="" Visible="false" Font-Bold="true" Font-Size="Large" /><br />
                <asp:Label ID="l_ProgrammID" runat="server" Text="" Visible="false" />
                <asp:Label ID="l_ReleaseNummer" runat="server" Text="" Visible="false" /> <br />

                <%--ListView für die Ergebnisse der UserSuche--%>
                <asp:ListView ID="lv_User" runat="server" DataKeyNames="BenutzerID"
                    OnSelectedIndexChanging="lvwUsers_SelectedIndexChanging">
                    <LayoutTemplate>
                        <table id="Table3" class="ExampleView" runat="server" width="100%">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="0" class="userTable">
                                        <tr id="Tr2" runat="server" style="">
                                            <th id="Th1" class="emptyTH" runat="server"></th>
                                            <th id="Th2" class="KdNrTH" runat="server">KdNr</th>
                                            <th id="Th3" class="NameTH" runat="server">Firmenname</th>
                                            <th id="Th4" class="EMailTH" runat="server">E-Mail-Adresse</th>
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
                                <%# Eval("NutzerNr") %>
                            </td>
                            <td>
                                <%# Eval("Name") %>
                            </td>
                            <td>
                                <%# Eval("Email") %>
                            </td>
                            <td class="action">
                                <asp:LinkButton ID="lb_Lizenzen" runat="server" Text="Lizenzen"
                                    CommandArgument='<%# Eval("NutzerNr") %>' /><br />
                                <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten"
                                    CommandName="Edit" /><br />
                                <asp:LinkButton ID="lb_delete" runat="server" Text="Löschen"
                                    CommandName="Delete" OnClientClick="return confirm('Sind Sie sicher, dass Sie diesen Benutzer löschen wolle?');" />
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
                                <%# Eval("NutzerNr") %>
                            </td>
                            <td>
                                <%# Eval("Name") %>
                            </td>
                            <td>
                                <%# Eval("Email") %>
                            </td>
                            <td class="action">
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Lizenzen"
                                    CommandArgument='<%# Eval("NutzerNr") %>' /><br />
                                <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten"
                                    CommandName="Edit" /><br />
                                <asp:LinkButton ID="lb_delete" runat="server" Text="Löschen"
                                    CommandName="Delete" OnClientClick="return confirm('Sind Sie sicher, dass Sie diesen Benutzer löschen wollen?');" />
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
                                <asp:TextBox ID="EditNutzerNr" runat="server" Text='<%# Bind("NutzerNr") %>' TextMode="SingleLine" MaxLength="255" />
                            </td>
                            <td>
                                <asp:TextBox ID="EditName" runat="server" Text='<%# Bind("Name") %>' TextMode="SingleLine" MaxLength="255" />
                            </td>
                            <td>
                                <asp:TextBox ID="EditEmail" runat="server" Text='<%# Bind("Email") %>' TextMode="SingleLine" MaxLength="255" />
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

                <%-- ListViews für die Ergebnisse der Prduktsuche --%>
                <%-- Release --%>
                <asp:ListView ID="lv_Product" runat="server" DataKeyNames="ReleaseID"
                    OnSelectedIndexChanging="lvwProducts_SelectedIndexChanging">
                    <LayoutTemplate>
                        <table id="Table3" class="ExampleView" runat="server">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="0" class="userTable">
                                        <tr id="Tr2" runat="server" style="">
                                            <th id="Th1" class="emptyTH" runat="server"></th>
                                            <th id="Th2" class="RlsTH" runat="server">Releasenummer</th>
                                            <th id="Th3" class="RlsDatTH" runat="server">Releasedatum</th>
                                            <th id="th4" class="BeschTH" runat="server"></th>
                                            <th id="Th5" class="ActionTH" runat="server">&bull;</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
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
                                <%# Eval("Releasenummer") %>
                            </td>
                            <td>
                                <%# Eval("Releasedatum", "{0:dd.MM.yy}") %>
                            </td>
                            <td></td>
                            <td class="action">
                                <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                    CommandName="Select" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td>
                                <asp:Image ID="pictureControlID" runat="server" AlternateText="ArrowAlternativ"
                                    ImageUrl="~/images/ListView/ArrowAlternativ.png" />
                            </td>
                            <td>
                                <%# Eval("Releasenummer") %>
                            </td>
                            <td>
                                <%# Eval("Releasedatum", "{0:dd.MM.yy}") %>
                            </td>
                            <td></td>
                            <td class="action">
                                <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                    CommandName="Select" />
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
                                <%# Eval("Releasenummer") %>
                            </td>
                            <td>
                                <%# Eval("Releasedatum", "{0:dd.MM.yy}") %>
                            </td>
                            <td>
                                <%# Eval("Beschreibung") %>
                            </td>
                            <td class="action">
                                <asp:LinkButton ID="lb_subRelease" runat="server" Text="Sub-Release"
                                    CommandArgument='<%# Eval("ReleaseVonProgramm") + ";" + Eval("Releasenummer") %>' OnCommand="lb_subRelease_Command" /><br />
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
                                <asp:TextBox ID="EditReleasenummer" runat="server" Text='<%# Bind("Releasenummer") %>' TextMode="SingleLine" MaxLength="255" />
                            </td>
                            <td>
                                <asp:TextBox ID="EditReleasedatum" runat="server" Text='<%# Bind("Releasedatum") %>' TextMode="SingleLine" MaxLength="255" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Beschreibung") %>' TextMode="MultiLine" MaxLength="511" />
                            </td>
                            <td>
                                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Ändern"
                                    CausesValidation="true" Text="Update" />
                                <asp:LinkButton ID="CancelButton" runat="server" CommandName="Abbrechen"
                                    CausesValidation="false" Text="Cancel" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                </asp:ListView>

                <%-- SubRelease --%>
                <asp:ListView ID="lv_subRelease" runat="server" DataKeyNames="ReleaseID"
                    OnSelectedIndexChanging="lv_subRelease_SelectedIndexChanging">
                    <LayoutTemplate>
                        <table id="Table3" class="ExampleView" runat="server">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="0" class="userTable">
                                        <tr id="Tr2" runat="server" style="">
                                            <th id="Th1" class="emptyTH" runat="server"></th>
                                            <th id="Th2" class="RlsTH" runat="server">Releasenummer</th>
                                            <th id="Th3" class="RlsDatTH" runat="server">Releasedatum</th>
                                            <th id="th4" class="BeschTH" runat="server"></th>
                                            <th id="Th5" class="ActionTH" runat="server">&bull;</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
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
                                <%# Eval("Releasenummer") %>
                            </td>
                            <td>
                                <%# Eval("Releasedatum", "{0:dd.MM.yy}") %>
                            </td>
                            <td></td>
                            <td class="action">
                                <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                    CommandName="Select" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td>
                                <asp:Image ID="pictureControlID" runat="server" AlternateText="ArrowAlternativ"
                                    ImageUrl="~/images/ListView/ArrowAlternativ.png" />
                            </td>
                            <td>
                                <%# Eval("Releasenummer") %>
                            </td>
                            <td>
                                <%# Eval("Releasedatum", "{0:dd.MM.yy}") %>
                            </td>
                            <td></td>
                            <td class="action">
                                <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                    CommandName="Select" />
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
                                <%# Eval("Releasenummer") %>
                            </td>
                            <td>
                                <%# Eval("Releasedatum") %>
                            </td>
                            <td>
                                <%# Eval("Beschreibung", "{0:dd.MM.yy}") %>
                            </td>
                            <td class="action">
                                <asp:LinkButton ID="lb_Builds" runat="server" Text="Builds"
                                    OnCommand="lb_Builds_Command" CommandArgument='<%# Eval("ReleaseVonProgramm") + ";" + this.l_ReleaseNummer.Text + ";" + Eval("ReleaseID") %>' /><br />
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
                                <asp:TextBox ID="EditReleasenummer" runat="server" Text='<%# Bind("Releasenummer") %>' TextMode="SingleLine" MaxLength="255" />
                            </td>
                            <td>
                                <asp:TextBox ID="EditReleasedatum" runat="server" Text='<%# Bind("Releasedatum") %>' TextMode="SingleLine" MaxLength="255" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Beschreibung") %>' TextMode="MultiLine" MaxLength="511" />
                            </td>
                            <td>
                                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Ändern"
                                    CausesValidation="true" Text="Update" />
                                <asp:LinkButton ID="CancelButton" runat="server" CommandName="Abbrechen"
                                    CausesValidation="false" Text="Cancel" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                </asp:ListView>

                <%-- Build --%>
                <asp:ListView ID="lv_Build" runat="server" DataKeyNames="ReleaseID"
                    OnSelectedIndexChanging="lv_Build_SelectedIndexChanging">
                    <LayoutTemplate>
                        <table id="Table3" class="ExampleView" runat="server">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="0" class="userTable">
                                        <tr id="Tr2" runat="server" style="">
                                            <th id="Th1" class="emptyTH" runat="server"></th>
                                            <th id="Th2" class="RlsTH" runat="server">Releasenummer</th>
                                            <th id="Th3" class="RlsDatTH" runat="server">Releasedatum</th>
                                            <th id="th4" class="BeschTH" runat="server"></th>
                                            <th id="Th5" class="ActionTH" runat="server">&bull;</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
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
                                <%# Eval("Releasenummer") %>
                            </td>
                            <td>
                                <%# Eval("Releasedatum", "{0:dd.MM.yy}") %>
                            </td>
                            <td></td>
                            <td class="action">
                                <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                    CommandName="Select" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td>
                                <asp:Image ID="pictureControlID" runat="server" AlternateText="ArrowAlternativ"
                                    ImageUrl="~/images/ListView/ArrowAlternativ.png" />
                            </td>
                            <td>
                                <%# Eval("Releasenummer") %>
                            </td>
                            <td>
                                <%# Eval("Releasedatum", "{0:dd.MM.yy}") %>
                            </td>
                            <td></td>
                            <td class="action">
                                <asp:LinkButton ID="lb_Details1" runat="server" Text="Details"
                                    CommandName="Select" />
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
                                <%# Eval("Releasenummer") %>
                            </td>
                            <td>
                                <%# Eval("Releasedatum") %>
                            </td>
                            <td>
                                <%# Eval("Beschreibung", "{0:dd.MM.yy}") %>
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
                                <asp:TextBox ID="EditReleasenummer" runat="server" Text='<%# Bind("Releasenummer") %>' TextMode="SingleLine" MaxLength="255" />
                            </td>
                            <td>
                                <asp:TextBox ID="EditReleasedatum" runat="server" Text='<%# Bind("Releasedatum") %>' TextMode="SingleLine" MaxLength="255" />
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Beschreibung") %>' TextMode="MultiLine" MaxLength="511" />
                            </td>
                            <td>
                                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Ändern"
                                    CausesValidation="true" Text="Update" />
                                <asp:LinkButton ID="CancelButton" runat="server" CommandName="Abbrechen"
                                    CausesValidation="false" Text="Cancel" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                </asp:ListView>

        </asp:View>
    </asp:MultiView>
</asp:Content>
