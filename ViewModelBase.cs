using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HashGenerator.VIewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public string Header { get; set; } = string.Empty;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
