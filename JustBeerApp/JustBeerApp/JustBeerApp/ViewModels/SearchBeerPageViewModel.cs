using JustBeerApp.Models;
using JustBeerApp.Services;
using JustBeerApp.Views;
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
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace JustBeerApp.ViewModels
{
    public class SearchBeerPageViewModel : BaseViewModel
    {
        protected IApiManager ApiManager = new ApiManager();
        public bool IsRunning { get; set; }
        public string BeerId { get; set; }
        public ObservableCollection<Datum> HomeBeers { get; set; } = new ObservableCollection<Datum>();
        public ObservableCollection<Datum> AuxiliarList { get; set; }
        public DelegateCommand GoToSearchBeerPage { get; set; }
        public DelegateCommand GoToBeerInfoPage { get; set; }
        public Beers BeerList { get; set; } = new Beers();
        public DelegateCommand GetBeerList { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public SearchBeerPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            GetBeerList = new DelegateCommand(async () =>
            {
                await GetBeers();

            });

            GetBeerList.Execute();

            AuxiliarList = HomeBeers;

            GoToSearchBeerPage = new DelegateCommand(async () =>
            {
                await navigation.NavigateAsync(NavigationConstants.SearchBeerDetailedPage);

            });

            GoToBeerInfoPage = new DelegateCommand(async () =>
            {
                await navigation.NavigateAsync(NavigationConstants.BeerInfoPage);

            });

            SearchCommand = new DelegateCommand(() =>
            {
                Search();
            });

            RefreshCommand = new DelegateCommand(() =>
           {
               //await GetBeers();
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
        public void Search()
        {
            IsRefreshing = true;


            if (string.IsNullOrEmpty(Filter))
            {
                AuxiliarList.OrderBy(b => b.NameDisplay).ToList();
            }
            else
            {
                AuxiliarList.Where(b => b.NameDisplay.ToLower().Contains(Filter.ToLower())).OrderBy(b => b.NameDisplay);
            }

            IsRefreshing = false;
        }
        public string Filter { get; set; }

        public bool IsRefreshing { get; set; }

    }

}