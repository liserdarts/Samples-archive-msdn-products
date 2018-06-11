using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Microsoft.VisualBasic;

namespace Microsoft.SAPSK.ContosoTours.Helper
{
    public class UtilityHelper
    {
        public static Image ByteToImage(byte[] image)
        {
            using (MemoryStream ms = 
                new MemoryStream(
                    image, 
                    0,
                    image.Length))
            {
                ms.Write(
                    image,
                    0,
                    image.Length);
                return Image.FromStream(ms, true);
            }         
        }

        public static byte[] FileToByte(string fileName)
        {
            byte[] byteImage = null;
            FileInfo imageFile = new FileInfo(fileName);

            using (FileStream fs = new FileStream(
                fileName,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read))
            {
                int fileSize = Convert.ToInt32(imageFile.Length);
                byteImage = new byte[fileSize];
                int bytesRead = fs.Read(
                    byteImage,
                    0,
                    fileSize);
                fs.Close();
            }
            return byteImage;
        }

        public static byte[] BitmapToByte(Bitmap bitmap)
        {
            byte[] bmpBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                bmpBytes = ms.GetBuffer();
                bitmap.Dispose();
                ms.Close();
            }
            return bmpBytes;
        }

        public static void ByteToFile(byte[] byteFile, string fileName)
        {
            using (FileStream streamWrite =
                new FileStream(fileName, FileMode.OpenOrCreate))
            {
                streamWrite.Write(byteFile, 0, byteFile.Length);
                streamWrite.Close();
            }
        }

        public static void EnableControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl.Controls.Count > 0)
                {
                    EnableControls(ctrl.Controls);
                }
                else if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = false;
                }
                else if (ctrl is DateTimePicker)
                {
                    ((DateTimePicker)ctrl).Enabled = true;
                }
                else if (ctrl is ComboBox)
                {
                    ((ComboBox)ctrl).Enabled = true;
                }
                else if (ctrl is LinkLabel)
                {
                    ((LinkLabel)ctrl).Enabled = true;
                }
                else if (ctrl is MonthCalendar)
                {
                    ((MonthCalendar)ctrl).Enabled = true;
                }
            }
        }

        public static void SetToReadOnly(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl.Controls.Count > 0)
                {
                    SetToReadOnly(ctrl.Controls);
                }
                else if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = true;
                }
                else if (ctrl is DateTimePicker)
                {
                    ((DateTimePicker)ctrl).Enabled = false;
                }
                else if (ctrl is ComboBox)
                {
                    ((ComboBox)ctrl).Enabled = false;
                }
                else if (ctrl is LinkLabel)
                {
                    ((LinkLabel)ctrl).Enabled = false;
                }
                else if (ctrl is MonthCalendar)
                {
                    ((MonthCalendar)ctrl).Enabled = false;
                }
            }
        }

        public static void ClearTextbox(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl.Controls.Count > 0)
                {
                    ClearTextbox(ctrl.Controls);
                }
                else if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Text = string.Empty;
                }
            }
        }

        public static long GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            long retVal = 0;
            retVal = DateAndTime.DateDiff(DateInterval.Month, startDate, endDate,
                FirstDayOfWeek.System, FirstWeekOfYear.System);
            return retVal;
        }

        
    }
}
