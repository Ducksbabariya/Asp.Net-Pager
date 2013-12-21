#region NameSpace
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using System.Collections.Generic;

//using System.Web;
#endregion

namespace TechTracking.Classes
{

    /// <summary>
    /// Summary description for Configuration
    /// </summary>
    public class ProjectConfiguration
    {

        #region Constructor

        public ProjectConfiguration()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region Variable
        private static string _RootPath;
        private static string _Culture;
        private static string _Theme;
        #endregion

        #region Methods

        /// <summary>
        /// Set the Default Value in Projects
        /// </summary>
        /// <param name="RootPath"></param>
        public static void OnApplicationStart(string RootPath)
        {
            _RootPath = RootPath;
            _Culture = Convert.ToString(ConfigurationManager.AppSettings["Culture"]);
            _Theme = Convert.ToString(ConfigurationManager.AppSettings["Theme"]);
        }

        #endregion

        #region Property

        #region AppSettings Key
        public static List<string> fromEmails
        {
            get
            {
                return ConfigurationManager.AppSettings["fromEmails"].Split(',').ToList<string>();
            }
        }
        /// <summary>
        /// Return TechnicianPhotoBasePath
        /// </summary>
        public static string DiscussionDocsBasePath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DiscussionDocsBasePath"]) + "/");
                //return HttpRuntime.AppDomainAppPath + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechnicianPhotoBasePath"]) + "/";
            }
        }
        /// <summary>
        /// Return TechnicianPhotoBasePath
        /// </summary>
        public static string TechnicianDocsBasePath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechnicianDocsBasePath"]) + "/");
                //return HttpRuntime.AppDomainAppPath + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechnicianPhotoBasePath"]) + "/";
            }
        }

        /// <summary>
        /// Return TechnicianPhotoBasePath
        /// </summary>
        public static string TechniciantempDocsBasePath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechniciantempDocsBasePath"]) + "/");
                //return HttpRuntime.AppDomainAppPath + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechniciantempPhotoBasePath"]) + "/";
            }
        }



        /// <summary>
        /// Return TechnicianPhotoBasePath
        /// </summary>
        public static string TechnicianPhotoBasePath
        {
            get
            { 
                return HttpContext.Current.Server.MapPath("~/" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechnicianPhotoBasePath"]) + "/"); 
                //return HttpRuntime.AppDomainAppPath + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechnicianPhotoBasePath"]) + "/";
            }
        }

        /// <summary>
        /// Return TechnicianPhotoBasePath
        /// </summary>
        public static string TechniciantempPhotoBasePath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechniciantempPhotoBasePath"]) + "/"); 
                //return HttpRuntime.AppDomainAppPath + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechniciantempPhotoBasePath"]) + "/";
            }
        }

        /// <summary>
        /// Return TechnicianPhotoBasePath
        /// </summary>
        public static string AccountPhotoBasePath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["AccountPhotoBasePath"]) + "/");
                //return HttpRuntime.AppDomainAppPath + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["AccountPhotoBasePath"]) + "/";
            }
        }
        /// <summary>
        /// Return TechnicianResumeBasePath
        /// </summary>
        public static string TechnicianResumeBasePath
        {
            get 
            { 
                return HttpContext.Current.Server.MapPath("~/" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechnicianResumeBasePath"]) + "/");
                //return HttpRuntime.AppDomainAppPath + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechnicianResumeBasePath"]) + "/";
            }
        }

        /// <summary>
        /// Return TechnicianResumeBasePath
        /// </summary>
        public static string TechniciantempResumeBasePath
        {
            get
            { 
                return HttpContext.Current.Server.MapPath("~/" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechniciantempResumeBasePath"]) + "/"); 
                //return HttpRuntime.AppDomainAppPath + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TechniciantempResumeBasePath"]) + "/";
            }
        }
        /// <summary>
        /// Return Culture
        /// </summary>
        public static string Culture
        {
            get { return _Culture; }
        }
        /// <summary>
        /// Return Theme
        /// </summary>
        public static string Theme
        {
            get { return _Theme; }
        }

        /// <summary>
        /// Support Email
        /// </summary>
        public static string SupportEmail
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SupportEmail"]); }
        }

        /// <summary>
        /// Admin Email
        /// </summary>
        public static string AdminEmail
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["AdminEmail"]); }
        }
        /// <summary>
        /// Return Error SiteUrl
        /// </summary>
        public static string NewAppEmail
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["NewAppEmail"]); }
        }
        /// <summary>
        /// Return Error SiteUrl
        /// </summary>
        public static string SiteUrl
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SiteUrl"]); }
        }
        /// <summary>
        /// Return Error Email ID
        /// </summary>
        public static string ErrorEmail
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ErrorEmail"]); }
        }
        /// <summary>
        /// Returns From Email ID
        /// </summary>
        public static int Port
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"]); }
        }
        /// <summary>
        /// Returns From Email ID
        /// </summary>
        public static string Password
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Password"]); }
        }
        /// <summary>
        /// Returns From Email ID
        /// </summary>
        public static string SMTPHostName
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTPHostName"]); }
        }
        /// <summary>
        /// Returns From Email ID
        /// </summary>
        public static string FromEmail
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FromEmail"]); }
        }
        /// <summary>
        /// Returns From Email ID
        /// </summary>
        public static string ShareEmail
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ShareEmail"]); }
        }
        /// <summary>
        /// Returns SMTPEmail
        /// </summary>
        public static string SMTPEmail
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTPEmail"]); }
        }
        /// <summary>
        /// Returns CC Email ID
        /// </summary>
        public static string CC
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CC"]); }
        }
        /// <summary>
        /// Returns BCC Email ID
        /// </summary>
        public static string BCC
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["BCC"]); }
        }
        /// <summary>
        /// Returns Valid Email Expression
        /// </summary>
        public static string ValidEmail
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ValidEmail"]); }
        }
        /// <summary>
        /// Returns Valid Photo Expression
        /// </summary>
        public static string ValidPhoto
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ValidPhoto"]); }
        }
        /// <summary>
        /// Returns Valid File Expression
        /// </summary>
        public static string ValidFile
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ValidFile"]); }
        }
        /// <summary>
        /// Returns Valid Document Expression
        /// </summary>
        public static string ValidDocument
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ValidDocument"]); }
        }
        /// <summary>
        /// Returns Valid PDF Expression
        /// </summary>
        public static string ValidPDF
        {
            get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ValidPDF"]); }
        }
        /// <summary>
        /// Returns Page Size
        /// </summary>
        public static int PageSize
        {
            get { return ConvertTo.Integer(System.Configuration.ConfigurationManager.AppSettings["PageSize"]); }
        }


        #endregion

        #region System Path

        /// <summary>
        /// Return the Root Path of the Project
        /// </summary>
        public static string ApplicationRootPath
        {
            get
            {
                if (_RootPath.EndsWith("\\"))
                {
                    return _RootPath;
                }
                else
                {
                    return _RootPath + "\\";
                }
            }

        }

        /// <summary>
        /// Return HostName
        /// </summary>
        public static string HostName
        {
            get { return HttpContext.Current.Request.Url.Host; }
        }

        /// <summary>
        /// Retrun Url Suffix
        /// </summary>
        private static string UrlSuffix
        {
            get
            {
                if (HttpContext.Current.Request.ApplicationPath == "/")
                {
                    return HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                    //return "121.247.170.47" + HttpContext.Current.Request.ApplicationPath;
                }
                else
                {
                    return HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/";
                    //return "121.247.170.47" + HttpContext.Current.Request.ApplicationPath + "/";
                }
            }

        }

        /// <summary>
        /// Retrun Secure User Base
        /// </summary>
        public static string SecureUrlBase
        {
            get
            {
                return "https://" + UrlSuffix;
            }

        }

        /// <summary>
        /// Retrun Url Base
        /// </summary>
        public static string UrlBase
        {
            get
            {
                return "http://" + UrlSuffix;
            }
        }

        /// <summary>
        /// Return Image Path
        /// </summary>
        public static string Images
        {
            get
            {
                if (HttpContext.Current.Request.IsSecureConnection)
                {
                    return SecureUrlBase + "App_Themes/" + ProjectSession.Theme + "/Images/";
                }
                else
                {
                    return UrlBase + "App_Themes/" + ProjectSession.Theme + "/Images/";
                }
            }
        }

        //for Common Images
        public static string ImagesDefault
        {
            get
            {
                if (HttpContext.Current.Request.IsSecureConnection)
                {
                    return SecureUrlBase + "App_Themes/Default/Images/";
                }
                else
                {
                    return UrlBase + "App_Themes/Default/Images/";
                }
            }
        }

        /// <summary>
        /// Return JS Path
        /// </summary>
        public static string JavaScript
        {
            get
            {
                if (HttpContext.Current.Request.IsSecureConnection)
                {
                    return SecureUrlBase + "Scripts";
                }
                else
                {
                    return UrlBase + "Scripts";
                }
            }
        }

        #endregion

        #region Other Setting

        public static string DateFormat
        {
            get { return System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ToString(); }// "dd/MM/yyyy"; }
        }

        #endregion

        #endregion
    }

}