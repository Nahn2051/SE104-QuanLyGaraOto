USE GaraOtoDB;

-- 1. Tìm các phiếu nhập có chứa vật tư mới (MaVTPT > 201)
DECLARE @PhieuNhapMoi TABLE (MaPhieuNhap INT);
INSERT INTO @PhieuNhapMoi
SELECT DISTINCT MaPhieuNhap FROM CT_PHIEUNHAP WHERE MaVTPT > 201;

-- 2. Xóa chi tiết phiếu nhập của các phiếu này
DELETE FROM CT_PHIEUNHAP WHERE MaPhieuNhap IN (SELECT MaPhieuNhap FROM @PhieuNhapMoi);

-- 3. Xóa các phiếu nhập này
DELETE FROM PHIEUNHAP WHERE MaPhieuNhap IN (SELECT MaPhieuNhap FROM @PhieuNhapMoi);

-- 4. Tìm và xóa CT Sửa chữa nếu có dùng vật tư mới
DELETE FROM CT_PHIEUSUACHUA WHERE MaVTPT > 201;

-- 5. Xóa 2 vật tư mới khỏi bảng VATTUPHUTUNG
DELETE FROM VATTUPHUTUNG WHERE MaVTPT > 201;

-- 6. Reset Identity cho VATTUPHUTUNG để ID tiếp theo bắt đầu từ 202
DBCC CHECKIDENT ('VATTUPHUTUNG', RESEED, 201);
GO
