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
		private readonly IShipmentService _shipmentService;
		private readonly IPermissionsService _permissionsService;
		#endregion
		#endregion

		#region .ctor
		public ScanningViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IShipmentService shipmentService, ISettingsHelper settingsHelper, IPermissionsService permissionsService)
			: base(logProvider, navigationService)
		{
			_shipmentService = shipmentService;
			_permissionsService = permissionsService;
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
				_scanCommand = _scanCommand ?? new MvxCommand(async () =>
				{											  
					if (await _permissionsService.CheckPermission(Permission.Camera, "Для сканирования батарей необходимо разрешение на использование камеры."))
					{
						string result = await NavigationService.Navigate<ScannerViewModel, object, string>(null);
						if (string.IsNullOrEmpty(result))
						{
							return;
						}

						var goods = await _shipmentService.CheckGoods(result);

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
				});
				return _scanCommand;
			}
		}
		#endregion
	}
}
