using CommunityToolkit.Mvvm.ComponentModel;

namespace Biblioteka.Models
{
    public partial class Kategoria : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string nazwa = string.Empty;
    }
}