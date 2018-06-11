using System;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;
using System.Threading;

namespace MusicBrainzSilverlightApplication
{
    public partial class MainPage : UserControl
    {
        private WebClient client;
        private SynchronizationContext thread;
        List<Album> albums;
        List list;

        //Event handlers
        public static event ClientRequestSucceededEventHandler succeedListener;
        public static event ClientRequestFailedEventHandler failListener;

        //Delegates
        public delegate void succeedDelegate(object sender, ClientRequestSucceededEventArgs e);
        public delegate void failDelegate(object sender, ClientRequestFailedEventArgs e);

        public MainPage()
        {
            InitializeComponent();
            Messages.Text = string.Empty;

            //web client
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);

            //Create new delegates
            succeedDelegate sd = new succeedDelegate(HandleClientRequestSucceeded);
            failDelegate fd = new failDelegate(HandleClientRequestFailed);

            //Create new event handlers
            succeedListener += new ClientRequestSucceededEventHandler(sd);
            failListener += new ClientRequestFailedEventHandler(fd);

            thread = System.Threading.SynchronizationContext.Current;
            if (thread == null)
                thread = new System.Threading.SynchronizationContext();
        }

        //Call web service
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string requestUri = "http://musicbrainz.org/ws/1/release/?limit=10&type=xml&artist={0}";
            client.OpenReadAsync(new Uri(String.Format(requestUri, Keyword.Text)));
            Messages.Text = "Searching...";
        }

        //Show results in grid
        void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            XElement results;
            albums = new List<Album>();
            Messages.Text = string.Empty;

            if (e.Error != null)
            {
                Messages.Text = e.Error.Message;
                return;
            }
            else
            {
                try
                {
                    XNamespace ns = @"http://musicbrainz.org/ns/mmd-1.0#";

                    results = XElement.Load(e.Result);

                    var q = from r in results.Descendants(ns + "release")
                            select new
                            {
                                Title = r.Element(ns + "title").Value,
                                ArtistId = r.Element(ns + "artist").Attribute("id").Value,
                                Artist = r.Element(ns + "artist").Element(ns + "name").Value
                            };

                    foreach (var i in q)
                    {
                        albums.Add(new Album()
                        {
                            Title = i.Title,
                            ArtistId = i.ArtistId,
                            Artist = i.Artist
                        });
                    }

                    ItemGrid.ItemsSource = albums;
                    Messages.Text = albums.Count.ToString() + " result(s) found.";
                }
                catch (Exception x)
                {
                    Messages.Text = x.Message;
                }
            }
        }

        //Save to list
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientContext ctx = new ClientContext("http://wingtipserver/clientom");

                list = ctx.Web.Lists.GetByTitle("MusicBrainz");
                ctx.Load(list);

                //Add list items
                foreach (Album album in albums)
                {
                    ListItemCreationInformation listItemCI = new ListItemCreationInformation();
                    ListItem item = list.AddItem(listItemCI);
                    item["ArtistId"] = album.ArtistId;
                    item["Title"] = album.Title;
                    item["Artist"] = album.Artist;
                    item.Update();
                }

                //Execute
                ctx.ExecuteQueryAsync(succeedListener, failListener);

            }
            catch (PropertyOrFieldNotInitializedException)
            {
                Messages.Text = "Property not initilaized.";
            }
            catch (InvalidQueryExpressionException)
            {
                Messages.Text = "Invalid query.";
            }
            catch (ClientRequestException x)
            {
                Messages.Text = "Request Exception. " + x.Message;
            }
            catch (ServerException x)
            {
                Messages.Text = "Server Exception. " + x.Message;
            }
            catch (Exception x)
            {
                Messages.Text = x.Message;
            }
        }

        //Delegate definitions
        public void HandleClientRequestSucceeded(object sender, ClientRequestSucceededEventArgs args)
        {
            thread.Post(new System.Threading.SendOrPostCallback(delegate(object state)
            {
                EventHandler h = OperationSucceeded;
                if (h != null)
                    h(this, EventArgs.Empty);
            }), null);
        }

        public void HandleClientRequestFailed(object sender, ClientRequestFailedEventArgs args)
        {
            thread.Post(new System.Threading.SendOrPostCallback(delegate(object state)
            {
                EventHandler h = OperationFailed;
                if (h != null)
                    h(this, args);
            }), null);
        }

        //Event handlers
        public void OperationSucceeded(object sender, EventArgs e)
        {
            if (list == null)
                Messages.Text = "null list";
            else
                Messages.Text += "Write to list succeeded!";
        }

        public void OperationFailed(object sender, EventArgs e)
        {
            ClientRequestFailedEventArgs args = (ClientRequestFailedEventArgs)e;
            Messages.Text += "Write to list failed! " + args.Message;
        }

    }

    public class Album
    {
        public string ArtistId { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
    }
}
