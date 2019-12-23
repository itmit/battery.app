﻿
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace battery.app.Core.ViewModels
{
	public class ScanningViewModel : MvxNavigationViewModel
	{
		private MvxCommand _scanCommand;
		private bool _isScanning;
		private App _app = App.Current;

		public ICommand ScanCommand
		{
			get
			{
				_scanCommand = _scanCommand ?? new MvxCommand(EnableScan);
				return _scanCommand;
			}
		}

		private void EnableScan()
		{
			var page = new ZXingScannerPage();
			page.OnScanResult += PageOnOnScanResult;

			_app.MainPage.Navigation.PushModalAsync(page);
		}

		private void PageOnOnScanResult(Result result)
		{
			if (result == null || string.IsNullOrEmpty(result.Text))
			{
				return;
			}

			_app.MainPage.Navigation.PopModalAsync();
			Task.Delay(1000);
			NavigationService.Navigate<DetailGoodsViewModel>(result.Text);
		}

		public bool IsScanning
		{
			get => _isScanning;
			set
			{
				if (SetProperty(ref _isScanning, value))
				{
					RaisePropertyChanged(() => IsNotScanning);
				}
			}
		}

		public bool IsNotScanning
		{
			get => !IsScanning;
		}

		public void OnResultScan(string resultText)
		{
			NavigationService.Navigate<DetailGoodsViewModel>(resultText);
		}

		public ScanningViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
	}
}
