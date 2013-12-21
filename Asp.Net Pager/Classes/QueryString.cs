using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace TechTracking.Classes
{

    /// <summary>
    /// Summary description for QueryString
    /// </summary>
    public class QueryString
    {
        public QueryString()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string GUIDKey
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["GUIDKey"].ToString(); }
        }

        static public string PMSStringEncode(string value, string key)
        {
            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value)) + System.Convert.ToChar("-") + System.Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
        }
        static public string PMSStringDecode(string value, string key)
        {
            String dataValue = "";
            String calcHash = "";
            String storedHash = "";

            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));

            try
            {
                dataValue = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(value.Split(System.Convert.ToChar("-"))[0]));
                storedHash = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(value.Split(System.Convert.ToChar("-"))[1]));
                calcHash = System.Text.Encoding.UTF8.GetString(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataValue)));

                if (storedHash != calcHash)
                {
                    //Data was corrupted
                    throw new ArgumentException("Hash value does not match");
                    //This error is immediately caught below
                }

            }
            catch (System.Exception)
            {
                //throw new ArgumentException("Invalid DezisionString");
            }
            return dataValue;
        }

        static public string QueryStringEncode(string value)
        {
            return System.Web.HttpUtility.UrlEncode(PMSStringEncode(value, GUIDKey));
        }
        static public string QueryStringDecode(string value)
        {
            return PMSStringDecode(value, GUIDKey);
        }

        public static Hashtable GetQueryString(string DataValue)
        {

            string[] AndArray = DataValue.Split(new Char[] { '&' });
            Hashtable objHash = new Hashtable();
            string[] splitArray;
            try
            {
                if (AndArray.Length > 0)
                {
                    for (int i = 0; i <= AndArray.Length - 1; i++)
                    {
                        splitArray = AndArray[i].Split(new Char[] { '=' });
                        objHash.Add(splitArray[0], splitArray[1]);
                    }
                }
            }
            catch (Exception)
            {
                return objHash;
            }
            return objHash;
        }

        public static void DecodeQueryString(string strData, ref string strmainVariable, string strParameter)
        {
            if (strData != null)
            {
                string DataString = QueryString.QueryStringDecode(strData);
                Hashtable objHash = QueryString.GetQueryString(DataString);

                strmainVariable = ConvertTo.String(objHash[strParameter]);
            }
        }
    }
}