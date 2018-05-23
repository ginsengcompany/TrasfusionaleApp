using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TrasfusionaleApp.Model
{
    public class ExtendedViewCell : ViewCell
    {
        /// <summary>

        /// The SelectedBackgroundColor property.

        /// </summary>

        public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create("SelectedBackgroundColor", typeof(Color), typeof(ExtendedViewCell), Color.Default);

        /// <summary>

        /// Gets or sets the SelectedBackgroundColor.

        /// </summary>

        public Color SelectedBackgroundColor

        {

            get { return (Color)GetValue(SelectedBackgroundColorProperty); }

            set { SetValue(SelectedBackgroundColorProperty, value); }

        }
    }
}
