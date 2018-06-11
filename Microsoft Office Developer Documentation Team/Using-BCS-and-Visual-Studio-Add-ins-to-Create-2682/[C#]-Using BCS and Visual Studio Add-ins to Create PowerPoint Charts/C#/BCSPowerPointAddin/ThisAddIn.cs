using System;
using System.Linq;
using Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace BCSPowerPointAddin
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }
        #region IRibbonExtensibilityMembers


        /// <summary>
        /// Creats the Ribbon
        /// </summary>
        /// <returns></returns>
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Called when [insert chart].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="BCSPowerPointAddin.ChartDataEventArgs"/> instance containing the event data.</param>
        public void OnInsertChart(object sender, ChartDataEventArgs args)
        {
            if (args != null)
            {
                ChartDataObject chartDataObject = args.ChartDataObject;
                InsertChart(chartDataObject);
            }
        }
        /// <summary>
        /// Called when [update chart].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="BCSPowerPointAddin.ChartDataEventArgs"/> instance containing the event data.</param>
        public void OnUpdateChart(object sender, ChartDataEventArgs args)
        {
            if (args != null)
            {
                ChartDataObject chartDataObject = args.ChartDataObject;
                UpdateChart(chartDataObject);
            }
        }
        /// <summary>
        /// Updates the chart.
        /// </summary>
        /// <param name="chartDataObject">The chart data object.</param>
        public void UpdateChart(ChartDataObject chartDataObject)
        {
            Chart chart = null;
            Shape shape = null;
            Microsoft.Office.Interop.Excel.Workbook Workbook = null;
            Microsoft.Office.Interop.Excel.Application Application = null;
        try
            {
                Selection selection = this.Application.ActiveWindow.Selection;

                if (selection.Type == PpSelectionType.ppSelectionShapes&& selection.ShapeRange.HasChart == Office.MsoTriState.msoTrue)
                {
                    int count = selection.ShapeRange.Count;
                    for (int i = 0; i < count; i++)
                    {
                        //Loop through all shapes
                        shape = selection.ShapeRange[i + 1];
                        if (shape.HasChart == Office.MsoTriState.msoTrue)
                        {
                            //Set the first chart in the selection
                            chart = shape.Chart;
                            if (chart != null)
                            {
                                break;
                            }
                        }
                    }
                    if (chart == null)
                        return;
                    chart = AssignExternalDataToChart(chartDataObject, chart, shape);
                    chart.ChartData.Activate();
                }
            }

            finally
            {
                //Close the Excel Workbook and clean up resources
                if (chart != null && chart.ChartData != null)
                {
                    Workbook = chart.ChartData.Workbook as Microsoft.Office.Interop.Excel.Workbook;
                    if (Workbook != null)
                        Application = Workbook.Application;
                    if (Application != null && Workbook != null)
                    {
                        //Quit Excel Application
                        Application.Quit();
                        Application = null;
                        Workbook = null;
                        //Force GC, this is recommended for releasing resources since, 
                        //quitting Excel doesnt ensure resources are cleaned immediatly
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        //Repeated calls is necessary is because memory for the Excel 
                        //would have survived for the first pass
                        //Hence second try to recalim completely.
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
            }

        }

        /// <summary>
        /// Inserts the chart.
        /// </summary>
        /// <param name="chartDataObject">The chart data object.</param>
        public void InsertChart(ChartDataObject chartDataObject)
        {
            Chart chart = null;
            Shape shape = null;
            Microsoft.Office.Interop.Excel.Workbook Workbook = null;
            Microsoft.Office.Interop.Excel.Application Application = null;
            try
            {
                //Add the new slide
                Slide slide = this.Application.ActivePresentation.Slides.Add(this.Application.ActivePresentation.Slides.Count + 1, PpSlideLayout.ppLayoutChart);
                //Insert the chart
                shape = slide.Shapes.AddChart(Office.XlChartType.xlLine);
                chart = shape.Chart;
                //Clear the chart contents
                chart.ChartArea.ClearContents();
                object[] Values = new object[chartDataObject.XValues.Count()];

                for (int i = 0; i < chartDataObject.Data.Rows.Count; i++)
                {
                    for (int j = 0; j < chartDataObject.XValues.Length; j++)
                    {
                        Values[j] = chartDataObject.Data.Rows[i][chartDataObject.XValues[j].ToString()];
                    }
                    SeriesCollection SeriesCollection = chart.SeriesCollection() as SeriesCollection;
                    //Create new series based on the data
                    Series Series = SeriesCollection.NewSeries();
                    Series.Name = chartDataObject.Data.Rows[i][chartDataObject.SeriesName].ToString();
                    Series.XValues = chartDataObject.XValues;
                    Series.Values = Values;
                }
                //Activate the chart to refresh with the fresh data.
                chart.ChartData.Activate();
            }
            finally
            {
                //Close the Excel Workbook and clean up resources.
                if (chart != null && chart.ChartData != null)
                {
                    Workbook = chart.ChartData.Workbook as Microsoft.Office.Interop.Excel.Workbook;
                    if (Workbook != null)
                        Application = Workbook.Application;
                    if (Application != null && Workbook!=null)
                    {
                        //Quit Excel Application
                        Application.Quit();
                        Application = null;
                        Workbook = null;
                        //Force the GC, this is recommended for releasing the resources. 
                        //Quitting Excel doesnt ensure resources are cleaned immediatly
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        //Repeated calls is necessary is because the memory for the Excel 
                        //would have survived for the first pass.
                        //Hence the second try to recalim completely.
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
            }

        }
        /// <summary>
        /// Processes the chart data.
        /// </summary>
        /// <param name="chartDataObject">The chart data object.</param>
        /// <param name="chart">The chart.</param>
        /// <param name="shape">The shape.</param>
        /// <returns></returns>
        private static Chart AssignExternalDataToChart(ChartDataObject chartDataObject, Chart chart, Shape shape)
        {
            chart = shape.Chart;
            chart.ChartArea.ClearContents();
            object[] Values = new object[chartDataObject.XValues.Count()];

            for (int i = 0; i < chartDataObject.Data.Rows.Count; i++)
            {
                for (int j = 0; j < chartDataObject.XValues.Length; j++)
                {
                    Values[j] = chartDataObject.Data.Rows[i][chartDataObject.XValues[j].ToString()];
                }
                SeriesCollection SeriesCollection = chart.SeriesCollection() as SeriesCollection;
                //Create new series based on the data
                Series Series = SeriesCollection.NewSeries();
                Series.Name = chartDataObject.Data.Rows[i][chartDataObject.SeriesName].ToString();
                Series.XValues = chartDataObject.XValues;
                Series.Values = Values;
            }
            return chart;
        }
        #endregion
        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
