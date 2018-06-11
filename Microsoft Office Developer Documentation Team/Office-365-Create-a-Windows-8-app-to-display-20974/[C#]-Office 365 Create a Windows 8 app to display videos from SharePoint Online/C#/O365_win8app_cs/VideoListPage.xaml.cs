// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using O365_Win8App.Data;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace O365_Win8App
{
    /// <summary>
    /// A page that displays a list of videos.
    /// </summary>
    public sealed partial class VideoListPage : O365_Win8App.Common.LayoutAwarePage
    {
        public VideoListPage()
        {
            this.InitializeComponent();
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
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var videoList = VideoDataSource.GetAllVideos();
            this.DefaultViewModel["Groups"] = videoList;
        }

        /// <summary>
        /// Invoked when a title is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a title for the selected video.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void HeaderClick(object sender, RoutedEventArgs e)
        {
            var video = (sender as FrameworkElement).DataContext;

            // Navigate to video detail page with video title as parameter
            this.Frame.Navigate(typeof(VideoDetailPage), ((VideoData)video).Title);
        }


        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            VideoDataSource.RefreshVideos(); //refresh the UI
        }



    }
}
