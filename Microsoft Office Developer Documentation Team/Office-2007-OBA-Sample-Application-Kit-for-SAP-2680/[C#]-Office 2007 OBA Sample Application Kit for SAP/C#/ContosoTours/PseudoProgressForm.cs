using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class PseudoProgressForm : Form
    {
        private int _multiplier;
        public PseudoProgressForm()
        {
            _multiplier = 1;
            InitializeComponent();
        }

        public PseudoProgressForm(int delaymultiplier)
        {
            _multiplier = delaymultiplier;
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

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (progressBar.Value < 95)
            {
                progressBar.Value += 1;
                label1.Text = string.Format("{0}%", progressBar.Value);
                if (progressBar.Value > 80)
                {
                    timer.Interval += (50 * _multiplier);
                }
                else if (progressBar.Value > 60)
                {
                    timer.Interval += (25 * _multiplier);
                }
                else
                {
                    timer.Interval += (7 * _multiplier);
                }
                timer.Start();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            timer.Stop();
            while(progressBar.Value < 100)
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