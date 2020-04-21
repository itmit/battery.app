using System;
using System.Linq;
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
		private MvxObservableCollection<Models.Battery> _batteries = new MvxObservableCollection<Models.Battery>();

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
		private Shipment _shipment;
		private readonly IPermissionsService _permissionsService;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ShippingCreateViewModel" />.
		/// </summary>
		/// <param name="navigationService">Сервис для навигации.</param>
		/// <param name="shipmentService">Сервис для получения информации о товарах.</param>
		public ShippingCreateViewModel(IMvxNavigationService navigationService, IShipmentService shipmentService, IPermissionsService permissionsService)
		{
			_navigationService = navigationService;
			_shipmentService = shipmentService;
			_permissionsService = permissionsService;
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


		public event EventHandler BatteryAdded;

		/// <summary>
		/// Возвращает товары в отгрузке.
		/// </summary>
		public MvxObservableCollection<Models.Battery> Batteries
		{
			get => _batteries;
			private set => SetProperty(ref _batteries, value);
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
				_scanGoodsCommand = _scanGoodsCommand ?? new MvxCommand(async () =>
				{
					if (await _permissionsService.CheckPermission(Permission.Camera, "Для сканирования QR-кода необходимо разрешение на использование камеры."))
					{
						string result = await _navigationService.Navigate<ScannerViewModel, object, string>(null);

						if (Batteries.Any(bat => bat.SerialNumber.Equals(result)))
						{
							Device.BeginInvokeOnMainThread(async () =>
							{
								await Application.Current.MainPage.DisplayAlert(Strings.Alert, "Батарея уже добавлена в отгрузку.", Strings.Ok);
							});
							return;
						}

						var goods = await _shipmentService.CheckGoods(result);

						if (goods == null)
						{
							Device.BeginInvokeOnMainThread(async () =>
							{
								await Application.Current.MainPage.DisplayAlert(Strings.Alert, "Батарея не найдена.", Strings.Ok);
							});
							return;
						}

						var newCollection = new MvxObservableCollection<Models.Battery>();
						foreach (var goodsCode in Batteries)
						{
							newCollection.Add(goodsCode);
						}

						newCollection.Add(goods);
						Batteries = newCollection;
						BatteryAdded?.Invoke(this, EventArgs.Empty);
					}
				});
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
			if (_batteries.Count == 0)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					await Application.Current.MainPage.DisplayAlert(Strings.Alert,
																	"Для создания отгрузки добавьте хотя бы одну батарею",
																	Strings.Ok);
				});
				return;
			}

			_shipment = _shipment == null ? new Shipment(_batteries, _dealer) : new Shipment(_batteries, _shipment.Dealer);

			await _navigationService.Navigate<ShippingConfirmViewModel, Shipment>(_shipment);
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

			

			await Application.Current.MainPage.Navigation.PopModalAsync();
		}

		#endregion
	}
}
