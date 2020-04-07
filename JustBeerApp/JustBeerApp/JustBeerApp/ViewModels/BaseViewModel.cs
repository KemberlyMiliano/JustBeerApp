using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JustBeerApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationService NavigationService { get; set; }
        protected IApiBeerService ApiService { get; set; }
        protected IPageDialogService PageDialogService { get; set; }
        protected IApiManager ApiManager { get; set; }
        public Data RandomBeer { get; set; } = new Data();
        public Beers BeerList { get; set; } = new Beers();
        public ObservableCollection<Datum> HomeBeers { get; set; } = new ObservableCollection<Datum>();

        public BaseViewModel(INavigationService navigationService, IApiBeerService apiService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            ApiService = apiService;
            PageDialogService = pageDialogService;
        }
        public async Task<bool> CheckInternetConnection()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                await PageDialogService.DisplayAlertAsync(Config.NullMessage, Config.NoInternetAlertMessage, Config.CancelMessage);
            }

            return false;
        }
        public async Task GetBeerData()
        {
            bool internetAccess = await CheckInternetConnection();

            if (internetAccess)
            {
                try
                {
                    RandomBeer = await ApiService.GetRandomBeers();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"API EXCEPTION {ex}");
                }
            }
        }
        public async Task GetBeers()
        {
            bool internetAccess = await CheckInternetConnection();

            if (internetAccess)
            {
                try
                {
                    BeerList = await ApiService.GetListOfBeers();
                    HomeBeers = BeerList.Data;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"API EXCEPTION {ex}");
                }
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
