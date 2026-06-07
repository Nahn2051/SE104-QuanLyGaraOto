USE GaraOtoDB;

-- Làm tròn Đơn giá về hàng nghìn (VD: 250047 -> 250000)
UPDATE VATTUPHUTUNG
SET DonGia = ROUND(DonGia, -3);

-- Xóa các người dùng rác, chỉ giữ lại tài khoản admin (1) và nhanvien (2)
DELETE FROM NGUOIDUNG WHERE MaNguoiDung NOT IN (1, 2);
GO
