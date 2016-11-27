using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.CCJY.OALib.Item.Mail
{
    public class NewMailParameter
    {
        public EmailContentObject EmailContent { get; set; }
        public string file_id { get; set; }
        public string rename_url { get; set; }
        public string file_name { get; set; }
        public string Sel_ATTACHMENT_ID { get; set; }
        public string Sel_ATTACHMENT_NAME { get; set; }

        public TRemindObject TRemind { get; set; }

        public NewMailParameter()
        {
            EmailContent = new EmailContentObject();
            TRemind = new TRemindObject();
        }
    }
}
