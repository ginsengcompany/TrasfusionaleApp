using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Model;
using TrasfusionaleApp.ModelView;
using TrasfusionaleApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrasfusionaleApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaTrasfusionale : ContentPage
	{
        private Operatore operatore;
        private ListaTrasfusionaleModelView modelView;

		public ListaTrasfusionale (Operatore operatore)
		{
			InitializeComponent ();
            this.operatore = operatore;
            modelView = new ListaTrasfusionaleModelView(operatore);
            BindingContext = modelView;
		}

        private void sceltaReparto(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if(picker.SelectedIndex > -1)
                modelView.recuperaListaTrasfusionali(picker.SelectedIndex);
        }
    }
}