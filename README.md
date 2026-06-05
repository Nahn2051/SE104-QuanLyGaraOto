# Quản Lý Gara Ô Tô (Garage Management System)

Đây là ứng dụng quản lý gara ô tô được xây dựng trên nền tảng WPF (Windows Presentation Foundation) sử dụng kiến trúc MVVM (Model-View-ViewModel) và Entity Framework Core.

## Công nghệ sử dụng
- **Ngôn ngữ**: C#
- **Framework**: .NET 8.0 (WPF)
- **Cơ sở dữ liệu**: SQL Server
- **ORM**: Entity Framework Core 8.0.12
- **Kiến trúc**: MVVM (Model - View - ViewModel)
- **Thư viện khác**: ClosedXML (Hỗ trợ xuất báo cáo Excel)

## Cấu trúc dự án
Dự án được chia thành các thư mục chính theo chuẩn MVVM:
- `Models/`: Chứa các thực thể (Entities) ánh xạ với bảng trong CSDL và DbContext (`GaraDbContext`).
- `ViewModels/`: Chứa logic xử lý của ứng dụng, kết nối giữa View và Model (Kế thừa từ `BaseViewModel` và sử dụng `RelayCommand`).
- `Views/`: Giao diện người dùng (XAML).
- `Services/`: Các dịch vụ bổ trợ, ví dụ: `AuthService` (Xử lý đăng nhập, phân quyền).
- `Migrations/`: Chứa các file cấu hình di chuyển cấu trúc dữ liệu của Entity Framework Core.

## Yêu cầu hệ thống
- Hệ điều hành: Windows 10/11
- Visual Studio 2022 (hoặc IDE tương đương hỗ trợ .NET 8 WPF)
- .NET 8.0 SDK
- SQL Server

## Hướng dẫn cài đặt

### 1. Cấu hình Cơ sở dữ liệu (Database)
Ứng dụng sử dụng chuỗi kết nối (Connection String) được định nghĩa trong `Models/GaraDbContext.cs`:
```csharp
Server=MSI;Database=GaraOtoDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True
```
> [!NOTE]
> Bạn có thể thay đổi `Server=MSI` thành tên Server SQL của bạn (ví dụ: `Server=.\SQLEXPRESS` hoặc `Server=localhost`).

### 2. Chạy Migration để tạo Database
Mở Package Manager Console (hoặc Terminal) trong thư mục chứa file `QuanLyGaraOto.csproj` và chạy lệnh sau để cập nhật cơ sở dữ liệu và seed dữ liệu mẫu:
```bash
dotnet ef database update
```
*(Nếu dùng Package Manager Console trong Visual Studio, chọn Default project là `QuanLyGaraOto` và chạy `Update-Database`)*

### 3. Khởi chạy ứng dụng
Chạy ứng dụng từ Visual Studio hoặc qua dòng lệnh:
```bash
dotnet run
```

## Tài khoản đăng nhập mẫu
Hệ thống đã được seed sẵn 2 tài khoản (Xem cấu hình trong `GaraDbContext.cs`):
- **Quản lý**: Tên đăng nhập: `admin` | Mật khẩu: `admin123`
- **Nhân viên**: Tên đăng nhập: `nhanvien` | Mật khẩu: `nv123`

## Các chức năng chính
- Cấu hình linh hoạt: Thay đổi quy định về tỉ lệ giá bán, số xe sửa tối đa, quy định thu tiền nợ. Có kiểm tra validation theo thời gian thực (VD: Không được giảm số xe tối đa xuống dưới số xe đã nhận trong ngày).
- Tiếp nhận xe: Quản lý chi tiết thông tin khách hàng, số điện thoại (bắt buộc nhập), email, địa chỉ, hiệu xe.
- Lập phiếu sửa chữa:
  - Tự động tính toán vật tư, tiền công.
  - Hỗ trợ tính năng **Giảm giá** linh hoạt (nhập số tiền trực tiếp hoặc phần trăm %).
  - Thông minh với loại tiền công **Bảo hành**: tự động đưa giá vật tư về 0đ.
  - Cho phép thanh toán ngay và tự động cập nhật công nợ.
- Lập phiếu thu tiền: Thanh toán nợ linh hoạt, tự động trích lập tiền phạt nếu khách trả vượt nợ.
- Quản lý kho: Nhập kho tự động, cảnh báo âm kho.
- Báo cáo thống kê: Xuất Excel Báo cáo doanh số và Báo cáo tồn kho hằng tháng.
- Tra cứu chuyên sâu: Tìm kiếm đa chiều kết hợp lọc theo ngày (`Từ ngày` - `Đến ngày`) với ràng buộc logic chặt chẽ.
- Tính năng Hủy/Hoàn tác (Rollback): Cho phép hủy Phiếu sửa chữa, Phiếu thu tiền, Phiếu nhập kho, tự động đối soát và trả lại chính xác số lượng tồn kho cũng như công nợ khách hàng một cách thông minh.
- Tra cứu lịch sử (Xe, Phiếu sửa chữa, Phiếu nhập kho, Phiếu thu tiền).
- Hủy phiếu & Hoàn tác (Rollback): Cho phép hủy các phiếu và hệ thống sẽ tự động hoàn tác dòng tiền, công nợ, và tồn kho. Xử lý triệt để các tình huống thu tiền vượt nợ (tiền phạt).
- Quản lý danh mục (Hiệu xe, Tiền công, Quy định hệ thống, Người dùng).
- Tìm kiếm nhanh chóng trong các danh mục quản lý (Hiệu xe, Vật tư, Tiền công).
- Hỗ trợ xuất dữ liệu báo cáo và danh sách ra file Excel (.xlsx) thông qua ClosedXML.
