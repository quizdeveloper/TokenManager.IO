using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Core.Database;
using TokenManager.Core.Enum;

namespace TokenManager.Dal.DBHelper
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<string, string> _connectionDictionary;

        public DbConnectionFactory(IDictionary<string, string> connectionDictionary)
        {
            _connectionDictionary = connectionDictionary;
        }

        /// <summary>
        /// Create MSSQL connection
        /// </summary>
        /// <param name="connectionName">Connection string</param>
        /// <returns></returns>
        public IDbConnection CreateMsSqlConnection(string connectionName)
        {
            return CreateDbConnection(GetConnectionString(connectionName), DataAccessProviderTypes.SqlServer);
        }

        /// <summary>
        /// Create SQL Lite connection
        /// </summary>
        /// <param name="connectionName">Connection string</param>
        /// <returns></returns>
        public IDbConnection CreateSqLiteConnection(string connectionName)
        {
            return CreateDbConnection(GetConnectionString(connectionName), DataAccessProviderTypes.SqLite);
        }

        /// <summary>
        /// Create MySql connection
        /// </summary>
        /// <param name="connectionName">Connection string</param>
        /// <returns></returns>
        public IDbConnection CreateMySqlConnection(string connectionName)
        {
            return CreateDbConnection(GetConnectionString(connectionName), DataAccessProviderTypes.MySql);
        }

        /// <summary>
        /// Create PostgreSql connection
        /// </summary>
        /// <param name="connectionName">Connection string</param>
        /// <returns></returns>
        public IDbConnection CreatePostgreSqlConnection(string connectionName)
        {
            return CreateDbConnection(GetConnectionString(connectionName), DataAccessProviderTypes.PostgreSql);
        }

        /// <summary>
        /// Common method help create a DB connection
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="providerType">Provider type : mssql, mysql...</param>
        /// <returns>IDbConnection</returns>
        private IDbConnection CreateDbConnection(string connectionString, DataAccessProviderTypes providerType)
        {
            DbConnection connection = null;

            if (connectionString != null)
            {
                DbProviderFactory factory = DbProviderFactoryUtils.GetDbProviderFactory(providerType);

                connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
            }
            return connection;
        }

        /// <summary>
        /// Get connection string from dictionary
        /// </summary>
        /// <param name="connectionName">Key of connection string</param>
        /// <returns>Connection string</returns>
        private string GetConnectionString(string connectionName)
        {
            _connectionDictionary.TryGetValue(connectionName, out string connectionString);

            if (connectionString == null)
            {
                throw new Exception(string.Format("Connection string {0} was not found", connectionName));
            }

            return connectionString;
        }
    }
}
