using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuanLyGaraOto.ViewModels
{
    /// <summary>
    /// Base class cho tất cả ViewModel, triển khai INotifyPropertyChanged
    /// để tự động cập nhật UI khi property thay đổi.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Phát sự kiện PropertyChanged cho 1 property cụ thể.
        /// Nếu không truyền tên, sẽ tự lấy tên property nơi gọi hàm.
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gán giá trị mới cho field và phát sự kiện PropertyChanged nếu giá trị thay đổi.
        /// Trả về true nếu giá trị thực sự thay đổi, false nếu giữ nguyên.
        /// </summary>
        /// <example>
        /// private string _tenKhach;
        /// public string TenKhach
        /// {
        ///     get => _tenKhach;
        ///     set => SetProperty(ref _tenKhach, value);
        /// }
        /// </example>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
