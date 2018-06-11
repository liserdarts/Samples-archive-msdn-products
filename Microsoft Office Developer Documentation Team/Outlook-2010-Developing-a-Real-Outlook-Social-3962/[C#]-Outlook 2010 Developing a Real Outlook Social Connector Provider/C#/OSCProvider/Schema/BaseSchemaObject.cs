using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace OSCProvider.Schema
{
    public abstract class SchemaObject
    {
        public abstract string Xml{get;}
        internal abstract XmlElement XmlEx { get; }
        internal ProviderSchemaVersion SchemaVersion = ProviderSchemaVersion.v1_0;
        
    }
}
