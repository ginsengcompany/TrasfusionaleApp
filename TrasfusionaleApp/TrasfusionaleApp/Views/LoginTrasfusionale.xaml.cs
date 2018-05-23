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
	public partial class LoginTrasfusionale : ContentPage
	{
        private Operatore infermiere,medico;
	    private ModelloHcs modello;

		public LoginTrasfusionale (Operatore infermiere,ModelloHcs modello)
		{
			InitializeComponent ();
            this.infermiere = infermiere;
		    this.modello = modello;

		}

        public async void scan(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;
                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    entryUsernameMedico.Text = result.Text;
                });
            };
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
        }

        private async void AvvioLogin(object sender, EventArgs e)
        {
            medico = new Operatore();
            medico.uid = entryUsernameMedico.Text;
            medico.password = entryPasswordMedico.Text;
            var esito = await medico.LoginTrasfusione();
            if (esito)
            {
                await DisplayAlert("Login", "Salve " + medico.nome + " " + medico.cognome, "OK");
                await Navigation.PushAsync(new CompilazioneModelloH(medico,infermiere,modello));
                entryUsernameMedico.Text = "";
                entryPasswordMedico.Text = "";
            }
            else
                await DisplayAlert("Login", "Accesso negato", "OK");
        }

        private void mostraPassword(object sender, EventArgs e)
        {
            if (entryPasswordMedico.IsPassword)
                entryPasswordMedico.IsPassword = false;
            else
                entryPasswordMedico.IsPassword = true;
        }
    }
}