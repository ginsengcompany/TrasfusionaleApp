using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Service;

namespace TrasfusionaleApp.Model
{
    public class DatiTrasfusione
    {
        public string _id { get; set; }
        public string uidInfermiere { get; set; }
        public string timerTrasfusione { get; set; }
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
            timerTrasfusione = "";
            paziente = new Paziente();
            uidSacca = "";
            inizioTrasfusione = "";
            uidMedico = "";
            note = "";
            datiPrimaTrasfusione = new DatiSalute();
            datiDopoTrasfusione = new DatiSalute();
        }

        public async void AvvioTimer()
        {
            
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
