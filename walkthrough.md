# Walkthrough: Quy trình hoạt động của Quản Lý Gara Ô Tô

Tài liệu này mô tả chi tiết luồng nghiệp vụ (business flow) từ khi khách hàng mang xe đến Gara cho đến khi thanh toán và nhận xe, cũng như các chức năng hỗ trợ của hệ thống.

## 1. Đăng nhập và Phân quyền
- **Đăng nhập**: Ứng dụng bắt đầu bằng màn hình đăng nhập (`LoginView`). Người dùng nhập tên tài khoản và mật khẩu. Hệ thống sẽ xác thực thông qua `AuthService`.
- **Phân quyền**: Có 2 vai trò chính:
  - **Nhân viên**: Có thể tiếp nhận xe, lập phiếu sửa chữa, thu tiền, nhập kho.
  - **Quản lý**: Có toàn quyền, bao gồm xem báo cáo doanh số, báo cáo tồn kho, quản lý danh sách người dùng và thay đổi các quy định hệ thống (tham số).

## 2. Luồng nghiệp vụ chính (Garage Workflow)

### Bước 1: Tiếp nhận xe (`TiepNhanXeView`)
Khi khách hàng mang xe tới, nhân viên tạo hồ sơ tiếp nhận xe:
- Nhập thông tin: Biển số, Tên chủ xe, Điện thoại, Địa chỉ, Ngày tiếp nhận.
- Chọn Hiệu xe (được lấy từ danh mục `HieuXe`).
- **Quy định**: Số lượng xe tiếp nhận trong ngày không được vượt quá số lượng tối đa (`SoXeSuaChuaToiDa` trong `ThamSo`).

### Bước 2: Lập phiếu sửa chữa (`PhieuSuaChuaView`)
Sau khi kiểm tra xe, kỹ thuật viên tiến hành sửa chữa:
- Nhập biển số xe hoặc chọn xe đã tiếp nhận.
- Ngày sửa chữa.
- **Chi tiết sửa chữa**: Với mỗi nội dung công việc (ví dụ: Thay nhớt, thay lốp):
  - Nhập nội dung sửa chữa.
  - Chọn vật tư, phụ tùng thay thế (`VatTuPhuTung`). Hệ thống tự động tính tiền vật tư = Số lượng * Đơn giá bán (Đơn giá bán = Đơn giá nhập * `TiLeDonGiaBan`).
  - Chọn loại tiền công (`TienCong`).
- Hệ thống sẽ cộng dồn tổng tiền của Phiếu Sửa Chữa và ghi nợ (`TienNo`) vào hồ sơ Xe. Trừ số lượng tồn kho của Vật tư.

### Bước 3: Lập phiếu thu tiền (`PhieuThuTienView`)
Sau khi xe được sửa xong, khách hàng thanh toán:
- Chọn biển số xe cần thanh toán. Hệ thống hiển thị số tiền khách đang nợ (`TienNo`).
- Nhập số tiền thu.
- **Quy định**: Nếu tham số `ApDungQDKiemTraSoTienThu` là 1 (Có), số tiền thu không được vượt quá số tiền khách đang nợ.
- Sau khi lưu, hệ thống tự động trừ tiền nợ của xe.

### Bước 4: Tra cứu xe (`TraCuuXeView`)
Hỗ trợ tìm kiếm thông tin xe theo các tiêu chí: Biển số, Tên chủ xe, Hiệu xe... để kiểm tra lịch sử sửa chữa hoặc nợ hiện tại.

## 3. Các nghiệp vụ quản lý kho

### Lập phiếu nhập kho (`PhieuNhapKhoView`)
Khi Gara hết vật tư, cần nhập hàng:
- Thêm các loại vật tư, phụ tùng và số lượng cần nhập, cùng đơn giá nhập.
- Khi lưu phiếu nhập, hệ thống tự động cộng dồn số lượng vào Tồn kho của vật tư.

## 4. Nghiệp vụ Báo cáo & Thống kê

- **Báo cáo doanh số (`BaoCaoDoanhSoView`)**: Cho phép Quản lý xem tổng doanh thu theo tháng. Báo cáo liệt kê doanh thu của từng hiệu xe, số lượt sửa chữa và tỷ lệ % doanh thu so với tổng doanh thu trong tháng.
- **Báo cáo tồn kho (`BaoCaoTonKhoView`)**: Xem lượng vật tư phụ tùng tồn đầu kỳ, phát sinh (nhập), sử dụng (xuất), tồn cuối kỳ trong 1 tháng nhất định.

## 5. Danh mục và Cài đặt hệ thống (Dành cho Quản lý)

- **Quản lý Hiệu xe / Tiền công / Vật tư**: Thêm, sửa, xóa các danh mục này để cung cấp dữ liệu nền cho phiếu sửa chữa.
- **Thay đổi quy định (`ThayDoiQuyDinhView`)**:
  - Số lượng xe sửa chữa tối đa trong ngày.
  - Tỷ lệ đơn giá bán (Tính giá bán dựa trên giá nhập).
  - Bật/tắt quy định kiểm tra số tiền thu.
- **Quản lý người dùng (`QuanLyNguoiDungView`)**: Tạo mới tài khoản cho nhân viên, đổi mật khẩu, phân quyền.

---
> [!TIP]
> **Khuyên dùng**: Đối với người mới bắt đầu, hãy đăng nhập bằng tài khoản `admin` để có thể thấy và thử nghiệm tất cả các tính năng. Hãy làm thử một luồng cơ bản: `Tiếp nhận xe -> Lập phiếu sửa chữa -> Lập phiếu thu tiền` để hiểu rõ sự liên kết dữ liệu trong hệ thống.
