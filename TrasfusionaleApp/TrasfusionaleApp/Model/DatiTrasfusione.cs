using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Service;

namespace TrasfusionaleApp.Model
{
    public class DatiTrasfusione
    {
        public string uidInfermiere { get; set; }
        public string uidPaziente { get; set; }
        public string uidSacca { get; set; }
        public string uidMedico { get; set; }
        public string note { get; set; }
        public string reparto { get; set; }
        public int letto { get; set; }
        public DatiSalute datiPrimaTrasfusione { get; set; }
        public DatiSalute datiDopoTrasfusione { get; set; }
        private string restInvioDatiInizioTrasfusione = "http://192.168.125.14:3000/trasfusioni/inserisciTrasfusione";
        private string restListTrasfusioniForReparto = "http://192.168.125.14:3000/trasfusioni/listaPerReparto";
        private string restInvioDatiFineTrasfusione = "http://192.168.125.14:3000/trasfusioni/aggiornamentoDopoLaTrasfusione";

        public DatiTrasfusione()
        {
            uidInfermiere = "";
            uidPaziente = "";
            uidSacca = "";
            uidMedico = "";
            note = "";
            reparto = "";
            letto = 0;
            datiPrimaTrasfusione = new DatiSalute();
            datiDopoTrasfusione = new DatiSalute();
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
