using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class SAPErrorForm : Form
    {
        public SAPErrorForm()
        {
            InitializeComponent();
        }

        public SAPErrorForm(
            string errorMessage)
        {
            InitializeComponent();

            textBoxError.Text = errorMessage;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgBrush =
                new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(Width, 0),
                    Color.WhiteSmoke,
                    Color.Red);

            e.Graphics.FillRectangle(
                lgBrush,
                new Rectangle(0, 0, Width, Height));

            base.OnPaint(e);
        }
    }
}