using Newtonsoft.Json;
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
	    private bool animazione = false;
        private ClassSock datiSock;
        private bool operazioneTerminata = false;
        private readonly string eventIniziaTrasfusione = "iniziotrasfusione";
        private readonly string eventFineTrasfusione = "finetrasfusione";
        private readonly string eventUpdateTrasfusione = "update";

        public Trasfusionale (Operatore infermiere, Operatore medico, Socket socket, ClassSock classSock)
		{
			InitializeComponent ();
            this.infermiere = infermiere;
            this.medico = medico;
            this.socket = socket;
            this.datiSock = classSock;
		}
        /*
	    private async void animazioneGoccia()
	    {
	        while (animazione)
	        {
	            await goccia.TranslateTo(0, 500, 2000);
	            await goccia.TranslateTo(0, 0, 0);
	        }
	    }
        */
        private void inizia (object sender, EventArgs e)
        {
            datiSock.datiPrimaTrasfusione.frequenzaCardiaca = entryFrequenzaCardiaca.Text;
            datiSock.datiPrimaTrasfusione.pressioneArteriosa = entryPressioneArteriosa.Text;
            datiSock.datiPrimaTrasfusione.temperatura = entryTemperatura.Text;
            socket.Emit(eventIniziaTrasfusione, JsonConvert.SerializeObject(datiSock));
            btnInizia.IsEnabled = false;
            btnFine.IsEnabled = true;
            animazione = true;
            //animazioneGoccia();
        }

        private async void fine (object sender, EventArgs e)
        {
            var response = await DisplayAlert("Trasfusionale", "Sei sicuro di voler terminare l'operazione?", "SI", "NO");
            if (!response)
                return;
            datiSock.datiPrimaTrasfusione.temperatura = entryTemperatura.Text;
            datiSock.datiPrimaTrasfusione.frequenzaCardiaca = entryFrequenzaCardiaca.Text;
            datiSock.datiPrimaTrasfusione.pressioneArteriosa = entryPressioneArteriosa.Text;
            socket.Emit(eventFineTrasfusione, JsonConvert.SerializeObject(datiSock));
            entryTemperatura.Text = "";
            entryFrequenzaCardiaca.Text = "";
            entryPressioneArteriosa.Text = "";
            labelNote.IsVisible = true;
            editorNote.IsVisible = true;
            btnFine.IsEnabled = false;
            operazioneTerminata = true;
            btnInviaDati.IsEnabled = true;
            //animazione = false;
        }

        private async void inviaDati(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Trasfusionale","Sei sicuro di voler inviare i dati?","SI","NO");
            if (!response)
                return;
            datiSock.datiDopoTrasfusione.temperatura = entryTemperatura.Text;
            datiSock.datiDopoTrasfusione.pressioneArteriosa = entryPressioneArteriosa.Text;
            datiSock.datiDopoTrasfusione.frequenzaCardiaca = entryFrequenzaCardiaca.Text;
            socket.Emit(eventUpdateTrasfusione, JsonConvert.SerializeObject(datiSock));
            await DisplayAlert("Trasfusionale", "I dati sono stati inviati", "OK");
            socket.Disconnect();
            App.Current.MainPage = new NavigationPage(new MainPage(infermiere));
        }

        protected override bool OnBackButtonPressed()
        {
            if (!operazioneTerminata)
                annullaOperazione();
            return true;
        }

        private void toolbarAnnulla(object sender, EventArgs e)
        {
            if (!operazioneTerminata)
                annullaOperazione();
        }

        private void controlloCampiEntry(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryPressioneArteriosa.Text) && !string.IsNullOrWhiteSpace(entryFrequenzaCardiaca.Text) && !string.IsNullOrWhiteSpace(entryTemperatura.Text))
                btnInizia.IsEnabled = true;
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