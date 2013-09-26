<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SampleData.aspx.cs" Inherits="HODOR.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="button" runat="server" OnClick="buttonClick" Text="CreateDemoData" />
        <asp:Label ID="label" runat="server" />
        <br />
        <asp:Button ID="button2" runat="server" OnClick="buttonClick2" Text="CreateMuchDemoData(approx 25min.)" />
        <br />
        <asp:Button ID="button3" runat="server" OnClick="buttonClick3" Text="TestSomething" />
    </div>
    </form>
</body>
</html>
