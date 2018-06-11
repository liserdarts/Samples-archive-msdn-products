// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using O365_Win8App.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;



namespace O365_Win8App
{
    /// <summary>
    /// A page that displays the video detail including the preview  
    /// </summary>
    public sealed partial class VideoDetailPage : O365_Win8App.Common.LayoutAwarePage
    {
        public VideoDetailPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter,
            Dictionary<String, Object> pageState)
        {
            // Get the video data from sharepoint site
            var videoDetail = VideoDataSource.GetVideos((String)navigationParameter);
            this.DefaultViewModel["Group"] = videoDetail;
            GetVideoFromLocal(videoDetail);
        }

        /// <summary>
        /// Get the video file from local storage if exist.
        /// </summary>
        /// <param name="group">pass the video detail object</param>
        private async void GetVideoFromLocal(VideoData videodetail)
        {
            try
            {
                var Videofile = await ApplicationData.Current.LocalFolder.GetFileAsync(videodetail.VideoName);
                if (Videofile != null)
                {
                    videodetail.VideoUrl = Videofile.Path;
                    this.DefaultViewModel["Groups"] = videodetail;
                }
            }
            catch (FileNotFoundException)
            {
                // If file does not exist in the local folder then retrieve from server.
                GetVideoFromServer(videodetail);
            }
        }

        /// <summary>
        /// Get the video stream from sharepoint site and save them in local folder
        /// </summary>
        /// <param name="group">pass video detail</param>
        private async void GetVideoFromServer(VideoData videodetail)
        {
            try
            {
                // progress ring will be visible.
                ProgressBarVisible(true);
                Uri videoUri = new Uri(videodetail.VideoUrl);
                byte[] response = await SharePointOnlineLoginHelper.AuthObj.CreateHttpRequest(
                    System.Net.Http.HttpMethod.Post, videoUri);

                // Creating video file from response stream and save in local storage
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(videodetail.VideoName,
                    CreationCollisionOption.ReplaceExisting);
                using (IRandomAccessStream session =
                await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    Stream stream = session.AsStreamForWrite();
                    await stream.WriteAsync(response, 0, response.Length);
                    InMemoryRandomAccessStream ras = new InMemoryRandomAccessStream();

                    await stream.CopyToAsync(ras.AsStreamForWrite());

                    // Get the recently saved file from local storage
                    var Videofile = await ApplicationData.Current.LocalFolder.GetFileAsync(videodetail.VideoName);
                    videodetail.VideoUrl = Videofile.Path;
                    this.DefaultViewModel["Groups"] = videodetail;

                    // Progress ring will be invisible
                    ProgressBarVisible(false);
                }
            }
            catch(Exception ex)
            {
                videodetail.Description = "Unable to stream this video. Exception: " + ex.Message;
                this.DefaultViewModel["Groups"] = videodetail;
            }
        }

        /// <summary>
        /// Function to visible and invisible progress ring 
        /// </summary>
        /// <param name="visible"></param>
        private void ProgressBarVisible(bool visible)
        {
            ProgressRingLoad.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
            ProgressRingLoad.IsActive = visible;
        }

    }
}
