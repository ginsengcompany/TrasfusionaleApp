using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TrasfusionaleApp.Model
{
    public class ListViewTrasfusionali : ObservableCollection<DatiTrasfusione>
    {
        public string uidInfermiere { get; set; }
        public Paziente paziente { get; set; }
        public string uidSacca { get; set; }
        public string uidMedico { get; set; }
        public string note { get; set; }
        public string reparto { get; set; }
        public int letto { get; set; }
        public DatiSalute datiPrimaTrasfusione { get; set; }
        public DatiSalute datiDopoTrasfusione { get; set; }

        public ListViewTrasfusionali()
        {
            uidInfermiere = "";
            paziente = new Paziente();
            uidSacca = "";
            uidMedico = "";
            note = "";
            reparto = "";
            letto = 0;
            datiPrimaTrasfusione = new DatiSalute();
            datiDopoTrasfusione = new DatiSalute();
        }

        public ICommand _terminaTrasfusione
        {
            get
            {
                return new Command(async () =>
                {
                    await terminaTrasfusione();
                });
            }
        }

        private async Task terminaTrasfusione()
        {
            
        }
    }
}
