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

        /// <summary>
        /// Truyền password từ PasswordBox vào ViewModel.
        /// WPF không hỗ trợ bind trực tiếp PasswordBox.Password vì lý do bảo mật,
        /// nên phải dùng event PasswordChanged trong code-behind.
        /// </summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = PasswordBox.Password;
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
