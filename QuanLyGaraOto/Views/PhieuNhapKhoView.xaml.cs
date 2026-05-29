using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class PhieuNhapKhoView : UserControl
    {
        public PhieuNhapKhoView()
        {
            InitializeComponent();
            DataContext = new PhieuNhapKhoViewModel();
        }
    }
}
