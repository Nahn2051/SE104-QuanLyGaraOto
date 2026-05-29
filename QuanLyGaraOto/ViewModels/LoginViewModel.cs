using System.Windows;
using QuanLyGaraOto.Services;

namespace QuanLyGaraOto.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        // =================================================================
        // Backing fields
        // =================================================================

        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isErrorVisible;

        // =================================================================
        // Properties — bind lên UI
        // =================================================================

        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                    ClearError();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                    ClearError();
            }
        }

        /// <summary>
        /// Thông báo lỗi hiển thị trên UI khi đăng nhập sai.
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        /// <summary>
        /// Ẩn/hiện thông báo lỗi.
        /// </summary>
        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => SetProperty(ref _isErrorVisible, value);
        }

        // =================================================================
        // Commands
        // =================================================================

        public RelayCommand LoginCommand { get; }

        // =================================================================
        // Constructor
        // =================================================================

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(
                execute: ExecuteLogin,
                canExecute: () => !string.IsNullOrWhiteSpace(Username)
                               && !string.IsNullOrWhiteSpace(Password)
            );
        }

        // =================================================================
        // Private Methods
        // =================================================================

        private void ExecuteLogin()
        {
            bool success = AuthService.Instance.Login(Username.Trim(), Password);

            if (success)
            {
                // Mở MainWindow
                var mainWindow = new MainWindow();
                mainWindow.Show();

                // Đóng LoginView (tìm Window chứa ViewModel này)
                CloseCurrentWindow();
            }
            else
            {
                ErrorMessage = "Sai tên đăng nhập hoặc mật khẩu!";
                IsErrorVisible = true;
            }
        }

        private void ClearError()
        {
            if (IsErrorVisible)
            {
                ErrorMessage = string.Empty;
                IsErrorVisible = false;
            }
        }

        /// <summary>
        /// Tìm và đóng Window hiện tại (LoginView).
        /// Duyệt qua danh sách Window đang mở, tìm Window có DataContext là LoginViewModel.
        /// </summary>
        private void CloseCurrentWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
