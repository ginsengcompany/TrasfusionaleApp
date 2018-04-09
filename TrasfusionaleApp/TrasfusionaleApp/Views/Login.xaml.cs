﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Model;
using TrasfusionaleApp.Service;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrasfusionaleApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        private Operatore operatore = new Operatore();
	    private bool medico = false;
		public Login (bool medico)
		{
			InitializeComponent ();
		    this.medico = medico;
		}

	    private async void AvvioLogin(object sender, EventArgs e)
	    {
	        operatore.uid = entryUsername.Text;
	        operatore.password = entryPassword.Text;
            var esito = await operatore.Login();
            if (esito)
            {
                await DisplayAlert("Login", "Login effettuata con successo", "OK");
                await Navigation.PushAsync(new MainPage());
            }
            else
                await DisplayAlert("Login", "Accesso negato", "OK");
        }

	    public async void scan()
	    {
	        var scanPage = new ZXingScannerPage();

	        scanPage.OnScanResult += (result) => {
	            // Stop scanning
	            scanPage.IsScanning = false;

	            // Pop the page and show the result
	            Device.BeginInvokeOnMainThread(() => {
	                Navigation.PopAsync();
	                entryUsername.Text = result.Text;
	            });
	        };

	        // Navigate to our scanner page
	        await Navigation.PushAsync(scanPage);

        }

	    private void AvviaScansione(object sender, EventArgs e)
	    {
	         scan();
	    }
	}
}