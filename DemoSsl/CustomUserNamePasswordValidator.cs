using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace DemoSsl
{
    public class CustomUserNamePasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (!userName.Equals("admin"))
            {
                throw new FaultException("Usuario incorrecto");
            }
        }
    }
}