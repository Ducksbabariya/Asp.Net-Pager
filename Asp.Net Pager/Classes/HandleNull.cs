using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechTracking.Classes
{
    public class HandleNull
    {
        #region Constructor
        public HandleNull()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Check for DBNULL for given Type
        /// </summary>
        /// <typeparam name="T">Type of Object to Check</typeparam>
        /// <param name="objValue">object to check</param>
        /// <returns></returns>
        public T CheckDBNull<T>(object objValue)
        {
            if (objValue != DBNull.Value)
                return (T)Convert.ChangeType(objValue, typeof(T));
            else if (typeof(T) == typeof(string))
                return (T)(object)string.Empty;
            else
                return default(T);
        }

        /// <summary>
        /// Get Nullable type object after checking DBNull
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public Nullable<T> CheckNullableDBNull<T>(object objValue) where T : struct
        {
            if (objValue != DBNull.Value)
                return (T)Convert.ChangeType(objValue, typeof(T));
            else if (typeof(T) == typeof(string))
                return (T)(object)string.Empty;
            else
                return null;
        }

        /// <summary>
        /// Check if passing object is null, if null then return default value depends on the type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objVal"></param>
        /// <returns></returns>
        public T CheckNull<T>(object objVal)
        {
            if (typeof(T) == typeof(int) && objVal != null && objVal.ToString().Length == 0)
            {
                objVal = 0;
            }
            else if (typeof(T) == typeof(Decimal) && objVal != null && objVal.ToString().Length == 0)
            {
                objVal = 0;
            }
            if (typeof(T) == typeof(DateTime) && objVal != null && objVal.ToString().Length > 0)
            {
                try
                {
                    DateTime.Parse(objVal.ToString());
                }
                catch { return default(T); }
            }
            if (objVal != null)
                return (T)Convert.ChangeType(objVal, typeof(T));
            else if (typeof(T) == typeof(string))
                return (T)(object)string.Empty;
            else
                return default(T);
        }

        /// <summary>
        /// Check if passing object is null, if null then return nullable default value depends on the type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objVal"></param>
        /// <returns></returns>
        public Nullable<T> CheckNullable<T>(object objVal) where T : struct
        {
            if (objVal != null)
            {
                if (objVal.GetType() == typeof(string))
                {
                    if (objVal == string.Empty)
                        return null;
                    else
                        return (T)Convert.ChangeType(objVal, typeof(T));
                }
                else
                    return (T)Convert.ChangeType(objVal, typeof(T));
            }
            else
                return null;
        }
        #endregion
    }
}