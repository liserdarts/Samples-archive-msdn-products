using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using Windows.UI.Core;
using System.IO;

namespace UcwaWinStoreHello
{
    public class UcwaAppMe
    {
        public Transport Transport { get { return this.ucwaApp.Transport; } }
        public string DisplayName { get { return this.Resource == null ? null : this.Resource.GetPropertyValue("name"); } }
        public string Title { get { return this.Resource == null ? null : this.Resource.GetPropertyValue("title"); } }
        public string Department { get { return this.Resource == null ? null : this.Resource.GetPropertyValue("department"); } }
        public string Uri { get { return this.Resource == null ? null : this.Resource.GetPropertyValue("uri"); } }
        public UcwaResource Resource { get; private set; }
        public UcwaResource Note { get; private set; }
        public UcwaResource Presence { get; private set; }
        public UcwaResource Phones { get; private set; }
        public Stream Photo { get; private set; }
        public UcwaResource Location { get; private set; }

        UcwaApp ucwaApp;
        bool makeMeAvailablePosted = false;
        public UcwaAppMe(UcwaApp app)
        {
            this.ucwaApp = app;
            this.Resource = this.ucwaApp.ApplicationResource.GetEmbeddedResource("me");
        }

        public async Task<HttpStatusCode> Refresh()
        {
            //this.ucwaApp.GetUpdatedApplicationResource(appUri);
            this.Resource = this.ucwaApp.ApplicationResource.GetEmbeddedResource("me");
            if (this.Resource == null)
            {
                var meUri = this.ucwaApp.ApplicationResource.GetLinkUri("me");
                if (string.IsNullOrEmpty(meUri))
                    return HttpStatusCode.BadRequest;
                var response = await this.Transport.GetRequest(meUri);
                if (response.StatusCode != HttpStatusCode.OK)
                    return response.StatusCode;
                this.Resource = new UcwaResource(response.GetResponseStream());
            }
            //if (makeMeAvailablePosted)
            await this.GetEmbeddedResources();

            return HttpStatusCode.OK;
        }
        private async Task GetEmbeddedResources()
        {
            if (!makeMeAvailablePosted)
                return;

            // Notice: note, presence and phones, etc. are available and ready for use only after makeMeAvailable has been posted.
            // otherwise, the execution will hang.
            await this.GetNote();
            await this.GetPresence();
            await this.GetPhones();
            await this.GetLocation();
            await this.GetPhoto();
            return;

        }
        public async Task<HttpStatusCode> PostMakeMeAvailable(string phoneNumber, string signInAs,
            string[] supportedMessageFormats, string[] supportedModalities)
        {
            string makeMeAvailableUri = this.Resource.GetLinkUri("makeMeAvailable");
            var requestData =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<input xmlns=\"http://schemas.microsoft.com/rtc/2012/03/ucwa\">" +
                "  <property name=\"phoneNumber\">" + phoneNumber + "</property>" +
                "  <property name=\"signInAs\">" + signInAs + "</property>";
            if (supportedMessageFormats != null)
            {
                requestData += " <propertyList name=\"supportedMessageFormats\">";
                foreach (var format in supportedMessageFormats)
                    requestData += "    <item>" + format + "</item>";
                requestData += "  </propertyList>";
            }
            if (supportedModalities != null)
            {
                requestData += "  <propertyList name=\"supportedModalities\">";
                foreach (var modality in supportedModalities)
                    requestData += "    <item>" + modality + "</item>";
                requestData += "  </propertyList>";
            }
            requestData += "</input>";

            var response = await Transport.PostRequest(makeMeAvailableUri, requestData);
            if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.OK)
                makeMeAvailablePosted = true;
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> SetNote(string msg, string noteUri = null)
        {
            string inputFormat = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                 "<input xmlns=\"http://schemas.microsoft.com/rtc/2012/03/ucwa\">" +
                                 "  <property name=\"message\">{0}</property>" +
                                 "</input>";

            if (noteUri == null)
                noteUri = this.Resource.GetEmbeddedResourceUri("note");
            if (string.IsNullOrEmpty(noteUri))
                return HttpStatusCode.NotFound;

            var requestData = string.Format(inputFormat, msg);
            var response = await Transport.PostRequest(noteUri, requestData);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> GetNote(string noteUri = null)
        {
            if (!makeMeAvailablePosted) return HttpStatusCode.NotFound;
            this.Note = null;
            if (noteUri == null)
                noteUri = this.Resource.GetEmbeddedResourceUri("note");
            if (string.IsNullOrEmpty(noteUri))
                return HttpStatusCode.NotFound;
            this.Note = await this.GetResource(noteUri);
            return HttpStatusCode.OK;
        }
        public string NoteMessage { get { return Note == null ? null : Note.GetPropertyValue("message"); } }
        public string NoteType { get { return Note == null ? null : Note.GetPropertyValue("type"); } }
        public async Task<HttpStatusCode> GetPresence(string presenceUri = null)
        {
            if (!makeMeAvailablePosted) return HttpStatusCode.NotFound;
            this.Presence = null;
            if (presenceUri == null)
                presenceUri = this.Resource.GetEmbeddedResourceUri("presence");
            if (string.IsNullOrEmpty(presenceUri))
                return HttpStatusCode.NotFound;
            this.Presence = await this.GetResource(presenceUri);
            return HttpStatusCode.OK;

        }
        public async Task<HttpStatusCode> SetPresence(string availability, string presenceUri = null)
        {

            string inputFormat = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                 "<input xmlns=\"http://schemas.microsoft.com/rtc/2012/03/ucwa\">" +
                                 "  <property name=\"availability\">{0}</property>" +
                                 "</input>";

            if (string.IsNullOrEmpty(availability))
                return HttpStatusCode.BadRequest;

            if (presenceUri == null)
                presenceUri = this.Resource.GetEmbeddedResourceUri("presence");
            if (string.IsNullOrEmpty(presenceUri))
                return HttpStatusCode.NotFound;

            var requestData = string.Format(inputFormat, availability);
            var response = await Transport.PostRequest(presenceUri, requestData);
            return response.StatusCode;
        }
        public string PresenceAvailability { get { return Presence == null ? null : Presence.GetPropertyValue("availability"); } }
        public string PresenceActivity { get { return Presence == null ? null : Presence.GetPropertyValue("activity"); } }

        public async Task<HttpStatusCode> GetPhones(string phonesUri = null)
        {
            if (!makeMeAvailablePosted) return HttpStatusCode.NotFound;
            // Get my phones
            if (phonesUri == null)
                phonesUri = this.Resource.GetEmbeddedResourceUri("phones");
            if (string.IsNullOrEmpty(phonesUri))
                return HttpStatusCode.NotFound;
            this.Phones = await GetResource(phonesUri);
            return HttpStatusCode.OK;
        }
        public async Task<HttpStatusCode> GetLocation(string locationUri = null)
        {
            if (!makeMeAvailablePosted) return HttpStatusCode.NotFound;
            if (locationUri == null)
                locationUri = this.Resource.GetEmbeddedResourceUri("location");
            if (string.IsNullOrEmpty(locationUri))
                return HttpStatusCode.NotFound;
            this.Location = await GetResource(locationUri);
            return HttpStatusCode.OK;
        }
        public string LocationCoordinates
        { get { return Location == null ? null : Location.GetPropertyValue("location"); } }
        public async Task<HttpStatusCode> GetPhoto(string photoUri = null)
        {
            if (!makeMeAvailablePosted) return HttpStatusCode.NotFound;
            if (photoUri == null)
                photoUri = this.Resource.GetEmbeddedResourceUri("photo");
            if (string.IsNullOrEmpty(photoUri))
                return HttpStatusCode.NotFound;
            this.Photo = await GetPhotoStream(photoUri);//GetResource(photoUri);
            return HttpStatusCode.OK;
        }
        public IEnumerable<UcwaAppPhoneLine> PhoneLines
        {
            get
            {
                List<UcwaAppPhoneLine> phoneList = new List<UcwaAppPhoneLine>();
                foreach (var xElem in this.Phones.EmbeddedResourceElements)
                {
                    var resPhone = new UcwaResource(xElem);
                    var phoneType = resPhone.GetPropertyValue("type");
                    var phoneNumber = resPhone.GetPropertyValue("number");
                    var inConactCard = resPhone.GetPropertyValue("includeInContactCard");
                    phoneList.Add(new UcwaAppPhoneLine(phoneNumber, phoneType, inConactCard));
                }
                return phoneList.AsEnumerable();
            }
        }

        private async Task<UcwaResource> GetResource(string uri) //, UcwaResource parent)
        {
            if (string.IsNullOrEmpty(uri))
                return null;
            var response = await Transport.GetRequest(uri);
            if (response.StatusCode == HttpStatusCode.OK)
                return new UcwaResource(response.GetResponseStream());
            return null;
        }
        private async Task<Stream> GetPhotoStream(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                return null;
            var response = await Transport.GetRequest(uri);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.GetResponseStream();
            return null;

        }
    }

    public class UcwaAppPhoneLine
    {
        public string Type { get; private set; }
        public string Number { get; private set; }
        public string InContactCard { get; private set; }
        public UcwaAppPhoneLine(string number, string type, string inConactCard)
        {
            this.Type = type;
            this.Number = number;
            this.InContactCard = inConactCard;
        }
    }
}
