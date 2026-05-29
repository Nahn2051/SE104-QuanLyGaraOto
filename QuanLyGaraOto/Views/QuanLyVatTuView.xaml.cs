using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class QuanLyVatTuView : UserControl
    {
        public QuanLyVatTuView()
        {
            InitializeComponent();
            DataContext = new QuanLyVatTuViewModel();
        }
    }
}
