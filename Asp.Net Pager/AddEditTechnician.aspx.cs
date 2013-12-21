using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechTracking.Classes;
using TechTracking.UserControls;
using TechTrackingDAL;
namespace TechTracking.Admin
{
    public partial class AddEditTechnician : BaseAdminPage
    {

        #region Properties
        public TechTrackingDAL.Technician objTechnician
        {
            get
            {
                return (TechTrackingDAL.Technician)ViewState["objTechnician"];
            }
            set
            {
                ViewState["objTechnician"] = value;
            }
        }
        public string strCheckedPositions
        {
            get
            {
                return General.ToStr(ViewState["CheckedPositions"]);
            }
            set
            {
                ViewState["CheckedPositions"] = value;
            }
        }
        public int TechnicianID
        {
            get { return (Request.QueryString["TechnicianID"] != null ? General.GetInt(base.GetQueryString("TechnicianID")) : 0); }
        }
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
        public bool _IsChecked
        {
            get
            {
                return ConvertTo.Boolean(ViewState["_IsChecked"]);
            }
            set
            {
                ViewState["_IsChecked"] = value;
            }
        }
        private DataTable dtPositions
        {
            get
            {
                if (ViewState["dtPositions"] != null)
                    return (DataTable)ViewState["dtPositions"];
                else
                    return null;
            }
            set
            {
                ViewState["dtPositions"] = value;
            }
        }
        public List<TechnicianReference> objTechnicianReferences
        {
            get
            {
                return (List<TechnicianReference>)ViewState["objTechnicianReferences"];
            }
            set
            {
                ViewState["objTechnicianReferences"] = value;
            }
        }
        public int ucCount
        {
            get
            {
                return ViewState["ucCount"] != null ? Convert.ToInt16(ViewState["ucCount"]) : 2;
            }
            set
            {
                ViewState["ucCount"] = value;
            }
        }
        public int ucLoadCount
        {
            get
            {
                return ViewState["ucLoadCount"] != null ? Convert.ToInt16(ViewState["ucLoadCount"]) : 1;
            }
            set
            {
                ViewState["ucLoadCount"] = value;
            }
        }
        #endregion

        #region PageEvents
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                objTechnicianReferences = new List<TechnicianReference>();
                ucCount = 2;
                ucLoadCount = 1;
                _TechnicianID = TechnicianID;
                LinkButton lnkAddTechnician = (LinkButton)this.Master.FindControl("lnkTechnician");
                if (lnkAddTechnician != null)
                {
                    lnkAddTechnician.CssClass = "nav_link active";
                }
                bindPositions();
                if (_TechnicianID > 0)
                {
                    //lblHeaderLabel.Text = "Edit Technician";

                    LoadData();
                    objTechnician = new TechTrackingDAL.Technician(_TechnicianID);
                    btnDelete.Visible = true;
                    btnShare.Visible = true;
                    btnTable.Visible = true;
                    btnShare.Visible = true;
                    btnShare.Attributes.Add("onclick", "javascript:return opPopup('PopupShare.aspx?" + SetQueryString("TechnicianID", General.ToStr(_TechnicianID)) + "','400','400','Share');");
                }
                else
                {
                    objTechnician = new TechTrackingDAL.Technician();
                    chkApprove.Checked = true;
                    btnSaveAndEmail.Visible = false;

                }
            }
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

        #region Methods
        private bool ValidateAirports()
        {
            if (string.IsNullOrEmpty(txtNearestAirport.Text))
            {
                txtNearestMajorCity.Text = "";
                return true;
            }
            if (!txtNearestAirport.Text.Contains("_"))
            {
                MessageBox.AddErrorMessage("Nearest airport must contain AirportCode and AirportName");
                return false;
            }
            string AirportCode, AirportName, City = string.Empty;
            string[] Airport = txtNearestAirport.Text.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (Airport.Length == 2)
            {
                if (Airport[0].Length != 3)
                {
                    MessageBox.AddErrorMessage("AirportCode must be three characters");
                    return false;
                }
                AirportCode = Airport[0];
                AirportName = Airport[1];
            }
            else
            {
                MessageBox.AddErrorMessage("Nearest airport must contain AirportCode and AirportName");
                return false;
            }
            City = txtNearestMajorCity.Text.Trim();
            if (!TechTrackingDAL.Technician.CheckAirportAndCity(AirportName, City, AirportCode))
            {
                MessageBox.AddErrorMessage("Airport information mismatched");
                return false;
            }
            objTechnician.NearestAirport = txtNearestAirport.Text.Trim();
            objTechnician.NearestMajorCity = txtNearestMajorCity.Text.Trim();
            return true;
        }
        /// <summary>
        /// Server side validate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public void checkdate(object source, ServerValidateEventArgs args)
        {
            DateTime systemtime = System.DateTime.Now;
            DateTime birthdate = Convert.ToDateTime(txtBirthdate.Text);

            if (birthdate >= systemtime)
            {
                args.IsValid = false;
                MessageBox.AddErrorMessage("Birth Date Must Be Less Than Todays Date");
            }
            else
            {
                args.IsValid = true;
            }

        }

