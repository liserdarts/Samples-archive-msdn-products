using System;
using System.Threading;
using System.Windows.Forms;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Opacity += 0.1;

            if (Opacity == 1)
            {
                timer.Stop();
                Thread.Sleep(2000);
                Close();
            }
        }
    }
}