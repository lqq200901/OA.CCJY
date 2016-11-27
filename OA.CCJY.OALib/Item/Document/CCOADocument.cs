using JumpKick.HttpLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OA.CCJY.OALib.Item.Mail
{
    public class CCOADocument:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string regular_DocumentList;
        string regular_DocumentInfo;
        string url;
        string PageUrl;

        #region CCOADocument属性
        private int totalPage;

        public int TotalPage
        {
            get { return totalPage; }
            set { totalPage = value; OnPropertyChanged("TotalPage"); }
        }

        private int currentPage;

        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        private int unreadDocument;

        public int UnreadDocument
        {
            get { return unreadDocument; }
            set { unreadDocument = value; OnPropertyChanged("UnreadDocument"); }
        }

        private int documentCount;

        public int DocumentCount
        {
            get { return documentCount; }
            set { documentCount = value; OnPropertyChanged("DocumentCount"); }
        }

        public Item.Other.AsyncObservableCollection<MailFile> DocumentList { get; set; }
        #endregion

        public CCOADocument()
        {
            try
            {
                url = Setting.GetSystemUrl.GetFullUrl("Document");
                PageUrl = Setting.GetSystemUrl.GetFullUrl("DocumentPage");
                regular_DocumentList = Setting.Regulars.GetRegular("DocumentList");
                regular_DocumentInfo = Setting.Regulars.GetRegular("InboxInfo");

                DocumentList = new Other.AsyncObservableCollection<MailFile>();
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        public void RefreshInfo()
        {
            try
            {
                Http.Get(url).OnSuccess(result =>
                {
                    try
                    {
                        Regex r;
                        MatchCollection matchCollection;
                        r = new Regex(regular_DocumentInfo, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        matchCollection = r.Matches(result);
                        if (matchCollection.Count > 0)
                        {
                            TotalPage = int.Parse(matchCollection[0].Groups["TotalPage"].Value);
                            CurrentPage = int.Parse(matchCollection[0].Groups["CurrentPage"].Value);
                            UnreadDocument = int.Parse(matchCollection[0].Groups["UnreadMail"].Value);
                            DocumentCount = int.Parse(matchCollection[0].Groups["MailCount"].Value);
                        }
                        else
                        {
                            TotalPage = 1;
                            CurrentPage = 1;
                        }
                    }
                    catch(Exception e)
                    {
                        throw (e);
                    }
                }).Go();
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        public void GetInfo(object sender, CCOAEvens.CCOAEvenArgs e)
        {
            RefreshInfo();
        }

        public void GetList(int pageNo)
        {
            try
            {
                Http.Get(PageUrl+pageNo.ToString()).OnSuccess(result =>
                {
                    try
                    {
                        Regex r;
                        MatchCollection matchCollection;
                        r = new Regex(regular_DocumentList, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        matchCollection = r.Matches(result);

                        if (matchCollection.Count > 0)
                        {
                            DocumentList.Clear();
                            foreach (Match m in matchCollection)
                            {
                                int day = m.Groups["day"].Success ? int.Parse(m.Groups["day"].Value) : 0;
                                int hour = m.Groups["day"].Success ? int.Parse(m.Groups["hour"].Value) : 0;
                                TimeSpan ts = new TimeSpan(day,hour, int.Parse(m.Groups["minute"].Value), 0);
                                DocumentList.Add(new MailFile() { 
                                   ID = int.Parse(m.Groups["docId"].Value) ,
                                   SendDate = DateTime.Now-ts,
                                   Title=m.Groups["title"].Value,
                                   Sender = Contacts.CCOAContacts.GetContactByName(m.Groups["sender"].Value).ID,
                                   State = Setting.State.GetCategory(m.Groups["category"].Value) * 10 + Setting.State.GetLevel(m.Groups["level"].Value)
                                });
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw (e);
                    }
                }).Go();
            }
            catch (Exception e)
            {
                throw (e);
            }
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
