using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechTracking.Classes;
using TechTracking.UserControls;
using TechTrackingDAL;

namespace TechTracking.Admin
{
    [Serializable]
    public class positionTravelDays
    {
        public int PKShowPositionID { get; set; }
        public int FKPositionID { get; set; }
        public int FKShowID { get; set; }
        public string PositionName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    [Serializable]
    public class _ShowPosition
    {
        public int PKShowPositionID { get; set; }
        public int FKPositionID { get; set; }
        public int FKShowID { get; set; }
        public int NoOfPosition { get; set; }
        public int Maxbidrate { get; set; }
        public string PositionName { get; set; }
    }
    public partial class AddEditShow : BaseAdminPage
    {
        string SelectedPositionID = null;
        #region Properties
        public DataSet GetShowPublishData
        {
            get
            {
                return (DataSet)ViewState["GetShowPublishData"];
            }
            set
            {
                ViewState["GetShowPublishData"] = value;
            }
        }
        public int ShowID
        {
            get { return (Request.QueryString["ShowID"] != null ? General.GetInt(GetQueryString("ShowID")) : 0); }
        }
        public int _ShowID
        {
            get
            {
                return General.GetInt(ViewState["ShowID"]);
            }
            set
            {
                ViewState["ShowID"] = value;
            }
        }

        public Show objShow
        {
            get
            {
                return (Show)ViewState["objShow"];
            }
            set
            {
                ViewState["objShow"] = value;
            }
        }
        public EmailTemplate emailTemplate
        {
            get
            {
                return (EmailTemplate)ViewState["emailTemplate"];
            }
            set
            {
                ViewState["emailTemplate"] = value;
                //Session["objShowPositions"] = value;
            }
        }
        public List<positionTravelDays> objpositionTravelDays
        {
            get
            {
                return (List<positionTravelDays>)ViewState["objpositionTravelDays"];
            }
            set
            {
                ViewState["objpositionTravelDays"] = value;
                //Session["objShowPositions"] = value;
            }
        }
        public List<_ShowPosition> objShowPositions
        {
            get
            {
                return (List<_ShowPosition>)ViewState["objShowPositions"];
            }
            set
            {
                ViewState["objShowPositions"] = value;
                //Session["objShowPositions"] = value;
            }
        }
        public List<string> objShowImportantDocs
        {
            get
            {
                return (List<string>)ViewState["objShowImportantDocs"];
            }
            set
            {
                ViewState["objShowImportantDocs"] = value;
            }
        }
        public DataSet ConfirmedTechs
        {
            get
            {
                return (DataSet)ViewState["ConfirmedTechs"];
            }
            set
            {
                ViewState["ConfirmedTechs"] = value;
            }
        }

        private bool fromDashboard
        {
            get { return Request.QueryString["fromDashboard"] != null && Request.QueryString["fromDashboard"] == "1" ? true : false; }
        }
        #endregion

        #region PageEvents

        protected void Page_Load(object sender, EventArgs e)
        {
            //ShowQualifiedTechs(46, null, null, null);
            //string CounterURL = ProjectConfiguration.SiteUrl + "Scripts/Counter/jquery.countdown.js";

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CounterURL", "assignDatePicker();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SetChoosen", "setChoosen();", true);
           
            if (!Page.IsPostBack)
            {
                _ShowID = ShowID;
                base.SortBy = "FirstName";
                base.OrderBy = "Asc";
                objShowPositions = new List<_ShowPosition>();
                objpositionTravelDays = new List<positionTravelDays>();
                ProjectSession.lstTravelDays = new List<string>();
                ProjectSession.lstPositionTravelDays = new List<PositionTravelDays>();
                ahBasicInfo.Attributes.Add("class", "active");
                LinkButton lnkShowListing = (LinkButton)this.Master.FindControl("lnkShows");
                if (lnkShowListing != null)
                {
                    lnkShowListing.CssClass = "nav_link active";
                }
                loadInitialData();


                objShowImportantDocs = new List<string>();
                if (_ShowID > 0)
                {
                    lblHeaderLabel.Text = "Edit Show";
                    objShow = new Show(_ShowID);
                    LoadData();
                    aConfirmedTechs.Visible = true;
                    BindConfirmedTechs();
                }
                else
                {
                    aConfirmedTechs.Visible = false;
                    objShow = new Show();
                }
            }
            imgLogoImage.Src = "~/Photo/" + objShow.LogoPath;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "initializeDatePickers", "initializeDatePickers();", true);
        }

