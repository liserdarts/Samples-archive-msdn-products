using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Security;
using System.Security.Policy;

namespace ContosoTours.Setup.Action
{
    [RunInstaller(true)]
    public partial class InstallAction : Installer
    {
        private static string _path = "path";
        private static string _name = "name";
        private static string _fullTrust = "FullTrust";
        private static string _user = "User";

        public InstallAction()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            foreach (string assembly in Directory.GetFiles(Context.Parameters[_path], "*.dll"))
            {
                SetupPolicy(assembly, Context.Parameters[_name]);
            }
        }

        public override void Uninstall(IDictionary savedState)
        {
            foreach (string assembly in Directory.GetFiles(Context.Parameters[_path], "*.dll"))
            {
                RemovePolicy(assembly, Context.Parameters[_name]);
            }

            base.Uninstall(savedState);
        }

        private static void SetupPolicy(string path, string name)
        {
            //Get a reference to the User level "All Code" group.
            PolicyLevel polLevel = GetPolicy(_user);

            if (polLevel != null)
            {
                UnionCodeGroup allCodeCG =
                    (UnionCodeGroup) polLevel.RootCodeGroup;

                //Create a new code group with the FullTrust permission
                //set and a URL as evidence of membership.
                PermissionSet permSet = polLevel.GetNamedPermissionSet(_fullTrust);
                UrlMembershipCondition urlMemCond = new UrlMembershipCondition(path);
                UnionCodeGroup cg = 
                    new UnionCodeGroup(urlMemCond, new PolicyStatement(permSet));
                cg.Name = name;
                allCodeCG.AddChild(cg);

                //Save the policy
                SecurityManager.SavePolicy();
            }
        }

        private static void RemovePolicy(string path, string name)
        {
            //Get a reference to the User level "All Code" group.
            PolicyLevel polLevel = GetPolicy(_user);
            
            if(polLevel != null)
            {
                UnionCodeGroup allCodeCG =
                    (UnionCodeGroup)polLevel.RootCodeGroup;
                
                //Determine the resolved membership condition for the 
                //specified path.
                string resolvedPath = 
                    new UrlMembershipCondition(path).ToString();

                //Locate a code group with the same name, the same resolved 
                //path, and FullTrust; if found, remove the child group.
                IEnumerator cgs = allCodeCG.Children.GetEnumerator();
                while (cgs.MoveNext())
                {
                    CodeGroup cg = (CodeGroup)cgs.Current;
                    if(cg.Name == name)
                    {
                        if(cg.MembershipCondition.ToString() == resolvedPath &&
                            allCodeCG.PermissionSetName == _fullTrust)
                        {
                            allCodeCG.RemoveChild(cg);
                            SecurityManager.SavePolicy();
                            break;
                        }
                    }
                }
            }
        }

        private static PolicyLevel GetPolicy(string policy)
        {
            PolicyLevel polLevel = null;
            IEnumerator polLevels = SecurityManager.PolicyHierarchy();

            while (polLevels.MoveNext())
            {
                if (((PolicyLevel)polLevels.Current).Label == policy)
                {
                    polLevel = (PolicyLevel)polLevels.Current;
                }
            }
            return polLevel;
        }
    }
}