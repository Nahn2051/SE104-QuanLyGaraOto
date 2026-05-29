using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class TiepNhanXeView : UserControl
    {
        public TiepNhanXeView()
        {
            InitializeComponent();

            // Set DataContext = ViewModel
            DataContext = new TiepNhanXeViewModel();
        }
    }
}
