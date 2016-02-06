using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace ApiOauthAuth.Seguridad
{
    public class RepositorioUsuario : IUserStore<Usuario>, IUserPasswordStore<Usuario>
    {
        private static Dictionary<string, Usuario> Usuarios = new Dictionary<string, Usuario>
        {
            {"1",new Usuario {UserName = "user1", Id = "1", PasswordHash = "123456"}},
            {"2",new Usuario {UserName = "user1", Id = "2", PasswordHash = "123456"}},
            {"3",new Usuario {UserName = "user1", Id = "3", PasswordHash = "123456"}}
        };

        public Task CreateAsync(Usuario user)
        {
            if (Usuarios.ContainsKey(user.Id))
            {
                throw new Exception("Usuario ya existe");
            }

            Usuarios.Add(user.Id, user);

            return Task.FromResult<Usuario>(null);
        }

        public Task DeleteAsync(Usuario user)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            List<Usuario> result = Usuarios.Where(x => x.Value.UserName.Equals(userName)).Select(x => x.Value).ToList();

            // Should I throw if > 1 user?
            if (result.Count == 1)
            {
                return Task.FromResult<Usuario>(result[0]);
            }

            return Task.FromResult<Usuario>(null);
        }

        public Task UpdateAsync(Usuario user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }


        public Task<string> GetPasswordHashAsync(Usuario user)
        {
            return Task.FromResult<string>(Usuarios[user.Id].PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Usuario user)
        {
            var hasPassword = !string.IsNullOrEmpty(Usuarios[user.Id].PasswordHash);

            return Task.FromResult<bool>(hasPassword);
        }

        public Task SetPasswordHashAsync(Usuario user, string passwordHash)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult<Object>(null);
        }
    }
}