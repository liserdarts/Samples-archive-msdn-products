// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;



namespace O365_Win8App.Data
{
    /// <summary>
    /// Base class for <see cref="VideoDataItem"/> and <see cref="VideoData"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class VideoDataModel : O365_Win8App.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public VideoDataModel(String title, String videoName, String description, string videoUrl)
        {
            this._title = title;
            this._videoName = videoName;
            this._description = description;
            this._videoUrl = videoUrl;
        }


        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _videoName = string.Empty;
        public string VideoName
        {
            get { return this._videoName; }
            set { this.SetProperty(ref this._videoName, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private string _videoUrl = string.Empty;
        public string VideoUrl
        {
            get { return this._videoUrl; }
            set { this.SetProperty(ref this._videoUrl, value); }
        }

        public override string ToString()
        {
            return this.Title;
        }

    }

    /// <summary>
    /// Generic Video data model.
    /// </summary>
    public class VideoData : VideoDataModel
    {
        public VideoData(String title, String videoname, String description, string videourl)
            : base(title, videoname, description, videourl)
        {
        }

    }

    /// <summary>
    /// Creates a collection of videos 
    /// VideoDataSource initializes with placeholder data rather than live production
    /// </summary>
    public sealed class VideoDataSource
    {
        private static VideoDataSource _VideoDataSource = new VideoDataSource();
        private static bool isDataInitialise;
        private ObservableCollection<VideoData> _videoList = new ObservableCollection<VideoData>();

        public VideoDataSource() { }

        public ObservableCollection<VideoData> VideoList
        {
            get
            {
              return this._videoList;
            }
        }

        /// <summary>
        /// function to get all videos from sharepoint site
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static IEnumerable<VideoData> GetAllVideos()
        {
           if(!isDataInitialise)
            _VideoDataSource.PopulateVideoDataSource();
            return _VideoDataSource.VideoList;
        }

        /// <summary>
        /// Get the video detail by title
        /// </summary>
        /// <param name="uniqueId">Pass video title</param>
        /// <returns>Returns list of videos</returns>
        public static VideoData GetVideos(string title)
        {
            // Simple linear search is acceptable for small data sets
            var videos = _VideoDataSource.VideoList.Where((video) => video.Title.Equals(title));
            if (videos.Count() == 1) return videos.First();
            return null;
        }

        /// <summary>
        /// This method refreshes the video list
        /// </summary>
        /// <returns>returns list of videos</returns>
        public static IEnumerable<VideoData> RefreshVideos()
        {
            // Clear the datasource collection and re-populate with new data
            _VideoDataSource.VideoList.Clear();
            isDataInitialise = false;
            _VideoDataSource.PopulateVideoDataSource();
            return _VideoDataSource.VideoList;
        }

        /// <summary>
        ///  This method get all videos from sharepoint site.
        /// </summary>
        /// <returns></returns>
        private async Task PopulateVideos()
        {
            JsonObject videobj;
            byte[] responseStream = await SharePointOnlineLoginHelper.AuthObj.CreateHttpRequest(System.Net.Http.HttpMethod.Get,
                new Uri(String.Format("{0}/_api/web/lists/getByTitle('" + SharePointOnlineLoginHelper.AuthObj.LibraryName + "')/items()",
                    SharePointOnlineLoginHelper.AuthObj.SiteUrl.AbsoluteUri)));
            // the "results" json structure contains the list metadata
            JsonObject listDict = JsonObject.Parse(Encoding.UTF8.GetString(responseStream, 0, responseStream.Length));
            JsonArray lists = listDict["d"].GetObject()["results"].GetArray();
            for (int i = 1; i < lists.Count(); i++)
            {
                try
                {

                    JsonObject list = lists[i].GetObject();

                    if (list["Title"].ValueType.ToString() != "Null")
                    {
                        string title = list.ContainsKey("Title") ? list["Title"].GetString() : string.Empty;
                        string description = list.ContainsKey("Description") ?
                            list["Description"].GetString() : string.Empty;
                        //string basetemplate = list.ContainsKey("BaseTemplate") ?
                        //    list["BaseTemplate"].GetNumber().ToString() : string.Empty;
                        i = i + 1;
                        videobj = lists[i].GetObject();

                        string videoname = videobj["VideoRenditionLabel"].GetString();
                        string videourl = SharePointOnlineLoginHelper.AuthObj.SiteUrl +
                            "/" + SharePointOnlineLoginHelper.AuthObj.LibraryName + "/";
                        videourl += list["Title"].GetString() + "/" + videobj["VideoRenditionLabel"].GetString();
                        var video = new VideoData(title, videoname, description, videourl);
                        this._videoList.Add(video);
                    }
                    else if (list["VideoRenditionLabel"].ValueType.ToString() != "Null")
                    {
                        string videoname = list["VideoRenditionLabel"].GetString();
                        i = i + 1;
                        videobj = lists[i].GetObject();
                        //  }
                        string description = videobj.ContainsKey("Description") ?
                         videobj["Description"].GetString() : string.Empty;
                        //string basetemplate = videobj.ContainsKey("BaseTemplate") ?
                        //    videobj["BaseTemplate"].GetNumber().ToString() : string.Empty;
                        string title = videobj.ContainsKey("Title") ? videobj["Title"].GetString() : string.Empty;
                        string videourl = SharePointOnlineLoginHelper.AuthObj.SiteUrl +
                            "/" + SharePointOnlineLoginHelper.AuthObj.LibraryName + "/";
                        videourl += videobj["Title"].GetString() + "/" + list["VideoRenditionLabel"].GetString();
                        var video = new VideoData(title, videoname, description, videourl);
                        this._videoList.Add(video);

                    }

                }
                catch
                {
                    // Do no crash the app if we cannot parse the payload for a list
                }
            }
        }

        /// <summary>
        /// Populates all videos from sharepoint site
        /// </summary>
        private async void PopulateVideoDataSource()
        {
            try
            {
                await PopulateVideos();
                isDataInitialise = true;
            }
            catch(Exception ex)
            {

            }
        }
    }
}