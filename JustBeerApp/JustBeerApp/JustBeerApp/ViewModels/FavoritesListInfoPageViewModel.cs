using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace JustBeerApp.ViewModels
{
    public class FavoritesListInfoPageViewModel : BaseViewModel
    {
        protected IApiTestManager ApiTestManager = new ApiTestManager();
        public DelegateCommand GetBeersMethod { get; set; }
        public ObservableCollection<Beers> Data { get; set; }
        public bool IsRunning { get; set; }
        public string BeerId { get; set; }
        public FavoritesListInfoPageViewModel(INavigationService navigationService, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigationService, apiService, pageDialogService)
        {
            GetBeersMethod = new DelegateCommand(async () =>
            {
                await GetBeersData();
            });
            GetBeersMethod.Execute();
        }

        public async Task GetBeersData()
        {
            var result = await ApiTestManager.GetBeersAsync();
            if (result != null)
                Data = new ObservableCollection<Beers>(result);

        }
    }
}
