using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JustBeerApp.Controls;
using JustBeerApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AWR = Android.Widget.RelativeLayout;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(VideoPlayerRenderer))]
namespace JustBeerApp.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, AWR>
    {
        private VideoView _videoView;

        public VideoPlayerRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;

            InitVideoPlayer();
            PlayVideo();
        }

        private void InitVideoPlayer()
        {
            if (Control != null) return;

            _videoView = new VideoView(Context);

            var relativeLayout = new AWR(Context);
            relativeLayout.AddView(_videoView);

            var layoutParams = new AWR.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            layoutParams.AddRule(LayoutRules.AlignParentTop);
            layoutParams.AddRule(LayoutRules.AlignParentBottom);
            layoutParams.AddRule(LayoutRules.AlignParentLeft);
            layoutParams.AddRule(LayoutRules.AlignParentRight);

            _videoView.LayoutParameters = layoutParams;
            _videoView.Prepared += VideoView_Prepared;

            SetNativeControl(relativeLayout);
        }

        void VideoView_Prepared(object sender, EventArgs e)
        {
            if (sender is MediaPlayer mp)
            {
                mp.SetVolume(0, 0);
                mp.Looping = true;
                mp.SetVideoScalingMode(VideoScalingMode.ScaleToFit);
            }
        }

        private void PlayVideo()
        {
            var resourceId = Resources.GetIdentifier(Path.GetFileNameWithoutExtension(Element.Source).ToLowerInvariant(), "raw", Context.PackageName);
            if (resourceId == 0)
            {
                Console.WriteLine($"Video {Element.Source} not found");
                return;
            }

            _videoView.SetVideoURI(GetVideoUri());
            _videoView.Start();
        }

        private Android.Net.Uri GetVideoUri()
            => Android.Net.Uri.Parse("android.resource://" + Context.PackageName + "/raw/" + Path.GetFileNameWithoutExtension(Element.Source).ToLowerInvariant());

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VideoPlayer.SourceProperty.PropertyName)
                PlayVideo();
        }

        protected override void Dispose(bool disposing)
        {
            if (Control == null || _videoView == null) return;

            _videoView.Prepared -= VideoView_Prepared;

            base.Dispose(disposing);
        }
    }
}