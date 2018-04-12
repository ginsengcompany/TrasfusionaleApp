using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Model;
using TrasfusionaleApp.Views;
using Xamarin.Forms;

namespace TrasfusionaleApp
{
	public partial class MainPage : ContentPage
	{
        private Operatore operatore;

		public MainPage(Operatore operatore)
		{
			InitializeComponent();
            this.operatore = operatore;
		}

        private async void IndividuaIlPaziente(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IndividuaPazienteView(operatore));
        }

	    private async void VaiInPaginaTrasfusionale(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new CompilazioneModelloH(operatore));

	    }

	    private async void vaiPaginaPretrasfusionale(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new PreTrasfusionale(operatore));
	    }
    }
}
