//Wofford Francesco

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

namespace WPFTTT
{
    /// <summary>
    /// Logica di interazione per CentarleOperativa.xaml
    /// </summary>
    public partial class CentarleOperativa : Window
    {
        public CentarleOperativa()
        {
            InitializeComponent();
        }

        private void ConnectDb()
        {
            SqlConnection sqlcon = new SqlConnection(@"Server = tcp:serverallarmi.database.windows.net,1433; Initial Catalog = dballarmi; Persist Security Info = False; User ID =cisco; Password =VMware1!; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();

                SqlCommand query = new SqlCommand("SELECT Top 5  Id, IdSensore, OraScatto, OraDisatt, IdOperatore from StoricoAllarmi", sqlcon);
                SqlDataReader reader;
                reader = query.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("@Id", typeof(string));
                dt.Columns.Add("@IdSensore", typeof(string));
                dt.Columns.Add("@OraScatto", typeof(string));
                dt.Columns.Add("@OraDisatt", typeof(string));
                dt.Columns.Add("@IdOperatore", typeof(string));
                dt.Load(reader);

                foreach (DataRow row in dt.Rows)
                {
                    robeValori a = new robeValori(row["id"].ToString(), row["idSensore"].ToString(), row["OraScatto"].ToString(), row["OraDisatt"].ToString(), row["IdOperatore"].ToString());
                }
            }
            finally
            {
                sqlcon.Close();
            }
            
        }



        private void Btn_Fire_On_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Fire_Off_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Gas_On_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Gas_Off_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Intrusion_On_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Intrusion_Off_Click(object sender, RoutedEventArgs e)
        {

        }
    }
        public class robeValori
            {
                public string Id { get; set; }
                public string IdSensore { get; set; }
                public string OraScatto { get; set; }
                public string OraDisatt { get; set; }
                public string IdOperatore { get; set; }

            public robeValori (string id, string IdSensore, string OraScatto, string OraDisatt, string IdOperatore)
            {
                this.Id = Id;
                this.IdSensore = IdSensore;
                this.OraScatto = OraScatto;
                this.OraDisatt = OraDisatt;
                this.IdOperatore = IdOperatore;
            }

    }
}
