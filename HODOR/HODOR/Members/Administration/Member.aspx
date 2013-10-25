<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPageIntern.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="HODOR.Members.Administration.Member" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                            <asp:Label ID="is_registered" runat="server" Text="Benutzer konnte nicht angelegt werden!" />
                        </p>
                        <p>
                            &nbsp;<asp:Repeater runat="server" DataSourceID="BenutzerDataSource">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li><%# Eval("NutzerNr") %></li>
                                    <li><%# Eval("Name") %></li>
                                    <li><%# Eval("Email") %></li>
                                    <li><%# Eval("RolleID") %></li>
                                    <p>
                                        <asp:LinkButton ID="lb_DeleteUser" runat="server" OnCommand="lb_EditDeleteUser_Click" CommandArgument='<%# Eval("NutzerNr") %>' CommandName="delete" Text="L&oumlschen" />
                                    </p>
                                    <p>
                                        <asp:LinkButton ID="lb_EditUser" runat="server" OnCommand="lb_EditDeleteUser_Click" CommandArgument='<%# Eval("NutzerNr") %>' CommandName="edit" Text="Bearbeiten" />
                                    </p>
                                    <br></br>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                            <asp:EntityDataSource ID="BenutzerDataSource" runat="server"
                                ConnectionString="name=HODOR_entities" DefaultContainerName="HODOR_entities"
                                EnableFlattening="False" EntitySetName="Benutzers"
                                Select="it.[NutzerNr], it.[Email], it.[Name], it.[RolleID]">
                            </asp:EntityDataSource>
                        </p>
                    </asp:View>
                    <br />
                    <asp:View ID="BuildView" runat="server">
                        <p>
                            <asp:Label ID="l_KdNr" runat="server" Text="Kundennummer:"></asp:Label>
                        </p>
                        <p>
                            <asp:TextBox ID="tb_KdNr" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="l_FirmenName" runat="server" Text="Firmenname:"></asp:Label>
                        </p>
                        <p>
                            <asp:TextBox ID="tb_Firmenname" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="l_EMail" runat="server" Text="E-Mail:"></asp:Label>
                        </p>
                        <p>
                            <asp:TextBox ID="tb_EMail" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="l_role" runat="server" Text="Berechtigungsrolle:"></asp:Label>
                        </p>
                        <p>
                            <asp:DropDownList ID="ddl_roles" runat="server">
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:Button ID="b_register" runat="server" Text="Registrieren"
                                OnClick="b_Register_Click" />
                        </p>
                    </asp:View>

                  
                </asp:MultiView>
               
            </ContentTemplate>
        </asp:UpdatePanel>
          
    </div>
</asp:Content>
