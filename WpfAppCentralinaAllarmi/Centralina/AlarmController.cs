using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Data.SqlClient;
using System.Windows;
using System.Threading;

namespace WpfAppCentralinaAllarmi.Centralina
{
    /// <summary>
    ///  classe che rappresenta la centralina controllo allarmi, connessa ai sensori
    /// </summary>
    public class AlarmController
    {
        public AlarmController(int id) {
            idCentralina = id;
        }
        public int idCentralina; // = a IdCasa su tabella dbo.SensoriLuogo
        public GeoCoordinate mapCoordinates { get; set; }
        //sensore che mappa tiposensore a sensore
        public Dictionary<string, Sensors.AbstractSensor> sensors;

        public int getSensorId(string sensor)
        {
            return sensors[sensor].sensorId;
        }
        /// <summary>
        /// Attiva l'allarme nel sensore modificandone lo stato.
        /// Prima però verifica che il sensore sia attivo.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        public void setAlarmState(string sensor, bool state)
        {
            Centralina.Sensors.AbstractSensor s = sensors[sensor];
            string sensorType = "";
            foreach(KeyValuePair<string, Centralina.Sensors.AbstractSensor> kvp in sensors)
            {
                if(kvp.Value.sensorId.ToString() == sensor)
                {
                    sensorType = kvp.Key;
                    break;
                }
            }
            if (s.abilitato == true)
            {
                s.alarmState = state;
                this.allarm(s.sensorId, sensorType, DateTime.Now);
            }          
        }

        /// <summary>
        /// metodo per controllare lo stato di tutti i sensori, la centralina deve chiamarlo periodicamente
        /// nell'esercizo lo stato viene cambiato dalla centralina stessa e poi letto da questo metodo.
        /// nella realtà questo metodo andrebbe a leggere uno stato determinato da avvenimenti esterni.
        /// </summary>
        /// <returns> dizionario che associal ad ogni sensore il suo stato</returns>
        public Dictionary<string, bool> getAlarms()
        {
            return sensors.ToDictionary(s => s.Key, s => s.Value.alarmState);
        }

        public void allarm(int sensorID, string sensorType, DateTime time)
        {
            SqlConnection conn;
            conn = new SqlConnection(@"Server=tcp:serverallarmi.database.windows.net,1433;Initial Catalog=dballarmi;Persist Security Info=False;User ID=cisco;Password=VMware1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            conn.Open();
            string query = "";
            SqlCommand cmd = new SqlCommand(query, conn);

        }

        /// <summary>
        /// metodo chhiamato all'avvio per creare il dizionario dei sensori
        /// </summary>        
        public Dictionary<string, Centralina.Sensors.AbstractSensor> initSensors()
        {
            Centralina.Sensors.SensorFactory factory = new Centralina.Sensors.SensorFactory();
            SqlDataReader reader = checkSensorsOnDb();
            Dictionary<string, Centralina.Sensors.AbstractSensor> dict = new Dictionary<string, Sensors.AbstractSensor>();
            while (reader.Read())
            {
                dict.Add(
                    reader.GetString(reader.GetOrdinal("TipoSensore")),
                    factory.getSensor(
                        reader.GetString(reader.GetOrdinal("TipoSensore")),
                        reader.GetBoolean(reader.GetOrdinal("IsAbilitato")),
                        reader.GetInt32(reader.GetOrdinal("Id"))
                        )
                    );
            }
            return dict;
        }
        /// <summary>
        /// metodo che verifica l'attivazione dei sensori
        /// da chiamare periodicamente
        /// </summary>
        public void setActivation()
        {

        }

        private SqlDataReader checkSensorsOnDb()
        {
            try
            {
                SqlConnection conn;
                conn = new SqlConnection(@"Server=tcp:serverallarmi.database.windows.net,1433;Initial Catalog=dballarmi;Persist Security Info=False;User ID=cisco;Password=VMware1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();
                string query = 
                    "SELECT s.Id, t.TipoSensore, s.IsAbilitato FROM dbo.AnaSensori AS s JOIN dbo.TipiSensori AS t ON s.IdTipo = t.Id WHERE s.IdLuogo = 2;";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                return reader;
            }
            catch(Exception e)
            {
                MessageBox.Show("Errore nella connessione al Db, contattare l'amministratore" + e.Message);
                return null;
            }
        }
    }
}