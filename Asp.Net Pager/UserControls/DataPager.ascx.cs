using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using TechTracking.Classes;

namespace TechTracking.UserControls
{
    public partial class DataPager : System.Web.UI.UserControl
    {
        #region "Delegates"

        private Delegate delUpdatePageIndex;
        public System.Delegate UpdatePageIndex
        {
            set { delUpdatePageIndex = value; UpdatePaging(this.PageIndex, this.RecordsPerPage, this.TotalRecords); }
        }
        #endregion

        #region "Properties"


        public static int _pagesToDisplay = 5;
        [Category("Behavior")]
        [Description("Total number of records")]
        [DefaultValue(0)]
        public int TotalRecords
        {
            get
            {
                object o = ViewState["TotalRecords"];
                if (o == null)
                    return 0;
                return (int)o;
            }
            set { 
                ViewState["TotalRecords"] = value;
               
                if (value > 0)
                    Visible = true;
                else
                    Visible = false;
            
            }
        }
        [Category("Behavior")]
        [Description("Current page index")]
        [DefaultValue(1)]
        public int PageIndex
        {
            get
            {
                object o = ViewState["PageIndex"];
                if (o == null)
                    return 1;
                return (int)o;
            }
            set { ViewState["PageIndex"] = value; }
        }
        [Category("Behavior")]
        [Description("Total number of records to each page")]
        //[DefaultValue(10)]
        public int RecordsPerPage
        {
            get
            {
                return ProjectConfiguration.PageSize;
            }
        }
        private decimal TotalPages
        {
            get
            {
                object o = ViewState["TotalPages"];
                if (o == null)
                    return 0;
                return (decimal)o;
            }
            set { ViewState["TotalPages"] = value; }
        }
        #endregion

        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int[] Paging = { 1, 2, 5, 10, 20, 30, 50, 100 };
                for (int p = 0; p < Paging.Length; p++)
                {
                    ddlRecords.Items.Add(General.ToStr(Paging[p]));
                    ddlRecords.SelectedIndex = 0;
                }
                           
                UpdatePaging(this.PageIndex, this.RecordsPerPage, this.TotalRecords);

            }
        }
        #endregion

        #region "Control Events"

        public void bindPager()
        {
            int start_pos;
            int end_pos;
            int halfSide = _pagesToDisplay / 2;

            int m_PageCount = Convert.ToInt16(TotalPages);

            int _currentPageNumber = this.PageIndex;

            if (_currentPageNumber > m_PageCount)
            {
                _currentPageNumber = m_PageCount;
            }
            else if (_currentPageNumber < 1)
            {
                _currentPageNumber = 1;
            }

            if (_currentPageNumber - halfSide <= 0)
            {
                start_pos = 1;
            }
            else
            {
                if (_pagesToDisplay % 2 == 0)
                {
                    start_pos = _currentPageNumber - halfSide + 1;
                }
                else
                {
                    start_pos = _currentPageNumber - halfSide;
                }
            }
            end_pos = _currentPageNumber + halfSide;

            if (end_pos > m_PageCount)
            {
                end_pos = m_PageCount;
                start_pos = end_pos - _pagesToDisplay + 1;
            }
            else if (end_pos < _pagesToDisplay)
            {
                end_pos = _pagesToDisplay;
                start_pos = 1;
            }

            if (end_pos > m_PageCount)
            {
                end_pos = m_PageCount;
            }
            if (start_pos <= 0)
            {
                start_pos = 1;
            }


            DataTable dtpager = new DataTable("Pager");
            dtpager.Columns.Add(new DataColumn("IntPageNo", typeof(int)));

            int inti;
            DataRow dbrow;
            for (inti = start_pos; inti <= end_pos; inti++)
            {
                dbrow = dtpager.NewRow();
                dbrow[0] = inti;
                dtpager.Rows.Add(dbrow);
            }
            Rptpager.DataSource = dtpager;
            Rptpager.DataBind();
        }
        protected void Rptpager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int intArgs = int.Parse(General.ToStr(e.CommandArgument));
           
            this.PageIndex = intArgs;
            UpdatePaging(this.PageIndex, this.RecordsPerPage, this.TotalRecords);
            callParent();
        }

        protected void Rptpager_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (this.PageIndex == General.GetInt(DataBinder.Eval(e.Item.DataItem, "IntPageNo")))
                {
                    LinkButton link = (LinkButton)e.Item.FindControl("link");
                    link.CssClass = "active";
                    link.OnClientClick = "return false;";
                }
            }

        }



        protected void btnMove_Click(object sender, CommandEventArgs e)
        {
            switch (General.ToStr(e.CommandArgument))
            {
                case "First":
                    this.PageIndex = 1;
                    break;
                case "Previous":
                    this.PageIndex--;
                    break;
                case "Next":
                    this.PageIndex++;
                    break;
                case "Last":
                    this.PageIndex = (int)this.TotalPages;
                    break;
            }           
            UpdatePaging(this.PageIndex, this.RecordsPerPage, this.TotalRecords);
            callParent();
        }
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            int newPage = Convert.ToInt32(txtPage.Text);
            if (newPage > this.TotalPages)
                this.PageIndex = (int)this.TotalPages;
            else
                this.PageIndex = newPage;
            UpdatePaging(this.PageIndex, this.RecordsPerPage, this.TotalRecords);
        }
        protected void ddlRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            this.PageIndex = 1;
            UpdatePaging(this.PageIndex, this.RecordsPerPage, this.TotalRecords);
        }
        #endregion

        #region "Web Methods"
        public void callParent()
        {
            //call method to re-populate parent page data, 
            //given current index:
            object[] aObj = new object[1];
            aObj[0] = this.PageIndex;
            delUpdatePageIndex.DynamicInvoke(aObj);
        }
        public void UpdatePaging(int pageIndex, int pageSize, int recordCount)
        {
            if (recordCount > 0)
            {
                lblTotalRecord.ForeColor = Color.Black;
                ddlRecords.Enabled = true;
                int currentEndRow = (pageIndex * pageSize);
                if (currentEndRow > recordCount) currentEndRow = recordCount;

                if (currentEndRow < pageSize) pageSize = currentEndRow;
                int currentStartRow = (currentEndRow - pageSize) + 1;

                this.TotalPages = Math.Ceiling((decimal)recordCount / pageSize);
                txtPage.Text = string.Format("{0:00}", this.PageIndex);
                lblTotalRecord.Text = string.Format("{0:00}-{1:00} of {2:00} record(s)", currentStartRow, currentEndRow, recordCount);
                lblTotalPage.Text = string.Format(" of {0:00} page(s)", this.TotalPages);

                btnMoveFirst.Enabled = (pageIndex == 1) ? false : true;
                btnMovePrevious.Enabled = (pageIndex > 1) ? true : false;
                btnMoveNext.Enabled = (pageIndex * pageSize < recordCount) ? true : false;
                btnMoveLast.Enabled = (pageIndex * pageSize >= recordCount) ? false : true;

               
                bindPager();
            }
            else
            {
                lblTotalPage.Text = "";
                lblTotalRecord.Text = "No Record Found!";
                lblTotalRecord.ForeColor = Color.Red;
                btnMoveFirst.Enabled = false;
                btnMovePrevious.Enabled = false;
                btnMoveNext.Enabled = false;
                btnMoveLast.Enabled = false;
                txtPage.Enabled = false;
                ddlRecords.Enabled = false;
                this.TotalPages = 1;
                bindPager();
            }
        }
        #endregion


    }
}