using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSDN.SharePoint.Samples
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class encapsulating collection of links
    /// </summary>
    [Serializable]
    public class DynamicLinks
    {
        /// <summary>
        /// The collection storing list of links
        /// </summary>
        private List<TabData> linkDetails = new List<TabData>();

        /// <summary>
        /// Gets or sets the link details.
        /// </summary>
        /// <value>The link details.</value>
        public List<TabData> LinkDetails
        {
            get { return this.linkDetails; }
            set { this.linkDetails = value; }
        }
    }
}
