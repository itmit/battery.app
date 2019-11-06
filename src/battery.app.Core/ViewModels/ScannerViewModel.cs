using System.Windows.Input;
using battery.app.Core.Pages;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using ZXing;

namespace battery.app.Core.ViewModels
{
	public class ScannerViewModel: MvxViewModelResult<string>
	{
		private IMvxNavigationService _navigationService;

		public ScannerViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;

		public void Handle_OnScanResult(Result result)
		{
			CloseCompletionSource.SetResult(result.Text);
			_navigationService.Close(this);
		}
	}
}
