// ***************************************************************
// <copyright file="BodyConversion.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// Filters scripts out of email messages.
// </summary>
// ***************************************************************

namespace Microsoft.Exchange.Samples.Agents.BodyConversion
{
    using System;
    using System.IO;
    using System.Text;
    using System.Diagnostics;
    using Microsoft.Exchange.Data.Transport;
    using Microsoft.Exchange.Data.Transport.Smtp;
    using Microsoft.Exchange.Data.Transport.Email;
    using Microsoft.Exchange.Data.TextConverters;

    /// <summary>
    /// Agent factory.
    /// </summary>
    public class BodyConversionFactory : SmtpReceiveAgentFactory
    {
        /// <summary>
        /// Creates a new BodyConversion.
        /// </summary>
        /// <param name="server">Exchange server.</param>
        /// <returns>A new BodyConversion.</returns>
        public override SmtpReceiveAgent CreateAgent(SmtpServer server)
        {
            return new BodyConversion();
        }
    }

    /// <summary>
    /// SmtpReceiveAgent for the BodyConversion sample.
    /// </summary>
    public class BodyConversion : SmtpReceiveAgent
    {
        /// <summary>
        /// An object to synchronize access to the log file.
        /// </summary>
        private object fileLock = new object();


        /// <summary>
        /// The constructor registers an end-of-data event handler.
        /// </summary>
        public BodyConversion()
        {
            Debug.WriteLine("[BodyConversion] Agent constructor");
            this.OnEndOfData += new EndOfDataEventHandler(this.OnEndOfDataHandler);
        }

