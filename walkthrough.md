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

## 5. Cải Tiến Quy Trình và Cho Phép Hoàn Tác (Hủy)

### 5.1. Tách Biệt Tiền Thu Từ Phiếu Sửa Chữa
- Không còn tạo phiếu thu tiền ngầm khi thanh toán ngay trong lúc lập Phiếu Sửa Chữa.
- Thêm trường `TienThu` trực tiếp vào bảng `PHIEUSUACHUA` trong Database, cho phép lưu trữ số tiền khách hàng trả ngay trên phiếu sửa chữa.
- Cập nhật giao diện **Lịch sử (Tra cứu) Phiếu sửa chữa** hiển thị trực tiếp số tiền "Đã Trả" và "Còn Nợ".

### 5.2. Hỗ Trợ Hủy Phiếu Và Rollback Dữ Liệu
Thêm các nút "🗑️ Hủy phiếu" trong các màn hình Tra cứu / Lịch sử, với logic tự động hoàn tác:
- **Hủy Phiếu Thu Tiền:** Số tiền thu được tự động cộng ngược trở lại vào `TienNo` của chủ xe.
- **Hủy Phiếu Nhập Kho:** Số lượng vật tư nhập tự động bị trừ đi khỏi `SoLuongTon`. (Hệ thống có kiểm tra an toàn: nếu tồn kho hiện tại nhỏ hơn số lượng cần trừ do đã xuất xưởng, sẽ chặn không cho hủy để tránh âm kho).
- **Hủy Phiếu Sửa Chữa:** Số lượng vật tư phụ tùng dùng cho sửa chữa được cộng trả lại vào kho. Tiền nợ đã phát sinh do phiếu này (tổng tiền - tiền đã trả) được trừ đi khỏi `TienNo` của chủ xe.
- **Xóa Xe Tiếp Nhận:** Cho phép xóa hẳn xe và chủ xe khỏi hệ thống nếu nhập sai (Chỉ cho phép xóa khi xe chưa từng phát sinh phiếu sửa chữa hay phiếu thu tiền nào).
### 5.3. Xử Lý Thu Tiền Vượt Nợ (Tiền Phạt)
- **Tình huống:** Khách nợ 150.000đ nhưng bị tính thu phạt thành 200.000đ.
- **Giải pháp thu tiền:** Nợ của khách sẽ chỉ được trừ kịch kim về `0đ`. 50.000đ thu dôi ra được ghi nhận riêng thành tiền phạt (doanh thu khác), không cộng dồn làm âm nợ (credit) để cấn trừ vào lần sửa sau.
- **Hoàn tác (Hủy) thu tiền thông minh:** Khi hủy phiếu thu 200.000đ này, hệ thống sẽ đối chiếu và chỉ cộng lại đúng `150.000đ` nợ gốc thực tế vào hồ sơ của khách (rollback theo `TienNoTruocThu`), đảm bảo không có đồng tiền phạt nào bị cộng dồn biến thành tiền khách nợ.

### 5.4. Cập Nhật UI/UX & Tính Logic
- **Đơn giá:** Đổi tên hiển thị từ "Đơn giá nhập" thành "Đơn giá bán" trong màn hình Quản lý vật tư để đúng logic phần mềm.
- **Ràng buộc Thời gian (Validation):** Bổ sung bộ lọc `Từ ngày` - `Đến ngày` ở toàn bộ các màn hình Lịch sử/Tra cứu (Lịch sử tiếp nhận xe, Tra cứu phiếu sửa chữa, Tra cứu phiếu thu, Tra cứu phiếu nhập). Kèm theo đó là logic tự động bắt lỗi hiển thị `MessageBox` cảnh báo chặn tìm kiếm nếu người dùng nhập `Từ ngày > Đến ngày`.
- **Lịch sử tiếp nhận xe:** Nâng cấp bảng `DataGrid` để hiển thị toàn bộ chi tiết liên lạc của khách hàng từ form Tiếp nhận xe (Bao gồm: *Biển số, Hiệu xe, Tên chủ xe, Điện thoại, Địa chỉ, Email, Ngày tiếp nhận, Tiền nợ*), thay vì chỉ hiển thị các cột cơ bản giống bên màn hình Tra cứu xe.
- **Tách riêng Màn hình Tra Cứu:**
  - `Tra cứu xe`: Chỉ cho phép tìm kiếm và xem lịch sử sửa chữa (Read-only, không có nút Xóa).
  - `Lịch sử tiếp nhận xe` (Mới): Được cấp quyền "🗑️ Xóa Xe" nếu có sai sót trong quá trình tiếp nhận (với điều kiện xe chưa phát sinh phiếu).

## 6. Verification
Tất cả mã nguồn đều đã được kiểm tra trên các `ViewModels` để đảm bảo EF Core theo dõi và cập nhật đúng dữ liệu. Database đã được đồng bộ thông qua EF Migration. Giao diện (WPF XAML) cũng đã được căn chỉnh và bổ sung các nút với màu sắc cảnh báo đúng chuẩn thiết kế hiện đại.

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

## 7. Các Cập Nhật Chốt Sổ Cuối Cùng (Bảo Hành, Giảm Giá & Validation)

### 7.1. Tính Năng Giảm Giá Phiếu Sửa Chữa
- Bổ sung ô **Giảm giá** trực tiếp trên màn hình lập Phiếu sửa chữa.
- Tính năng tự động nhận diện giá trị thông minh:
  - Nhập số thẳng (VD: `50000`): Giảm trực tiếp 50.000 VNĐ.
  - Nhập phần trăm (VD: `10%`): Tự động tính 10% của Tổng tiền phiếu và giảm số tiền tương ứng.
- Hiển thị trực quan số tiền giảm trong Lịch sử Tra cứu Phiếu sửa chữa.
- Logic Hoàn tác (Hủy phiếu) được nâng cấp để cộng/trừ chính xác số tiền nợ sau khi đã khấu trừ giảm giá, đảm bảo dòng tiền tuyệt đối không bị sai lệch.

### 7.2. Tự Động Hóa Tiền Công Bảo Hành
- Nếu nhân viên chọn loại tiền công là **"Bảo hành"**, hệ thống sẽ **tự động ép Đơn giá của vật tư/phụ tùng đó về 0đ**.
- Tính năng này giúp gara dễ dàng bảo hành linh kiện cho khách mà không tốn công tính toán hay sửa đơn giá bằng tay, vẫn đảm bảo ghi nhận vật tư đã xuất khỏi kho để bảo hành.

### 7.3. Ràng Buộc Validation Chặt Chẽ
- **Tiếp nhận xe:** Bắt buộc phải nhập số điện thoại (`DienThoai` Not Null) để Gara dễ dàng liên lạc. Dấu `*` màu đỏ được thêm vào giao diện để cảnh báo trực quan.
- **Quy định số xe tối đa:** Khi quản lý đổi số xe tối đa trong ngày, hệ thống sẽ chốt chặn query ngay xem hôm nay đã nhận bao nhiêu chiếc. Nếu số muốn đổi `<` số đã nhận hôm nay, hệ thống lập tức báo lỗi và cấm lưu để ngăn chặn lỗi logic quy trình kinh doanh.
