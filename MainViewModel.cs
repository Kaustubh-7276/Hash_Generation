using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HashGenerator.VIewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ViewModelBase> TabItems { get; set; }
        private ViewModelBase _selectedTab;

        public ViewModelBase SelectedTab
        {
            get => _selectedTab;
            set { _selectedTab = value; OnPropertyChanged(); }
        }

        public RelayCommand SwitchTabCommand { get; }

        public MainViewModel()
        {
            // 1. Setup Shared Data (Categories)
            var sharedCategories = new ObservableCollection<UploadCategory>();

            // 2. Initialize ViewModels
            var singleHashVM = new SingleHashViewModel();
            singleHashVM.Categories = sharedCategories;

            var bulkHashVM = new BulkHashViewModel();
            // Note: If BulkHashViewModel needs categories, assign them here:
            // bulkHashVM.BulkCategories = sharedCategories; 
            bulkHashVM.BulkCategories = sharedCategories;
            var manageVM = new ManageCategoriesViewModel(sharedCategories);

            // 3. Add all three tabs to the collection
            TabItems = new ObservableCollection<ViewModelBase>
            {
                singleHashVM,
                bulkHashVM,   // Added Bulk Tab at Index 1
                manageVM      // Manage Tab moved to Index 2
            };

            // 4. Set Default Tab (Single Hash)
            SelectedTab = TabItems[0];

            // 5. Command for Password-Protected Tab Switching
            SwitchTabCommand = new RelayCommand(p => ValidateAndSwitch(p));
        }

        private void ValidateAndSwitch(object parameter)
        {
            var passwordBox = parameter as PasswordBox;

            // Password check for the Admin/Manage Tab
            if (passwordBox?.Password == "SBI@123")
            {
                // Switch to Manage Categories (now at Index 2)
                SelectedTab = TabItems[2];
                passwordBox.Clear();

                if (TabItems[2] is ManageCategoriesViewModel manageVM)
                {
                    manageVM.IsAuthenticated = true;
                }
            }
            else
            {
                MessageBox.Show("Access Denied: Invalid Admin Password", "Security", MessageBoxButton.OK, MessageBoxImage.Warning);
                passwordBox?.Clear();
            }
        }
    }
}
