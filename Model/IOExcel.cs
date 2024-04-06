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

            excelApp = new Excel.Application();
        }

        public void DBToExcelTablePubAndSub()
        {
            int row = 1;

            workbook = excelApp.Workbooks.Add();

            worksheet = workbook.Worksheets[1];

            for (int i = 0; i < allPublication.Count(); i++)
            {
                List<Subscribe> tempSubs = new List<Subscribe>();

                for (int o = 0; o < allSubs.Count(); o++)
                {
                    if (allPublication[i].id_Publication == allSubs[o].id_Publication)
                    {
                        tempSubs.Add(allSubs[o]);
                    }
                }

                List<SubscriberOfThePostOffice> tempSubscruber = new List<SubscriberOfThePostOffice>();

                for (int d = 0; d < tempSubs.Count(); d++)
                {
                    for (int s = 0; s < allSubscriberOfThePostOffices.Count(); s++)
                    {
                        if (tempSubs[d].id_Subscriber == allSubscriberOfThePostOffices[s].id_Subscriber)
                        {
                            tempSubscruber.Add(allSubscriberOfThePostOffices[s]);
                        }
                    }
                }

                worksheet.Cells[row, 1] = $"Публикация: {allPublication[i].Name}";

                worksheet.Cells[row, 1].Font.Bold = true;

                worksheet.Cells[row, 1].Font.size = 14;

                row++;

                if (tempSubscruber.Count() == 1)
                {
                    worksheet.Cells[row, 1] = $"Подписчик";

                    worksheet.Cells[row, 1].Font.Size = 12;
                }

                if (tempSubscruber.Count() > 1)
                {
                    worksheet.Cells[row, 1] = $"Подписчики";

                    worksheet.Cells[row, 1].Font.Size = 12;
                }

                row++;

                worksheet.Cells[row, 1] = $"Фамилия";

                worksheet.Cells[row, 2] = $"Имя";

                worksheet.Cells[row, 3] = $"Отчество";

                worksheet.Cells[row, 4] = $"Номер телефона";

                worksheet.Cells[row, 5] = $"Дата рождения";

                Excel.Range rangeTitle = worksheet.Range[$"A{row}: E{row}"];

                rangeTitle.EntireColumn.AutoFit();

                rangeTitle.EntireRow.AutoFit();

                rangeTitle.Interior.Color = Excel.XlRgbColor.rgbAliceBlue;

                row++;

                for (int k = 0, col = 1; k < tempSubscruber.Count(); k++, row++)
                {
                    worksheet.Cells[row, col] = tempSubscruber[k].Surname;
                    
                    col++;
                    
                    worksheet.Cells[row, col] = tempSubscruber[k].Name;
                    
                    col++;

                    worksheet.Cells[row, col] = tempSubscruber[k].MiddleName;

                    col++;

                    worksheet.Cells[row, col] = tempSubscruber[k].NumberPhone;

                    col++;

                    worksheet.Cells[row, col] = tempSubscruber[k].Birthday;

                    col = 1;
                }
                row++;
            }
            excelApp.Visible = true;

            excelApp.UserControl = true;
        }
    }
}
