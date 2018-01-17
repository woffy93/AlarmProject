using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace WpfAppCentralinaAllarmi.Centralina
{
    /// <summary>
    ///  classe che rappresenta la centralina controllo allarmi, connessa ai sensori
    /// </summary>
    public class AlarmController
    {
        public AlarmController(Dictionary<string, Sensors.AbstractSensor> s) {
            sensors = s;
        }
        //contiene l'elenco dei sensori attivi
        public List<string> sensorsToRead { get; set; }

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

        public void allarmDeactivated(int sensorID, string sensorType, DateTime time)
        {
            //scrivere allarme su db remoto
            //MOCK
            Console.WriteLine("Allarme Disattivato");
        }
    }
}