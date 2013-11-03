<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SampleData.aspx.cs" Inherits="HODOR.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" onsubmit="createDemoDataWithInput" defaultbutton="bt_expandedDemoData">
    <div>
        <asp:Label ID="label" runat="server" />
        <br />
        <br />
        <br />
            <table border="1">
            <tr>
                <td><asp:Label ID="label1" runat="server" Text="ProgramName:" /></td>
                <td><asp:TextBox ID="tb_ProgramName" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="label2" runat="server" Text="NumberOfReleases:" /></td>
                <td><asp:TextBox ID="tb_NumberOfReleases" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="label3" runat="server" Text="NumberOfSubreleasesPerRelease:" /></td>
                <td><asp:TextBox ID="tb_NumberOfSubreleasesPerRelease" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="label4" runat="server" Text="NumberOfBuildsPerSubrelease:" /></td>
                <td><asp:TextBox ID="tb_NumberOfBuildsPerSubrelease" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="label5" runat="server" Text="NameOfUserWithLicenseForProgram:<br/>(Will be created if neccessary)" /></td>
                <td><asp:TextBox ID="tb_NameOfUserWithLicenseForProgram" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Button ID="bt_expandedDemoData" runat="server" OnClick="createDemoDataWithInput" Text="Create Demo Data with above Input" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
