using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Service;
using Xamarin.Forms;

namespace TrasfusionaleApp.Model
{
    public class DatiTrasfusione : INotifyPropertyChanged
    {
        public string _id { get; set; }
        public string uidInfermiere { get; set; }
        private string timerTrasfusione;

        public string TimerTrasfusione
        {
            get { return timerTrasfusione; }
            set
            {

                OnPropertyChanged();
                timerTrasfusione = value;
                
            }
        }


        public Paziente paziente { get; set; }
        public string uidSacca { get; set; }
        public string uidMedico { get; set; }
        public string note { get; set; }
        public string inizioTrasfusione { get; set; }
        public DatiSalute datiPrimaTrasfusione { get; set; }
        public DatiSalute datiDopoTrasfusione { get; set; }
        private string restInvioDatiInizioTrasfusione = "http://192.168.125.14:3000/trasfusioni/inserisciTrasfusione";
        public static readonly string restListTrasfusioniForReparto = "http://192.168.125.14:3000/trasfusioni/listaPerReparto";
        private string restInvioDatiFineTrasfusione = "http://192.168.125.14:3000/trasfusioni/aggiornamentoDopoLaTrasfusione";
        public static readonly string restFineTrasfusione = "http://192.168.125.14:3000/trasfusioni/fineTrasfusione";


        public DatiTrasfusione()
        {
            _id = "";
            uidInfermiere = "";
            timerTrasfusione = "0";
            paziente = new Paziente();
            uidSacca = "";
            inizioTrasfusione = "";
            uidMedico = "";
            note = "";
            datiPrimaTrasfusione = new DatiSalute();
            datiDopoTrasfusione = new DatiSalute();
        }
       private int ore = 0, minuti = 0, secondi = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public async void CalcoloTimer()
        {
            string puntiniMinuti = ":";
            string puntiniSecondi = ":";
            DateTime TempoAttuale = new DateTime();
            int oraTrasfu = Int32.Parse(inizioTrasfusione.Substring(11,2));
            int minTrasfu = Int32.Parse(inizioTrasfusione.Substring(14, 2));
            int secTrasfu = Int32.Parse(inizioTrasfusione.Substring(17));
            TempoAttuale = DateTime.Now;
            int oraAt = Int32.Parse( String.Format("{0:HH}", TempoAttuale));
            int minAt = Int32.Parse( String.Format("{0:mm}", TempoAttuale));
            int secAt = Int32.Parse( String.Format("{0:ss}", TempoAttuale));

            if (secTrasfu > secAt)
            {
                secAt = secAt + 60;
                secondi = secAt - secTrasfu;
                if (secondi < 10)
                    puntiniSecondi = ":0";
                minAt = minAt - 1;
            }
            if (minTrasfu > minAt)
            {
                minAt = minAt + 60;
                minuti = minAt - minTrasfu;
                if (minuti < 10)
                    puntiniMinuti = ":0";
                oraAt = oraAt - 1;
            }

            ore = oraAt - oraTrasfu;
            if(minuti<10)
                TimerTrasfusione = ore.ToString() + puntiniMinuti + minuti.ToString() + puntiniSecondi + secondi.ToString();
            
        }


        public async void AvvioTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                string puntiniMinuti = ":";
                string puntiniSecondi = ":";
                secondi++;
                if (secondi >= 60)
                {
                    secondi = 0;
                    minuti = minuti + 1;
                    if (minuti >= 60)
                    {
                        minuti = 0;
                        ore = ore + 1;
                    }
                }
                if (minuti < 10)
                    puntiniMinuti = ":0";
                if (secondi < 10)
                    puntiniSecondi = ":0";
                TimerTrasfusione = ore.ToString() + puntiniMinuti + minuti.ToString() + puntiniSecondi + secondi.ToString();
                return true;
            });
        }

        public async Task<bool> invioDatiInizioTrasfusione()
        {
            REST<DatiTrasfusione, string> rEST = new REST<DatiTrasfusione, string>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token", App.Current.Properties["access-token"].ToString()));
            var res = await rEST.PostJson(restInvioDatiInizioTrasfusione, this, headers);
            if (rEST.responseMessage != System.Net.HttpStatusCode.Created)
            {
                await App.Current.MainPage.DisplayAlert("Errore " + rEST.responseMessage, "I dati che hai inserito non sono stati inviati", "OK");
                return false;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Dati Inviati", "I dati che hai inserito sono stati inviati correttamente", "OK");
                return true;
            }
        }
    }
}
