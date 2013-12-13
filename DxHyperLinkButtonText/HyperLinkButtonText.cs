using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;

namespace DXX
{
    [DefaultProperty("Text")]
    [ToolboxData(@"<{0}:HyperLinkText runat=""server"" Text=""Enter text with a <a>hyperlink</a>."" EncodeHtml=""false"" />")]
    [ToolboxBitmap(typeof(ASPxHyperLink))]
    [SupportsEventValidation]
    public class HyperLinkButtonText : ASPxHyperLink
    {
        #region asp:LinkButton members
        public event EventHandler Click;

        [Themeable(false)]
        public virtual bool CausesValidation { get; set; }

        [Themeable(false)]
        [Bindable(true)]
        public string CommandArgument { get; set; }

        [Themeable(false)]
        public virtual string ValidationGroup { get; set; }

        protected virtual PostBackOptions GetPostBackOptions()
        {
            return new PostBackOptions(this, CommandArgument, null, false, true, false, true, CausesValidation, ValidationGroup);
        }

        protected virtual void OnClick(EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        [Themeable(false)]
        public virtual string OnClientClick { get; set; }
        #endregion

        public HyperLinkButtonText()
        {
            Visible = true;
        }

        [Bindable(true)]
        public virtual string SpriteCssClass { get; set; }

        [Localizable(true)]
        [Bindable(true)]
        public virtual string NullDisplayText { get; set; }

        [Localizable(true)]
        [Bindable(true)]
        public virtual string NullUrlDisplayText { get; set; }

        [Themeable(false)]
        public string PopupControlID { get; set; }

        protected override void RaisePostBackEvent(string eventArgument)
        {
            OnClick(new EventArgs());
            base.RaisePostBackEvent(eventArgument);
        }

        // There was a problem with Visible == false in RenderControl during a callback when Visible was not overridden.
        public override bool Visible { get; set; }

        public override void RenderControl(HtmlTextWriter writer)
        {
            if (!Visible) return;

            if (Click != null ||
                CausesValidation ||
                !String.IsNullOrWhiteSpace(CommandArgument) ||
                !String.IsNullOrWhiteSpace(ValidationGroup))
            {
                PostBackOptions postBackOptions = GetPostBackOptions();
                Page.ClientScript.RegisterForEventValidation(postBackOptions);
                string url = Page.ClientScript.GetPostBackEventReference(postBackOptions);
                if (!String.IsNullOrWhiteSpace(NavigateUrl) && NavigateUrl != url)
                {
                    throw new Exception("NavigateUrl cannot be set when one or more post back properties (i.e., Click event, CausesValidation, CommandArgument, PostBackUrl, or ValidationGroup) are set.");
                }

                NavigateUrl = url;
            }

            if (!String.IsNullOrWhiteSpace(OnClientClick))
            {
                string js = "function(s,e) { " + OnClientClick + " }";
                if (!String.IsNullOrWhiteSpace(this.ClientSideEvents.Click) && this.ClientSideEvents.Click != js)
                {
                    throw new ArgumentException("Use either OnClientClick or ClientSideEvents.Click, not both.");
                }

                this.ClientSideEvents.Click = js;
            }

            bool isPopupElement = !String.IsNullOrEmpty(PopupControlID);
            if (!isPopupElement && String.IsNullOrEmpty(NavigateUrl) && !String.IsNullOrEmpty(NullUrlDisplayText))
            {
                if (!String.IsNullOrWhiteSpace(SpriteCssClass))
                {
                    writer.WriteLine("<span class=\"SpriteLink\">");
                    writer.Write("<span class=\"" + SpriteCssClass + "\"></span>");
                }

                WriteText(writer, NullUrlDisplayText);

                if (!String.IsNullOrWhiteSpace(SpriteCssClass))
                {
                    writer.WriteLine("</span>");
                }
            }
            else
            {
                string beforeLinkText = null;
                string hyperLinkText = null;
                string afterLinkText = null;

                string text = this.Text;

                if (String.IsNullOrEmpty(text) && !String.IsNullOrEmpty(NullDisplayText))
                {
                    hyperLinkText = NullDisplayText;
                    isPopupElement = false;
                }
                else if (!String.IsNullOrEmpty(text))
                {
                    Match match = Regex.Match(text, @"([\w\W]*)<a[^>]*>([\w\W]+)</a>([\w\W]*)");

                    beforeLinkText = match.Groups[1].Value;
                    hyperLinkText = match.Groups[2].Value;
                    afterLinkText = match.Groups[3].Value;
                }

                if (!String.IsNullOrEmpty(beforeLinkText))
                {
                    if (EncodeHtml)
                    {
                        writer.WriteEncodedText(beforeLinkText);
                    }
                    else
                    {
                        writer.Write(beforeLinkText);
                    }
                }

                if (!String.IsNullOrEmpty(hyperLinkText))
                {
                    base.Text = hyperLinkText;
                }

                if (!String.IsNullOrWhiteSpace(SpriteCssClass))
                {
                    writer.WriteLine("<span class=\"SpriteLink\">");
                    writer.Write("<span class=\"" + SpriteCssClass + "\"></span>");
                }

                if (isPopupElement)
                {
                    Attributes.CssStyle.Add("cursor", "pointer");
                    base.RenderControl(writer);
                }
                else if (!String.IsNullOrEmpty(NavigateUrl))
                {
                    base.RenderControl(writer);
                }
                else
                {
                    WriteText(writer, base.Text);
                }

                if (!String.IsNullOrWhiteSpace(SpriteCssClass))
                {
                    writer.WriteLine("</span>");
                }

                if (!String.IsNullOrEmpty(afterLinkText))
                {
                    if (EncodeHtml)
                    {
                        writer.WriteEncodedText(afterLinkText);
                    }
                    else
                    {
                        writer.Write(afterLinkText);
                    }
                }
            }
        }

        private void WriteText(HtmlTextWriter writer, string text)
        {
            if (!String.IsNullOrWhiteSpace(CssClass))
            {
                writer.Write("<span class=\"{0}\">", CssClass);
            }

            if (EncodeHtml)
            {
                writer.WriteEncodedText(text);
            }
            else
            {
                writer.Write(text);
            }

            if (!String.IsNullOrWhiteSpace(CssClass))
            {
                writer.Write("</span>");
            }
        }
    }
}
