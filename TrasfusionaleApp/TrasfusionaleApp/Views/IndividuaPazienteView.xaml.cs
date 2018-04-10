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
	public partial class IndividuaPazienteView : ContentPage
	{
        private Operatore operatore = new Operatore();
		public IndividuaPazienteView (Operatore operatore)
		{
			InitializeComponent ();
            this.operatore = operatore;
		}
	}
}