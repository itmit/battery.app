using battery.app.Core.ViewModels.Battery;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages.Battery
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BatteryDetailPage : MvxContentPage<BatteryDetailViewModel>
	{
		public BatteryDetailPage()
		{
			InitializeComponent();
		}
	}
}