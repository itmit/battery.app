using battery.app.Core.Models;
using battery.app.Core.ViewModels;
using battery.app.Core.Views.ViewCell;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages.Shipment
{

	[MvxTabbedPagePresentation(WrapInNavigationPage = false)]
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShipmentListPage : MvxContentPage<ShippingListViewModel>
    {
        public ShipmentListPage()
        {
            InitializeComponent();
		}

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (ViewModel.User.Role == UserRole.Dealer)
			{
				ShipmentListView.ItemTemplate = new DataTemplate(typeof(DealerShipmentViewCell));
			}
			else if (ViewModel.User.Role == UserRole.Stockman)
			{
				ShipmentListView.ItemTemplate = new DataTemplate(typeof(StockmanShipmentViewCell));
			}

		}

		private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
	}
}