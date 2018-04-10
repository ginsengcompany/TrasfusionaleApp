using System;
using System.Collections.Generic;
using System.Text;

namespace TrasfusionaleApp.Model
{
    public class Paziente
    {
        public string uid { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string gruppo { get; set; }
        public string rh { get; set; }
        public string dataAssegnazioneTrasfusione { get; set; }
    }
}
