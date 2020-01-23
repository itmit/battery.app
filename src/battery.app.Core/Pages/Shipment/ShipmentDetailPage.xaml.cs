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
	public partial class ShipmentDetailPage : MvxContentPage<ShipmentDetailViewModel>
	{
		public ShipmentDetailPage()
		{
			InitializeComponent();
		}

		private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
	}
}