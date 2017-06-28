using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace TLGX_Consumer.Models
{
    public class ConversionClass
    {

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            DataTable dataTable = new DataTable();
            /*if (from == 0)
            {
                var properties = type.GetProperties().Where(p => !p.GetIndexParameters().Any());

                foreach (PropertyInfo info in properties)
                {
                    dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
                }

                foreach (T entity in list)
                {
                    object[] values = new object[properties.Count()];
                    int i = 0;
                    foreach (PropertyInfo property in properties)
                    {
                        values[i] = property.GetValue(entity);
                        i = i + 1;
                    }
                    //for (int i = 0; i < properties.Count(); i++)
                    //{
                    //   values[i] = properties[i].GetValue(entity);
                    //}

                    dataTable.Rows.Add(values);
                }
            }
            else
            {*/
                var properties = type.GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
                }

                foreach (T entity in list)
                {
                    object[] values = new object[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        values[i] = properties[i].GetValue(entity);
                    }

                    dataTable.Rows.Add(values);
                }
            //}

            return dataTable;
        }
    }
}
