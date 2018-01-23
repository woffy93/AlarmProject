//Boz Federico, Del Pup Nicola

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFTTT;

namespace WPFTTT
{
    /// <summary>
    /// Logica di interazione per LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Server = tcp:serverallarmi.database.windows.net,1433; Initial Catalog = dballarmi; Persist Security Info = False; User ID =cisco; Password =VMware1!; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();

                String query = "SELECT count (1) from AnaOperatori Where Login=@Login and Password=@Password";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.Parameters.AddWithValue("@Login", txtUser.Text);
                sqlcmd.Parameters.AddWithValue("@Password", txtPwd.Password);
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());

                
                if (count == 1)
                {
                    MainWindow Map = new MainWindow();    //dashboard con mappa
                    Map.Show();
                    this.Close();
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
        }
    }
}
