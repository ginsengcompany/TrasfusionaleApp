using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Service;

namespace TrasfusionaleApp.Model
{
   public class Sacca
    {
        public string uid { get; set; }
        public string uidPaziente { get; set; }
        private readonly string datiSacca = "http://192.168.125.14:3000/sacche/datisacca";
        public static readonly string eventSocketSacca = "uidsacca";

        public async Task<bool> InviaSacca()
        {
            REST<Sacca, Sacca> connessioneDatiSacca = new REST<Sacca, Sacca>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token", App.Current.Properties["access-token"].ToString()));
            var result = await connessioneDatiSacca.PostJson(datiSacca, this, headers);
            if (connessioneDatiSacca.responseMessage != System.Net.HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("Attenzione", connessioneDatiSacca.warning, "OK");
                return false;
            }
            else
            {
                this.uidPaziente = result.uidPaziente;
                return true;
            }
        }
    }

}
