using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using ClosedXML.Excel;
using Microsoft.Win32;

namespace QuanLyGaraOto.Services
{
    public static class ExcelExportService
    {
        /// <summary>
        /// Mở hộp thoại lưu file và tạo một file Excel cơ bản từ danh sách dữ liệu
        /// </summary>
        public static void ExportListToExcel<T>(IEnumerable<T> data, string sheetName, string title, Dictionary<string, string> columnMapping)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = ".xlsx",
                FileName = $"{sheetName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using var workbook = new XLWorkbook();
                    var ws = workbook.Worksheets.Add(sheetName);

                    // 1. Title
                    ws.Cell(1, 1).Value = title.ToUpper();
                    ws.Cell(1, 1).Style.Font.Bold = true;
                    ws.Cell(1, 1).Style.Font.FontSize = 16;
                    ws.Range(1, 1, 1, columnMapping.Count).Merge();
                    ws.Range(1, 1, 1, columnMapping.Count).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // 2. Headers
                    int colIndex = 1;
                    var propertyNames = new List<string>();
                    foreach (var kvp in columnMapping)
                    {
                        ws.Cell(3, colIndex).Value = kvp.Value;
                        ws.Cell(3, colIndex).Style.Font.Bold = true;
                        ws.Cell(3, colIndex).Style.Fill.BackgroundColor = XLColor.LightGray;
                        ws.Cell(3, colIndex).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        propertyNames.Add(kvp.Key);
                        colIndex++;
                    }

                    // 3. Data
                    int rowIndex = 4;
                    foreach (var item in data)
                    {
                        for (int i = 0; i < propertyNames.Count; i++)
                        {
                            var prop = typeof(T).GetProperty(propertyNames[i]);
                            if (prop != null)
                            {
                                var val = prop.GetValue(item);
                                
                                // Định dạng số hoặc ngày tháng nếu cần
                                if (val is decimal || val is int || val is double)
                                {
                                    ws.Cell(rowIndex, i + 1).Value = XLCellValue.FromObject(val);
                                    ws.Cell(rowIndex, i + 1).Style.NumberFormat.Format = "#,##0";
                                }
                                else if (val is DateTime dateVal)
                                {
                                    ws.Cell(rowIndex, i + 1).Value = dateVal;
                                    ws.Cell(rowIndex, i + 1).Style.NumberFormat.Format = "dd/MM/yyyy HH:mm";
                                }
                                else
                                {
                                    ws.Cell(rowIndex, i + 1).Value = val?.ToString() ?? "";
                                }
                            }
                            ws.Cell(rowIndex, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                        rowIndex++;
                    }

                    // 4. Auto-fit columns
                    ws.Columns().AdjustToContents();

                    // Save
                    workbook.SaveAs(dialog.FileName);
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi khi xuất file Excel:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Mở hộp thoại để lưu file Excel tự do, truyền callback để custom logic
        /// </summary>
        public static void ExportCustomExcel(string defaultFileName, Action<IXLWorkbook> generateWorkbookLogic)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = ".xlsx",
                FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using var workbook = new XLWorkbook();
                    generateWorkbookLogic(workbook);
                    workbook.SaveAs(dialog.FileName);
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi khi xuất file Excel:\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
