#region NameSpace
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

#endregion

namespace TechTracking.Classes
{

    public enum EmailTemplates
    {
        PublishShowEmailTemplate = 1,
        NewAppNotification = 2,
        NewRegistration = 3,
        WelcomeMessage = 4,
        ShareTechnician = 5,
        PositionApproval = 6,
        BidApproved = 7,
        BidApprovalNotify = 8,
        ResetPassword = 9,
        ForgetUsername = 10,
        SecondaryRegistration = 11,
        BidUpdate = 12




    }

    [Serializable]
    public class PositionTravelDays
    {
        public string PositionName;
        public int PositionId;
        public List<string> pdate;
    }
    /// <summary>
    /// Summary description for ProjectSession
    /// </summary>
    public class ProjectSession
    {
        #region Constructor

        public ProjectSession()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion


        #region Properties

        public static string ToSelectedLocations
        {
            get
            {
                return General.ToStr(HttpContext.Current.Session["ToSelectedLocations"]);
            }
            set { HttpContext.Current.Session["ToSelectedLocations"] = value; }
        }
        public static string ToSelectedPeoples
        {
            get
            {
                return General.ToStr(HttpContext.Current.Session["ToSelectedPeoples"]);
            }
            set { HttpContext.Current.Session["ToSelectedPeoples"] = value; }
        }
        public static string ToSelectedRating
        {
            get
            {
                return General.ToStr(HttpContext.Current.Session["ToSelectedRating"]);
            }
            set { HttpContext.Current.Session["ToSelectedRating"] = value; }
        }
        public static string ToSelectedGroups
        {
            get
            {
                return General.ToStr(HttpContext.Current.Session["ToSelectedGroups"]);
            }
            set { HttpContext.Current.Session["ToSelectedGroups"] = value; }
        }

        public static Dictionary<int, string> lstSelectedTechIds
        {
            get
            {
                return (Dictionary<int, string>)HttpContext.Current.Session["lstSelectedTechIds"];
            }
            set
            {
                if (value != null && value.Count > 0)
                {
                    value = value.GroupBy(pair => pair.Key)
                         .Select(group => group.First())
                         .ToDictionary(pair => pair.Key, pair => pair.Value);
                }
                HttpContext.Current.Session["lstSelectedTechIds"] = value;
            }
        }

        public static Dictionary<string, string> SelectedTechs
        {
            get
            {
                return (Dictionary<string, string>)HttpContext.Current.Session["SelectedTechs"];
            }
            set { HttpContext.Current.Session["SelectedTechs"] = value; }
        }
        public static bool RatingStaus
        {
            get
            {
                return General.GetBoolean(General.ToStr(HttpContext.Current.Session["RatingStaus"]));
            }
            set { HttpContext.Current.Session["RatingStaus"] = value; }
        }
        public static int Month
        {
            get
            {
                return General.GetInt(HttpContext.Current.Session["Month"]);
            }
            set { HttpContext.Current.Session["Month"] = value; }
        }

        public static int Year
        {
            get
            {
                return General.GetInt(HttpContext.Current.Session["Year"]);
            }
            set { HttpContext.Current.Session["Year"] = value; }
        }
        public static DateTime VisibleDate
        {
            get
            {
                return General.GetDateTime(HttpContext.Current.Session["VisibleDate"]);
            }
            set { HttpContext.Current.Session["VisibleDate"] = value; }
        }

        public static Dictionary<DateTime, string> lstShowDates
        {
            get
            {
                return (Dictionary<DateTime, string>)HttpContext.Current.Session["lstShowDates"];
            }
            set { HttpContext.Current.Session["lstShowDates"] = value; }
        }
        public static Dictionary<DateTime, string> lstTravelDates
        {
            get
            {
                return (Dictionary<DateTime, string>)HttpContext.Current.Session["lstTravelDates"];
            }
            set { HttpContext.Current.Session["lstTravelDates"] = value; }
        }


