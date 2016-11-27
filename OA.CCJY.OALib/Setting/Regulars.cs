using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OA.CCJY.OALib.Setting
{
    static class Regulars
    {
        private static Dictionary<string, string> regulars;

        static Regulars()
        {
            regulars = new Dictionary<string, string>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@"Setting\Regular.xml");
                XmlNode xn = xmlDoc.SelectSingleNode("Regular");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xnlc in xnl)
                {
                    foreach (XmlNode link in xnlc)
                    {
                        regulars.Add(link.Name, link.InnerText);
                    }
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        /// <summary>
        /// 获取Key对应的Regular
        /// </summary>
        /// <param name="key">Regular名称</param>
        /// <returns>返回名称对应的Regular</returns>
        public static string GetRegular(string key)
        {
            string result=null;
            try
            {
                if (regulars != null)
                {
                    result = regulars[key];
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
