using battery.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages
{

	[MvxTabbedPagePresentation(WrapInNavigationPage = false)]
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShippingListPage : MvxContentPage<ShippingListViewModel>
    {
        public ShippingListPage()
        {
            InitializeComponent();
        }

		private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
	}
}