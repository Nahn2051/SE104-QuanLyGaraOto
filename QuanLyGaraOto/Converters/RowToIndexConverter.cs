using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace QuanLyGaraOto.Converters
{
    public class RowToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Ép kiểu object truyền vào thành DataGridRow
            if (value is DataGridRow row)
            {
                // Lấy Index của dòng đó trong danh sách (bắt đầu từ 0) và cộng thêm 1
                return row.GetIndex() + 1;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
