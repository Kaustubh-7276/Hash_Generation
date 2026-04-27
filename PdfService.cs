using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
namespace HashGenerator.Services
{
    public class PdfService
    {
        public void GeneratePdf(List<Grid> pages)
        {
            try
            {
                // 1. Create the FixedDocument
                FixedDocument document = new FixedDocument();

                foreach (Grid pageContent in pages)
                {
                    FixedPage fixedPage = new FixedPage();
                    fixedPage.Width = 793;  // A4 Width at 96 DPI
                    fixedPage.Height = 1122; // A4 Height at 96 DPI

                    Viewbox viewbox = new Viewbox();
                    viewbox.Stretch = Stretch.Uniform;
                    viewbox.Child = pageContent;

                    fixedPage.Children.Add(viewbox);

                    PageContent page = new PageContent();
                    ((IAddChild)page).AddChild(fixedPage);

                    document.Pages.Add(page);
                }

                // 2. Use the built-in WPF PrintDialog (No System.Printing needed)
                PrintDialog printDlg = new PrintDialog();

                // 3. Show the dialog to the user
                // The user should select "Microsoft Print to PDF" here
                if (printDlg.ShowDialog() == true)
                {
                    // 4. Print the document using the DocumentPaginator
                    printDlg.PrintDocument(document.DocumentPaginator, "Generating PDF Content");
                }
            }
            catch (Exception ex)
            {
                // Using standard string concatenation for C# 7.3
                MessageBox.Show("An error occurred during PDF generation: " + ex.Message);
            }
        }
    }
}
