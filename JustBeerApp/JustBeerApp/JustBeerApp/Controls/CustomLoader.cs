using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JustBeerApp.Controls
{
    public class CustomLoader : Image
    {
        private CancellationTokenSource cancellationToken;

        public static BindableProperty IsRunningProperty = BindableProperty.Create(
            propertyName: nameof(IsRunning),
            returnType: typeof(bool),
            declaringType: typeof(CustomLoader),
            defaultValue: false);

        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        public static BindableProperty RotationLenghtProperty = BindableProperty.Create(
            propertyName: nameof(RotationLenght),
            returnType: typeof(int),
            declaringType: typeof(CustomLoader),
            defaultValue: 2500);

        public int RotationLenght
        {
            get { return (int)GetValue(RotationLenghtProperty); }
            set { SetValue(RotationLenghtProperty, value); }
        }

        public static BindableProperty EasingProperty = BindableProperty.Create(
            propertyName: nameof(Easing),
            returnType: typeof(Easing),
            declaringType: typeof(CustomLoader),
            defaultValue: Easing.CubicInOut);

        public Easing Easing
        {
            get { return (Easing)GetValue(EasingProperty); }
            set { SetValue(EasingProperty, value); }
        }
        public CustomLoader()
        {
            Opacity = 0;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsRunningProperty.PropertyName)
            {
                if (IsRunning)
                {
                    this.FadeTo(1);
                    cancellationToken = new CancellationTokenSource();
                    _ = RotateElement(this, cancellationToken.Token);
                }
                else
                {
                    cancellationToken?.Cancel();
                    this.FadeTo(0);
                }
            }
        }
        private async Task RotateElement(VisualElement element, CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                await element.RotateTo(360, (uint)RotationLenght, this.Easing);
                await element.RotateTo(0, 0);
            }
        }

    }
}
