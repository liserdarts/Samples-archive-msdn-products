using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Vsip.AllowParams
{
    internal partial class OptionsControl : UserControl
    {
        private OptionsPage _parent;

        public OptionsControl()
        {
            InitializeComponent();
        }

        public OptionsControl(OptionsPage parent)
        {
            _parent = parent;
            InitializeComponent();
        }
    }
}
