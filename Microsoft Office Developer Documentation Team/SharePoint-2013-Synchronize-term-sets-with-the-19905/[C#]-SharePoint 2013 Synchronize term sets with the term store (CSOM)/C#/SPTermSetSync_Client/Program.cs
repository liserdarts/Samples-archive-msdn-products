using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;

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

    public class Wrapper
    {
        public readonly TermSetItem Item;
        public readonly Guid Id;
        public Guid ParentId;
        public string Name;
        public List<Wrapper> ChildTerms = new List<Wrapper>();
        public ClientResult<string> TermDescription = null;

        public Wrapper(TermSetItem item, Guid id, Guid parentId, string name)
        {
            Item = item;
            Id = id;
            ParentId = parentId;
            Name = name;
        }
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

        // For each Term.Id GUID, the following table returns a Wrapper object that encapsulates
        // a Term object.  This enables the terms to be fetched as a flat list
        // by using the TermSet.GetAllTerms() method, and it provides a way to track additional state
        // for each object (e.g., the Wrapper.TermDescription client result placeholders).
        static private Dictionary<Guid, Wrapper> itemsById;

        // The context object for the client object model session.
        static private ClientContext clientContext;

        // A list of Term property expressions to be used with the ClientContext.Load() method.
        // Without this list, we would need to copy and paste this code to each place where
        // a term is loaded.
        static Expression<Func<Term, object>>[] termRetrievals = new Expression<Func<Term, object>>[] {
      termArg => termArg.Id,
      termArg => termArg.Name,
      termArg => termArg.Parent.Id,
      termArg => termArg.IsDeprecated,
      termArg => termArg.Labels.Include(
        labelArg => labelArg.IsDefaultForLanguage,
        labelArg => labelArg.Value
      )
    };

        static void Main(string[] args)
        {
            Log("\r\nTermSetSync (Client OM): Starting...\r\n");

            // Uncomment this section to create a sample input file.
#if false
      GenerateExampleXmlFile();
#else
            TermSetGoal termSetGoal = LoadTermSetGoal();

            Program.clientContext = new ClientContext(siteUrl);

            TaxonomySession taxonomySession = TaxonomySession.GetTaxonomySession(Program.clientContext);
            taxonomySession.UpdateCache();
            TermStore termStore = taxonomySession.GetDefaultSiteCollectionTermStore();

            TermSet termSet = CreateTermSetAndGroup(termStore, termSetGoal);
            SyncTermSet(termSet, termSetGoal);
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

        // If Term and TermGroup objects do not already exist, this function creates them as
        // specified by the TermSetGoal object that was read from the XML
        // input file.  The code also calls the ClientContext.Load() method to query 
        // client object model properties that will be needed later.
        static TermSet CreateTermSetAndGroup(TermStore termStore, TermSetGoal termSetGoal)
        {
            // Load the data
            Program.clientContext.Load(termStore,
              termStoreArg => termStoreArg.WorkingLanguage,
              termStoreArg => termStoreArg.Groups.Include(
                groupArg => groupArg.Id,
                groupArg => groupArg.Name
              )
            );

            TermSet termSet = termStore.GetTermSet(termSetGoal.Id);
            Program.clientContext.Load(termSet,
              termSetArg => termSetArg.Id,
              termSetArg => termSetArg.Name,
              termSetArg => termSetArg.Group.Id);

            Program.ExecuteQuery(); // WCF CALL #1

            // Set up the group and the term set.
            Program.lcid = termStore.WorkingLanguage;

            TermGroup group = termStore.Groups.ToList().FirstOrDefault(g => g.Name == termSetGoal.GroupName);
            if (group == null)
            { // (ServerObjectIsNull is not used here)
                Log("* Creating missing group \"" + termSetGoal.GroupName + "\"");
                group = termStore.CreateGroup(termSetGoal.GroupName, Guid.NewGuid());
            }

            if (termSet.ServerObjectIsNull.Value)
            {
                Log("* Creating missing term set \"" + termSetGoal.Name + "\"");
                termSet = group.CreateTermSet(termSetGoal.Name, termSetGoal.Id, Program.lcid);
                Program.clientContext.Load(termSet,
                  termSetArg => termSetArg.Id,
                  termSetArg => termSetArg.Name,
                  termSetArg => termSetArg.Group.Id);
            }


#if false
      else {
        if (termSet.Group.Id != group.Id) {
          Log("* Moving term set to group \"" + group.Name + "\"");
          termSet.Move(group);
        } else {
          Log("Verified term set \"" + termSetGoal.Name + "\"");
        }
        if (termSet.Name != termSetGoal.Name)
          termSet.Name = termSetGoal.Name;
      }
#endif

            return termSet;
        }

        // Update the objects in the Taxonomy TermSet to match the objects specified
        // in the TermSetGoal object that was read from the XML file.
        static void SyncTermSet(TermSet termSet, TermSetGoal termSetGoal)
        {
            // Load the tree of terms as a flat list.
            Log("Loading terms...");
            TermCollection allTerms = termSet.GetAllTerms();

            Program.clientContext.Load(allTerms,
              allTermsArg => allTermsArg.Include(Program.termRetrievals)
            );

            Program.ExecuteQuery(); // WCF CALL #2

            Program.itemsById = new Dictionary<Guid, Wrapper>();
            Wrapper termSetWrapper = new Wrapper(termSet, termSet.Id, Guid.Empty, termSet.Name);
            Program.itemsById.Add(termSetWrapper.Id, termSetWrapper);

            foreach (Term term in allTerms)
            {
                Guid parentItemId = termSet.Id;
                if (!term.Parent.ServerObjectIsNull.Value)
                    parentItemId = term.Parent.Id;
                Program.itemsById.Add(term.Id, new Wrapper(term, term.Id, parentItemId, term.Name));
            }

            foreach (Wrapper wrapper in Program.itemsById.Values)
            {
                if (wrapper != termSetWrapper)
                    Program.itemsById[wrapper.ParentId].ChildTerms.Add(wrapper);
            }

            Log("Step 1: Adds and Moves");
            ProcessAddsAndMoves(termSetWrapper, termSetGoal);

            Log("Step 2: Deletes");
            ProcessDeletes(termSetWrapper, termSetGoal); // Step 2

            Log("Step 3: Property Updates");

            Log("Querying auxiliary data");
            foreach (Wrapper wrapper in Program.itemsById.Values)
            {
                if (wrapper != termSetWrapper)
                    wrapper.TermDescription = ((Term)wrapper.Item).GetDescription(Program.lcid);
            }
            Program.ExecuteQuery(); // WCF CALL #3

            ProcessPropertyUpdates(termSetWrapper, termSetGoal); // Step 3
            Program.ExecuteQuery(); // WCF CALL #4
        }

        // This method adds a Wrapper object to the Program.itemsById table.
        // It locates the parent object by its GUID and then updates
        // its ChildTerms collection.
        //
        // Note: The root of the tree is the term set and therefore 
        // it does not have a parent, so this function does not add it.
        static void AddWrapperToTree(Wrapper wrapper)
        {
            Program.itemsById.Add(wrapper.Id, wrapper);
            Wrapper wrappedParent = Program.itemsById[wrapper.ParentId];
            wrappedParent.ChildTerms.Add(wrapper);
        }

        // For a Term object, if the intendedName is already in use by a sibling Term object,
        // then this method generates a unique name to avoid a conflict.
        static string GenerateNameWithoutSiblingConflicts(Wrapper parentWrapper, string intendedName)
        {
            foreach (Wrapper sibling in parentWrapper.ChildTerms)
            {
                if (sibling.Name == intendedName)
                {
                    string safeName = intendedName + " " + Guid.NewGuid().ToString();
                    Log("Sibling conflict detected -- introducing temporary name \"" + safeName + "\"");
                    return safeName;
                }
            }
            return intendedName; // The name is okay.
        }

        // STEP 1: Add missing terms, and move existing terms to the correct
        // place in the tree. Possibly introduce temporary names to avoid
        // sibling conflicts.
        static void ProcessAddsAndMoves(Wrapper parentWrapper, ItemGoal parentItemGoal)
        {
            Debug.Assert(parentWrapper.Id == parentItemGoal.Id);
            foreach (TermGoal termGoal in parentItemGoal.TermGoals)
            {
                // Does the term already exist?
                Wrapper wrapper;
                if (!Program.itemsById.TryGetValue(termGoal.Id, out wrapper))
                {
                    Log("* Creating missing term \"" + termGoal.Name + "\"");
                    string safeName = GenerateNameWithoutSiblingConflicts(parentWrapper, termGoal.Name);
                    Term newTerm = parentWrapper.Item.CreateTerm(safeName, Program.lcid, termGoal.Id);
                    wrapper = new Wrapper(newTerm, termGoal.Id, parentWrapper.Id, safeName);
                    AddWrapperToTree(wrapper);

                    Program.clientContext.Load(newTerm, Program.termRetrievals);
                }
                else
                {
                    // The term exists, so ensure that it is a child of parentItem.
                    if (wrapper.ParentId == parentWrapper.Id)
                    {
                        Log("Verified position of term \"" + wrapper.Name + "\"");
                    }
                    else
                    {
                        Log("* Moving Term \"" + wrapper.Name + "\" to be a child of \"" + parentWrapper.Name + "\"");

                        // Avoid key violations introduced by the move.
                        string safeName = GenerateNameWithoutSiblingConflicts(parentWrapper, termGoal.Name);
                        if (wrapper.Item.Name != safeName)
                            wrapper.Item.Name = safeName;

                        ((Term)wrapper.Item).Move(parentWrapper.Item);

                        // Update the data structure.
                        Wrapper oldParent = Program.itemsById[wrapper.ParentId];
                        oldParent.ChildTerms.Remove(wrapper);
                        wrapper.ParentId = parentWrapper.Id;
                        parentWrapper.ChildTerms.Add(wrapper);
                    }
                }

                ProcessAddsAndMoves(wrapper, termGoal); // Recurse.
            }
        }

        // STEP 2: Delete any leftover terms.
        static void ProcessDeletes(Wrapper parentWrapper, ItemGoal parentItemGoal)
        {
            foreach (Wrapper wrapper in parentWrapper.ChildTerms.ToList())
            {
                TermGoal termGoal = parentItemGoal.TermGoals.FirstOrDefault(t => t.Id == wrapper.Id);
                if (termGoal == null)
                {
                    Log("* Deleting extra term \"" + wrapper.Name + "\"");
                    wrapper.Item.DeleteObject();
                    parentWrapper.ChildTerms.Remove(wrapper);
                }
                else
                {
                    ProcessDeletes(wrapper, termGoal);  // Recurse.
                }
            }
        }

        // STEP 3: Update the term properties, including correcting any
        // temporary names that were introduced in step 1.
        static void ProcessPropertyUpdates(Wrapper parentWrapper, ItemGoal parentItemGoal)
        {
            foreach (Wrapper wrapper in parentWrapper.ChildTerms)
            {
                // Find the corresponding TermGoal object.
                TermGoal termGoal = parentItemGoal.TermGoals.First(t => t.Id == wrapper.Id);

                Term term = (Term)wrapper.Item;

                // -----------------
                if (term.Name != termGoal.Name)
                { // Consider the TaxonomyItem.NormalizeName() method.
                    Log("* Renaming term from \"" + term.Name + "\" to \"" + termGoal.Name + "\"");
                    term.Name = termGoal.Name;
                }

                HashSet<string> labels = new HashSet<string>(
                    term.Labels.ToList().Where(l => !l.IsDefaultForLanguage).Select(l => l.Value));

                HashSet<string> labelsGoal = new HashSet<string>(termGoal.OtherLabels);

                // Delete any extra labels.
                foreach (string label in labels.Except(labelsGoal))
                {
                    Log("* Term \"" + term.Name + "\": Deleting label \"" + label + "\"");
                    term.Labels.ToList().First(l => l.Value == label).DeleteObject();
                }

                // Add any missing labels.
                foreach (string label in labelsGoal.Except(labels))
                {
                    Log("* Term \"" + term.Name + "\": Adding label \"" + label + "\"");
                    term.CreateLabel(label, Program.lcid, isDefault: false);
                }

                if (wrapper.TermDescription.Value != termGoal.Description)
                {
                    Log("* Term \"" + term.Name + "\": Updating description");
                    term.SetDescription(termGoal.Description, Program.lcid);
                }

                if (term.IsDeprecated != termGoal.IsDeprecated)
                {
                    Log("* Term \"" + term.Name + "\": Marking as "
                      + (termGoal.IsDeprecated ? "Deprecated" : "Not deprecated"));
                    term.Deprecate(termGoal.IsDeprecated);
                }
                // -----------------

                ProcessPropertyUpdates(wrapper, termGoal); // recurse
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

        static void ExecuteQuery()
        {
            Log("--> ExecuteQuery()");
            Program.clientContext.ExecuteQuery();
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


