using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class QuanLyHieuXeView : UserControl
    {
        public QuanLyHieuXeView()
        {
            InitializeComponent();
            DataContext = new QuanLyHieuXeViewModel();
        }
    }
}
