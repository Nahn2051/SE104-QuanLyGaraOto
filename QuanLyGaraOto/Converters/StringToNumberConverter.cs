using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace QuanLyGaraOto.Converters
{
    public class StringToNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Trả về nguyên gốc để WPF tự động áp dụng StringFormat (như N0)
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                // Loại bỏ mọi dấu phẩy phân cách, khoảng trắng và các chữ cái (chỉ giữ lại số và dấu chấm thập phân)
                string clean = Regex.Replace(s, @"[^\d\.]", "");

                if (targetType == typeof(decimal) || targetType == typeof(decimal?))
                {
                    if (decimal.TryParse(clean, out decimal d)) return d;
                    return 0m;
                }
                else if (targetType == typeof(double) || targetType == typeof(double?))
                {
                    if (double.TryParse(clean, out double d)) return d;
                    return 0d;
                }
                else if (targetType == typeof(int) || targetType == typeof(int?))
                {
                    if (int.TryParse(clean, out int i)) return i;
                    return 0;
                }
            }

            // Nếu người dùng nhập rỗng hoặc toàn ký tự lạ, trả về giá trị 0 an toàn để tắt các nút Lưu
            if (targetType == typeof(decimal)) return 0m;
            if (targetType == typeof(double)) return 0d;
            if (targetType == typeof(int)) return 0;
            return null;
        }
    }
}
