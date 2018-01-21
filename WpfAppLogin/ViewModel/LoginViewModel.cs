using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppLogin.ViewModel;

namespace WpfAppLogin
{
    public class LoginViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public bool LoginIsRunning { get; set; } //flag indicating if the login command is running

        public ICommand LoginCommand { get; set; }

        // Constructor
        public LoginViewModel()
        {
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await Login(parameter));
        }


        public async Task Login(object parameter)
        {
            await RunCommand(() => this.LoginIsRunning, async () =>
            {
                await Task.Delay(5000);
                var email = this.Email;
                var pass = (parameter as IHavePassword).SecurePassword.Unsecure();

                //((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = ApplicationPage.Map;

            
            });
        }
    }
}



