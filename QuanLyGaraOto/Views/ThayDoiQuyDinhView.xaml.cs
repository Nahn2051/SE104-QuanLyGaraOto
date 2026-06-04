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

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DecimalValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Cho phép số và dấu chấm, dấu phẩy (tùy locale)
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
