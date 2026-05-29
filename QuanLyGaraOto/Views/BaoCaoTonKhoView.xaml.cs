using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class BaoCaoTonKhoView : UserControl
    {
        public BaoCaoTonKhoView()
        {
            InitializeComponent();
            DataContext = new BaoCaoTonKhoViewModel();
        }
    }
}
