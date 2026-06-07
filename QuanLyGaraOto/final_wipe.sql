USE GaraOtoDB;

-- 1. Xóa sạch sẽ toàn bộ Phiếu Nhập Kho và Lịch Sử Nhập Kho
DELETE FROM CT_PHIEUNHAP;
DELETE FROM PHIEUNHAP;
DBCC CHECKIDENT ('PHIEUNHAP', RESEED, 0);
DBCC CHECKIDENT ('CT_PHIEUNHAP', RESEED, 0);

-- 2. Xóa toàn bộ Vật Tư Phụ Tùng rác (bất cứ vật tư nào có mã > 200 do người dùng tạo thêm)
DELETE FROM CT_PHIEUSUACHUA WHERE MaVTPT > 200;
DELETE FROM VATTUPHUTUNG WHERE MaVTPT > 200;

-- 3. Đưa bộ đếm Vật Tư về đúng 200 để tạo mới sẽ là 201
DBCC CHECKIDENT ('VATTUPHUTUNG', RESEED, 200);
GO
