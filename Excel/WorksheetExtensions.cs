using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Shared.Html.Excel
{
    public static class WorksheetExtensions
    {
        public static void LabelThis(this ExcelRange range, string label)
        {
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            range.Style.Font.Italic = true;
            range.Value = label;
        }
    }
}