        private void loadInitialData()
        {
            DataSet ds = Show.AddEditShowBindings();
            if (ds != null && ds.Tables.Count > 0)
            {
                dlPositions.DataSource = ds.Tables[0];
                dlPositions.DataBind();
                ddlCustomer.DataSource = ds.Tables[1];
                ddlCustomer.DataTextField = "CustomerName";
                ddlCustomer.DataValueField = "PKCustomerID";
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, new ListItem("--Select--", "0"));
                chklstProjectManagers.DataSource = ds.Tables[2];
                chklstProjectManagers.DataTextField = "FirstName";
                chklstProjectManagers.DataValueField = "PKAccountID";
                chklstProjectManagers.DataBind();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets and sets data for edit
        /// </summary>
        private void LoadData()
        {

            // Repeater
            string[] strImportantDocPath = objShow.ImportantDocPath.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
            objShowImportantDocs = new List<string>(strImportantDocPath);
            string[] travelDays = objShow.TravelDays.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
            ProjectSession.lstTravelDays = new List<string>();

            foreach (string strDate in travelDays)
            {
                ProjectSession.lstTravelDays.Add(DateSaver.DispalyDate(strDate, DateSaver.DateFormater.MMddyyyy, '/'));
            }
            try
            {
                if (travelDays.Length == 2)
                {
                    txtArrivalDate.Text = DateSaver.DispalyDate(travelDays[0], DateSaver.DateFormater.MMddyyyy, '/');
                    txtLeaveDate.Text = DateSaver.DispalyDate(travelDays[1], DateSaver.DateFormater.MMddyyyy, '/');
                }
            }
            catch (Exception)
            {

                throw;
            }


            btnTravelDaysBinder_Click(null, null);
            bindRepeater();
            //  BindSelectedPositionDataList();
            //Basic Info
            txtTitle.Text = objShow.Title;
            txtNearestAirport.Text = objShow.NearestAirport;
            txtCity.Text = objShow.City;
            txtStartDate.Text = hdnsStartDate.Value = DateSaver.DispalyDate(objShow.StartDate, DateSaver.DateFormater.MMddyyyy, '/');
            txtEndDate.Text = DateSaver.DispalyDate(objShow.EndDate, DateSaver.DateFormater.MMddyyyy, '/');
            txtBidStartdate.Text = hdnSSDate.Value = DateSaver.DispalyDate(objShow.BidStartDate, DateSaver.DateFormater.MMddyyyy, '/');
            txtBidEndDate.Text = DateSaver.DispalyDate(objShow.BidEndDate, DateSaver.DateFormater.MMddyyyy, '/');
            ddlCustomer.SelectedValue = General.ToStr(objShow.FkCustomerID);
            hdnStartDate.Value = txtBidEndDate.Text;
            txtEmail.Text = objShow.Email;
            txtDescription.Text = objShow.Description;
            if (objShow.LogoPath != null)
            {
                imgLogoImage.Src = "~/Photo/" + objShow.LogoPath;
                btnAddLogo.Visible = false;
                lnkRemoveImage.Visible = true;
            }

            if (!string.IsNullOrEmpty(objShow.ProjectManagers))
            {
                string[] ProjectManagers = objShow.ProjectManagers.Split(',');
                foreach (string str in ProjectManagers)
                {
                    try { chklstProjectManagers.Items.FindByValue(str).Selected = true; }
                    catch { }
                }
            }

            // Show Positions
            DataSet ds = ShowPosition.SelectByFKShowID(_ShowID);
            objShowPositions = new List<_ShowPosition>();
            foreach (DataListItem dlItem in dlPositions.Items)
            {
                HiddenField hdnPositionID = (HiddenField)dlItem.FindControl("hdnPositionID");

                DataRow[] drpos = ds.Tables[0].Select("FKPositionID = " + hdnPositionID.Value);
                if (drpos.Length > 0)
                {
                    _ShowPosition objShowposition = new _ShowPosition();
                    CheckBox chkPosition = (CheckBox)dlItem.FindControl("chkPosition");
                    chkPosition.Checked = true;
                    TextBox txtQuantity = (TextBox)dlItem.FindControl("txtQuantity");
                    txtQuantity.Text = General.ToStr(drpos[0]["NoOfPosition"]);
                    objShowposition.Maxbidrate = General.GetInt(drpos[0]["Maxbidrate"]);
                    objShowposition.FKPositionID = General.GetInt(hdnPositionID.Value);
                    objShowposition.NoOfPosition = General.GetInt(txtQuantity.Text);
                    objShowposition.PositionName = dlItem.FindControl<Label>("lblPositionName").Text;
                    objShowPositions.Add(objShowposition);
                }
            }
            if (objShowPositions.Where(s => s.Maxbidrate > 0).Count() > 0)
            {
                chkMaximumRate.Checked = true;
            }
            BindSelectedPositionDataListfoeMaxBidRate();
            //Position wise travel days

            DataSet dsTravelDays = Show.SelectShowPositions_New(_ShowID);
            objpositionTravelDays = new List<positionTravelDays>();

            foreach (DataListItem dlItem in dlPositions.Items)
            {
                CheckBox chkPosition = (CheckBox)dlItem.FindControl("chkPosition");
                if (chkPosition.Checked)
                {
                    Label PositionName = dlItem.FindControl<Label>("lblPositionName");
                    HiddenField hdnPositionID = (HiddenField)dlItem.FindControl("hdnPositionID");
                    objpositionTravelDays.Add(new positionTravelDays()
                    {
                        FKPositionID = General.GetInt(hdnPositionID.Value),
                        PositionName = PositionName.Text
                    });
                }
            }
            BindSelectedPositionDataList();
            foreach (DataListItem ITravelDays in dlTravelDays.Items)
            {
                HiddenField hdnPositionID = (HiddenField)ITravelDays.FindControl("hdnPositionID");

                DataRow[] drpos = dsTravelDays.Tables[0].Select("fkPostionId = " + hdnPositionID.Value);
                if (drpos.Length > 0)
                {
                    ITravelDays.FindControl<TextBox>("txtArrivalDate").Text = DateSaver.DispalyDate(General.ToStr(drpos[0]["fromDate"]), DateSaver.DateFormater.MMddyyyy, '/');
                    ITravelDays.FindControl<TextBox>("txtLeaveDate").Text = DateSaver.DispalyDate(General.ToStr(drpos[0]["toDate"]), DateSaver.DateFormater.MMddyyyy, '/');
                }
            }

            // Tech View
            List<DateTime> lstdates = new List<DateTime>();
            foreach (string strDate in travelDays)
            {
                lstdates.Add(DateSaver.GetDate(strDate));
            }
            ucCalender.LoadData(objShow.StartDate, objShow.EndDate, lstdates);
            txtRate.Text = General.ToStr(objShow.DayRateValue);

            // Publish
            chklstSelection.Items[1].Selected = objShow.IsAllAvailableTech;
            chklstSelection.Items[2].Selected = objShow.IsOnlyAccessTech;
            chklstSelection.Items[3].Selected = objShow.IsOnlyTechWithPassport;
            chklstSelection.Items[4].Selected = objShow.IsOnlyShareRoomTech;
            chklstSelection.Items[0].Selected = objShow.IsAllTech;


        }



        /// <summary>
        /// set index for display different view
        /// </summary>
        /// <param name="ActiveViewIndex"></param>
        private bool setIndex(int ActiveViewIndex)
        {
            spnNext.InnerText = "Next";
            ahBasicInfo.Attributes.Remove("class");
            ahPositionNeeds.Attributes.Remove("class");
            ahTravelDays.Attributes.Remove("class");
            ahTechView.Attributes.Remove("class");
            ahPublish.Attributes.Remove("class");
            aBiddingRates.Attributes.Remove("class");
            switch (ActiveViewIndex)
            {
                case -1:
                    #region case -1
                    ahBasicInfo.Attributes.Add("class", "active");
                    mlvShow.ActiveViewIndex = ActiveViewIndex + 1;
                    bindRepeater();
                    #endregion
                    break;
                case 0:
                    #region BasicInfo
                    Page.Validate("vwBasicInfo");
                    if (Page.IsValid)
                    {
                        int validShow = Show.CheckShowTitle(_ShowID, txtTitle.Text);
                        if (validShow == -2)
                        {
                            MessageBox.AddErrorMessage("Show title already exists");
                            ahBasicInfo.Attributes.Add("class", "active");
                            return false;
                        }
                        if (DateSaver.GetDate(txtStartDate.Text, DateSaver.DateFormater.MMddyyyy, '/') > DateSaver.GetDate(txtEndDate.Text, DateSaver.DateFormater.MMddyyyy, '/'))
                        {
                            MessageBox.AddErrorMessage("Show start date must be less than show end date");
                            ahBasicInfo.Attributes.Add("class", "active");
                            return false;
                        }
                        if (DateSaver.GetDate(txtBidStartdate.Text, DateSaver.DateFormater.MMddyyyy, '/') > DateSaver.GetDate(txtBidEndDate.Text, DateSaver.DateFormater.MMddyyyy, '/'))
                        {
                            MessageBox.AddErrorMessage("Bid start date must be less than bid end date");
                            ahBasicInfo.Attributes.Add("class", "active");
                            return false;
                        }
                        if (DateSaver.GetDate(txtBidStartdate.Text, DateSaver.DateFormater.MMddyyyy, '/') > DateSaver.GetDate(txtStartDate.Text, DateSaver.DateFormater.MMddyyyy, '/'))
                        {
                            MessageBox.AddErrorMessage("Bid start date must be less than show start date and show end date");
                            ahBasicInfo.Attributes.Add("class", "active");
                            return false;
                        }
                        if (DateSaver.GetDate(txtBidStartdate.Text, DateSaver.DateFormater.MMddyyyy, '/') > DateSaver.GetDate(txtEndDate.Text, DateSaver.DateFormater.MMddyyyy, '/'))
                        {
                            MessageBox.AddErrorMessage("Bid start date must be less than show end date");
                            ahBasicInfo.Attributes.Add("class", "active");
                            return false;
                        }
                        if (DateSaver.GetDate(txtBidEndDate.Text, DateSaver.DateFormater.MMddyyyy, '/') > DateSaver.GetDate(txtStartDate.Text, DateSaver.DateFormater.MMddyyyy, '/'))
                        {
                            MessageBox.AddErrorMessage("Bid end date must be less than show start date");
                            ahBasicInfo.Attributes.Add("class", "active");
                            return false;
                        }
                        if (_ShowID == 0)
                        {
                            if (DateSaver.GetDate(txtBidStartdate.Text, DateSaver.DateFormater.MMddyyyy, '/') < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00))
                            {
                                MessageBox.AddErrorMessage("Bid start date must be greater than or equal today");
                                ahBasicInfo.Attributes.Add("class", "active");
                                return false;
                            }
                        }
                        IEnumerable<string> ProjectManagersChecked = (from item in chklstProjectManagers.Items.Cast<ListItem>()
                                                                      where item.Selected
                                                                      select item.Value);
                        if (ProjectManagersChecked.Count() == 0)
                        {
                            MessageBox.AddErrorMessage("Please select atleast one project manager");
                            ahBasicInfo.Attributes.Add("class", "active");
                            return false;
                        }
                        objShow.ProjectManagers = string.Join<string>(",", ProjectManagersChecked);
                        objShow.FkCustomerID = General.GetInt(ddlCustomer.SelectedValue);
                        objShow.Title = txtTitle.Text;
                        objShow.NearestAirport = txtNearestAirport.Text;
                        objShow.City = txtCity.Text;
                        hdnsStartDate.Value = txtStartDate.Text;
                        ProjectSession.ShowStartDate = objShow.StartDate = DateSaver.GetDate(txtStartDate.Text, DateSaver.DateFormater.MMddyyyy, '/');
                        ProjectSession.ShowEndDate = objShow.EndDate = DateSaver.GetDate(txtEndDate.Text, DateSaver.DateFormater.MMddyyyy, '/');
                        objShow.BidStartDate = DateSaver.GetDate(txtBidStartdate.Text, DateSaver.DateFormater.MMddyyyy, '/');
                        objShow.BidEndDate = DateSaver.GetDate(txtBidEndDate.Text, DateSaver.DateFormater.MMddyyyy, '/');
                        objShow.Email = txtEmail.Text;
                        objShow.Description = txtDescription.Text;
                        hdnStartDate.Value = txtBidEndDate.Text;
                        List<DateTime> lstDates = new List<DateTime>();
                        if (ProjectSession.lstTravelDays != null && ProjectSession.lstTravelDays.Count > 0)
                        {
                            lstDates = ProjectSession.lstTravelDays.Select(s => DateSaver.GetDate(s, DateSaver.DateFormater.MMddyyyy, '/')).ToList();
                        }
                        List<DateTime> lstShowDays = DatetimeParser.DateRange(objShow.StartDate, objShow.EndDate);
                        int count = lstDates.Where(p => !lstShowDays.Any(p2 => p2.Date == p.Date)).Count();
                        if (count > 0)
                        {
                            ProjectSession.lstTravelDays = new List<string>();
                            //MessageBox.AddErrorMessage("Please Select Proper TravelDays");
                            //ahBasicInfo.Attributes.Add("class", "active");
                            //return false;
                        }



                        ucCalender.LoadData(objShow.StartDate, objShow.EndDate, lstDates);
                        if (fluLogoImage.HasFile)
                        {
                            string LogoPath = System.IO.Path.GetRandomFileName() + Path.GetExtension(fluLogoImage.FileName);
                            fluLogoImage.SaveAs(ProjectConfiguration.TechnicianPhotoBasePath + LogoPath);
                            objShow.LogoPath = LogoPath;
                        }
                        ahPositionNeeds.Attributes.Add("class", "active");
                        mlvShow.ActiveViewIndex = ActiveViewIndex + 1;
                    }
                    else
                    {
                        ahBasicInfo.Attributes.Add("class", "active");
                    }
                    return true;
                    #endregion
                    break;
                case 1:
                    #region Show Positions
                    List<_ShowPosition> objtmpShowPositions = new List<_ShowPosition>();
                    List<positionTravelDays> tmpobjpositionTravelDays = new List<positionTravelDays>();
                    foreach (DataListItem dlItem in dlPositions.Items)
                    {
                        CheckBox chkPosition = (CheckBox)dlItem.FindControl("chkPosition");
                        if (chkPosition.Checked)
                        {
                            _ShowPosition objShowposition = new _ShowPosition();
                            Label PositionName = dlItem.FindControl<Label>("lblPositionName");
                            HiddenField hdnPositionID = (HiddenField)dlItem.FindControl("hdnPositionID");
                            TextBox txtQuantity = (TextBox)dlItem.FindControl("txtQuantity");
                            if (string.IsNullOrEmpty(txtQuantity.Text))
                            {
                                MessageBox.AddErrorMessage("Please Insert Quantity for selected positions");
                                ahPositionNeeds.Attributes.Add("class", "active");
                                return false;
                            }
                            objShowposition.FKPositionID = General.GetInt(hdnPositionID.Value);
                            SelectedPositionID += objShowposition.FKPositionID + ",";
                            objShowposition.NoOfPosition = General.GetInt(txtQuantity.Text);
                            objShowposition.PositionName = PositionName.Text;
                            objtmpShowPositions.Add(objShowposition);
                            tmpobjpositionTravelDays.Add(new positionTravelDays()
                            {
                                FKPositionID = objShowposition.FKPositionID,
                                PositionName = PositionName.Text
                            });
                        }
                    }
                    if (tmpobjpositionTravelDays.Count == 0)
                    {
                        ahPositionNeeds.Attributes.Add("class", "active");
                        MessageBox.AddErrorMessage("Please Select positions");
                        return false;
                    }
                    else
                    {
                        if (objShowPositions == null || objShowPositions.Count == 0 || objShowPositions.Where(s => objtmpShowPositions.Where(o => o.FKPositionID == s.FKPositionID).Count() == 0).Count() > 0 || objShowPositions.Count != objtmpShowPositions.Count)
                        {
                            objShowPositions = new List<_ShowPosition>();
                            objShowPositions = objtmpShowPositions;
                            BindSelectedPositionDataListfoeMaxBidRate();
                        }
                        if (objpositionTravelDays == null || objpositionTravelDays.Count == 0 || objShowPositions.Where(s => objpositionTravelDays.Where(o => o.FKPositionID == s.FKPositionID).Count() == 0).Count() > 0 || objShowPositions.Count != objpositionTravelDays.Count)
                        {
                            objpositionTravelDays = new List<positionTravelDays>();
                            objpositionTravelDays = tmpobjpositionTravelDays;
                            BindSelectedPositionDataList();
                        }

                        aBiddingRates.Attributes.Add("class", "active");
                        mlvShow.ActiveViewIndex = ActiveViewIndex + 1;
                    }

                    return true;
                    #endregion
                    break;
                case 2:
                    if (objShowPositions == null || objShowPositions.Count == 0)
                    {
                        ahPositionNeeds.Attributes.Add("class", "active");
                        MessageBox.AddErrorMessage("Please Select positions");
                        return false;
                    }
                    else
                    {
                        if (chkMaximumRate.Checked)
                        {
                            dlMaxBidrates.Items.Cast<DataListItem>().Where(d => string.IsNullOrEmpty(d.FindControl<TextBox>("txtMaxBidRate").Text) == false).ToList()
                                .ForEach(
                                        f =>
                                            objShowPositions.Where(o => o.FKPositionID == General.GetInt(f.FindControl<HiddenField>("hdnPositionID").Value)).ToList().ForEach(r => r.Maxbidrate = General.GetInt(f.FindControl<TextBox>("txtMaxBidRate").Text))
                                        );
                        }
                        ahTravelDays.Attributes.Add("class", "active");
                        mlvShow.ActiveViewIndex = ActiveViewIndex + 1;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "setArrivalDates", "setArrivalDates();", true);
                        return true;
                    }

                    break;
                case 3:
                    #region Position Travel Dates
                    //if (chkAllPosition.Checked)
                    //{                   
                    ProjectSession.lstTravelDays = new List<string>();
                    if (!string.IsNullOrEmpty(txtArrivalDate.Text) && !string.IsNullOrEmpty(txtLeaveDate.Text))
                    {
                        DateTime StartDate = DateSaver.GetDate(txtArrivalDate.Text, DateSaver.DateFormater.MMddyyyy, '/');
                        DateTime EndDate = DateSaver.GetDate(txtLeaveDate.Text, DateSaver.DateFormater.MMddyyyy, '/');
                        //if (ProjectSession.ShowStartDate > StartDate)
                        //{
                        //    ProjectSession.lstTravelDays = new List<string>();
                        //    MessageBox.AddErrorMessage("Show positions arrival date can not be less than show start date");
                        //    return false;
                        //}
                        //if (ProjectSession.ShowEndDate < EndDate)
                        //{
                        //    ProjectSession.lstTravelDays = new List<string>();
                        //    MessageBox.AddErrorMessage("Show positions leave date can not be greater than show end date");
                        //    return false;
                        //}
                        if (EndDate < StartDate)
                        {
                            ProjectSession.lstTravelDays = new List<string>();
                            MessageBox.AddErrorMessage("Show positions leave date can not be greater than arrival date");
                            return false;
                        }
                        else
                            ProjectSession.lstTravelDays = DatetimeParser.DateRange(StartDate, EndDate).Select(s => DateSaver.DispalyDate(s, DateSaver.DateFormater.MMddyyyy, '/')).ToList();
                    }
                    //}
                    //else
                    //{
                    foreach (DataListItem dlItem in dlTravelDays.Items)
                    {
                        Label lblPositionName = dlItem.FindControl<Label>("lblPositionName");
                        HiddenField hdnPositionID = (HiddenField)dlItem.FindControl("hdnPositionID");
                        TextBox dtxtArrivalDate = (TextBox)dlItem.FindControl("txtArrivalDate");
                        TextBox dtxtLeaveDate = (TextBox)dlItem.FindControl("txtLeaveDate");
                        if (!string.IsNullOrEmpty(dtxtArrivalDate.Text) && !string.IsNullOrEmpty(dtxtLeaveDate.Text))
                        {
                            DateTime StartDate = DateSaver.GetDate(dtxtArrivalDate.Text, DateSaver.DateFormater.MMddyyyy, '/');
                            DateTime EndDate = DateSaver.GetDate(dtxtLeaveDate.Text, DateSaver.DateFormater.MMddyyyy, '/');
                            //if (ProjectSession.ShowStartDate > StartDate)
                            //{
                            //    ProjectSession.lstPositionTravelDays = new List<PositionTravelDays>();
                            //    MessageBox.AddErrorMessage(lblPositionName.Text + " position arrival date can not be less than show start date");
                            //    return false;
                            //}
                            //if (ProjectSession.ShowEndDate < EndDate)
                            //{
                            //    ProjectSession.lstPositionTravelDays = new List<PositionTravelDays>();
                            //    MessageBox.AddErrorMessage(lblPositionName.Text + " position leave date can not be less than show end date");
                            //    return false;
                            //}

                            if (EndDate < StartDate)
                            {
                                ProjectSession.lstPositionTravelDays = new List<PositionTravelDays>();
                                MessageBox.AddErrorMessage(lblPositionName.Text + " Show positions leave date can not be greater than arrival date");
                                return false;
                            }
                            ProjectSession.lstPositionTravelDays.Add(new PositionTravelDays()
                            {
                                PositionId = General.GetInt(hdnPositionID.Value),
                                pdate = DatetimeParser.DateRange(StartDate, EndDate).Select(s => DateSaver.DispalyDate(s, DateSaver.DateFormater.yyyyMMdd, '-')).ToList()
                            });
                        }
                        else if (!string.IsNullOrEmpty(dtxtArrivalDate.Text) || !string.IsNullOrEmpty(dtxtLeaveDate.Text))
                        {
                            if (string.IsNullOrEmpty(dtxtArrivalDate.Text))
                            {
                                MessageBox.AddErrorMessage("Please select arrival date for " + lblPositionName.Text + " position");
                            }
                            else
                            {
                                MessageBox.AddErrorMessage("Please select leave date for " + lblPositionName.Text + " position");
                            }
                            ProjectSession.lstPositionTravelDays = new List<PositionTravelDays>();
                            return false;
                        }


                        //}
                    }
                    List<DateTime> lstDates1 = new List<DateTime>();
                    if (ProjectSession.lstTravelDays != null && ProjectSession.lstTravelDays.Count > 0)
                    {
                        lstDates1 = ProjectSession.lstTravelDays.Select(s => DateSaver.GetDate(s)).ToList();
                    }
                    ucCalender.LoadData(objShow.StartDate, objShow.EndDate, lstDates1);
                    lblShowTitle.Text = objShow.Title;
                    lblShowYear.Text = objShow.StartDate.ToString("yyyy");
                    lblShowLocation.Text = objShow.City;
                    ltrShowDescription.InnerHtml = objShow.Description;
                    imgShowLogoPreview.Src = "~/Photo/" + objShow.LogoPath;
                    //lblImDocPath.Text = objShow.ImportantDocPath;                        
                    int days = (int)objShow.EndDate.Subtract(objShow.StartDate).TotalDays + 1;

                    spnTravelDays.InnerText = General.ToStr(lstDates1.Count);
                    spnShowDays.InnerText = General.ToStr(days - lstDates1.Count);

                    string showDates = string.Empty;
                    if (objShow.StartDate.Month == objShow.EndDate.Month)
                    {
                        showDates = objShow.StartDate.ToString("MMM") + " " + objShow.StartDate.ToString("dd") + "-" + objShow.EndDate.ToString("dd") + ", " + objShow.StartDate.ToString("yyyy");
                    }
                    else
                    {
                        showDates = objShow.StartDate.ToString("MMM") + " " + objShow.StartDate.ToString("dd") + "-" + objShow.EndDate.ToString("MMM") + " " + objShow.EndDate.ToString("dd") + ", " + objShow.StartDate.ToString("yyyy");
                    }
                    lblShowDates.Text = showDates;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClientScript", "countDown()", true);
                    ahTechView.Attributes.Add("class", "active");
                    mlvShow.ActiveViewIndex = ActiveViewIndex + 1;
                    //  return true;

                    // ahTravelDays.Attributes.Add("class", "active");
                    //  mlvShow.ActiveViewIndex = ActiveViewIndex + 1;
                    spnNext.InnerText = "Next";
                    return true;
                    #endregion
                    break;
                case 4:
                    #region Tech View
                    //Page.Validate("vldRate");
                    //if (Page.IsValid)
                    //{
                    objShow.DayRateValue = (float)General.GetDouble(txtRate.Text);
                    ahPublish.Attributes.Add("class", "active");
                    mlvShow.ActiveViewIndex = ActiveViewIndex + 1;
                    spnNext.InnerText = "Publish";

                    //}
                    //else
                    //{
                    //    ahTechView.Attributes.Add("class", "active");
                    //    return false;
                    //}                    
                    bindPublishData();
                    return true;
                    #endregion
                    break;
                case 5:
                    #region Publish
                    spnNext.InnerText = "Publish";
                    int selectedItems = chklstSelection.Items.Cast<ListItem>().Where(x => x.Selected).Count();
                    if (selectedItems == 0)
                    {
                        ahPublish.Attributes.Add("class", "active");
                        MessageBox.AddErrorMessage("Please select techs to publish");
                        return false;
                    }
                    objShow.TravelDays = string.Join("&", ProjectSession.lstTravelDays.ToArray());
                    objShow.IsAllAvailableTech = chklstSelection.Items[1].Selected;
                    objShow.IsOnlyAccessTech = chklstSelection.Items[2].Selected;
                    objShow.IsOnlyTechWithPassport = chklstSelection.Items[3].Selected;
                    objShow.IsOnlyShareRoomTech = chklstSelection.Items[4].Selected;
                    objShow.IsAllTech = chklstSelection.Items[0].Selected;

                    objShow.IsPublished = true;
                    objShow.IsActive = true;
                    objShow.ImportantDocPath = string.Join("&", objShowImportantDocs.ToArray());
                    DataTable dtShowPositions = Methods.ToDataTable(objShowPositions);
                    dtShowPositions.Columns.Remove("PKShowPositionID");
                    dtShowPositions.Columns.Remove("FKShowID");
                    dtShowPositions.Columns.Remove("PositionName");
                    if (ProjectSession.lstPositionTravelDays == null || ProjectSession.lstPositionTravelDays.Count == 0)
                        ProjectSession.lstPositionTravelDays = new List<PositionTravelDays>();
                    DataTable PositionWiseTravelDates = ProjectSession.lstPositionTravelDays.Select(s => new { PositionId = s.PositionId, pdate = string.Join("&", s.pdate.ToArray()) }).ToList().ToDataTable();
                    int returnValue = 0;
                    if (_ShowID > 0)
                    {
                        returnValue = objShow.Update(dtShowPositions, PositionWiseTravelDates);
                    }
                    else
                    {
                        objShow.PublishedDate = DateTime.Now;
                        emailTemplate = new EmailTemplate((int)EmailTemplates.PublishShowEmailTemplate);
                        returnValue = objShow.Insert(dtShowPositions, PositionWiseTravelDates);

                    }
                    if (returnValue == -2)
                    {
                        MessageBox.AddErrorMessage("Show Title already exists");
                    }
                    else
                    {
                        if (_ShowID > 0)
                        {
                            MessageBox.AddSuccessMessage("Show updated successfully");
                        }
                        else
                        {
                            if (emailTemplate.IsOn)
                                ShowQualifiedTechs(returnValue, chkByLocation.Checked ? selectedLocations.Value : string.Empty, chkByTechs.Checked ? selectedTechs.Value : string.Empty, chkByRating.Checked ? txtPrompt.Text : string.Empty);
                            MessageBox.AddSuccessMessage("Show published successfully");
                            
                            _ShowID = returnValue;
                            aConfirmedTechs.Visible = true;
                        }

                    }
                    ahPublish.Attributes.Add("class", "active");
                    return true;
                    #endregion
                    break;
                default:
                    Response.Redirect(this.ResolveClientUrl("BidListing.aspx"));
                    break;
            }
            return false;
        }
        public void bindPublishData()
        {
            if (GetShowPublishData == null || GetShowPublishData.Tables.Count < 1)
            {
                GetShowPublishData = Show.GetShowPublishData();
                ddlByLocations.DataSource = GetShowPublishData.Tables[0];
                ddlByLocations.DataValueField = "City";
                ddlByLocations.DataTextField = "City";
                ddlByLocations.DataBind();
                ddlByTechs.DataSource = GetShowPublishData.Tables[1];
                ddlByTechs.DataValueField = "PKTechnicianID";
                ddlByTechs.DataTextField = "Name";
                ddlByTechs.DataBind();
            }
            var locationarray = General.ToStr(selectedLocations.Value).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            ddlByLocations.Items.Cast<ListItem>().Where(w => locationarray.Contains(w.Value)).ToList().ForEach(s => s.Attributes["Selected"] = "selected");
            var peoplesarray = General.ToStr(selectedTechs.Value).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            ddlByTechs.Items.Cast<ListItem>().Where(w => peoplesarray.Contains(w.Value)).ToList().ForEach(s => s.Attributes["Selected"] = "selected");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SetChoosen", "setChoosen();", true);
        }
        private void ShowQualifiedTechs(int ShowID, string Locations, string Peoples, string Rating)
        {
            DataSet ds = Show.ShowQualifiedQuaTechs(ShowID, Locations, Peoples, Rating);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dtdr in dt.Rows)
            {
                string FirstName = General.ToStr(dtdr["FirstName"]);
                string LastName = General.ToStr(dtdr["LastName"]);
                string Email = General.ToStr(dtdr["Email"]);
                string title = General.ToStr(dtdr["title"]);
                string bidStartdate = DateSaver.DispalyDate(Convert.ToDateTime(dtdr["BidStartDate"]), DateSaver.DateFormater.MMddyyyy, '/');
                string showStartdate = DateSaver.DispalyDate(Convert.ToDateTime(dtdr["ShowStartDate"]), DateSaver.DateFormater.MMddyyyy, '/');
                SentLoginDetailEmail(FirstName, LastName, Email, title, bidStartdate, showStartdate);
            }
        }
        private void clearActive()
        {
            spnNext.InnerText = "Next";
            ahBasicInfo.Attributes.Remove("class");
            ahPositionNeeds.Attributes.Remove("class");
            ahTravelDays.Attributes.Remove("class");
            ahTechView.Attributes.Remove("class");
            ahPublish.Attributes.Remove("class");
            aBiddingRates.Attributes.Remove("class");
            aConfirmedTechs.Attributes.Remove("class");
            pnlShow.Visible = true;
            pnlConfirmedTechs.Visible = false;
        }



        private void SentLoginDetailEmail(string FirstName, string LastName, string EmailAddress, string title, string bidStartdate, string showStartdate)
        {
            #region File Read Code

            string MailBody = string.Empty;

            System.Text.StringBuilder stringBuilderText = new System.Text.StringBuilder();
            string mailContent = null;
            //mailContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/") + "Templates\\WelcomeMessage.html");
            if (emailTemplate == null)
                emailTemplate = new EmailTemplate((int)EmailTemplates.PublishShowEmailTemplate);

            mailContent = emailTemplate.Body.Replace("\n", "<br />").Replace("\r", "");


            string MailSubject = emailTemplate.Subject;

            mailContent = General.ToStr(mailContent.Replace("[@@FirstName]", FirstName));
            mailContent = General.ToStr(mailContent.Replace("[@@LastName]", LastName));

            mailContent = General.ToStr(mailContent.Replace("[@@BidStartDate]", bidStartdate));
            mailContent = General.ToStr(mailContent.Replace("[@@ShowStartDate]", showStartdate));
            //mailContent = General.ToStr(mailContent.Replace("[@@Email]", Email));
            mailContent = General.ToStr(mailContent.Replace("[@@title]", title));
            StringBuilder stringBuilderCheckList = new StringBuilder();
            stringBuilderText.Append(mailContent);
            MailBody = stringBuilderText.ToString();
            #endregion

            //string MailSubject = "Thank You For Registering With Tech Track";
            //EmailUtillity.SendMail(EmailAddress.Trim(), MailSubject, MailBody);
            try
            {
                //Email.Send(ProjectConfiguration.FromEmail, objTechnician.Email, ProjectConfiguration.CC, ProjectConfiguration.BCC, MailSubject, MailBody, string.Empty, true, Email.EmailType.Default);
                Email.Send(emailTemplate.EmailFrom, EmailAddress, emailTemplate.CCEmail, ProjectConfiguration.BCC, MailSubject, MailBody, string.Empty, true, Email.EmailType.Default);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// binds both repeater for Documents
        /// </summary>
        private void bindRepeater()
        {
            rptImportantDoc.DataSource = objShowImportantDocs;
            rptImportantDoc.DataBind();
            rptViewImportantDoc.DataSource = objShowImportantDocs;
            rptViewImportantDoc.DataBind();
            pnlImportantDoc.Visible = false;
            lblNoRecordsTechView.Visible = true;
            if (objShowImportantDocs.Count > 0)
            {
                lblNoRecordsTechView.Visible = false;
                pnlImportantDoc.Visible = true;
            }
        }

        private void BindSelectedPositionDataList()
        {
            dlTravelDays.DataSource = objpositionTravelDays;//ShowPosition.GetPositionName(SelectedPositionID);
            dlTravelDays.DataBind();
        }
        private void BindSelectedPositionDataListfoeMaxBidRate()
        {
            dlMaxBidrates.DataSource = objShowPositions;//ShowPosition.GetPositionName(SelectedPositionID);
            dlMaxBidrates.DataBind();
        }
        #endregion

        #region ControlEvents

        protected void lnkEmail_Click(object sender, EventArgs e)
        {
            ProjectSession.ToSelectedGroups = "Groups : " + string.Join(",", chklstSelection.Items.Cast<ListItem>().Where(li => li.Selected == true).Select(s => s.Text).ToArray());
            if (chkByLocation.Checked)
                ProjectSession.ToSelectedGroups += "\n\n" + "Locations : " + selectedLocations.Value;
            if (chkByTechs.Checked)
                ProjectSession.ToSelectedGroups += "\n\n" + "Peoples : " + selectedTechsNames.Value;
            if (chkByRating.Checked)
                ProjectSession.ToSelectedGroups += "\n\n" + "Rating at or above : " + txtPrompt.Text;
            //ProjectSession.ToSelectedRating = lblPrompt.Text;
            //ProjectSession.ToSelectedPeoples = selectedTechsNames.Value;
            //ProjectSession.ToSelectedLocations = selectedLocations.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openPopup", "TravelDaysPopup('PopupShowEmailTemplate.aspx', 'Show Email Template',500,750);", true);
            //lnkEmail.Attributes["href"] = "javascript:void";
            //lnkEmail.Attributes["onclick"] = "return ";
        }

        protected void chkAllPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPosition.Checked)
            {
                dlTravelDays.Visible = false;
                tabAllSelected.Visible = true;
            }
            else
            {
                dlTravelDays.Visible = true;
                tabAllSelected.Visible = false;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "setArrivalDates", "setArrivalDates();", true);
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            if (fromDashboard)
                Response.Redirect("Dashboard.aspx");
            else
                Response.Redirect("ShowListing.aspx");
        }
        protected void btnTravelDaysBinder_Click(object sender, EventArgs e)
        {
            string travelDays = string.Empty;
            if (ProjectSession.lstTravelDays != null && ProjectSession.lstTravelDays.Count > 0)
            {
                int count = 0;
                foreach (string strDate in ProjectSession.lstTravelDays)
                {
                    if (count == 0)
                    {
                        travelDays += strDate;
                    }
                    else if (count == 1)
                    {
                        count = -1;
                        travelDays += " To " + strDate + "<br/>";
                    }
                    count++;
                }
            }
            trTravelDays.Visible = false;
            if (travelDays.Length > 0)
            {
                trTravelDays.Visible = true;
            }
            ltrTravelDays.Text = travelDays;

        }

        protected void lnkTravelDays_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartDate.Text) || string.IsNullOrEmpty(txtEndDate.Text))
            {
                MessageBox.AddErrorMessage("Please enter start date and end date first");
                return;
            }
            if (DateSaver.GetDate(txtStartDate.Text, DateSaver.DateFormater.MMddyyyy, '/') > DateSaver.GetDate(txtEndDate.Text, DateSaver.DateFormater.MMddyyyy, '/'))
            {
                MessageBox.AddErrorMessage("Show Start date must be greater than Show end date");
                return;
            }
            ProjectSession.ShowStartDate = DateSaver.GetDate(txtStartDate.Text, DateSaver.DateFormater.MMddyyyy, '/');
            ProjectSession.ShowEndDate = DateSaver.GetDate(txtEndDate.Text, DateSaver.DateFormater.MMddyyyy, '/');

            string script = "TravelDaysPopup('PopupTravaleDays.aspx?" + base.SetQueryString("ShowID", General.ToStr(_ShowID)) + "','Select Travel Days','400','450');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClientScript", script, true);
        }

        protected void lnkRemoveImage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(objShow.LogoPath))
            {
                if (File.Exists(ProjectConfiguration.TechnicianPhotoBasePath + objShow.LogoPath))
                {
                    File.Delete(ProjectConfiguration.TechnicianPhotoBasePath + objShow.LogoPath);
                }
                objShow.LogoPath = string.Empty;
                imgLogoImage.Src = "";
            }
            lnkRemoveImage.Visible = false;
            btnAddLogo.Visible = true;
        }

        protected void rptImportantDoc_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                if (File.Exists(ProjectConfiguration.TechnicianResumeBasePath + General.ToStr(e.CommandArgument)))
                {
                    File.Delete(ProjectConfiguration.TechnicianResumeBasePath + General.ToStr(e.CommandArgument));
                }
                objShowImportantDocs.RemoveAll(x => x.Contains(General.ToStr(e.CommandArgument)));
                bindRepeater();
            }
        }

        protected void btnAddLogo_Click(object sender, EventArgs e)
        {
            Page.Validate("vwLogo");
            if (Page.IsValid)
            {
                if (fluLogoImage.HasFile)
                {
                    string LogoPath = System.IO.Path.GetRandomFileName() + Path.GetExtension(fluLogoImage.FileName);
                    fluLogoImage.SaveAs(ProjectConfiguration.TechnicianPhotoBasePath + LogoPath);
                    objShow.LogoPath = LogoPath;
                    imgLogoImage.Src = "~/Photo/" + objShow.LogoPath;
                    lnkRemoveImage.Visible = true;
                    btnAddLogo.Visible = false;
                }
            }
        }
        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            Page.Validate("vwDoc");
            if (Page.IsValid)
            {
                if (fluImportantDoc.HasFile)
                {
                    string ImportantDocPath = System.IO.Path.GetRandomFileName() + Path.GetExtension(fluImportantDoc.FileName);
                    fluImportantDoc.SaveAs(ProjectConfiguration.TechnicianResumeBasePath + ImportantDocPath);
                    objShowImportantDocs.Add(ImportantDocPath);
                    bindRepeater();
                    //objShow.ImportantDocPath = ImportantDocPath;
                }
            }
        }
        protected void aNext_Click(object sender, EventArgs e)
        {
            int ActiveViewIndex = mlvShow.ActiveViewIndex;
            bool valid = setIndex(ActiveViewIndex);
            if (ActiveViewIndex == 3 && !valid)
            {
                ahTravelDays.Attributes.Add("class", "active");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "setArrivalDates", "setArrivalDates();", true);
            }
        }

        protected void ahBasicInfo_Click(object sender, EventArgs e)
        {
            clearActive();
            ahBasicInfo.Attributes["class"] = "active";
            mlvShow.ActiveViewIndex = 0;
            setIndex(-1);
        }

        protected void aBiddingRates_Click(object sender, EventArgs e)
        {
            clearActive();
            bool valid = true;
            if (mlvShow.ActiveViewIndex != 3 && mlvShow.ActiveViewIndex != 5)
            {
                valid = setIndex(mlvShow.ActiveViewIndex);
            }
            if (valid)
            {
                if (validateFT())
                {
                    mlvShow.ActiveViewIndex = 2;
                    if (!setIndex(1))
                    {
                        mlvShow.ActiveViewIndex = 1;
                    }
                }
            }
        }
        protected void ahPositionNeeds_Click(object sender, EventArgs e)
        {
            clearActive();
            bool valid = true;
            if (mlvShow.ActiveViewIndex != 3 && mlvShow.ActiveViewIndex != 5 && mlvShow.ActiveViewIndex != 4)
            {
                valid = setIndex(mlvShow.ActiveViewIndex);
            }
            if (valid)
            {
                Page.Validate("vwBasicInfo");
                if (Page.IsValid)
                {
                    mlvShow.ActiveViewIndex = 1;
                    if (!setIndex(0))
                    {
                        mlvShow.ActiveViewIndex = 0;
                    }
                }
                else
                {
                    MessageBox.AddErrorMessage("Please insert basic Info properly");
                    mlvShow.ActiveViewIndex = 0;
                    setIndex(-1);
                    return;
                }
            }
        }
        protected void ahTravelDays_Click(object sender, EventArgs e)
        {
            clearActive();
            bool valid = true;
            if (mlvShow.ActiveViewIndex != 3 && mlvShow.ActiveViewIndex != 5)
            {
                valid = setIndex(mlvShow.ActiveViewIndex);
            }
            if (valid)
            {
                if (validateFT())
                {
                    mlvShow.ActiveViewIndex = 3;
                    if (!setIndex(2))
                    {
                        mlvShow.ActiveViewIndex = 2;
                    }
                }
            }
        }
        protected void ahTechView_Click(object sender, EventArgs e)
        {
            clearActive();
            bool valid = true;
            if (mlvShow.ActiveViewIndex != 3 && mlvShow.ActiveViewIndex != 5)
            {
                valid = setIndex(mlvShow.ActiveViewIndex);
            }
            if (valid)
            {
                if (validateFT())
                {
                    mlvShow.ActiveViewIndex = 4;
                    if (!setIndex(3))
                    {
                        mlvShow.ActiveViewIndex = 3;
                    }
                }
            }
        }
        private bool validateFT()
        {
            Page.Validate("vwBasicInfo");
            if (!Page.IsValid)
            {
                MessageBox.AddErrorMessage("Please insert basic Info properly");
                mlvShow.ActiveViewIndex = 0;
                setIndex(-1);
                return false;
            }
            bool isValid = true;
            int checkedCount = 0;


            foreach (DataListItem dlItem in dlPositions.Items)
            {
                CheckBox chkPosition = (CheckBox)dlItem.FindControl("chkPosition");
                if (chkPosition.Checked)
                {
                    checkedCount++;
                    TextBox txtQuantity = (TextBox)dlItem.FindControl("txtQuantity");
                    if (string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        MessageBox.AddErrorMessage("Please Insert Quantity for selected positions");
                        ahPositionNeeds.Attributes.Add("class", "active");
                        mlvShow.ActiveViewIndex = 1;
                        isValid = false;
                        return false;
                    }
                }
            }
            if (checkedCount > 0 && isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void ahPublish_Click(object sender, EventArgs e)
        {
            clearActive();
            bool valid = true;
            if (mlvShow.ActiveViewIndex != 3 && mlvShow.ActiveViewIndex != 5)
            {
                valid = setIndex(mlvShow.ActiveViewIndex);
            }
            if (valid)
            {
                if (validateFT())
                {
                    mlvShow.ActiveViewIndex = 5;
                    setIndex(4);
                }
                else
                {
                    mlvShow.ActiveViewIndex = 0;
                    setIndex(-1);
                    return;
                }
            }
        }

        #endregion

        #region Confirmed Techs Events
        protected void aConfirmedTechs_Click(object sender, EventArgs e)
        {
            clearActive();
            pnlConfirmedTechs.Visible = true;
            pnlShow.Visible = false;
            aConfirmedTechs.Attributes["class"] = "active";
        }
        private void BindConfirmedTechs()
        {
            if (ConfirmedTechs == null)
                ConfirmedTechs = Show.GetConfirmedTechsByShow(_ShowID);
            DataView dv = ConfirmedTechs.Tables[0].DefaultView;
            dv.Sort = base.SortBy + " " + base.OrderBy;
            gvConfirmedTechs.DataSource = dv.ToTable();
            gvConfirmedTechs.DataBind();

        }
        protected void btnConfirmedTechs_Click(object sender, EventArgs e)
        {
            ConfirmedTechs = null;
            BindConfirmedTechs(); 
        }
        protected void gvConfirmedTechs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "remove")
            {
                Bid.Delete(General.GetInt(e.CommandArgument));
                MessageBox.AddSuccessMessage("Technician removed successfully");
                ConfirmedTechs = null;
                BindConfirmedTechs();
            }
            else if (e.CommandName.ToLower() == "sort")
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
                BindConfirmedTechs();
            }
        }
        protected void gvConfirmedTechs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header && base.SortBy != null)
            {
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

                    case "Location":
                        PlaceHolder pcLocation = (PlaceHolder)e.Row.FindControl("pcLocation");
                        pcLocation.Controls.Add(ImgSort);
                        break;

                    case "Position":
                        PlaceHolder pcPosition = (PlaceHolder)e.Row.FindControl("pcPosition");
                        pcPosition.Controls.Add(ImgSort);
                        break;

                    case "BidRate":
                        PlaceHolder pcBidRate = (PlaceHolder)e.Row.FindControl("pcBid");
                        pcBidRate.Controls.Add(ImgSort);
                        break;

                    case "Rating":
                        PlaceHolder pcRating = (PlaceHolder)e.Row.FindControl("pcRating");
                        pcRating.Controls.Add(ImgSort);
                        break;

                    case "Rank":
                        PlaceHolder pcRank = (PlaceHolder)e.Row.FindControl("pcRank");
                        pcRank.Controls.Add(ImgSort);
                        break;

                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkUpdateBidRate = (LinkButton)e.Row.FindControl("lnkUpdateBidRate");
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (lnkUpdateBidRate != null)
                {
                    string makeBidScript = "TravelDaysPopup('popupChangeHourlyRate.aspx?" + base.SetQueryString("PKBidID", General.ToStr(drv["PKBidID"])) + "','Change Hourly Rate','250','300'); return false;";
                    lnkUpdateBidRate.OnClientClick = makeBidScript;
                }
                Label lblRating = (Label)e.Row.FindControl("lblRating");
                lblRating.Text = ConvertTo.Float(DataBinder.Eval(e.Row.DataItem, "Rating")).ToString("F2");
            }
        }
        #endregion
    }
}