using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using battery.app.Core.ViewModels.ShipmentViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages.Shipment
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShipmentConfirmPage : MvxContentPage<ShippingConfirmViewModel>
	{
		public ShipmentConfirmPage()
		{
			InitializeComponent();
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			ViewModel.SelectedBattery = (sender as View)?.BindingContext as Models.Battery;
		}
	}
}