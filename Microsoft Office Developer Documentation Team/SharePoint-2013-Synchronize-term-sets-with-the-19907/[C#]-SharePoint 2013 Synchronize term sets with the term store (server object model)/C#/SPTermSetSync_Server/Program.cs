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

namespace TermSetSync
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
        // The LCID is needed because methods such as TermSet.CreateTerm()
        // require an LCID input.
        static private int lcid;

        static void Main(string[] args)
        {
            Log("\r\nTermSetSync (Server OM): Starting...\r\n");

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

                SyncTermSet(termStore, termSetGoal);
            }
#endif

            Log("\r\nThe operation completed successfully.");

            if (Debugger.IsAttached)
                Debugger.Break();
        }

        // This function parses the XML input file and returns a tree
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

        // Create the TermSetGoal objects in the specified TermStore object.
        static void SyncTermSet(TermStore termStore, TermSetGoal termSetGoal)
        {
            Group group = termStore.Groups.FirstOrDefault(g => g.Name == termSetGoal.GroupName);
            if (group == null)
            {
                Log("* Creating missing group \"" + termSetGoal.GroupName + "\"");
                group = termStore.CreateGroup(termSetGoal.GroupName);
            }

            TermSet termSet = termStore.GetTermSet(termSetGoal.Id);
            if (termSet == null)
            {
                Log("* Creating missing term set \"" + termSetGoal.Name + "\"");
                termSet = group.CreateTermSet(termSetGoal.Name, termSetGoal.Id, lcid);
            }
            else
            {
                if (termSet.Group.Id != group.Id)
                {
                    Log("* Moving term set to group \"" + group.Name + "\"");
                    termSet.Move(group);
                }

                if (termSet.Name != termSetGoal.Name)
                    termSet.Name = termSetGoal.Name;
            }

            termStore.CommitAll();

            // Load the tree of terms as a flat list.
            Dictionary<Guid, Term> termsById = new Dictionary<Guid, Term>();
            foreach (Term term in termSet.GetAllTerms())
                termsById.Add(term.Id, term);

            Log("Step 1: Adds and Moves");
            ProcessAddsAndMoves(termSet, termSetGoal, termsById);

            Log("Step 2: Deletes");
            // Requery the TermSet object to reflect changes to the topology.
            termSet = termStore.GetTermSet(termSetGoal.Id);
            ProcessDeletes(termSet, termSetGoal); // Step 2

            Log("Step 3: Property Updates");
            termSet = termStore.GetTermSet(termSetGoal.Id);
            ProcessPropertyUpdates(termSet, termSetGoal); // Step 3

            termStore.CommitAll();
        }

        // For a Term object, if the intendedName is already being used by a sibling Term object,
        // then the following method generates a unique name to avoid a conflict.
        static string GenerateNameWithoutSiblingConflicts(TermSetItem itemParent, string name)
        {
            foreach (Term sibling in itemParent.Terms)
            {
                if (sibling.Name == name)
                {
                    string safeName = name + " " + Guid.NewGuid().ToString();
                    Log("Sibling conflict detected -- introducing temporary name \"" + safeName + "\"");
                    return safeName;
                }
            }
            return name; // The name is okay.
        }

        static void MoveToParentItem(Term term, TermSetItem newParentItem)
        {
            Log("* Moving Term \"" + term.Name + "\" to be a child of \"" + newParentItem.Name + "\"");

            // Avoid key violations that are introduced by the move.
            string safeName = GenerateNameWithoutSiblingConflicts(newParentItem, term.Name);
            if (term.Name != safeName)
                term.Name = safeName;

            if (newParentItem is TermSet)
                term.Move((TermSet)newParentItem);
            else
                term.Move((Term)newParentItem);

            term.TermStore.CommitAll();
        }

        // STEP 1: Add missing terms, and move existing terms to the correct
        // place in the tree, and possibly introducing temporary names
        // to avoid sibling conflicts.
        static void ProcessAddsAndMoves(TermSetItem parentItem, ItemGoal parentItemGoal,
          Dictionary<Guid, Term> termsById)
        {

            foreach (TermGoal termGoal in parentItemGoal.TermGoals)
            {
                // Does the term already exist?
                Term term;
                if (termsById.TryGetValue(termGoal.Id, out term))
                {
                    // The term exists, so ensure that it is a child of parentItem.
                    Guid termParentId = (term.Parent != null)
                      ? term.Parent.Id : term.TermSet.Id;

                    if (termParentId != parentItem.Id)
                    {
                        MoveToParentItem(term, parentItem);
                    }
                    else
                    {
                        Log("Verified position of term \"" + term.Name + "\"");
                    }
                }
                else
                {
                    Log("* Creating missing term \"" + termGoal.Name + "\"");
                    string safeName = GenerateNameWithoutSiblingConflicts(parentItem, termGoal.Name);
                    term = parentItem.CreateTerm(safeName, lcid, termGoal.Id);
                    parentItem.TermStore.CommitAll();

                    termsById.Add(term.Id, term);
                }

                ProcessAddsAndMoves(term, termGoal, termsById); // recurse
            }
        }

        // STEP 2: Delete any leftover terms.
        static void ProcessDeletes(TermSetItem parentItem, ItemGoal parentItemGoal)
        {
            foreach (Term term in parentItem.Terms)
            {
                TermGoal termGoal = parentItemGoal.TermGoals.FirstOrDefault(t => t.Id == term.Id);
                if (termGoal == null)
                {
                    Log("* Deleting extra term \"" + term.Name + "\"");
                    term.Delete();
                    term.TermStore.CommitAll();
                }
                else
                {
                    ProcessDeletes(term, termGoal);  // recurse
                }
            }
        }

        // STEP 3: Update the term properties, including correcting any
        // temporary names that were introduced in step 1.
        static void ProcessPropertyUpdates(TermSetItem parentItem, ItemGoal parentItemGoal)
        {
            foreach (Term term in parentItem.Terms)
            {
                TermGoal termGoal = parentItemGoal.TermGoals.FirstOrDefault(t => t.Id == term.Id);
                if (termGoal == null)
                    continue;  // This is a term that would have been deleted by the ProcessDeletes() method.

                // -----------------
                string goalName = TaxonomyItem.NormalizeName(termGoal.Name);
                if (term.Name != goalName)
                {
                    Log("* Renaming term from \"" + term.Name + "\" to \"" + termGoal.Name + "\"");
                    term.Name = goalName;
                }

                HashSet<string> labels = new HashSet<string>(
                    term.Labels.Where(l => !l.IsDefaultForLanguage).Select(l => l.Value));

                HashSet<string> labelsGoal = new HashSet<string>(termGoal.OtherLabels);

                // Delete any extra labels.
                foreach (string label in labels.Except(labelsGoal))
                {
                    Log("* Term \"" + term.Name + "\": Deleting label \"" + label + "\"");
                    term.Labels.First(l => l.Value == label).Delete();
                }

                // Add any missing labels.
                foreach (string label in labelsGoal.Except(labels))
                {
                    Log("* Term \"" + term.Name + "\": Adding label \"" + label + "\"");
                    term.CreateLabel(label, lcid, isDefault: false);
                }

                if (term.GetDescription() != termGoal.Description)
                {
                    Log("* Term \"" + term.Name + "\": Updating description");
                    term.SetDescription(termGoal.Description, lcid);
                }

                if (term.IsDeprecated != termGoal.IsDeprecated)
                {
                    Log("* Term \"" + term.Name + "\": Marking as "
                      + (termGoal.IsDeprecated ? "Deprecated" : "Not deprecated"));
                    term.Deprecate(termGoal.IsDeprecated);
                }
                // -----------------

                ProcessPropertyUpdates(term, termGoal); // Recurse.
            }
        }

        #region Utilities

        // This function uses the XmlSerializer object to write an example input file
        // that illustrates the XML syntax.
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


