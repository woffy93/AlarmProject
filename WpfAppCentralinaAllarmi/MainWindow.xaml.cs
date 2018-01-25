using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfAppCentralinaAllarmi
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// WindowLoaded fa partire il loop che controlla i sensori per
    /// aggiornale l'UI a seconda di quali siano attivati.
    /// Le luci sono aggiornate dal loop e non dalla pressione del tasto perché abbiamo
    /// voluto simulare il ciclo di vita di una reale centralina che legge i sensori
    /// ed invia segnali in base al loroo stato
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] sensorLabels = { "Incendio", "Fuoriuscita di gas", "Intrusione" };
        private Centralina.AlarmController controller;
        private Ellipse[] lights;
        public int Id;

        /*TODO:
         * Controllo su db remoto per vedere quali sensori sono da controllare
         */

        public MainWindow(int id)
        {
            InitializeComponent();
            this.Id = Id;
        }

        //metodi per controllo UI

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //inizializzo centralina sensori
            controller = new Centralina.AlarmController(Id);
            controller.dbUserId = "cisco";
            controller.dbPassword = "VMWare1!";
            controller.sensors = controller.initSensors();
            //funzione col main loop
            control();
        }


        /// <summary>
        /// I bottoni cambiano lo stato del sensore, attivando/disattivando l'allarme.
        /// Tutte le altre attività (cambio colore luci, lettura/scritturadb) dipendono dai metodi
        /// che leggono lo stato dei sensori
        /// </summary>

        private void Btn_Fire_On_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[0], true);
        }

        private void Btn_Fire_Off_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[0], false);
        }

        private void Btn_Gas_On_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[1], true);
        }

        private void Btn_Gas_Off_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[1], false);
 
        }

        private void Btn_Intrusion_On_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[2], true);
        }

        private void Btn_Intrusion_Off_Click(object sender, RoutedEventArgs e)
        {
            controller.setAlarmState(sensorLabels[2], false);
        }

        //Metodi helper

        private async Task<Dictionary<string, bool>> checkSensors()
        {
            await Task.Delay(500);
            return this.controller.getAlarms();
        }

        //metodo per le luci, a seconda dello stato le colora in modo diverso
        private void setLightColor(string name, bool state)
        {
            if (name == sensorLabels[0])
            {
                Light_fire.Fill = state ?
                    new SolidColorBrush(Color.FromRgb(255, 0, 0)) :
                    new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            else if (name == sensorLabels[1])
            {
                Light_Gas.Fill = state ?
                    new SolidColorBrush(Color.FromRgb(255, 0, 0)) :
                    new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
            else
            {
                Light_Intrusion.Fill = state ?
                    new SolidColorBrush(Color.FromRgb(255, 0, 0)) :
                    new SolidColorBrush(Color.FromRgb(0, 255, 0));
            }
        }

        //metodo asincrono per controllare i sensori e colorare l'UI senza bloccare tutto
        private async void control()
        {
            while (true)
            {
                Dictionary<string, bool> dict = await checkSensors();
                dict.ToList()
                    .ForEach(t => setLightColor(t.Key, t.Value));
                await Task.Run(() => this.checkAlarms());
            }                    
        }

        /// <summary>
        /// Metodo che controlla cambiamenti nello stato degli alalrmi sul db remoto
        /// </summary>
        private void checkAlarms()
        {
            this.controller.checkAlarmDeactivation();
        }
    }
}