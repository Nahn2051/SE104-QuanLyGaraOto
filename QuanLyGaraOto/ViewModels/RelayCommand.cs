using System.Windows.Input;

namespace QuanLyGaraOto.ViewModels
{
    /// <summary>
    /// RelayCommand KHÔNG tham số — dùng cho các button đơn giản.
    /// </summary>
    /// <example>
    /// // Trong ViewModel:
    /// public ICommand LuuCommand { get; }
    /// 
    /// public MyViewModel()
    /// {
    ///     LuuCommand = new RelayCommand(
    ///         execute: () => LuuDuLieu(),
    ///         canExecute: () => IsValid
    ///     );
    /// }
    /// </example>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute is null || _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }

        /// <summary>
        /// Gọi hàm này khi điều kiện CanExecute thay đổi
        /// để UI tự cập nhật trạng thái enable/disable của button.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }

    /// <summary>
    /// RelayCommand CÓ tham số — dùng khi cần truyền CommandParameter từ XAML.
    /// </summary>
    /// <example>
    /// // Trong ViewModel:
    /// public ICommand XoaCommand { get; }
    /// 
    /// public MyViewModel()
    /// {
    ///     XoaCommand = new RelayCommand&lt;Xe&gt;(
    ///         execute: (xe) => XoaXe(xe),
    ///         canExecute: (xe) => xe != null
    ///     );
    /// }
    /// 
    /// // Trong XAML:
    /// &lt;Button Command="{Binding XoaCommand}" CommandParameter="{Binding SelectedXe}" /&gt;
    /// </example>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T?> _execute;
        private readonly Predicate<T?>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T?> execute, Predicate<T?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute is null)
                return true;

            // Hỗ trợ cả trường hợp parameter là null (cho nullable types)
            if (parameter is T typedParam)
                return _canExecute(typedParam);

            // Nếu parameter null và T là reference type hoặc nullable
            if (parameter is null && default(T) is null)
                return _canExecute(default);

            return false;
        }

        public void Execute(object? parameter)
        {
            if (parameter is T typedParam)
                _execute(typedParam);
            else if (parameter is null && default(T) is null)
                _execute(default);
        }

        /// <summary>
        /// Gọi hàm này khi điều kiện CanExecute thay đổi.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
