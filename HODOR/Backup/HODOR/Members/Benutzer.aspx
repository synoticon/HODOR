<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPage.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Benutzer.aspx.cs" Inherits="HODOR.Benutzer" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="ajaxTest">
        <asp:LinkButton ID="About" runat="server" OnCommand="MenuLink_Command" CommandName="About">Über uns</asp:LinkButton>
        &nbsp; |&nbsp;
        <asp:LinkButton ID="Events" runat="server" OnCommand="MenuLink_Command" CommandName="Events">Veranstaltungen</asp:LinkButton>
        <br />
        <br />
        <asp:UpdatePanel runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="About" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="Events" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="AboutView" runat="server">
                        <p>
                            HODOR ist ein hoch modernes Versionierungstool, welches vielseitig verwendet werden
                            kann.</p>
                    </asp:View>
                    <br />
                    <asp:View ID="EventsView" runat="server">
                        <p>
                            bla blub</p>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
