<%@ Page Title="HODOR - Hyper Organised Deployment Order Revisioning" MasterPageFile="~/SubPageIntern.Master"
    Language="C#" AutoEventWireup="true" CodeBehind="Produktverwaltung.aspx.cs" Inherits="HODOR.Members.Administration.Produktverwaltung
    " %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="ajaxTest">
        <asp:LinkButton ID="Upload" runat="server" OnCommand="MenuLink_Command" CommandName="Upload">Programm Anlegen</asp:LinkButton>
          &nbsp; |&nbsp;
         <asp:LinkButton ID="Edit" runat="server" OnCommand="MenuLink_Command" CommandName="Edit">Programm Anlegen</asp:LinkButton>
        <br />
        <br />
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="UploadView" runat="server">
                           <div>

            <asp:FileUpload ID="FileUpload1" runat="server" /><br />
            <asp:RegularExpressionValidator ID="regexpName" runat="server"
                ErrorMessage="This expression does not validate."
                ControlToValidate="FileUpload1"
                ValidationExpression=".*[a-zA-Z0-9]{1,254}_\d{1,10}_\d{1,10}_\d{1,10}..*" />

            <br />
            <br />
            <asp:Button ID="b_upload" runat="server" OnClick="OnClick_b_upload" Text="Upload File" />&nbsp;<br />

            <asp:Label ID="l_message" runat="server"></asp:Label>
        </div>
                    </asp:View>
                      <asp:View ID="EditView" runat="server">

            

            <br />
            <asp:Table ID="Table1" runat="server" BorderStyle="Dashed" BorderColor="Black" BorderWidth="1" CellPadding="10" GridLines="Both">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>
                          </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <asp:DropDownList ID="DDL_Programm" runat="server" OnSelectedIndexChanged="SelectedChangeProgramm" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <p>Release</p>
                        <asp:DropDownList ID="DDL_Release" runat="server" OnSelectedIndexChanged="SelectedChangeRelease" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <p>Sub-Release</p>
                        <asp:DropDownList ID="DDL_SubRelease" runat="server" OnSelectedIndexChanged="SelectedChangeSubRelease" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                    <asp:TableHeaderCell>
                        <p>Build</p>
                        <asp:DropDownList ID="DDL_Build" runat="server" OnSelectedIndexChanged="SelectedChangeBuild" AutoPostBack="true">
                            <asp:ListItem Text="---Select---" Value="null" />
                        </asp:DropDownList>
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Beschreibung:
                    </asp:TableCell>
                     <asp:TableCell>
                       <asp:TextBox id="ta_Programmdiscription" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                       <asp:TextBox id="ta_Releasediscription" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                    </asp:TableCell>
                      <asp:TableCell>
                        <asp:TextBox  ID="ta_SubReleasediscription" TextMode="multiline" Columns="50" Rows="5" runat="server"  />
                    </asp:TableCell>
                      <asp:TableCell>
                        <asp:TextBox  ID="ta_Builddiscription" TextMode="multiline" Columns="50" Rows="5" runat="server"  />
      
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
                          <asp:Button ID="b_save" OnClick="OnClick_b_save" Text="Save" runat="server" />
                           </asp:View>
                </asp:MultiView>

            </contenttemplate>


    </div>
</asp:Content>
