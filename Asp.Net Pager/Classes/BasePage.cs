#region NameSpace
using System;
using System.Web.UI.WebControls;
using System.Web.UI;
//using Microsoft.Web.Services;
//using Microsoft.Web.Services.Routing;
//using Microsoft.Web.Services.Referral;
using System.Collections.Generic;
#endregion
namespace TechTracking.Classes
{

    public class BasePage : System.Web.UI.Page
    {
        #region Property Declaration

        protected string SortBy
        {
            get
            {
                return Convert.ToString(ViewState["SortBy"]);
            }
            set { ViewState["SortBy"] = value; }
        }

        protected string OrderBy
        {
            get
            {
                return ViewState["OrderBy"] != null ? Convert.ToString(ViewState["OrderBy"]) : "Asc";
            }
            set { ViewState["OrderBy"] = value; }
        }
        #endregion



        #region Enum

        protected enum MessageBoxType
        {
            Success,
            Error,
            Warning
        };

        #endregion

        #region Constructor

        public BasePage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region Methods

        /// <summary>
        ///Override InitializeCulture for Initialize the Current Culture
        /// </summary>
        protected override void InitializeCulture()
        {
            //try
            //{
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(ProjectSession.Culture.Substring(0, 2));
            //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ProjectSession.Culture.Substring(0, 2));
            //    Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.GetCultureInfo(CultureInfo.GetCultureInfo(ProjectSession.Culture).LCID).DateTimeFormat;
            //}
            //catch
            //{
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(ProjectConfiguration.Culture);
            //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ProjectConfiguration.Culture);
            //}
            //finally
            //{
            //    base.InitializeCulture();
            //}
        }

        /// <summary>
        /// Override the Page Loag Method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// Override PreInt for set current Theme
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            //Page.Theme = ProjectSession.Theme;
        }

        /// <summary>
        /// Show Message in label
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        protected void ShowMessage(string message, Label messageLabel, MessageBoxType messageType)
        {
            messageLabel.Text = messageType.ToString().ToUpper() + @" - " + message;
            messageLabel.CssClass = messageType.ToString();
        }

        /// <summary>
        /// Get Query String Value after decription
        /// </summary>
        /// <param name="queryStringName"></param>
        /// <returns></returns>
        protected string GetQueryString(string queryStringName)
        {
            string currentValue = string.Empty;
            if (Request.QueryString[queryStringName] != null)
                currentValue = EncryptionDecryption.GetDecrypt(Convert.ToString(Request.QueryString[queryStringName]));

            return currentValue;
        }

        /// <summary>
        /// Set Query encrypted query string value
        /// </summary>
        /// <param name="queryStringName"></param>
        /// <param name="queryStringValue"></param>
        /// <returns></returns>
        protected string SetQueryString(string queryStringName, string queryStringValue)
        {
            string currentValue = string.Empty;
            currentValue = queryStringName + "=" + EncryptionDecryption.GetEncrypt(queryStringValue);
            return currentValue;
        }

