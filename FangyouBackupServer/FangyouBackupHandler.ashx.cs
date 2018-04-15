using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using FangyouCoreEntity;
namespace FangyouBackupServer
{
    /// <summary>
    /// FangyouBackupHandler 的摘要说明
    /// </summary>
    public class FangyouBackupHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sign = context.Request.QueryString["report"];
  
            var logFile = new FileLog(context.Request.MapPath("./") + "reportLog.txt");
            logFile.log(sign);
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}