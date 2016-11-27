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
    class MailModule : ModuleBase
    {
        CCOA ccoa;
        public MailModule(CCOA oa)
        {
            this.ccoa = oa;
        }

        protected override UserControl CreateViewAndViewModel()
        {
            return new MailView() { DataContext = new MailViewModel(ccoa) };
        }

        public override string Name
        {
            get { return "电子邮件"; }
        }
    }
}
