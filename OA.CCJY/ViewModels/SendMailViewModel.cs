using ViewModelLibrary;
using OA.CCJY.OALib;
using OA.CCJY.OALib.Item;
using OA.CCJY.OALib.Item.Other;
using OA.CCJY.OALib.Item.Contacts;
using OA.CCJY.OALib.Item.Mail;
using System;

namespace OA.CCJY.ViewModels
{
    public class SendMailViewModel : ViewModelBase
    {
        private CCOA ccoa;
        private CCOAFile OAFile;
        private NewMailParameter mailParameter=new NewMailParameter();

        #region ÃüÁî
        public DelegateCommand RefreshCheckCodeCommand { get; private set; }
        public DelegateCommand LoginCommand { get; private set; }
        #endregion

        #region µÇÂ¼ÐÅÏ¢

        public string Title
        {
            get { return mailParameter.EmailContent.subject; }
            set { mailParameter.EmailContent.subject = value; OnPropertyChanged("Title"); }
        }


        public string Content
        {
            get { return mailParameter.EmailContent.content; }
            set { mailParameter.EmailContent.content = value; OnPropertyChanged("Content"); }
        }
        public AsyncObservableCollection<Contact> Recipients { get; set; }

        #endregion

        public SendMailViewModel(CCOA ccoa,CCOAFile OAFile)
        {
            try
            {
                this.ccoa = ccoa;
                this.OAFile = OAFile;

                Title = OAFile.Title;
                Content = ((MailFile)OAFile).Content;
            }
            catch(Exception e)
            {
                int i = 1;
            }
        }
    }

}