using OA.CCJY.OALib;
using OA.CCJY.ViewModels;
using System.Windows;
using ViewModelLibrary;

namespace OA.CCJY.Windows
{
    /// <summary>
    /// PopWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopWindow : Window
    {
        private IModule module;

        PopWindowsViewModel poopVM;

        public PopWindow(IModule module)
        {
            this.module = module;

            InitializeComponent();
            poopVM = new PopWindowsViewModel(module);
            DataContext = poopVM;
        }
    }
}
