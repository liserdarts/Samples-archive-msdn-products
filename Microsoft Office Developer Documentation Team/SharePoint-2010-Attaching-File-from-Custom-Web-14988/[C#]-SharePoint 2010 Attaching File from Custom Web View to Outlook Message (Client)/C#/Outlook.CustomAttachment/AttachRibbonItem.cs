// <copyright file="AttachRibbonItem.cs" company="Microsoft">
// Copyright Microsoft 2010
// </copyright>
// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

////  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
////  {
////      return new AttachRibbonItem();
////  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.

namespace Outlook.CustomAttachment
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Office = Microsoft.Office.Core;

    /// <summary>
    /// The Ribbon UI for Outlook.
    /// </summary>
    [ComVisible(true)]
    public class AttachRibbonItem : Office.IRibbonExtensibility
    {
        /// <summary>
        /// The Ribbon UI.
        /// </summary>
        private Office.IRibbonUI ribbon;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachRibbonItem"/> class.
        /// </summary>
        public AttachRibbonItem()
        {
            // default constructor
        }

        /// <summary>
        /// Occurs when [attach_ clicked].
        /// </summary>
        public event EventHandler Attach_Clicked;

        /// <summary>
        /// Gets the custom UI.
        /// </summary>
        /// <param name="ribbonID">The ribbon ID.</param>
        /// <returns>The resource text.</returns>
        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("Outlook.CustomAttachment.AttachRibbonItem.xml");
        }        

        /// <summary>
        /// Occurs when the Ribbon loads.
        /// </summary>
        /// <param name="ribbonUI">The ribbon UI.</param>
        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
                this.ribbon = ribbonUI;
                this.ribbon.InvalidateControl("CCAttachButton");
        }

        /// <summary>
        /// Event fired when the control is clicked.
        /// </summary>
        /// <param name="control">The control.</param>
        public void Control_Clicked(Office.IRibbonControl control)
        {
                if (this.Attach_Clicked != null)
                {
                    this.Attach_Clicked(this, null);
                }
        }

        /// <summary>
        /// Gets the image from the resource file for use by the ribbon button
        /// </summary>
        /// <param name="imageName">Name of the image as it appears in the resource file.</param>
        /// <returns>The image to display.</returns>
        public stdole.IPictureDisp GetImage(string imageName)
        {
            return PictureConverter.IconToPictureDisp((System.Drawing.Icon)global::Outlook.CustomAttachment.Properties.Resources.ResourceManager.GetObject(imageName));
        }


        /// <summary>
        /// Gets the resource text.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The resource text.</returns>
        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }

            return null;
        }
    }
}
