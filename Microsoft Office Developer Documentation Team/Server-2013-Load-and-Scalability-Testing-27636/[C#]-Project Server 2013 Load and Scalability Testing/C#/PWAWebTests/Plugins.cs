using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.WebTesting;
using System.ComponentModel;

namespace PWAWebTests
{
    [DisplayName("Generate GUID and store in context parameter")]
    public class GenerateGUID:WebTestRequestPlugin
    {
        private string contextParameterName;

        public string ContextParameterName
        {
            get { return contextParameterName; }
            set { contextParameterName = value; }
        }

        public override void PreRequestDataBinding(object sender, PreRequestDataBindingEventArgs e)
        {
            string value = Guid.NewGuid().ToString();

            if (e.WebTest.Context.ContainsKey(this.contextParameterName))
            {
                e.WebTest.Context[this.contextParameterName] = value;
            }
            else
            {
                e.WebTest.Context.Add(this.contextParameterName, value);
            }

        }

    }
}
