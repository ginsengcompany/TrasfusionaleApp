using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrasfusionaleApp.Views;
using Xamarin.Forms;

namespace TrasfusionaleApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private async void VaiInPaginaTrasfusionale(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new Login(true));
	    }
	}
}
