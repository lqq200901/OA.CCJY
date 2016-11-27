using JumpKick.HttpLib;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace OA.CCJY.OALib.Item.Mail
{
    public class MailFile:CCOAFile
    {
        private static string regular_EmailDetailContent;
        private static string ViewUrl;
        private static string regular_EmailDetailAttachment;

        private string content = string.Empty;
        public string Content {
            get {return content; }
            set {content=value;OnPropertyChanged("Content"); }
        }

        static MailFile()
        {
            ViewUrl = Setting.GetSystemUrl.GetFullUrl("EmailView");
            regular_EmailDetailContent = Setting.Regulars.GetRegular("EmailDetailContent");
            regular_EmailDetailAttachment = Setting.Regulars.GetRegular("EmailDetailAttachment");
        }

        public void GetEmailDetail()
        {
            GetEmailDetail(this);
        }
        public static void GetEmailDetail(MailFile mf)
        {
            try
            {
                Http.Get(ViewUrl + mf.ID.ToString()).OnSuccess(result =>
                {
                    SynchronizationContext.Current.Post(new SendOrPostCallback((s) =>
                    {
                        try
                        {
                            Regex r;
                            MatchCollection matchCollection;
                            r = new Regex(regular_EmailDetailContent, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            matchCollection = r.Matches((string)s);

                            if (matchCollection.Count > 0)
                            {
                                if(mf!=null) mf.Content = matchCollection[0].Groups["content"].Value;
                            }

                            r = new Regex(regular_EmailDetailAttachment, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            matchCollection = r.Matches((string)s);
                            if (matchCollection.Count > 0)
                            {
                                if (mf != null)
                                {
                                    if (mf.Attachs != null)
                                        mf.Attachs.Clear();
                                    else
                                        mf.Attachs = new Other.AsyncObservableCollection<Attach>();

                                    foreach (Match m in matchCollection)
                                    {
                                        mf.Attachs.Add(new Attach()
                                        {
                                            AID = int.Parse(m.Groups["id"].Value),
                                            Name = m.Groups["filename"].Value,
                                            Path = Setting.GetSystemUrl.domain + m.Groups["url"].Value,
                                            State = false
                                        });
                                    }
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
    }
}
