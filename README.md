DxHyperLinkButtonText
=====================

Displays a hyperlink within localized text. The link may optionally post back like a button. 

Derived from DevExpress ASPxHyperlink. Several methods/properties of asp:LinkButton are added to provide the post back behavior.

Requirement: Licensed version of DevExpress product that includes ASPxHyperlink.

This project references DevExpress.Web.v12.2 version 12.2.15.0, but the version can be changed to a newer or older version.

The server control source code is at DxHyperLinkButtonText/DxHyperLinkButtonText/HyperLinkButtonText.cs and is really all you need to add to your application. The rest is just scaffolding and a test web page. The source code is short and should be self-explanatory. The default target framework is ASP.NET 4.5, but can be changed to a newer or older version.

Examples,

    <dxx:HyperLinkButtonText ID="Link" runat="server"
            Text="Derived from <a>DevExpress ASPxHyperLink Class</a>"
            NavigateUrl="http://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxEditorsASPxHyperLinktopic" />


    <dxx:HyperLinkButtonText ID="Button" runat="server"
            Text="Invoke the server-side <a>Click</a> event"
            OnClick="Button_Click" />
