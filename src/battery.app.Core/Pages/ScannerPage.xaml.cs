using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using battery.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace battery.app.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScannerPage : MvxContentPage<ScannerViewModel>
	{
		public ScannerPage()
		{
			InitializeComponent();
		}

		private void BarcodeScanView_OnOnScanResult(Result result)
		{
			ViewModel.Handle_OnScanResult(result);
		}
	}
}