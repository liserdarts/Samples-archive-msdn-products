using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using ConsoleAppDotNET35;

namespace ActivityLibrary1
{
    public sealed class UcmaHello : CodeActivity
    {
        private UCProgram ucma;

        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }

        //Calls the UcMaIM() method in the UCMA project.
        //This activity executes when the user enters 1. The UcMaIM() 
        // method will send an instant message with the text “hello”.
        protected override void Execute(CodeActivityContext context)
        {
            ucma = new UCProgram();
            ucma.UcmaIM(1);
        }
    }
}