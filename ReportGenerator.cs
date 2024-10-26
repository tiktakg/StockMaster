using StockMaster.Entitys;
using StockMaster.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace StockMaster

{

    public class ReportGenerator
    {
        public static void GenerateReport(int userId)
        {
            // Retrieve the user data
            User currentUser = Tools.GetCurrentUserById(userId);
            List<PortfolioViewModel> portfolioData = Tools.GetCurrentPortfolio(Tools.GetIdPortfolio(userId));

            // Define the file path and append the username to the file name
            string filePath = Environment.CurrentDirectory + $"\\Отчет_{currentUser.Username}.docx";

            using (var document = DocX.Create(filePath))
            {
                // Report title and user information
                document.InsertParagraph("Отчет по портфелю").FontSize(16).Bold().Alignment = Alignment.center;
                document.InsertParagraph($"Сгенерировано для: {currentUser.Username} ({currentUser.Email})")
                        .FontSize(12).Italic().SpacingAfter(10);

                if (portfolioData == null || portfolioData.Count == 0)
                {
                    document.InsertParagraph("Нет данных для отображения.").FontSize(12).Italic();
                }
                else
                {
                    var table = document.AddTable(portfolioData.Count + 1, 4);

                    // Table headers
                    table.Rows[0].Cells[0].Paragraphs[0].Append("Название").Bold();
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Цена покупки").Bold();
                    table.Rows[0].Cells[2].Paragraphs[0].Append("Количество").Bold();
                    table.Rows[0].Cells[3].Paragraphs[0].Append("Сумма").Bold();

                    // Populate table data
                    for (int i = 0; i < portfolioData.Count; i++)
                    {
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append(portfolioData[i].Name ?? "N/A");
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(portfolioData[i].BuyPrice?.ToString("F2") ?? "N/A");
                        table.Rows[i + 1].Cells[2].Paragraphs[0].Append(portfolioData[i].Quantity?.ToString("F2") ?? "N/A");
                        table.Rows[i + 1].Cells[3].Paragraphs[0].Append(portfolioData[i].Sum?.ToString("F2") ?? "N/A");
                    }

                    document.InsertTable(table);
                }

                document.Save();
            }

            var messageBox = new MessageBoxForOpenFile();
            messageBox.ShowDialog();

            if (messageBox.IsOpenClicked)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
        }
    }

}
