<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true"
    CodeBehind="Verwaltung.aspx.cs" Inherits="HODOR.Members.Administration.ProduktVerwaltung" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <p>
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="l_SearchInput" runat="server" Text="Suchbegriff eingeben:    " /><br />
                        <asp:TextBox ID="tb_SearchInput" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:RadioButton ID="rb_UserSearch" runat="server" Text="Benutzer" GroupName="RadioButtonSearch" />
                    </asp:TableCell><asp:TableCell>
                        <asp:RadioButton ID="rb_ProductSearch" runat="server" Text="Produkt" GroupName="RadioButtonSearch" />
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell>
                        <asp:RegularExpressionValidator ID="SearchInputValidator" runat="server"
                            ControlToValidate="tb_SearchInput" ErrorMessage="Bitte geben Sie einen Suchbegriff ein."
                            ValidationExpression="[a-zA-Z0-9]{1,254}"></asp:RegularExpressionValidator><br />
                    </asp:TableCell><asp:TableCell>
                        <asp:Button ID="b_Search" runat="server" OnClick="SearchButton_Click" Text="Suchen" />
                    </asp:TableCell></asp:TableRow></asp:Table></p><br />
        <asp:Label ID="l_noCatch" runat="server" Text="Kein Treffer." Visible="false" /><br />
          <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="UserView" runat="server">
                <p>
                    <asp:EntityDataSource ID="UserDataSource" runat="server" 
                        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
                        EnableFlattening="false" EntitySetName="Benutzers"
                        Select="it.[NutzerNr], it.[Email], it.[Name], it.[RolleID]">
                    </asp:EntityDataSource>

                    <asp:ListView ID="lv_User" runat="server" DataSourceID="UserDataSource" 
                        OnSelectedIndexChanging="lvwUsers_SelectedIndexChanging">
                        <LayoutTemplate>
                            <Table width="100%" border="0" cellspacing="0" cellpadding="0" class="ListView">
                                <asp:PlaceHolder id="itemPlaceHolder" runat="server" />
                            </Table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <th class="NzNr_incon1">
                                    <li><%# Eval("NutzerNr") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Details" 
                                        CommandName="Select" CommandArgument='<%# Eval("NutzerNr") %>'
                                        OnCommand="l_Rolle_Load"/>
                                </th>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <th class="NzNr_incon2">
                                    <li><%# Eval("NutzerNr") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Details" 
                                        CommandName="Select" CommandArgument='<%# Eval("NutzerNr") %>'
                                        OnCommand="l_Rolle_Load"/>
                                </th>
                            </tr>
                        </AlternatingItemTemplate>
                        <SelectedItemTemplate>
                            <tr>
                                <th class="NzNr_incon3">
                                    <li><%# Eval("NutzerNr") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten" 
                                        CommandName="Edit"/>
                                </th>
                            </tr>
                            <tr class="sub">
                                <td colspan="4">
                                    <li><%# Eval("Name") %></li>
                                </td>
                                <td colspan="4">
                                    <li><%# Eval("Email") %></li>
                                </td>
                                <td colspan="4">
                                    <li><asp:Label ID="l_Rolle" runat="server" Text="" /></li>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>

                </p>
            </asp:View>
            <asp:View ID="ProductView" runat="server">
                <p>
                    <asp:ListBox ID="lb_Product" runat="server" 
                        OnSelectedIndexChanged="lb_SelectedIndexChanged_Main" AutoPostBack="true" /><br />
                    <asp:Label ID="l_ProgrammName" runat="server" Text="" /><br />
                </p>
                <p>
                    <asp:EntityDataSource ID="ProductDataSource" runat="server" 
                        ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
                        EnableFlattening="false" EntitySetName="Releases"
                        Select="it.[Releasenummer], it.[Releasedatum], it.[Beschreibung]">
                    </asp:EntityDataSource>
                    <asp:ListView ID="lv_Product" runat="server" DataSourceID="ProductDataSource" 
                        OnSelectedIndexChanging="lvwProducts_SelectedIndexChanging"><LayoutTemplate>
                            <Table width="100%" border="0" cellspacing="0" cellpadding="0" class="ListView">
                                <asp:PlaceHolder id="itemPlaceHolder" runat="server" />
                            </Table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <th class="RlNr_incon1">
                                    <li><%# Eval("Releasenummer") %></li>
                                </th>
                                <th>
                                    <li><%# Eval("Releasedatum", "[0:dd.MM.yy]") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Details" 
                                        CommandName="Select"/>
                                </th>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <th class="RlNr_incon2">
                                    <li><%# Eval("Releasenummer") %></li>
                                </th>
                                <th>
                                    <li><%# Eval("ReleaseDatum", "[0:dd.MM.yy]") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Details" 
                                        CommandName="Select"/>
                                </th>
                            </tr>
                        </AlternatingItemTemplate>
                        <SelectedItemTemplate>
                            <tr>
                                <th class="RlNr_incon3">
                                    <li><%# Eval("Releasenummer") %></li>
                                </th>
                                <th>
                                    <li><%# Eval("ReleaseDatum", "[0:dd.MM.yy]") %></li>
                                </th>
                                <th class="action">
                                    <asp:LinkButton ID="lb_Details1" runat="server" Text="Bearbeiten" 
                                        CommandName="edit"/>
                                </th>
                            </tr>
                            <tr class="sub">
                                <td colspan="4">
                                    <li><%# Eval("Beschreibung") %></li>
                                </td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </p>
                </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
