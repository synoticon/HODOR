<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPage.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Startseite.aspx.cs" Inherits="HODOR.Startseite" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="loginContent">
        <asp:MultiView ID="StartseiteMultiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="LoginView" runat="server">
                <div id="login">
                           <table cellpadding="4" cellspacing="0" style="border-collapse: collapse;">
                                <tr>
                                    <td>
                                        <table cellpadding="0">
                                            <tr>
                                                <td align="center" colspan="2" style="color: White; background-color: #507CD1; font-size: 0.9em;
                                                    font-weight: bold;">
                                                    Anmelden
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Benutzername:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                        ErrorMessage="Der Benutzername ist erforderlich." ToolTip="Der Benutzername ist erforderlich."
                                                        ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Kennwort:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                        ErrorMessage="Das Kennwort ist erforderlich." ToolTip="Das Kennwort ist erforderlich."
                                                        ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color: Red;">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False" Text=""></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="LoginButton" runat="server" BackColor="White" BorderColor="#507CD1"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                        Font-Size="0.8em" ForeColor="#284E98" Text="Anmelden" ValidationGroup="Login1" OnClick="OnClick_LoginButton"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                </div>
                <div class="newOld">
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="PwReset" OnCommand="MenuLink_Command">Reset</asp:LinkButton>
                </div>
            </asp:View>
            <asp:View ID="PwResetView" runat="server">
                <div id="reset">
                    <p>
                        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE"
                            BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                            Font-Size="0.8em">
                            <SubmitButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
                            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                            <SuccessTextStyle Font-Bold="True" ForeColor="#507CD1" />
                            <TextBoxStyle Font-Size="0.8em" />
                            <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
                            <UserNameTemplate>
                                <table cellpadding="4" cellspacing="0" style="border-collapse: collapse;">
                                    <tr>
                                        <td>
                                            <table cellpadding="0">
                                                <tr>
                                                    <td align="center" colspan="2" style="color: White; background-color: #507CD1; font-size: 0.9em;
                                                        font-weight: bold;">
                                                        Kennwort vergessen?
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="color: Black; font-style: italic;">
                                                        Geben Sie zum Erhalt des Kennworts Ihre E-Mail-Adresse ein.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">E-Mail-Adresse:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                            ErrorMessage="Die E-Mail-Adresse ist erforderlich." ToolTip="Die E-Mail-Adresse ist erforderlich."
                                                            ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="color: Red;">
                                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False" Text="E-Mail-Adresse fehlerhaft!"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2">
                                                        <asp:Button ID="SubmitButton" runat="server" BackColor="White" BorderColor="#507CD1"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                            Font-Size="0.8em" ForeColor="#284E98" Text="Senden" ValidationGroup="PasswordRecovery1" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </UserNameTemplate>
                        </asp:PasswordRecovery>
                    </p>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
