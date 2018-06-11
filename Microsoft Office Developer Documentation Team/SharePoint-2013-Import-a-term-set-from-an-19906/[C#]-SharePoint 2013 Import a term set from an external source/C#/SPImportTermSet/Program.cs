using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Taxonomy;

namespace TermSetImport
{

    #region Helper Classes
    [Serializable]
    [DebuggerDisplay("{DebugText}")]
    public abstract class ItemGoal
    {
        [XmlAttribute]
        public Guid Id;

        [XmlAttribute]
        public string Name;

        [XmlElement("Term")]
        public List<TermGoal> TermGoals = new List<TermGoal>();

        internal string DebugText { get { return this.Name + " (" + this.GetType() + " " + this.Id + ")"; } }
    }

    [XmlType("TermSet")]
    public class TermSetGoal : ItemGoal
    {
        [XmlAttribute]
        public string GroupName;
    }

    [XmlType("Term")]
    public class TermGoal : ItemGoal
    {
        [XmlAttribute, DefaultValue(false)]
        public bool IsDeprecated = false;

        [XmlElement, DefaultValue("")]
        public string Description = "";

        [XmlElement("OtherLabel")]
        public List<string> OtherLabels = new List<string>();
    }
    #endregion

    class Program
    {
        // The URL of the SharePoint SPSite object to connect to
        const string siteUrl = "http://localhost/";

        // Stores the locale identifier (LCID) for the term store's default working language.
        // The LCID is needed because certain methods such as TermSet.CreateTerm()
        // require an LCID input.
        static private int lcid;

        static void Main(string[] args)
        {
            Log("\r\nTermSetImport (Server OM): Starting...\r\n");

            // Uncomment this section to create a sample input file.
#if false
      GenerateExampleXmlFile();
#else
            TermSetGoal termSetGoal = LoadTermSetGoal();

            using (SPSite site = new SPSite(siteUrl))
            {
                TaxonomySession taxonomySession = new TaxonomySession(site, updateCache: true);
                TermStore termStore = taxonomySession.DefaultSiteCollectionTermStore;

                Program.lcid = termStore.WorkingLanguage;

                ImportTermSet(termStore, termSetGoal);
            }
#endif

            Log("\r\nThe operation completed successfully.");

            if (Debugger.IsAttached)
                Debugger.Break();
        }

        // Parses the XML input file and returns a tree
        // of TermSetGoal and TermGoal objects.
        static TermSetGoal LoadTermSetGoal()
        {
            Log("Reading ExternalTaxonomyData.xml\r\n");
            XmlSerializer serializer = new XmlSerializer(typeof(TermSetGoal));
            TermSetGoal termSetGoal;
            using (Stream stream = new FileStream("ExternalTaxonomyData.xml", FileMode.Open))
            {
                termSetGoal = (TermSetGoal)serializer.Deserialize(stream);
            }

            HashSet<Guid> ids = new HashSet<Guid>();

            foreach (ItemGoal itemGoal in EnumerateTreeDepthFirst((ItemGoal)termSetGoal,
                g => g.TermGoals.Cast<ItemGoal>()))
            {
                if (ids.Contains(itemGoal.Id))
                    throw new InvalidOperationException("The ID " + itemGoal.Id + " is used more than once");
                ids.Add(itemGoal.Id);
            }

            return termSetGoal;
        }

        // Create the goal objects in the specified term store
        static void ImportTermSet(TermStore termStore, TermSetGoal termSetGoal)
        {
            Log("Setting up term set \"" + termSetGoal.Name + "\"");
            Group group = termStore.Groups.FirstOrDefault(g => g.Name == termSetGoal.GroupName);
            if (group == null)
            {
                Log("Creating missing group \"" + termSetGoal.GroupName + "\"");
                group = termStore.CreateGroup(termSetGoal.GroupName);
            }

            TermSet termSet = termStore.GetTermSet(termSetGoal.Id);
            if (termSet != null)
            {
                Log("Deleting existing term set \"" + termSetGoal.Name + "\"");
                termSet.Delete();
                termStore.CommitAll();
            }

            Log("Creating new term set \"" + termSetGoal.Name + "\"");
            termSet = group.CreateTermSet(termSetGoal.Name, termSetGoal.Id);
            termStore.CommitAll();

            ImportTerms(termSet, termSetGoal);

            termStore.CommitAll();
        }

        // This is a recursive function that creates a Term object
        // corresponding to each TermGoal object read from the input file.
        static void ImportTerms(TermSetItem parentItem, ItemGoal parentItemGoal)
        {
            foreach (TermGoal termGoal in parentItemGoal.TermGoals)
            {

                Log("Creating term \"" + termGoal.Name + "\"");
                Term term = parentItem.CreateTerm(termGoal.Name, lcid, termGoal.Id);

                term.Name = termGoal.Name;

                foreach (string label in termGoal.OtherLabels)
                    term.CreateLabel(label, lcid, isDefault: false);

                term.SetDescription(termGoal.Description, lcid);

                if (termGoal.IsDeprecated)
                    term.Deprecate(true);

                ImportTerms(term, termGoal); // recurse
            }
        }

        #region Utilities

        // This function uses the XmlSerializer to write an example input file
        // illustrating the XML syntax.
        static void GenerateExampleXmlFile()
        {
            TermSetGoal termSet = new TermSetGoal();
            termSet.Name = "TermSet";
            var parentTerm = new TermGoal() { Name = "Term1" };
            termSet.TermGoals.Add(parentTerm);
            parentTerm.TermGoals.Add(new TermGoal() { Name = "Term2", OtherLabels = new List<string>(new[] { "A", "B", "C" }) });

            XmlSerializer serializer = new XmlSerializer(typeof(TermSetGoal));
            using (Stream stream = new FileStream("ExampleInput.xml", FileMode.Create))
            {
                serializer.Serialize(stream, termSet);
            }
        }

        static void Log(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

        // Given a root node and a parent-child relationship defined by the enumerateChildren delegate,
        // this function returns an enumerator that visits each node in a depth-first preorder traversal.
        static public IEnumerable<T> EnumerateTreeDepthFirst<T>(T root, Func<T, IEnumerable<T>> enumerateChildren)
        {
            // Because of "yield return", an explicit stack is more efficient here than recursion.
            Stack<IEnumerator<T>> stack = new Stack<IEnumerator<T>>();

            yield return root;
            IEnumerator<T> enumerator = enumerateChildren(root).GetEnumerator();

            for (; ; )
            {
                if (enumerator.MoveNext())
                {
                    T node = enumerator.Current;
                    yield return node;
                    stack.Push(enumerator);
                    enumerator = enumerateChildren(node).GetEnumerator();
                }
                else
                {
                    if (stack == null || stack.Count == 0)
                        break;
                    enumerator = stack.Pop();
                }
            }
        }
        #endregion
    }
}


