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
    public class SearchBeerPageViewModel : BaseViewModel
    {
        public string BeerId { get; set; }
        public Data Data { get; set; } = new Data();
        public DelegateCommand Search { get; set; }

        public SearchBeerPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService)
        {

            Search = new DelegateCommand(async () =>
            {
                await GetBeerIngredientData();
            });


            async Task GetBeerIngredientData()
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    try
                    {
                        Data = await apiService.GetBeers(BeerId);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"API EXCEPTION {ex}");
                        await pageDialogService.DisplayAlertAsync("Error", $"{ex}", "ok");
                    }

                }
                else
                {
                    await pageDialogService.DisplayAlertAsync("Alet", "No tienes Conexion a internet", "Ok");
                }
            }
        }

        
       
       
    }
}
