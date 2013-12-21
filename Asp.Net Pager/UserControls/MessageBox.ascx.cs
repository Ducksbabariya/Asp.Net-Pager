using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechTracking.Classes;

namespace TechTracking.UserControls
{
    public enum MessageType
    {
        Success,
        Info,
        Warning,
        Error
    }

    public class NotificationMessage
    {
        public string Text { get; set; }
        public MessageType Type { get; set; }
        public bool AutoHide { get; set; }
    }
    public partial class MessageBox : System.Web.UI.UserControl
    {
        private const string KEY_NOTIFICATION_MESSAGES = "NotificationMessages";

        public double Width
        {
            get
            {
                return ViewState["Width"] != null ? Convert.ToDouble(ViewState["Width"]) : 98;
            }
            set
            {
                ViewState["Width"] = value;
            }
        }

        public static void AddMessage(NotificationMessage msg)
        {
            List<NotificationMessage> messages = NotificationMessages;
            if (messages == null)
            {
                messages = new List<NotificationMessage>();
            }
            messages.Add(msg);
            HttpContext.Current.Session[KEY_NOTIFICATION_MESSAGES] = messages;
        }

        private static void ClearMessages()
        {
            HttpContext.Current.Session[KEY_NOTIFICATION_MESSAGES] = null;
        }

        private static List<NotificationMessage> NotificationMessages
        {
            get
            {
                List<NotificationMessage> messages = (List<NotificationMessage>)
                    HttpContext.Current.Session[KEY_NOTIFICATION_MESSAGES];
                return messages;
            }
        }

        public static void AddInfoMessage(string msg)
        {
            AddMessage(new NotificationMessage()
            {
                Text = msg,
                Type = MessageType.Info,
                AutoHide = true
            });
        }

        public static void AddSuccessMessage(string msg)
        {
            AddMessage(new NotificationMessage()
            {
                Text = msg,
                Type = MessageType.Success,
                AutoHide = true
            });
        }

        public static void AddWarningMessage(string msg)
        {
            AddMessage(new NotificationMessage()
            {
                Text = msg,
                Type = MessageType.Warning,
                AutoHide = false
            });
        }

        public static void AddErrorMessage(string msg)
        {
            AddMessage(new NotificationMessage()
            {
                Text = msg,
                Type = MessageType.Error,
                AutoHide = false
            });
        }

        protected void Page_Prerender(object sender, EventArgs e)
        {
            if (NotificationMessages != null)
            {
                int index = 1;
                foreach (var msg in NotificationMessages)
                {
                    Panel msgPanel = new Panel();
                    //msgPanel.Width = Unit.Percentage(Width);
                    //msgPanel.CssClass = "PanelNotificationBox Panel" + msg.Type;
                    if (msg.Type == MessageType.Success)
                        msgPanel.CssClass = "notification done";
                    else if (msg.Type == MessageType.Error)
                        msgPanel.CssClass = "notification undone";
                    else if (msg.Type == MessageType.Info)
                        msgPanel.CssClass = "notification information";
                    else if (msg.Type == MessageType.Warning)
                        msgPanel.CssClass = "notification warning";
                    if (msg.AutoHide)
                    {
                        msgPanel.CssClass += " AutoHide";
                    }
                    msgPanel.ID = msg.Type + "Msg" + index;
                    Literal msgLiteral = new Literal();
                    msgLiteral.Mode = LiteralMode.Transform;
                    msgLiteral.Text = msg.Text;
                    msgPanel.Controls.Add(msgLiteral);
                    this.Controls.Add(msgPanel);
                    index++;
                }
                ClearMessages();

                IncludeTheCssAndJavaScript();
            }
        }

        private void IncludeTheCssAndJavaScript()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClientScript", "$(\"html, body\").animate({ scrollTop: ($('.notification').offset().top-15) }, 600);",true);            
             
            //ClientScriptManager cs = Page.ClientScript;
            //Uri uri = HttpContext.Current.Request.Url;
            //string SiteURL = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port + "/";
            // Include the jQuery library (if not already included)
            //string jqueryURL = this.TemplateSourceDirectory +
            //    "~/Scripts/jquery-1.6.4.min.js";
            //string jqueryURL = ProjectConfiguration.SiteUrl + "Scripts/jquery-1.6.4.min.js";
            //if (!cs.IsStartupScriptRegistered(jqueryURL))
            //{
            //    //cs.RegisterClientScriptInclude(jqueryURL, jqueryURL);
            //    ScriptManager.RegisterClientScriptInclude(this, this.GetType(), jqueryURL, jqueryURL);
            //}
            //string notifierScriptURL = ProjectConfiguration.SiteUrl + "Scripts/MessageBox/Notify.js";
            //if (!cs.IsStartupScriptRegistered(notifierScriptURL))
            //{
            //    //cs.RegisterClientScriptInclude(notifierScriptURL, notifierScriptURL);
            //    ScriptManager.RegisterClientScriptInclude(this, this.GetType(), notifierScriptURL, notifierScriptURL);
            //}
            //string cssRelativeURL = ProjectConfiguration.SiteUrl + "Scripts/MessageBox/Notify.css";
            //if (!cs.IsClientScriptBlockRegistered(cssRelativeURL))
            //{
            //    string cssLinkCode = string.Format(
            //        @"<link href='{0}' rel='stylesheet' type='text/css' />",
            //        cssRelativeURL);
            //    //cs.RegisterClientScriptBlock(this.GetType(), cssRelativeURL, cssLinkCode);
            //    //ScriptManager.RegisterClientScriptBlock(this,this.GetType(), cssRelativeURL, cssLinkCode,true);
            //    //ScriptManager.RegisterClientScriptInclude(this, this.GetType(), cssRelativeURL, cssRelativeURL);

            //}
            // ****************************** code to increase height *********************************************
            //string FooterCss = "$(\"#container\").css(\"height\", ($(\"#container\").height() + 60));";

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ClientScript", FooterCss, true);

        }
    }
}