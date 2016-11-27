using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OA.CCJY.OALib.Setting
{
    static class State
    {
        private static Dictionary<string, int> Category;
        private static Dictionary<string, int> Level;
        private static Dictionary<string, int> ReadState;

        static State()
        {
            Category = new Dictionary<string, int>();
            Level = new Dictionary<string, int>();
            ReadState = new Dictionary<string, int>();
            Dictionary<string, int> dic=Category;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@"Setting\State.xml");
                XmlNode xn = xmlDoc.SelectSingleNode("State");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xnlc in xnl)
                {
                    if (xnlc.Name == "Category") dic = Category;
                    if (xnlc.Name == "Level") dic = Level;
                    if (xnlc.Name == "ReadState") dic = ReadState;

                    foreach (XmlNode link in xnlc)
                    {
                        dic.Add(link.Name,int.Parse(link.InnerText));
                    }
                }
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        public static string GetCategory(int value)
        {
            return GetKey(Category, value);
        }

        public static string GetLevel(int value)
        {
            return GetKey(Level, value);
        }

        public static string GetReadState(int value)
        {
            return GetKey(ReadState, value);
        }
         private static string GetKey(Dictionary<string ,int> dic, int value)
        {
            string result=null;
             foreach(var v in dic)
             {
                 if (v.Value == value) result = v.Key;
             }
             return result;
        }

         public static int GetCategory(string key)
         {
             return GetValue(Category, key);
         }

         public static int GetLevel(string key)
         {
             return GetValue(Level, key);
         }

         public static int GetReadState(string key)
         {
             return GetValue(ReadState, key);
         }
         private static int GetValue(Dictionary<string, int> dic, string key)
        {
            int result = -1;
            try
            {
                result = dic[key];
            }
             catch
            {
                result = -2;
            }
            return result;
        }
    }
}
