using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Contoso.IncidentDashboard
{
    /// <summary>
    /// The StatusHeader UserControl is used to display status for a single
    /// incident status type.
    /// </summary>
    public partial class StatusHeader : UserControl
    {
        /// <summary>
        /// The Image dependency property is used to set the image to display.
        /// A dependency property is used so that the Image can be set within
        /// the xaml markup of the parent.
        /// </summary>
        public ImageSource Image
        {
            get { return imgIcon.Source; }
            set { 
                imgIcon.Source = value;
                imgIcon.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource),
                                        typeof(StatusHeader), null);

        /// <summary>
        /// The Label dependency property is used to set the label on the 
        /// control. A dependency property is used to allow setting the label
        /// from xaml.
        /// </summary>        
        public string Label
        {
            get { return txtLabel.Text; }
            set { txtLabel.Text = value; }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string),
                                        typeof(StatusHeader), null);

        /// <summary>
        /// The _count variable and its Count property are used to set the 
        /// number displayed as count on the label.
        /// </summary>
        private double _count = 0;
        public double Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                txtCount.Text = " (" + _count + ")";
            }
        }

        /// <summary>
        /// The Selected property sets the background of the usercontrol based
        /// on the value provided. Background color is used to indicate a 
        /// selected or unselected state.
        /// </summary>      
        public bool Selected
        {
            get { 
                return ((SolidColorBrush)LayoutRoot.Background).Color.Equals(Colors.White);
            }
            set
            {
                if (value) LayoutRoot.Background = new SolidColorBrush(Colors.White);
                else LayoutRoot.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register("Selected", typeof(bool),
                                        typeof(StatusHeader), null);

        /// <summary>
        /// The Status property is used to hold the status token indicated by 
        /// the SP request.
        /// </summary>
        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), 
                                        typeof(StatusHeader), null);

        /// <summary>
        /// The StatusHeader Constructor. Calls InitializeComponent to lay
        /// child controls out.
        /// </summary>
        public StatusHeader()
        {
            InitializeComponent();
        }
    }
}
