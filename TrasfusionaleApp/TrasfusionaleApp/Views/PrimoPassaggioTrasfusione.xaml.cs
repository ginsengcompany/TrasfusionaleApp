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
	    private Sacca sacca;
	    private bool scanSaccaEseguita = false, scanPazienteEseguita=false;
	    private Paziente paziente;
	    private bool pazienteScan = false, saccaScan = false;

        public PrimoPassaggioTrasfusione ()
		{
			InitializeComponent ();
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
                    saccaScan = true;
                    if (pazienteScan && saccaScan)
                    {
                        if (sacca.uidPaziente == paziente.uid)
                        {
                            await DisplayAlert("Pre-Trasfusionale", "Associazione corretta", "OK");
                            btnPrelievo.IsEnabled = true;
                        }
                        else
                        {
                            await DisplayAlert("Pre-Trasfusionale", "Associazione non corretta", "OK");
                           btnPrelievo.IsEnabled = false;
                        }
                    }
                   else
                        btnPrelievo.IsEnabled = false;
                }
                else
                {
                    labelSacca.Text = "";
                    saccaScan = false;
                    btnPrelievo.IsEnabled = pazienteScan && saccaScan;
                }
            }
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
                    pazienteScan = true;
                    if (pazienteScan && saccaScan)
                    {
                        if (sacca.uidPaziente == paziente.uid)
                        {
                            await DisplayAlert("Pre-Trasfusionale", "Associazione corretta", "OK");
                           btnPrelievo.IsEnabled = true;
                        }
                        else
                        {
                            await DisplayAlert("Pre-Trasfusionale", "Associazione non corretta", "OK");
                            btnPrelievo.IsEnabled = false;
                        }
                    }
                    else
                        btnPrelievo.IsEnabled = false;
                }
                else
                {
                    NomePaziente.Text = "";
                    CognomePaziente.Text = "";
                    RepartoPaziente.Text = "";
                    LettoPaziente.Text = "";
                    pazienteScan = false;
                  btnPrelievo.IsEnabled = pazienteScan && saccaScan;
                }
            }
        }
    }
}