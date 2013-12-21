using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web;
using System.IO;
using System.Data.OleDb;
using System.Net;
using System.Xml;

using System.Globalization;
using System.Net.NetworkInformation;
using System.ComponentModel;

namespace TechTracking.Classes
{
    public static class Methods
    {
        public static void selectedText(this DropDownList ddl,string _selectedValue)
        {
            try
            {
                ddl.Items.FindByText(_selectedValue).Selected = true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
    public class General
    {
        #region CONST
        private const string strTamperProofKey = "astkvsnanvpi";
        private const string strItemCSS = "nrlbg";
        private const string strAltItemCSS = "altbg";
        #endregion

        #region Enum
        public General()
        {

        }

        public enum PropertyPageType
        {
            Detail = 1,
            Print = 2
        }


        public enum Counter : int
        {
            CpuUsage = 1,
            AvailableMemory,
            UsedMemory,
            RequestsCurrent,
            SessionsActive,
            WebServiceCurrentConnections,
            WebserviceConnectionAttemptsPerSecond,
            MachineName,
            PeakWorkingSet,
            PageFileUsage,
            NonpagedSystemMemorySize,
            TotalProcessorTime,
            UserProcessorTime,
            LocalMachineTime,
            CacheTotalHitRatio,
            CacheTotalTurnoverRate,
            CacheTotalEntries,
            SplitIOPerSec,
            MemoryPagesPerSec

        }

        public enum SubscriptionListType
        {
            PerformanceChart = 0,
            ManageWebRoles = 1
        }
        #endregion

        #region Validation Expression

        /// <summary>
        ///  Decomposed method which actually creates the pattern object and determines the match.
        ///  Used by all of the other functions.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strRegex"></param>
        /// <returns></returns>
        private bool MatchString(string str, string strRegex)
        {
            str = str.Trim();
            System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex(strRegex);
            return pattern.IsMatch(str);
        }

        /// <summary>
        /// check text is Numeric or not.
        /// Allows one or more positive or negative, integer or decimal numbers. This is a more generic validation function.
        /// </summary>
        /// <param name="strNumeric"></param>
        /// <returns></returns>
        public bool IsValidNumericText(string strNumeric)
        {
            // Allows one or more positive or negative, integer or decimal numbers. This is a more generic validation function.
            string strRegExPattern = "^([+-]?\\d+(\\.\\d+)?)$";
            return MatchString(strNumeric, strRegExPattern);
        }

        /// <summary>
        /// check text is Numeric or not.
        /// Allows one or more positive or negative, integer or decimal numbers. This is a more generic validation function.
        /// </summary>
        /// <param name="strNumeric"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email))
                return false;
            // Allows one or more positive or negative, integer or decimal numbers. This is a more generic validation function.
            string strRegExPattern = @"^([A-Za-z0-9_\-\.\'])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,6})$";
            Email = Email.Trim();
            System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex(strRegExPattern);
            return pattern.IsMatch(Email);
        }
        /// <summary>
        /// Email Regular Exprestion
        /// </summary>
        /// <returns></returns>
        public string EmailExp()
        {
            //return "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            return @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        }

        /// <summary>
        /// Number Regular Exprestion
        /// </summary>
        /// <returns></returns>
        public string NumberExp()
        {
            return "^([0-9]*|\\d*\\d{1}?\\d*)$";
        }

        /// <summary>
        /// Regular Exprestion for number with fraction.
        /// </summary>
        /// <returns></returns>
        public string NumberWithFractionExp()
        {
            return "^\\d+(?:\\.\\d{0,4})?$";
        }

        /// <summary>
        /// Regular Exprestion for number with Decimal
        /// </summary>
        /// <returns></returns>
        public string DecimalExp()
        {
            return "^\\d*\\.?\\d{1,2}$";
        }

        /// <summary>
        /// Website url Regular Exprestion
        /// like http://www.tatvasoft.com
        /// </summary>
        /// <returns></returns>
        public string WebSiteExp()
        {
            return "http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?";
        }

        /// <summary>
        /// Regular Exprestion for alpha format
        /// </summary>
        /// <returns></returns>
        public string AlphaFormat()
        {
            return "^[a-zA-Z]*$";
        }

        /// <summary>
        /// Regular Exprestion for alpha numeric
        /// </summary>
        /// <returns></returns>
        public string AlphaNumericFormat()
        {
            return "^[a-zA-Z0-9]+$";
        }

        /// <summary>
        /// fileExtension - Seperate all the files withj pipe ('|') sign
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public string AllowedExtensionExp(string fileExtension)
        {
            return "^(([a-zA-Z]:)|(\\\\{2}\\w+)\\$?)(\\\\(\\w[\\w].*))+(" + fileExtension + ")$";
        }

        /// <summary>
        /// Minimun lenth 
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public string MinLength(int Length)
        {
            return "[0-9a-zA-Z!@#$%^& *]{" + Length.ToString() + ",}";
        }

        /// <summary>
        /// Sql injection
        /// </summary>
        /// <returns></returns>
        public string SqlInjection()
        {
            return "^[a-zA-Z'.\\s | \\d | \\- | \\/ | \\$ | \\£ | \\€ | \\( | \\) | \\ | \\! | \\% | \\+ | \\& | \\, | \\! $]{1,200}$";
        }

        #endregion

        #region Methods



        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear(); // make all variables NULL,session id not changed
            HttpContext.Current.Session.Abandon(); // session is destroyed,so on next request new session created        
        }

        /// <summary>
        /// Bind the GridView with DataTable
        /// </summary>
        /// <param name="grdGridView"></param>
        /// <param name="dtDataTable"></param>
        public void GridBind(ref GridView grdGridView, DataTable dtDataTable)
        {
            grdGridView.DataSource = dtDataTable;
            grdGridView.DataBind();
        }

        /// <summary>
        /// Bind the GridView with DataSet
        /// </summary>
        /// <param name="grdGridView"></param>
        /// <param name="dataTable"></param>
        public void GridBind(ref GridView grdGridView, DataSet dsDataSet)
        {
            grdGridView.DataSource = dsDataSet;
            grdGridView.DataBind();
        }

        /// <summary>
        /// Bind the GridView with ListTable object
        /// </summary>
        /// <param name="grdGridView"></param>
        /// <param name="dataTable"></param>
        public void GridBind(ref GridView grdGridView, object objDataObject)
        {
            grdGridView.DataSource = objDataObject;
            grdGridView.DataBind();
        }

        /// <summary>
        /// Bind the DropDownList with ListTable object
        /// </summary>
        /// <param name="drpDropDown"></param>
        /// <param name="dtDataTable"></param>
        /// <param name="IsSelectNeed"></param>
        /// <param name="strValueField"></param>
        /// <param name="strDataField"></param>
        /// <param name="strSelectValue"></param>
        public static void DropDownListBind(ref DropDownList drpDropDown, object objDataSource, bool IsSelectNeed, string strValueField, string strDataField, string strSelectValue)
        {
            drpDropDown.DataSource = objDataSource;
            drpDropDown.DataTextField = strDataField;
            drpDropDown.DataValueField = strValueField;
            drpDropDown.DataBind();
            if (IsSelectNeed)
                drpDropDown.Items.Insert(0, new ListItem(strSelectValue, "0"));
        }
        /// <summary>
        /// Bind the DropDownList with ListTable object
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="dtDataTable"></param>
        /// <param name="IsSelectNeed"></param>
        /// <param name="strValueField"></param>
        /// <param name="strDataField"></param>
        /// <param name="strSelectValue"></param>
        public void ListBoxBind(ListBox lst, object objDataSource, bool IsSelectNeed, string strValueField, string strDataField, string strSelectValue)
        {
            lst.DataSource = objDataSource;
            lst.DataTextField = strDataField;
            lst.DataValueField = strValueField;
            lst.DataBind();
            if (IsSelectNeed)
                lst.Items.Insert(0, new ListItem(strSelectValue, "0"));
        }

        /// <summary>
        /// Generate random 
        /// </summary>
        /// <param name="intLength"></param>
        /// <returns></returns>
        public string GenerateRandomString(int intLength)
        {
            string strRandom = string.Empty;
            Random rRandom = new Random();
            int intCount = 0;
            int intTemp = 0;
            while (intCount <= intLength)
            {
                intTemp = rRandom.Next(0, 9);
                if (intTemp > 0 && intTemp <= 9)
                {
                    strRandom += intTemp.ToString();
                    intCount++;
                }

            }
            return strRandom;

        }
        /// <summary>
        /// Set Dropdown Selected Value if Exists
        /// </summary>
        /// <param name="ddlItems">Dropdown as a Reference</param>
        /// <param name="value">value</param>
        public static void SetDropdownSelectedValueIfExists(ref DropDownList ddlItems, string value)
        {
            ListItem lstItem = ddlItems.Items.FindByValue(value);
            if (lstItem != null)
                ddlItems.SelectedValue = lstItem.Value;
        }
        /// <summary>
        /// Replace Invoted comma with other
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string doReplaceAndTrim(object str)
        {
            if (Convert.ToString(str) == "")
            {
                return string.Empty;
            }
            else
            {
                return str.ToString().Replace("'", "''").Trim();
            }
        }

        /// <summary>
        /// Seting for Sorting 
        /// </summary>
        /// <param name="strSortOrder">SortOrder</param>
        /// <returns></returns>
        public String GetSortingSetting(String strSortFieldId, String strSortOrder)
        {
            return strSortFieldId + "#" + (strSortOrder.ToLower() == "asc" ? "up" : "down");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        private void PrepareControlForExport(Control control)
        {

            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    switch (((LinkButton)current).Text.Trim().ToLower())
                    {
                        case "edit":
                        case "delete":
                            //IGNORE THIS.
                            break;
                        default:
                            control.Controls.AddAt(i, new LiteralControl("<span style='font-weight:bold;color:#306b9c;'>" + (current as LinkButton).Text + "</span>"));
                            break;
                    }

                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }
        /// <summary>
        /// Export GridView Data to excel
        /// </summary>
        /// <param name="rptList">Repeater</param>
        /// <param name="fileName">File name</param>
        public void Export(Repeater rptList, string fileName)
        {

            HtmlForm form1 = new HtmlForm();
            form1.Controls.Clear();
            form1.Controls.Add(rptList);
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            PrepareControlForExport(rptList);
            // PrepareControlForExport(rptList.HeaderTemplate);

            foreach (RepeaterItem rep in rptList.Items)
            {
                PrepareControlForExport(rep);
            }

            // PrepareControlForExport(rptList.FooterTemplate);
            rptList.RenderControl(htmlWrite);
            HttpContext.Current.Response.Clear();
            String str = "<table width='100%' border='0' cellspacing='1' cellpadding='3' class='border'>";
            str += stringWrite.ToString();
            str += "</table>";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
            HttpContext.Current.Response.ContentType = "Application/x-msexcel";
            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Export GridView Data to excel
        /// </summary>
        /// <param name="gvList">GridView</param>
        /// <param name="fileName">File name</param>
        public void Export(GridView gvList, string fileName)
        {

            HtmlForm form1 = new HtmlForm();
            form1.Controls.Clear();
            form1.Controls.Add(gvList);
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            PrepareControlForExport(gvList);
            // PrepareControlForExport(rptList.HeaderTemplate);

            foreach (GridViewRow rep in gvList.Rows)
            {
                PrepareControlForExport(rep);
            }

            // PrepareControlForExport(rptList.FooterTemplate);
            gvList.RenderControl(htmlWrite);
            HttpContext.Current.Response.Clear();
            String str = "<table width='100%' border='0' cellspacing='1' cellpadding='3' class='border'>";
            str += stringWrite.ToString();
            str += "</table>";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
            HttpContext.Current.Response.ContentType = "Application/x-msexcel";
            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Export file
        /// </summary>
        /// <param name="strFileName">Absolute path of file to be export</param>
        public void Export(string strFileName)
        {
            FileInfo objFile = null;
            try
            {
                objFile = new FileInfo(strFileName);
                if (objFile.Exists)
                {
                    strFileName = Path.GetFileNameWithoutExtension(objFile.Name) + "_" +
                                    objFile.LastWriteTime.ToString("yyyy-MM-dd") +
                                    Path.GetExtension(objFile.Name);
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);//TimeReport.xls");
                    System.Web.HttpContext.Current.Response.Charset = "";
                    System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    System.Web.HttpContext.Current.Response.ContentType = "application/x-msexcel";
                    System.Web.HttpContext.Current.Response.WriteFile(objFile.FullName);
                    System.Web.HttpContext.Current.Response.End();
                }

            }
            finally
            {
                if (objFile != null) { objFile = null; }
            }
        }
        /// <summary>
        /// Read Excel file and load data to DataTable.
        /// Read data from only first excel sheet.
        /// If sheet name is not specified then it will try to read from 1st sheet.
        /// </summary>
        /// <param name="strFileName">Excel file path (Absolute path)
        /// Allowlable format xls and xlsx</param>
        /// <returns>returns null if invalid excel file does not have any sheets.</returns>
        public DataTable ReadExcelFile(String strFileName)
        {
            return ReadExcelFile(strFileName, String.Empty);
        }

        /// <summary>
        /// Read Excel file and load data to DataTable.
        /// Read data from only first excel sheet.
        /// </summary>
        /// <param name="strFileName">Excel file path (Absolute path)
        /// <param name="strSheetName">Sheet Name</param>
        /// Allowlable format xls and xlsx</param>
        /// <returns>returns null if invalid excel file does not have any sheets.</returns>
        public DataTable ReadExcelFile(String strFileName, string strSheetName)
        {
            String strConnectionString = String.Empty;
            strConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + @";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"";";
            OleDbConnection oledbConn = new OleDbConnection(strConnectionString);
            try
            {
                // Open connection
                oledbConn.Open();
                if (strSheetName.Trim().Length == 0)
                {
                    DataTable dtSchema = oledbConn.GetSchema("Tables");
                    if (dtSchema == null || dtSchema.Rows.Count == 0)
                        return null;
                    //DataTable dtColumns = cnExcelConnection.GetSchema("Columns");
                    strSheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();
                    if ((strSheetName == ""))
                    {
                        return null;
                    }
                }
                // Create OleDbCommand object and select data from worksheet Sheet1
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + strSheetName + "]", oledbConn);
                // Create new OleDbDataAdapter 
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = cmd;
                // Create a DataSet which will hold the data extracted from the worksheet.
                DataSet ds = new DataSet();
                // Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                //ErrorConsole.ReportError(ex, true, true);
            }
            finally
            {
                // Close connection
                oledbConn.Close();
            }
            return null;
        }

        /// <summary>
        /// Get Record Number, by page number
        /// </summary>
        /// <param name="recNo">Record Number</param>
        /// <param name="intPageSize">Page Size</param>
        /// <param name="intPageNumber">Page Number</param>
        /// <returns>Actual Record Number</returns>
        public string GetRecordNo(object recNo, int intPageSize, int intPageNumber)
        {
            if (recNo == null || recNo == DBNull.Value)
                return string.Empty;
            return ((((intPageNumber - 1) * intPageSize)) + (Convert.ToInt32(recNo) + 1)).ToString();
        }

        /// <summary>
        /// if object is not able to be converted to int, it returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetInt(object obj)
        {
            string strObj = Convert.ToString(obj);
            int result;
            int.TryParse(strObj, out result);
            return result;
        }

        /// <summary>
        /// if object is not able to be converted to int, it returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double GetDouble(object obj)
        {
            string strObj = Convert.ToString(obj);
            double result;
            double.TryParse(strObj, out result);

            return result;
        }



        /// <summary>
        /// if object is not able to be converted to int, it returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64 GetInt64(object obj)
        {
            string strObj = Convert.ToString(obj);
            Int64 result;
            Int64.TryParse(strObj, out result);
            return result;
        }

        /// <summary>
        /// if object is not able to be converted to int, it returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? GetNullInt(object obj)
        {
            string strObj = Convert.ToString(obj);
            int result;
            bool bResult = int.TryParse(strObj, out result);
            int? result1 = new int?();
            return bResult ? result : result1;
        }

        public static DateTime GetDateForSave(string strDate)
        {
            DateTime dtFinal = new DateTime();
            string Day = "0", Year = "0", Month = "0";
            string Time = string.Empty;
            string[] strArr;

            if (strDate.Contains(" "))
            {
                Time = strDate.Substring(strDate.IndexOf(" ") + 1);
                strDate = strDate.Substring(0, strDate.IndexOf(" "));
            }

            strArr = strDate.ToString().Split('-');
            List<string> LstValues = new List<string>();

            foreach (string strKey in strArr)
            {
                if (strKey != string.Empty)
                {
                    LstValues.Add(strKey);
                }
            }
            string DateFormat = "dd-MM-yyyy";

            if (LstValues.Count == 3)
            {
                if (DateFormat == "yyyy-MM-dd")
                {
                    Year = LstValues[0];
                    Month = LstValues[1];
                    Day = LstValues[2];
                }
                else if (DateFormat == "dd-MM-yyyy")
                {
                    Day = LstValues[0];
                    Month = LstValues[1];
                    Year = LstValues[2];
                }
                else if (DateFormat == "MM-dd-yyyy")
                {
                    Month = LstValues[0];
                    Day = LstValues[1];
                    Year = LstValues[2];
                }
            }
            if (Time != string.Empty)
            {
                dtFinal = Convert.ToDateTime(Month + "-" + Day + "-" + Year + " " + Time);
            }
            else
            {
                dtFinal = Convert.ToDateTime(Month + "-" + Day + "-" + Year);
            }
            return dtFinal;
        }

        public static DateTime GetDateForSaveFormatddmmyyyy(string strDate)
        {
            DateTime dtFinal = new DateTime();
            string Day = "0", Year = "0", Month = "0";
            string Time = string.Empty;
            string[] strArr;

            if (strDate.Contains(" "))
            {
                Time = strDate.Substring(strDate.IndexOf(" ") + 1);
                strDate = strDate.Substring(0, strDate.IndexOf(" "));
            }

            strArr = strDate.ToString().Split('/');
            List<string> LstValues = new List<string>();

            foreach (string strKey in strArr)
            {
                if (strKey != string.Empty)
                {
                    LstValues.Add(strKey);
                }
            }
            string DateFormat = "dd-MM-yyyy";

            if (LstValues.Count == 3)
            {
                if (DateFormat == "yyyy-MM-dd")
                {
                    Year = LstValues[0];
                    Month = LstValues[1];
                    Day = LstValues[2];
                }
                else if (DateFormat == "dd-MM-yyyy")
                {
                    Day = LstValues[0];
                    Month = LstValues[1];
                    Year = LstValues[2];
                }
                else if (DateFormat == "MM-dd-yyyy")
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





        /// <summary>
        /// checks for the textbox value and converts it into decimal , if empty value then 0
        /// </summary>
        /// <param name="txtObject"></param>
        /// <returns></returns>
        public static Nullable<decimal> GetDecimalNullableValue(System.Web.UI.WebControls.TextBox txtObject)
        {
            if (txtObject.Text != string.Empty)
            {
                return Convert.ToDecimal(txtObject.Text.Trim().Replace(",", ""));
            }
            else
                return null;
        }

        /// <summary>
        /// return Date in short date format if it is null then it return null value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Nullable<DateTime> FormatNullDateToStore(string strDate)
        {
            if (!String.IsNullOrEmpty(strDate.Trim()))
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                return Convert.ToDateTime(strDate, culture);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// return Date in short date format if it is null then it return null value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Nullable<DateTime> FormatNullDateToStore2(string strDate)
        {
            if (!String.IsNullOrEmpty(strDate.Trim()))
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                return Convert.ToDateTime(Convert.ToDateTime(strDate), culture);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converts int to string
        /// </summary>
        /// <param name="intParameter"></param>
        /// <returns></returns>
        public static string FormatIntegerToDisplay(int? intParameter)
        {
            if (intParameter != null)
                return intParameter.ToString();
            else
                return string.Empty;
        }

        public static string ToStr(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    return Convert.ToString(readField);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// return Date in short date format if it is null then it return minimum value 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime FormatDateToStore(object objDate)
        {
            if (!String.IsNullOrEmpty(objDate.ToString()))
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                return Convert.ToDateTime(objDate.ToString(), culture);
            }
            else
            {
                return (DateTime)DateTime.MinValue;
            }
        }

        /// <summary>
        /// checks for the string value and converts it into decimal , if empty value then 0
        /// </summary>
        /// <param name="txtObject"></param>
        /// <returns></returns>
        public static Nullable<decimal> GetDecimalNullableValue(string strDecimal)
        {
            if (strDecimal != string.Empty)
            {
                return Convert.ToDecimal(strDecimal.Trim().Replace(",", ""));
            }
            else
                return null;
        }

        /// <summary>
        /// Converts decimal value to string to be displayed in labels
        /// </summary>
        /// <param name="decValue"></param>
        /// <returns></returns>
        public static string GetDecimalToDisplay(Decimal? objValue)
        {
            if (objValue == null)
                return string.Empty;
            else
                return Convert.ToDecimal(objValue).ToString("C").Replace("$", "");
        }

        /// <summary>
        /// Converts decimal value to string to be displayed in labels
        /// </summary>
        /// <param name="decValue"></param>
        /// <returns></returns>
        public static string GetDecimalToDisplay(string objValue)
        {
            if (objValue == "")
                return string.Empty;
            else
                return Convert.ToDecimal(objValue).ToString("C").Replace("$", "");
        }

        /// <summary>
        /// Converts decimal value to string to be displayed in labels
        /// </summary>
        /// <param name="decValue"></param>
        /// <returns></returns>
        public static string FormatDBNullDateToDisplay(DateTime? objValue)
        {
            if (objValue != null)
                return string.Format("{0:dd/MM/yyyy}", objValue);
            else
                return string.Empty;
        }

        /// <summary>
        /// Converts decimal value to string to be displayed in labels
        /// </summary>
        /// <param name="decValue"></param>
        /// <returns></returns>
        public static string FormatDBNullDateTimeToDisplay(string objValue)
        {
            if (!string.IsNullOrEmpty(objValue))
                return string.Format("{0:dd/MM/yyyy hh:mm tt}", Convert.ToDateTime(objValue));
            else
                return string.Empty;
        }

        /// <summary>
        /// checks for the string value and converts it into decimal , if empty value then 0
        /// </summary>
        /// <param name="txtObject"></param>
        /// <returns></returns>
        public static bool GetBoolean(string strBool)
        {
            if (strBool != string.Empty)
            {
                return Convert.ToBoolean(strBool);
            }
            else
                return false;
        }

        /// <summary>
        /// checks for the string value and converts it into decimal , if empty value then 0
        /// </summary>
        /// <param name="txtObject"></param>
        /// <returns></returns>
        public static bool GetBoolean(bool? nullBool)
        {
            if (nullBool != null)
            {
                return Convert.ToBoolean(nullBool);
            }
            else
                return false;
        }


        /// <summary>
        /// Selects the given value from the dropdown.
        /// </summary>
        /// <param name="drpDwn"></param>
        /// <param name="objVal"></param>
        /// <param name="FindByValue"></param>
        public static void SetDropdownValue(DropDownList drpDwn, object objVal, bool FindByValue)
        {
            drpDwn.ClearSelection();
            if (!string.IsNullOrEmpty(Convert.ToString(objVal)))
            {
                ListItem lst = null;
                if (FindByValue)
                    lst = drpDwn.Items.FindByValue(objVal.ToString());
                else
                    lst = drpDwn.Items.FindByText(objVal.ToString());

                if (lst != null)
                    lst.Selected = true;
            }
        }




        #endregion

        #region "Encryption / Decryption"

        public static string ShowNetworkInterfaces()
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1)
            {

                return null;
            }
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties(); //  .GetIPInterfaceProperties();
                //Console.WriteLine();
                //Console.WriteLine(adapter.Description);
                //Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                //Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                //Console.Write("  Physical address ........................ : ");
                return adapter.GetPhysicalAddress().ToString();
                //byte[] bytes = address.GetAddressBytes();
                //for (int i = 0; i < bytes.Length; i++)
                //{
                //    // Display the physical address in hexadecimal.
                //    Console.Write("{0}", bytes[i].ToString("X2"));
                //    // Insert a hyphen after each byte, unless we are at the end of the 
                //    // address.
                //    if (i != bytes.Length - 1)
                //    {
                //        Console.Write("-");
                //    }
                //}                
            }
            return null;
        }

        public string TamperProofStringEncode(string strValue, string strKey)
        {
            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strKey));
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(strValue)) + System.Convert.ToChar("-") + System.Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strValue)));
        }

        public string TamperProofStringDecode(string strValue, string strKey)
        {
            String strDataValue = "";
            String strCalcHash = "";
            strValue = strValue.Trim();
            strValue = strValue.Replace(" ", "+");

            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strKey));

            try
            {
                strDataValue = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(strValue.Split(System.Convert.ToChar("-"))[0]));

                strCalcHash = System.Text.Encoding.UTF8.GetString(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strDataValue)));
            }
            catch
            {
                throw new ArgumentException("Invalid TamperProofString");
            }
            return strDataValue;
        }


        /// <summary>
        /// Encode string
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Encode(string strValue)
        {
            if (new HandleNull().CheckNull<string>(strValue).Trim().Length == 0) return strValue;
            return TamperProofStringEncode(strValue, strTamperProofKey);
        }

        /// <summary>
        /// Decode String
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public string Decode(string strValue)
        {
            if (new HandleNull().CheckNull<string>(strValue).Trim().Length == 0) return strValue;
            return TamperProofStringDecode(strValue, strTamperProofKey);
        }



        public static decimal GetDecimal(object obj)
        {
            string strObj = Convert.ToString(obj);
            decimal result;
            decimal.TryParse(strObj, out result);
            return result;
        }
        public static DateTime GetDateTime(object obj)
        {
            string strObj = Convert.ToString(obj);
            DateTime result;
            DateTime.TryParse(strObj, out result);
            return result;
        }
        public static int getDayofweekMethod(DateTime dt)
        {
            double a = (double)Math.Floor((14 - (double)dt.Month) / 12);
            double y = (double)dt.Year - a;
            double m = (double)dt.Month + 12 * a - 2;
            double d = ((double)dt.Day + y + Math.Floor(y / 4) - Math.Floor(y / 100) + Math.Floor(y / 400) + Math.Floor((31 * m) / 12)) % (double)7;
            return General.GetInt(d);

        }

        #region Methods for Tech Tracking
        /// <summary>
        /// Creates the drop down menu.
        /// </summary>
        /// <param name="_tmpComboBox">The DropDownList.</param>
        /// <param name="_DataSource">The datasource.</param>
        /// <param name="_ValueMember">The value member.</param>
        /// <param name="_DisplayMember">The display member.</param>
        /// <param name="_DefaultDisp">The default display value (Optional).</param>
        public void BindDropDownList(ref DropDownList _tmpdropDown, DataTable _DataSource, string _ValueMember, string _DisplayMember, string _DefaultDisp)
        {
            if (_DataSource != null)
            {
                _tmpdropDown.Items.Clear();
                // Set display, value options
                _tmpdropDown.DataValueField = _ValueMember;
                _tmpdropDown.DataTextField = _DisplayMember;

                // Create custom row
                if (!string.IsNullOrEmpty(_DefaultDisp))
                {
                    DataRow _row = _DataSource.NewRow();
                    _row[1] = _DefaultDisp;
                    _row[0] = 0;

                    // Insert Custom row at beginning
                    _DataSource.Rows.InsertAt(_row, 0);
                }

                _tmpdropDown.DataSource = _DataSource;
                _tmpdropDown.DataBind();
                _tmpdropDown.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Returns a string with backslashes before characters that need to be quoted
        /// </summary>
        /// <param name="InputTxt">Text string need to be escape with slashes</param>
        public string AddSlashes(string InputTxt)
        {
            // List of characters handled:
            // \000 null
            // \010 backspace
            // \011 horizontal tab
            // \012 new line
            // \015 carriage return
            // \032 substitute
            // \042 double quote
            // \047 single quote
            // \134 backslash
            // \140 grave accent

            string Result = InputTxt;

            try
            {
                Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, @"[\000\010\011\012\015\032\042\047\134\140]", "\\$0");
            }
            catch (Exception Ex)
            {
                // handle any exception here
                Console.WriteLine(Ex.Message);
            }

            return Result;
        }
        /// <summary>
        /// Un-quotes a quoted string
        /// </summary>
        /// <param name="InputTxt">Text string need to be escape with slashes</param>
        public string StripSlashes(string InputTxt)
        {
            // List of characters handled:
            // \000 null
            // \010 backspace
            // \011 horizontal tab
            // \012 new line
            // \015 carriage return
            // \032 substitute
            // \042 double quote
            // \047 single quote
            // \134 backslash
            // \140 grave accent

            string Result = InputTxt;

            try
            {
                Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, @"(\\)([\000\010\011\012\015\032\042\047\134\140])", "$2");
            }
            catch (Exception Ex)
            {
                // handle any exception here
                Console.WriteLine(Ex.Message);
            }

            return Result;
        }
        /// <summary>
        /// Get IP Address
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            string s = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (s == "")
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            else
                return s;
        }
        #endregion

        #endregion
    }
}