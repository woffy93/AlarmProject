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
    /// homepage con mappa con i vari segnalini-casa sulla mappa, tabella di storico allarmi e bottone per aprire la gestione
    /// del singolo luogo.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            String nome = "piero";
            InitializeComponent();
            LblLogin.Content = "Benvenuto, " + nome ;  // aggiungere nome utente
            Load_page();


        }
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();    //dashboard con mappa
            login.Show();
            this.Close();
        }


        private void Load_page()
        {
            SqlConnection conn = new SqlConnection(@"Server = tcp:serverallarmi.database.windows.net,1433;
                                                    Initial Catalog = dballarmi; 
                                                    Persist Security Info = False;
                                                    User ID =cisco; Password =VMware1!; 
                                                    MultipleActiveResultSets = False;
                                                    Encrypt = True; TrustServerCertificate = False; 
                                                    Connection Timeout = 30;");
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = CbxElenco.SelectedValue.ToString();
            
        }

        private void CbxElenco_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);

            switch(CbxElenco.SelectedIndex+1)
            {
                case 1: PinCasa1.Background = new SolidColorBrush(Color.FromRgb(255, 191 ,0));
                        PinFiera.Background = new SolidColorBrush(Color.FromRgb(0,128, 0));
                        PinOspedale.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinSME.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinSavio.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                    break;
                case 2: PinSME.Background = new SolidColorBrush(Color.FromRgb(255, 191, 0));
                        PinCasa1.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinFiera.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinOspedale.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinSavio.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                    break;
                case 3: PinSavio.Background = new SolidColorBrush(Color.FromRgb(255, 191, 0));
                        PinCasa1.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinOspedale.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinFiera.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinSME.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                    break;
                case 4: PinOspedale.Background = new SolidColorBrush(Color.FromRgb(255, 191, 0));
                        PinCasa1.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinSME.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinSavio.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinFiera.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                    break;
                case 5: PinFiera.Background = new SolidColorBrush(Color.FromRgb(255, 191, 0));
                        PinCasa1.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinOspedale.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinSavio.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        PinSME.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                    break;
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int Id= CbxElenco.SelectedIndex + 1;//id del Luogo in allarme
            //CentraleOperativa.Show();

        }
    }






}
