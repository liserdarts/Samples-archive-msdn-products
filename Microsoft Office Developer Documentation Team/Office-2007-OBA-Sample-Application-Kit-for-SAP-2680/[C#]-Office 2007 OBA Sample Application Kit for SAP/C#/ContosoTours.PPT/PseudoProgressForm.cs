using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Microsoft.SAPSK.ContosoTours.PPT
{
    public partial class PseudoProgressForm : Form
    {
        public PseudoProgressForm()
        {
            InitializeComponent();
        }

        public string ProgressLabel
        {
            set
            {
                labelProgress.Text = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgBrush =
                new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(Width, 0),
                    Color.WhiteSmoke,
                    Color.FromArgb(30, 119, 211));

            e.Graphics.FillRectangle(
                lgBrush,
                new Rectangle(0, 0, Width, Height));

            base.OnPaint(e);
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            timer.Stop();
            if (progressBar.Value < 95)
            {
                progressBar.Value += 1;
                label1.Text = string.Format("{0}%", progressBar.Value);
                if (progressBar.Value > 80)
                {
                    timer.Interval += 30;
                }
                else if (progressBar.Value > 60)
                {
                    timer.Interval += 20;
                }
                else
                {
                    timer.Interval += 5;
                }
                timer.Start();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            timer.Stop();
            while (progressBar.Value < 100)
            {
                progressBar.Value += 1;
                label1.Text = string.Format("{0}%", progressBar.Value);
                Application.DoEvents(); //call system to continue painting the label
                Thread.Sleep(1);
            }
            base.OnClosing(e);
        }
    }
}