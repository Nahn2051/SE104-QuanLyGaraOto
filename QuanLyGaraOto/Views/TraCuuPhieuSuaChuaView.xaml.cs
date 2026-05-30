using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class TraCuuPhieuSuaChuaView : UserControl
    {
        public TraCuuPhieuSuaChuaView()
        {
            InitializeComponent();
            DataContext = new TraCuuPhieuSuaChuaViewModel();
        }
    }
}
