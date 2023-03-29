using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal interface IDbConnection
    {
        void Connect();
        void Disconnect();
        DataTable ExecuteQuery(string query, SqlParameter[] parameters);
    }
}
