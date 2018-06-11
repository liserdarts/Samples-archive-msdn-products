using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.SharePoint;

namespace XmlObjectSnapshot
{
    class Program
    {
        static void Main(string[] args)
        {
            // The XML "model" file is the input, which defines the set of SharePoint
            // classes and properties that will be crawled via reflection.
            // The XmlReporter class itself does not have any dependencies on
            // SharePoint, and can work with any API.
            XmlReporter reporter = new XmlReporter("ContentTypeSnapshotModel.xml");

            Log("Opening web...");

            using (SPSite site = new SPSite("http://intranet.contoso.com/"))
            {
                SPWeb web = site.RootWeb;

                Log("Traversing objects...");
                XElement reportXml = reporter.GenerateReport("SPWeb", web);
                XDocument document = new XDocument(reportXml);

                Log("Writing output...");

                // The ContentTypeSnapshot.xml is the report, i.e. a big dump of all the
                // SharePoint objects.  You can view it using the Visual Studio text editor,
                // which has great support for large files, and also supports an "outline"
                // mode that allows you to "drill down" by expanding only the XML branches
                // that you are interested in (via the "Toggle All Outlining" editor
                // command: Ctrl+M, Ctrl+L).
                //
                // WinMerge is also a very useful tool for comparing these files:  If you
                // dump the state before and after making a change in SharePoint, you can
                // then use WinMerge to compare the two text files to understand exactly
                // which data properties were affected by your change.
                using (StreamWriter streamWriter = new StreamWriter("ContentTypeSnapshot.xml"))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(streamWriter,
                        new XmlWriterSettings { NewLineOnAttributes = true, Indent = true }))
                    {
                        document.Save(xmlWriter);
                    }
                    streamWriter.WriteLine();
                }

            }
            Log("Done.");

