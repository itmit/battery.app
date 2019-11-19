using System;
using battery.app.Core.ViewModels;
using battery.app.Core.Views;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace battery.app.Core.Pages
{

	[MvxTabbedPagePresentation(WrapInNavigationPage = false)]
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanningPage : MvxContentPage<ScanningViewModel>
    {
        public ScanningPage()
        {
            InitializeComponent();
        }

		private void ZXingScannerView_OnOnScanResult(Result result)
		{
			ViewModel.OnResultScan(result.Text);
		}
	}
}