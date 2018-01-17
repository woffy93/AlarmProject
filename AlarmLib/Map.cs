using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace MapLibrary
{
    /// <summary>
    /// classe che controlla la mappa nella sala controllo, contiene una lista di centraline
    /// </summary>
    public class Map
    {
        public Image map { get; set; }
        public List<ClientInterface> allarmClients { get; set; }
        
        public void render()
        {
            allarmClients.ForEach(c => drawClient(c, c.getAlarmState()));
        }

        public void drawClient(ClientInterface c, Color col) { }
    }
}
