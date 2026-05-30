using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace QuanLyGaraOto.Converters
{
    public class BienSoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string bienSo)
            {
                // Giả định chuẩn Biển số VN thường có dạng:
                // 2 số mã tỉnh, 1 chữ cái serie (và có thể thêm số/chữ) - 4 hoặc 5 số cuối
                // Ví dụ DataBase lưu: "51F12345" -> Hiển thị: "51F-123.45"
                // Hoặc: "51F1234" -> Hiển thị: "51F-1234"

                if (string.IsNullOrWhiteSpace(bienSo)) return bienSo;

                // Nếu độ dài = 8 (VD: 51F12345)
                if (bienSo.Length == 8)
                {
                    return $"{bienSo.Substring(0, 3)}-{bienSo.Substring(3, 3)}.{bienSo.Substring(6, 2)}";
                }
                // Nếu độ dài = 7 (VD: 51F1234)
                else if (bienSo.Length == 7)
                {
                    return $"{bienSo.Substring(0, 3)}-{bienSo.Substring(3, 4)}";
                }
                // Nếu độ dài = 9 (VD: 29A123456 - Xe tải/khách đặc biệt)
                else if (bienSo.Length == 9)
                {
                    return $"{bienSo.Substring(0, 4)}-{bienSo.Substring(4, 3)}.{bienSo.Substring(7, 2)}";
                }

                // Nếu không đúng form chuẩn, fallback format bằng cách chèn dấu '-' ở giữa
                if (bienSo.Length > 4)
                {
                    return $"{bienSo.Substring(0, 3)}-{bienSo.Substring(3)}";
                }

                return bienSo;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Nếu có TwoWay binding từ UI về DB, tự động tước bỏ ký tự đặc biệt
            if (value is string str)
            {
                return Regex.Replace(str, @"[^a-zA-Z0-9]", "").ToUpper();
            }
            return value;
        }
    }
}
