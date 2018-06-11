using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using InsertingOpenXmlSample.Properties;
using Microsoft.Office.Interop.Word;
using a = DocumentFormat.OpenXml.Drawing;
using pic = DocumentFormat.OpenXml.Drawing.Pictures;
using Word = Microsoft.Office.Interop.Word;
using wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;

namespace InsertingOpenXmlSample
{
public partial class ThisDocument
{
    private Int32 contentControlSeedId = 100000;
    private void ThisDocument_Startup(object sender, System.EventArgs e)
    {
    }
    private void ThisDocument_Shutdown(object sender, System.EventArgs e)
    {
    }
    #region VSTO Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InternalStartup()
    {
        this.btnOM.Click += new System.EventHandler(this.btnOM_Click);
        this.button1.Click += new System.EventHandler(this.button1_Click);
        this.Shutdown += new System.EventHandler(this.ThisDocument_Shutdown);
        this.Startup += new System.EventHandler(this.ThisDocument_Startup);

    }
    #endregion
    /// <summary>
    /// Finds the bookmark range
    /// </summary>
    /// <param name="BookmarkName"></param>
    /// <returns></returns>
    private Word.Range FindRange(string BookmarkName)
    {
        object bookmarkName = BookmarkName;
        Word.Range bookmarkRange = this.Bookmarks.get_Item(ref bookmarkName).Range;
        return bookmarkRange;
    }
    private void button1_Click(object sender, EventArgs e)
    {
        //Inserts a table with 300 rows using Open Xml
        object bookmarkName = "bkflatOpc";
        if (!this.Bookmarks.Exists("bkflatOpc"))
        {
            MessageBox.Show("Please create a bookmark with name bkflatOpc");
            return;
        }
        Word.Bookmark tblBookmark = this.Bookmarks.get_Item(ref bookmarkName);
        if (tblBookmark.Range.Tables.Count > 0)
        {
            MessageBox.Show("Table already exists.Please delete this table.");
            return;
        }
        this.Application.ScreenUpdating = false;
        string openxml = string.Empty;
        int incrementor = 1;
        //Get existing content control id , if there is any
        if (this.ContentControls.Count > 0)
        {
            object index = this.ContentControls.Count;
            Word.ContentControl cc = this.ContentControls.get_Item(ref index);
            contentControlSeedId = Int32.Parse(cc.ID);
            contentControlSeedId = contentControlSeedId + 1;
        }
        //Get stream for the range. This is the System.IO.Packaging.Package stream
        Stream packageStream = this.Paragraphs[1].Range.GetPackageStreamFromRange();
        //Use Open Xml SDK to process it.
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(packageStream, true))
        {
            //Insert image to Image Part
            ImagePart imgPart = 
                wordDoc.MainDocumentPart.AddImagePart(ImagePartType.Jpeg, "RImage");
            System.Drawing.Image img = 
                (System.Drawing.Image)Resources.ResourceManager.GetObject("RImage");
            img.Save(imgPart.GetStream(), System.Drawing.Imaging.ImageFormat.Jpeg);
            //Remove all children
            wordDoc.MainDocumentPart.Document.Body.RemoveAllChildren();
            //Generate table markup.
            DocumentFormat.OpenXml.Wordprocessing.Table table = 
                new DocumentFormat.OpenXml.Wordprocessing.Table();
            TableProperties properties =
               new TableProperties(
               new DocumentFormat.OpenXml.Wordprocessing.TableStyle() 
               { Val = "TableGrid" },
               new TableWidth() { Width = 0, Type = TableWidthUnitValues.Auto },
               new TableLook() { Val = "04A0" });
            table.AppendChild<TableProperties>(properties);
            for (int i = 0; i < 301; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < 3; j++)
                {
                    incrementor++;
                    SdtCell contentControl = 
                        GenerateSdtCell("Sample Text", incrementor + contentControlSeedId);
                    UInt32 value = (UInt32)(incrementor + contentControlSeedId);
                    contentControl
                        .Descendants<Run>()
                        .First()
                        .AppendChild<Drawing>
                        (GenerateDrawing("RImage",value,"RImage.jpg",321933L,288000L, 
                        19050L, 17957L,0L,0L));
                    row.AppendChild<SdtCell>(contentControl);
                }
                table.AppendChild<TableRow>(row);
            }
            int bookmarkCount = this.Bookmarks.Count;
            table.
                PrependChild<BookmarkStart>(new BookmarkStart() {
                    Name = "bkflatOpc",
                    Id = bookmarkCount.ToString()
                }
                );
            table.
                AppendChild<BookmarkEnd>
                (new BookmarkEnd(){ Id = bookmarkCount.ToString()});
            wordDoc.MainDocumentPart.Document
                .Body
                .AppendChild<DocumentFormat.OpenXml.Wordprocessing.Table>(table);
            wordDoc.MainDocumentPart.Document.Save();
            //Flush the contents of the package
            wordDoc.Package.Flush();
            //Convert back to flat opc using this in-memory package
            XDocument xDoc = OpcHelper.OpcToFlatOpc(wordDoc.Package);
            openxml = xDoc.ToString();

        }
        this.Application.ScreenUpdating = false;
        Word.Range range = FindRange("bkflatOpc");
        //Insert this flat opc Xml
        range.InsertXML(openxml, ref missing);
        this.Application.ScreenUpdating = true;
    }
    /// <summary>
    /// Creates a picture
    /// </summary>
    /// <param name="rID">Relationship id</param>
    /// <param name="docPrID"></param>
    /// <param name="pictureName">Name of the picture</param>
    /// <param name="cxExtent">Height of the picture</param>
    /// <param name="cyExtent">Width of the picture</param>
    /// <param name="lEffectExtent">Extent on left Edge</param>
    /// <param name="rEffectExtent">Extent on right Edge</param>
    /// <param name="tEffectExtent">Extent on top Edge</param>
    /// <param name="bEffectExtent">Extent on bottom Edge</param>
    /// <returns></returns>
    public static Drawing GenerateDrawing(string rID, UInt32 docPrID, 
        string pictureName, Int64 cxExtent, Int64 cyExtent, 
        Int64 lEffectExtent, Int64 rEffectExtent, Int64 
        tEffectExtent, Int64 bEffectExtent)
    {
    DocumentFormat.OpenXml.Wordprocessing.Drawing DrawingElement =
        new DocumentFormat.OpenXml.Wordprocessing.Drawing(
        new wp.Inline(
            new wp.Extent() { Cx = cxExtent, Cy = cyExtent },
            new wp.EffectExtent() 
            { LeftEdge = lEffectExtent, TopEdge = tEffectExtent, 
                RightEdge = rEffectExtent, BottomEdge = bEffectExtent },
            new wp.DocProperties() 
            { Id = (UInt32Value)docPrID, Name = pictureName },
            new wp.NonVisualGraphicFrameDrawingProperties(
                new a.GraphicFrameLocks() { NoChangeAspect = true }),
            new a.Graphic(
            new a.GraphicData(
            new pic.Picture(
                new pic.NonVisualPictureProperties(
                    new pic.NonVisualDrawingProperties() 
                    { Id = (UInt32Value)0U, Name = pictureName },
                    new pic.NonVisualPictureDrawingProperties()),
                new pic.BlipFill(
                    new a.Blip() 
                    { Embed = rID,CompressionState = 
                        DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print 
                    },
                    new a.Stretch(
                        new a.FillRectangle())),
                new pic.ShapeProperties(
                    new a.Transform2D(
                        new a.Offset() { X = 0L, Y = 0L },
                        new a.Extents() { Cx = cxExtent, Cy = cyExtent }),
                    new a.PresetGeometry(
                        new a.AdjustValueList()
                    ) { 
                        Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle 
                    }))
                ) 
                { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
            ));
    return DrawingElement;
    }
    /// <summary>
    /// Generates a content control
    /// </summary>
    /// <param name="text">Text content</param>
    /// <param name="contentcontrolId">Content control id</param>
    /// <returns></returns>
    public static SdtCell GenerateSdtCell(string text, int contentcontrolId)
    {
        SdtCell element =
        new SdtCell(
        new SdtProperties(
            new SdtId() { Val = contentcontrolId },
            new SdtPlaceholder(
                new DocPartReference())),
        new SdtContentCell(
            new TableCell(

                new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                    new ProofError() { Type = ProofingErrorValues.SpellStart },
                    new Run(
                        new Text(text)),
                    new ProofError() { Type = ProofingErrorValues.SpellEnd }
                ))));
        return element;
    }
    Word.Row wordRow = null;
    private void btnOM_Click(object sender, EventArgs e)
    {
        //Inserts a table with 300 rows using automation
        object bookmarkName = "bkAutomation";
        if (!this.Bookmarks.Exists("bkAutomation"))
        {
            MessageBox.Show("Please create bookmark with name bkAutomation");
            return;
        }
        Word.Bookmark tblBookmark = this.Bookmarks.get_Item(ref bookmarkName);
        if (tblBookmark.Range.Tables.Count > 0)
        {
            MessageBox.Show("Table already exists.Please delete this table.");
            return;
        }
        Word.Range range = tblBookmark.Range;
        Word.Table table = 
            this.Tables.Add(range, 1, 3, ref missing, ref missing);
        table.Borders.InsideLineStyle = 
            Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        table.Borders.OutsideLineStyle = 
            Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        for (int i = 0; i < 301; i++)
        {
            wordRow = table.Rows.Add(ref missing);
            foreach (Cell cell in wordRow.Cells)
            {
                ContentControl contentcontrol = 
                    cell.Range.ContentControls.Add
                    (WdContentControlType.wdContentControlRichText, ref missing);
            }
        }
        object tableRange = table.Range;
        //Add bookmark back to the table
        table.Range.Bookmarks.Add("bkAutomation", ref tableRange);
    }
}
}
