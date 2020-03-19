using JustBeerApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JustBeerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBeerPage : ContentPage
    {
        public SearchBeerPage()
        {
            InitializeComponent();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //if (string.IsNullOrEmpty(e.NewTextValue))
            //{
            //    list.ItemsSource = tempdata;
            //}

            //else
            //{
            //    list.ItemsSource = tempdata.Where(x => x.Name.StartsWith(e.NewTextValue));
            //}
        }
    }
}