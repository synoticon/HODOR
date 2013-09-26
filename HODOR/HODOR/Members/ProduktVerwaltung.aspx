<%@ Page Title="" Language="C#" MasterPageFile="~/SubPageIntern.Master" AutoEventWireup="true"
    CodeBehind="ProduktVerwaltung.aspx.cs" Inherits="HODOR.Members.ProduktVerwaltung" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <html>
    <head id="Head1" runat="server">
    </head>
    <body>
        <form id="form1" runat="server">
        <div>
            Search by product or by category?
            <br />
            <asp:RadioButton ID="radioProduct" runat="server" AutoPostBack="true" GroupName="SearchType"
                Text="Product" OnCheckedChanged="radioButton_CheckedChanged" />
            &nbsp;
            <asp:RadioButton ID="radioCategory" runat="server" AutoPostBack="true" GroupName="SearchType"
                Text="Category" OnCheckedChanged="radioButton_CheckedChanged" />
            <br />
            <br />
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="viewProductSearch" runat="server">
                    <div>
                        <asp:FileUpload ID="FileUpload1" runat="server" /><br />
                            <asp:RegularExpressionValidator ID="regexpName" runat="server"     
                                    ErrorMessage="This expression does not validate." 
                                    ControlToValidate="FileUpload1"     
                                    ValidationExpression=".*[a-zA-Z0-9]{1,254}_\d{1,10}_\d{1,10}_\d{1,10}..*" />
                        <br />
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Upload File" />&nbsp;<br />
                        <br />
                        <asp:Label ID="Label1" runat="server"></asp:Label></div>
                        <textarea id="TextArea1" cols="20" rows="2" runat="server"></textarea>
                </asp:View>
                <asp:View ID="viewCategorySearch" runat="server">
                    Enter category:
                    <asp:TextBox ID="textCategory" runat="server">
                    </asp:TextBox>
                </asp:View>
            </asp:MultiView>&nbsp;<br />
            <br />
            <asp:Button ID="btnSearch" OnClick="Button1_Click" runat="server" Text="Search" />
        </div>
        </form>
    </body>
    </html>
</asp:Content>