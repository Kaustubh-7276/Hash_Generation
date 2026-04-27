using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
namespace HashGenerator.VIewModel
{
    public class ManageCategoriesViewModel : ViewModelBase
    {
        private bool _isAuthenticated;
        public ObservableCollection<UploadCategory> Categories { get; set; }
        public string NewCode { get; set; }
        public string NewDescription { get; set; }
        public RelayCommand AddCommand { get; }
        public RelayCommand RemoveCommand { get; }

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set { _isAuthenticated = value; OnPropertyChanged(); }
        }

        public RelayCommand UnlockCommand { get; }

        public ManageCategoriesViewModel(ObservableCollection<UploadCategory> sharedCategories)
        {
            Header = "Manage Categories";
            Categories = sharedCategories;
            // Initialize Lists
            LoadCategoriesFromFile();
            UnlockCommand = new RelayCommand(p =>
            {
                var passwordBox = p as System.Windows.Controls.PasswordBox;
                if (passwordBox?.Password == "SBI@123")
                {
                    IsAuthenticated = true;
                    passwordBox.Clear();
                }
                else
                {
                    System.Windows.MessageBox.Show("Access Denied: Invalid Admin Password", "Security Alert");
                }
            });
            AddCommand = new RelayCommand(_ => AddCategory());
            RemoveCommand = new RelayCommand(p => RemoveCategory(p as UploadCategory));
        }

        private void AddCategory()
        {
            if (string.IsNullOrWhiteSpace(NewCode)) return;
            Categories.Add(new UploadCategory { Code = NewCode, Description = NewDescription });
            SaveToJson();
            NewCode = ""; NewDescription = ""; // Reset inputs
        }

        private void SaveToJson()
        {
            try
            {
                string fileName = "UploadTypes.json";
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo directory = new DirectoryInfo(currentDir);

                // Standard C# 7.3 null-check loop to find the root directory
                while (directory != null && !File.Exists(Path.Combine(directory.FullName, fileName)))
                {
                    directory = directory.Parent;
                }

                // Determine final path
                string path = (directory != null)
                    ? Path.Combine(directory.FullName, fileName)
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                // Serialize using Newtonsoft.Json with indentation
                // 'Formatting.Indented' is the direct equivalent of 'WriteIndented = true'
                string json = JsonConvert.SerializeObject(Categories, Formatting.Indented);

                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                // Log error (Safe for .NET Framework debugging)
                System.Diagnostics.Debug.WriteLine("JSON Save Error: " + ex.Message);
            }
        }
        private void RemoveCategory(UploadCategory category)
        {
            if (category != null)
            {
                Categories.Remove(category);
                SaveToJson();
            }
        }
        // 6. JSON Loading Logic
        private void LoadCategoriesFromFile()
        {
            try
            {
                string fileName = "UploadTypes.json";
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo directory = new DirectoryInfo(currentDir);

                while (directory != null && !File.Exists(Path.Combine(directory.FullName, fileName)))
                {
                    directory = directory.Parent;
                }

                string filePath = (directory != null)
                    ? Path.Combine(directory.FullName, fileName)
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);

                    var list = JsonConvert.DeserializeObject<List<UploadCategory>>(jsonContent);

                    if (list != null)
                    {
                        Categories.Clear();
                        foreach (var item in list)
                        {
                            Categories.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }
    }
}