        public static int PositionID
        {
            get
            {
                return General.GetInt(HttpContext.Current.Session["PositionID"]);
            }
            set { HttpContext.Current.Session["PositionID"] = value; }
        }
        public static DateTime ShowStartDate
        {
            get
            {
                return (DateTime)HttpContext.Current.Session["ShowStartDate"];
            }
            set { HttpContext.Current.Session["ShowStartDate"] = value; }
        }
        public static DateTime ShowEndDate
        {
            get
            {
                return (DateTime)HttpContext.Current.Session["ShowEndDate"];
            }
            set { HttpContext.Current.Session["ShowEndDate"] = value; }
        }
        public static List<string> lstTravelDays
        {
            get
            {
                return (List<string>)HttpContext.Current.Session["lstTravelDays"];
            }
            set { HttpContext.Current.Session["lstTravelDays"] = value; }
        }
        public static List<PositionTravelDays> lstPositionTravelDays
        {
            get
            {
                return (List<PositionTravelDays>)HttpContext.Current.Session["lstPositionTravelDays"];
            }
            set { HttpContext.Current.Session["lstPositionTravelDays"] = value; }
        }
        public static int AUserID
        {
            get
            {
                if (HttpContext.Current.Session["AUserId"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertTo.Integer(HttpContext.Current.Session["AUserId"].ToString());
                }
            }
            set { HttpContext.Current.Session["AUserId"] = value; }
        }

        public static int UserID
        {
            get
            {
                if (HttpContext.Current.Session["UserID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertTo.Integer(HttpContext.Current.Session["UserID"].ToString());
                }
            }
            set { HttpContext.Current.Session["UserID"] = value; }
        }
        public static string SSN
        {
            get
            {
                if (HttpContext.Current.Session["SSN"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return ConvertTo.String(HttpContext.Current.Session["SSN"]);
                }
            }
            set { HttpContext.Current.Session["SSN"] = value; }
        }
        public static int LevelID
        {
            get
            {
                if (HttpContext.Current.Session["LevelID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ConvertTo.Integer(HttpContext.Current.Session["LevelID"].ToString());
                }
            }
            set { HttpContext.Current.Session["LevelID"] = value; }
        }
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["UserName"] == null)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["UserName"]);
                }
            }
            set { HttpContext.Current.Session["UserName"] = value; }
        }

