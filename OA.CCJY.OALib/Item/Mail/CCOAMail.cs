using JumpKick.HttpLib;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;

namespace OA.CCJY.OALib.Item.Mail
{
    public class CCOAMail:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string regular_InboxMailList;
        string regular_InboxInfo;
        string regular_EmailDetailContent;
        string regular_EmailDetailAttachment;
        string url;
        string PageUrl;
        string ViewUrl;
        //string UnreadStr = "未阅读";

        #region CCOAMail属性
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

        private int unreadMail;

        public int UnreadMail
        {
            get { return unreadMail; }
            set { unreadMail = value; OnPropertyChanged("UnreadMail"); }
        }

        private int mailCount;

        public int MailCount
        {
            get { return mailCount; }
            set { mailCount = value; OnPropertyChanged("MailCount"); }
        }

        public Item.Other.AsyncObservableCollection<MailFile> MailList { get; set; }
        #endregion

        public CCOAMail()
        {
            try
            {
                url = Setting.GetSystemUrl.GetFullUrl("Email");
                PageUrl = Setting.GetSystemUrl.GetFullUrl("EmailPage");
                
                regular_InboxMailList = Setting.Regulars.GetRegular("InboxMailList");
                regular_InboxInfo = Setting.Regulars.GetRegular("InboxInfo");

                MailList = new Other.AsyncObservableCollection<MailFile>();
            }
            catch(Exception e)
            {
                throw (e);
            }
        }

        public void RefreshInboxInfo()
        {
            try
            {
                Http.Get(url).OnSuccess(result =>
                {
                    try
                    {
                        Regex r;
                        MatchCollection matchCollection;
                        r = new Regex(regular_InboxInfo, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        matchCollection = r.Matches(result);
                        if (matchCollection.Count > 0)
                        {
                            TotalPage = int.Parse(matchCollection[0].Groups["TotalPage"].Value);
                            CurrentPage = int.Parse(matchCollection[0].Groups["CurrentPage"].Value);
                            UnreadMail = int.Parse(matchCollection[0].Groups["UnreadMail"].Value);
                            MailCount = int.Parse(matchCollection[0].Groups["MailCount"].Value);
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

        public void GetInboxInfo(object sender, CCOAEvens.CCOAEvenArgs e)
        {
            if(e.ResultToRaiseEvent==0) RefreshInboxInfo();
        }

        public void GetMailList(int pageNo)
        {
            try
            {
                Http.Get(PageUrl+pageNo.ToString()).OnSuccess(result =>
                {
                    SynchronizationContext.Current.Post(new SendOrPostCallback((s) => {
                        try
                        {
                            Regex r;
                            MatchCollection matchCollection;
                            r = new Regex(regular_InboxMailList, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            matchCollection = r.Matches((string)s);

                            if (matchCollection.Count > 0)
                            {
                                MailList.Clear();
                                foreach (Match m in matchCollection)
                                {
                                    MailList.Add(new MailFile()
                                    {
                                        ID = int.Parse(m.Groups["docId"].Value),
                                        SendDate = DateTime.ParseExact(m.Groups["date"].Value, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture),
                                        Title = m.Groups["title"].Value,
                                        Sender = int.Parse(m.Groups["userId"].Value),
                                        State = (Setting.State.GetReadState(m.Groups["State"].Value))
                                    });
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw (e);
                        }
                    }), result);
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
