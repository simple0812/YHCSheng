using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YHCSheng.Utils
{
    public class Null
    {
        public Null()
        {
        }

        /// <summary>
        /// Default value of null short 
        /// </summary>
        private const short nullShort = -1;

        /// <summary>
        /// Default value of null int
        /// </summary>
        private const int nullInt = -1;

        /// <summary>
        /// Get default value if short value is null
        /// </summary>
        public static short NullShort
        {
            get { return nullShort; }
        }

        /// <summary>
        /// Get default value if integer value is null
        /// </summary>
        public static int NullInteger
        {
            get { return nullInt; }
        }

        /// <summary>
        /// Get default value if single value is null
        /// </summary>
        public static float NullSingle
        {
            get { return float.MinValue; }
        }

        /// <summary>
        /// Get default value if double value is null
        /// </summary>
        public static double NullDouble
        {
            get { return double.MinValue; }
        }

        /// <summary>
        /// Get default value if decimal value is null
        /// </summary>
        public static decimal NullDecimal
        {
            get { return decimal.MinValue; }
        }

        /// <summary>
        /// Get default value if Date value is null
        /// </summary>
        public static DateTime NullDate
        {
            get { return DateTime.MinValue; }
        }

        /// <summary>
        /// Get default value if string value is null
        /// </summary>
        public static string NullString
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Get default value if bool value is null
        /// </summary>
        public static bool NullBoolean
        {
            get { return false; }
        }

        /// <summary>
        /// Get default value if Guid value is null
        /// </summary>
        public static Guid NullGuid
        {
            get { return Guid.Empty; }
        }

        /// <summary>
        /// Convert an object null value to default value
        /// </summary>
        /// <param name="instanceValue">instance value</param>
        /// <param name="instance">object</param>
        /// <returns>updated object</returns>
        public static object SetNull(object instanceValue, object instance)
        {
            if (Convert.IsDBNull(instanceValue))
            {
                if (instance is short)
                {
                    return NullShort;
                }
                else if (instance is int)
                {
                    return NullInteger;
                }
                else if (instance is Single)
                {
                    return NullSingle;
                }
                else if (instance is double)
                {
                    return NullDouble;
                }
                else if (instance is decimal)
                {
                    return NullDecimal;
                }
                else if (instance is DateTime)
                {
                    return NullDate;
                }
                else if (instance is string)
                {
                    return NullString;
                }
                else if (instance is bool)
                {
                    return NullBoolean;
                }
                else if (instance is Guid)
                {
                    return NullGuid;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return instanceValue;
            }
        }

        /// <summary>
        /// Set an object instance property to null value
        /// </summary>
        /// <param name="propertyInfo">object property</param>
        /// <returns>object instance</returns>
        public static object SetNull(PropertyInfo propertyInfo)
        {
            switch (propertyInfo.PropertyType.ToString())
            {
                case "System.Int16":
                    return NullShort;
                case "System.Int32":
                    return NullInteger;
                case "System.Int64":
                    return NullInteger;
                case "System.Single":
                    return NullSingle;
                case "System.Double":
                    return NullDouble;
                case "System.Decimal":
                    return NullDecimal;
                case "System.DateTime":
                    return NullDate;
                case "System.String":
                    return NullString;
                case "System.Char":
                    return NullString;
                case "System.Boolean":
                    return NullBoolean;
                case "System.Guid":
                    return NullGuid;
                default:
                    //Type type = propertyInfo.PropertyType;
                    //if (type.BaseType.Equals(typeof(System.Enum)))
                    //{
                    //    System.Array enumValues = System.Enum.GetValues(type);
                    //    Array.Sort(enumValues);
                    //    return System.Enum.ToObject(type, enumValues.GetValue(0));
                    //}
                    //else
                    //{
                    return null;
                //}
            }
        }

        /// <summary>
        /// Validate whether object instance is null, and return specified null value
        /// </summary>
        /// <param name="instance">object</param>
        /// <param name="dbNull">defalut value</param>
        /// <returns>object</returns>
        public static object GetNull(object instance, object dbNull)
        {
            if (instance == null)
            {
                return dbNull;
            }
            else if (instance is short)
            {
                if (Convert.ToInt16(instance) == NullShort)
                {
                    return dbNull;
                }
            }
            else if (instance is int)
            {
                if (Convert.ToInt32(instance) == NullInteger)
                {
                    return dbNull;
                }
            }
            else if (instance is Single)
            {
                if (Convert.ToSingle(instance) == NullSingle)
                {
                    return dbNull;
                }
            }
            else if (instance is double)
            {
                if (Convert.ToDouble(instance) == NullDouble)
                {
                    return dbNull;
                }
            }
            else if (instance is decimal)
            {
                if (Convert.ToDecimal(instance) == NullDecimal)
                {
                    return dbNull;
                }
            }
            else if (instance is DateTime)
            {
                if (Convert.ToDateTime(instance).Date == NullDate.Date)
                {
                    return dbNull;
                }
            }
            else if (instance is string)
            {
                if (instance == null)
                {
                    return dbNull;
                }
                else
                {
                    if (instance.ToString() == NullString)
                    {
                        return dbNull;
                    }
                }
            }
            else if (instance is bool)
            {
                if (Convert.ToBoolean(instance) == NullBoolean)
                {
                    return dbNull;
                }
            }
            else if (instance is Guid)
            {
                if (((System.Guid)instance).Equals(NullGuid))
                {
                    return dbNull;
                }
            }
            return instance;
        }

        /// <summary>
        /// Valiate whether object instance is null
        /// </summary>
        /// <param name="instance">object</param>
        /// <returns>true: null, false: not null</returns>
        public static bool IsNull(object instance)
        {
            if (instance != null)
            {
                if (instance is int)
                {
                    return instance.Equals(NullInteger);
                }
                else if (instance is Single)
                {
                    return instance.Equals(NullSingle);
                }
                else if (instance is double)
                {
                    return instance.Equals(NullDouble);
                }
                else if (instance is decimal)
                {
                    return instance.Equals(NullDecimal);
                }
                else if (instance is DateTime)
                {
                    return ((DateTime)instance).Date.Equals(NullDate.Date);
                }
                else if (instance is string)
                {
                    return instance.Equals(NullString);
                }
                else if (instance is bool)
                {
                    return instance.Equals(NullBoolean);
                }
                else if (instance is Guid)
                {
                    return instance.Equals(NullGuid);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
