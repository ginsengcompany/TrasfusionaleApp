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
	public partial class IndividuaPazienteView : ContentPage
	{
        private Operatore operatore = new Operatore();
        private Provetta provetta;
        private Paziente paziente;
        private bool scanEseguita = false;
		public IndividuaPazienteView (Operatore operatore)
		{
			InitializeComponent ();
            this.operatore = operatore;
            entryCodiceProvetta.Unfocused += async (object sender, FocusEventArgs e) =>
            {
                if (!String.IsNullOrEmpty(entryCodiceProvetta.Text))
                {
                    provetta = new Provetta();
                    provetta.uid = entryCodiceProvetta.Text;
                    if (await provetta.InviaProvetta())
                    {
                        if ((paziente = await provetta.localizzaPaziente()) != default(Paziente))
                        {
                            entryCodiceProvetta.Text = provetta.uid;
                            labelCognomePaziente.Text = paziente.cognome;
                            labelNomePaziente.Text = paziente.nome;
                            labelRepartoPaziente.Text = paziente.reparto;
                            labelLettoPaziente.Text = paziente.letto.ToString();
                        }
                        else
                        {
                            entryCodiceProvetta.Text = "";
                            labelCognomePaziente.Text = "";
                            labelNomePaziente.Text = "";
                            labelRepartoPaziente.Text = "";
                            labelLettoPaziente.Text = "";
                        }
                    }
                    else
                    {
                        entryCodiceProvetta.Text = "";
                        labelCognomePaziente.Text = "";
                        labelNomePaziente.Text = "";
                        labelRepartoPaziente.Text = "";
                        labelLettoPaziente.Text = "";
                    }
                }
            };
		}

        private async void scan(object sender, EventArgs e)
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
                    entryCodiceProvetta.Text = result.Text;
                    scanEseguita = true;
                    controlloscanProvetta();
                });
            };
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
        }

        private async void controlloscanProvetta()
        {
            if (scanEseguita)
            {
                scanEseguita = false;
                if(await provetta.InviaProvetta())
                {
                    if ((paziente = await provetta.localizzaPaziente()) != default(Paziente))
                    {
                        entryCodiceProvetta.Text = provetta.uid;
                        labelCognomePaziente.Text = paziente.cognome;
                        labelNomePaziente.Text = paziente.nome;
                        labelRepartoPaziente.Text = paziente.reparto;
                        labelLettoPaziente.Text = paziente.letto.ToString();
                        labelIdPaziente.Text = paziente.uid;
                    }
                    else
                    {
                        entryCodiceProvetta.Text = "";
                        labelCognomePaziente.Text = "";
                        labelNomePaziente.Text = "";
                        labelRepartoPaziente.Text = "";
                        labelLettoPaziente.Text = "";
                        labelIdPaziente.Text = "";
                    }
                }
                else
                {
                    entryCodiceProvetta.Text = "";
                    labelCognomePaziente.Text = "";
                    labelNomePaziente.Text = "";
                    labelRepartoPaziente.Text = "";
                    labelLettoPaziente.Text = "";
                    labelIdPaziente.Text = "";
                }
            }
        }
    }
}