using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.CCJY.OALib.Item.Mail
{
    public class EmailContentObject
    {
        public string to_uid { get; set; }
        public string _blank_ { get; set; }
        public string cc_uid { get; set; }
        public string bcc_uid { get; set; }
        public string to_webmail { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public string important { get; set; }
        public string read_receipt { get; set; }
        public string email_status { get; set; }
        public string hidden_id { get; set; }
        public string upload_opt { get; set; }

        public Attachment attachment { get; set; }

        public EmailContentObject()
        {
            attachment = new Attachment();
        }
    }
}
