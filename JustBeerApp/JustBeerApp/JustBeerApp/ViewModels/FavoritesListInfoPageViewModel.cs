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
        protected IApiManager ApiManager = new ApiManager();
        public DelegateCommand GetBeersCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<Beer> ActionSheetCommand { get; set; }
        public ObservableCollection<Beer> Data { get; set; } = new ObservableCollection<Beer>();
        public bool IsRefreshing { get; set; }
        public FavoritesListInfoPageViewModel(INavigationService navigationService, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigationService, apiService, pageDialogService)
        {
            GetBeersCommand = new DelegateCommand(async () =>
            {
                await GetBeersData();
            });
            GetBeersCommand.Execute();

            RefreshCommand = new DelegateCommand(async () =>
            {
                IsRefreshing = true;
                Data.Clear();
                await GetBeersData();
                IsRefreshing = false;
            });


            ActionSheetCommand = new DelegateCommand<Beer>((param) =>
            {
                ActionSheertMethod(param);
            });

        }
        public async Task GetBeersData()
        {
            try
            {
                var beer = await ApiManager.ShowDataAsync();
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

        public async void ActionSheertMethod(Beer param)
        {
            string Answer = await Application.Current.MainPage.DisplayActionSheet("Just Beer", "Cancel", null, "Show Beer Info", "Delete from Favorites");
            switch (Answer)
            {
                case "Show Beer Info":
                    await GoToBeerInfo(param);
                    break;
                case "Delete from Favorites":
                    await RemoveBeers(param);
                    break;
            }
        }

        public async Task RemoveBeers(Beer param)
        {
            IsRefreshing = true;
            Data.Clear();
            ApiManager.RemoveData(param);
            await GetBeersData();
            IsRefreshing = false;
        }

        public async Task GoToBeerInfo(Beer param)
        {
            var nav = new NavigationParameters();
            nav.Add("BeerInfo", param);
            await NavigationService.NavigateAsync(NavigationConstants.FavoriteBeerInfoPage, nav);
        }
    }
}
