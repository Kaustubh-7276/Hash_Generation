using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
namespace HashGenerator
{
    /// <summary>
    /// Interaction logic for PrintPreviewWindow.xaml
    /// </summary>
    public partial class PrintPreviewWindow : Window
    {
        public PrintPreviewWindow()
        {
            InitializeComponent();
            LoadPrinters();
        }

        public IDocumentPaginatorSource Document
        {
            set => docViewer.Document = value;
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (printerList.SelectedItem is PrintQueue selectedQueue)
            {
                PrintDialog pd = new PrintDialog();

                // Assign the user's selected printer directly to the dialog
                pd.PrintQueue = selectedQueue;

                // Execute the print without showing the system dialog again
                pd.PrintDocument(docViewer.Document.DocumentPaginator, "Hash Report Generation");

                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a printer first.");
            }
        }
        private void LoadPrinters()
        {
            try
            {
                // 1. Initialize the local print server
                var printServer = new LocalPrintServer();

                // 2. Get all local and network connected printers
                var flags = new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections };
                var printerQueues = printServer.GetPrintQueues(flags);

                // 3. Bind to the ComboBox
                printerList.ItemsSource = printerQueues;

                // 4. Pre-select the system's default printer
                printerList.SelectedItem = printServer.DefaultPrintQueue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading printers: " + ex.Message);
            }
        }
    }
}
