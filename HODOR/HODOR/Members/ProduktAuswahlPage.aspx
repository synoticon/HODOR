<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPageIntern.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="ProduktAuswahlPage.aspx.cs"
    Inherits="HODOR.ProduktAuswahlPage" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManagerProxy runat="server" />
    <div id="test">
        <div id="produktinfo">
            <form method="post" action="Produkte.aspx">
            <br />
            <asp:Label ID="lb_programmname_text" runat="server" Text="Programm:" />
            <br />
            <asp:DropDownList ID="DDL_Programm" runat="server" AppendDataBoundItems="true" DataTextField="Company_Name"
                DataValueField="id">
                <asp:ListItem Text="---Select---" Value="0" />
            </asp:DropDownList>
            <asp:Button ID="button1" Text="Click me!" runat="server" OnClick="submit" />
            <br />
            <br />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    Loading ...
                </ProgressTemplate>
            </asp:UpdateProgress>
            <br />
            </form>
            <asp:UpdatePanel runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="button1" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ModView" runat="server">
                            Dies ist die ModView
                            <asp:Repeater ID="TestRepeater" runat="server" DataSourceID="SqlDataSource1">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <%# Eval("Title") %></li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                          <!--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnSelected="DS_OnSelecting"
                                ConnectionString=""></asp:SqlDataSource>-->
                        </asp:View>
                        <br />
                        <br />
                        <asp:View ID="TestView" runat="server">
                            Dies ist die TestView
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
