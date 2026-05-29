using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class QuanLyTienCongView : UserControl
    {
        public QuanLyTienCongView()
        {
            InitializeComponent();
            DataContext = new QuanLyTienCongViewModel();
        }
    }
}
