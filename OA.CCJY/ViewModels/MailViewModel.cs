using System;
using ViewModelLibrary;
using OA.CCJY.OALib;
using OA.CCJY.OALib.Item.Other;
using OA.CCJY.OALib.Item.Mail;
using System.Collections.Specialized;
using System.Threading;
using System.Windows.Data;
using System.Globalization;
using OA.CCJY.Windows;
using OA.CCJY.Module;

namespace OA.CCJY.ViewModels
{
    public class MailViewModel : ViewModelBase
    {
        private CCOA ccoa;

        #region √¸¡Ó
        public DelegateCommand RefreshMailListCommand { get; private set; }
        public DelegateCommand TransmitMailCommand { get; private set; }
        #endregion

        #region  Ù–‘
        public AsyncObservableCollection<MailFile> MailList { get; private set; }

        private MailFile selectFile;
        public MailFile SelectFile {
            get
            {
                return selectFile;
            }
            set
            {
                selectFile = value;
                if(selectFile!=null) selectFile.GetEmailDetail();
                OnPropertyChanged("SelectFile");
            }
        }

        #endregion

        public MailViewModel(CCOA ccoa)
        {
            MailList = new AsyncObservableCollection<MailFile>();
            this.ccoa = ccoa;
            this.ccoa.Mail.GetMailList(1);
            ccoa.Mail.MailList.CollectionChanged +=new NotifyCollectionChangedEventHandler((s,e)=> {
                SynchronizationContext.Current.Post(RefreshMailList, s);
            });
            RefreshMailListCommand = new DelegateCommand(() => { this.ccoa.Mail.GetMailList(1); });
            TransmitMailCommand = new DelegateCommand(() =>
            {
                if (SelectFile != null)
                {
                    var popWindows = new PopWindow(new SendMailModule(ccoa,SelectFile));
                    popWindows.ShowDialog();
                }
            }
            );
        }

        private void RefreshMailList(object state)
        {
            MailList.Clear();
            foreach (MailFile mf in (AsyncObservableCollection<MailFile>)state)
                MailList.Add(mf);
        }
    }

    [ValueConversion(typeof(int), typeof(String))]
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int reValue = System.Convert.ToInt32(value);
            string imgPath = "/Icon/MailState1.png";
            if (reValue == 1)
            {
                imgPath = "/Icon/MailState2.png";
            }
            return imgPath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            return value;
        }


    }
}