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

namespace JustBeerApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public DelegateCommand GetBeerList { get; set; }
        public DelegateCommand<Datum> GoToInfoBeerPage { get; set; }
        public HomePageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            GetBeerList = new DelegateCommand(async () =>
            {
                await GetBeers();

            });

            GetBeerList.Execute();

            GoToInfoBeerPage = new DelegateCommand<Datum>(async (param) =>
            {
                var nav = new NavigationParameters();
                nav.Add("Beer", param);

                await NavigationService.NavigateAsync(NavigationConstants.BeerInfoPage, nav);

            });
        }

    }
}
