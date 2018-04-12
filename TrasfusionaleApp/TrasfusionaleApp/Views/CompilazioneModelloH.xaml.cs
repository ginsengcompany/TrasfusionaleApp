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
	public partial class CompilazioneModelloH : ContentPage
	{
	    private Operatore operatore;
		public CompilazioneModelloH (Operatore operatore)
		{
			InitializeComponent ();
		    this.operatore = operatore;
		}

	    private void InvioDati(object sender, EventArgs e)
	    {
	        if (entryConfermaPassword.Text == operatore.password)
	        {
	            if (operatore.codice_operatore != 1)
	                Navigation.PushAsync(new LoginTrasfusionale(operatore));
	            else
	            {
	                DisplayAlert("xxx", "xxxx", "xxx");
	            }
            }
            else
	        {
	            DisplayAlert("Attenzione", "password non corretta", "ok");
	        }
	    }
	}
}