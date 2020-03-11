using JustBeerApp.ViewModels;
using JustBeerApp.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JustBeerApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(new Uri("/NavigationPage/HomePage", UriKind.Absolute));
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            //containerRegistry.RegisterForNavigation<BeerInfoPage, BeerInfoPageViewModel>();
            //containerRegistry.RegisterForNavigation<FavoritesPage, FavoritesPageViewModel>();
            //containerRegistry.RegisterForNavigation<RandomBeerPage, RandomBeerPageViewModel>();
            //containerRegistry.RegisterForNavigation<SearchBeerPage, SearchBeerPageViewModel>();
        }
    }
}
