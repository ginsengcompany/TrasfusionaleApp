using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Model;
using TrasfusionaleApp.Service;
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

        private async void InvioDati(object sender, EventArgs e)
	    {
	        if (medico == default(Operatore))
	        {
	            if (entryConfermaPassword.Text == infermiere.password)
	            {
	                modelloInf.compatibilitaImmunologica = compatibilitaImmunologica.IsToggled;
	                modelloInf.identificazioneRiceventeRichiesta = identificazioneRiceventeRichiesta.IsToggled;
	                modelloInf.identificazioneRiceventeScan = identificazioneRiceventeScan.IsToggled;
	                modelloInf.ispezioneEmocomponenti = ispezioneEmocomponenti.IsToggled;
	                modelloInf.verificaOgniUnitaDaTrasfondere = verificaOgniUnitaDaTrasfondere.IsToggled;
                    await Navigation.PushAsync(new LoginTrasfusionale(infermiere,modelloInf));
                }
                else
                    await DisplayAlert("Attenzione", "password non corretta", "ok");
            }
            else
	        {
                if (entryConfermaPassword.Text == medico.password)
                {
                    modelloMed.compatibilitaImmunologica = compatibilitaImmunologica.IsToggled;
                    modelloMed.identificazioneRiceventeRichiesta = identificazioneRiceventeRichiesta.IsToggled;
                    modelloMed.identificazioneRiceventeScan = identificazioneRiceventeScan.IsToggled;
                    modelloMed.ispezioneEmocomponenti = ispezioneEmocomponenti.IsToggled;
                    modelloMed.verificaOgniUnitaDaTrasfondere = verificaOgniUnitaDaTrasfondere.IsToggled;
                    InvioModelliCompilati modelliCompilati = new InvioModelliCompilati(modelloInf, modelloMed, infermiere, medico);
                    var result = await modelliCompilati.inviaDati();
                    if (result)
                        await Navigation.PushAsync(new PrimoPassaggioTrasfusione());
                    else
                    {
                        await DisplayAlert("Attenzione", "I modelli compilati non risultano correttamente idonei per proseguire nel processo trasfusionale", "OK");
                        await Navigation.PushModalAsync(new MainPage(infermiere));
                    }
                }
                else
                    await DisplayAlert("Attenzione", "password non corretta", "ok");
            }
	    }

        private class InvioModelliCompilati
        {
            public ModelloHcs modello_medico { get; set; }
            public ModelloHcs modello_infermiere { get; set; }
            public Operatore infermiere { get; set; }
            public Operatore medico { get; set; }
            private readonly string restInvioModelliCompilati = "";

            public InvioModelliCompilati() { }
            public InvioModelliCompilati(ModelloHcs modelloInf, ModelloHcs modelloMed, Operatore infermiere, Operatore medico)
            {
                this.modello_medico = modelloMed;
                this.modello_infermiere = modelloInf;
                this.medico = medico;
                this.infermiere = infermiere;
            }

            public async Task<bool> inviaDati()
            {
                REST<InvioModelliCompilati, bool> connessioneModelliCompilati = new REST<InvioModelliCompilati, bool>();
                var result = await connessioneModelliCompilati.PostJson(restInvioModelliCompilati, this);
                if (connessioneModelliCompilati.responseMessage != System.Net.HttpStatusCode.OK)
                    return false;
                else
                    return true;
            }
        }
	}
}