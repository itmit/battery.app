using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages
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
			
		}

		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
	}
}