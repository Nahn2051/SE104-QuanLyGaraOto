using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class BaoCaoDoanhSoView : UserControl
    {
        public BaoCaoDoanhSoView()
        {
            InitializeComponent();
            DataContext = new BaoCaoDoanhSoViewModel();
        }
    }
}
