using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OA.CCJY.OALib.Setting
{
    static class GetSystemUrl
    {
        private static Dictionary<string, string> urlData;
        public static string domain { get; private set; }

        static GetSystemUrl()
        {
            urlData = new Dictionary<string, string>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@"Setting\Url.xml");
                XmlNode xn = xmlDoc.SelectSingleNode("CCOA.Url");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xnlc in xnl)
                {
                    foreach (XmlNode link in xnlc)
                    {
                        urlData.Add(link.Name, link.InnerText);
                    }
                }
                domain = urlData["Domain"];
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        /// <summary>
        /// 获取Key对应的地址
        /// </summary>
        /// <param name="key">Url名称</param>
        /// <returns>返回名称对应的Url</returns>
        public static string GetUrl(string key)
        {
            string result=null;
            try
            {
                if (urlData != null)
                {
                    result = urlData[key];
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public static string GetFullUrl(string key)
        {
            string url = GetUrl(key);
            if (url != null)
                return domain + url;
            else
                return null;
        }
    }
}
