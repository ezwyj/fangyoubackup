using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FangyouBackup
{
    public class SqlBase
    {
        protected string _conn;
        
        public virtual void Backup() { }
        public virtual void Restore() { }

        public SqlTypeEnum GetSqlVersion()
        {
            System.Data.SqlClient.SqlConnection DbConn = new SqlConnection(_conn);
            DbConn.Open();
            SqlCommand comm = DbConn.CreateCommand();
            comm.CommandText = "select @@version";
            comm.CommandType = System.Data.CommandType.Text;
            var result = comm.ExecuteScalar();
            if (result.ToString().IndexOf("2000")>1)
            {
                return SqlTypeEnum.Sql2000;
            }
            if (result.ToString().IndexOf("2005") > 1)
            {
                return SqlTypeEnum.Sql2005;
            }
            if (result.ToString().IndexOf("2008") > 1)
            {
                return SqlTypeEnum.Sql2008;
            }
            return SqlTypeEnum.Sql2000;
        }
    }
}
