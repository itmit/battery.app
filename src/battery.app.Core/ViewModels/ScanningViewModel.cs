
using System.Threading.Tasks;
using System.Windows.Input;
using battery.app.Core.Models;
using battery.app.Core.Properties;
using battery.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace battery.app.Core.ViewModels
{
	public class ScanningViewModel : MvxNavigationViewModel
	{
		private MvxCommand _scanCommand;
		private bool _isScanning;
		private IShipmentService _shipmentService;

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

			Application.Current.MainPage.Navigation.PushModalAsync(page);
		}

		private async void PageOnOnScanResult(Result result)
		{
			if (result == null || string.IsNullOrEmpty(result.Text))
			{
				return;
			}

			var goods = await _shipmentService.CheckGoods(result.Text);

			if (goods == null)
			{
				await Application.Current.MainPage.Navigation.PopModalAsync();
				Device.BeginInvokeOnMainThread(async () =>
				{
					await Application.Current.MainPage.DisplayAlert(Strings.Alert, "Батарея не найдена.", Strings.Ok);
				});
				return;
			}


			/*
			Application.Current.MainPage.Navigation.PopModalAsync();
			Task.Delay(1000);
			*/
			await NavigationService.Navigate<DetailGoodsViewModel, Goods>(goods);
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

		public async void OnResultScan(string resultText)
		{
			if (string.IsNullOrEmpty(resultText))
			{
				return;
			}

			var goods = await _shipmentService.CheckGoods(resultText);

			if (goods == null)
			{
				await Application.Current.MainPage.Navigation.PopModalAsync();
				Device.BeginInvokeOnMainThread(async () =>
				{
					await Application.Current.MainPage.DisplayAlert(Strings.Alert, "Батарея не найдена.", Strings.Ok);
				});
				return;
			}

			await NavigationService.Navigate<DetailGoodsViewModel, Goods>(goods);
		}

		public ScanningViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IShipmentService shipmentService)
			: base(logProvider, navigationService)
		{
			_shipmentService = shipmentService;
		}
	}
}
