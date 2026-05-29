using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class PhieuThuTienView : UserControl
    {
        public PhieuThuTienView()
        {
            InitializeComponent();
            DataContext = new PhieuThuTienViewModel();
        }
    }
}
