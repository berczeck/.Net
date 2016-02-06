using Microsoft.AspNet.Identity;

namespace ApiOauthAuth.Seguridad
{
    public class ApplicationUserManager : UserManager<Usuario>
    {
        public ApplicationUserManager(IUserStore<Usuario> store) : base(store)
        {
        }

    }
}