using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class QuanLyNguoiDungView : UserControl
    {
        public QuanLyNguoiDungView()
        {
            InitializeComponent();
            DataContext = new QuanLyNguoiDungViewModel();
        }
    }
}
