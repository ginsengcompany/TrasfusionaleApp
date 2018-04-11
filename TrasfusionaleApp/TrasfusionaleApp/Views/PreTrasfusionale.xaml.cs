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
	public partial class PreTrasfusionale : ContentPage
	{
        private Operatore operatore;
        private Paziente paziente;
        private Provetta provetta;
        private bool scanPazienteEseguita = false, scanProvettaEseguita = false;
        private bool pazienteScan = false, provettaScan = false;
		public PreTrasfusionale (Operatore operatore)
		{
			InitializeComponent ();
            this.operatore = operatore;
            btnPrelievo.IsEnabled = false;
		}

        private async Task scanPaziente()
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
                });
            };
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
        }

        private async Task scanProvetta()
        {
            var scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;
                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    provetta = new Provetta();
                    provetta.uid = result.Text;
                    scanProvettaEseguita = true;
                });
            };
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
        }

        private async void btnScanPaziente(object sender, EventArgs e)
        {
            await scanPaziente();
            if (scanPazienteEseguita)
            {
                scanPazienteEseguita = false;
                if(await paziente.prelevaDatiPaziente())
                {
                    NomePaziente.Text = paziente.nome;
                    CognomePaziente.Text = paziente.cognome;
                    RepartoPaziente.Text = paziente.reparto;
                    LettoPaziente.Text = paziente.letto.ToString();
                    pazienteScan = true;
                    btnPrelievo.IsEnabled = pazienteScan && provettaScan;
                }
                else
                {
                    NomePaziente.Text = "";
                    CognomePaziente.Text = "";
                    RepartoPaziente.Text = "";
                    LettoPaziente.Text = "";
                    pazienteScan = false;
                    btnPrelievo.IsEnabled = pazienteScan && provettaScan;
                }
            }
        }

        private async void btnScanProvetta(object sender, EventArgs e)
        {
            await scanProvetta();
            if (scanProvettaEseguita)
            {
                scanProvettaEseguita = false;
                if (await provetta.InviaProvetta())
                {
                    labelProvetta.Text = provetta.uid;
                    provettaScan = true;
                    btnPrelievo.IsEnabled = pazienteScan && provettaScan;
                }
                else
                {
                    labelProvetta.Text = "";
                    provettaScan = false;
                    btnPrelievo.IsEnabled = pazienteScan && provettaScan;
                }
            }
        }

        private void prelievo(object sender, EventArgs e)
        {

        }
	}
}