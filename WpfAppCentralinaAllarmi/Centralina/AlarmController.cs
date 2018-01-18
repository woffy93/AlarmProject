using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Data.SqlClient;
using System.Windows;

namespace WpfAppCentralinaAllarmi.Centralina
{
    /// <summary>
    ///  classe che rappresenta la centralina controllo allarmi, connessa ai sensori
    /// </summary>
    public class AlarmController
    {
        public AlarmController(Dictionary<string, Sensors.AbstractSensor> s, int id) {
            idCentralina = id;
            sensors = s;
        }
        public int idCentralina; // = a IdCasa su tabella dbo.SensoriLuogo
        public GeoCoordinate mapCoordinates { get; set; }
        public Dictionary<string, Sensors.AbstractSensor> sensors;

        public int getSensorId(string sensor)
        {
            return sensors[sensor].sensorId;
        }

        public void setAlarmState(string sensor, bool state)
        {
            sensors[sensor].alarmState = state;
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
            //scrivere allarme su db remoto
            //MOCK
            Console.WriteLine("Allarme scattato");
        }

        public void initSensors()
        {
            SqlDataReader reader = checkSensorsOnDb();
            if (reader == null)
            {
                return;
            }
        }

        public void setActivation()
        {

        }

        private SqlDataReader checkSensorsOnDb()
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Server=tcp:serverallarmi.database.windows.net,1433;Initial Catalog=dballarmi;Persist Security Info=False;User ID=cisco;Password=VMWARE1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                string query = "SELECT s.Id, t.TipoSensore, s.IsAbilitato FROM dbo.AnaSensori AS s JOIN dbo.TipiSensori AS t ON s.IdTipo = t.Id WHERE s.IdLuogo = 2;";
                SqlCommand cmd = conn.CreateCommand();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch
            {
                MessageBox.Show("Errore nella connessione al Db, contattare l'amministratore");
                return null;
            }
        }
    }
}