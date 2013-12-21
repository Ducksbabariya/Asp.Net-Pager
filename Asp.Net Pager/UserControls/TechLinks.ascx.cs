using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechTracking.Classes;

namespace TechTracking.UserControls
{
    public partial class TechLinks : System.Web.UI.UserControl
    {

        #region Properties
        //public int TechnicianID
        //{
        //    get { return (Request.QueryString["TechnicianID"] != null ? General.GetInt(base.GetQueryString("TechnicianID")) : 0); }
        //}
        public int _TechnicianID
        {
            get
            {
                return General.GetInt(ViewState["TechnicianID"]);
            }
            set
            {
                ViewState["TechnicianID"] = value;
            }
        }
        #endregion
        #region PageEvents
        protected void Page_Load(object sender, EventArgs e)
        {
            setActive();
            SetLinks(_TechnicianID);
        }
        #endregion



        #region Methods

        public void setActive()
        {
            string sPagePath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oFileInfo = new System.IO.FileInfo(sPagePath);
            string sPageName = oFileInfo.Name.ToLower();
            switch (sPageName)
            {
                case "addedittechnician.aspx":
                    lnkProfile.CssClass = "TechLinks Active";
                    break;
                case "techshows.aspx":
                    lnkShows.CssClass = "TechLinks Active";
                    break;
                case "techniciandocs.aspx":
                    lnkDocuments.CssClass = "TechLinks Active";
                    break;
                case "discussionlisting.aspx":
                    lnkDiscussion.CssClass = "TechLinks Active";
                    break;

                case "discussion.aspx":
                    lnkDiscussion.CssClass = "TechLinks Active";
                    break;
                case "timesheet.aspx":
                    lnkTimeSheets.CssClass = "TechLinks Active";
                    break;
                default:
                    break;
            }

        }
        public void SetLinks(int TechnicianID)
        {
            lnkProfile.PostBackUrl = "~/Admin/AddEditTechnician.aspx?TechnicianID=" + Request.QueryString["TechnicianID"];
            lnkShows.PostBackUrl = "~/Admin/TechShows.aspx?TechnicianID=" + Request.QueryString["TechnicianID"];
            lnkDocuments.PostBackUrl = "~/Admin/TechnicianDocs.aspx?TechnicianID=" + Request.QueryString["TechnicianID"];
            lnkDiscussion.PostBackUrl = "~/Admin/DiscussionListing.aspx?TechnicianID=" + Request.QueryString["TechnicianID"];
            lnkTimeSheets.PostBackUrl = "~/Admin/TechShows.aspx?TechnicianID=" + Request.QueryString["TechnicianID"];

        }
        #endregion
    }
}