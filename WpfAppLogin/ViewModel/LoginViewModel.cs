using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppLogin.ViewModel;
using WpfAppLogin.ViewModel.Base;

namespace WpfAppLogin
{
    public class LoginViewModel : BaseViewModel
    {
        public string Email { get; set; }
        

        public ICommand LoginCommand { get; set; }

        //Constructor
        public LoginViewModel()
        {
            
            // Create commands
            LoginCommand = new RelayParametrizedCommand(async (parameter) => await Login(parameter));
            

        }

       public async Task Login(object parameter)
        {
            await Task.Delay(500);
        }

    }


}

