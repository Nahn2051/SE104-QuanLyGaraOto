USE GaraOtoDB;

DECLARE @MaXe INT;
SELECT @MaXe = MaXe FROM XE WHERE BienSo = '68A03235';

IF @MaXe IS NOT NULL
BEGIN
    -- Lấy danh sách phiếu sửa chữa của xe này
    DECLARE @PhieuSuaChuaTable TABLE (MaPhieuSuaChua INT);
    INSERT INTO @PhieuSuaChuaTable SELECT MaPhieuSuaChua FROM PHIEUSUACHUA WHERE MaXe = @MaXe;

    -- Xóa CT_PHIEUSUACHUA
    DELETE FROM CT_PHIEUSUACHUA WHERE MaPhieuSuaChua IN (SELECT MaPhieuSuaChua FROM @PhieuSuaChuaTable);
    
    -- Xóa PHIEUSUACHUA
    DELETE FROM PHIEUSUACHUA WHERE MaXe = @MaXe;

    -- Xóa PHIEUTHUTIEN
    DELETE FROM PHIEUTHUTIEN WHERE MaXe = @MaXe;

    -- Xóa XE
    DELETE FROM XE WHERE MaXe = @MaXe;
END
GO
