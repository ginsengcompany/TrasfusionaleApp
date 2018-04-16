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
		public Trasfusionale (Operatore infermiere, Operatore medico)
		{
			InitializeComponent ();
            btnFine.IsEnabled = false;
            this.infermiere = infermiere;
            this.medico = medico;
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
            socket = IO.Socket("http://192.168.125.14:3001");
            btnInizia.IsEnabled = false;
            btnFine.IsEnabled = true;
            socket.Emit("uidinfermiere", infermiere.uid);
            socket.Emit("uidmedico", medico.uid);
            animazione = true;
            animazioneGoccia();
            
            
            
        }

        private void fine (object sender, EventArgs e)
        {
            socket.Disconnect();
            btnFine.IsEnabled = false;
            btnInizia.IsEnabled = true;
            animazione = false;
        }
	}
}