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
        //attributi

        public int idCentralina; // = a IdCasa su tabella dbo.SensoriLuogo
        public GeoCoordinate mapCoordinates { get; set; }
        //sensore che mappa tiposensore a sensore
        //la mappatura è univoca poiché la centralina è pensata
        //per avere un solo sensore per tipo
        public Dictionary<string, Sensors.AbstractSensor> sensors;
        public string dbUserId { get; set; }
        public string dbPassword { get; set; }


        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="id">The identifier.</param>
        public AlarmController(int id) {
            idCentralina = id;
        }


        //metodi

        /// <summary>
        /// Ritorna l'id di un signolo sensore
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <returns></returns>
        public int getSensorId(string sensor)
        {
            return sensors[sensor].sensorId;
        }
        /// <summary>
        /// Attiva/disattiva l'allarme nel sensore modificandone lo stato.
        /// Quando lo attiva scrive anche sul db prima però verifica che il sensore sia attivo.
        /// 
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        public void setAlarmState(string sensor, bool state)
        {
            Centralina.Sensors.AbstractSensor s = sensors[sensor];
            
            //se il sensore è abilitato scrivo sul db e ne cambio lo stato localmente
            if (s.abilitato == true)
            {               
                if(state == true) //caso state = true
                {
                    s.alarmState = true;
                    s.alarmTime = DateTime.Now;
                    this.allarm(s.sensorId, s.alarmTime);
                }else //caso state = false
                {
                    //se lo stato del sensore è salvato true lo cambio in false
                    if(sensors[sensor].alarmState)
                    {
                        s.alarmState = false;
                    }
                }
                
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

        /// <summary>
        /// Scrive materialmente gli allarmi sul DB
        /// </summary>
        /// <param name="sensorID">The sensor identifier.</param>
        /// <param name="sensorType">Type of the sensor.</param>
        /// <param name="time">The time.</param>
        public void allarm(int sensorID, DateTime time)
        {
            SqlConnection conn;
            conn = new SqlConnection(@"Server=tcp:serverallarmi.database.windows.net,1433;Initial Catalog=dballarmi;Persist Security Info=False;User ID="+dbUserId+";Password="+dbPassword+";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            conn.Open();
            string query = "INSERT INTO dbo.StoricoAllarmi (IdSensore, OraScatto) VALUES(@IdSensore, @OraScatto);";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@IdSensore", sensorID);
            cmd.Parameters.AddWithValue("@OraScatto", time);
            cmd.ExecuteNonQuery();
            conn.Close();

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
        /// Metodo che ricava dal db i sensori con tutti i loro dati
        /// </summary>
        /// <returns></returns>
        private SqlDataReader checkSensorsOnDb()
        {
            try
            {
                SqlConnection conn;
                conn = new SqlConnection(@"Server=tcp:serverallarmi.database.windows.net,1433;Initial Catalog=dballarmi;Persist Security Info=False;User ID=" + dbUserId + ";Password=" + dbPassword + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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
                MessageBox.Show("Errore nella connessione al Db, contattare l'amministratore. \n" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo che controlla sul db se l'allarme di un sensore è stato disattivato
        /// </summary>
        /// <param name="sensorID">The sensor identifier.</param>
        private void checkAlarmsOnDb(string sensorID)
        {
            string sensorType = sensors
                .ToDictionary(k => k.Value.sensorId, k => k.Key)[Int32.Parse(sensorID)];
            try
            {
                SqlDataReader reader;
                SqlConnection conn;
                conn = new SqlConnection(@"Server=tcp:serverallarmi.database.windows.net,1433;Initial Catalog=dballarmi;Persist Security Info=False;User ID=" + dbUserId + ";Password=" + dbPassword + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();
                string query =
                    "SELECT Id, OraDisatt FROM dbo.StoricoAllarmi WHERE IdSensore = @IdSensore AND OraScatto = @OraScatto AND OraDisatt IS NOT NULL;";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@IdSensore", Int32.Parse(sensorID));
                cmd.Parameters.AddWithValue("@OraScatto", sensors[sensorType].alarmTime);
                reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //recupero il tipo del sensore
                        setAlarmState(sensorType, false);
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Errore nella connessione al Db, contattare l'amministratore. \n" + e.Message);                
            }
        }


        /// <summary>
        /// metodo da chiamare periodicamente per controllare se gli allarmi sono stati disattivati
        /// </summary>
        public void checkAlarmDeactivation()
        {
            this.sensors
                .Where(s => s.Value.alarmState == true)
                .ToDictionary(s => s.Value.sensorId, s => s.Value)
                .Keys
                .ToList()
                .ForEach(k => this.checkAlarmsOnDb(k.ToString()));   
        }
    }
}