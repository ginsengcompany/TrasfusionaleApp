using System;
using System.Collections.Generic;
using System.Text;

namespace TrasfusionaleApp.Model
{
    public class ClassSock
    {
        public static readonly string eventUid = "uid";
        public static readonly string eventIniziaTrasfusione = "iniziotrasfusione";
        public static readonly string eventFineTrasfusione = "finetrasfusione";
        public string uidInfermiere { get; set; }
        public string uidPaziente { get; set; }
        public string uidSacca { get; set; }
        public string uidMedico { get; set; }
        public DatiSalute datiPrimaTrasfusione { get; set; }
        public DatiSalute datiDopoTrasfusione { get; set; }
    }
}
