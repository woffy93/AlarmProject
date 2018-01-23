using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin
{
    //An interface for a class that can provide a SecurePassword
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}
