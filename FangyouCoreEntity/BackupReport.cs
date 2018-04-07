using System;
using System.Collections.Generic;
using System.Text;

namespace FangyouCoreEntity
{
    public class BackupReport
    {
        public string FangyouClient { get; set; }
        public string FangyouVer { get; set; }
        public DateTime BackupTime { get; set; }
        public bool BackupState { get; set; }
    }
}
