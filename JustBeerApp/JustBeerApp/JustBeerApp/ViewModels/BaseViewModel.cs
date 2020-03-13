using JustBeerApp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JustBeerApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationService NavigationService { get; set; }
        protected IApiBeerService ApiService { get; set; }

        protected IPageDialogService PageDialogService { get; set; }

        public BaseViewModel(INavigationService navigationService, IApiBeerService apiService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            ApiService = apiService;

           
        }
        async Task<bool> CheckInternetConnection()
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
