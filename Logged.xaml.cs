using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plamen
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Logged : ContentPage
	{
		public Logged (Person person)
		{
			InitializeComponent ();

            ResultLabel.Text = $"Welcome {person.Name}";
        }

        private async void BtnOut_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}