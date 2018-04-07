using FangyouCoreEntity;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FangyouBackup
{
    public class BackupJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var checkSql =new SqlBase();

            switch (checkSql.GetSqlVersion())
            {
                case SqlTypeEnum.Sql2000:
                    var backup2000 = new Sql2000Desktop();
                    backup2000.Backup();
                    break;
                case SqlTypeEnum.Sql2005:
                    var backup2005 = new Sql2005();
                    backup2005.Backup();
                    break;
                case SqlTypeEnum.Sql2008:
                    var backup2008 = new Sql2008();
                    backup2008.Backup();
                    break;
            }
        }
    }
}
