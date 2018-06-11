using Microsoft.BusinessData.MetadataModel;

namespace BCSPowerPointAddin
{
    /// <summary>
    /// Chart's data object
    /// </summary>
    public class ChartDataObject
    {
        public System.Data.DataTable Data { get; set; }

        public IEntity Entity { get; set; }

        public string SeriesName { get; set; }

        public string[] XValues { get; set; }


    }
   
}
