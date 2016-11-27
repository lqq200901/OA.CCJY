using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.CCJY.OALib.Item.Contacts
{
    static class CCOAContacts
    {
        private static readonly string orgXmlFilename = @"setting\Org.xml";
        private static List<Contact> contacts;

        static CCOAContacts()
        {
            contacts = Contact.GetContacts(orgXmlFilename);
        }

        public static Contact GetContactByID(int id)
        {
            Contact contact=null;
            if (contacts != null)
            {
                contacts.ForEach(c => { if (c.ID == id) contact = c; });
            }
            return contact;
        }

        public static Contact GetContactByName(string name)
        {
            Contact contact = null;
            if (contacts != null)
            {
                contacts.ForEach(c => { if (c.Name == name) contact = c; });
            }
            return contact;
        }
    }
}
