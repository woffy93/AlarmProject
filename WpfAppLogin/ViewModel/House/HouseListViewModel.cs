using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin
{
    public class HouseListViewModel
    {
        public static HouseListViewModel Instance => new HouseListViewModel();
        public List<HouseItemViewModel> Houses { get; set; } = new List<HouseItemViewModel>();

        public HouseListViewModel()
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
            foreach (DataRow row in dt.Rows)
            {
                HouseItemViewModel a = new HouseItemViewModel { Name = row[0].ToString() };
                if (a != null)
                    Houses.Add(a);
            }
            conn.Close();


            //Houses.Add(new HouseItemViewModel { Name = "aa" });
            //Houses.Add(new HouseItemViewModel { Name = "bb" });
            //    Houses = new List<HouseItemViewModel>
            //    {
            //        new HouseItemViewModel
            //        {
            //            Name="aa"
            //        },
            //        new HouseItemViewModel
            //        {
            //            Name="bb"
            //        },
            //        new HouseItemViewModel
            //        {
            //            Name="bb"
            //        },
            //        new HouseItemViewModel
            //        {
            //            Name="bb"
            //        },

            //    };
        }

    }
}
