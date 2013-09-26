<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPageIntern.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="HODOR.Members.Administration.Member" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form2" runat="server">
    <div id="ajaxTest">
        <asp:LinkButton ID="Sight" runat="server" OnCommand="MenuLink_Command" CommandName="Sight">Ansicht</asp:LinkButton>
        &nbsp; |&nbsp;
        <asp:LinkButton ID="Build" runat="server" OnCommand="MenuLink_Command" CommandName="Build">Anlegen</asp:LinkButton>
        <br />
        <br />
        <asp:UpdatePanel runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Sight" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="Build" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="SightView" runat="server">
                        <p>
                            &nbsp;<asp:Repeater runat="server" DataSourceID="BenutzerDataSource">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li><%# Eval("Nutzer_Nr") %></li>
                                    <li><%# Eval("Name") %></li>
                                    <li><%# Eval("Email") %></li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="BenutzerDataSource" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:ia211ConnectionString %>" 
                                SelectCommand="SelectUser">
                            </asp:SqlDataSource>
                        </p>
                    </asp:View>
                    <br />
                    <asp:View ID="BuildView" runat="server">
                        <p>
                            <asp:Label ID="l_FirmenName" runat="server" Text="Name der Firma:" />
                            <br />
                            <asp:TextBox ID="tb_FirmenName" runat="server" MaxLength="255" TextMode="SingleLine" />
                            <br />
                            <asp:Label ID="l_KundenNummer" runat="server" Text="Kundennummer:" />
                            <br />
                            <asp:TextBox ID="tb_KundenNummer" runat="server" MaxLength="255" TextMode="SingleLine" />
                            <br />
                            <asp:Label ID="l_EMail" runat="server" Text="E-Mail:" />
                            <br />
                            <asp:TextBox ID="tb_EMail" runat="server" MaxLength="255" TextMode="SingleLine" />
                            <br />
                            <asp:Button ID="b_Register" runat="server" Text="Anlegen" OnClick="b_Register_Click" />
                        </p>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</form>
</asp:Content>
