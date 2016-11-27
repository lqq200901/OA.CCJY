using OA.CCJY.OALib;
using OA.CCJY.OALib.Item;
using OA.CCJY.ViewModels;
using OA.CCJY.Views;
using System.Windows.Controls;
using ViewModelLibrary;

namespace OA.CCJY.Module
{
    class SendMailModule : ModuleBase
    {
        CCOA ccoa;
        CCOAFile OAFile;
        public SendMailModule(CCOA oa,CCOAFile OAFile)
        {
            this.ccoa = oa;
            this.OAFile = OAFile;
        }

        protected override UserControl CreateViewAndViewModel()
        {
            return new SendMailView() { DataContext = new SendMailViewModel(ccoa,OAFile) };
        }

        public override string Name
        {
            get { return "发送邮件"; }
        }
    }
}
