using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace JustBeerApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public Datum SelectedBeer { get; set; }
        public ObservableCollection<Datum> HomeBeers { get; set; } = new ObservableCollection<Datum>();
        public Beers BeerList { get; set; } = new Beers();
        public DelegateCommand GetBeerList { get; set; }
        public DelegateCommand GoToInfoBeerPage { get; set; }
        public HomePageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            GetBeerList = new DelegateCommand(async () =>
            {
                await GetBeers();

            });

            GetBeerList.Execute();

            GoToInfoBeerPage = new DelegateCommand(async () =>
            {
                var nav = new NavigationParameters();
                nav.Add("Beer", SelectedBeer);

                await NavigationService.NavigateAsync(NavigationConstants.BeerInfoPage, nav);

            });
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
    }
}
