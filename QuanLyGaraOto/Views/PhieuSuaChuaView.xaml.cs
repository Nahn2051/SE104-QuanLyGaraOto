using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class PhieuSuaChuaView : UserControl
    {
        public PhieuSuaChuaView()
        {
            InitializeComponent();
            DataContext = new PhieuSuaChuaViewModel();
        }
    }
}
