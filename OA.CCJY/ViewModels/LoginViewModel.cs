using System;
using System.Windows.Media.Imaging;
using System.Windows;
using ViewModelLibrary;
using OA.CCJY.OALib;
using System.Windows.Controls;

namespace OA.CCJY.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private CCOA ccoa;

        #region ����
        public DelegateCommand RefreshCheckCodeCommand { get; private set; }
        public DelegateCommand LoginCommand { get; private set; }
        #endregion

        #region ��¼��Ϣ
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

        public LoginViewModel(CCOA ccoa)
        {
            this.ccoa = ccoa;

            //RefreshCheckCodeCommand = new DelegateCommand(RefreshCheckCode);
            LoginCommand = new DelegateCommand(() => {
                if (Username.Length < 1) {
                        LoginInfo = "�����˺Ų���Ϊ�գ�"; return;
                }
                ccoa.Login(Username, Password);
            }) { IsEnabled = false };

            Username = "linqingqiang3416";
            Password = "12345678";

            ccoa.Subscribe(Login);
        }

        private void Login(object sender, CCOAEvens.CCOAEvenArgs e)
        {
            if (e.ResultToRaiseEvent ==0)
            {
                LoginInfo = "";
            }
            else if (e.ResultToRaiseEvent < -1)
            {
                LoginInfo = "�������Ӱ칫ϵͳʧ�ܣ�\n"+e.MessageToRaiseEvent;
            }else if (e.ResultToRaiseEvent == -1)
            {
                LoginInfo = "����ϵͳ��ʼ��ʧ�ܣ�\n";
            }
        }
    }
}