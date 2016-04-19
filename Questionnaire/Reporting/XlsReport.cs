using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;

namespace Questionnaire.Reporting
{
    public class XlsReport : Report
    {

        public byte[] GenerateReport(string reportID)
        {

            //fetch data
            var result = FetchReportData(reportID);

            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 20 * 256);//Question
            sheet.SetColumnWidth(1, 20 * 256);//Response
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 20 * 256);

            int basicRowNumber = 2;
            //save basic info to sheet
            foreach (var resultRow in result)
            {
                //Create a new Row
                var QuestionnaireName = sheet.CreateRow(basicRowNumber++);

                //Set the Values for Cells
                QuestionnaireName.CreateCell(1).SetCellValue(resultRow.QuestionnaireName);
                QuestionnaireName.Cells[0].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);

                basicRowNumber = 4;

                var row1 = sheet.CreateRow(basicRowNumber++);

                row1.CreateCell(0).SetCellValue("Date of Call");
                row1.Cells[0].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);
                row1.CreateCell(1).SetCellValue(resultRow.field1);
                row1.Cells[1].CellStyle = GetCellStyle(workbook);
                row1.CreateCell(2).SetCellValue("Company");
                row1.Cells[2].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);
                row1.CreateCell(3).SetCellValue(resultRow.field2);
                row1.Cells[3].CellStyle = GetCellStyle(workbook);

                var row2 = sheet.CreateRow(basicRowNumber++);

                row2.CreateCell(0).SetCellValue("Client Lead");
                row2.Cells[0].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);
                row2.CreateCell(1).SetCellValue(resultRow.field3);
                row2.Cells[1].CellStyle = GetCellStyle(workbook);
                row2.CreateCell(2).SetCellValue("Deltek Sales Lead");
                row2.Cells[2].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);
                row2.CreateCell(3).SetCellValue(resultRow.field4);
                row2.Cells[3].CellStyle = GetCellStyle(workbook);

                var row3 = sheet.CreateRow(basicRowNumber++);

                row3.CreateCell(0).SetCellValue("Infotek Lead");
                row3.Cells[0].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);
                row3.CreateCell(1).SetCellValue(resultRow.field5);
                row3.Cells[1].CellStyle = GetCellStyle(workbook);
                row3.CreateCell(2).SetCellValue("Contact Number");
                row3.Cells[2].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);
                row3.CreateCell(3).SetCellValue(resultRow.Contact);
                row3.Cells[3].CellStyle = GetCellStyle(workbook);

                var row4 = sheet.CreateRow(basicRowNumber++);

                row4.CreateCell(0).SetCellValue("Contact Email");
                row4.Cells[0].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);
                row4.CreateCell(1).SetCellValue(resultRow.Email);
                row4.Cells[1].CellStyle = GetCellStyle(workbook);
                row4.CreateCell(2).SetCellValue("Created At");
                row4.Cells[2].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);
                row4.CreateCell(3).SetCellValue(resultRow.CreatedAt);
                row4.Cells[3].CellStyle = GetCellStyle(workbook);
                break;
            }

            //Create a header row
            var headerRow = sheet.CreateRow(9);
            headerRow.CreateCell(0).SetCellValue("Question");
            headerRow.Cells[0].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);
            headerRow.CreateCell(1).SetCellValue("Response");
            headerRow.Cells[1].CellStyle = GetCellStyle(workbook, HSSFColor.RoyalBlue.Index);

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 10;

            //Populate the sheet with values from the grid data

            foreach (var resultRow in result)
            {
                //Create a new Row
                var row = sheet.CreateRow(rowNumber++);

                //Set the Values for Cells
                row.CreateCell(0).SetCellValue(resultRow.Text);
                row.Cells[0].CellStyle = GetCellStyle(workbook);
                row.CreateCell(1).SetCellValue(resultRow.Response);
                row.Cells[1].CellStyle = GetCellStyle(workbook);
            }

            //Write the Workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            return output.ToArray();


        }

        private ICellStyle GetCellStyle(HSSFWorkbook workbook, short colorIndex = 0)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;//Left and right center
            style.VerticalAlignment = VerticalAlignment.Center;//Up and down the middle
            style.WrapText = true;//Word wrap
            style.BorderBottom = BorderStyle.Thin;//The next frame as a thin line frame
            style.BorderLeft = BorderStyle.Thin;//Left border
            style.BorderRight = BorderStyle.Thin;//Right border
            style.BorderTop = BorderStyle.Thin;//The upper frame
            if (colorIndex != 0)
            {
                style.FillForegroundColor = colorIndex;
                style.FillPattern = FillPattern.SolidForeground;//Fill pattern for panchromatic
            }
            IFont font = workbook.CreateFont();
            font.FontName = "Open Sans";
            font.FontHeightInPoints = 10;
            if (colorIndex != 0)
            {
                font.Boldweight = short.MaxValue;//Bold
                font.Color = HSSFColor.White.Index;
            }
            style.SetFont(font);
            return style;
        }


    }
}