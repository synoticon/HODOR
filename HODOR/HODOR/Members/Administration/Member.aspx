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
                            <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" BackColor="#EFF3FB" 
                                BorderColor="#B5C7DE" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Names="Verdana" Font-Size="0.8em">
                                <ContinueButtonStyle BackColor="White" BorderColor="#507CD1" 
                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                                    ForeColor="#284E98" />
                                <CreateUserButtonStyle BackColor="White" BorderColor="#507CD1" 
                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                                    ForeColor="#284E98" />
                                <TitleTextStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <WizardSteps>
                                    <asp:CreateUserWizardStep runat="server" />
                                    <asp:CompleteWizardStep runat="server" />
                                </WizardSteps>
                                <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" 
                                    BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" 
                                    HorizontalAlign="Center" />
                                <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" 
                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                                    ForeColor="#284E98" />
                                <SideBarButtonStyle BackColor="#507CD1" Font-Names="Verdana" 
                                    ForeColor="White" />
                                <SideBarStyle BackColor="#507CD1" Font-Size="0.9em" VerticalAlign="Top" />
                                <StepStyle Font-Size="0.8em" />
                            </asp:CreateUserWizard>
                        </p>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</form>
</asp:Content>