        /// <summary>
        /// Invoked by Exchange when the entire message has been received.
        /// </summary>
        /// <param name="source">The source of this event.</param>
        /// <param name="eodArgs">The arguments for this event.</param>
        public void OnEndOfDataHandler(ReceiveMessageEventSource source, EndOfDataEventArgs eodArgs)
        {

            Debug.WriteLine("[BodyConversion] OnEndOfDataHandler is called");

            // The purpose of this sample is to show how TextConverters can be used
            // to update the body. This sample shows how to ensure that no active
            // content, such as scripts, can pass through in a message body.

            EmailMessage message = eodArgs.MailItem.Message;
            Stream originalBodyContent = null;
            Stream newBodyContent = null;
            Encoding encoding;
            string charsetName;

            try
            {

                Body body = message.Body;
                BodyFormat bodyFormat = body.BodyFormat;

                if (!body.TryGetContentReadStream(out originalBodyContent))
                {
                    // You cannot decode the body.
                    return;
                }

                if (BodyFormat.Text == bodyFormat)
                {
                    // Plain text body. Add a disclaimer to this body. 

                    charsetName = body.CharsetName;
                    if (null == charsetName ||
                        !Microsoft.Exchange.Data.Globalization.Charset.TryGetEncoding(charsetName, out encoding))
                    {
                        // Either no charset, or charset is not supported by the system.
                        return;
                    }

                    // Create and set up the TextToText conversion object.
                    TextToText textToTextConversion = new TextToText();

                    // Don't change the body code page.
                    textToTextConversion.InputEncoding = encoding;
                    // By default, the output code page is the same as the input code page.

                    // Add a disclaimer to indicate that the body is not being filtered.
                    textToTextConversion.HeaderFooterFormat = HeaderFooterFormat.Text;
                    textToTextConversion.Footer = "the plain text body is NOT being filtered";

                    // Open the stream to write updated content into.
                    newBodyContent = body.GetContentWriteStream();

                    // Do the conversion. This will replace the original text.
                    // with an updated version.
                    try
                    {
                        // The easiest way to do the conversion in one step is by using the Convert 
                        // method. It creates the appropriate converter stream
                        // and copies from one stream to another.
                        textToTextConversion.Convert(originalBodyContent, newBodyContent);
                    }
                    catch (Microsoft.Exchange.Data.TextConverters.TextConvertersException)
                    {
                        // The conversion has failed.
                    }

                }
                else if (BodyFormat.Html == bodyFormat)
                {
                    // The HTML body. 

                    // Filter the original HTML to remove any scripts and other
                    // potentially unsafe content.

                    charsetName = body.CharsetName;
                    if (null == charsetName ||
                        !Microsoft.Exchange.Data.Globalization.Charset.TryGetEncoding(charsetName, out encoding))
                    {
                        // Either no charset, or charset is not supported by the system.
                        return;
                    }

                    // Create and set up the HtmlToHtml conversion object.
                    HtmlToHtml htmlToHtmlConversion = new HtmlToHtml();

                    // Don't change the body code page.
                    htmlToHtmlConversion.InputEncoding = encoding;
                    // By default, the output code page is the same as the input code page.

                    // Set up output HTML filtering. HTML filtering will ensure that
                    // no scripts or active content can pass through.
                    htmlToHtmlConversion.FilterHtml = true;

                    // Add a disclaimer to indicate that the body is being filtered.
                    htmlToHtmlConversion.HeaderFooterFormat = HeaderFooterFormat.Html;
                    htmlToHtmlConversion.Footer = "<p><font face=\"Arial\" size=\"+1\">the HTML body is being filtered</font></p>";

                    // Open the stream to write updated content into.
                    newBodyContent = body.GetContentWriteStream();

                    // Do the conversion. This will replace the original HTML
                    // with a filtered version.
                    try
                    {
                        // The easiest way to do the conversion in one step is by using the Convert 
                        // method. It creates an appropriate converter stream
                        // and copies from one stream to another.
                        htmlToHtmlConversion.Convert(originalBodyContent, newBodyContent);
                    }
                    catch (Microsoft.Exchange.Data.TextConverters.TextConvertersException)
                    {
                        // The conversion has failed.
                    }

                }
                else if (BodyFormat.Rtf == bodyFormat)
                {
                    // This is compressed RTF body in a TNEF message.

                    // Compressed RTF body can contain the encapsulated original HTML to be 
                    // filtered.

                    // Do not modify the message format here, just filter out 
                    // potentially unsafe content. Convert RTF to HTML, filter out any
                    // unsafe HTML content, and convert the result back to RTF.

                    // Note that conversion from RTF to HTML and back to RTF can result in loss of data. Some
                    // embedded images in RTF can be lost and formatting can slightly change.

                    // First wrap the original body content, which is in a "compressed RTF" format, into
                    // a stream that will do RTF decompression. Reading from this stream
                    // will return original RTF (the same format that the Word and RichEdit controls are using).
                    ConverterStream uncompressedRtf = new ConverterStream(originalBodyContent, new RtfCompressedToRtf(), ConverterStreamAccess.Read);

                    // Create and configure the RtfToHtml conversion object.
                    RtfToHtml rtfToHtmlConversion = new RtfToHtml();

                    // Set up output HTML filtering. HTML filtering will ensure that
                    // no scripts or active content can pass through.
                    rtfToHtmlConversion.FilterHtml = true;

                    // Add a disclaimer to indicate that the body is being filtered.
                    rtfToHtmlConversion.HeaderFooterFormat = HeaderFooterFormat.Html;
                    rtfToHtmlConversion.Footer = "<p><font face=\"Arial\" size=1>the RTF body is being filtered</font></p>";

                    // Now create a wrapping TextReader object, which returns the filtered 
                    // HTML converted (or extracted) from the original RTF.
                    ConverterReader html = new ConverterReader(uncompressedRtf, rtfToHtmlConversion);

                    // After you filter the HTML, convert it back to RTF. For the purposes of this sample, do not
                    // change the message format.

                    // Create and configure the HtmlToRtf conversion object.
                    HtmlToRtf htmlToRtfConversion = new HtmlToRtf();

                    // Create a wrapping stream that returns RTF converted back from filtered HTML.
                    ConverterStream filteredRtf = new ConverterStream(html, htmlToRtfConversion);

                    // Create and configure the RtfToRtfCompressed conversion object, which
                    // will compress the resulting RTF.
                    RtfToRtfCompressed rtfCompressionConversion = new RtfToRtfCompressed();

                    // Always write it back in a compressed form.
                    rtfCompressionConversion.CompressionMode = RtfCompressionMode.Compressed;

                    // Open the stream to write updated body content into.
                    newBodyContent = body.GetContentWriteStream();

                    // Finally, perform the complete chain of conversions you set up. This 
                    // will read the original compressed RTF chunk by chunk, convert it to HTML 
                    // and filter scripts, then convert HTML back to RTF, compress it and write 
                    // back into the current message body.
                    try
                    {
                        // Use the Convert method because it is convenient. This Convert
                        // method compresses the resulting RTF it reads from the conversion 
                        // chain stream you set up and writes the result into the output stream. 
                        // It saves a few lines of code that would be required to create another wrapper stream and 
                        // copy from one stream to another.
                        rtfCompressionConversion.Convert(filteredRtf, newBodyContent);
                    }
                    catch (Microsoft.Exchange.Data.TextConverters.TextConvertersException)
                    {
                        // The conversion has failed.
                    }
                }

                else
                {
                    // Handle cases where the body format is not one of the above.
                }
            }

            finally
            {
                if (originalBodyContent != null)
                {
                    originalBodyContent.Close();
                }

                if (newBodyContent != null)
                {
                    newBodyContent.Close();
                }
            }
        }
    }
}
