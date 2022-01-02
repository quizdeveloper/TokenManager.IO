using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenManager.Core.Database
{
    public abstract class SqlBase
    {
        #region Variables & constructor
        protected IDbConnection _dbConnection;
        protected int? commantTimeOut;
        private bool _disposed;

        public SqlBase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        #endregion

        #region Common function

        /// <summary>
        /// Function help run create, update, delete commands
        /// </summary>
        /// <param name="sqlStatement">Sql command</param>
        /// <param name="parameters">Input param</param>
        /// <returns></returns>
        protected virtual int ExecuteScalar(string sqlStatement, object parameters = null)
        {
            var result = _dbConnection.Execute(sqlStatement, parameters);
            return result;
        }

        /// <summary>
        /// Function help run create, update, delete commands with Store Procedure
        /// </summary>
        /// <param name="spName">Store procedure name</param>
        /// <param name="param">Parameters to execute SP</param>
        /// <returns>Number rows effect</returns>
        protected virtual int ExecuteScalarStoreProcedure(string spName, object param = null)
        {
            var executeResult = _dbConnection.Execute(
            spName,
            param,
            null,
            null,
            CommandType.StoredProcedure);
            return executeResult;
        }

        /// <summary>
        /// Get a list object with sql command
        /// </summary>
        /// <typeparam name="TEntity">Model of object</typeparam>
        /// <param name="sqlStatement">Select SQL command</param>
        /// <param name="param">Param query</param>
        /// <returns>List object</returns>
        protected virtual IEnumerable<TEntity> QuerySql<TEntity>(string sqlStatement, object param = null)
        {
            var queryResult = _dbConnection.Query<TEntity>(
                    sqlStatement,
                    param,
                    null,
                    commandTimeout: commantTimeOut,
                    commandType: CommandType.Text
                );
            return queryResult;
        }


        /// <summary>
        /// Get a list object by using Store Procedure
        /// </summary>
        /// <typeparam name="TEntity">Model of object</typeparam>
        /// <param name="spName">Store Procedure name</param>
        /// <param name="param">Param of Store Procedure</param>
        /// <returns>List object</returns>
        protected virtual IEnumerable<TEntity> QueryStoreProcedure<TEntity>(string spName, object param = null)
        {
            var queryResult = _dbConnection.Query<TEntity>(
                   spName,
                   param,
                   null,
                   commandTimeout: commantTimeOut,
                   commandType: CommandType.StoredProcedure
               );
            var result = queryResult.ToList();
            return result;
        }

        /// <summary>
        /// Get an object by sql query command
        /// </summary>
        /// <typeparam name="TEntity">Model of object</typeparam>
        /// <param name="sql">Sql command</param>
        /// <param name="param">Param of query</param>
        /// <param name="commandType">Inline query or SP</param>
        /// <returns>Object</returns>
        protected virtual TEntity QuerySingleOrDefault<TEntity>(string sql, object param = null, CommandType? commandType = null)
        {
            return _dbConnection.QuerySingleOrDefault<TEntity>(sql, param, null, commandTimeout: commantTimeOut, commandType);
        }

        #endregion

        #region Dispose

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                if(_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
                _dbConnection.Dispose();
                _dbConnection = null;
            }

            _disposed = true;
        }

        ~SqlBase()
        {
            Dispose(false);
        }

        #endregion

    }
}
