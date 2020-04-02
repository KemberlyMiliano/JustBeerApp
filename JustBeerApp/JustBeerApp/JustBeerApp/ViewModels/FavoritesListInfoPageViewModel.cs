using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JustBeerApp.ViewModels
{
    public class FavoritesListInfoPageViewModel : BaseViewModel 
    {
        protected IApiTestManager ApiTestManager = new ApiTestManager();
        public DelegateCommand GetBeersMethod { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public ObservableCollection<Beer> Data { get; set; } = new ObservableCollection<Beer>();
        public bool IsRefreshing { get; set; }
        public string BeerId { get; set; }
        public FavoritesListInfoPageViewModel(INavigationService navigationService, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigationService, apiService, pageDialogService)
        {
            GetBeersMethod = new DelegateCommand(async () =>
            {
                await GetBeersData();
            });
            GetBeersMethod.Execute();

            RefreshCommand = new DelegateCommand(async () => 
            {
                IsRefreshing = true;
                await GetBeersData();
                IsRefreshing = false;
            });

        }
        public void RefreshMethod() 
        {
            
        }

        public async Task GetBeersData()
        {
            var result = await ApiTestManager.ShowDataAsync();
            if (result != null)
                Data.Add(result);
        }
    }
}