        //Rename UserType to RoleType
        public static string RoleType
        {
            get
            {
                if (HttpContext.Current.Session["RoleType"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["RoleType"].ToString();
                }
            }
            set { HttpContext.Current.Session["RoleType"] = value; }
        }
        /// <summary>
        /// Profile Image
        /// </summary>
        public static string ProfileImage
        {
            get
            {
                if (HttpContext.Current.Session["ProfileImage"] == null)
                {
                    return "";
                }
                else
                {
                    return HttpContext.Current.Session["ProfileImage"].ToString();
                }
            }
            set { HttpContext.Current.Session["ProfileImage"] = value; }
        }

        public static string EmailAddress
        {
            get
            {
                if (HttpContext.Current.Session["EmailAddress"] == null)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["EmailAddress"]);
                }
            }
            set { HttpContext.Current.Session["EmailAddress"] = value; }
        }

        public static string FirstName
        {
            get
            {
                if (HttpContext.Current.Session["FirstName"] == null)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["FirstName"]);
                }
            }
            set { HttpContext.Current.Session["FirstName"] = value; }
        }

        public static string Rank
        {
            get
            {
                if (HttpContext.Current.Session["Rank"] == null)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["Rank"]);
                }
            }
            set { HttpContext.Current.Session["Rank"] = value; }
        }

        public static string Rating
        {
            get
            {
                if (HttpContext.Current.Session["Rating"] == null)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["Rating"]);
                }
            }
            set { HttpContext.Current.Session["Rating"] = value; }
        }

        public static string LastName
        {
            get
            {
                if (HttpContext.Current.Session["LastName"] == null)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["LastName"]);
                }
            }
            set { HttpContext.Current.Session["LastName"] = value; }
        }
        public static string NewMessages
        {
            get
            {
                if (HttpContext.Current.Session["NewMessages"] == null)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["NewMessages"]);
                }
            }
            set { HttpContext.Current.Session["NewMessages"] = value; }
        }
        public static int NewProjects
        {
            get
            {

                return General.GetInt(HttpContext.Current.Session["NewProjects"]);

            }
            set { HttpContext.Current.Session["NewProjects"] = value; }
        }
        public static string Password
        {
            get
            {
                if (HttpContext.Current.Session["Password"] == null)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["Password"]);
                }
            }
            set { HttpContext.Current.Session["Password"] = value; }
        }

        public static string Theme
        {
            get
            {
                if (HttpContext.Current.Session["Theme"] == null)
                {
                    return ProjectConfiguration.Theme;
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["Theme"]);
                }
            }
            set { HttpContext.Current.Session["Theme"] = value; }
        }

        public static string Culture
        {
            get
            {
                if (HttpContext.Current.Session["Culture"] == null)
                {
                    return ProjectConfiguration.Culture;
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["Culture"]);
                }
            }
            set { HttpContext.Current.Session["Culture"] = value; }
        }

        public static string TechName
        {
            get
            {
                if (HttpContext.Current.Session["TechName"] == null)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString(HttpContext.Current.Session["TechName"]);
                }
            }
            set { HttpContext.Current.Session["TechName"] = value; }
        }

        #region Methods to set sessions

        /// <summary>
        /// set the project sessions
        /// </summary>
        /// <param name="dataTableAccount"></param>
        public static void SetAdminProjectSessions(DataTable dataTableAccount)
        {
            ProjectSession.UserID = ConvertTo.Integer(dataTableAccount.Rows[0]["PKAccountID"]);
            ProjectSession.FirstName = Convert.ToString(dataTableAccount.Rows[0]["FirstName"]);
            ProjectSession.UserName = ConvertTo.String(dataTableAccount.Rows[0]["UserName"]);
            ProjectSession.Password = EncryptionDecryption.GetDecrypt(dataTableAccount.Rows[0]["Password"].ToString());
            ProjectSession.EmailAddress = Convert.ToString(dataTableAccount.Rows[0]["Email"]);
            //ProjectSession.AdminDispalyName = Convert.ToString(dataTableUser.Rows[0]["DisplayName"]);
            ProjectSession.LevelID = ConvertTo.Integer(dataTableAccount.Rows[0]["FKLevelID"]);
            ProjectSession.RoleType = ConvertTo.String(dataTableAccount.Rows[0]["LevelName"]);
            ProjectSession.ProfileImage = Convert.ToString(dataTableAccount.Rows[0]["ProfileImagePath"]);

        }


        public static void SetTechnicianProjectSessions(DataTable dataTableAccount)
        {
            ProjectSession.UserID = ConvertTo.Integer(dataTableAccount.Rows[0]["PKTechnicianID"]);
            ProjectSession.SSN = Convert.ToString(dataTableAccount.Rows[0]["SSN"]);
            //ProjectSession.UserName = ConvertTo.String(dataTableAccount.Rows[0]["UserName"]);
            //ProjectSession.Password = EncryptionDecryption.GetDecrypt(dataTableAccount.Rows[0]["Password"].ToString());
            //ProjectSession.EmailAddress = Convert.ToString(dataTableAccount.Rows[0]["Email"]);
            ////ProjectSession.AdminDispalyName = Convert.ToString(dataTableUser.Rows[0]["DisplayName"]);
            ProjectSession.LevelID = ConvertTo.Integer(dataTableAccount.Rows[0]["FKLevelID"]);
            //ProjectSession.RoleType = ConvertTo.String(dataTableAccount.Rows[0]["LevelName"]);
            //ProjectSession.ProfileImage = Convert.ToString(dataTableAccount.Rows[0]["PhotoPath"]);
            //ProjectSession.NewMessages = Convert.ToString(dataTableAccount.Rows[0]["NewMessages"]);
            //ProjectSession.Rank = ConvertTo.String(dataTableAccount.Rows[0]["Rank"]);
            //ProjectSession.Rating = ConvertTo.Float(dataTableAccount.Rows[0]["Rating"]).ToString("F2");

        }
        public static void SetTechInfo(DataTable dataTableAccount)
        {
            ProjectSession.UserID = ConvertTo.Integer(dataTableAccount.Rows[0]["PKTechnicianID"]);
            ProjectSession.FirstName = Convert.ToString(dataTableAccount.Rows[0]["FirstName"]);
            ProjectSession.SSN = Convert.ToString(dataTableAccount.Rows[0]["SSN"]);
            ProjectSession.UserName = ConvertTo.String(dataTableAccount.Rows[0]["UserName"]);
            ProjectSession.Password = EncryptionDecryption.GetDecrypt(dataTableAccount.Rows[0]["Password"].ToString());
            ProjectSession.EmailAddress = Convert.ToString(dataTableAccount.Rows[0]["Email"]);
            //ProjectSession.AdminDispalyName = Convert.ToString(dataTableUser.Rows[0]["DisplayName"]);
            ProjectSession.LevelID = ConvertTo.Integer(dataTableAccount.Rows[0]["FKLevelID"]);
            ProjectSession.ProfileImage = Convert.ToString(dataTableAccount.Rows[0]["PhotoPath"]);
            ProjectSession.NewMessages = Convert.ToString(dataTableAccount.Rows[0]["NewMessages"]);
            ProjectSession.Rank = ConvertTo.String(dataTableAccount.Rows[0]["Rank"]);
            ProjectSession.Rating = ConvertTo.Float(dataTableAccount.Rows[0]["Rating"]).ToString("F2");
            ProjectSession.NewProjects = ConvertTo.Integer(dataTableAccount.Rows[0]["NewProjects"]); //General.ToStr(dataTableAccount.Rows[0]["ArrowStatus"])
            ProjectSession.RatingStaus = Convert.ToBoolean(dataTableAccount.Rows[0]["ArrowStatus"]);

        }
        /// <summary>
        /// Logout Session and Redirect to the Login Page
        /// </summary>
        public static void LogOut()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/Login.aspx");
        }

        /// <summary>
        /// Logout Session and Redirect to the Admin Login Page
        /// </summary>
        public static void AdminLogOut()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/Admin/Login.aspx");
        }
        #endregion
        #endregion
    }

}