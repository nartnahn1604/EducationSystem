using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_UIT.PasswordSecure
{
    internal interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }
}
