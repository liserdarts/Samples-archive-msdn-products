﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CustomIMConversation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThreadAttribute]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new SearchForm());
        }
    }
}