$text = Get-Content -Path "Migrations\20260529100708_SeedDanhMuc.cs" -Raw
$matches = [regex]::Matches($text, '\{\s*(\d+),\s*(\d+)m,\s*(\d+),\s*"[^"]+"\s*\}')
$sql = "USE GaraOtoDB;`r`n"
foreach ($m in $matches) {
    $id = $m.Groups[1].Value
    $qty = $m.Groups[3].Value
    $sql += "UPDATE VATTUPHUTUNG SET SoLuongTon = $qty WHERE MaVTPT = $id;`r`n"
}
$sql | Set-Content -Path "restore_qty.sql" -Encoding UTF8
