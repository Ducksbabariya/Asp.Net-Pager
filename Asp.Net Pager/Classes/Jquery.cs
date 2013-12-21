using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TechTracking.Classes
{
    public static class Jquery
    {
        public static void SetFooter(this Page page)
        {
            ClientScriptManager cs = page.ClientScript;
          
            string SiteURL = ProjectConfiguration.SiteUrl;
            //string MainJquery = SiteURL + "Scripts/jquery-1.6.4.min.js";
            //if (!cs.IsStartupScriptRegistered(MainJquery))
            //{
            //    //cs.RegisterClientScriptInclude(notifierScriptURL, notifierScriptURL);
            //    ScriptManager.RegisterClientScriptInclude(page, page.GetType(), MainJquery, MainJquery);
            //}
            //string JqueryUi = SiteURL + "Scripts/jqueryui/jquery-ui-1.8.16.custom.min.js";
            //if (!cs.IsStartupScriptRegistered(MainJquery))
            //{
            //    //cs.RegisterClientScriptInclude(notifierScriptURL, notifierScriptURL);
            //    ScriptManager.RegisterClientScriptInclude(page, page.GetType(), JqueryUi, JqueryUi);
            //}
            string notifierScriptURL = SiteURL + "Scripts/MessageBox/Notify.js";
            if (!cs.IsStartupScriptRegistered(notifierScriptURL))
            {
                //cs.RegisterClientScriptInclude(notifierScriptURL, notifierScriptURL);
                ScriptManager.RegisterClientScriptInclude(page, page.GetType(), notifierScriptURL, notifierScriptURL);
                //ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "ClientScript", "setLoader();", true);
            }

            //string FooterCss = "$(document).ready(function(){ $(\"#container\").css(\"height\", ($(document).height() - 60));});";
            //if (ScriptManager.GetCurrent(page) != null)
            //{
            //    ScriptManager.RegisterStartupScript(page, page.GetType(), "ClientScript", FooterCss, true);
            //}
            //else
            //{
            //    page.ClientScript.RegisterStartupScript(page.GetType(),
            //                                            "ClientScript",
            //                                            FooterCss,
            //                                            true);
            //}
        }
        public static void NewUserSetFooter(this Page page)
        {
            ClientScriptManager cs = page.ClientScript;
            Uri uri = HttpContext.Current.Request.Url;
            string SiteURL = ProjectConfiguration.SiteUrl;
            string notifierScriptURL = SiteURL + "Scripts/MessageBox/NewUser.js";
            if (!cs.IsStartupScriptRegistered(notifierScriptURL))
            {              
                ScriptManager.RegisterClientScriptInclude(page, page.GetType(), notifierScriptURL, notifierScriptURL);
            }
        }
        public static void LoginSetFooter(this Page page)
        {
            ClientScriptManager cs = page.ClientScript;
            Uri uri = HttpContext.Current.Request.Url;
            string SiteURL = ProjectConfiguration.SiteUrl;
            string notifierScriptURL = SiteURL + "Scripts/MessageBox/Login.js";
            if (!cs.IsStartupScriptRegistered(notifierScriptURL))
            {
                ScriptManager.RegisterClientScriptInclude(page, page.GetType(), notifierScriptURL, notifierScriptURL);
            }
        }
        /// <summary>
        /// Shows the errors.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="text">The text.</param>
        public static void FlipDiv(this Page page, string DivId, string Direction)
        {

            string FlipDiv = "$('#" + DivId + "').flippy({'direction':'" + Direction + "','duration':'750'});";


            if (ScriptManager.GetCurrent(page) != null)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(),
                                                    "FlipDiv",
                                                    FlipDiv,
                                                    true);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(),
                                                        "FlipDiv",
                                                        FlipDiv,
                                                        true);
            }

        }
    }
}