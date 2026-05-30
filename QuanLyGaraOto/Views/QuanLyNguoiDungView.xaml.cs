using System.Windows.Controls;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class QuanLyNguoiDungView : UserControl
    {
        private bool _isUpdatingPassword = false;

        public QuanLyNguoiDungView()
        {
            InitializeComponent();
            var vm = new QuanLyNguoiDungViewModel();
            DataContext = vm;
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MatKhau")
            {
                if (!_isUpdatingPassword)
                {
                    _isUpdatingPassword = true;
                    txtMatKhauHidden.Password = (DataContext as QuanLyNguoiDungViewModel)?.MatKhau ?? "";
                    _isUpdatingPassword = false;
                }
            }
        }

        private void txtMatKhauHidden_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_isUpdatingPassword) return;
            _isUpdatingPassword = true;
            
            if (DataContext is QuanLyNguoiDungViewModel vm)
            {
                vm.MatKhau = txtMatKhauHidden.Password;
            }
            if (txtMatKhauVisible.Text != txtMatKhauHidden.Password)
            {
                 txtMatKhauVisible.Text = txtMatKhauHidden.Password;
            }
            
            _isUpdatingPassword = false;
        }

        private void chkShowPassword_Changed(object sender, System.Windows.RoutedEventArgs e)
        {
            if (chkShowPassword.IsChecked == true)
            {
                txtMatKhauHidden.Visibility = System.Windows.Visibility.Collapsed;
                txtMatKhauVisible.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                txtMatKhauHidden.Visibility = System.Windows.Visibility.Visible;
                txtMatKhauVisible.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
