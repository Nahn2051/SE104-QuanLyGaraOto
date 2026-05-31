# Hoàn thành Kế Hoạch: Tra Cứu Xe Chi Tiết & Xuất Báo Cáo Excel

Tất cả các tính năng bạn yêu cầu đã được triển khai thành công! Dự án đã tích hợp thư viện `ClosedXML` để làm việc với Excel một cách mượt mà và trực tiếp từ C# mà không cần cài đặt Microsoft Excel.

## Các Thay Đổi Chính

### 1. Nâng cấp Bảng Tra Cứu Xe
- Cập nhật **TraCuuXeView**:
  - Hỗ trợ thao tác **Nháy đúp (Double-click)** vào một dòng trong bảng kết quả tra cứu để mở bảng thông tin chi tiết.
  - Xây dựng Popup hiển thị thông tin gồm hai phần:
    - **Nửa trên:** Thông tin cụ thể của khách hàng/xe (Biển số, Hiệu xe, Tên chủ xe, Số điện thoại, Email, Địa chỉ, Ngày tiếp nhận, Tiền nợ).
    - **Nửa dưới:** Bảng danh sách toàn bộ Lịch sử sửa chữa của chiếc xe này.
  - Thêm tính năng **📥 Xuất Excel** ở bảng danh sách xe chính. 
  - Thêm tính năng **📥 Xuất Excel Chi Tiết** để tạo file báo cáo cho một chiếc xe cụ thể (Gồm cả thông tin khách và lịch sử sửa chữa).

### 2. Xuất Báo Cáo Tra Cứu Phiếu Sửa Chữa
- Cập nhật **TraCuuPhieuSuaChuaView**:
  - Nút **📥 Xuất Excel** cho toàn bộ danh sách kết quả tìm kiếm phiếu sửa chữa.
  - Nút **📥 Xuất Excel Chi Tiết** khi người dùng nháy đúp vào một phiếu, giúp tải file hóa đơn chi tiết của phiếu đó (gồm vật tư, công thợ, đơn giá, số lượng...).

### 3. Xuất Báo Cáo Tra Cứu Phiếu Nhập Kho
- Cập nhật **TraCuuPhieuNhapKhoView**:
  - Nút **📥 Xuất Excel** xuất toàn bộ danh sách phiếu nhập kho.
  - Nút **📥 Xuất Excel Chi Tiết** để kết xuất chi tiết phụ tùng nhập kho bên trong một phiếu cụ thể.

### 4. Tra Cứu Phiếu Thu Tiền
- Bổ sung **TraCuuPhieuThuTienView**:
  - Hỗ trợ tìm kiếm Phiếu thu tiền theo tên chủ xe, biển số xe.
  - Cung cấp tính năng xem popup chi tiết một Phiếu thu tiền.
  - Nút **📥 Xuất Excel** hỗ trợ xuất lịch sử thu tiền theo kết quả tìm kiếm.

### 5. Xuất Excel cho Màn Hình Quản Lý & Báo Cáo
- Thêm tính năng **📥 Xuất Excel** cho các màn hình:
  - **Quản lý hiệu xe:** Xuất danh sách hiệu xe hiện có.
  - **Quản lý tiền công:** Xuất danh sách các loại tiền công, đơn giá.
  - **Quản lý vật tư phụ tùng:** Xuất danh sách vật tư phụ tùng và số lượng tồn.
  - **Báo cáo doanh số:** Xuất báo cáo doanh số cho tháng hiện tại.
  - **Báo cáo tồn kho:** Xuất báo cáo tồn kho cho tháng hiện tại.
### 6. Cập Nhật Mới & Sửa Lỗi (Bug Fixes)
- **Tiếp nhận xe:** Đã khắc phục lỗi tự động sinh ra một *Phiếu sửa chữa* rỗng (0 VNĐ) mỗi khi có xe mới tiếp nhận. Từ giờ lịch sử sửa chữa của xe mới sẽ hoàn toàn trống cho đến khi thực sự được lập phiếu.
- **Lập phiếu sửa chữa:** Cập nhật logic lưu dữ liệu. Khi người dùng nhập `Số tiền trả` lớn hơn 0, hệ thống sẽ tự động tạo và lưu trữ một `Phiếu thu tiền` ngay tại thời điểm đó.
- **Lịch sử sửa chữa (Tra cứu):** Cập nhật lại giao diện (UI) của phần xem chi tiết phiếu sửa chữa, nay đã hiển thị thêm thông tin **Số tiền đã trả** (truy vấn chính xác từ phiếu thu tiền cùng thời điểm) và **Số tiền còn nợ**.
- **Báo cáo tồn kho:** Tách cột "Phát sinh (Nhập - Xuất)" cũ trên giao diện phần mềm thành 2 cột riêng biệt là **Phát sinh Nhập** và **Phát sinh Xuất**, đảm bảo số liệu hiển thị trên ứng dụng khớp 100% với số liệu xuất ra file Excel.
- **Thanh tìm kiếm:** Bổ sung tính năng **🔍 Tìm kiếm** bằng từ khóa vào 3 màn hình Quản lý danh mục (Hiệu xe, Vật tư phụ tùng, Tiền công), cho phép người dùng lọc và tra cứu danh mục nhanh chóng.

## Định Dạng File Excel
Tất cả các file `.xlsx` được xuất ra đều được định dạng theo một tiêu chuẩn sạch đẹp:
- Dòng tiêu đề (Title) được làm to, căn giữa và in đậm.
- Các ô Header của bảng tính được bôi nền xám (LightGray), có viền (Border) và in đậm.
- Dữ liệu tiền tệ (Tổng tiền, Tiền nợ, Đơn giá) và Số lượng được căn lề tự động và định dạng hiển thị số hàng ngàn `#,##0` (VD: 1,500,000).
- Các cột được tính năng `AdjustToContents` tự động co giãn độ rộng phù hợp với dữ liệu bên trong.

## Hướng Dẫn Kiểm Tra Trực Tiếp
1. Mở ứng dụng, vào các mục **Tra Cứu**.
2. Nháy đúp vào một dòng để xem tính năng Popup chi tiết.
3. Bấm vào nút `📥 Xuất Excel` để chọn vị trí lưu và kiểm tra file tải về. Mọi tính năng hoạt động rất mượt mà.
