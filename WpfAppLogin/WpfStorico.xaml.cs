//Del Pup Nicola

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
using System.Configuration;

namespace WpfAppLogin
{
    /// <summary>
    /// Logica di interazione per WpfStorico.xaml
    /// </summary>
    public partial class WpfStorico : Window
    {
        public WpfStorico()
        {
            InitializeComponent();
            CentraFinestra();
            this.SizeToContent = SizeToContent.WidthAndHeight;
            PopolaStorico();
        }

        private void PopolaStorico()
        {
            SqlConnection conn = new SqlConnection(@"Server = tcp:serverallarmi.database.windows.net,1433; Initial Catalog = dballarmi; Persist Security Info = False; User ID =cisco; Password =VMware1!; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT Id, IdSensore, OraScatto, OraDisatt, IdOperatore FROM StoricoAllarmi WHERE OraScatto > DateADD(minute, -10, @oraNow) ORDER BY OraScatto DESC", conn);
            sc.Parameters.AddWithValue("@oraNow", DateTime.Now);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("IdSensore", typeof(string));
            dt.Columns.Add("OraScatto", typeof(string));
            dt.Columns.Add("OraDisatt", typeof(string));
            dt.Columns.Add("IdOperatore", typeof(string));
            dt.Load(reader);


            GridStorico.ItemsSource = dt.AsDataView();
            GridStorico.DisplayMemberPath = "nome";
            conn.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //bottone Aggiorna, nuova query al db
        private void BtnAggiorna_Click(object sender, RoutedEventArgs e)
        {
            PopolaStorico();
        }

        //bottone Esci
        private void BtnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //centra la finestra all'avvio
        private void CentraFinestra()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}//Del Pup Nicola

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
using System.Configuration;

namespace WpfAppLogin
{
    /// <summary>
    /// Logica di interazione per WpfStorico.xaml
    /// </summary>
    public partial class WpfStorico : Window
    {
        public WpfStorico()
        {
            InitializeComponent();
            CentraFinestra();
            this.SizeToContent = SizeToContent.WidthAndHeight;
            PopolaStorico();
        }

        private void PopolaStorico()
        {
            SqlConnection conn = new SqlConnection(@"Server = tcp:serverallarmi.database.windows.net,1433; Initial Catalog = dballarmi; Persist Security Info = False; User ID =cisco; Password =VMware1!; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT Id, IdSensore, OraScatto, OraDisatt, IdOperatore FROM StoricoAllarmi WHERE OraScatto > DateADD(minute, -10, @oraNow) ORDER BY OraScatto DESC", conn);
            sc.Parameters.AddWithValue("@oraNow", DateTime.Now);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("IdSensore", typeof(string));
            dt.Columns.Add("OraScatto", typeof(string));
            dt.Columns.Add("OraDisatt", typeof(string));
            dt.Columns.Add("IdOperatore", typeof(string));
            dt.Load(reader);


            GridStorico.ItemsSource = dt.AsDataView();
            GridStorico.DisplayMemberPath = "nome";
            conn.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //bottone Aggiorna, nuova query al db
        private void BtnAggiorna_Click(object sender, RoutedEventArgs e)
        {
            PopolaStorico();
        }

        //bottone Esci
        private void BtnEsci_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //centra la finestra all'avvio
        private void CentraFinestra()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}