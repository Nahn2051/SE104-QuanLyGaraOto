USE GaraOtoDB;

-- 1. Xóa chi tiết sửa chữa và phiếu sửa chữa nếu dính tới vật tư > 200
DELETE FROM CT_PHIEUSUACHUA WHERE MaVTPT > 200;

-- 2. Xóa chi tiết phiếu nhập và phiếu nhập nếu dính tới vật tư > 200
DECLARE @PhieuNhapMoi TABLE (MaPhieuNhap INT);
INSERT INTO @PhieuNhapMoi
SELECT DISTINCT MaPhieuNhap FROM CT_PHIEUNHAP WHERE MaVTPT > 200;

DELETE FROM CT_PHIEUNHAP WHERE MaPhieuNhap IN (SELECT MaPhieuNhap FROM @PhieuNhapMoi);
DELETE FROM PHIEUNHAP WHERE MaPhieuNhap IN (SELECT MaPhieuNhap FROM @PhieuNhapMoi);

-- 3. Xóa vật tư có mã > 200 (Bao gồm cái 201 anh vừa nói)
DELETE FROM VATTUPHUTUNG WHERE MaVTPT > 200;

-- 4. Reset bộ đếm về 200
DBCC CHECKIDENT ('VATTUPHUTUNG', RESEED, 200);
GO
