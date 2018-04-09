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


        public async Task<bool> Login()
        {
            REST<Operatore,string> connessioneLogin = new REST<Operatore, string>();
            var result = await connessioneLogin.PostJson("http://192.168.125.14:3000/operatore/login", this);
            if (connessioneLogin.responseMessage != System.Net.HttpStatusCode.OK)
                return false;
            else
                return true;
        }
    }

    
}
