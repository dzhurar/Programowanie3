using CommunityToolkit.Mvvm.ComponentModel;

namespace Biblioteka.Models
{
    public partial class Ksiazka : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string tytul = string.Empty;

        [ObservableProperty]
        private string autor = string.Empty;

        [ObservableProperty]
        private int? rokWydania;

        [ObservableProperty]
        private int kategoriaId;

        [ObservableProperty]
        private bool przeczytana;

        [ObservableProperty]
        private string kategoriaNazwa = string.Empty;
    }
}