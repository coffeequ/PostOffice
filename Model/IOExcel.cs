using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace PostOffice.Model
{
    class IOExcel
    {
        Model.DataBasePostOffice dataBasePostOffice;

        Excel.Application excelApp;

        Excel.Workbook workbook;

        Excel.Worksheet worksheet;

        List<Subscribe> allSubs;

        List<Publication> allPublication;

        List<SubscriberOfThePostOffice> allSubscriberOfThePostOffices;

        public IOExcel(Model.DataBasePostOffice dataBasePostOffice)
        {
            this.dataBasePostOffice = dataBasePostOffice;

            allSubs = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            allPublication = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            allSubscriberOfThePostOffices = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();
        }

        public void DBToExcelTablePubAndSub()
        {
            int row = 1;

            int col = 1;

            workbook = excelApp.Workbooks.Add();

            worksheet = workbook.Worksheets[1];

            for (int i = 0; i < allPublication.Count(); i++)
            {
                worksheet.Cells[row, col] = $"Публикация: {allPublication[i].Name}";
                
                worksheet.Cells[row, col].Font.Bold = true;
                
                worksheet.Cells[row, col].Font.size = 16;
                
                row++;
                
                worksheet.Cells[row, col] = $"Подписчик";
                
                col++;
                
                worksheet.Cells[row, col] = $"Статус активности";
                
                col++;
                
                worksheet.Cells[row, col] = $"Дата начала действия подписки";
                
                col++;
                
                worksheet.Cells[row, col] = $"Дата окончания действия подписки";
                
                col++;
                
                worksheet.Cells[row, col] = $"Дата регистрации";

                Excel.Range rangeTitle = worksheet.Range[$"A{row}: E{row}"];

                rangeTitle.EntireColumn.AutoFit();

                rangeTitle.EntireRow.AutoFit();

                rangeTitle.Interior.Color = Excel.XlRgbColor.rgbAliceBlue;

                row++;

               
            }

        }
    }
}
