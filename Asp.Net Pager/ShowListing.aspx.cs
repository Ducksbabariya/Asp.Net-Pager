using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using TechTracking.Classes;
using TechTracking.UserControls;
using TechTrackingDAL;
namespace TechTracking.Admin
{
    public partial class ShowListing : BaseAdminPage
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
        #endregion

        #region Delegates
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInt"></param>
        public delegate void delPopulateData(int myInt);
        #endregion

        #region PageEvents
        protected void Page_Load(object sender, EventArgs e)
        {
            base.SortBy = "StartDate";
            base.OrderBy = "Desc";
            if (!Page.IsPostBack)
            {
                LinkButton lnkShowListing = (LinkButton)this.Master.FindControl("lnkShows");
                if (lnkShowListing != null)
                    lnkShowListing.CssClass = "nav_link active";


                BindRepeater(1);
            }
            delPopulateData delPopulate = new delPopulateData(this.BindRepeater);
            pagerApps.UpdatePageIndex = delPopulate;

        }
        #endregion

        #region Methods
        /// <summary>
        /// bind repeater with data
        /// </summary>
        /// <param name="currentPageIndex"></param>
        private void BindRepeater(int currentPageIndex)
        {
            DataSet ds = Show.SelectAllNEWSP(currentPageIndex, pagerApps.RecordsPerPage, SearchText, base.SortBy, base.OrderBy, SearchColumn);
            pagerApps.TotalRecords = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            rptShowList.DataSource = ds;
            rptShowList.DataBind();
        }


        #endregion

        #region Control Events

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSortBy.SelectedValue != "sortBy")
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
                BindRepeater(pagerApps.PageIndex);
                delPopulateData delPopulate = new delPopulateData(this.BindRepeater);
                pagerApps.UpdatePageIndex = delPopulate;
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pagerApps.PageIndex = 1;
            BindRepeater(pagerApps.PageIndex);
            delPopulateData delPopulate = new delPopulateData(this.BindRepeater);
            pagerApps.UpdatePageIndex = delPopulate;
        }
        protected void rptShowList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {
                string pkShowID = General.ToStr(e.CommandArgument);
                string PasswordScript = "MakeBidPopup('PopupPasswordConfirm.aspx?" + base.SetQueryString("ShowID", pkShowID) + "','Password Confirmation','320','370'); ";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Password Confirm", PasswordScript, true);


                if (rptShowList.Items.Count == 1 && pagerApps.PageIndex != 1)
                {
                    pagerApps.PageIndex -= 1;
                }
                BindRepeater(pagerApps.PageIndex);
                delPopulateData delPopulate = new delPopulateData(this.BindRepeater);
                pagerApps.UpdatePageIndex = delPopulate;



            }
            else if (e.CommandName == "Edit")
            {
                //HiddenField hdbBidStartDate = (HiddenField)e.Item.FindControl("hdbBidStartDate");
                //if (hdbBidStartDate != null)
                //{
                //    if (DateTime.Now > DateSaver.GetDate(hdbBidStartDate.Value))
                //    {
                //        MessageBox.AddErrorMessage("Bid is started for this show");
                //    }
                //    else
                //    {
                Response.Redirect(this.ResolveClientUrl("~/Admin/AddEditShow.aspx?" + base.SetQueryString("ShowID", Convert.ToString(e.CommandArgument))));
                //   }
                //}
            }

        }
        protected void rptShowList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                TechTracking.UserControls.Calender ucCalender = (TechTracking.UserControls.Calender)e.Item.FindControl("ucCalender");
                string[] travelDays = General.ToStr(drv["TravelDates"]).Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
                List<DateTime> lstdates = new List<DateTime>();
                foreach (string strDate in travelDays)
                {
                    lstdates.Add(DateSaver.GetDate(strDate));
                }
                ucCalender.LoadData(DateSaver.GetDate(drv["StartDate"]), DateSaver.GetDate(drv["EndDate"]), lstdates);

                string script = "return MakeBidPopup('PopupMyProspectDetail.aspx?" + base.SetQueryString("ShowID", General.ToStr(drv.Row["PKShowID"])) + "','Show Detail','500','770'); return false;";
                System.Web.UI.HtmlControls.HtmlImage imgLogo = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgLogo");
                if (imgLogo != null)
                {
                    imgLogo.Attributes["onclick"] = script;
                }



                //string sa = "return MakeBidPopup('PopupPasswordConfirm.aspx?" + base.SetQueryString("ShowID", General.ToStr(drv.Row["PKShowID"])) + "','Show Detail','500','750'); return false;";
                //if (DateTime.Now > DateSaver.GetDate(drv["BidStartDate"]))
                //{
                LinkButton lnkView = (LinkButton)e.Item.FindControl("lnkView");
                //lnkView.CommandName = "View";
                //lnkView.Text = "View";
                lnkView.OnClientClick = script;
                //}
                LinkButton lnkMore = (LinkButton)e.Item.FindControl("lnkMore");
                if (lnkMore.Text.Length > 50)
                {
                    lnkMore.Text = lnkMore.Text.Substring(0, 47) + "...";
                    lnkMore.OnClientClick = script;
                    lnkMore.Style["cursor"] = "default";
                }
                else
                {
                    lnkMore.OnClientClick = "return false;";
                }
            }
        }


        #endregion


    }
}