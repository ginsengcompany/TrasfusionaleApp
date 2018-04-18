using System;
using System.Collections.Generic;
using System.Text;

namespace TrasfusionaleApp.Model
{
    public class ClassSock
    {
        public string uidInfermiere { get; set; }
        public string uidPaziente { get; set; }
        public string uidSacca { get; set; }
        public string uidMedico { get; set; }
        public string note { get; set; }
        public DatiSalute datiPrimaTrasfusione { get; set; }
        public DatiSalute datiDopoTrasfusione { get; set; }

        public ClassSock()
        {
            uidInfermiere = "";
            uidPaziente = "";
            uidSacca = "";
            uidMedico = "";
            note = "";
            datiPrimaTrasfusione = new DatiSalute();
            datiDopoTrasfusione = new DatiSalute();
        }
    }
}
