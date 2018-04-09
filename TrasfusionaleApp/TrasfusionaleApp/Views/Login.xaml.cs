using System;
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
        Operatore operatore = new Operatore();
		public Login ()
		{
			InitializeComponent ();
		}

	    private async void AvvioLogin(object sender, EventArgs e)
	    {
	        operatore.uid = entryUsername.Text;
	        operatore.password = entryPassword.Text;
            operatore.Login();
            await  Navigation.PushAsync(new IndividuaPazienteView());
          
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