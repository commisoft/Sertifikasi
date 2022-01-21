using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace PembayaranListrik.DAL
{

    public class ReflectionCache<T>
    {
        public Type ModelType { get; private set; }
        public IDictionary<PropertyInfo, Func<T, object>> ModelTypeProperties { get; private set; }
        public DataTable ModelTypeTable { get; private set; }


        public ReflectionCache()
        {
            //Cache type  
            this.ModelType = typeof(T);

            //Cache PropertyInfo into collection coupled with   
            this.ModelTypeProperties = ModelType.GetProperties().ToDictionary(k => k, v =>
            {
                var expressionParam = Expression.Parameter(ModelType);
                var getterDelagateExpression = Expression.Lambda<Func<T, object>>(
                    Expression.Convert(
                        Expression.Property(expressionParam, v.Name),
                        typeof(object)
                    ),
                    expressionParam
                ).Compile();
                return getterDelagateExpression;
            });

            //Create structured empty table for the type  
            ModelTypeTable = new DataTable();
            var columns = this.ModelTypeProperties.Keys.Select(p => new DataColumn(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType)).ToArray();
            ModelTypeTable.Columns.AddRange(columns);
            ModelTypeTable.AcceptChanges();
        }

    }

    public class ModelBulkInsert<T> where T : class
    {
        ReflectionCache<T> reflectionCache;
        String connectionString;
        public ModelBulkInsert(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task BulkInsert(IEnumerable<T> items, String tableName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                Type modelType = typeof(T);
                DataTable dataTable;

                if (reflectionCache == null)
                {
                    reflectionCache = new ReflectionCache<T>();
                }

                dataTable = reflectionCache.ModelTypeTable.Clone();
                using (dataTable)
                {
                    await connection.OpenAsync();
                    var bulkCopy = new SqlBulkCopy(connection);
                    bulkCopy.DestinationTableName = tableName;
                    var rows = items.Select(r =>
                    {
                        var row = dataTable.NewRow();
                        Array.ForEach(reflectionCache.ModelTypeProperties.Keys.ToArray(), (p) =>
                        {
                            var getter = reflectionCache.ModelTypeProperties[p];
                            row[p.Name] = getter(r) ?? DBNull.Value;
                        });
                        return row;
                    });
                    foreach (var row in rows)
                    {
                        dataTable.Rows.Add(row);
                    }
                    dataTable.AcceptChanges();
                    if (dataTable.Rows.Count > 0)
                    {
                        await bulkCopy.WriteToServerAsync(dataTable);
                    }
                    connection.Close();
                }
            }
        }
    }


}