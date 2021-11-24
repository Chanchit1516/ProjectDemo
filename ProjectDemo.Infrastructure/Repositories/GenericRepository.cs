using Dapper;
using ProjectDemo.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly string _tableName;
        private readonly string _schemaName;
        private readonly string _fullTableName;

        protected SqlTransaction Transaction { get; private set; }
        protected SqlConnection Connection { get { return Transaction.Connection; } }
        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        protected GenericRepository(SqlTransaction transaction)
        {
            _tableName = typeof(T).GetCustomAttribute<TableAttribute>()?.Name ?? typeof(T).Name;
            _schemaName = typeof(T).GetCustomAttribute<TableAttribute>()?.Schema ?? null;
            _fullTableName = _schemaName == null ? _tableName : $"[{_schemaName}].[{_tableName}]";
            Transaction = transaction;
        }

        public async Task<T> GetAsync(int id)
        {
            var key = GetKeyProperty() ?? "Id";
            var result = await Connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_fullTableName} WHERE {key}=@Id", new { Id = id }, Transaction);
            if (result == null)
                return null;
            //throw new KeyNotFoundException($"{tName} with {key} [{id}] could not be found.");
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Connection.QueryAsync<T>($"SELECT * FROM {_fullTableName}", null, Transaction);
        }

        public async Task<T> GetByKeyAsync(string column, string value)
        {
            object parameter = new ExpandoObject();
            ((IDictionary<string, object>)parameter)[column] = value;
            return await Connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_fullTableName} WHERE {column}=@{column} AND isActive = 1 AND isDeleted = 0", parameter, Transaction);
        }

        public async Task<int> DeleteRowAsync(int id)
        {
            var key = GetKeyProperty() ?? "Id";
            return await Connection.ExecuteAsync($"DELETE FROM {_fullTableName} WHERE {key}=@Id", new { Id = id }, Transaction);
        }

        public async Task<int> InsertAsync(T t)
        {
            var insertQuery = GenerateInsertQuery();
            return await Connection.ExecuteAsync(insertQuery, t, Transaction);
        }

        public async Task<int> InsertRangeAsync(IEnumerable<T> list)
        {
            var insertQuery = GenerateInsertQuery();
            return await Connection.ExecuteAsync(insertQuery, list, Transaction);
        }

        public async Task<int> UpdateAsync(T t)
        {
            var updateQuery = GenerateUpdateQuery();
            return await Connection.ExecuteAsync(updateQuery, t, Transaction);
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<T> list)
        {
            var updateQuery = GenerateUpdateQuery();
            return await Connection.ExecuteAsync(updateQuery, list, Transaction);
        }







        private string GetKeyProperty()
        {
            var props = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).ToList();
            return props.Count == 0 ? null : props.First().Name;
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_fullTableName} ");

            insertQuery.Append("(");

            var properties = GenerateListInsertOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });



            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append($");");

            var key = GetKeyProperty();
            if (!string.IsNullOrEmpty(key))
            {
                insertQuery.Append($" SELECT CAST(SCOPE_IDENTITY() as int)");
            }

            return insertQuery.ToString();
        }

        private static List<string> GenerateListInsertOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            foreach (var prop in listOfProperties)
            {
                var mm = prop.GetCustomAttributes(typeof(NotMappedAttribute), false);
            }

            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    let keyAttribute = prop.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false)
                    where keyAttribute.Length <= 0 && (attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore") || prop.PropertyType.Name == "Guid"
                    select prop.Name).ToList();
        }

        private static List<string> GenerateListUpdateOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        private string GenerateUpdateQuery()
        {
            var key = GetKeyProperty() ?? "Id";
            var updateQuery = new StringBuilder($"UPDATE {_fullTableName} SET ");
            var properties = GenerateListUpdateOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals(key))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append($" WHERE {key}=@{key}");

            return updateQuery.ToString();
        }

    }
}
