// Copyright Microsoft Corporation
// Not intended for use in a production environment
//
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;


namespace O365_RWSQueryTool_cs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // constants and strings used globally and in lots of places

        const String CRLF = "\r\n";
        const String indent = "  "; // two characters for the indent level when formatting Json replies.

        // Passwords are stored in SecureStrings, and handled as infrequently as possible
        private SecureString passwordEntered = new SecureString();
        private String userNameEntered = System.String.Empty;
        private String fqdnHost = System.String.Empty;
        private String fullRestURL = System.String.Empty;
        private String serverCookie = System.String.Empty;

        
        // 
        // 
        //
        /// <summary>
        /// setSteps consolidates the enable/disable UI controls operations.
        /// the application's flow is maintained by disabling edit boxes,
        /// buttons, lables, and such, in "steps". If you go back to a step,
        /// and make some change that invalidates the displayed data or 
        /// choices in later steps, this routine is the one that is called
        /// to enable and disable those other steps. 
        /// </summary>
        /// <param name="step">specifies which step to operate one</param>
        /// <param name="enable">specifies whether enable or disable the step. 
        /// enable does only that one step, while disable does that step and
        /// all the higher-numbered steps.</param>
        private void setSteps(int step, Boolean enable)
        {
            // enable turns on JUST the specified step
            // disable turns off all steps from the specified on to the end
            for (int i = 1; i <= 7; i++)
            {
                if (((enable == true) && (i == step)) || ((enable == false) && (i >= step)))
                {
                    try
                    {
                        switch (i)
                        {
                            case 1:
                                {
                                    enterCredentials.IsEnabled = (bool)enable;
                                    break;
                                }
                            case 2:
                                {
                                    fqdnLabel.IsEnabled = (bool)enable;
                                    if (!enable) fqdnLookupResult.Content = String.Empty;
                                    fqdnLookup.IsEnabled = (bool)enable;
                                    fqdnLookupResult.IsEnabled = (bool)enable;
                                    fqdnReportingServer.IsEnabled = (bool)enable;
                                    break;
                                }
                            case 3:
                                {
                                    rptSelectionLabel.IsEnabled = (bool)enable;
                                    rptSelection.IsEnabled = (bool)enable;
                                    if (enable == false)
                                    {
                                        rptSelection.SelectedIndex = 0;
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    parameters.IsEnabled = enable;
                                    if (enable == false)
                                    {
                                        TextRange txtRange = new TextRange(parameters.Document.ContentStart, parameters.Document.ContentEnd);
                                        txtRange.Text = System.String.Empty;
                                    }
                                    parametersClear.IsEnabled = enable;
                                    parametersDefault.IsEnabled = enable;
                                    parametersLabel.IsEnabled = enable;
                                    jsonFormat.IsEnabled = enable;
                                    atomFormat.IsEnabled = enable;
                                    resultsFormatLabel.IsEnabled = enable;
                                    break;
                                }
                            case 5:
                                {
                                    generateRestUrl.IsEnabled = enable;
                                    builtRequestUrl.IsEnabled = enable;
                                    if (enable == false)
                                    {
                                        copyRestUrl.IsEnabled = enable;
                                        builtRequestUrl.Content = System.String.Empty;
                                    }
                                    break;
                                }
                            case 6:
                                {
                                    sendRequest.IsEnabled = enable;
                                    resultsLabel.IsEnabled = enable;
                                    resultsDisplay.IsEnabled = enable;
                                    if (enable == false)
                                    {
                                        copyFullResults.IsEnabled = enable;
                                        resultsDisplay.Text = System.String.Empty;
                                    }
                                    else
                                    {
                                        copyRestUrl.IsEnabled = enable;
                                    }
                                    break;
                                }
                            case 7:
                                {
                                    appendToLog.IsEnabled = enable;
                                    if (enable == true)
                                    {
                                        copyFullResults.IsEnabled = enable;
                                    }
                                    break;
                                }
                            default:
                                break;
                        }

                    }
                    catch  { }
                }
            }
        }

        /// <summary>
        /// this method parses through the service document's Atom-formatted XML to 
        /// identify which of the known reports are actually available. First it
        /// disables all the reports in the drop-down list. Then, for each of
        /// those present, it goes through and matches the items in the drop-down
        /// list of reports in the response, and then enables only those that appear
        /// present. Reports go in or out of scope based on user permissions 
        /// and organization subscriptions.
        /// </summary>
        /// <param name="response"></param>
        private void enableAvailableReports(HttpWebResponse response)
        {
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(response.GetResponseStream(), encode);
            // create a "temporary" string building where we'll add the text.
            StringBuilder sb = new StringBuilder();
            // Atom data is returned as an XML document, so create a new parser object
            XmlDocument doc = new XmlDocument();
            // and load the received stream into the document object
            doc.LoadXml(readStream.ReadToEnd());
            // first go through each of the items and disable it 
            foreach (ComboBoxItem cdItem in rptSelection.Items) cdItem.IsEnabled = false;
            // then do a search in the XML for each child element that contains a report name
            // they appear as collection elements, where the report name is inside href XML attribute
            // the href attribute is used because this is the REST relative resource path, as oppoed
            // to the title, which can be anything. Use that relative path in the drop down lists.
            try
            {
                XmlNodeList reportElements = doc.SelectNodes("//@href");
                //
                if (reportElements.Count > 0)
                {
                    // iterate through the names of the reports
                    foreach (XmlNode reportNode in reportElements)
                    {
                        // search for comboboxitems containing that name
                        foreach (ComboBoxItem rptListItem in rptSelection.Items)
                        {
                            //check whether the report name from the server matches the START 
                            // of the report name in the drop-down list. Allow a parenthetical
                            // comment (e.g, "(inbound)") in the list, so make sure it doesn't
                            // miss a valid match there.

                            if (((string)rptListItem.Content).Equals(((string)reportNode.InnerText)))
                            {
                                // Enable it if it matches exactly
                                rptListItem.IsEnabled = true;

                            }
                            else
                            {
                                if (((string)rptListItem.Content).StartsWith(((string)reportNode.InnerText + " ")))
                                {
                                    // and enable it if it matches the name with a space after it 
                                    // because some are named "Report (inbound)" and such.
                                    rptListItem.IsEnabled = true;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // nothing to do here but exit if there are no href attibutes,
                // because in that case just leave all of the reports disabled.
            }
        }

        
        /// <summary>
        /// This event is called to look up and verify that the web service endpoint is up and running, 
        /// and that the credentials supplied work with it. Getting these set before designing the URLs
        /// helps ensure the use doesn't get way into designing their query only to discover they
        /// don't really have permissions
        /// </summary>
        /// <param name="sender">object triggering the event</param>
        /// <param name="e">arguments sent to the event handler</param>
        private void fqdnLookup_Click(object sender, RoutedEventArgs e)
        {
            // determine whether the endpoint name is actually in the form of a fully-qualified domain name
            fqdnHost = fqdnReportingServer.Text;
            String pattern = @"(?=^.{1,254}$)(^(?:(?!\d+\.)[a-zA-Z0-9_\-]{1,63}\.?)+(?:[a-zA-Z]{1,})$)";
            Boolean isValidFqdn = Regex.IsMatch(fqdnHost, pattern);
            fqdnLabel.IsEnabled = true;
            // 
            // next try to do a simple dns lookup of the host. some might do a UDP ping to ensure the
            // server is "alive". but, current security practices have servers not responding to pings,
            // to avoid Denial-of-Service attacks targeting the ping interface. So, check the DNS 
            // records instead.
            //
            IPHostEntry hostInfo = new IPHostEntry();
            // only attempt the DNS lookup if endpoint appears to be a valid domain name.
            if (isValidFqdn == true)
            {
                // First do a DNS Lookup to make sure the server actually exists. Some DNS errors come back
                // having an empty address list, and others throw and exception. Either error mode means  can't
                // use that as a web services endpoint
                try
                {
                    // this is the call to do the DNS lookup
                    hostInfo = Dns.GetHostEntry(fqdnHost);
                    if (hostInfo.AddressList == null)
                    {
                        fqdnLookupResult.Content = "DNS lookup failed (check tooltip)";
                        fqdnLookupResult.ToolTip = "The DNS lookup returned no addresses; check the name";
                        setSteps(3, false);
                        return;
                    }
                }
                catch (Exception lookupException)
                {
                    fqdnLookupResult.Content = "DNS lookup failed (check tooltip)";
                    fqdnLookupResult.ToolTip = "The DNS lookup failed with the error \"" + lookupException.Message + "\"" ;
                    setSteps(3, false);
                    return;
                }
                // Next check whether anything returns from the Reporting web service endpoint
                // using the credentials the user entered. This simple query retrieves the service definition
                // document. One benefit is that retrieving this document doesn't require the
                // Web service to access the various log databases, so it should give minimal load to 
                // the Web service. Use the returned document, if there is one, to set the available
                // reports.
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format("https://{0}/ecp/reportingwebservice/reporting.svc", fqdnHost));
                request.Method = "GET";
                // this header specifies the ODATA provider's minimum version. Use $select in later queries,
                // it needs to have at least ODATA (v1). There isn't any support for anything higher than 2.0, 
                // use that as a maximum also.
                request.Headers.Add("DataServiceVersion", "1.0");
                request.Headers.Add("MaxDataServiceVersion", "2.0");
                // Add RWS language header
                request.Headers.Add("Accept-Language","EN-US");
                // Add RWS service version section header
                request.Headers.Add("X-RWS-Version","2013-V1");
                // becuase of authentication reasons, avoid redirecting the HTTPS request, just out of caution.
                request.AllowAutoRedirect = false;
                // this is the "simple" way to get the credentials. Eventually if/when the Forefront reporting system comes 
                // online, we'll include how to authenticate using a token obtained from LiveID.
                request.Credentials = new NetworkCredential(userNameEntered, passwordEntered);
                
                HttpWebResponse response = null;
                // outer try block is for any random other exceptions, but also to ensure that no matter what happens, the
                // response object gets properly closed.
                try
                {
                    // inner try block is for the actual HTTPS call to the host
                    try
                    {
                        // this makes the probe-call to get the service description document
                        response = (HttpWebResponse)request.GetResponse();
                    }
                    // because this is just a probing request, don't need to worry about errors coming back via 
                    // this exception source. 
                    catch { }
                    // if the request succeeded, including loggin in, use the response with a proper StatusCode.
                    if (response != null)
                    {
                        if  (response.StatusCode != HttpStatusCode.OK) {
                            // if the reuest failed, indicate in the label and its tooltip what the error was. this part
                            // is for when a normal HTTP error occurs, like a 404 not found error. 
                            fqdnLookupResult.Content = "Attempt failed: " + response.StatusCode.ToString() + "  (check tooltip)";

                            fqdnLookupResult.ToolTip = "The server was found, but the request failed with this" + CRLF + "HTTP error: " +
                                response.StatusDescription + ". Check the server, and " + CRLF + "possibly user name and password.";
                           // since it failed, disable the buttons for actions that depend on this having worked.
                            setSteps(3, false); 
                            // get out of the event handler.
                            return;
                        }
                        else
                        {
                            // if the code arrives here, the service document is available, so parse it and
                            // enable the report types that the user has access to
                            enableAvailableReports(response);
                        }

                    }
                    else
                    {
                        // this error condition is for when the HttpWebRequest fails so completely that there is no response
                        // object, and thus no HTTP error. This can happen if the user puts in a domain that exists, but that
                        // doesn't reply properly with the requested service document.
                        fqdnLookupResult.Content = "Attempt failed (check tooltip)";
                        fqdnLookupResult.ToolTip = "A server with that FQDN was found, but the request failed. " + CRLF + 
                            "Verify the server name, user name and password are correct.";
                        // since it failed, disable the buttons for actions that depend on this having worked.
                        setSteps(3, false);
                        // get out of the event handler
                        return;
                    }
                }
                finally
                {
                    // make sure the response, if it exists, gets properly closed.
                    if (response != null) response.Close();
                }
                fqdnLookupResult.Content = "Request successful  (check tooltip)";
                fqdnLookupResult.ToolTip = "The server was reached, the credentials were accepted," + CRLF + "and the service document was retrieved";
                // since this one actually succeeded, enable the buttons for the next step in line
                setSteps(3, true);
                // get out of the event handler
                return;
            }
            else
            {
                // this error is reported when the domain name just isn't normal-looking
                // according to the regular expressions above.
                fqdnLookupResult.Content = "Invalid service endpoint (check tooltip)";
                fqdnLookupResult.ToolTip = "The name does not appear to be a valid domain name";
                setSteps(3,false);
            }
        }

        
        /// <summary>
        /// this event is called when the user puts the cursor back into the endpoint
        /// text entry box. The action is to clear the status message so it doesn't look
        /// like that status came from the domain address being edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fqdnReportingServer_GotFocus(object sender, RoutedEventArgs e)
        {
            fqdnLookupResult.Content = " ";
        }

        /// <summary>
        /// this event is called when the application is fully loaded. Catch this at the
        /// start of execution, once all the controls are defined and initialized, so that 
        /// the code can disable most of them, to prevent the user clicking buttons 
        /// that won't actually work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void O365RWSQ_Loaded(object sender, RoutedEventArgs e)
        {
            setSteps(1, false);
            setSteps(1, true);
        }

        /// <summary>
        /// this event handler is called when they click the button to enter their username
        /// and password. it's shown modally. Notice how the securePassword is collected;
        /// we're only really taking a reference to the password, not actually moving the
        /// password itself.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterCredentials_Click(object sender, RoutedEventArgs e)
        {
            UserCredentials userCredsDialog = new UserCredentials();

            userCredsDialog.Owner = this;
            // if the user previously entered a user name, copy that up to the dialog so they don't have to 
            // retype it.
            userCredsDialog.credsUserName.Text = (string.IsNullOrEmpty(userNameEntered) ? "" : userNameEntered);
            // show modally so nothing else can happen
            userCredsDialog.ShowDialog();

            // retrieve the username and password from the dialog
            passwordEntered = userCredsDialog.credsPassword.SecurePassword;
            userNameEntered = userCredsDialog.credsUserName.Text;

            // yes, it does seem interesting that the code doesn't check the values here.
            // but those are checked in the validation routine when they click the OK button,
            // so this event only gets called when they click that OK button AND 
            // everything is verified as fine.
            userNameEntered = userCredsDialog.credsUserName.Text;

            // things seem to have worked out okay, so enable the controls for step 2.
            setSteps(2, true);
        }

        /// <summary>
        /// this event is called when the user makes a different selection from the drop-down list of reports.
        /// Call this so that the appropriate next steps can be performed. But the primary action of this
        /// is to check the newly selected item for content in its Tag property. We keep the default parameters
        /// in that, so it's convenient to initialize the edit box for those parameters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rptSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // the end-resule of this event is also to set the contents of the parameters area, 
            // so code can "select" anything in there now.
            TextRange txtRange = new TextRange(parameters.Document.ContentStart, parameters.Document.ContentEnd);
            // the first selction is a blank one, so both -1 and 0 represent non-selections.
            if (rptSelection.SelectedIndex > 0)
            {
                // disable step 4 and after, and
                // then enable just step 4.
                setSteps(4, false);
                setSteps(4, true);
                //
                // then clear the contents of the parameters editing box, and initialize it as proper
                if (((ListBoxItem)rptSelection.SelectedItem).Tag != null)
                {
                    // so that the user doesn't have to work with a single huge, badly line-wrapped 
                    // editing style, break the parameters at the & signs that separate the URL
                    // parameters. We replace those before the web services call is made.
                    txtRange.Text = Regex.Replace(((ListBoxItem)rptSelection.SelectedItem).Tag.ToString(), @"&",CRLF) + CRLF + " ";
                }
                else
                {
                    // since there are no parameters in the Tag propery, just clean the text.
                    txtRange.Text = System.String.Empty;
                }

            }
            else
            {
                // if somehow the user selects nothing, clear the parameters box, 
                // and disable the later steps
                txtRange.Text = System.String.Empty;
                setSteps(4, false);
            }
        }

        /// <summary>
        /// this event handler is called when the Clear button is clicked, and it is intended to 
        /// clear the contents of the parameter editing box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parametersClear_Click(object sender, RoutedEventArgs e)
        {
            // select all the text in the box, and delete it.
            TextRange txtRange = new TextRange(parameters.Document.ContentStart, parameters.Document.ContentEnd);
            txtRange.Text = System.String.Empty;

            // just because there are no parameters, that does not mean the web service can't be
            // called, so disable all the later steps, and then enble step 5.
            setSteps(5, false);
            setSteps(5, true);

        }

        /// <summary>
        /// this event handler is called when the user clicks the Copy button. It copies the REST URL to the
        /// clipboard for use elsewhere.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyRestUrl_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText((string)builtRequestUrl.Content);
        }

        /// <summary>
        /// this event handler builds the actual HTTPS REST query string from the several components
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateRestUrl_Click(object sender, RoutedEventArgs e)
        {
            TextRange txtRange = new TextRange(parameters.Document.ContentStart, parameters.Document.ContentEnd);
            txtRange.ClearAllProperties();

            // Remove the line breaks between parameters, and replace them with the &
            // because this is an HTTP GET, not a POST.
            String requestArgs = Regex.Replace(txtRange.Text.Trim(), @"\r\n?|\n", "&");
            // depending on the setting of the radio-buttons, add the ODATA2 format specifier
            String formatArg = (jsonFormat.IsChecked == true ? "$format=Json" : "$format=Atom");

            // and now we create the URL out of the component pieces.
            UriBuilder ub = new UriBuilder("https", fqdnHost);
            ub.Path = string.Format("ecp/reportingwebservice/reporting.svc/{0}",
                    Regex.Replace(((ListBoxItem)rptSelection.SelectedItem).Content.ToString(), @" \(.*", ""));
            ub.Query = (requestArgs == System.String.Empty ? formatArg : requestArgs + "&" + formatArg);
            fullRestURL = Uri.EscapeUriString(ub.Uri.ToString());
            // take the URL string and place it in the edit text box so it can be viewed.
            builtRequestUrl.Content = fullRestURL;
            // since the REST URL is now created, enable the use of the button to submit the request
            setSteps(6, true);
        }


        private void parametersDefault_Click(object sender, RoutedEventArgs e)
        {
            if (((ListBoxItem)rptSelection.SelectedItem).Tag != null)
            {
                TextRange txtRange = new TextRange(parameters.Document.ContentStart, parameters.Document.ContentEnd);

                // so that the user doesn't have to work with a single huge, badly line-wrapped 
                // editing style, break the parameters at the & signs that separate the URL
                // parameters. Replace those before the web services call is made.
                txtRange.Text = Regex.Replace(((ListBoxItem)rptSelection.SelectedItem).Tag.ToString(), @"&", CRLF) + CRLF + " ";
            }

            // because the parameters changed, that means the REST URL needs to be rebuilt.
            setSteps(5, false);
            setSteps(5, true);
        }


        /// <summary>
        /// this utility routine takes the response steam contents that comes back from a JSON
        /// query and formats it so that the information will be reasonably readable. The data
        /// sent back is compacted with white space removed for faster transfer. But, in 
        /// this sample, it needs to be human-readable.
        /// 
        /// 
        /// IMPORTANT: the text data produced should NOT be considered "correct", in that
        /// commas, {,[,], and }  embedded in non-structural portions will be handled as
        /// if they WERE structural. So it will be human-readable, but not guaranteed to 
        /// be syntactically correct. Do NOT use the output as actual Javascript code!
        /// 
        /// 
        /// </summary>
        /// <param name="sourceStream">the stream containing the information from the service</param>
        /// <param name="destString">the string that will contain the formatted material</param>
        /// <returns></returns>
        private int prettyPrintJson(StreamReader sourceStream, ref StringBuilder destString  )
        {
            String tmpData = sourceStream.ReadToEnd();
            int indentLevel = 0;
            int charsRead = 0;
            char ch;
            for (int j = 0; j < tmpData.Length; j++)
            {
                ch = tmpData[j];
                charsRead++;
                switch (ch)
                {
                    case '{':
                    case '[':
                        {
                            // to open a block or array, add a line, indent, place the character, 
                            // then add another line and increment the indentation level.
                            destString.Append("\r\n");
                            indentLevel++;
                            for (int i = 0; i < indentLevel; i++)
                            {
                                destString.Append(indent);
                            }
                            destString.Append(ch);
                            destString.Append("\r\n");
                            indentLevel++;
                            for (int i = 0; i < indentLevel; i++)
                            {
                                destString.Append(indent);
                            }
                            break;
                        }
                    case ',':
                        {
                            // for a comma, break the line and indent to get there.
                            // yes, this could mess with string contents. But really, you're 
                            // not supposed to use this json data in an application. Normally you'd
                            // use the information as returned from the service.
                            //
                            // THIS part is what causes the likelihood of the Json data being corrupted,
                            // because it doesn't take into account that when a comma occurs inside a string
                            // you're NOT supposed to break the line and effectively add whitespace.
                            //
                            destString.Append(ch);
                            destString.Append("\r\n");
                            for (int i = 0; i < indentLevel; i++)
                            {
                                destString.Append(indent);
                            }
                            break;
                        }
                    case ']':
                    case '}':
                        {
                            // To close a block or array, break the line, decrement the indent
                            // and add the closing character, and then break the line again and decrement
                            // the indent level
                            destString.Append("\r\n");
                            indentLevel--;
                            for (int i = 0; i < indentLevel; i++)
                            {
                                destString.Append(indent);
                            }
                            destString.Append(ch);
                            if (tmpData[(j < tmpData.Length - 1 ? j + 1 : j)] == ',')
                            {
                                j++;
                                charsRead++;
                                destString.Append(',');
                            }
                            if (j < tmpData.Length)
                            {
                                destString.Append("\r\n");
                                indentLevel--;
                                for (int i = 0; i < indentLevel; i++)
                                {
                                    destString.Append(indent);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            // if it's not one of those special characters, just pass it through.
                            destString.Append(ch);
                            break;
                        }
                }
            }
            return charsRead;
        }


        /// <summary>
        /// this event handler is called when the user clicks the button to make the reporting web
        /// service request. it takes the full REST url, adds the appropriate headers to the
        /// request, send it away, and wait for the response. in a real-world application, those
        /// should be done asynchronously. but here, since this is just a demonstration, the code  
        /// is inline.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendRequest_Click(object sender, RoutedEventArgs e)
        {
            // 
            int charsRead = 0;
            // clear the currently displayed text 
            resultsDisplay.Text = System.String.Empty;
            // create the two date-time variables for calculating the request duration
            System.DateTime responseReceived = new DateTime();
            System.DateTime requestStarted = new DateTime();
            // create the Web request container; REST uses HTTP(S) GET calls
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(fullRestURL);
            request.Method = "GET";
            // this header specifies the ODATA provider's minimum version. The intent is to use $select in later queries,
            // so it has to be least ODATA2. Use that same version as the maximum for when 3.0 is implemented.
            request.Headers.Add("DataServiceVersion", "2.0");
            request.Headers.Add("MaxDataServiceVersion", "2.0");
            // Add RWS language header
            request.Headers.Add("Accept-Language", "EN-US");
            // Add RWS service version section header
            request.Headers.Add("X-RWS-Version", "2013-V1");
            
            if (serverCookie != string.Empty)
            {
                request.Headers.Add("Cookie", serverCookie.ToString());
            }

            // becuase of authentication reasons, avoid redirecting the HTTPS request, just out of caution.
            request.AllowAutoRedirect = false;
            // this is the "simple" way to get the credentials. Eventually if/when the Forefront reporting system comes 
            // online, the application can be updated to authenticate using a token obtained from LiveID.
            request.Credentials = new NetworkCredential(userNameEntered, passwordEntered);
            // empty the response so that if it all falls through, it doesn't have the variable holding on to an old reference
            HttpWebResponse response = null;
            // outer try block is for any random other exceptions, but also to ensure that no matter what happens, the
            // response object gets properly closed.
            try
            {
                // create the variables that will hold the components of information to be displayed for the user
                int responseCode = 0; // the numeric response code
                String responseDesc = System.String.Empty; // descriptive HTTP response
                String requestHeadersBlock = System.String.Empty; // will hold formatted text of the headers sent
                String responseHeadersBlock = System.String.Empty; // the headers that came back
                String badDataReturned = System.String.Empty; // if the request fails, keep that bad data separate
                String requestDataReturned = System.String.Empty; // and where to put the returns when they succeed
                // inner try block is for the actual HTTPS call to the host
                try
                {
                    // get the current time, to calculate the time for processing
                    requestStarted = System.DateTime.Now;
                    // make the HTTP request to the server
                    response = (HttpWebResponse)request.GetResponse();
                    // if the call succeeds, some time later it will arrive here
                    responseReceived = System.DateTime.Now;
                    // record the response code and description
                    responseCode = (int)response.StatusCode;
                    responseDesc = response.StatusDescription;
                    // retrieve the headers SENT to the service and create the formatted block of text 
                    if (request.Headers.Count > 0)
                    {
                        for (int i = 0; i < request.Headers.Count; i++)
                        {
                            requestHeadersBlock += CRLF + request.Headers.GetKey(i) + ":" + request.Headers.GetValues(i)[0].ToString();
                        }
                    }
                    // retrieve the headers RECEIVED from the service and create the formatted block of text
                    if (response.Headers.Count > 0)
                    {
                        for (int i = 0; i < response.Headers.Count; i++)
                        {
                            responseHeadersBlock += CRLF + response.Headers.GetKey(i) + ":" + response.Headers.GetValues(i)[0].ToString();
                            // check to see if the header is the server's cookie; store it to return on the next call.
                            if ((response.Headers.GetKey(i) != null) && (response.Headers.GetKey(i) == "Set-Cookie"))
                            {
                                // set the cookie value to the header value
                                // note that normally you would verify the path of the cookie to make sure the code returns the right one. 
                                // however, for the Reporting web service, it always returns the same path, "/ecp/reportingwebservice",
                                // so it is reasonably safe to just return it, for this sample.
                                serverCookie = response.Headers.GetValues(i)[0].ToString();
                            }
                        }
                    }
                }
                // arrive here when something goes wrong with the request 
                catch (WebException excptn)
                {
                    // record when the response failed
                    responseReceived = System.DateTime.Now;
                    // get the response object passed in the exception
                    HttpWebResponse caughtResponse = (HttpWebResponse)excptn.Response;
                    // record the response HTTP code and description
                    responseCode = (int)caughtResponse.StatusCode;
                    responseDesc = caughtResponse.StatusDescription;
                    // retrieve the headers SENT to the service and create the formatted block of text
                    if (request.Headers.Count > 0)
                    {
                        for (int i = 0; i < request.Headers.Count; i++)
                        {
                            requestHeadersBlock += CRLF + request.Headers.GetKey(i) + ":" + request.Headers.GetValues(i)[0].ToString();
                        }
                    }
                    // retrieve the headers RECEIVED from the service and create the formatted block of text
                    // note that it only captures the FIRST value of that header, in case there are multiple values
                    if (caughtResponse.Headers.Count > 0)
                    {
                        for (int i = 0; i < caughtResponse.Headers.Count; i++)
                        {
                            responseHeadersBlock += CRLF + caughtResponse.Headers.GetKey(i) + ":" + caughtResponse.Headers.GetValues(i)[0].ToString();
                            // check to see if the header is the server's cookie, needed to return on the next call.
                            if ((caughtResponse.Headers.GetKey(i) != null) && (caughtResponse.Headers.GetKey(i) == "Set-Cookie"))
                            {
                                // set the cookie value to the header value
                                // note that normally you would verify the path of the cookie to make sure the code returns the right one. 
                                // however, for the Reporting web service, it always returns the same path, "/ecp/reportingwebservice",
                                // so it is reasonably safe to just return it, for this sample.
                                serverCookie = caughtResponse.Headers.GetValues(i)[0].ToString();
                            }
                        }
                    }
                    // now start the process of formatting the response body into something more human-readable
                    // force the character encoding so there are no mismatches
                    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                    // Pipes the stream to a higher level stream reader with the required encoding format. 
                    StreamReader readStream = new StreamReader(caughtResponse.GetResponseStream(), encode);
                    // create a "temporary" string building to add the text.
                    StringBuilder sb = new StringBuilder();
                    if (atomFormat.IsChecked == true)
                    {
                        // Atom data is returned as an XML document, so create a new parser object
                        XmlDocument doc = new XmlDocument();
                        // and load the received stream into the document object
                        doc.LoadXml(readStream.ReadToEnd());
                        // create a base writer to receive the formatted XML contents
                        System.IO.TextWriter tr = new System.IO.StringWriter(sb);
                        // and then create an XML writer based on that textwriter
                        XmlTextWriter wr = new XmlTextWriter(tr);
                        // enable indenting of the output text
                        wr.Formatting = Formatting.Indented;
                        // then save the XML document into the XML writer
                        doc.Save(wr);
                        // extract out the formatted text that came with the HTTP error
                        badDataReturned = sb.ToString();
                        // and close the XML writer
                        wr.Close();
                    }
                    else
                    {
                        // although this looks simpler, the JavaScript Json data has to be formatted
                        // by brute-force effort, which has been encapsulated into the PrettyPrintJson routine.
                        // remember that the formatted Json data should NOT be considered syntactically
                        // perfect. the ONLY intent is to allow a human to make sense of it.
                        charsRead = prettyPrintJson(readStream, ref sb);
                        // retrieve the data for use in the output
                        badDataReturned = sb.ToString();
                    }
                    // and close the stream holding the response body
                    readStream.Close();
                }
                // calculate the number of milliseconds that the request took to complete
                System.TimeSpan milliseconds = responseReceived.Subtract(requestStarted);
                // process the normal response block
                if (response != null)
                {
                    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                    // Pipes the stream to a higher level stream reader with the required encoding format. 
                    StreamReader readStream = new StreamReader(response.GetResponseStream(), encode);
                    // create a "temporary" string building into which the code adds the text.
                    StringBuilder sb = new StringBuilder();
                    if (atomFormat.IsChecked == true)
                    {
                        // Atom data is returned as an XML document, so create a new parser object
                        XmlDocument doc = new XmlDocument();
                        // and load the received stream into the document object
                        doc.LoadXml(readStream.ReadToEnd());
                        TextWriter tr = new StringWriter(sb);
                        // and then create an XML writer based on that textwriter
                        XmlTextWriter wr = new XmlTextWriter(tr);
                        // enable indenting of the output text
                        wr.Formatting = Formatting.Indented;
                        // then save the XML document into the XML writer
                        doc.Save(wr);
                        // extract out the formatted text that came with the HTTP error
                        requestDataReturned = sb.ToString();
                        // and close the XML writer
                        wr.Close();
                    }
                    else
                    {
                        // although this looks simpler, the JavaScript Json data has to be formatted
                        // by brute-force effort, which has been encapsulated into the PrettyPrintJson routine
                        // remember that the formatted Json data should NOT be considered syntactically
                        // perfect. the ONLY intent is to allow a human to make sense of it.
                        charsRead = prettyPrintJson(readStream, ref sb);
                        // retrieve the data for use in the output
                        requestDataReturned = sb.ToString();
                    }
                    // and close the stream holding the response body
                    readStream.Close();
                    //
                    // now format the results block for a successful request/response.
                    resultsDisplay.Text =
                        "==========================================================" + CRLF +
                        "====               SUCCESSFUL                         ====" + CRLF +
                        "==========================================================" + CRLF +
                        "Type:              " + ((ListBoxItem)rptSelection.SelectedItem).Content.ToString() + CRLF +
                        "Request sent:      " + requestStarted.ToString() + CRLF +
                        "Response received: " + responseReceived.ToString() + CRLF +
                        "Response time:     " + milliseconds.TotalMilliseconds + " ms" + CRLF +
                        "User name:         " + userNameEntered + CRLF +
                        "HTTP response:     " + responseCode + " " + responseDesc + CRLF +
                        "==== REQUEST URL =========================================" + CRLF +
                        fullRestURL + CRLF +
                        "==== HEADERS SENT ========================================" +
                        (requestHeadersBlock == System.String.Empty ? "*** No headers ***" : requestHeadersBlock) + CRLF +
                        "==== HEADERS RCVD ========================================" + 
                        (responseHeadersBlock == System.String.Empty ? "*** No headers ***" : responseHeadersBlock) + CRLF +
                        "==== RESULTS DOCUMENT ====================================" + CRLF +
                        requestDataReturned + CRLF +
                        "==========================================================" + CRLF +
                        "==========================================================" + CRLF + CRLF;
                }
                else
                {
                    // and do a similar formatting for the failed reqests
                    resultsDisplay.Text =
                        "==========================================================" + CRLF +
                        "====               FAILURE                            ====" + CRLF +
                        "==========================================================" + CRLF +
                        "Type:              " + ((ListBoxItem)rptSelection.SelectedItem).Content.ToString() + CRLF +
                        "Request sent:      " + requestStarted.ToString() + CRLF +
                        "Response received: " + responseReceived.ToString() + CRLF +
                        "Response time:     " + milliseconds.TotalMilliseconds + " ms" + CRLF +
                        "User name:         " + userNameEntered + CRLF +
                        "HTTP response:     " + responseCode + " " + responseDesc + CRLF +
                        "==== REQUEST URL =========================================" + CRLF +
                        fullRestURL + CRLF +
                        "==== HEADERS SENT ========================================" +
                        (requestHeadersBlock == System.String.Empty ? "*** No headers ***" : requestHeadersBlock) + CRLF +
                        "==== HEADERS RCVD ========================================" +
                        (responseHeadersBlock == System.String.Empty ? "*** No headers ***" : responseHeadersBlock) + CRLF +
                        "==== RESULTS DOCUMENT ====================================" + CRLF +
                        badDataReturned + CRLF +
                        "==========================================================" + CRLF +
                        "==========================================================" + CRLF + CRLF;
                    return;
                }
            }
            finally
            {
                // arrive here to ensure that the response stream is assured of being closed
                if (response != null) response.Close();
                // and make sure to enable the copy and log buttons in the UI.
                setSteps(7, true);
            }
        }

        /// <summary>
        /// this event handler is called whenever the text in the parameters box is changed
        /// by the user. All it does is disable the steps that won't work if this is not done.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parameters_TextChanged(object sender, TextChangedEventArgs e)
        {

            setSteps(5, false);
            setSteps(5, true);
        }

        /// <summary>
        /// this event handler is called when the user clicks on the Copy Full Results
        /// button. It copies the entire contents of the results text block up to the
        /// clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyFullResults_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(resultsDisplay.Text);
        }

        /// <summary>
        /// this very simple utility routine just builds the filename from today's date, and
        /// adds the current results information to the log file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppendToLog_Click(object sender, RoutedEventArgs e)
        {
            String myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            String todaysDate = Regex.Replace(System.DateTime.Now.ToShortDateString(),@"/",@"-");
            String logFileName = myDocumentsPath + string.Format("\\O365RWSLog_{0}.txt", todaysDate);
            StreamWriter logFile = File.AppendText(logFileName);
            logFile.Write(resultsDisplay.Text);
            logFile.Flush();
            logFile.Close();
        }


        /// <summary>
        /// this is called when the the text changes in the fqdn field. this is encased in a 
        /// try-catch block because if the fqdnReportingServer field is initialized before every
        /// single other component is intialized, it's possible that the setSteps call will
        /// occur before other items have been created, which throws an exception.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fqdnReportingServer_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                setSteps(3, false);
            }
            catch 
            {
            } 
        }

        /// <summary>
        /// this is called when the json format is checked. it's only action is to disable later
        /// steps that are no longer valid because the query URL needs to be rebuilt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jsonFormat_Checked(object sender, RoutedEventArgs e)
        {
            setSteps(5, false);
            setSteps(5, true);
        }

        /// <summary>
        /// this is called when the atom format is checked. it's only action is to disable later
        /// steps that are no longer valid because the query URL needs to be rebuilt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void atomFormat_Checked(object sender, RoutedEventArgs e)
        {
            setSteps(5, false);
            setSteps(5, true);
        }

    }
}
