using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using JustBeerApp.Controls;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views.InputMethods;
using Android.Support.V7.Widget;
using Android.Text;

[assembly: ExportRenderer(typeof(SearchPage), typeof(JustBeerApp.Droid.Renderers.SearchPageRenderer))]
namespace JustBeerApp.Droid.Renderers
{
    public class SearchPageRenderer : PageRenderer
    {
        public SearchPageRenderer(Context context) : base(context)
        {

        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (Element is ISearchPage && Element is Page page && page.Parent is NavigationPage navigationPage)
            {
                //Workaround to re-add the SearchView when navigating back to an ISearchPage, because Xamarin.Forms automatically removes it
                navigationPage.Popped += HandleNavigationPagePopped;
                navigationPage.PoppedToRoot += HandleNavigationPagePopped;
            }
        }
        //Adding the SearchBar in OnSizeChanged ensures the SearchBar is re-added after the device is rotated, because Xamarin.Forms automatically removes it
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            if (Element is ISearchPage && Element is Page page && page.Parent is NavigationPage navigationPage && navigationPage.CurrentPage is ISearchPage)
            {
                AddSearchToToolbar(page.Title);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (GetToolbar() is Toolbar toolBar)
                toolBar.Menu?.RemoveItem(Resource.Menu.MainMenu);

            base.Dispose(disposing);
        }

        //Workaround to re-add the SearchView when navigating back to an ISearchPage, because Xamarin.Forms automatically removes it
        void HandleNavigationPagePopped(object sender, NavigationEventArgs e)
        {
            if (sender is NavigationPage navigationPage
                && navigationPage.CurrentPage is ISearchPage)
            {
                AddSearchToToolbar(navigationPage.CurrentPage.Title);
            }
        }
        void AddSearchToToolbar(string pageTitle)
        {
            if (GetToolbar() is Toolbar toolBar
                && toolBar.Menu?.FindItem(Resource.Id.ActionSearch)?.ActionView?.JavaCast<SearchView>().GetType() != typeof(SearchView))
            {
                toolBar.Title = pageTitle;
                toolBar.InflateMenu(Resource.Menu.MainMenu);

                if (toolBar.Menu?.FindItem(Resource.Id.ActionSearch)?.ActionView?.JavaCast<SearchView>() is SearchView searchView)
                {
                    searchView.QueryTextChange += HandleQueryTextChange;
                    searchView.ImeOptions = (int)ImeAction.Search;
                    searchView.InputType = (int)InputTypes.TextVariationFilter;
                    searchView.MaxWidth = int.MaxValue; //Set to full width - http://stackoverflow.com/questions/31456102/searchview-doesnt-expand-full-width
                }
            }
        }

        void HandleQueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if (Element is ISearchPage searchPage)
                searchPage.OnSearchBarTextChanged(e.NewText);
        }

        Toolbar GetToolbar() => CrossCurrentActivity.Current.Activity.FindViewById<Toolbar>(Resource.Id.toolbar);
    }
}