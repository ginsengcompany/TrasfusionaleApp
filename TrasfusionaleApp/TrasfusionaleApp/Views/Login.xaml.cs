using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public Login ()
		{
			InitializeComponent ();
		}

	    private async void AvvioLogin(object sender, EventArgs e)
	    {
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
	                DisplayAlert("Scanned Barcode", result.Text, "OK");
	            });
	        };

	        // Navigate to our scanner page
	        await Navigation.PushAsync(scanPage);

        }

    }
}