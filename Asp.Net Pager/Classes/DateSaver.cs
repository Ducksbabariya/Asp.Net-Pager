using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using TechTracking.Classes;
using System.Web.UI;
namespace TechTracking.Classes
{

    public static class EMethods
    {
        public static T FindControl<T>(this Control startingControl, string id) where T : Control
        {
            T foundControl = default(T);

            int controlCount = startingControl.Controls.Count;

            foreach (Control c in startingControl.Controls)
            {
                if (c is T && string.Equals(id, c.ID,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    foundControl = c as T;
                    break;
                }
                else
                {
                    foundControl = FindControl<T>(c, id);
                    if (foundControl != null)
                    {
                        break;
                    }
                }
            }
            return foundControl;
        }
    }
    public static class DateSaver
    {
        public enum DateFormater
        {
            yyyyMMdd,
            MMddyyyy,
            ddMMyyyy
        }
        public static DateTime GetDate(string strDate, DateFormater df, char separator)
        {
            //string dateFormat = ProjectConfiguration.DateFormat;
            //if (dateFormat.IndexOf('-') > 0)
            //{
            //    separator = '-';
            //}
            //else
            //{
            //    separator = '/';
            //}
            DateTime dtFinal = new DateTime();
            string Day = "0", Year = "0", Month = "0";
            string Time = string.Empty;
            string[] strArr;

            if (strDate.Contains(" "))
            {
                Time = strDate.Substring(strDate.IndexOf(" ") + 1);
                strDate = strDate.Substring(0, strDate.IndexOf(" "));
            }

            strArr = strDate.ToString().Split(separator);
            DateTime newDate = new DateTime();
            DateTime.TryParse(strDate, out newDate);
            if (newDate != DateTime.MinValue)
            {

            }
            List<string> LstValues = new List<string>();

            foreach (string strKey in strArr)
            {
                if (strKey != string.Empty)
                {
                    LstValues.Add(strKey);
                }
            }
            if (LstValues.Count == 3)
            {
                if (df == DateFormater.yyyyMMdd)
                {
                    Year = LstValues[0];
                    Month = LstValues[1];
                    Day = LstValues[2];
                }
                else if (df == DateFormater.ddMMyyyy)
                {
                    Day = LstValues[0];
                    Month = LstValues[1];
                    Year = LstValues[2];
                }
                else if (df == DateFormater.MMddyyyy)
                {
                    Month = LstValues[0];
                    Day = LstValues[1];
                    Year = LstValues[2];
                }
            }
            if (Time != string.Empty)
            {
                dtFinal = new DateTime(General.GetInt(Year), General.GetInt(Month), General.GetInt(Day));
            }
            else
            {
                dtFinal = new DateTime(General.GetInt(Year), General.GetInt(Month), General.GetInt(Day));
            }
            return dtFinal;
        }
        public static DateTime GetDate(object strDate)
        {
            DateTime newDate = new DateTime();
            DateTime.TryParse(strDate.ToString(), out newDate);
            return newDate;
        }
        public static string DispalyDate(string strDate, DateFormater df, char separator)
        {
            string returnDate = string.Empty;
            DateTime dtFinal = new DateTime();
            if (!string.IsNullOrEmpty(strDate))
            {
                dtFinal = Convert.ToDateTime(strDate);
                if (dtFinal != DateTime.MinValue)
                {
                    if (df == DateFormater.yyyyMMdd)
                    {
                        returnDate = dtFinal.ToString("yyyy") + separator.ToString() + dtFinal.ToString("MM") + separator.ToString() + dtFinal.ToString("dd");
                    }
                    else if (df == DateFormater.ddMMyyyy)
                    {
                        returnDate = dtFinal.ToString("dd") + separator.ToString() + dtFinal.ToString("MM") + separator.ToString() + dtFinal.ToString("yyyy");
                    }
                    else if (df == DateFormater.MMddyyyy)
                    {
                        returnDate = dtFinal.ToString("MM") + separator.ToString() + dtFinal.ToString("dd") + separator.ToString() + dtFinal.ToString("yyyy");
                    }
                }
            }
            return returnDate;
        }

        public static string DispalyFullDate(object strDate, DateFormater df)
        {
            char separator = '/';
            string returnDate = string.Empty;
            DateTime dtFinal = new DateTime();
            if (!string.IsNullOrEmpty(strDate.ToString()))
            {
                dtFinal = GetDate(strDate);
                if (dtFinal != DateTime.MinValue)
                {
                    if (df == DateFormater.yyyyMMdd)
                    {
                        returnDate = dtFinal.ToString("yyyy") + separator.ToString() + dtFinal.ToString("MM") + separator.ToString() + dtFinal.ToString("dd") + " " + dtFinal.ToString("hh:mm:ss");
                    }
                    else if (df == DateFormater.ddMMyyyy)
                    {
                        returnDate = dtFinal.ToString("dd") + separator.ToString() + dtFinal.ToString("MM") + separator.ToString() + dtFinal.ToString("yyyy") + " " + dtFinal.ToString("hh:mm:ss");
                    }
                    else if (df == DateFormater.MMddyyyy)
                    {
                        returnDate = dtFinal.ToString("MM") + separator.ToString() + dtFinal.ToString("dd") + separator.ToString() + dtFinal.ToString("yyyy") + " " + dtFinal.ToString("hh:mm:ss");
                    }
                }
            }
            return returnDate;
        }
        public static string DispalyDate(DateTime strDate, DateFormater df, char separator)
        {
            string returnDate = string.Empty;
            DateTime dtFinal = new DateTime();
            if (!string.IsNullOrEmpty(strDate.ToString()))
            {
                dtFinal = Convert.ToDateTime(strDate);
                if (dtFinal != DateTime.MinValue)
                {
                    if (df == DateFormater.yyyyMMdd)
                    {
                        returnDate = dtFinal.ToString("yyyy") + separator.ToString() + dtFinal.ToString("MM") + separator.ToString() + dtFinal.ToString("dd");
                    }
                    else if (df == DateFormater.ddMMyyyy)
                    {
                        returnDate = dtFinal.ToString("dd") + separator.ToString() + dtFinal.ToString("MM") + separator.ToString() + dtFinal.ToString("yyyy");
                    }
                    else if (df == DateFormater.MMddyyyy)
                    {
                        returnDate = dtFinal.ToString("MM") + separator.ToString() + dtFinal.ToString("dd") + separator.ToString() + dtFinal.ToString("yyyy");
                    }
                }
            }
            return returnDate;
        }
    }
}