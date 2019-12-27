using System.Threading.Tasks;
using System.Windows.Input;
using battery.app.Core.Models;
using battery.app.Core.Properties;
using battery.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
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
		/// Текущее приложение.
		/// </summary>
		private readonly Application _app = Application.Current;

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
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ShippingCreateViewModel" />.
		/// </summary>
		/// <param name="navigationService">Сервис для навигации.</param>
		/// <param name="shipmentService">Сервис для получения информации о товарах.</param>
		public ShippingCreateViewModel(IMvxNavigationService navigationService, IShipmentService shipmentService)
		{
			_navigationService = navigationService;
			_shipmentService = shipmentService;
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
			var result = await _navigationService.Navigate<ShippingConfirmViewModel, Shipment, bool>(new Shipment(_goods, _dealer));

			if (result)
			{
				await Task.Run(() =>
				{
					_navigationService.Close(this, true);
				});
			}
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
		private void ScanGoods()
		{
			var page = new ZXingScannerPage();
			page.OnScanResult += PageOnOnScanResult;

			_app.MainPage.Navigation.PushModalAsync(page);
		}
		#endregion
	}
}
