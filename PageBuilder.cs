using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HashGenerator.Services
{
    public class PageBuilder
    {
        public Grid CreatePage(int pageNumber, int totalPages)
        {
            var root = new Grid
            {
                Width = LayoutConstants.PageWidth,
                Height = LayoutConstants.PageHeight,
                Background = Brushes.White
            };

            var outer = new Border
            {
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C2F83")),
                BorderThickness = new Thickness(2),
                Margin = new Thickness(20),
                Padding = new Thickness(LayoutConstants.Margin)
            };

            var layout = new Grid();

            layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Header
            layout.RowDefinitions.Add(new RowDefinition()); // Content
            layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Footer

            // 🔷 HEADER
            layout.Children.Add(CreateHeader());

            // 🔷 CONTENT PLACEHOLDER
            var content = new StackPanel { Name = "ContentPanel" };
            Grid.SetRow(content, 1);
            layout.Children.Add(content);

            // 🔷 FOOTER
            var footer = new TextBlock
            {
                Text = $"Page {pageNumber} of {totalPages}",
                HorizontalAlignment = HorizontalAlignment.Right,
                FontSize = 10
            };
            Grid.SetRow(footer, 2);
            layout.Children.Add(footer);

            outer.Child = layout;
            root.Children.Add(outer);

            // 🔥 WATERMARK
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

        private UIElement CreateHeader()
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var logo = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resources/SBI.png")),
                Height = 60
            };

            var text = new StackPanel();

            text.Children.Add(new TextBlock
            {
                Text = "FILE HASH REPORT",
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2C2F83"))
            });

            text.Children.Add(new TextBlock
            {
                Text = "State Bank of India - Confidential",
                FontSize = 11,
                Foreground = Brushes.Gray
            });

            Grid.SetColumn(logo, 0);
            Grid.SetColumn(text, 1);

            grid.Children.Add(logo);
            grid.Children.Add(text);

            return grid;
        }
    }
}
