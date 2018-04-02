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
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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