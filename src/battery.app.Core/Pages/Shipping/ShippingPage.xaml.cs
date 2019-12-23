using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using battery.app.Core.ViewModels.Shipping;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages.Shipping
{
	[MvxTabbedPagePresentation(WrapInNavigationPage = false)]
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShippingPage : MvxContentPage<ShippingViewModel>
	{
		public ShippingPage()
		{
			InitializeComponent();
		}
	}
}