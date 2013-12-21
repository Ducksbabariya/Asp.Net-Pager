using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechTrackingDAL;
using TechTracking.Classes;
using System.Text.RegularExpressions;
namespace TechTracking.UserControls
{
    public partial class References : System.Web.UI.UserControl
    {
        //public TechnicianReference objTechnicianReference = new TechnicianReference();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void LoadData(TechnicianReference objTechnicianReference)
        {
            txtLastName.Text = objTechnicianReference.LastName;
            txtFirstName.Text = objTechnicianReference.FirstName;
            txtPhoneNumber.Text = objTechnicianReference.PhoneNumber;
            txtEmail.Text = objTechnicianReference.Email;
            hdnPKTechnicianReferenceID.Value = objTechnicianReference.PKTechnicianReferenceID.ToString();
            hdnFKTechnicianID.Value = objTechnicianReference.FKTechnicianID.ToString();
        }
        public bool validate()
        {
            if (General.IsValidEmail(txtEmail.Text) && !string.IsNullOrEmpty(txtFirstName.Text) && !string.IsNullOrEmpty(txtLastName.Text))
            {
                return true;
            }
            revEmail.Validate();
            rfvEmail.Validate();
            rfvFirstName.Validate();
            rfvLastName.Validate();           
            return false;
        }
        public bool Subvalidate()
        {
            if (string.IsNullOrEmpty(txtEmail.Text) && string.IsNullOrEmpty(txtFirstName.Text) && string.IsNullOrEmpty(txtLastName.Text))
            {
                return true;
            }
            else if (General.IsValidEmail(txtEmail.Text) && !string.IsNullOrEmpty(txtFirstName.Text) && !string.IsNullOrEmpty(txtLastName.Text))
            {
                return true;
            }
            revEmail.Validate();
            rfvEmail.Validate();
            rfvFirstName.Validate();
            rfvLastName.Validate();
            return false;
        }
        public TechnicianReference getReferenceData()
        {
            TechnicianReference obj = new TechnicianReference();
            if (validate())
            {
                obj.FirstName = txtFirstName.Text;
                obj.LastName = txtLastName.Text;
                obj.Email = txtEmail.Text;
                obj.PhoneNumber = txtPhoneNumber.Text;
            }
            return obj;
        }
    }
}