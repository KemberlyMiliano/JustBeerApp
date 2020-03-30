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
        public DelegateCommand GoToSearchBeerPage { get; set; }
        public DelegateCommand<Datum> GoToInfoBeerPage { get; set; }
        public DelegateCommand GetBeerList { get; set; }

        public SearchBeerPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            GetBeerList = new DelegateCommand(async () =>
            {
                await GetBeers();

            });

            GetBeerList.Execute();

            GoToSearchBeerPage = new DelegateCommand(async () =>
            {
                await navigation.NavigateAsync(NavigationConstants.SearchBeerDetailedPage);

            });

            GoToInfoBeerPage = new DelegateCommand<Datum>(async (param) =>
            {
                var nav = new NavigationParameters();
                nav.Add("Beer", param);

                await navigation.NavigateAsync(NavigationConstants.BeerInfoPage);
            });
        }
    }
}