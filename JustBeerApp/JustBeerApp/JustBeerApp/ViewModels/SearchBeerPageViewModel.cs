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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JustBeerApp.ViewModels
{
    public class SearchBeerPageViewModel : BaseViewModel
    {
        protected IApiManager ApiManager = new ApiManager();
        public bool IsRunning { get; set; }
        public string BeerId { get; set; }
        public ObservableCollection<Data> Data { get; set; } = new ObservableCollection<Data>();
        public DelegateCommand Search { get; set; }

        public SearchBeerPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {

            Search = new DelegateCommand(async () =>
            {
                await GetBeerIngredientData();
                
            });
        }

        public async Task GetBeerIngredientData()
        {
            //bool internetAccess = await CheckInternetConnection();
            //if (internetAccess)
            //{
            //    try
            //    {
            //        Data = await ApiService.GetBeers(BeerId);
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine($"API EXCEPTION {ex}");
            //    }

            //}
            IsRunning = true;

            var result = await ApiManager.GetBeerAsync(BeerId);
            if (result != null)
                Data = new ObservableCollection<Data>(result);

            IsRunning = false;

        }
        
        }
}
