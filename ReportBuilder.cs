using HashGenerator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace HashGenerator.Services
{
    public class ReportBuilder
    {
        public List<Grid> BuildPages(string file, string gen, string branch, string passKey, string hash)
        {
            var pages = new List<Grid>();

            var page = CreatePage(file, gen, branch, passKey, hash);

            pages.Add(page);

            return pages;
        }

        private Grid CreatePage(string file, string gen, string branch, string passKey, string hash)
        {
            var root = new Grid
            {
                Width = 793,
                Height = 1122,
                Background = Brushes.White
            };

            var border = new Border
            {
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C2F83")),
                BorderThickness = new Thickness(2),
                Margin = new Thickness(20),
                Padding = new Thickness(30)
            };

            var main = new StackPanel();

            // HEADER
            main.Children.Add(CreateHeader("FILE HASH REPORT"));

            // LINE
            main.Children.Add(new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 1, 0, 0),
                Margin = new Thickness(0, 8, 0, 18)
            });

            // DETAILS
            main.Children.Add(CreateDetails(file, gen, branch, passKey));

            // HASH
            main.Children.Add(CreateHash(hash));

            // SIGNATURE
            main.Children.Add(CreateSignature());

            // FOOTER
            main.Children.Add(new TextBlock
            {
                Text = $"TELLER ID: 243186 | Timestamp: {DateTime.Now}",
                FontSize = 9,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 40, 0, 0)
            });

            border.Child = main;
            root.Children.Add(border);

            // WATERMARK
            root.Children.Add(new TextBlock
            {
                Text = "CONFIDENTIAL",
                FontSize = 80,
                Foreground = new SolidColorBrush(Color.FromArgb(25, 0, 0, 0)),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                RenderTransform = new RotateTransform(-30)
            });

            return root;
        }

        // ================= HEADER =================
        private UIElement CreateHeader(string ReportName)
        {
            var grid = new Grid
            {
                Margin = new Thickness(0, 0, 0, 10)
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(160) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var logo = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resources/SBI.png")),
                Height = 55
            };

            var textPanel = new StackPanel
            {
                Margin = new Thickness(10, 0, 0, 0)
            };

            textPanel.Children.Add(new TextBlock
            {
                Text = ReportName,
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C2F83"))
            });

            textPanel.Children.Add(new TextBlock
            {
                Text = "State Bank of India - Confidential",
                FontSize = 11,
                Foreground = Brushes.Gray
            });

            Grid.SetColumn(logo, 0);
            Grid.SetColumn(textPanel, 1);

            grid.Children.Add(logo);
            grid.Children.Add(textPanel);

            return grid;
        }

        // ================= DETAILS =================
        private StackPanel CreateDetails(string file, string gen, string branch, string passKey)
        {
            var panel = new StackPanel();

            panel.Children.Add(CreateRow("Original File Name:", file));
            panel.Children.Add(CreateRow("Generated File Name:", gen));
            panel.Children.Add(CreateRow("Branch Code:", branch));

            // PASSKEY
            var row = new Grid { Margin = new Thickness(0, 12, 0, 0) };

            row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220) });
            row.ColumnDefinitions.Add(new ColumnDefinition());

            var lbl = CreateLabel("Security PassKey:");

            var box = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Padding = new Thickness(10, 4, 10, 4),
                Width = 90,
                Child = new TextBlock
                {
                    Text = passKey,
                    FontWeight = FontWeights.Bold
                }
            };

            Grid.SetColumn(lbl, 0);
            Grid.SetColumn(box, 1);

            row.Children.Add(lbl);
            row.Children.Add(box);

            panel.Children.Add(row);

            return panel;
        }

        // ================= ROW =================
        private Grid CreateRow(string label, string value)
        {
            var grid = new Grid
            {
                Margin = new Thickness(0, 6, 0, 6)
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var lbl = CreateLabel(label);

            var val = new TextBlock
            {
                Text = value,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 480
            };

            Grid.SetColumn(lbl, 0);
            Grid.SetColumn(val, 1);

            grid.Children.Add(lbl);
            grid.Children.Add(val);

            return grid;
        }

        private TextBlock CreateLabel(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontWeight = FontWeights.Bold,
                FontSize = 13
            };
        }

        // ================= HASH =================
        private UIElement CreateHash(string hash)
        {
            return new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(245, 245, 245)),
                Margin = new Thickness(0, 25, 0, 10),
                Padding = new Thickness(12),
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                Child = new StackPanel
                {
                    Children =
                    {
                        new TextBlock
                        {
                            Text = "FILE HASH VALUE:",
                            FontWeight = FontWeights.Bold
                        },
                        new TextBlock
                        {
                            Text = hash,
                            FontFamily = new FontFamily("Consolas"),
                            TextWrapping = TextWrapping.Wrap
                        }
                    }
                }
            };
        }

        // ================= SIGNATURE =================
        private UIElement CreateSignature()
        {
            return new StackPanel
            {
                Margin = new Thickness(0, 100, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Right,
                Children =
                {
                    new Border
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(0,1,0,0),
                        Width = 220
                    },
                    new TextBlock
                    {
                        Text = "Authorised Official Signature",
                        FontSize = 10
                    }
                }
            };
        }
        public List<Grid> BuildBulkPages(ObservableCollection<BulkHashResultItem> items, string branch)
        {
            var pages = new List<Grid>();
            int itemsPerPage = 12; // Adjusted for your signature and header height
            int totalPages = (int)Math.Ceiling((double)items.Count / itemsPerPage);

            for (int i = 0; i < totalPages; i++)
            {
                var pageItems = items.Skip(i * itemsPerPage).Take(itemsPerPage).ToList();
                pages.Add(CreateBulkPage(pageItems, branch, i + 1, totalPages));
            }
            return pages;
        }
        private Grid CreateBulkPage(List<BulkHashResultItem> items, string branch, int pageNum, int totalPages)
        {
            var root = new Grid { Width = 793, Height = 1122, Background = Brushes.White };
            var border = new Border
            {
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C2F83")),
                BorderThickness = new Thickness(2),
                Margin = new Thickness(20),
                Padding = new Thickness(30)
            };

            var main = new StackPanel();
            main.Children.Add(CreateHeader("BULK FILE HASH REPORT")); // Reusing your existing Header

            // Bulk Info Bar
            var infoBar = new DockPanel { Margin = new Thickness(0, 5, 0, 15) };
            infoBar.Children.Add(new TextBlock { Text = $"BULK FILE HASH GENERATION SUMMARY", FontWeight = FontWeights.Bold, Foreground = Brushes.DarkBlue });
            var pageTxt = new TextBlock { Text = $"Page {pageNum} of {totalPages} | Branch: {branch}", HorizontalAlignment = HorizontalAlignment.Right };
            DockPanel.SetDock(pageTxt, Dock.Right);
            infoBar.Children.Add(pageTxt);
            main.Children.Add(infoBar);

            // TABLE STRUCTURE
            var tableGrid = new Grid();
            tableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });  // Sr
            tableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) }); // Names
            tableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) }); // PassKey
            tableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Hash

            // Table Header Row
            AddTableRow(tableGrid, new[] { "Sr", "File Details", "PassKey", "Hash (Prefix)" }, true, 0);

            // Table Data Rows
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                string fileInfo = $"Orig: {item.BulkOriginalFileName}\nGen: {item.BulkGeneratedFileName}";
                string shortHash = item.BulkFileHashValue.Length > 15 ? item.BulkFileHashValue.Substring(0, 15) + "..." : item.BulkFileHashValue;

                AddTableRow(tableGrid, new[] { item.BulkSrNo.ToString(), fileInfo, item.BulkPassKey, shortHash }, false, i + 1);
            }
            main.Children.Add(tableGrid);

            // Only add signature on the LAST page
            if (pageNum == totalPages)
            {
                main.Children.Add(CreateSignature());
            }

            // FOOTER
            main.Children.Add(new TextBlock
            {
                Text = $"TELLER ID: 243186 | Generated: {DateTime.Now} | SBI Confidential",
                FontSize = 9,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 30, 0, 0)
            });

            border.Child = main;
            root.Children.Add(border);
            return root;
        }

        private void AddTableRow(Grid grid, string[] columns, bool isHeader, int rowIndex)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            for (int i = 0; i < columns.Length; i++)
            {
                var border = new Border
                {
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    Padding = new Thickness(5),
                    Background = isHeader ? new SolidColorBrush(Color.FromRgb(240, 240, 240)) : Brushes.Transparent
                };
                var txt = new TextBlock
                {
                    Text = columns[i],
                    FontWeight = isHeader ? FontWeights.Bold : FontWeights.Normal,
                    FontSize = isHeader ? 11 : 10,
                    TextWrapping = TextWrapping.Wrap
                };
                border.Child = txt;
                Grid.SetColumn(border, i);
                Grid.SetRow(border, rowIndex);
                grid.Children.Add(border);
            }
        }
    }
}
