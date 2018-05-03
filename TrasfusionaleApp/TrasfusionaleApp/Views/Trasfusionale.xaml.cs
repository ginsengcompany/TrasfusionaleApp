using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrasfusionaleApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Trasfusionale : ContentPage
	{
        private Operatore infermiere, medico;
        private DatiTrasfusione datiTrasfusione;

        public Trasfusionale (Operatore infermiere, Operatore medico, DatiTrasfusione datiTrasfusione)
		{
			InitializeComponent ();
            this.infermiere = infermiere;
            this.medico = medico;
            this.datiTrasfusione = datiTrasfusione;
		}
        
        private async void inizia (object sender, EventArgs e)
        {
            datiTrasfusione.datiDopoTrasfusione.temperatura = entryTemperatura.Text;
            datiTrasfusione.datiDopoTrasfusione.pressioneArteriosa = entryPressioneArteriosa.Text;
            datiTrasfusione.datiDopoTrasfusione.frequenzaCardiaca = entryFrequenzaCardiaca.Text;
            var response = await datiTrasfusione.invioDatiInizioTrasfusione();
            if (response)
                App.Current.MainPage = new NavigationPage(new MainPage(infermiere));
        }

        protected override bool OnBackButtonPressed()
        {
            annullaOperazione();
            return true;
        }

        private void toolbarAnnulla(object sender, EventArgs e)
        {
            annullaOperazione();
        }

        private void controlloCampiEntry(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryPressioneArteriosa.Text) && !string.IsNullOrWhiteSpace(entryFrequenzaCardiaca.Text) && !string.IsNullOrWhiteSpace(entryTemperatura.Text))
                btnInizia.IsEnabled = true;
            else
                btnInizia.IsEnabled = false;
        }

        private async void annullaOperazione()
        {
            var response = await DisplayAlert("Attenzione", "Sei sicuro di voler annullare l'operazione?", "SI", "NO");
            if (response)
                App.Current.MainPage = new NavigationPage(new MainPage(infermiere));
        }
    }
}