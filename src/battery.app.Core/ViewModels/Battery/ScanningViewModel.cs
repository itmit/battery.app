using System;
using System.Collections.Generic;
using System.Windows.Input;
using battery.app.Core.Properties;
using battery.app.Core.Services;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace battery.app.Core.ViewModels.Battery
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
					if (!await _permissionsService.CheckPermission(Permission.Camera, "Для сканирования батарей необходимо разрешение на использование камеры."))
					{
						return;
					}

					var result = await NavigationService.Navigate<ScannerViewModel, object, string>(null);
					if (string.IsNullOrEmpty(result))
					{
						return;
					}

					Models.Battery battery = null;

					try
					{
						battery = await _shipmentService.CheckGoods(result);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						Crashes.TrackError(e, new Dictionary<string, string> {
							{ "Sender", GetType().FullName }
						});
					}

					if (battery == null)
					{
						Device.BeginInvokeOnMainThread(async () =>
						{
							await Application.Current.MainPage.DisplayAlert(Strings.Alert, "Батарея не найдена.", Strings.Ok);
						});
						return;
					}

					await NavigationService.Navigate<BatteryDetailViewModel, Models.Battery>(battery);
				});
				return _scanCommand;
			}
		}
		#endregion
	}
}
