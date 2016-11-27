using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OA.CCJY.Setting
{
    static class System
    {
        private static Dictionary<string, string> SystemSetting;

        static System()
        {
            SystemSetting = new Dictionary<string, string>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@"Setting\System.xml");
                XmlNode xn = xmlDoc.SelectSingleNode("System");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xnlc in xnl)
                {
                    foreach (XmlNode link in xnlc)
                    {
                        SystemSetting.Add(link.Name, link.InnerText);
                    }
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        /// <summary>
        /// 获取Key对应的系统设置
        /// </summary>
        /// <param name="key">系统设置项的名称</param>
        /// <returns>返回名称对应的系统设置</returns>
        public static string GetSystemSetting(string key)
        {
            string result=null;
            try
            {
                if (SystemSetting != null)
                {
                    result = SystemSetting[key];
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
}