        /// <summary>
        /// Load data from Database
        /// </summary>
        private void LoadData()
        {
            DataSet ds = TechTrackingDAL.Technician.Select(_TechnicianID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                lblHeaderLabel.Text = General.ToStr(dt.Rows[0]["FirstName"]) + " " + General.ToStr(dt.Rows[0]["LastName"]);
                ProjectSession.TechName = General.ToStr(dt.Rows[0]["FirstName"]) + " " + General.ToStr(dt.Rows[0]["LastName"]);
                txtName.Text = General.ToStr(dt.Rows[0]["FirstName"]);
                txtNearestMajorCity.Text = General.ToStr(dt.Rows[0]["NearestMajorCity"]);
                txtLastName.Text = General.ToStr(dt.Rows[0]["LastName"]);
                txtNearestAirport.Text = General.ToStr(dt.Rows[0]["NearestAirport"]);
                txtUsername.Text = General.ToStr(dt.Rows[0]["UserName"]);
                txtPassword.Attributes["value"] = EncryptionDecryption.GetDecrypt(General.ToStr(dt.Rows[0]["Password"]));
                txtEmail.Text = General.ToStr(dt.Rows[0]["Email"]);
                txtCityOfLocation.Text = General.ToStr(dt.Rows[0]["City"]);
                txtSSN.Text = General.ToStr(dt.Rows[0]["SSN"]);
                chkApprove.Checked = _IsChecked = General.GetBoolean(General.ToStr(dt.Rows[0]["IsApproved"]));
                txtAddressLine1.Text = General.ToStr(dt.Rows[0]["Address"]);
                if (!string.IsNullOrEmpty(General.ToStr(dt.Rows[0]["HomeAddress"])))
                {
                    txtAddressLine2.Text = General.ToStr(dt.Rows[0]["HomeAddress"]);
                    //try
                    //{
                    //    string[] Address = General.ToStr(dt.Rows[0]["HomeAddress"]).Split(new string[] { "||||" }, StringSplitOptions.None);
                    //    txtAddressLine1.Text = Address[0];
                    //    txtAddressLine2.Text = Address.Length > 1 ? Address[1] : null;
                    //}
                    //catch (Exception)
                    //{

                    //}
                }
                if (dt.Rows[0]["BirthDate"] != DBNull.Value)
                    txtBirthdate.Text = ConvertTo.Date(dt.Rows[0]["BirthDate"]).ToString("MM/dd/yyyy");

                if (General.GetBoolean(General.ToStr(dt.Rows[0]["IsEmailSent"])))
                {
                    hdnIsEmailSent.Value = "1";
                    btnSaveAndEmail.Visible = true;
                }
                else
                {
                    hdnIsEmailSent.Value = "0";
                    btnSaveAndEmail.Visible = false;
                }

                if (!string.IsNullOrEmpty(General.ToStr(dt.Rows[0]["PhotoPath"])))
                {
                    a_ClickToEnlarge.Visible = true;
                    imgUpload.ImageUrl = "~/Photo/" + General.ToStr(dt.Rows[0]["PhotoPath"]);
                }
                else
                {
                    a_ClickToEnlarge.Visible = false;
                }

                if (!string.IsNullOrEmpty(General.ToStr(dt.Rows[0]["ResumePath"])))
                {
                    a_download.Visible = true;
                    a_download.HRef = "~/Download.aspx?ATTACH=YES&Path=" + General.ToStr(dt.Rows[0]["ResumePath"]);
                }
                else
                {
                    a_download.Visible = false;
                }
                if (dt.Rows[0]["OvationRef"] != DBNull.Value && !string.IsNullOrEmpty(General.ToStr(dt.Rows[0]["OvationRef"])))
                {
                    int[] ovationRef = General.ToStr(dt.Rows[0]["OvationRef"]).Split(',').Select(i => int.Parse(i)).ToArray();
                    foreach (int ovation in ovationRef)
                    {
                        if (ovation == Convert.ToInt32(Enums.Ovations.FaceBook))
                            chkbFaceBook.Checked = true;
                        else if (ovation == Convert.ToInt32(Enums.Ovations.Linkedin))
                            chkbLinkedin.Checked = true;
                        else if (ovation == Convert.ToInt32(Enums.Ovations.Friend))
                            chkbFriend.Checked = true;
                        else if (ovation == Convert.ToInt32(Enums.Ovations.Recruiter))
                            chkbRecruiter.Checked = true;

                    }
                }

                rdbShirtSize.SelectedValue = General.ToStr(dt.Rows[0]["ShirtSize"]);
                rdblstGender.SelectedValue = General.GetBoolean(General.ToStr(dt.Rows[0]["Gender"])) == true ? "1" : "0";
                txtState.Text = General.ToStr(dt.Rows[0]["State"]);
                txtZipCode.Text = General.ToStr(dt.Rows[0]["ZipCode"]);
                txtCellPhone.Text = General.ToStr(dt.Rows[0]["Phone1"]);
                txtNickName.Text = General.ToStr(dt.Rows[0]["NickName"]);
                aMailTO.HRef = "mailto:" + General.ToStr(dt.Rows[0]["Email"]);
                if (dt.Rows[0]["LinkedInURL"] != DBNull.Value && !string.IsNullOrEmpty(General.ToStr(dt.Rows[0]["LinkedInURL"])))
                {
                    aLinkdeInURL.HRef = General.ToStr(dt.Rows[0]["LinkedInURL"]);
                    aLinkdeInURL.Visible = true;
                }


                if (General.GetBoolean(General.ToStr(dt.Rows[0]["ishotelshare"])))
                {
                    chkbSharehotelroom.Checked = true;
                }
                if (General.GetBoolean(General.ToStr(dt.Rows[0]["isAccess"])))
                {
                    chkbAccessTech.Checked = true;
                }
                if (General.GetBoolean(General.ToStr(dt.Rows[0]["hasPassport"])))
                {
                    chkbPassport.Checked = true;
                }
                if (General.GetBoolean(General.ToStr(dt.Rows[0]["isInterviewed"])))
                {
                    chkbInterview.Checked = true;
                }
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    chklstApprovedPositions.Items.FindByValue(General.ToStr(dr["PKPositionID"])).Selected = true;
                }
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    chklstAppliedPositions.Items.FindByValue(General.ToStr(dr["PKPositionID"])).Selected = true;
                }
                if (ds.Tables.Count > 3)
                {
                    objTechnicianReferences = ds.Tables[3].AsEnumerable().Select(s => new TechnicianReference
                        {
                            FKTechnicianID = s.Field<int>("FKTechnicianID"),
                            PKTechnicianReferenceID = s.Field<int>("PKTechnicianReferenceID"),
                            FirstName = s.Field<string>("FirstName"),
                            LastName = s.Field<string>("LastName"),
                            Email = s.Field<string>("Email"),
                            PhoneNumber = s.Field<string>("PhoneNumber")
                        }).ToList();
                    objTechnicianReferences.ForEach(TechRef => LoadTechnicianReference(ucLoadCount++, TechRef));
                    if (ucLoadCount > 4) lnkAddReference.Visible = false;
                }
            }
        }
        /// <summary>
        /// manage user controls visibility
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="TechRef"></param>
        private void LoadTechnicianReference(int Index, TechnicianReference TechRef)
        {
            ucCount = Index > 2 ? Index : 2;
            switch (Index)
            {
                case 1:
                    TReference1.LoadData(TechRef);
                    break;
                case 2:
                    TReference2.LoadData(TechRef);
                    break;
                case 3:
                    TReference3.Visible = true;
                    TReference3.LoadData(TechRef);
                    break;
                case 4:
                    TReference4.Visible = true;
                    TReference4.LoadData(TechRef);
                    break;
                case 5:
                    TReference5.Visible = true;
                    TReference5.LoadData(TechRef);
                    break;
            }

        }

        /// <summary>
        /// bind checkboxlist with positions
        /// </summary>
        private void bindPositions()
        {
            DataSet ds = Position.SelectTechPositions(_TechnicianID);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtAppliedPositions = ds.Tables[0];//.Select("(StatusID = 0) or (StatusID = 3)").CopyToDataTable();
                DataTable dtApprovedPositions = new DataTable();
                if (_TechnicianID == 0)
                    dtApprovedPositions = dtAppliedPositions;
                else
                {
                    if (ds.Tables[0].Select("(StatusID = 1) or (StatusID = 0)").ToList().Count > 0)
                        dtApprovedPositions = ds.Tables[0].Select("(StatusID = 1) or (StatusID = 0)").CopyToDataTable();
                    else
                        dtApprovedPositions = ds.Tables[0].Clone();
                }

                chklstAppliedPositions.DataSource = dtAppliedPositions;
                chklstAppliedPositions.DataTextField = "PositionName";
                chklstAppliedPositions.DataValueField = "PKPositionID";
                chklstAppliedPositions.DataBind();

                chklstApprovedPositions.DataSource = dtApprovedPositions;
                chklstApprovedPositions.DataTextField = "PositionName";
                chklstApprovedPositions.DataValueField = "PKPositionID";
                chklstApprovedPositions.DataBind();
            }


        }

        /// <summary>
        /// Sent Login Detail email to the Technician
        /// </summary>
        private void SentLoginDetailEmail()
        {
            #region File Read Code

            string MailBody = string.Empty;

            System.Text.StringBuilder stringBuilderText = new System.Text.StringBuilder();
            string mailContent = null;
            //mailContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/") + "Templates\\Welcome.html");

            EmailTemplate emailTemplate = new EmailTemplate((int)EmailTemplates.WelcomeMessage);
            mailContent = emailTemplate.Body.Replace("\n", "<br />").Replace("\r", "");
            string MailSubject = emailTemplate.Subject;

            mailContent = General.ToStr(mailContent.Replace("[@@FirstName]", objTechnician.Firstname));
            mailContent = General.ToStr(mailContent.Replace("[@@Href]", ProjectConfiguration.SiteUrl + "Login.aspx"));
            StringBuilder stringBuilderCheckList = new StringBuilder();
            stringBuilderText.Append(mailContent);
            MailBody = stringBuilderText.ToString();
            #endregion
            try
            {
                //Email.Send(ProjectConfiguration.FromEmail, objTechnician.Email, ProjectConfiguration.CC, ProjectConfiguration.BCC, MailSubject, MailBody, string.Empty, true, Email.EmailType.Default);
                Email.Send(emailTemplate.EmailFrom, objTechnician.Email, emailTemplate.CCEmail, ProjectConfiguration.BCC, MailSubject, MailBody, string.Empty, true, Email.EmailType.Default);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Save Data into Database
        /// </summary>
        /// <returns></returns>
        private int SaveData()
        {
            objTechnician.OvationRef = string.Empty;
            if (chkbFaceBook.Checked)
                objTechnician.OvationRef += ",0";
            if (chkbLinkedin.Checked)
                objTechnician.OvationRef += ",1";
            if (chkbFriend.Checked)
                objTechnician.OvationRef += ",2";
            if (chkbRecruiter.Checked)
                objTechnician.OvationRef += ",3";
            objTechnician.OvationRef = objTechnician.OvationRef.TrimStart(',');


            IEnumerable<string> PostionsChecked = (from item in chklstApprovedPositions.Items.Cast<ListItem>()
                                                   where item.Selected
                                                   select item.Value);
            IEnumerable<string> PostionsApplied = (from item in chklstAppliedPositions.Items.Cast<ListItem>()
                                                   where item.Selected
                                                   select item.Value);

            dtPositions = new DataTable();
            if (!dtPositions.Columns.Contains("FKPositionID"))
                dtPositions.Columns.Add("FKPositionID", typeof(int));
            if (!dtPositions.Columns.Contains("StatusID"))
                dtPositions.Columns.Add("StatusID", typeof(int));


            foreach (string item in PostionsChecked.ToList().ToArray())
            {
                dtPositions.Rows.Add(item, 1);
            }
            foreach (string item in PostionsApplied.ToList().ToArray())
            {
                if (!PostionsChecked.Contains(item))
                    dtPositions.Rows.Add(item, 0);
            }


            //if (PostionsChecked.Count() == 0)
            //{
            //    MessageBox.AddErrorMessage("Please select approved position to be continue...");
            //    return;
            //}
            strCheckedPositions = string.Join<string>(",", PostionsChecked);
            objTechnician.Firstname = txtName.Text;
            objTechnician.LastName = txtLastName.Text;
            objTechnician.NearestAirport = txtNearestAirport.Text;
            objTechnician.City = txtCityOfLocation.Text;
            objTechnician.UserName = txtUsername.Text.Trim();
            objTechnician.Password = EncryptionDecryption.GetEncrypt(txtPassword.Text.Trim());
            objTechnician.Email = txtEmail.Text.Trim();
            objTechnician.IsAccess = chkbAccessTech.Checked;
            objTechnician.HasPassport = chkbPassport.Checked;
            objTechnician.IsInterviewed = chkbInterview.Checked;
            objTechnician.Ishotelshare = chkbSharehotelroom.Checked;
            objTechnician.SSN = txtSSN.Text.Trim();
            objTechnician.IsAcceptTerms = true;
            if (!string.IsNullOrEmpty(txtBirthdate.Text.Trim()))
                objTechnician.BirthDate = ConvertTo.Date(txtBirthdate.Text.Trim());
            else
                objTechnician.BirthDate = null;
            objTechnician.Address = txtAddressLine1.Text.Trim();
            objTechnician.HomeAddress = txtAddressLine2.Text.Trim();


            objTechnician.ShirtSize = rdbShirtSize.SelectedValue;
            objTechnician.Gender = rdblstGender.SelectedValue == "0" ? false : true;
            objTechnician.State = txtState.Text.Trim();
            objTechnician.ZipCode = txtZipCode.Text.Trim();
            objTechnician.Phone1 = txtCellPhone.Text.Trim();
            objTechnician.NickName = txtNickName.Text.Trim();



            // objTechnician.DtApprovaldate = DateTime.Now;
            objTechnician.ApprovedBy = ProjectSession.UserID;
            objTechnician.IsApproved = chkApprove.Checked;
            if (chkApprove.Checked)
                objTechnician.IsEmailSent = true;
            else
                objTechnician.IsEmailSent = false;
            objTechnician.dtPosition = dtPositions;
            if (fluResume.HasFile)
            {
                string ResumePath = System.IO.Path.GetRandomFileName() + Path.GetExtension(fluResume.FileName);
                fluResume.SaveAs(ProjectConfiguration.TechnicianResumeBasePath + ResumePath);
                objTechnician.ResumePath = ResumePath;
            }
            if (fluImage.HasFile)
            {
                string ImagePath = System.IO.Path.GetRandomFileName() + Path.GetExtension(fluImage.FileName);
                fluImage.SaveAs(ProjectConfiguration.TechnicianPhotoBasePath + ImagePath);
                objTechnician.PhotoPath = ImagePath;
            }
            objTechnician.PKTechnicianID = _TechnicianID;
            objTechnicianReferences = new List<TechnicianReference>();
            objTechnicianReferences.Add(TReference1.getReferenceData());
            objTechnicianReferences.Add(TReference2.getReferenceData());
            objTechnicianReferences.Add(TReference3.getReferenceData());
            objTechnicianReferences.Add(TReference4.getReferenceData());
            objTechnicianReferences.Add(TReference5.getReferenceData());
            objTechnicianReferences = objTechnicianReferences.Where(s => !string.IsNullOrEmpty(s.FirstName) && !string.IsNullOrEmpty(s.LastName)).ToList();
            DataTable dtTechnicianReference = objTechnicianReferences.Select(s => new { FirstName = s.FirstName, LastName = s.LastName, Email = s.Email, PhoneNumber = s.PhoneNumber }).ToList().ToDataTable();

            int returnValue = 0;
            if (_TechnicianID == 0)
            {
                if (chkApprove.Checked)
                    objTechnician.DtApprovaldate = DateTime.Now;

                returnValue = objTechnician.AdminInsert(dtTechnicianReference);
            }
            else
            {
                if (chkApprove.Checked && _IsChecked != true)
                    objTechnician.DtApprovaldate = DateTime.Now;

                returnValue = objTechnician.AdminUpdate(dtTechnicianReference);
            }

            return returnValue;
        }
        #endregion

        #region Control Events
        protected void lnkAddReference_Click(object sender, EventArgs e)
        {
            ucCount += 1;
            switch (ucCount)
            {
                case 3: TReference3.Visible = true;
                    break;
                case 4: TReference4.Visible = true;
                    break;
                case 5: TReference5.Visible = true;
                    break;
            }
            if (ucCount > 4)
                lnkAddReference.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (_TechnicianID > 0)
            {
                int RerurnVal = TechTrackingDAL.Technician.Delete(_TechnicianID);
                if (RerurnVal == -1)
                {
                    MessageBox.AddErrorMessage("Sorry, This Technician is already used in the System");
                    return;
                }
                else
                {
                    MessageBox.AddSuccessMessage("Technician deleted successfully");
                }
                Response.Redirect("~/Admin/TechnicianListing.aspx");
            }
        }
        protected void btnTable_Click(object sender, EventArgs e)
        {
            if (_TechnicianID > 0)
            {
                TechTrackingDAL.Technician.TechnicianUpdateTabled(_TechnicianID, true);
                MessageBox.AddSuccessMessage("Successfully tabled");
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Page.Validate("vldInformation");
            if (Page.IsValid)
            {
                if (!TReference1.validate())
                {
                    MessageBox.AddErrorMessage("Please enter atleast one reference");
                    return;
                }
                if (TReference2.Subvalidate() && TReference3.Subvalidate() && TReference4.Subvalidate() && TReference5.Subvalidate())
                {
                    if (!ValidateAirports()) return;
                    int returnValue = SaveData();
                    if (returnValue == -2)
                    {
                        MessageBox.AddErrorMessage("This email already exists");
                        return;
                    }
                    else if (returnValue == -1)
                    {
                        MessageBox.AddErrorMessage("This userName already exists");
                        return;
                    }
                    else
                    {
                        if (objTechnician.IsApproved == true && hdnIsEmailSent.Value == "0" && objTechnician.Email.Length > 0)
                            SentLoginDetailEmail();
                        if (_TechnicianID > 0)
                            MessageBox.AddSuccessMessage("Technician updated successfully");
                        else
                        {
                            MessageBox.AddSuccessMessage("Technician added successfully");
                            Response.Redirect("~/Admin/AddEditTechnician.aspx?" + SetQueryString("TechnicianID", General.ToStr(_TechnicianID)));
                        }
                        _TechnicianID = returnValue;
                    }
                    bindPositions();
                    LoadData();
                }
                else
                {
                    MessageBox.AddErrorMessage("Please correct references information");
                }

            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Page.Validate("vwLogo");
            if (Page.IsValid)
            {
                if (fluImage.FileBytes.Length > 0 && fluImage.HasFile)
                {
                    string ImagePath = System.IO.Path.GetRandomFileName() + Path.GetExtension(fluImage.FileName);

                    objTechnician.PhotoPath = ImagePath;
                    fluImage.SaveAs(ProjectConfiguration.TechnicianPhotoBasePath + ImagePath);

                    HttpContext.Current.Session["ImageBytes"] = fluImage.FileBytes;
                    imgUpload.ImageUrl = "~/ImageHandler.ashx";
                }
            }
        }

        protected void btnSaveAndEmail_Click(object sender, EventArgs e)
        {
            //Page.Validate("vldInformation");
            if (Page.IsValid)
            {
                if (!TReference1.validate())
                {
                    MessageBox.AddErrorMessage("Please enter atleast one reference");
                    return;
                }
                if (TReference2.Subvalidate() && TReference3.Subvalidate() && TReference4.Subvalidate() && TReference5.Subvalidate())
                {
                    if (!ValidateAirports()) return;
                    int returnValue = SaveData();
                    if (returnValue == -2)
                    {
                        MessageBox.AddErrorMessage("This email already exists");
                        return;
                    }
                    else if (returnValue == -1)
                    {
                        MessageBox.AddErrorMessage("This userName already exists");
                        return;
                    }
                    else
                    {
                        if (objTechnician.IsApproved == true && hdnIsEmailSent.Value == "0" && objTechnician.Email.Length > 0)
                            //SentLoginDetailEmail();
                            if (_TechnicianID > 0)
                            {
                                SentLoginDetailEmail();
                                MessageBox.AddSuccessMessage("Technician updated successfully");
                            }
                        _TechnicianID = returnValue;
                    }
                    bindPositions();
                    LoadData();
                }
                else
                {
                    MessageBox.AddErrorMessage("Please correct references information");
                }

            }


        }
        #endregion

        #region Web Methods
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static string[] GetAirportList(string prefixText, int count)
        {
            DataSet ds = TechTrackingDAL.Technician.SearchAirports(prefixText + "%");
            DataTable dt = ds.Tables[0];
            string[] items = new string[dt.Rows.Count];
            int i = 0;

            for (i = 0; i < dt.Rows.Count; i++)
            {
                items[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["Airport"].ToString(), dt.Rows[i]["City"].ToString());
            }
            return items;
        }
        #endregion
    }
}