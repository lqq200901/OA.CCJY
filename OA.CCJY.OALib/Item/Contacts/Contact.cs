using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OA.CCJY.OALib.Item.Contacts
{
    public class Contact
    {
        /// <summary>
        /// 人员编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 联系人类别 0：组织 1：联系人
        /// </summary>
        public int ContactType { get; set; }

        /// <summary>
        /// 父结点编号
        /// </summary>
        public int Parent { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 子结点
        /// </summary>
        public List<Contact> Child { get; set; }

        public Contact()
        {
            Child = new List<Contact>();
        }

        public static List<Contact> GetContacts(string contactsXmlFile)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(contactsXmlFile);
                XmlNode xn = xmlDoc.SelectSingleNode("org");

                return AnalysisContactsXmlToList(xn);
            }
            catch
            {
                return null;
            }   
        }

        private static Contact AnalysisContactsXml(XmlNode node)
        {
           Contact contacts = new Contact();
           contacts.Name = node.Attributes["b"].Value;
           contacts.ID =int.Parse(node.Attributes["a"].Value);

            foreach(var n in node.ChildNodes)
            {
            }

            return contacts;
        }

        private static List<Contact> AnalysisContactsXmlToList(XmlNode node,int parent=0)
        {
            List<Contact> contacts=new List<Contact>();

            try
            {
                Contact contact = new Contact();
                contact.Name = node.Attributes["b"].Value;
                contact.ID = int.Parse(node.Attributes["a"].Value);

                if (contact.ID == parent) contact.Parent = -1;
                contact.ContactType = 0;
                contacts.Add(contact);

                if (node.HasChildNodes)
                {
                    contact.Child = new List<Contact>();
                    foreach (XmlNode n in node.ChildNodes)
                    {
                        var cl = AnalysisContactsXmlToList(n, contact.ID);
                        contacts.AddRange(cl);
                        contact.Child.AddRange(cl);
                    }
                }
                else
                {
                    if (node.Name == "u")
                    {
                        contact.Account = node.Attributes["h"].Value;
                        contact.ContactType = 1;
                    }
                }
            }
            catch(Exception e)
            {
                string s = e.Message;
            }
            return contacts;
        }
    }
}
