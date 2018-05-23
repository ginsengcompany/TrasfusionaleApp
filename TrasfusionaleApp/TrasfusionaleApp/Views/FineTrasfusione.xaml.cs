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
	public partial class FineTrasfusione : ContentPage
	{
        private DatiTrasfusione datiTrasfusione;
        private Operatore infermiere;

		public FineTrasfusione (DatiTrasfusione dati, Operatore infermiere)
		{
			InitializeComponent ();
            datiTrasfusione = dati;
            this.infermiere = infermiere;
		}

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void controlloCampiEntry(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryPressioneArteriosa.Text) && !string.IsNullOrWhiteSpace(entryFrequenzaCardiaca.Text) && !string.IsNullOrWhiteSpace(entryTemperatura.Text))
                btnFine.IsEnabled = true;
            else
                btnFine.IsEnabled = false;
        }

        private async void fineTrasfusione(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(entryPressioneArteriosa.Text.Trim()) && !string.IsNullOrWhiteSpace(entryFrequenzaCardiaca.Text.Trim()) && !string.IsNullOrWhiteSpace(entryTemperatura.Text.Trim()))
            {
                datiTrasfusione.datiDopoTrasfusione.pressioneArteriosa = entryPressioneArteriosa.Text;
                datiTrasfusione.datiDopoTrasfusione.temperatura = entryTemperatura.Text;
                datiTrasfusione.datiDopoTrasfusione.frequenzaCardiaca = entryFrequenzaCardiaca.Text;
                if (!string.IsNullOrWhiteSpace(editorNote.Text.Trim()))
                    datiTrasfusione.note = editorNote.Text;
                var response = await datiTrasfusione.invioDatiFineTrasfusione();
                if (response)
                    App.Current.MainPage = new NavigationPage(new MainPage(infermiere));
            }
        }
	}
}