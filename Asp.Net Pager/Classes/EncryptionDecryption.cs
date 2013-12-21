using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
namespace TechTracking.Classes
{
    /// <summary>
    /// Summary description for clsEncryptionDecryption
    /// </summary>
    public class EncryptionDecryption
    {

        public EncryptionDecryption()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static string keyString = "552F79D3-1F36-48ab-934C-4629C2274D43";
        private const string strTamperProofKey = "astkvsnanvpi";
        public static string TamperProofStringEncode(string strValue, string strKey)
        {
            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strKey));
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(strValue)) + System.Convert.ToChar("-") + System.Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strValue)));
        }

        public static string TamperProofStringDecode(string strValue, string strKey)
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
        /// Get Encrpted Value of Passed value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetEncrypt(string value)
        {
            if (new HandleNull().CheckNull<string>(value).Trim().Length == 0) return value;
            return TamperProofStringEncode(value, strTamperProofKey);
            //return Encrypt(keyString, value);
        }

        /// <summary>
        /// Get Decrypted value of passed encrypted string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetDecrypt(string value)
        {
            if (new HandleNull().CheckNull<string>(value).Trim().Length == 0) return value;
            return TamperProofStringDecode(value, strTamperProofKey);
            //return Decrypt(keyString, value);
        }

        /// <summary>
        /// Encrypt value
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string Encrypt(string Passphrase, string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        /// <summary>
        /// decrypt value
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strData"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string Decrypt(string Passphrase, string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            catch
            {
                return "";
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }
}