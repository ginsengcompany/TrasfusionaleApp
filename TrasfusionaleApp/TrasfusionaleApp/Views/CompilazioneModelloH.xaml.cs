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
	    private Operatore medico, infermiere;
	    private ModelloHcs modelloInf, modelloMed;
		public CompilazioneModelloH (Operatore operatore)
		{
			InitializeComponent ();
		    this.infermiere = operatore;
		}

	    public CompilazioneModelloH(Operatore medico, Operatore infermiere, ModelloHcs modello)
	    {
	        InitializeComponent();
	        this.medico = medico;
	        this.infermiere = infermiere;
	        this.modelloInf = modello;
	        this.modelloMed = modello;

	    }

        private void InvioDati(object sender, EventArgs e)
	    {
	        if (entryConfermaPassword.Text == infermiere.password)
	        {
	            if (infermiere.codice_operatore != 1)
	            {
	                modelloInf.compatibilitaImmunologica = compatibilitaImmunologica.IsToggled;
	                modelloInf.identificazioneRiceventeRichiesta = identificazioneRiceventeRichiesta.IsToggled;
	                modelloInf.identificazioneRiceventeScan = identificazioneRiceventeScan.IsToggled;
	                modelloInf.ispezioneEmocomponenti = ispezioneEmocomponenti.IsToggled;
	                modelloInf.verificaOgniUnitaDaTrasfondere = verificaOgniUnitaDaTrasfondere.IsToggled;

                    Navigation.PushAsync(new LoginTrasfusionale(infermiere,modelloInf));
                }
                else
	            {

	            }
            }
            else
	        {
	            DisplayAlert("Attenzione", "password non corretta", "ok");
	        }
	    }
	}
}