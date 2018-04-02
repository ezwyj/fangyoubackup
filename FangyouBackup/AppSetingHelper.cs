using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FangyouBackup
{
    public class AppSetingHelper
    {
        public static void UpdateAppString(string newName, string newConString)
        {
            //实例化xml
            XmlDocument doc = new XmlDocument();
            //获取App.config
            string str = System.Environment.CurrentDirectory;
            doc.Load(str + "\\FangyouBackup.exe.config");
            //找到xml单个节点
            XmlNode xNode = doc.SelectSingleNode("//appSettings");
            //找到单个Add节点
            XmlElement xElt = (XmlElement)xNode.SelectSingleNode("//add[@key='" + newName + "']");
            if (xElt != null)
            {
                xElt.SetAttribute("value", newConString);
            }
            else
            {
                XmlElement newElt = doc.CreateElement("add");
                newElt.SetAttribute("key", newName);
                newElt.SetAttribute("value", newConString);
                xNode.AppendChild(newElt);
            }
            //更新App.config
            doc.Save(str + "\\FangyouBackup.exe.config");

        }
    }
}
