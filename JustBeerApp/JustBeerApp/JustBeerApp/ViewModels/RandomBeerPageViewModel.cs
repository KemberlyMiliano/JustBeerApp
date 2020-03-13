using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JustBeerApp.ViewModels
{
    public class RandomBeerPageViewModel : BaseViewModel
    {
        public Data RandomBeer { get; set; } = new Data();
        public DelegateCommand GetRandomBeer {get; set;}

        public RandomBeerPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            GetRandomBeer = new DelegateCommand(async () =>
            {
                await GetBeerData();
                
            });


            async Task GetBeerData()
            {
                
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    try
                    {
                        RandomBeer = await apiService.GetRandomBeers();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"API EXCEPTION {ex}");
                    }

                }
                else
                {
                    await pageDialogService.DisplayAlertAsync(Config.NullMessage, Config.NoInternetAlertMessage, Config.CancelMessage);
                }
            }
        }
        
       
    }
}
