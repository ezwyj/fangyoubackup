using System;
using System.Collections.Generic;
using System.Text;

namespace FangyouBackup
{
    public class SqlBase
    {
        protected string _conn;
        
        public virtual void Backup() { }
        public virtual void Restore() { }
    }
}
