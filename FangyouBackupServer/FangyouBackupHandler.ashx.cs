using System;
using System.Collections.Generic;
using System.Web;

namespace FangyouBackupServer
{
    /// <summary>
    /// FangyouBackupHandler 的摘要说明
    /// </summary>
    public class FangyouBackupHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sign = context.Request.QueryString["sign"];

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