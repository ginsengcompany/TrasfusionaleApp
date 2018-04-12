using System;
using System.Collections.Generic;
using System.Text;

namespace TrasfusionaleApp.Model
{
    public class ModelloHcs
    {
        public bool compatibilitaImmunologica { get; set; }
        public bool ispezioneEmocomponenti { get; set; }
        public bool identificazioneRiceventeRichiesta { get; set; }
        public bool identificazioneRiceventeScan { get; set; }
        public bool verificaOgniUnitaDaTrasfondere { get; set; }
    }
}
