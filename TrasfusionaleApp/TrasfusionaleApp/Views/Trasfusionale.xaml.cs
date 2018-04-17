using Quobject.SocketIoClientDotNet.Client;
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
        private Socket socket;
	    private Paziente paziente;
	    private bool animazione = false;
        private readonly string eventIniziaTrasfusione = "iniziotrasfusione";
        private readonly string eventFineTrasfusione = "finetrasfusione";
		public Trasfusionale (Operatore infermiere, Operatore medico, Socket socket)
		{
			InitializeComponent ();
            btnFine.IsEnabled = false;
            this.infermiere = infermiere;
            this.medico = medico;
            this.socket = socket;
            paziente= new Paziente();
		    paziente.temperatura = entryTemperatura.Text;
		    paziente.frequenzaCardiaca = entryFrequenzaCardiaca.Text;
		    paziente.pressioneArteriosa = entryPressioneArteriosa.Text;

		}

	    private async void animazioneGoccia()
	    {
	        while (animazione)
	        {
	            await goccia.TranslateTo(0, 500, 2000);
	            await goccia.TranslateTo(0, 0, 0);
	        }
	    }

        private void inizia (object sender, EventArgs e)
        {
            socket.Emit(eventIniziaTrasfusione);
            btnInizia.IsEnabled = false;
            btnFine.IsEnabled = true;
            animazione = true;
            animazioneGoccia();
        }

        private async void fine (object sender, EventArgs e)
        {
            socket.Emit(eventFineTrasfusione);
            socket.Disconnect();
            btnFine.IsEnabled = false;
            btnInizia.IsEnabled = true;
            animazione = false;
            await DisplayAlert("Trasfusionale", "Il log dell'operazione è stato inviato", "OK");
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

        private async void annullaOperazione()
        {
            var response = await DisplayAlert("Attenzione", "Sei sicuro di voler annullare l'operazione?", "SI", "NO");
            if (response)
            {
                socket.Disconnect();
                App.Current.MainPage = new NavigationPage(new MainPage(infermiere));
            }
        }
    }
}