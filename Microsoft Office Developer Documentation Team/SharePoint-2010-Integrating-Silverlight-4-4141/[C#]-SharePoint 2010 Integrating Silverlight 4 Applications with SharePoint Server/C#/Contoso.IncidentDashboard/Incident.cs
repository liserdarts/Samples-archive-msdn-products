using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Contoso.IncidentDashboard
{
    /// <summary>
    /// The Incident class is used to store data pertaining to a single
    /// incident.
    /// </summary>
    public class Incident
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public string Agent { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
