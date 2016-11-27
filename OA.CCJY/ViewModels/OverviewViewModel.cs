using System;
using System.Windows.Media.Imaging;
using System.Windows;
using ViewModelLibrary;
using OA.CCJY.OALib;
using System.Windows.Controls;

namespace OA.CCJY.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        private CCOA ccoa;

        #region ÃüÁî
        public DelegateCommand RefreshCheckCodeCommand { get; private set; }
        public DelegateCommand LoginCommand { get; private set; }
        #endregion

        #region µÇÂ¼ÐÅÏ¢
        bool isChange = false;
        string username = string.Empty;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    if (!isChange) Password = "";
                    username = value;
                    OnPropertyChanged("username");
                }
            }
        }

        string password = string.Empty;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                if (!isChange) isChange = true;
                if (password.Length > 0) LoginCommand.IsEnabled = true;
                OnPropertyChanged("Password");
            }
        }


        private bool savePassword = false;
        public bool SavePassword
        {
            get { return savePassword; }
            set { savePassword = value; OnPropertyChanged("CheckCode"); }
        }

        string loginInfo = string.Empty;
        public string LoginInfo
        {
            get { return loginInfo; }
            set
            {
                loginInfo = value;
                OnPropertyChanged("LoginInfo");
            }
        }

        #endregion

        public OverviewViewModel(CCOA ccoa)
        {
            this.ccoa = ccoa;
        }
    }
}