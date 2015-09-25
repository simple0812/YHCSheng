using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace YHCSheng.Utils
{
    public class DependencyInjection<T> where T : new()
    { /// <summary>
        /// Create intance of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Data row</param>
        /// <returns>Object Entity</returns>
        public static T FillEntity(DataRow dr)
        {
            //Activator.CreateInstance<T>();// 
            T t = new T();

            List<string> columns = new List<string>();
            foreach (DataColumn c in dr.Table.Columns)
            {
                columns.Add(c.ColumnName.ToLower());
            }

            List<PropertyInfo> properties = null;// (List<PropertyInfo>)DataCache.GetCache(t.GetType().FullName);
            if (properties == null)
            {
                properties = new List<PropertyInfo>();
                foreach (PropertyInfo p in t.GetType().GetProperties())
                {
                    properties.Add(p);
                }
                // DataCache.SetCache(t.GetType().FullName, properties);
            }

            foreach (PropertyInfo p in properties)
            {
                if (p.CanWrite == true)
                {
                    object propertyValue = Null.SetNull(p);
                    if (columns.Contains(p.Name.ToLower()) == true)
                    {
                        object dbValue = dr[p.Name.ToLower()];
                        if (Convert.IsDBNull(dbValue) == true)
                        {
                            p.SetValue(t, propertyValue, null);
                        }
                        else
                        {
                            Type propertyType = p.PropertyType;
                            p.SetValue(t, Convert.ChangeType(dbValue, propertyType), null);
                        }
                    }
                }
            }

            return t;
        }

        /// <summary>
        /// Create intance of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Data set</param>
        /// <returns>Object Entity</returns>
        public static T FillEntity(DataSet ds)
        {
            T t = new T();// Activator.CreateInstance<T>();// new T();
            if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
            {
                t = FillEntity(ds.Tables[0].Rows[0]);
            }

            return t;
        }

        /// <summary>
        /// Create intance of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Data table</param>
        /// <returns>Object Entity</returns>
        public static T FillEntity(DataTable dt)
        {
            T t = Activator.CreateInstance<T>();// new T();
            if (dt != null && dt.Rows.Count == 1)
            {
                t = FillEntity(dt.Rows[0]);
            }

            return t;
        }

        /// <summary>
        /// Create intance list of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Data set</param>
        /// <returns>Object entity list</returns>
        public static List<T> FillList(DataSet ds)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                T t = Activator.CreateInstance<T>();// new T();
                t = FillEntity(dr);
                list.Add(t);
            }

            return list;
        }

        /// <summary>
        /// Create intance list of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Data table</param>
        /// <returns>Object entity list</returns>
        public static List<T> FillList(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T t = Activator.CreateInstance<T>();// new T();
                t = FillEntity(dr);
                list.Add(t);
            }

            return list;
        }

        /// <summary>
        /// Create intance list of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Data row collections</param>
        /// <returns>Object entity list</returns>
        public static List<T> FillList(DataRow[] rows)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in rows)
            {
                T t = Activator.CreateInstance<T>();// new T();
                t = FillEntity(dr);
                list.Add(t);
            }

            return list;
        }

        /// <summary>
        /// Create intance list of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Data reader</param>
        /// <returns>Object entity list</returns>
        public static T FillEntity(IDataReader dr)
        {
            return FillEntity(dr, true);
        }

        /// <summary>
        /// Create intance list of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Datareader</param>
        /// <param name="isCloseReader">Whether closing datareader</param>
        /// <returns>Object entity</returns>
        public static T FillEntity(IDataReader dr, bool isCloseReader)
        {
            T t = new T();// Activator.CreateInstance<T>();// new T();
            while (dr.Read())
            {
                List<string> columns = new List<string>();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    columns.Add(dr.GetName(i).ToLower());
                }

                List<PropertyInfo> properties = null;// (List<PropertyInfo>)DataCache.GetCache(t.GetType().FullName);
                if (properties == null)
                {
                    properties = new List<PropertyInfo>();
                    foreach (PropertyInfo p in t.GetType().GetProperties())
                    {
                        properties.Add(p);
                    }
                    //DataCache.SetCache(t.GetType().FullName, properties);
                }

                foreach (PropertyInfo p in properties)
                {
                    if (p.CanWrite == true)
                    {
                        object propertyValue = Null.SetNull(p);
                        if (columns.Contains(p.Name.ToLower()) == true)
                        {
                            object dbValue = dr[p.Name.ToLower()];
                            if (Convert.IsDBNull(dbValue) == true)
                            {
                                p.SetValue(t, propertyValue, null);
                            }
                            else
                            {
                                Type propertyType = p.PropertyType;
                                p.SetValue(t, Convert.ChangeType(dbValue, propertyType), null);
                            }
                        }
                    }
                }

                break;
            }

            if (isCloseReader == true && dr != null)
            {
                dr.Close();
            }

            return t;
        }

        /// <summary>
        /// Create intance list of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Datareader</param>
        /// <returns>Object entity list</returns>
        public static List<T> FillList(IDataReader dr)
        {
            return FillList(dr, true);
        }

        /// <summary>
        /// Create intance list of object entity according to record set from database 
        /// </summary>
        /// <param name="dr">Datareader</param>
        /// <param name="isCloseReader">Whether closing datareader</param>
        /// <returns>Object entity list</returns>
        public static List<T> FillList(IDataReader dr, bool isCloseReader)
        {
            T t = new T();// Activator.CreateInstance<T>();
            //new T();
            List<T> list = new List<T>();
            List<string> columns = new List<string>();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                columns.Add(dr.GetName(i).ToLower());
            }


            List<PropertyInfo> properties = null;// (List<PropertyInfo>)DataCache.GetCache(t.GetType().FullName);
            if (properties == null)
            {
                properties = new List<PropertyInfo>();
                //foreach (PropertyInfo p in t.GetType().GetProperties())
                //{
                //    properties.Add(p);
                //}
                foreach (PropertyInfo p in typeof(T).GetProperties())
                {
                    properties.Add(p);
                }
                //DataCache.SetCache(t.GetType().FullName, properties);
            }

            while (dr.Read())
            {
                t = new T();// Activator.CreateInstance<T>(); //new T();
                foreach (PropertyInfo p in properties)
                {
                    if (p.CanWrite == true)
                    {
                        object propertyValue = Null.SetNull(p);
                        if (columns.Contains(p.Name.ToLower()) == true)
                        {
                            object dbValue = dr[p.Name.ToLower()];
                            if (Convert.IsDBNull(dbValue) == true)
                            {
                                p.SetValue(t, propertyValue, null);
                            }
                            else
                            {
                                Type propertyType = p.PropertyType;
                                p.SetValue(t, Convert.ChangeType(dbValue, propertyType), null);
                            }
                        }
                    }
                }
                list.Add(t);
            }

            if (isCloseReader == true && dr != null)
            {
                dr.Close();
            }

            return list;
        }
    }
}
