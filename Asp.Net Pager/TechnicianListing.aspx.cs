using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TechTracking.Classes;
using TechTracking.UserControls;
using System.Linq;
namespace TechTracking.Admin
{
    public partial class TechnicianListing : BaseAdminPage
    {

        #region Properties
        /// <summary>
        /// gets value from Search textBox
        /// </summary>
        public string SearchColumn
        {
            get
            {
                return ddlSortBy.SelectedValue;
            }
        }




        /// <summary>
        /// gets value from Search textBox
        /// </summary>
        public string SearchText
        {
            get
            {
                return txtSearch.Text.Trim();
            }
        }
        /// <summary>
        /// gets value from Status dropdown
        /// </summary>
        public int Status
        {
            get
            {
                return General.GetInt(ddlStatus.SelectedValue);
            }
        }

        #endregion

        #region Delegate
        public delegate void delPopulateData(int myInt);
        #endregion

        #region PageEvent
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(base.SortBy))
                base.SortBy = "FirstName";

            if (!Page.IsPostBack)
            {
                if (ProjectSession.LevelID == 3)
                {
                    LinkButton lnkTechnicianListing = (LinkButton)this.Master.FindControl("lnkTechnician");
                    if (lnkTechnicianListing != null)
                    {
                        lnkTechnicianListing.CssClass = "nav_link active";
                    }
                    
                }
                if (ProjectSession.LevelID == 1)
                {
                    LinkButton lnkTechnicianListing = (LinkButton)this.Master.FindControl("lnkTechnician");
                    if (lnkTechnicianListing != null)
                    {
                        lnkTechnicianListing.CssClass = "nav_link active";
                    }
                }
                pagerApps.PageIndex = 1;
                BindGrid(pagerApps.PageIndex);
                //ddlSortBy.ClearSelection();
                //ddlSortBy.Items.FindByValue(base.SortBy).Selected = true;
            }
            delPopulateData delPopulate = new delPopulateData(this.BindGrid);
            pagerApps.UpdatePageIndex = delPopulate;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "manageMultiple", "manageMultiple();", true);

        }

        protected override void OnPreInit(EventArgs e)
        {
            if (ProjectSession.LevelID == 1)
            {
                this.MasterPageFile = this.ResolveUrl("~/Master/Admin.Master");
            }
            if (ProjectSession.LevelID == 3)
            {
                this.MasterPageFile = this.ResolveUrl("~/Master/PM.Master");
            }
            base.OnPreInit(e);
        }
        #endregion

        #region Grid Events

        protected void gvTechnicianListing_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "sort")
            {
                if (base.OrderBy == "Asc")
                {
                    base.SortBy = General.ToStr(e.CommandArgument);
                    base.OrderBy = "Desc";
                }
                else
                {
                    base.SortBy = General.ToStr(e.CommandArgument);
                    base.OrderBy = "Asc";
                }
                BindGrid(pagerApps.PageIndex);
                try
                {
                    ddlSortBy.ClearSelection();
                    ddlSortBy.Items.FindByValue(base.SortBy).Selected = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            #region Not now used
            if (e.CommandName.ToLower() == "interview_check")
            {
                int PKTechnicianID = Convert.ToInt32(e.CommandArgument);
                bool interview = TechTrackingDAL.Technician.TechnicianUpdateInterview(PKTechnicianID, true);

                //BindGrid(pagerApps.PageIndex);
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                LinkButton lnkInterviewtrue = (LinkButton)gvrow.FindControl("lnkInterviewtrue");
                LinkButton lnkInterviewFalse = (LinkButton)gvrow.FindControl("lnkInterviewFalse");
                if (lnkInterviewFalse != null && lnkInterviewtrue != null)
                {
                    lnkInterviewFalse.Visible = !(lnkInterviewFalse.Visible);
                    lnkInterviewtrue.Visible = !(lnkInterviewtrue.Visible);
                }

            }
            if (e.CommandName.ToLower() == "interview_uncheck")
            {
                int PKTechnicianID = Convert.ToInt32(e.CommandArgument);
                bool interview = TechTrackingDAL.Technician.TechnicianUpdateInterview(PKTechnicianID, false);

                //BindGrid(pagerApps.PageIndex);
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                LinkButton lnkInterviewtrue = (LinkButton)gvrow.FindControl("lnkInterviewtrue");
                LinkButton lnkInterviewFalse = (LinkButton)gvrow.FindControl("lnkInterviewFalse");
                if (lnkInterviewFalse != null && lnkInterviewtrue != null)
                {
                    lnkInterviewFalse.Visible = !(lnkInterviewFalse.Visible);
                    lnkInterviewtrue.Visible = !(lnkInterviewtrue.Visible);
                }
            }
            if (e.CommandName.ToLower() == "passport_uncheck")
            {
                int PKTechnicianID = Convert.ToInt32(e.CommandArgument);
                bool interview = TechTrackingDAL.Technician.TechnicianUpdatePassport(PKTechnicianID, false);

                //BindGrid(pagerApps.PageIndex);

                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                LinkButton lnkPassporttrue = (LinkButton)gvrow.FindControl("lnkPassporttrue");
                LinkButton lnkPassportfalse = (LinkButton)gvrow.FindControl("lnkPassportfalse");
                if (lnkPassporttrue != null && lnkPassportfalse != null)
                {
                    lnkPassportfalse.Visible = !(lnkPassportfalse.Visible);
                    lnkPassporttrue.Visible = !(lnkPassporttrue.Visible);
                }

            }
            if (e.CommandName.ToLower() == "passport_check")
            {
                int PKTechnicianID = Convert.ToInt32(e.CommandArgument);
                bool interview = TechTrackingDAL.Technician.TechnicianUpdatePassport(PKTechnicianID, true);

                // BindGrid(pagerApps.PageIndex);

                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                LinkButton lnkPassporttrue = (LinkButton)gvrow.FindControl("lnkPassporttrue");
                LinkButton lnkPassportfalse = (LinkButton)gvrow.FindControl("lnkPassportfalse");
                if (lnkPassporttrue != null && lnkPassportfalse != null)
                {
                    lnkPassportfalse.Visible = !(lnkPassportfalse.Visible);
                    lnkPassporttrue.Visible = !(lnkPassporttrue.Visible);
                }
            }
            if (e.CommandName.ToLower() == "access_uncheck")
            {
                int PKTechnicianID = Convert.ToInt32(e.CommandArgument);
                bool interview = TechTrackingDAL.Technician.TechnicianUpdateAccess(PKTechnicianID, false);

                // BindGrid(pagerApps.PageIndex);

                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                LinkButton lnkAccesstrue = (LinkButton)gvrow.FindControl("lnkAccesstrue");
                LinkButton lnkAccessFalse = (LinkButton)gvrow.FindControl("lnkAccessFalse");
                if (lnkAccessFalse != null && lnkAccesstrue != null)
                {
                    lnkAccessFalse.Visible = !(lnkAccessFalse.Visible);
                    lnkAccesstrue.Visible = !(lnkAccesstrue.Visible);
                }

            }
            if (e.CommandName.ToLower() == "access_check")
            {
                int PKTechnicianID = Convert.ToInt32(e.CommandArgument);
                bool interview = TechTrackingDAL.Technician.TechnicianUpdateAccess(PKTechnicianID, true);

                // BindGrid(pagerApps.PageIndex);


                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                LinkButton lnkAccesstrue = (LinkButton)gvrow.FindControl("lnkAccesstrue");
                LinkButton lnkAccessFalse = (LinkButton)gvrow.FindControl("lnkAccessFalse");
                if (lnkAccessFalse != null && lnkAccesstrue != null)
                {
                    lnkAccessFalse.Visible = !(lnkAccessFalse.Visible);
                    lnkAccesstrue.Visible = !(lnkAccesstrue.Visible);
                }
            }
            #endregion

            if (e.CommandName.ToLower() == "del")
            {
                int RerurnVal = TechTrackingDAL.Technician.Delete(ConvertTo.Integer(e.CommandArgument));
                if (RerurnVal == -1)
                {
                    MessageBox.AddErrorMessage("Sorry, This Technician is already used in the System");
                    return;
                }
                else
                {
                    if (gvTechnicianListing.Rows.Count == 1 && pagerApps.PageIndex > 1)
                    {
                        pagerApps.PageIndex -= 1;
                    }
                    BindGrid(pagerApps.PageIndex);
                    delPopulateData delPopulate = new delPopulateData(this.BindGrid);
                    pagerApps.UpdatePageIndex = delPopulate;
                    MessageBox.AddSuccessMessage("Technician deleted successfully");
                }
            }
            else if (e.CommandName == "delNow")
            {

                string toDeleteTechIDs = string.Join(",", gvTechnicianListing.Rows.Cast<GridViewRow>()
                                     .Where(a => ((CheckBox)a.FindControl("chkDelete")).Checked)
                                     .Select(g => gvTechnicianListing.DataKeys[g.RowIndex].Value).ToArray());

                int RerurnVal = TechTrackingDAL.Technician.MultipleDelete(toDeleteTechIDs);
                if (RerurnVal == -1)
                {
                    MessageBox.AddErrorMessage("Sorry, This Technician is already used in the System");
                    return;
                }
                else
                {
                    if (General.GetInt(hdnSelected.Value) == pagerApps.RecordsPerPage)
                    {
                        pagerApps.PageIndex -= 1;
                    }
                    BindGrid(pagerApps.PageIndex);
                    delPopulateData delPopulate = new delPopulateData(this.BindGrid);
                    pagerApps.UpdatePageIndex = delPopulate;
                    MessageBox.AddSuccessMessage("Technicians deleted successfully");
                }
            }
            else if (e.CommandName == "ShareNow")
            {
                string toShareTechIDs = string.Join(",", gvTechnicianListing.Rows.Cast<GridViewRow>()
                                     .Where(a => ((CheckBox)a.FindControl("chkShare")).Checked)
                                     .Select(g => gvTechnicianListing.DataKeys[g.RowIndex].Value).ToArray());
                string ShareScript = "opPopup('PopupShare.aspx?" + SetQueryString("TechnicianID", toShareTechIDs) + "','400','400','Share Multiple');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShareScript", ShareScript, true);
            }

        }


        protected void gvTechnicianListing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header && base.SortBy != null)
            {
                //LinkButton lnkHeaderDeleteNow = (LinkButton)e.Row.FindControl("lnkHeaderDeleteNow");
                //lnkHeaderDeleteNow.Attributes["onclick"] = "return false;";
                //LinkButton lnkHeaderShareNow = (LinkButton)e.Row.FindControl("lnkHeaderShareNow");
                //lnkHeaderShareNow.Attributes["onclick"] = "return false;";
                Image ImgSort = new Image();
                if (base.OrderBy.ToLower() == "asc")
                    ImgSort.ImageUrl = "~/App_Themes/Default/images/grid_ar-down.gif";
                else
                    ImgSort.ImageUrl = "~/App_Themes/Default/images/grid_ar-up.gif";

                switch (base.SortBy)
                {
                    case "FirstName":
                        PlaceHolder pcName = (PlaceHolder)e.Row.FindControl("pcName");
                        pcName.Controls.Add(ImgSort);
                        break;

                    case "LastName":
                        PlaceHolder pcLastName = (PlaceHolder)e.Row.FindControl("pcLastName");
                        pcLastName.Controls.Add(ImgSort);
                        break;

                    case "Rank":
                        PlaceHolder pcRank = (PlaceHolder)e.Row.FindControl("pcRank");
                        pcRank.Controls.Add(ImgSort);
                        break;

                    case "Rating":
                        PlaceHolder pcRating = (PlaceHolder)e.Row.FindControl("pcRating");
                        pcRating.Controls.Add(ImgSort);
                        break;

                    case "AvgBid":
                        PlaceHolder pcAvgBid = (PlaceHolder)e.Row.FindControl("pcAvgBid");
                        pcAvgBid.Controls.Add(ImgSort);
                        break;

                    case "Location":
                        PlaceHolder pcLocation = (PlaceHolder)e.Row.FindControl("pcLocation");
                        pcLocation.Controls.Add(ImgSort);
                        break;

                    case "Positions":
                        PlaceHolder pcPositions = (PlaceHolder)e.Row.FindControl("pcPositions");
                        pcPositions.Controls.Add(ImgSort);
                        break;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRating = (Label)e.Row.FindControl("lblRating");
                lblRating.Text = ConvertTo.Float(DataBinder.Eval(e.Row.DataItem, "Rating")).ToString("F2");

                LinkButton lnkShare = (LinkButton)e.Row.FindControl("lnkShare");
                lnkShare.Attributes.Add("onclick", "javascript:return opPopup('PopupShare.aspx?" + SetQueryString("TechnicianID", Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PKTechnicianID"))) + "','400','400','Share');");
            }

        }
        #endregion

        #region Methods
        /// <summary>
        /// bind grid with data
        /// </summary>
        /// <param name="currentPageIndex"></param>
        private void BindGrid(int currentPageIndex)
        {

            DataSet ds = TechTrackingDAL.Technician.SelectAll(currentPageIndex, pagerApps.RecordsPerPage, SearchText, base.SortBy, base.OrderBy, Status, SearchColumn);
            pagerApps.TotalRecords = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            gvTechnicianListing.DataSource = ds;
            gvTechnicianListing.DataBind();

        }
        #endregion

        #region Control Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            pagerApps.PageIndex = 1;
            BindGrid(pagerApps.PageIndex);

            delPopulateData delPopulate = new delPopulateData(this.BindGrid);
            pagerApps.UpdatePageIndex = delPopulate;
        }

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSortBy.SelectedValue != "All")
            {

                if (base.OrderBy == "Asc")
                {
                    base.SortBy = General.ToStr(ddlSortBy.SelectedValue);
                    base.OrderBy = "Desc";
                }
                else
                {
                    base.SortBy = General.ToStr(ddlSortBy.SelectedValue);
                    base.OrderBy = "Asc";
                }
            }
            else
            {
                base.SortBy = "FirstName";
                base.OrderBy = "Asc";
            }
            BindGrid(pagerApps.PageIndex);
            delPopulateData delPopulate = new delPopulateData(this.BindGrid);
            pagerApps.UpdatePageIndex = delPopulate;


        }
        #endregion

        #region WebMethods
        /// <summary>
        /// web method Update Technician Access
        /// </summary>
        /// <param name="PKTechnicianID"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod()]
        public static bool UpdateTechnicianAccess(int PKTechnicianID)
        {
            return TechTrackingDAL.Technician.TechnicianUpdateInterview(PKTechnicianID, true);
        }



        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] GetCompletionList(string prefixText, int count)
        {
            return new string[10];
        }
        #endregion



    }
}