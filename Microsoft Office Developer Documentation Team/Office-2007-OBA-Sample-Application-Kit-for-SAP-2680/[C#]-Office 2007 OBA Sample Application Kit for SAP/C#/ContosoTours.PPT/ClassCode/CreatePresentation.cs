using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.PPT.Properties;
using Shape=Microsoft.Office.Interop.PowerPoint.Shape;

namespace Microsoft.SAPSK.ContosoTours.PPT
{
    public class CreatePresentation
    {
        enum SlideTemplate
        {
            PackageSlide = 1,
            EventSlide = 2,
            EventPricingSlide = 3,
            BlankSlide = 4,
            ChartClassSlide = 5,
            ChartAgeSlide = 6
        }

        private Presentation presentation;

        private Slides slides;

        public int _packageID;
        public string _directoryPath = string.Empty;
        public string _fullName;
        public string _xlsxLocationFile = string.Empty;

        private string _packageImageFileName;
        private string _pptxFileName;

        private float imageTop = 0;
        private float imageLeft = 0;
        private float imageHeight = 0;
        private float imageWidth = 0;

        private StatisticList statList = null;

        internal CreatePresentation()
        { }

        internal CreatePresentation(int packageID)
        {
            _packageID = packageID;
        }

        public void GeneratePresentation(int packageID)
        {
            _packageID = packageID;
            ViewPresentation();
        }

        private void CreatePackageSlide()
        {
            Slide slide;
            Shape pictureShape;
            Slide slidePackage;
            slidePackage = slides[SlideTemplate.PackageSlide];
            //set image layout
            imageTop = slidePackage.Shapes[1].Top;
            imageLeft = slidePackage.Shapes[1].Left;
            imageWidth = slidePackage.Shapes[1].Width;
            imageHeight = slidePackage.Shapes[1].Height;

            _packageImageFileName =
                  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                  "PackageImage" +
                  _packageID.ToString() +
                  ".jpg");

            SAPPackageReadWrite packageRW = new SAPPackageReadWrite(Config._dbConnectionName);

            using (SAPDataReaderPackage rdrPackage =
                packageRW.ReaderSelectByPackageID(_packageID))
            {
                if (rdrPackage.DataReader != null && rdrPackage.DataReader.HasRows)
                {
                    rdrPackage.DataReader.Read();
                    ByteToFile(rdrPackage.PackageImage, _packageImageFileName);

                    slide = DuplicateSlide(slidePackage);

                    _pptxFileName = rdrPackage.PackageName.Trim();

                    _pptxFileName = _pptxFileName.Replace(" ", "");

                    //package poster
                    do
                    {
                        slide.Shapes[1].Delete();
                    } while (slide.Shapes.Count > 0);
                    
                    if (File.Exists(_packageImageFileName))
                    {
                        pictureShape = slide.Shapes.AddPicture(
                            _packageImageFileName,
                            MsoTriState.msoFalse,
                            MsoTriState.msoCTrue,
                            imageLeft,
                            imageTop,
                            imageWidth,
                            imageHeight);
                        pictureShape.ZOrder(MsoZOrderCmd.msoSendToBack);
                    }

                    slide = DuplicateSlide(slidePackage);
                    slide.Shapes[1].Delete();
                    if (File.Exists(_packageImageFileName))
                    {
                        pictureShape = slide.Shapes.AddPicture(
                            _packageImageFileName,
                            MsoTriState.msoFalse,
                            MsoTriState.msoCTrue,
                            imageLeft,
                            imageTop,
                            imageWidth,
                            imageHeight);
                        pictureShape.ZOrder(MsoZOrderCmd.msoSendToBack);
                    }
                    slide.Shapes[4].TextFrame.TextRange.Text = rdrPackage.PackageName.Trim();
                    slide.Shapes[5].TextFrame.TextRange.Text = rdrPackage.PackageDescription.Trim();
                }
            }
        }

        private void CreateEventSlide()
        {
            Slide slide;
            Shape pictureShape;
            Table table;

            //get statistics

            PseudoProgressForm progress = new PseudoProgressForm();
            progress.ProgressLabel = "Querying SAP...";
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork +=
                delegate(object sender, DoWorkEventArgs e)
                {
                    statList = FlightDetail.GetStatistics(_packageID);
                };
            backgroundWorker.RunWorkerCompleted +=
                delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    progress.Close();
                    progress.Dispose();
                };

            backgroundWorker.RunWorkerAsync();
            progress.ShowDialog();

            SAPPackageEventReadOnly eventReadOnly =
                new SAPPackageEventReadOnly(Config._dbConnectionName);

            using (SAPDataReaderPackageEvent rdrEvent =
                eventReadOnly.ReaderSelectByPackageID(_packageID))
            {
                if (rdrEvent.DataReader != null &&
                    rdrEvent.DataReader.HasRows)
                {
                    while (rdrEvent.DataReader.Read())
                    {
                        string posterEventFile = string.Empty;
                        posterEventFile =
                           Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                           "PackageImage" +
                           _packageID.ToString() +
                           "_" +
                           rdrEvent.EventID.ToString() +
                           ".jpg");

                        ByteToFile(rdrEvent.EventPhoto, posterEventFile);

                        #region event slide

                        slide = DuplicateSlide(slides[SlideTemplate.EventSlide]);

                        //event name
                        slide.Shapes[5].TextFrame.TextRange.Text = rdrEvent.EventName;

                        //event description
                        slide.Shapes[6].TextFrame.TextRange.Text = rdrEvent.EventDescription;

                        //event date
                        slide.Shapes[8].TextFrame.TextRange.Text = rdrEvent.EventDate.ToString("MM/dd/yyyy hh:mmtt");

                        //venue name + city
                        slide.Shapes[7].TextFrame.TextRange.Text = rdrEvent.VenueCity + ", " + rdrEvent.VenueState;

                        //event posters
                        slide.Shapes[2].Delete();
                        if (File.Exists(posterEventFile))
                        {
                            pictureShape = slide.Shapes.AddPicture(
                                posterEventFile,
                                MsoTriState.msoFalse,
                                MsoTriState.msoCTrue, imageLeft, imageTop, imageWidth, imageHeight);
                            pictureShape.ZOrder(MsoZOrderCmd.msoSendToBack);
                        }
                        #endregion

                        //create sports actor

                        //create pricing
                        slide = DuplicateSlide(slides[SlideTemplate.EventPricingSlide]);
                        table = slide.Shapes[4].Table;
                        if (table.Rows.Count == 4 && table.Columns.Count == 2)
                        {
                            table.Cell(2, 2).Shape.TextFrame.TextRange.Text = rdrEvent.GoldPackagePrice.ToString("c");
                            table.Cell(3, 2).Shape.TextFrame.TextRange.Text = rdrEvent.SilverPackagePrice.ToString("c");
                            table.Cell(4, 2).Shape.TextFrame.TextRange.Text = rdrEvent.BronzePackagePrice.ToString("c");
                            slide.Shapes[1].Delete();
                            if (File.Exists(posterEventFile))
                            {
                                pictureShape = slide.Shapes.AddPicture(
                                    posterEventFile,
                                    MsoTriState.msoFalse,
                                    MsoTriState.msoCTrue, imageLeft, imageTop, imageWidth, imageHeight);
                                pictureShape.ZOrder(MsoZOrderCmd.msoSendToBack);
                            }
                        }
                        else
                        {
                            slide.Delete();
                        }

                    } //while (rdrEvent.DataReader.Read());
                }
            }
        }

        private void CreateChartSlide()
        {
            Shape pictureShape;
            if (statList == null)
            {
                slides[SlideTemplate.ChartClassSlide].Delete();
                slides[SlideTemplate.ChartAgeSlide].Delete();
            }
            else if (statList != null)
            {
                if (statList.AdultAgeStat +
                    statList.ChildAgeStat +
                    statList.InfantAgeStat == 0)
                {
                    slides[SlideTemplate.ChartAgeSlide].Delete();
                }
                else if (slides.Count != (int)SlideTemplate.ChartAgeSlide)
                {
                    if (File.Exists(_packageImageFileName))
                    {
                        slides[SlideTemplate.ChartAgeSlide].Shapes[1].Delete();
                        pictureShape = slides[SlideTemplate.ChartAgeSlide].Shapes.AddPicture(
                            _packageImageFileName,
                            MsoTriState.msoFalse,
                            MsoTriState.msoCTrue, imageLeft, imageTop, imageWidth, imageHeight);
                        pictureShape.ZOrder(MsoZOrderCmd.msoSendToBack);
                    }
                    slides[SlideTemplate.ChartAgeSlide].MoveTo(slides.Count);
                }

                if (statList.EconomyClassStat +
                    statList.BusinessClassStat +
                    statList.FirstClassStat == 0)
                {
                    slides[SlideTemplate.ChartClassSlide].Delete();
                }
                else if (slides.Count > ((int)SlideTemplate.ChartClassSlide) + 1)
                {
                    if (File.Exists(_packageImageFileName))
                    {
                        slides[SlideTemplate.ChartClassSlide].Shapes[1].Delete();
                        pictureShape = slides[SlideTemplate.ChartClassSlide].Shapes.AddPicture(
                            _packageImageFileName,
                            MsoTriState.msoFalse,
                            MsoTriState.msoCTrue, imageLeft, imageTop, imageWidth, imageHeight);
                        pictureShape.ZOrder(MsoZOrderCmd.msoSendToBack);
                    }
                    slides[SlideTemplate.ChartClassSlide].MoveTo(slides.Count - 1);
                }

            }

            if (File.Exists(_xlsxLocationFile))
            {
                Slide slide;
                slide = DuplicateSlide(slides[SlideTemplate.BlankSlide]);
                slide.Shapes.AddOLEObject(
                    122,
                    148,
                    486,
                    284,
                    "",
                    _xlsxLocationFile,
                    MsoTriState.msoFalse,
                    "",
                    0,
                    "",
                    MsoTriState.msoFalse);
                if (File.Exists(_packageImageFileName))
                {
                    slide.Shapes[1].Delete();
                    pictureShape = slide.Shapes.AddPicture(
                        _packageImageFileName,
                        MsoTriState.msoFalse,
                        MsoTriState.msoCTrue, imageLeft, imageTop, imageWidth, imageHeight);
                    pictureShape.ZOrder(MsoZOrderCmd.msoSendToBack);
                }
                File.Delete(_xlsxLocationFile);
            }
        }

        private void ViewPresentation()
        {
            Presentations presentations = Globals.ThisAddIn.Application.Presentations;

            #region initialize powerpoint template
            //now open the template format for the presentation
            if (_directoryPath.Length == 0)
            {
                if (!File.Exists(Config.PPTTemplate))
                {
                    throw new ApplicationException("Template not found.");
                }

                //Globals.ThisAddIn.Application.ActivePresentation.Close();

                //ByteToFile(Properties.Resources.packageTemplate, Config.PPTTemplate);

                //presentation = presentations.Open(
                //    Config.PPTTemplate,
                //    MsoTriState.msoCTrue,
                //    MsoTriState.msoCTrue,
                //    MsoTriState.msoCTrue);
            }
            else
            {
                presentation = Globals.ThisAddIn.Application.ActivePresentation;
            }
            //get the template slides
            slides = presentation.Slides;
            if (slides.Count < 5)
            {
                throw new ApplicationException("Template was altered.");
            }
            #endregion


            /* Create package poster */
            CreatePackageSlide();
            CreateEventSlide();
            CreateChartSlide();
            //delete the four template slide
            for (int i = 1; i <= 4; i++)
            {
                slides[1].Delete();
            }

            if (_directoryPath.Length > 0)
            {
                _pptxFileName += DateTime.Now.ToString("MMddyyyyhhmmss");
                presentation.SaveAs(
                    Path.Combine(_directoryPath, _pptxFileName),
                    PpSaveAsFileType.ppSaveAsDefault,
                    MsoTriState.msoTrue);
                File.SetAttributes(_fullName, FileAttributes.Archive);
                File.Delete(_fullName);
            }
        }

        #region helper
        private void ByteToFile(byte[] byteFile, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.SetAttributes(fileName, FileAttributes.Archive);
            }
            using (FileStream streamWrite =
                new FileStream(fileName, FileMode.OpenOrCreate))
            {
                streamWrite.Write(byteFile, 0, byteFile.Length);
                streamWrite.Close();
            }
        }

        private void ClearSlides()
        {
            Slides slides = presentation.Slides;
            do
            {
                Slide slide = slides[1];
                slide.Delete();
            } while (slides.Count != 0);
        }

        private Slide DuplicateSlide(Slide slide, int position)
        {
            SlideRange slideRange = slide.Duplicate();
            slideRange.MoveTo(position);
            return slides[position];
        }

        private Slide DuplicateSlide(Slide slide)
        {
            SlideRange slideRange = slide.Duplicate();
            slideRange.MoveTo(slides.Count);
            return slides[slides.Count];
        }
        #endregion
    }
}
