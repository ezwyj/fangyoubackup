using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FangyouCoreEntity
{
    public class SqlBase
    {
        protected string _conn;
        
        public virtual void Backup() { }
        public virtual void Restore() { }

        public SqlTypeEnum GetSqlVersion()
        {

            var conn = string.Format("server={0};database={1};uid={2};pwd={3}", GlobleVariable.DatabaseAddress, GlobleVariable.DatabaseName, GlobleVariable.DatabaseUser, GlobleVariable.DatabasePassword);

            System.Data.SqlClient.SqlConnection db= new SqlConnection(conn);
            db.Open();
            SqlCommand command = db.CreateCommand();
            command.CommandText= "select @@version";
            var result = command.ExecuteScalar();
            db.Close();
            db.Dispose();
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
