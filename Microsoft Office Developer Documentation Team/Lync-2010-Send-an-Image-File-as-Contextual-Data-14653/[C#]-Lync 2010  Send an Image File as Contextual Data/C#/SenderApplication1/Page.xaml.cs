using System;
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
using System.IO;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Controls;

namespace SenderApplication1
{
    public partial class Page : UserControl
    {
        ConversationContextualInfo contextInfo;
        string imageData;
        string AppID = "{add your guid here}";
        Conversation conversation;

        public Page()
        {
            InitializeComponent();
            SendInitialData();
        }

        void SendInitialData()
        {
            contextInfo = new ConversationContextualInfo();
            contextInfo.ApplicationId = AppID;
            contextInfo.ApplicationData = "initial context data";
            IMButton.ContextualInformation = contextInfo;
            IMButton.Source = "sip:barbara@contoso.com";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            bool? result = ofd.ShowDialog();
            if (!result.HasValue || result.Value == false)
                return;
            FileStream filestream = ofd.File.OpenRead();
            byte[] binaryData = new Byte[filestream.Length];
            long bytesreaD = filestream.Read(binaryData, 0, (int)filestream.Length);
            imageData = Convert.ToBase64String(binaryData, 0, binaryData.Length);
            conversation = (Conversation)Microsoft.Lync.Model.LyncClient.GetHostingConversation();
            conversation.BeginSendContextData(AppID, "text/plain", imageData, SendDataCallback, null);
            textBox1.Text = imageData.Length.ToString();
        }

        public void SendDataCallback(IAsyncResult res)
        {
            conversation.EndSendContextData(res);
        }
    }
}
