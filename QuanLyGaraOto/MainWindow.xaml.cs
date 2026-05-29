using System.Windows;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}