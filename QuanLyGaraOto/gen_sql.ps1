$text = Get-Content -Path "Migrations\20260529100708_SeedDanhMuc.cs" -Raw
$matches = [regex]::Matches($text, '\{\s*(\d+),\s*(\d+)m,\s*\d+,\s*"[^"]+"\s*\}')
$sql = "USE GaraOtoDB;`r`n"
foreach ($m in $matches) {
    $id = $m.Groups[1].Value
    $price = $m.Groups[2].Value
    $sql += "UPDATE VATTUPHUTUNG SET DonGia = $price WHERE MaVTPT = $id;`r`n"
}
$sql += "DELETE FROM NGUOIDUNG WHERE MaNguoiDung NOT IN (1, 2);`r`n"
$sql | Set-Content -Path "restore_prices.sql" -Encoding UTF8
