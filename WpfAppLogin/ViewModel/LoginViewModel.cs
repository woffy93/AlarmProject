using System;
using System.Data;
using System.Data.SqlClient;
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
                //await Task.Delay(5000);
                var email = this.Email;
                var pass = (parameter as IHavePassword).SecurePassword.Unsecure();

                SqlConnection sqlcon = new SqlConnection(@"Server = tcp:serverallarmi.database.windows.net,1433; Initial Catalog = dballarmi; Persist Security Info = False; User ID =cisco; Password =VMware1!; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
                try
                {
                    if (sqlcon.State == ConnectionState.Closed)
                        await sqlcon.OpenAsync();

                    String query = "SELECT count (1) from AnaOperatori Where Login=@Login and Password=@Password";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@Login", email);
                    sqlcmd.Parameters.AddWithValue("@Password", pass);
                    int count = Convert.ToInt32(sqlcmd.ExecuteScalar());


                    if (count == 1)
                    {
                        ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = ApplicationPage.Map;
                    }
                    else
                    {
                        MessageBox.Show("User or Password Incorrect!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlcon.Close();
                }
                //((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = ApplicationPage.Map;

            
            });
        }
    }
}



