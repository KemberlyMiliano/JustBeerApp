using System;
using AVFoundation;
using AVKit;
using JustBeerApp.Controls;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.IO;
using CoreMedia;
using System.ComponentModel;
using JustBeerApp.iOS;
using JustBeerApp.iOS.Renderers;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(VideoPlayerRenderer))]
namespace JustBeerApp.iOS.Renderers
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        private AVQueuePlayer _player;
        private AVPlayerViewController _playerViewController;
        private AVPlayerLooper _playerLooper;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;

            InitPlayer();
            PlayVideo();
        }

        private void InitPlayer()
        {
            if (Control != null) return;

            _player = new AVQueuePlayer
            {
                Muted = true,
                ActionAtItemEnd = AVPlayerActionAtItemEnd.None
            };

            _playerViewController = new AVPlayerViewController
            {
                Player = _player,
                VideoGravity = AVLayerVideoGravity.ResizeAspectFill,
                ShowsPlaybackControls = false
            };

            SetNativeControl(_playerViewController.View);
        }

        private void PlayVideo()
        {
            var videoUrl = GetVideoUrl(Element.Source);
            if (videoUrl == null)
            {
                Console.WriteLine($"Video {Element.Source} not found");
                return;
            }

            var itemToPlay = new AVPlayerItem(AVAsset.FromUrl(videoUrl));

            _playerLooper = AVPlayerLooper.FromPlayer(_player, itemToPlay, CMTimeRange.InvalidRange);

            if (itemToPlay != null)
                _player.Play();
        }

        private NSUrl GetVideoUrl(string path) => string.IsNullOrWhiteSpace(path)
                ? null
                : NSBundle.MainBundle.GetUrlForResource(Path.GetFileNameWithoutExtension(path),
                                                        Path.GetExtension(path).Substring(1),
                                                        Path.GetDirectoryName(path));

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VideoPlayer.SourceProperty.PropertyName)
                PlayVideo();
        }

        public override UIViewController ViewController => _playerViewController;
    }
}