using battery.app.Core.ViewModels.Battery;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages.Battery
{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, WrapInNavigationPage = false)]
    public partial class ScanningPage : MvxContentPage<ScanningViewModel>
    {
        public ScanningPage()
        {
            InitializeComponent();
        }
	}
}