using System;
using System.Threading.Tasks;
using System.Windows.Input;
using battery.app.Core.Models;
using battery.app.Core.Properties;
using battery.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace battery.app.Core.ViewModels.ShipmentViewModels
{
	/// <summary>
	/// Представляет модель страницы создания отгрузки.
	/// </summary>
	public class ShippingCreateViewModel : MvxViewModel<Dealer, bool>
	{
		#region Data
		#region Fields
		/// <summary>
		/// Команда для закрытия страницы.
		/// </summary>
		private MvxCommand _closePageCommand;

		/// <summary>
		/// Дилер.
		/// </summary>
		private Dealer _dealer;

		/// <summary>
		/// Отсканированные товаров.
		/// </summary>
		private MvxObservableCollection<Goods> _goods = new MvxObservableCollection<Goods>();

		/// <summary>
		/// Сервис для навигации.
		/// </summary>
		private readonly IMvxNavigationService _navigationService;

		/// <summary>
		/// Команда открытия подтверждения отгрузки.
		/// </summary>
		private MvxCommand _openShippingConfirmPage;

		/// <summary>
		/// Команда для открытия сканера QR кода товара.
		/// </summary>
		private ICommand _scanGoodsCommand;

		/// <summary>
		/// Сервис для получения информации о батареях.
		/// </summary>
		private readonly IShipmentService _shipmentService;
		private ISettingsHelper _settingsHelper;
		private Shipment _shipment;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ShippingCreateViewModel" />.
		/// </summary>
		/// <param name="navigationService">Сервис для навигации.</param>
		/// <param name="shipmentService">Сервис для получения информации о товарах.</param>
		public ShippingCreateViewModel(IMvxNavigationService navigationService, IShipmentService shipmentService, ISettingsHelper settingsHelper)
		{
			_navigationService = navigationService;
			_shipmentService = shipmentService;
			_settingsHelper = settingsHelper;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Возвращает команду для закрытия страницы.
		/// </summary>
		public ICommand ClosePageCommand
		{
			get
			{
				_closePageCommand = _closePageCommand ?? new MvxCommand(ClosePage);
				return _closePageCommand;
			}
		}

		/// <summary>
		/// Возвращает товары в отгрузке.
		/// </summary>
		public MvxObservableCollection<Goods> Goods
		{
			get => _goods;
			private set => SetProperty(ref _goods, value);
		}

		/// <summary>
		/// Возвращает команду для открытия страницы подтверждения отгрузки.
		/// </summary>
		public ICommand OpenShippingConfirmPage
		{
			get
			{
				_openShippingConfirmPage = _openShippingConfirmPage ?? new MvxCommand(OpenShippingConfirm);
				return _openShippingConfirmPage;
			}
		}

		/// <summary>
		/// Возвращает команду для открытия сканера товаров.
		/// </summary>
		public ICommand ScanGoodsCommand
		{
			get
			{
				_scanGoodsCommand = _scanGoodsCommand ?? new MvxCommand(ScanGoods);
				return _scanGoodsCommand;
			}
		}
		#endregion

		#region Overrided
		/// <summary>
		/// Подготавливает параметры подели.
		/// </summary>
		/// <param name="parameter">Дилер.</param>
		public override void Prepare(Dealer parameter)
		{
			_dealer = parameter;
		}
		#endregion

		#region Private
		/// <summary>
		/// Закрывает страницу.
		/// </summary>
		private void ClosePage()
		{
			_navigationService.Close(this);
		}

		/// <summary>
		/// Открывает страницу подтверждения отгрузки.
		/// </summary>
		private async void OpenShippingConfirm()
		{
			if (_goods.Count == 0)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					await Application.Current.MainPage.DisplayAlert(Strings.Alert,
																	"Для создания отгрузки добавите хотя бы одну батарею",
																	Strings.Ok);
				});
				return;
			}

			_shipment = _shipment == null ? new Shipment(_goods, _dealer) : new Shipment(_goods, _shipment.Dealer);

			var result = await _navigationService.Navigate<ShippingConfirmViewModel, Shipment, bool>(_shipment);

			if (result)
			{
				await Task.Run(() =>
				{
					_navigationService.Close(this, true);
				});
			}
		}

		private async Task<bool> CheckStoragePermission()
		{
			try
			{
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
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

				status = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
				return status == PermissionStatus.Granted;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return false;
		}

		/// <summary>
		/// Вызывает при успешном сканировании товара и добавляет код товара в отгрузку.
		/// </summary>
		/// <param name="result">Результат сканирования.</param>
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

			var newCollection = new MvxObservableCollection<Goods>();
			foreach (var goodsCode in Goods)
			{
				newCollection.Add(goodsCode);
			}

			newCollection.Add(goods);
			Goods = newCollection;

			await Application.Current.MainPage.Navigation.PopModalAsync();
		}

		/// <summary>
		/// Открывает сканирование товара.
		/// </summary>
		private async void ScanGoods()
		{
			var page = new ZXingScannerPage();
			page.OnScanResult += PageOnOnScanResult;

			if (await CheckStoragePermission())
			{
				await Application.Current.MainPage.Navigation.PushModalAsync(page);
			}
		}
		#endregion
	}
}
