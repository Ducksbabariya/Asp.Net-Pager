using System;
using System.IO;
using System.Text;
using TechTracking.Classes;

namespace TechTracking
{
    public partial class Download : System.Web.UI.Page
    {
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAFile();
        }
        #endregion
        #region Download File
        public void GetAFile()
        {
            // HttpPostedFileBase file = Request.Files[0];
            string Attachment = Request.QueryString["ATTACH"];
            string ID = Server.MapPath("~/Resume/" + Request.QueryString["Path"]);
            FileStream f = new FileStream(ID, FileMode.OpenOrCreate);

            FileInfo File = new FileInfo(ID);
            int intID;
            //int Size = File.ContentLength;
            int Size = (int)Math.Ceiling((decimal)File.Length);
            string FileName = File.Name;
            string ContentType = GetContentType(ID);
            Byte[] FileData = new Byte[Size];
            f.Read(FileData, 0, Size);
            f.Close();
            //File.InputStream.Read(FileData, 0, Size);
            Response.ContentType = ContentType;
            StringBuilder SB = new StringBuilder();
            if (Attachment == "YES")
            {
                SB.Append("attachment; ");
            }
            SB.Append("filename=");
            SB.Append(FileName);
            Response.AddHeader("Content-Disposition", SB.ToString());
            Response.BinaryWrite(FileData);
            Response.Flush();
            Response.End();


        }
        private string GetContentType(string fileName)
        {
            string contentType = "application/octetstream";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (registryKey != null && registryKey.GetValue("Content Type") != null)
                contentType = registryKey.GetValue("Content Type").ToString();
            return contentType;
        }
        #endregion
    }
}