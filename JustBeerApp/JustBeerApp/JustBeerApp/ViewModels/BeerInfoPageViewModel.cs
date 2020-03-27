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
        public string FavoriteIcon { get; set; }
        public string ID { get; set; }
        public Data BeerInfo { get; set; }
        public DelegateCommand AddToFavoritesCommand { get; set; }
        public ObservableCollection<Beer> FavoriteList { get; set; }
        public BeerInfoPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            GetBeerInfo();
            AddToFavoritesCommand = new DelegateCommand(async () =>
            {
                await AddToFavorites(BeerInfo.Beer);
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

        public async Task AddToFavorites(Beer beerInformation)
        {
            FavoriteList.Add(beerInformation); 
             FavoriteIcon = FavoriteList.Contains(beerInformation) ? "orangeHeart.png" : "favoritesIcon.png";
        }
    }
}
