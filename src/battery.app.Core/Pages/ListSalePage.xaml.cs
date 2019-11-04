using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListSalePage : ContentPage
	{
		public ListSalePage()
		{
			InitializeComponent();
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SalePage(), false);
		}

		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
	}
}