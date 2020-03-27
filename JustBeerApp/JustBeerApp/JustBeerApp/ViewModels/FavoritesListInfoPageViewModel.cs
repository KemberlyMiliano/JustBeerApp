using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JustBeerApp.ViewModels
{
    public class FavoritesListInfoPageViewModel : BaseViewModel
    {
        public ObservableCollection<Data> Data { get; set; }
        public bool IsRunning { get; set; }
        public string BeerId { get; set; }
        public ICommand ShowData { get; set; }
        public FavoritesListInfoPageViewModel(INavigationService navigationService, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigationService, apiService, pageDialogService)
        {
            ShowData = new Command(async () =>   await GetBeerData2());
        }
        public async Task GetBeerData2()
        {
            IsRunning = true;
            var result = await ApiManager.GetBeerAsync();
            if (result != null)
                Data = new ObservableCollection<Data>(result);
            IsRunning = false;
        }
    }
}
