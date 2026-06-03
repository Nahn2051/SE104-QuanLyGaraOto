using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class LichSuTiepNhanXeView : UserControl
    {
        public LichSuTiepNhanXeView()
        {
            InitializeComponent();
            DataContext = new LichSuTiepNhanXeViewModel();
        }
    }
}
