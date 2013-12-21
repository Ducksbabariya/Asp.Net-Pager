using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechTracking.Classes;

namespace TechTracking.UserControls
{
    [Serializable]
    public class DatetimeParser
    {
        public static List<DateTime> DateRange(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                                    .Select(d => fromDate.AddDays(d)).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
    public partial class Calender : System.Web.UI.UserControl
    {

        public int DayStyleHeight
        {
            set
            {
                calTechview.DayStyle.Height = value;
            }
        }
        public int DayStyleHeaderHeight
        {
            set
            {
                calTechview.DayHeaderStyle.Height = value;
            }
        }
        public int CellPadding
        {
            set
            {
                calTechview.CellPadding = value;
            }
        }
        public int CellSpacing
        {
            set
            {
                calTechview.CellSpacing = value;
            }
        }
        public string fntSize
        {
            set
            {
                calTechview.Attributes["Font-Size"] = value;
            }
        }
        public bool ShowNextPrevMonth
        {
            set
            {
                calTechview.ShowNextPrevMonth = value;
            }
        }
        public string ToolTip
        {
            get
            {
                return General.ToStr(ViewState["ToolTip"]);
            }
            set
            {
                ViewState["ToolTip"] = value;
            }
        }
        private List<DateTime> iDates
        {
            get
            {
                return (List<DateTime>)ViewState["iDates"];
            }
            set
            {
                ViewState["iDates"] = value;
            }
        }
        private List<DateTime> TravelDates
        {
            get
            {
                return (List<DateTime>)ViewState["TravelDates"];
            }
            set
            {
                ViewState["TravelDates"] = value;
            }
        }

        private Dictionary<DateTime, string> dicTravelDates
        {
            get
            {
                return (Dictionary<DateTime, string>)ViewState["dicTravelDates"];
            }
            set
            {
                ViewState["dicTravelDates"] = value;
            }
        }
        private Dictionary<DateTime, string> dicShowDates
        {
            get
            {
                return (Dictionary<DateTime, string>)ViewState["dicShowDates"];
            }
            set
            {
                ViewState["dicShowDates"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadData(DateTime StartDate, DateTime EndDate, List<DateTime> lstTravelDays)
        {
            try
            {
                calTechview.VisibleDate = StartDate;
                if (StartDate.Month == EndDate.Month)
                {
                    calTechview.ShowNextPrevMonth = false;
                }
                else
                {
                    calTechview.ShowNextPrevMonth = true;
                }
                iDates = DatetimeParser.DateRange(StartDate, EndDate);
                if (iDates == null)
                    iDates = new List<DateTime>();
                TravelDates = lstTravelDays;

            }
            catch (Exception ex)
            {

            }
        }

        public void LoadData(List<DateTime> lstShowDays, List<DateTime> lstTravelDays, DateTime visibleMonth)
        {
            try
            {
                TravelDates = lstTravelDays;
                calTechview.VisibleDate = visibleMonth;
                if (lstShowDays.Count == 0)
                {
                    iDates = new List<DateTime>();
                    calTechview.ShowNextPrevMonth = false;
                }
                else
                {
                    lstShowDays.Sort();

                    if (lstShowDays.First().Month == lstShowDays.Last().Month)
                    {
                        calTechview.ShowNextPrevMonth = false;
                    }
                    else
                    {
                        calTechview.ShowNextPrevMonth = true;
                    }
                    iDates = lstShowDays;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void LoadData(Dictionary<DateTime, string> lstShowDays, Dictionary<DateTime, string> lstTravelDays, DateTime visibleMonth)
        {
            try
            {
                dicTravelDates = lstTravelDays;
                calTechview.VisibleDate = visibleMonth;
                if (lstShowDays.Count == 0)
                {
                    dicShowDates = new Dictionary<DateTime, string>();
                    calTechview.ShowNextPrevMonth = false;
                }
                else
                {
                    //lstShowDays.Sort();

                    if (lstShowDays.First().Key.Month == lstShowDays.Last().Key.Month)
                    {
                        calTechview.ShowNextPrevMonth = false;
                    }
                    else
                    {
                        calTechview.ShowNextPrevMonth = true;
                    }
                    dicShowDates = lstShowDays;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void calTechview_DayRender(object sender, DayRenderEventArgs e)
        {
            if (dicShowDates != null && dicTravelDates != null)
            {
                if (dicTravelDates.Count > 0 && dicTravelDates.Keys.Where(s => s.Date == e.Day.Date).Count() > 0)
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#67A2A6");
                    e.Cell.ToolTip = dicTravelDates.Where(s => s.Key.Date == e.Day.Date).First().Value +" - Travel Day";
                }
                else if (dicShowDates.Keys.Where(s => s.Date == e.Day.Date).Count() > 0)
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#CEEDF0");
                    e.Cell.ToolTip = dicShowDates.Where(s => s.Key.Date == e.Day.Date).First().Value + " - Show Day";
                }
            }
            else
            {
                if (TravelDates.Count > 0 && TravelDates.Where(s => s.Date == e.Day.Date).Count() > 0)
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#67A2A6");

                    e.Cell.ToolTip = "Travel Day";
                }
                else if (iDates.Where(s => s.Date == e.Day.Date).Count() > 0)
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#CEEDF0");

                    e.Cell.ToolTip = "Show Day";
                }
            }
        }
    }
}