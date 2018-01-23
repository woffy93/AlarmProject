//Boz Federico, Del pup Nicola

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTTT;

namespace WPFTTT
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LblLogin.Content = "Benvenuto, ";  // aggiungere nome utente
            Load_CbxElenco();
        }
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();    //dashboard con mappa
            login.Show();
            this.Close();
        }

        private void Load_CbxElenco()
        {
            CbxElenco.Text = "-- Seleziona Edificio --";
            SqlConnection conn = new SqlConnection(@"Server = tcp:serverallarmi.database.windows.net,1433; Initial Catalog = dballarmi; Persist Security Info = False; User ID =cisco; Password =VMware1!; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT nome, id FROM Luoghi order by Id", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("nome", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Load(reader);


            CbxElenco.ItemsSource = dt.AsDataView();
            CbxElenco.DisplayMemberPath = "nome";
            conn.Close();

        }
        private void CbxElenco_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = CbxElenco.SelectedValue.ToString();
        }
        private void CbxElenco_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);

        }

    }






}