            if (Debugger.IsAttached)
                Debugger.Break();
        }

        static void Log(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

    }
    #region ReportModel, etc.

    [Serializable]
    [XmlType("Model")]
    public class ReportModel
    {
        [XmlElement("Type")]
        public List<ReportType> Types = new List<ReportType>();
    }

    [Serializable]
    [XmlType("Type")]
    public class ReportType
    {
        [XmlAttribute]
        public string Name;

        [XmlElement("Element", typeof(ReportElementProperty))]
        [XmlElement("Attribute", typeof(ReportAttributeProperty))]
        [XmlElement("Collection", typeof(ReportCollectionProperty))]
        public List<ReportBaseProperty> Properties = new List<ReportBaseProperty>();

        [XmlAttribute]
        public bool UseToString;

        [XmlAttribute]
        public bool MatchChildClasses;

        public override string ToString() { return (this.Name ?? "(null)") + " (ReportType)"; }
    }

    [Serializable]
    [XmlType]
    public abstract class ReportBaseProperty
    {
        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public bool UseToString;

        public override string ToString() { return (this.Name ?? "(null)") + " (" + this.GetType().Name + ")"; }
    }

    [Serializable]
    [XmlType]
    public class ReportElementProperty : ReportBaseProperty
    {
        [XmlAttribute]
        public bool AsEmbeddedXml;
    }

    [Serializable]
    [XmlType]
    public class ReportAttributeProperty : ReportBaseProperty
    {
    }

    [Serializable]
    [XmlType]
    public class ReportCollectionProperty : ReportBaseProperty
    {
        [XmlAttribute]
        public string OrderBy;

        [XmlElement("Element", typeof(ReportElementProperty))]
        [XmlElement("Collection", typeof(ReportCollectionProperty))]
        public ReportBaseProperty ItemProperty = null;
    }

    #endregion

    class XmlReporter
    {
        static readonly ReportType UseToStringReportType = new ReportType() { Name = "(UseToString)", UseToString = true };

        Dictionary<string, ReportType> TypesByName = new Dictionary<string, ReportType>();

        public XmlReporter(string modelFilename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ReportModel));

            using (Stream stream = new FileStream(modelFilename, FileMode.Open))
            {
                ReportModel model = (ReportModel)serializer.Deserialize(stream);

                foreach (ReportType type in model.Types)
                    TypesByName.Add(type.Name, type);

                foreach (Type basicType in new[] { 
                    typeof(bool), 
                    typeof(byte), 
                    typeof(System.Int16), 
                    typeof(System.UInt16), 
                    typeof(System.Int32), 
                    typeof(System.UInt32), 
                    typeof(System.Int64), 
                    typeof(System.UInt64), 
                    typeof(string), 
                    typeof(Guid), 
                    typeof(float), 
                    typeof(double),
                    typeof(DateTime)
                    })
                {
                    if (!TypesByName.ContainsKey(basicType.Name))
                        TypesByName.Add(basicType.FullName, new ReportType() { Name = basicType.FullName, UseToString = true });
                }
            }
        }

        public XElement GenerateReport(string elementName, object value)
        {
            return this.GenerateReport(elementName, value, null, 100);
        }

        XElement GenerateReport(string elementName, object value, ReportType forcedReportType, int maxDepth)
        {
            maxDepth -= 1;

            if (value == null)
                return null;

            Type type = value.GetType();
            ReportType reportType = forcedReportType;

            if (reportType == null)
                TypesByName.TryGetValue(type.FullName, out reportType);
            if (reportType == null)
                TypesByName.TryGetValue(type.Name, out reportType);

            if (reportType == null && type.IsEnum)
                reportType = UseToStringReportType;

            if (reportType == null)
            {
                // Check for MatchChildClasses
                for (Type baseClass = type.BaseType; baseClass != null; baseClass = baseClass.BaseType)
                {
                    if (TypesByName.TryGetValue(baseClass.FullName, out reportType))
                    {
                        if (!reportType.MatchChildClasses)
                            reportType = null;
                    }
                    if (reportType != null)
                        break;

                    if (TypesByName.TryGetValue(baseClass.Name, out reportType))
                    {
                        if (!reportType.MatchChildClasses)
                            reportType = null;
                    }
                    if (reportType != null)
                        break;
                }
            }

            if (reportType == null)
                throw new NotSupportedException("Missing definition for data type " + type.FullName);

            if (reportType.UseToString)
            {
                string str = value.ToString();
                if (string.IsNullOrEmpty(str))
                    return null;
                return new XElement(elementName, str);
            }

            XElement resultElement = new XElement(elementName);

            if (maxDepth <= 0)
            {
                resultElement.Add(new XElement("ERROR: Maximum recursion reached"));
            }
            else
            {
                foreach (ReportBaseProperty reportProperty in reportType.Properties)
                {
                    string[] accessors = reportProperty.Name.Split('.');

                    if (accessors.Length == 0)
                        throw new InvalidOperationException("Invalid property name \"" + (reportProperty.Name ?? "(null)") + "\"");

                    object currentValue = value;

                    for (int i = 0; i < accessors.Length; ++i)
                    {
                        string accessor = accessors[i];
                        PropertyInfo propertyInfo = currentValue.GetType().GetProperty(accessor,
                            BindingFlags.Instance | BindingFlags.Public);
                        if (propertyInfo == null)
                            throw new InvalidOperationException(type.FullName + " does not define a property " + accessor);

                        currentValue = propertyInfo.GetValue(currentValue, new object[0]);

                        if (currentValue == null)
                            continue;
                    }

                    XObject propertyXml = GenerateReportProperty(reportProperty, currentValue, maxDepth);

                    if (propertyXml != null)
                        resultElement.Add(propertyXml);
                }
            }


            return resultElement;
        }

        XObject GenerateReportProperty(ReportBaseProperty reportBaseProperty, object propertyValue, int maxDepth)
        {
            if (propertyValue == null)
                return null;

            if (reportBaseProperty is ReportCollectionProperty)
            {
                // -------------------
                // Item collection
                ReportCollectionProperty reportProperty = (ReportCollectionProperty)reportBaseProperty;

                ReportElementProperty reportItemProperty = reportProperty.ItemProperty as ReportElementProperty;
                if (reportItemProperty == null)
                    throw new InvalidOperationException("Missing item property for " + reportBaseProperty.Name);

                List<XElement> itemElements = new List<XElement>();

                foreach (object item in (System.Collections.IEnumerable)propertyValue)
                {
                    XElement itemElement = (XElement)GenerateReportProperty(reportItemProperty, item,
                        maxDepth);
                    if (itemElement != null)
                        itemElements.Add(itemElement);
                }

                if (itemElements.Count == 0)
                    return null;

                if (!string.IsNullOrEmpty(reportProperty.OrderBy))
                {
                    string[] attributeNames = reportProperty.OrderBy.Split(',');

                    IEnumerable<XElement> enumeration = itemElements;

                    // Build up the sorting criteria
                    foreach (string attributeName in attributeNames.Reverse())
                    {
                        enumeration = enumeration.OrderBy(
                            delegate(XElement element)
                            {
                                XAttribute attribute = element.Attribute(attributeName);
                                if (attribute == null)
                                    return null;
                                return attribute.Value;
                            }
                        );
                    }

                    // Perform the sort
                    itemElements = enumeration.ToList();
                }

                return new XElement(reportProperty.Name, itemElements.ToArray());
            }

            ReportType forcedReportType = reportBaseProperty.UseToString
                ? UseToStringReportType : null;

            if (reportBaseProperty is ReportAttributeProperty)
            {
                // -------------------
                // Attribute
                ReportAttributeProperty reportProperty = (ReportAttributeProperty)reportBaseProperty;

                XElement propertyElement = GenerateReport(reportBaseProperty.Name, propertyValue, forcedReportType,
                    maxDepth);
                if (propertyElement == null)
                    return null;

                if (propertyElement.HasElements || propertyElement.HasAttributes)
                    throw new InvalidOperationException("The type " + propertyValue.GetType().FullName
                        + " cannot be used as an attribute");

                return new XAttribute(reportProperty.Name, propertyElement.Value);
            }

            if (reportBaseProperty is ReportElementProperty)
            {
                // -------------------
                // Element
                ReportElementProperty reportProperty = (ReportElementProperty)reportBaseProperty;
                if (reportProperty.AsEmbeddedXml)
                {
                    // Embedded XML
                    if (propertyValue != null && !(propertyValue is string))
                        throw new InvalidOperationException("AsEmbeddedXml used with a non-string value");

                    XElement parsed = null;
                    string propertyXml = (string)propertyValue;
                    if (propertyXml == null || propertyXml.Trim().Length == 0)
                        return null;

                    try
                    {
                        parsed = XElement.Load(new StringReader((string)propertyValue));
                    }
                    catch (Exception ex)
                    {
                        parsed = new XElement("Error", ex.Message);
                    }
                    return new XElement(reportProperty.Name, parsed, new XAttribute("EmbeddedXml", true));
                }

                // Regular element
                XElement propertyElement = GenerateReport(reportBaseProperty.Name, propertyValue, forcedReportType,
                    maxDepth);
                if (propertyElement == null)
                    return null;
                return propertyElement;
            }

            throw new InvalidOperationException();
        }


    }
}