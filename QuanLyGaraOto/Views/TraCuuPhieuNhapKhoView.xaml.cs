using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class TraCuuPhieuNhapKhoView : UserControl
    {
        public TraCuuPhieuNhapKhoView()
        {
            InitializeComponent();
            DataContext = new TraCuuPhieuNhapKhoViewModel();
        }
    }
}
