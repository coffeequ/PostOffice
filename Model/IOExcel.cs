using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace PostOffice.Model
{
    class IOExcel
    {
        Model.DataBasePostOffice dataBasePostOffice;

        Excel.Application excelApp;

        Excel.Workbook workbook;

        Excel.Worksheet worksheet;

        Excel.Worksheet worksheet2;

        Excel.Worksheet worksheet3;

        List<Subscribe> allSubs;

        List<Publication> allPublication;

        List<SubscriberOfThePostOffice> allSubscriberOfThePostOffices;

        List<Correspondence> AllCorrespondences;

        public IOExcel(Model.DataBasePostOffice dataBasePostOffice)
        {
            this.dataBasePostOffice = dataBasePostOffice;

            allSubs = dataBasePostOffice.postOfficeEntities.Subscribe.ToList();

            allPublication = dataBasePostOffice.postOfficeEntities.Publication.ToList();

            allSubscriberOfThePostOffices = dataBasePostOffice.postOfficeEntities.SubscriberOfThePostOffice.ToList();

            AllCorrespondences = dataBasePostOffice.postOfficeEntities.Correspondence.ToList();

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

                else if (tempSubscruber.Count() > 1)
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

                    worksheet.Cells[row, col] = tempSubscruber[k].smallBrithday;

                    col = 1;
                }
                row++;
            }

            worksheet2 = workbook.Worksheets.Add(After: worksheet);

            row = 1;

            worksheet2.Cells[row, 1] = $"Издание";

            worksheet2.Cells[row, 1].Font.Size = 14;

            worksheet2.Cells[row, 2] = $"Количество подписчиков";

            worksheet2.Cells[row, 2].Font.Size = 14;

            Excel.Range rangeTitle3 = worksheet2.Range[$"A{row}: B{row}"];

            rangeTitle3.ColumnWidth = 25;

            rangeTitle3.EntireRow.AutoFit();

            rangeTitle3.Interior.Color = Excel.XlRgbColor.rgbAliceBlue;

            row++;

            int countAllPublicationSubscribe = 0;

            for (int h = 0; h < allPublication.Count(); h++)
            {
                worksheet2.Cells[row, 1] = allPublication[h].Name;

                worksheet2.Cells[row, 2] = allPublication[h].CountSubscribeLastYear;

                countAllPublicationSubscribe += allPublication[h].CountSubscribeLastYear;

                row++;
            }
            row++;

            worksheet2.Cells[row, 1] = "Общее количество подписок: ";

            worksheet2.Cells[row, 1].Font.Bold = true;

            worksheet2.Cells[row, 2] = countAllPublicationSubscribe;

            worksheet2.Cells[row, 2].Font.Bold = true;

            Excel.ChartObjects chartObjects2 = (Excel.ChartObjects)worksheet2.ChartObjects(Type.Missing);

            Excel.ChartObject chartObject2 = chartObjects2.Add(350, 10, 400, 250);

            Excel.Chart chart2 = chartObject2.Chart;

            Excel.Range rangeXl2;

            rangeXl2 = worksheet2.get_Range($"A{2}:A{allSubscriberOfThePostOffices.Count + 1}", $"B{2}:B{allSubscriberOfThePostOffices.Count + 1}");

            chart2.SetSourceData(rangeXl2, Type.Missing);

            chart2.ChartType = Excel.XlChartType.xl3DColumn;

            worksheet3 = workbook.Worksheets.Add(After: worksheet2);

            row = 1;

            worksheet3.Cells[row, 1] = $"Наименование издания";

            worksheet3.Cells[row, 1].Font.Size = 14;

            worksheet3.Cells[row, 2] = $"Сумма за 2024 год";

            worksheet3.Cells[row, 2].Font.Size = 14;

            Excel.Range rangeTitle2 = worksheet3.Range[$"A{row}: B{row}"];

            rangeTitle2.ColumnWidth = 25;

            rangeTitle2.EntireRow.AutoFit();

            rangeTitle2.Interior.Color = Excel.XlRgbColor.rgbAliceBlue;

            row++;

            decimal resultPrice = 0;

            decimal allPrice = 0;

            for (int i = 0; i < allPublication.Count; i++, row++)
            {
                worksheet3.Cells[row, 1] = $"{allPublication[i].Name}";

                resultPrice = 0;

                for (int j = 0; j < allPublication[i].Subscribe.Count(); j++)
                {
                    if (allPublication[i].Subscribe.ToList()[j].EntryTime.Year == DateTime.Now.Year & allPublication[i].Subscribe.ToList()[j].EndTime.Year == DateTime.Now.Year)
                    {
                        resultPrice += allPublication[i].Subscribe.ToList()[j].ResultPrice;
                        allPrice += resultPrice;
                    }
                }

                worksheet3.Cells[row, 2] = double.Parse(resultPrice.ToString());
            }

            worksheet3.Cells[row, 1] = $"Итоговая сумма по всем изданиям за 2024 год: ";

            worksheet3.Cells[row, 1].Font.Bold = true;

            worksheet3.Cells[row, 2] = $"{double.Parse(allPrice.ToString())}";

            worksheet3.Cells[row, 2].Font.Bold = true;

            Excel.ChartObjects chartObjects = (Excel.ChartObjects)worksheet3.ChartObjects(Type.Missing);

            Excel.ChartObject chartObject = chartObjects.Add(350, 10, 400, 250);

            Excel.Chart chart = chartObject.Chart;

            Excel.Range rangeXl;

            rangeXl = worksheet3.get_Range($"A{2}:A{allPublication.Count + 1}", $"B{2}:B{allPublication.Count + 1}");

            chart.SetSourceData(rangeXl, Type.Missing);

            chart.ChartType = Excel.XlChartType.xlPie;

            excelApp.Visible = true;

            excelApp.UserControl = true;


        }

        public void DBToExcelTableCorrespondence()
        {
            int row = 1;

            workbook = excelApp.Workbooks.Add();

            worksheet = workbook.Worksheets[1];

            for (int i = 0; i < allSubscriberOfThePostOffices.Count; i++)
            {
                List<Subscribe> subscribes = new List<Subscribe>();

                for (int l = 0; l < allSubs.Count(); l++)
                {
                    if (allSubscriberOfThePostOffices[i].id_Subscriber == allSubs[l].id_Subscriber)
                    {
                        subscribes.Add(allSubs[l]);
                    }
                }

                List<Correspondence> ActiveCorrespondence = new List<Correspondence>();

                for (int m = 0; m < subscribes.Count; m++)
                {
                    for (int n = 0; n < AllCorrespondences.Count; n++)
                    {
                        if (subscribes[m].id_Subscribe == AllCorrespondences[n].id_Subscribe)
                        {
                            ActiveCorrespondence.Add(AllCorrespondences[n]);
                        }
                    }
                }


                worksheet.Cells[row, 1] = $"Подписчик: {allSubscriberOfThePostOffices[i].Surname} {allSubscriberOfThePostOffices[i].Name} {allSubscriberOfThePostOffices[i].MiddleName}";

                worksheet.Cells[row, 1].Font.Bold = true;

                worksheet.Cells[row, 1].Font.Size = 14;

                row++;

                worksheet.Cells[row, 1] = $"Адрес доставки";

                worksheet.Cells[row, 2] = $"Дата отправки";

                worksheet.Cells[row, 3] = $"Дата доставки";

                Excel.Range rangeTitle = worksheet.Range[$"A{row}: C{row}"];

                rangeTitle.EntireColumn.AutoFit();

                rangeTitle.EntireRow.AutoFit();

                rangeTitle.Interior.Color = Excel.XlRgbColor.rgbAliceBlue;

                row++;

                for (int j = 0, col = 1; j < ActiveCorrespondence.Count(); j++, row++)
                {
                    worksheet.Cells[row, col] = ActiveCorrespondence[j].DeliveryAddres;
                    
                    col++;

                    worksheet.Cells[row, col] = ActiveCorrespondence[j].smallDateOfDispatch;

                    col++;

                    worksheet.Cells[row, col] = ActiveCorrespondence[j].smallDateOfDelivery;

                    col = 1;
                }

                row++;

            }

            excelApp.Visible = true;

            excelApp.UserControl = true;
        }
    }
}
