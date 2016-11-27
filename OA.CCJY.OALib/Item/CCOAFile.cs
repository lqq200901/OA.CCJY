using OA.CCJY.OALib.Item.Contacts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OA.CCJY.OALib.Item
{
    public class CCOAFile: ICCOAFile,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 文件编号
        /// </summary>
        public int ID { get;  set; }

        /// <summary>
        /// 文件标题
        /// </summary>
        public string Title { get;  set; }

        /// <summary>
        /// 文件状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public int Sender { get;  set; }
        /// <summary>
        /// 发件人姓名
        /// </summary>
        public string SenderName
        {
            get
            {
                return CCOAContacts.GetContactByID(Sender).Name;
            }
        }

        /// <summary>
        /// 发件日期
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// 文件附件
        /// </summary>
        public Other.AsyncObservableCollection<Attach> Attachs{ get; set; }

        public CCOAFile()
        {
            Attachs = new Other.AsyncObservableCollection<Attach>();
        }

        public bool SetSender(int senderID)
        {
            bool result = false;

            var sender = CCOAContacts.GetContactByID(senderID);
            if(sender!=null)
            {
                Sender = sender.ID;
                result = true;
            }

            return result;
        }

        public bool SetSender(string senderName)
        {
            bool result = false;

            var sender = CCOAContacts.GetContactByName(senderName);
            if (sender != null)
            {
                Sender = sender.ID;
                result = true;
            }

            return result;
        }

        public void OnPropertyChanged(string msg)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(msg));
            }
        }
    }
}
