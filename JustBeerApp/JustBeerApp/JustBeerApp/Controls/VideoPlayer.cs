using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace JustBeerApp.Controls
{
    public class VideoPlayer : View
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(
                nameof(Source),
                typeof(string),
                typeof(VideoPlayer), null);
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

    }
}
