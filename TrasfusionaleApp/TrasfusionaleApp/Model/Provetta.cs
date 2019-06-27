using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Service;

namespace TrasfusionaleApp.Model
{
    public class Provetta
    {
        public string uid { get; set; }
        public string uidPaziente { get; set; }
        private readonly string datiProvetta = "http://192.168.125.97:3000/provette/datiprovetta";
        private readonly string datiPaziente = "http://192.168.125.97:3000/paziente/datiPaziente";
        public async Task<bool> InviaProvetta()
        {
            REST<Provetta, Provetta> connessioneDatiProvetta = new REST<Provetta, Provetta>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token", App.Current.Properties["access-token"].ToString()));
            var result = await connessioneDatiProvetta.PostJson(datiProvetta, this, headers);
            if (connessioneDatiProvetta.responseMessage != System.Net.HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("Attenzione", connessioneDatiProvetta.warning, "OK");
                return false;
            }
            else
            {
                this.uidPaziente = result.uidPaziente;
                return true;
            }
        }
        public async Task<Paziente> localizzaPaziente()
        {
            REST<Provetta, Paziente> connessioneLocalizzaPaziente = new REST<Provetta, Paziente>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token", App.Current.Properties["access-token"].ToString()));
            var result = await connessioneLocalizzaPaziente.PostJson(datiPaziente, this,headers);
            if (connessioneLocalizzaPaziente.responseMessage != System.Net.HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("Attenzione", connessioneLocalizzaPaziente.warning, "OK");
                return default(Paziente);
            }
            else
                return result;
        }
    }
}
