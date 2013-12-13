<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DxHyperLinkButtonText.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DxHyperLinkButtonText</title>
    <style>
        body
        {
            font: 12px Tahoma,Geneva,sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxx:HyperLinkButtonText ID="Link" runat="server"
            Text="Derived from <a>DevExpress ASPxHyperLink Class</a>"
            NavigateUrl="http://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxEditorsASPxHyperLinktopic" />
        
        <br />
        <br />

        <dxx:HyperLinkButtonText ID="Button" runat="server"
            Text="Invoke the server-side <a>Click</a> event"
            OnClick="Button_Click" />

    </div>
    </form>
</body>
</html>
