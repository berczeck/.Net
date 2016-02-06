using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApiOauthAuth.Seguridad
{
    public sealed class Usuario : IdentityUser// IUser
    {

        public Usuario()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}