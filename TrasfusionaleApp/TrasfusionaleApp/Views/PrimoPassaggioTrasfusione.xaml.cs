using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace TrasfusionaleApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrimoPassaggioTrasfusione : ContentPage
	{
        private Operatore infermiere, medico;
	    private Sacca sacca;
	    private bool scanSaccaEseguita = false, scanPazienteEseguita=false;
	    private Paziente paziente;
	    private bool pazienteScan = false, saccaScan = false;
        private DatiTrasfusione datiTrasfusione;

        public PrimoPassaggioTrasfusione (Operatore infermiere, Operatore medico)
		{
			InitializeComponent ();
            this.infermiere = infermiere;
            this.medico = medico;
            datiTrasfusione = new DatiTrasfusione();
            datiTrasfusione.uidInfermiere = infermiere.uid;
            datiTrasfusione.uidMedico = medico.uid;
		}

	    private async void ScannerizzaSacca(object sender, EventArgs e)
	    {
	        var scanPage = new ZXingScannerPage();
	        scanPage.OnScanResult += (result) => {
	            // Stop scanning
	            scanPage.IsScanning = false;
	            // Pop the page and show the result
	            Device.BeginInvokeOnMainThread(() => {
	                Navigation.PopAsync();
	                sacca = new Sacca();
	                sacca.uid = result.Text;
	                scanSaccaEseguita = true;
	                controllaScanSacca();
	            });
	        };
	        // Navigate to our scanner page
	        await Navigation.PushAsync(scanPage);
        }

	    private async void ScannerizzaPaziente(object sender, EventArgs e)
	    {
	        var scanPage = new ZXingScannerPage();
	        scanPage.OnScanResult += (result) => {
	            // Stop scanning
	            scanPage.IsScanning = false;

	            // Pop the page and show the result
	            Device.BeginInvokeOnMainThread(() => {
	                Navigation.PopAsync();
	                paziente = new Paziente();
	                paziente.uid = result.Text;
	                scanPazienteEseguita = true;
	                controllaScanPaziente();
	            });
	        };
	        // Navigate to our scanner page
	        await Navigation.PushAsync(scanPage);

        }
        private async void controllaScanSacca()
        {
            if (scanSaccaEseguita)
            {
                scanSaccaEseguita = false;
                if (await sacca.InviaSacca())
                {
                    labelSacca.Text = sacca.uid;
                    datiTrasfusione.uidSacca = sacca.uid;
                    saccaScan = true;
                    if (pazienteScan && saccaScan)
                    {
                        if (sacca.uidPaziente == paziente.uid)
                        {
                            await DisplayAlert("Pre-Trasfusionale", "Associazione corretta", "OK");
                            btnTrasfusione.IsEnabled = true;
                        }
                        else
                        {
                            await DisplayAlert("Pre-Trasfusionale", "Associazione non corretta", "OK");
                            btnTrasfusione.IsEnabled = false;
                        }
                    }
                    else
                        btnTrasfusione.IsEnabled = false;
                }
                else
                {
                    labelSacca.Text = "";
                    saccaScan = false;
                    btnTrasfusione.IsEnabled = pazienteScan && saccaScan;
                }
            }
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
            var response = await DisplayAlert("ATTENZIONE", "Sei sicuro di voler annullare l'operazione?", "SI", "No");
            if (response)
                App.Current.MainPage = new NavigationPage(new MainPage(infermiere));
        }

        private async void controllaScanPaziente()
        {
            if (scanPazienteEseguita)
            {
                scanPazienteEseguita = false;
                if (await paziente.prelevaDatiPaziente())
                {
                    NomePaziente.Text = paziente.nome;
                    CognomePaziente.Text = paziente.cognome;
                    RepartoPaziente.Text = paziente.reparto;
                    LettoPaziente.Text = paziente.letto.ToString();
                    datiTrasfusione.uidPaziente = paziente.uid;
                    datiTrasfusione.letto = paziente.letto;
                    datiTrasfusione.reparto = paziente.reparto;
                    pazienteScan = true;
                    if (pazienteScan && saccaScan)
                    {
                        if (sacca.uidPaziente == paziente.uid)
                        {
                            await DisplayAlert("Pre-Trasfusionale", "Associazione corretta", "OK");
                            btnTrasfusione.IsEnabled = true;
                        }
                        else
                        {
                            await DisplayAlert("Pre-Trasfusionale", "Associazione non corretta", "OK");
                            btnTrasfusione.IsEnabled = false;
                        }
                    }
                    else
                        btnTrasfusione.IsEnabled = false;
                }
                else
                {
                    NomePaziente.Text = "";
                    CognomePaziente.Text = "";
                    RepartoPaziente.Text = "";
                    LettoPaziente.Text = "";
                    pazienteScan = false;
                    btnTrasfusione.IsEnabled = pazienteScan && saccaScan;
                }
            }
        }

        private async void vaiTrasfusionale(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Trasfusionale(infermiere,medico,datiTrasfusione));
        }

    }
}