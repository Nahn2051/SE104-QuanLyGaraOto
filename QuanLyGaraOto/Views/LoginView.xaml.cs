using System.Windows;
using System.Windows.Input;
using QuanLyGaraOto.ViewModels;

namespace QuanLyGaraOto.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();

            // Focus vào ô username khi mở form
            Loaded += (_, _) => UsernameBox.Focus();
        }

        private bool _isUpdatingPassword = false;

        /// <summary>
        /// Truyền password từ PasswordBox vào ViewModel.
        /// WPF không hỗ trợ bind trực tiếp PasswordBox.Password vì lý do bảo mật,
        /// nên phải dùng event PasswordChanged trong code-behind.
        /// </summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_isUpdatingPassword) return;
            _isUpdatingPassword = true;

            if (DataContext is LoginViewModel vm)
            {
                vm.Password = PasswordBox.Password;
            }
            if (VisiblePasswordBox.Text != PasswordBox.Password)
            {
                VisiblePasswordBox.Text = PasswordBox.Password;
            }

            _isUpdatingPassword = false;
        }

        private void chkShowPassword_Changed(object sender, RoutedEventArgs e)
        {
            if (chkShowPassword.IsChecked == true)
            {
                PasswordBox.Visibility = Visibility.Collapsed;
                VisiblePasswordBox.Visibility = Visibility.Visible;
            }
            else
            {
                _isUpdatingPassword = true;
                PasswordBox.Password = VisiblePasswordBox.Text;
                _isUpdatingPassword = false;

                PasswordBox.Visibility = Visibility.Visible;
                VisiblePasswordBox.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Cho phép kéo thả cửa sổ bằng header.
        /// </summary>
        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// Nút đóng (X).
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
