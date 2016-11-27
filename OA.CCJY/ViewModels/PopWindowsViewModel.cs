using OA.CCJY.Module;
using OA.CCJY.OALib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ViewModelLibrary;

namespace OA.CCJY.ViewModels
{
    class PopWindowsViewModel : ViewModelBase
    {
        private IModule model;

        public UserControl UserInterface
        {
            get
            {
                return model.UserInterface;
            }
        }

        public string Title
        {
            get
            {
                return model.Name;
            }
        }

        public PopWindowsViewModel(IModule model)
        {
            try
            {
                this.model = model;
            }
            catch (Exception e)
            {
                MessageBox.Show("系统初始化失败\n"+e.Message,"系统运行错误",MessageBoxButton.OK,MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
