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
    class OverviewModule : ModuleBase
    {
        CCOA ccoa;
        public OverviewModule(CCOA oa)
        {
            this.ccoa = oa;
        }

        protected override UserControl CreateViewAndViewModel()
        {
            return new OverviewView() { DataContext = new OverviewViewModel(ccoa) };
        }

        public override string Name
        {
            get { return "事务概况"; }
        }
    }
}
