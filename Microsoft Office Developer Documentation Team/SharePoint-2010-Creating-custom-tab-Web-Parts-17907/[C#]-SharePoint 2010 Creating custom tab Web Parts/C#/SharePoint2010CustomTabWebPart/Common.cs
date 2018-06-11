using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePoint2010CustomTabWebPart
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ResourceDependencyAttribute : Attribute
    {
        private string _path;

        public string Path { get { return _path; } }

        private int _priority;
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        public ResourceDependencyAttribute(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("Path should be non empty.");
            _priority = 100;
            _path = path;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class ScriptDependecyAttribute : ResourceDependencyAttribute
    {
        public ScriptDependecyAttribute(string scriptPath)
            : base(scriptPath)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class CSSDependecyAttribute : ResourceDependencyAttribute
    {
        public CSSDependecyAttribute(string cssPath)
            : base(cssPath)
        {
        }
    }
}
