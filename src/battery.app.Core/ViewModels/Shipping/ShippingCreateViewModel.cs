using System.Windows.Input;
using battery.app.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace battery.app.Core.ViewModels.Shipping
{
	/// <summary>
	/// Представляет модель страницы создания отгрузки.
	/// </summary>
	public class ShippingCreateViewModel : MvxViewModel<Dealer>
	{
		/// <summary>
		/// Команда для добавления товара в отгрузку.
		/// </summary>
		private ICommand _addGoodsCommand;

		/// <summary>
		/// Сервис для навигации.
		/// </summary>
		private readonly IMvxNavigationService _navigationService;

		/// <summary>
		/// Команда для открытия сканера QR кода товара.
		/// </summary>
		private ICommand _scanGoodsCommand;

		/// <summary>
		/// Коды отсканированных товаров.
		/// </summary>
		private MvxObservableCollection<string> _goodsCodes = new MvxObservableCollection<string>();

		/// <summary>
		/// Текущее приложение.
		/// </summary>
		private readonly Application _app = Application.Current;
		
		/// <summary>
		/// Команда для закрытия страницы.
		/// </summary>
		private MvxCommand _closePageCommand;

		/// <summary>
		/// Команда открытия подтверждения отгрузки.
		/// </summary>
		private MvxCommand _openShippingConfirmPage;

		/// <summary>
		/// Дилер.
		/// </summary>
		private Dealer _dealer;

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ShippingCreateViewModel" />.
		/// </summary>
		/// <param name="navigationService">Сервис для навигации.</param>
		public ShippingCreateViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;

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
		/// Открывает страницу подтверждения отгрузки.
		/// </summary>
		private void OpenShippingConfirm()
		{
			_navigationService.Navigate<ShippingConfirmViewModel, Shipment>(new Shipment(_goodsCodes, _dealer));
		}

		/// <summary>
		/// Закрывает страницу.
		/// </summary>
		private void ClosePage()
		{
			_navigationService.Close(this);
		}

		/// <summary>
		/// Возвращает команду для добавления товаров в отгрузку.
		/// </summary>
		public ICommand AddGoodsCommand
		{
			get
			{
				_addGoodsCommand = _addGoodsCommand ?? new MvxCommand<string>(AddGoods);
				return _addGoodsCommand;
			}
		}

		/// <summary>
		/// Добавляет код товара в отгрузку.
		/// </summary>
		/// <param name="code">Код товара.</param>
		private void AddGoods(string code)
		{
			GoodsCodes.Add(code);
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

		/// <summary>
		/// Вызывает при успешном сканировании товара и добавляет код товара в отгрузку.
		/// </summary>
		/// <param name="result">Результат сканирования.</param>
		private void PageOnOnScanResult(Result result)
		{
			if (result == null || string.IsNullOrEmpty(result.Text))
			{
				return;
			}

			var newCollection = new MvxObservableCollection<string>();
			foreach (var goodsCode in GoodsCodes)
			{
				newCollection.Add(goodsCode);
			}
			newCollection.Add(result.Text);
			GoodsCodes = newCollection;

			_app.MainPage.Navigation.PopModalAsync();
		}

		/// <summary>
		/// Возвращает коды товаров в отгрузке.
		/// </summary>
		public MvxObservableCollection<string> GoodsCodes
		{
			get => _goodsCodes;
			private set => SetProperty(ref _goodsCodes, value);
		}

		/// <summary>
		/// Подготавливает параметры подели.
		/// </summary>
		/// <param name="parameter">Дилер.</param>
		public override void Prepare(Dealer parameter)
		{
			_dealer = parameter;
		}
	}
}
