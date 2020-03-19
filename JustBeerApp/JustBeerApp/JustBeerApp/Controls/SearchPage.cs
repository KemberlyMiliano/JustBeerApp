using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JustBeerApp.Controls
{
    public class SearchPage : ContentPage, ISearchPage
    {
        public SearchPage()
        {
            SearchBarTextChanged += HandleSearchBarTextChanged;
        }

        public event EventHandler<string> SearchBarTextChanged;

        void HandleSearchBarTextChanged(object sender, string searchBarText)
        {
            //Logic to handle updated search bar text
        }

        void ISearchPage.OnSearchBarTextChanged(string text)
        {
            SearchBarTextChanged?.Invoke(this, text);
        }
    }
}
