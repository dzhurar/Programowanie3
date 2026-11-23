using Biblioteka.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Biblioteka.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ZapiszCommand))]
        private Ksiazka? wybranaKsiazka;

        private bool CzyWybranoKsiazke()
        {
            return WybranaKsiazka != null;
        }

        [RelayCommand(CanExecute = nameof(CzyWybranoKsiazke))]
        private void Zapisz()
        {
        }

        [RelayCommand] private void Odswiez()
        {

        }
    }
}
