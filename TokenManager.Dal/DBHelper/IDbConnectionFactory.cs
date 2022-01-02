using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenManager.Dal.DBHelper
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateMsSqlConnection(string connectionName);

        IDbConnection CreateSqLiteConnection(string connectionName);

        IDbConnection CreateMySqlConnection(string connectionName);

        IDbConnection CreatePostgreSqlConnection(string connectionName);
    }
}
