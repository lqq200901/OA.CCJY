using OA.CCJY.OALib;
using OA.CCJY.ViewModels;
using OA.CCJY.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ViewModelLibrary;

namespace OA.CCJY.Module
{
    class LoginModule : ModuleBase
    {
        CCOA ccoa;
        public LoginModule(CCOA oa)
        {
            this.ccoa = oa;
        }

        protected override UserControl CreateViewAndViewModel()
        {
            return new LoginView() { DataContext = new LoginViewModel(ccoa) };
        }

        public override string Name
        {
            get { return "登录系统"; }
        }
    }
}
