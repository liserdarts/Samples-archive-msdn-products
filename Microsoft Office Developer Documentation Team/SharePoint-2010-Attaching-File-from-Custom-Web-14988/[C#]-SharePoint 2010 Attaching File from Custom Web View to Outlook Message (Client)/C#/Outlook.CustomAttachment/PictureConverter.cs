using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Outlook.CustomAttachment
{
    internal class PictureConverter : AxHost
    {
        /// <summary>
        /// Prevents a default instance of the PictureConverter class from being created.
        /// Initializes a new instance of the <see cref="PictureConverter"/> class.
        /// </summary>
        private PictureConverter()
            : base(String.Empty)
        {
        }

        /// <summary>
        /// Images to picture disp.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The image to display.</returns>
        public static stdole.IPictureDisp ImageToPictureDisp(Image image)
        {
            return (stdole.IPictureDisp)GetIPictureDispFromPicture(image);
        }

        /// <summary>
        /// Icons to picture disp.
        /// </summary>
        /// <param name="icon">The icon value.</param>
        /// <returns>The image to display.</returns>
        public static stdole.IPictureDisp IconToPictureDisp(Icon icon)
        {
            return ImageToPictureDisp(icon.ToBitmap());
        }

        /// <summary>
        /// Pictures the disp to image.
        /// </summary>
        /// <param name="picture">The picture.</param>
        /// <returns>The image to display.</returns>
        public static Image PictureDispToImage(stdole.IPictureDisp picture)
        {
            return GetPictureFromIPicture(picture);
        }
    }
}
