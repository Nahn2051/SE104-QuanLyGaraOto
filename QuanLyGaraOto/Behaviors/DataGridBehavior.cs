using System.Windows;
using System.Windows.Controls;

namespace QuanLyGaraOto.Behaviors
{
    public static class DataGridBehavior
    {
        public static readonly DependencyProperty AutoNumberProperty =
            DependencyProperty.RegisterAttached("AutoNumber", typeof(bool), typeof(DataGridBehavior),
                new UIPropertyMetadata(false, OnAutoNumberChanged));

        public static bool GetAutoNumber(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoNumberProperty);
        }

        public static void SetAutoNumber(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoNumberProperty, value);
        }

        private static void OnAutoNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid)
            {
                if ((bool)e.NewValue)
                {
                    dataGrid.LoadingRow += DataGrid_LoadingRow;
                    dataGrid.UnloadingRow += DataGrid_UnloadingRow;
                }
                else
                {
                    dataGrid.LoadingRow -= DataGrid_LoadingRow;
                    dataGrid.UnloadingRow -= DataGrid_UnloadingRow;
                }
            }
        }

        private static void DataGrid_LoadingRow(object? sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private static void DataGrid_UnloadingRow(object? sender, DataGridRowEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                // Khi một dòng bị xóa, cần refresh lại số thứ tự cho các dòng còn lại
                // Dispatcher để đảm bảo việc xóa đã thực sự hoàn tất trên collection
                dataGrid.Dispatcher.InvokeAsync(() =>
                {
                    for (int i = 0; i < dataGrid.Items.Count; i++)
                    {
                        var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                        if (row != null)
                        {
                            row.Header = (i + 1).ToString();
                        }
                    }
                });
            }
        }
    }
}
