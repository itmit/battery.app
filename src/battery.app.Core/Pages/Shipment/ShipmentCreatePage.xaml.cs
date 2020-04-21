using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using battery.app.Core.Design;
using battery.app.Core.ViewModels.ShipmentViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages.Shipment
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShipmentCreatePage : MvxContentPage<ShippingCreateViewModel>
	{
		public ShipmentCreatePage()
		{
			InitializeComponent();
		}

		private bool _firstTime = true;

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (_firstTime)
			{
				ViewModel.BatteryAdded += (sender, args) =>
				{
					QRs.Children.Insert(0, new Frame
					{
						Margin = 5,
						Style = Application.Current.Resources["FrameShipment"] as Style,
						HeightRequest = 50,
						WidthRequest = 50,
						IsClippedToBounds = true,
						VerticalOptions = LayoutOptions.Center,
						HasShadow = DesignClass.HasShadow,
						Content = new Image
						{
							Source = "qrcode.png",
							Style = Application.Current.Resources["ImageShipment"] as Style
						}
					});
				};
				_firstTime = false;
			}
		}
	}
}