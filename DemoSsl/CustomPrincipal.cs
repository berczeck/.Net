using System.Linq;
using System.Security.Principal;
using System.Threading;

namespace DemoSsl
{
    public class CustomPrincipal : IPrincipal
    {
        private IIdentity _identity;
        private string[] _roles;

        public CustomPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        // helper method for easy access (without casting) 
        public static CustomPrincipal Current
        {
            get { return Thread.CurrentPrincipal as CustomPrincipal; }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        // return all roles (custom property) 
        public string[] Roles
        {
            get
            {
                EnsureRoles();
                return _roles;
            }
        } // IPrincipal role check 

        public bool IsInRole(string role)
        {
            EnsureRoles();
            return _roles.Contains(role);
        } // cache roles for subsequent requests 

        protected virtual void EnsureRoles()
        {
            _roles = new[] {"Admin", "Test"};
        }
    }
}