using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeysApplication
{
    public class CreateFile
    {
        public static void CreateXLSXFile(string excelFileName, string worksheetName, DataTable data)
        {
            if (data != null)
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet worksheet = workbook.CreateSheet("ForeignKeys"); //worksheetName
                ICreationHelper creationHelper = workbook.GetCreationHelper();                              

                IRow columnName = worksheet.CreateRow(0);

                for (int i = 0; i < data.Columns.Count; i++)
                {
                    columnName.CreateCell(i).SetCellValue(data.Columns[i].ColumnName.ToString());
                    worksheet.SetColumnWidth(data.Columns[i].Ordinal, 5000);
                }               

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    IRow row = worksheet.CreateRow(i+1);

                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        row.CreateCell(j).SetCellValue(creationHelper.CreateRichTextString(data.Rows[i].ItemArray[j].ToString()));
                    }
                }

                using (FileStream fileWriter = File.Create(@"C:\Users\grzegorz.borowski\Desktop\Prezes\Dupa.xlsx"))
                {
                    workbook.Write(fileWriter);
                    fileWriter.Close();
                }

                worksheet = null;
                workbook = null;
            }
        }

        public static void CreateCSVFile()
        {

        }
    }
}
