using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Service;

namespace TrasfusionaleApp.Model
{
    public class Paziente
    {
        public string uid { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string gruppo { get; set; }
        public string temperatura { get; set; }
        public string frequenzaCardiaca { get; set; }
        public string pressioneArteriosa { get; set; }
        public string rh { get; set; }
        public string idReparto { get; set; }
        public string reparto { get; set; }
        public int letto { get; set; }
        public string dataAssegnazioneTrasfusione { get; set; }
        private readonly string datiPaziente = "http://192.168.125.31:3000/paziente/datiPaziente";

        public async Task<bool> prelevaDatiPaziente()
        {
            REST<Paziente, Paziente> connessioneLocalizzaPaziente = new REST<Paziente, Paziente>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token", App.Current.Properties["access-token"].ToString()));
            var result = await connessioneLocalizzaPaziente.PostJson(datiPaziente, this, headers);
            if (connessioneLocalizzaPaziente.responseMessage != System.Net.HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("Attenzione", connessioneLocalizzaPaziente.warning, "OK");
                return false;
            }
            else
            {
                copia(result);
                return true;
            }
        }

        public Paziente()
        {
            uid = "";
            nome = "";
            cognome = "";
            gruppo = "";
            temperatura = "";
            frequenzaCardiaca = "";
            pressioneArteriosa = "";
            rh = "";
            reparto = "";
            letto = 0;
            dataAssegnazioneTrasfusione = "";
    }

        public void copia(Paziente paziente)
        {
            this.uid = paziente.uid;
            this.nome = paziente.nome;
            this.cognome = paziente.cognome;
            this.gruppo = paziente.gruppo;
            this.idReparto = paziente.idReparto;
            this.reparto = paziente.reparto;
            this.rh = paziente.rh;
            this.letto = paziente.letto;
            this.dataAssegnazioneTrasfusione = paziente.dataAssegnazioneTrasfusione;
        }
    }
}
