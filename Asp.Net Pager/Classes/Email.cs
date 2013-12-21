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
#endregion

namespace TechTracking.Classes
{

    /// <summary>
    /// This class is used to Send Email to users
    /// <Created By> Hardik Panchal - 31 Oct 2011</Created>
    /// </summary>
    public static class Email
    {
        #region Constructor



        public enum EmailType
        {
            Default
        };

        #endregion

        #region Methods

        /// <summary>
        /// Sending An Email 
        /// </summary>
        /// <param name="MailFrom"></param>
        /// <param name="MailTo"></param>
        /// <param name="MailCC"></param>
        /// <param name="MailBCC"></param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="Attachment"></param>
        /// <param name="IsBodyHtml"></param>
        /// <returns></returns>
        public static bool Send(string MailFrom, string MailTo, string MailCC, string MailBCC, string Subject, string Body, string Attachment, bool IsBodyHtml, EmailType emailType = EmailType.Default)
        {

            string str = string.Empty;
            System.Net.Mail.MailMessage MailMesg = new System.Net.Mail.MailMessage(MailFrom, MailTo);

            if (MailCC != string.Empty)
            {
                string[] mailCC = MailCC.Split(';');
                foreach (string email in mailCC)
                    MailMesg.CC.Add(email);
            }

            if (MailBCC != string.Empty)
            {
                //string[] mailBCC = MailBCC.Split(';');
                //foreach (string email in mailBCC)
                MailBCC = MailBCC.Replace(";", ",");
                MailMesg.Bcc.Add(MailBCC);
            }

            if (!string.IsNullOrEmpty(Attachment))
            {
                string[] attachment = Attachment.Split(';');
                foreach (string attachFile in attachment)
                {
                    try
                    {
                        System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(attachFile);
                        MailMesg.Attachments.Add(attach);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            MailMesg.Subject = Subject;
            MailMesg.Body = GetMasterEmailBody(Body, emailType);
            MailMesg.IsBodyHtml = IsBodyHtml;

            System.Net.Mail.SmtpClient objSMTP = new System.Net.Mail.SmtpClient();

            System.Net.NetworkCredential myCredential = new System.Net.NetworkCredential();
            myCredential.UserName = ProjectConfiguration.SMTPEmail;
            myCredential.Password = ProjectConfiguration.Password;
            objSMTP.Credentials = myCredential;
            objSMTP.Host = ProjectConfiguration.SMTPHostName;
            objSMTP.Port = ProjectConfiguration.Port;
            try
            {
                objSMTP.Send(MailMesg);
                return true;
            }
            catch (Exception ex)
            {
                str = ex.Message;
                MailMesg.Dispose();
                MailMesg = null;
            }
            return false;
        }

        /// <summary>
        /// Get the Master Email Format
        /// </summary>
        /// <param name="Body"></param>
        /// <param name="ToName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string GetMasterEmailBody(string Body, EmailType emailType)
        {
            string filePath = string.Empty;
            switch (emailType)
            {
                case EmailType.Default:
                    // filePath = ProjectConfiguration.ApplicationRootPath + "/EmailTemplate/Master.htm";
                    break;
            }
            if (filePath != string.Empty && System.IO.File.Exists(filePath))
            {
                StreamReader _srdr = new StreamReader(filePath);
                string _strMailBody = _srdr.ReadToEnd();
                _srdr.Close();
                //_strMailBody = _strMailBody.Replace("##SiteHeaderName##", ProjectConfiguration.SiteNameHeader);
                //_strMailBody = _strMailBody.Replace("##SiteName##", ProjectConfiguration.SiteName);
                //_strMailBody = _strMailBody.Replace("##TODAYDATE##", System.DateTime.Today.ToString("dd/MM/yyyy"));
                //_strMailBody = _strMailBody.Replace("##USERNAME##", ToName);
                _strMailBody = _strMailBody.Replace("[@EMAILBODY]", Body);
                //_strMailBody = _strMailBody.Replace("##SiteUrl##", ProjectConfiguration.UrlBase);
                //_strMailBody = _strMailBody.Replace("#ImagePath#", ProjectConfiguration.Images + "/main");

                return _strMailBody;
            }
            else
                return Body;
        }

        /// <summary>
        /// Make change HTML content
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static string stripHTML(string htmlText)
        {
            string ret = ConvertTo.String(htmlText);
            ret = ret.Replace("<", "&lt;");
            ret = ret.Replace(">", "&gt;");
            return ret;
        }

        /// <summary>
        /// Read the Email Template File
        /// </summary>
        /// <param name="dtTable"></param>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetEmailText(DataTable dtTable, System.Text.StringBuilder strText)
        {
            string strTemplate = "[@{0}]";
            System.Text.StringBuilder strTextToReplace = new System.Text.StringBuilder();
            System.Text.StringBuilder strTemp = new System.Text.StringBuilder();
            foreach (DataColumn col in dtTable.Columns)
            {

                strTextToReplace.Length = 0;
                strTextToReplace.Append(string.Format(strTemplate, col.ColumnName));
                strTemp.Length = 0;
                strTemp.Append(strText.Replace(strTextToReplace.ToString(), stripHTML(dtTable.Rows[0][col].ToString())));
                strText.Length = 0;
                strText.Append(strTemp);
            }
            return strTemp.ToString();
        }

        #endregion
    }
}