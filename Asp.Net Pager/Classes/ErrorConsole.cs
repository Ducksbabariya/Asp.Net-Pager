#region NameSpace
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
#endregion
namespace TechTracking.Classes
{
    /// <summary>
    /// This Class is used the Error Handling 
    /// <Created By> Hardik Panchal - 01 Nov 2011</Created>
    /// </summary>
    public class ErrorConsole
    {
        #region Constructor

        public ErrorConsole()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region Methods

        /// <summary>
        /// Insert ErrorLog to Database
        /// </summary>
        /// <param name="strError"></param>
        /// <param name="strPageName"></param>
        public static void InsertErrorLog(string strError, string strPageName)
        {

        }

        /// <summary>
        /// Send Error Email to Site Admin
        /// </summary>
        /// <param name="ex"></param>
        public static void SendErrorEmail(Exception ex)
        {
            StringBuilder Error = new StringBuilder();
            string strTmp = null;

            //---insert error log into database 
            InsertErrorLog(ex.Message.ToString(), HttpContext.Current.Request.RawUrl);

            //----- Generate the Html Structure for the Sending Email 

            Error.Append("<b>Error in :</b> " + HttpContext.Current.Request.Path + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Url :</b> " + HttpContext.Current.Request.RawUrl + "<br><br>");

            // Get the exception object for the last error message that occured. 

            Error.Append("<b>Error Message : &nbsp;</b>" + ex.Message.ToString());

            Error.Append("<br><b>Error StackTrace : &nbsp;</b>" + ex.StackTrace.ToString());
            //'k 

            strTmp = ex.Source;
            Error.Append("<br><br><b>Error Source &nbsp;&nbsp;: &nbsp;&nbsp;</b>" + strTmp);

            if ((!object.ReferenceEquals(ex.TargetSite.Name, DBNull.Value)))
            {
                Error.Append("<br><br><b>Error Target Site :&nbsp; </b>" + ex.TargetSite.Name);
            }

            Error.Append("<br><br><b>QueryString Data</b><br>------------------------<br>");
            // Gathering QueryString information 
            Error.Append(HttpContext.Current.Request.QueryString.ToString() + "<br>");

            Error.Append("<br>");

            Error.Append("<br><b>Post Data</b><br>--------------<br>");
            // Gathering Post Data information 
            Error.Append(HttpContext.Current.Request.Form.ToString() + "<br>");
            Error.Append("<br>");
            Error.Append("<br>");

            Error.Append("<b>Session Info</b><br>--------------<br>");

            if (HttpContext.Current.Session != null)
            {
                System.Collections.Specialized.NameObjectCollectionBase.KeysCollection SessionKeys = HttpContext.Current.Session.Keys;

                for (int i = 0; i <= SessionKeys.Count - 1; i++)
                {
                    Error.Append("<br>" + (i + 1).ToString() + ":-><b>Name: </b>" + SessionKeys[i].ToString());

                    //if (HttpContext.Current.Session[i].GetType == "String")
                    //{
                    Error.Append(" <--> <b>Value:</b>" + HttpContext.Current.Session[SessionKeys[i].ToString()]);
                    //}

                    //if (HttpContext.Current.Session[i].GetType.Name == "DataTable")
                    //{
                    //    Error.Append(" <--> <b>Value:</b> Number of rows:" + (DataTable)HttpContext.Current.Session[SessionKeys[i].ToString()].Rows.Count.ToString());
                    //}
                }

                Error.Append("<br>Total Session Count:- " + HttpContext.Current.Session.Count);
            }

            string MailFrom = null;
            string MailTo = null;
            string Subject = null;

            MailFrom = "error@nu-ron.com";//ProjectConfiguration.AdminEmailID ;

            MailTo = ProjectConfiguration.ErrorEmail;

            Subject = "Error: " + HttpContext.Current.Request.RawUrl + " - " + DateTime.Now.ToString("dd/MM/yyy HH:mm:ss");

            //--sending error mail 
            Email.Send(MailFrom, MailTo, "", "", Subject, Error.ToString(), "", true);
        }

        public static IHttpHandler GetNotFoundHttpHandler()
        {
            return System.Web.Compilation.BuildManager.CreateInstanceFromVirtualPath("~/ErrorPage.htm", typeof(Page)) as Page;
        }

        #endregion
    }

}