        /// <summary>
        /// Check Other Shared Pages
        /// </summary>
        /// <returns>false when exists in shared pages list</returns>
        protected bool CheckOtherPages()
        {
            List<string> lstPages = new List<string>() { "MYPEOPLELISTING.ASPX", "SHOWEVALLISTING.ASPX", "SCORETOTECH.ASPX", "SHOWEVALLISTING.ASPX", "TECHSCORELISTING.ASPX", "EDITPROFILE.ASPX", "TECHNICIANLISTING.ASPX", "ADDEDITTECHNICIAN.ASPX", "POPUPSHARE.ASPX", "TECHSHOWS.ASPX", "TIMESHEET.ASPX", "POPUPNEWTIMEENTRY.ASPX", "TECHNICIANDOCS.ASPX", "DISCUSSIONLISTING.ASPX", "DISCUSSION.ASPX" };
            string path = Request.Path.ToString();
            string[] strPath = null;

            strPath = path.Split('/');
            string strPageName = strPath[strPath.Length - 1].Trim().ToUpper();

            if (!lstPages.Contains(strPageName))
                return true;
            else
                return false;
        }
        #endregion
    }

    public class BaseTechPage : BasePage
    {

        #region Constructor

        public BaseTechPage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region Methods

        /// <summary>
        /// Override PreInt for set current Theme
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            //Page.Theme = "Default";
            //ProjectSession.Theme = Page.Theme;
            //this.Theme = "DCWebTheme";
            //if (ProjectSession.RoleType != "Admin")
            //    ProjectSession.UserName = string.Empty;
            // General.ValidateUser("admin");
            
            base.OnPreInit(e);
            string returnUrl = "?returnUrl=" + Request.RawUrl;
            //if (ProjectSession.UserID == 0)
            //{
            //    DataTable dataTableUser = TechTrackingDAL.Technician.TechnicianLogin("technician", EncryptionDecryption.GetEncrypt("technician")).Tables[0];
            //    if (dataTableUser.Rows.Count > 0)
            //    {
            //        ProjectSession.SetTechnicianProjectSessions(dataTableUser);
            //    }
            //}
            //ProjectSession.UserID = 122;
            if (ProjectSession.UserID == 0)
            {
                Response.Redirect(this.ResolveClientUrl("~/Login.aspx" + returnUrl));
            }
            if (ProjectSession.LevelID != 2)
            {
                Response.Redirect(this.ResolveClientUrl("~/Login.aspx" + returnUrl));
            }
            //else if (string.IsNullOrEmpty(ProjectSession.SSN))
            //{
            //    if (!Request.Url.AbsoluteUri.Contains("TermsAndConditions"))
            //        Response.Redirect(this.ResolveClientUrl("~/technician/TermsAndConditions.aspx"));
            //}
            else
            {


            }
        }

        #endregion
    }
    public class BaseAdminPopupPage : BasePage
    {
        #region Constructor

        public BaseAdminPopupPage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region Methods

        /// <summary>
        /// Override PreInt for set current Theme
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (ProjectSession.UserID == 0)
            {

                Response.Write("<span style='color:red;'> Sorry, Your session is Expired.</span>");
                //Response.Redirect(this.ResolveClientUrl("~/Login.aspx"));
            }
            else
            {

            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);


        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

        }

        #endregion
    }
    public class BaseTechPopupPage : BasePage
    {
        #region Constructor

        public BaseTechPopupPage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region Methods

        /// <summary>
        /// Override PreInt for set current Theme
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (ProjectSession.UserID == 0)
            {
                Control cntentPlaceHolder = this.Master.FindControl("ContentPlaceHolder1");
                if (cntentPlaceHolder != null)
                    cntentPlaceHolder.Visible = false;

                //Response.Write("<span style='color:red;'> Sorry, Your session is Expired.</span>");
                //Response.Redirect(this.ResolveClientUrl("~/Login.aspx"));
            }
            else
            {

            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);


        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

        }

        #endregion
    }

    public class BaseAdminPage : BasePage
    {
        #region Constructor

        public BaseAdminPage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region Methods

        /// <summary>
        /// Override PreInt for set current Theme
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            //Page.Theme = ProjectSession.Theme;
            //Page.Theme = "Admin";
            //ProjectSession.Theme = Page.Theme;
            // clsCommon.validateUser();
            base.OnPreInit(e);
            //ProjectSession.UserID = 10;
            //Check for user session
            string returnUrl = "?returnUrl=" + Request.RawUrl;
            if (ProjectSession.UserID == 0)
            {
                
                //Redirect to login page 
                Response.Redirect(this.ResolveClientUrl("~/Admin/Login.aspx" + returnUrl));
            }
            if (ProjectSession.LevelID != 1 && (ProjectSession.LevelID == 3 && CheckOtherPages()))
            {
                Response.Redirect(this.ResolveClientUrl("~/Login.aspx" + returnUrl));
            }
            if (ProjectSession.LevelID == 2)
            {
                Response.Redirect(this.ResolveClientUrl("~/Admin/Login.aspx" + returnUrl));
            }

        }


        #endregion
    }

    public class BasePMPage : BasePage
    {
        #region Constructor

        public BasePMPage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region Methods

        /// <summary>
        /// Override PreInt for set current Theme
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            //Page.Theme = ProjectSession.Theme;
            //Page.Theme = "PM";
            //ProjectSession.Theme = Page.Theme;
            // clsCommon.validateUser();
            base.OnPreInit(e);
            base.OnPreInit(e);
            string returnUrl = "?returnUrl=" + Request.RawUrl;
            //Check for user session
            if (ProjectSession.UserID == 0)
            {
                //Redirect to login page 
                Response.Redirect(this.ResolveClientUrl("~/Admin/Login.aspx" + returnUrl));
            }
            if (ProjectSession.LevelID != 3 && (ProjectSession.LevelID == 1 && CheckOtherPages()))
            {
                Response.Redirect(this.ResolveClientUrl("~/Admin/Login.aspx" + returnUrl));
            }
            if (ProjectSession.LevelID == 2)
            {
                Response.Redirect(this.ResolveClientUrl("~/Admin/Login.aspx" + returnUrl));
            }
        }

        #endregion
    }

}