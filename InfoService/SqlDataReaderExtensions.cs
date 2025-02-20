using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountInfo
{
    public static class SqlDataReaderExtensions
    {
        public static List<T> GetList<T>(this SqlDataReader reader) where T : new()
        {
            var resultList = new List<T>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (reader.Read())
            {
                var instance = new T();

                foreach (var property in properties)
                {
                    if (!reader.HasColumn(property.Name) || reader[property.Name] == DBNull.Value)
                        continue;

                    var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                    property.SetValue(instance, value, null);
                }

                resultList.Add(instance);
            }

            return resultList;
        }

        private static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
