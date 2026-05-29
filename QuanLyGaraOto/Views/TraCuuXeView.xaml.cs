using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class TraCuuXeView : UserControl
    {
        public TraCuuXeView()
        {
            InitializeComponent();
            DataContext = new TraCuuXeViewModel();
        }
    }
}
