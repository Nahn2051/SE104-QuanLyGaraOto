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

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
