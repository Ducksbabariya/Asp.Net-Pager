using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;

namespace TechTracking.UserControls
{
    public partial class SliderBar : System.Web.UI.UserControl
    {
        #region Properties

        public string SliderValue
        {
            get
            {
                return hdnVal.Value;

            }
            set { hdnVal.Value = value; }
        }

        public Unit Width
        {
            get
            {
                return search_bar.Width;
            }
            set
            {
                search_bar.Width = value;
            }
        }

        public string Text
        {
            get
            {
                return lblVal.Text;

            }
            set { lblVal.Text = value; }
        }
        #endregion

        #region PageEvents
        protected void Page_Load(object sender, EventArgs e)
        {
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClientScript", "SetSliderValue();", true);

        }
        #endregion

        public void SetValue()
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClientScript", "setSliderValue(" + SliderValue + ");", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SliderValue", "setSliderValue(" + SliderValue + ");", true);
        }


    }
}