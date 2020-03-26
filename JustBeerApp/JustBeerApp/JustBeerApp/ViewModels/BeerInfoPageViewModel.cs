using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace JustBeerApp.ViewModels
{
    public class BeerInfoPageViewModel : BaseViewModel
    {
        public string ID { get; set; }
        public Data BeerInfo { get; set; }
        public DelegateCommand AddToFavoritesCommand { get; set; }
        public BeerInfoPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {

            AddToFavoritesCommand = new DelegateCommand(async () =>
            {
                await GetBeerInfo();
                AddToFavorites(BeerInfo);
            });
        }

        public async Task GetBeerInfo()
        {
            bool internetAccess = await CheckInternetConnection();

            if (internetAccess)
            {
                try
                {
                    BeerInfo = await ApiService.GetBeers(ID);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"API EXCEPTION {ex}");
                }
            }
        }

        public void AddToFavorites(Data beer)
        {

        }
    }
}
