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
using Xamarin.Forms;

namespace JustBeerApp.ViewModels
{
    public class FavoritesListInfoPageViewModel : BaseViewModel 
    {
        protected IApiTestManager ApiTestManager = new ApiTestManager();
        public DelegateCommand GetBeersCommand { get; set; }
        public DelegateCommand<Beer> RemoveBeersCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Beer> GoToBeerInfo { get; set; }
        public ObservableCollection<Beer> Data { get; set; } = new ObservableCollection<Beer>();
        public bool IsRefreshing { get; set; }
        public FavoritesListInfoPageViewModel(INavigationService navigationService, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigationService, apiService, pageDialogService)
        {
            GetBeersCommand = new DelegateCommand(async () =>
            {
                await GetBeersData();
            });
            GetBeersCommand.Execute();

            RemoveBeersCommand = new DelegateCommand<Beer>(async (param) =>
            {
                IsRefreshing = true;
                Data.Clear();
                await RemoveBeers(param);
                await GetBeersData();
                IsRefreshing = false;
            });

            RefreshCommand = new DelegateCommand(async () => 
            {
                IsRefreshing = true;
                Data.Clear();
                await GetBeersData();
                IsRefreshing = false;
            });

            GoToBeerInfo = new DelegateCommand<Beer>(async (param) =>
            {
                var nav = new NavigationParameters();
                nav.Add("BeerInfo", param);

                await NavigationService.NavigateAsync(NavigationConstants.FavoriteBeerInfoPage, nav);
            });

        }
        public async Task GetBeersData()
        {
            try
            {
                var beer = await ApiTestManager.ShowDataAsync();
                if (beer != null)
                {
                    if (Data == null)
                        Data = new ObservableCollection<Beer>();
                    Data = beer;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API EXCEPTION {ex}");
                Data.Clear();
            }
        }

        public async Task RemoveBeers(Beer param)
        {
             ApiTestManager.RemoveData(param);
            
        }
    }
}
