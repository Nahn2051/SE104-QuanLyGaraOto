using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class ThayDoiQuyDinhView : UserControl
    {
        public ThayDoiQuyDinhView()
        {
            InitializeComponent();
            DataContext = new ThayDoiQuyDinhViewModel();
        }
    }
}
