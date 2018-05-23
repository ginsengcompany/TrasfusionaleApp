using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Service;

namespace TrasfusionaleApp.Model
{
    public class Operatore
    {
        public string uid { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string tipoOperatore { get; set; }
        public int codice_operatore { get; set; }
        public string password { get; set; }
        private readonly string restLogin = "http://192.168.125.24:3000/operatore/login";
        private readonly string restMe = "http://192.168.125.24:3000/operatore/me";
        private readonly string restLoginTrasfusionale = "http://192.168.125.24:3000/operatore/loginTrasfusione";
        public static readonly string eventSocketMedico = "uidmedico";
        public static readonly string eventSocketInfermiere = "uidinfermiere";

        public async Task<bool> Login()
        {
            REST<Operatore,string> connessioneLogin = new REST<Operatore, string>();
            var result = await connessioneLogin.PostJson(restLogin, this);
            if (connessioneLogin.responseMessage != System.Net.HttpStatusCode.OK)
                return false;
            else
            {
                if (App.Current.Properties.ContainsKey("access-token"))
                    App.Current.Properties["access-token"] = connessioneLogin.warning;
                else
                    App.Current.Properties.Add("access-token", connessioneLogin.warning);
                var esito = await me();
                return esito;
            }
        }

        public async Task<bool> LoginTrasfusione()
        {
            REST<Operatore, string> connessioneLogin = new REST<Operatore, string>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token", App.Current.Properties["access-token"].ToString()));
            var result = await connessioneLogin.PostJson(restLoginTrasfusionale, this, headers);
            if (connessioneLogin.responseMessage != System.Net.HttpStatusCode.OK)
                return false;
            else
            {
                var esito = await meTrasfusionale(connessioneLogin.warning);
                return esito;
            }
        }

        private async Task<bool> meTrasfusionale(string token)
        {
            REST<object, Operatore> connessioneMe = new REST<object, Operatore>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token", token));
            var result = await connessioneMe.GetSingleJson(restMe, headers);
            if (connessioneMe.responseMessage != System.Net.HttpStatusCode.OK)
                return false;
            else
            {
                this.uid = result.uid;
                this.nome = result.nome;
                this.tipoOperatore = result.tipoOperatore;
                this.cognome = result.cognome;
                this.codice_operatore = result.codice_operatore;
                return true;
            }
        }

        private async Task<bool> me()
        {
            REST<object, Operatore> connessioneMe = new REST<object, Operatore>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token",App.Current.Properties["access-token"].ToString()));
            var result = await connessioneMe.GetSingleJson(restMe,headers);
            if (connessioneMe.responseMessage != System.Net.HttpStatusCode.OK)
                return false;
            else
            {
                this.uid = result.uid;
                this.nome = result.nome;
                this.tipoOperatore = result.tipoOperatore;
                this.cognome = result.cognome;
                this.codice_operatore = result.codice_operatore;
                return true;
            }
        }
    }

    
}
