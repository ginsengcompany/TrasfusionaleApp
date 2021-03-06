﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrasfusionaleApp.Model;
using TrasfusionaleApp.Views;
using Xamarin.Forms;

namespace TrasfusionaleApp
{
	public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new Login());
            if (App.Current.Properties.ContainsKey("struttura"))
                App.Current.Properties["struttura"] = "150021";
            else
                App.Current.Properties.Add("struttura", "150021");
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
