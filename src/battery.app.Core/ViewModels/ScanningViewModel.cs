using System;
using System.Threading.Tasks;
using System.Windows.Input;
using battery.app.Core.Models;
using battery.app.Core.Properties;
using battery.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace battery.app.Core.ViewModels
{
	public class ScanningViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private bool _isScanning;
		private MvxCommand _scanCommand;
		private readonly ISettingsHelper _settingsHelper;
		private readonly IShipmentService _shipmentService;
		#endregion
		#endregion

		#region .ctor
		public ScanningViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IShipmentService shipmentService, ISettingsHelper settingsHelper)
			: base(logProvider, navigationService)
		{
			_shipmentService = shipmentService;
			_settingsHelper = settingsHelper;
		}
		#endregion

		#region Properties
		public bool IsNotScanning => !IsScanning;

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

		public ICommand ScanCommand
		{
			get
			{
				_scanCommand = _scanCommand ?? new MvxCommand(EnableScan);
				return _scanCommand;
			}
		}
		#endregion

		#region Public
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
		#endregion

		#region Private
		private async Task<bool> CheckStoragePermission()
		{
			try
			{
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
				if (status == PermissionStatus.Granted)
				{
					return true;
				}

				if (!await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
				{
					Device.BeginInvokeOnMainThread(async () =>
					{
						var answer = await Application.Current.MainPage.DisplayAlert(Strings.Alert,
																					 "Для сканирования батарей необходимо разрешение на использование камеры.",
																					 Strings.Ok,
																					 Strings.Cancel);

						if (answer)
						{
							_settingsHelper.OpenAppSettings();
						}
					});
				}

				status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
				return status == PermissionStatus.Granted;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return false;
		}

		private async void EnableScan()
		{
			var page = new ZXingScannerPage();
			page.OnScanResult += PageOnOnScanResult;

			if (await CheckStoragePermission())
			{
				await Application.Current.MainPage.Navigation.PushModalAsync(page);
			}
		}

		private async void PageOnOnScanResult(Result result)
		{
			if (result == null || string.IsNullOrEmpty(result.Text))
			{
				return;
			}

			var goods = await _shipmentService.CheckGoods("23403");

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
		#endregion
	}
}
