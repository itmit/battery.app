using System;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ZXing;

namespace battery.app.Core.ViewModels
{
	public class ScannerViewModel: MvxViewModelResult<string>
	{
		private readonly IMvxNavigationService _navigationService;

		public ScannerViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;

		public void OnScanResult(Result result)
		{
			_navigationService.Close(this, result.Text);
		}
	}
}
