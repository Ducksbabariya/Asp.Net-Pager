using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace TechTracking.Classes
{
    public class ConvertTo
    {
        /// <summary> 
        /// check for given value is null string 
        /// </summary> 
        /// <param name="readField"></param> 
        /// <returns>if value=string return string else ""</returns> 
        /// <remarks></remarks> 
        public static string String(object readField)
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
        /// check for given value is not double 
        /// </summary> 
        /// <param name="readField"></param> 
        /// <returns>if value=double return double else 0.0</returns> 
        /// <remarks></remarks> 
        public static double Double(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0.0;
                    }
                    else
                    {
                        return Convert.ToDouble(readField);
                    }
                }
                else
                {
                    return 0.0;
                }
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary> 
        /// check for given value is not decimal 
        /// </summary> 
        /// <param name="readField"></param> 
        /// <returns>if value=double return double else 0.0</returns> 
        /// <remarks></remarks> 
        public static decimal Decimal(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {

                        return 0;
                    }
                    else
                    {
                        decimal x;
                        if (decimal.TryParse(readField.ToString(), out x))
                        {
                            return x;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Convert to float
        /// </summary>
        /// <param name="readField"></param>
        /// <returns></returns>
        public static float Float(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {

                        return 0;
                    }
                    else
                    {
                        float x;
                        if (float.TryParse(readField.ToString(), out x))
                        {
                            return x;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary> 
        /// check given value is boolean or null 
        /// </summary> 
        /// <param name="readField"></param> 
        /// <returns>if value=boolen return true else false</returns> 
        /// <remarks></remarks> 
        public static bool Boolean(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    bool x;
                    bool.TryParse(Convert.ToString(readField), out x);
                    return x;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary> 
        /// check given value is interger or null 
        /// </summary> 
        /// <param name="readField"></param> 
        /// <returns>if value=integer return integer else 0</returns> 
        /// <remarks></remarks> 
        public static int Integer(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        int toReturn = 0;
                        int.TryParse(readField.ToString().Trim(), out toReturn);
                        return toReturn;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        ///// <summary> 
        ///// check given value is interger or null 
        ///// </summary> 
        ///// <param name="readField"></param> 
        ///// <returns>if value=integer return integer else 0</returns> 
        ///// <remarks></remarks> 
        //public static Int64 Integer64(object readField)
        //{
        //    if ((readField != null))
        //    {
        //        if (readField.GetType() != typeof(System.DBNull))
        //        {
        //            if (readField.ToString().Trim().Length == 0)
        //            {
        //                return 0;
        //            }
        //            else
        //            {
        //                Int64 toReturn = 0;
        //                Int64.TryParse(readField.ToString().Trim(), out toReturn);
        //                return toReturn;
        //            }
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        /// <summary> 
        /// check given value is interger or null 
        /// </summary> 
        /// <param name="readField"></param> 
        /// <returns>if value=integer return integer else 0</returns> 
        /// <remarks></remarks> 
        public static long Long(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        long toReturn = 0;
                        long.TryParse(readField.ToString().Trim(), out toReturn);
                        return toReturn;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary> 
        /// check given value is interger or null 
        /// </summary> 
        /// <param name="readField"></param> 
        /// <returns>if value=integer return integer else 0</returns> 
        /// <remarks></remarks> 
        public static short Short(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        short toReturn = 0;
                        short.TryParse(readField.ToString().Trim(), out toReturn);
                        return toReturn;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary> 
        /// check given value of date is date or null 
        /// </summary> 
        /// <param name="readField">date value to check</param> 
        /// <returns>return date if valid format else return nothing</returns> 
        /// <remarks></remarks> 
        public static DateTime Date(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    return Convert.ToDateTime(readField);
                }
            }
            return DateTime.MinValue;
        }

        /// <summary> 
        /// check given value of date is date or null 
        /// </summary> 
        /// <param name="readField">date value to check</param> 
        /// <returns>return date if valid format else return nothing</returns> 
        /// <remarks></remarks> 
        public static string DateFormat(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    return Convert.ToDateTime(readField).GetDateTimeFormats('d')[5];
                }
            }
            return string.Empty;
        }

        /// <summary> 
        /// check given value of date is date or null 
        /// </summary> 
        /// <param name="readField">date value to check</param> 
        /// <param name="dateFormat">format for which u want to check like MM/dd/yyyy</param> 
        /// <returns>return date if valid format else return nothing</returns> 
        /// <remarks></remarks> 
        public static string Date(object readField, string dateFormat)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (dateFormat != "")
                    {
                        return Convert.ToDateTime(readField).ToString(dateFormat);
                    }
                    return Convert.ToDateTime(readField).ToString();
                }
            }
            return DateTime.MinValue.ToString();
        }

        /// <summary> 
        /// for save null value in database 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public static object DBNullValue(string value)
        {
            if (value == null | string.IsNullOrEmpty(value))
            {
                return System.DBNull.Value;
            }
            return value;
        }

        /// <summary>
        /// Set Default Value to Datarow
        /// </summary>
        /// <param name="dr"></param>
        public static void DefaultValuesForDBNull(System.Data.DataRow dr)
        {
            TypeCode typeCode = default(TypeCode);

            foreach (System.Data.DataColumn col in dr.Table.Columns)
            {
                typeCode = Type.GetTypeCode(col.DataType);
                if (Convert.IsDBNull(dr[col]))
                {
                    switch (typeCode)
                    {
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Decimal:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                        case TypeCode.Byte:
                            dr[col] = 0;
                            break;
                        case TypeCode.Char:
                        case TypeCode.String:
                            dr[col] = string.Empty;
                            break;
                        case TypeCode.Boolean:
                            dr[col] = false;
                            break;
                        case TypeCode.DateTime:
                            dr[col] = DateTime.Now;
                            break;
                        default:
                            break;
                        // break 
                    }
                }
            }
        }

        /// <summary>
        /// Set Default Value for DataTable
        /// </summary>
        /// <param name="dtTable"></param>
        public static void DefaultValuesForDBNull(System.Data.DataTable dtTable)
        {
            foreach (System.Data.DataRow dr in dtTable.Rows)
            {
                DefaultValuesForDBNull(dr);
            }
        }

    }

}