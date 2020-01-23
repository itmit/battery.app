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
using Xamarin.Forms;

namespace battery.app.Core.ViewModels.ShipmentViewModels
{
	/// <summary>
	/// Представляет модель для страницы подтверждения отгрузки.
	/// </summary>
	public class ShippingConfirmViewModel : MvxViewModel<Shipment, bool>
	{
		#region Data
		#region Fields
		/// <summary>
		/// Команда для закрытия страницы.
		/// </summary>
		private MvxCommand _closePageCommand;
		/// <summary>
		/// Дилеры.
		/// </summary>
		private MvxObservableCollection<Dealer> _dealers;

		/// <summary>
		/// Сервис для работы с дилерами.
		/// </summary>
		private readonly IDealerService _dealerService;

		/// <summary>
		/// Товары в отгрузке.
		/// </summary>
		private MvxObservableCollection<Goods> _goods;

		/// <summary>
		/// Сервис для навигации.
		/// </summary>
		private readonly IMvxNavigationService _navigationService;

		/// <summary>
		/// Выбранной дилер.
		/// </summary>
		private Dealer _selectedDealer;

		/// <summary>
		/// Команда для отправки, создания отгрузки.
		/// </summary>
		private MvxCommand _sendShipmentCommand;

		/// <summary>
		/// Созданная отгрузка.
		/// </summary>
		private Shipment _shipment;

		private readonly IShipmentService _shipmentService;
		private Goods _selectedGoods;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ShippingConfirmViewModel" />.
		/// </summary>
		/// <param name="navigationService">Сервис для навигации.</param>
		/// <param name="dealerService">Сервис для получения дилеров.</param>
		/// <param name="shipmentService">Сервис для получения информации о товарах.</param>
		public ShippingConfirmViewModel(IMvxNavigationService navigationService, IDealerService dealerService, IShipmentService shipmentService)
		{
			_navigationService = navigationService;
			_dealerService = dealerService;
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
		/// Возвращает коллекцию дилеров.
		/// </summary>
		public MvxObservableCollection<Dealer> Dealers
		{
			get => _dealers;
			private set => SetProperty(ref _dealers, value);
		}

		/// <summary>
		/// Возвращает товары в отгрузке.
		/// </summary>
		public MvxObservableCollection<Goods> Goods
		{
			get => _goods;
			private set => SetProperty(ref _goods, value);
		}

		public Goods SelectedGoods
		{
			get => _selectedGoods;
			set
			{
				if (value != null && SetProperty(ref _selectedGoods, value))
				{
					_navigationService.Navigate<DetailGoodsViewModel, Goods>(value);
				}
			}
		}

		/// <summary>
		/// Возвращает или устанавливает выбранного дилера.
		/// </summary>
		public Dealer SelectedDealer
		{
			get => _selectedDealer;
			set
			{
				if (SetProperty(ref _selectedDealer, value))
				{
					_shipment.Dealer = value;
				}
			}
		}

		/// <summary>
		/// Возвращает команду для отправки, создания отгрузки.
		/// </summary>
		public ICommand SendShipmentCommand
		{
			get
			{
				_sendShipmentCommand = _sendShipmentCommand ?? new MvxCommand(SendShipment);
				return _sendShipmentCommand;
			}
		}
		#endregion

		#region Overrided
		/// <summary>
		/// Инициализирует данные модели.
		/// </summary>
		/// <returns></returns>
		public override async Task Initialize()
		{
			await base.Initialize();
			Dealers = new MvxObservableCollection<Dealer>();
			Goods = new MvxObservableCollection<Goods>(_shipment.Goods);
			try
			{
				Dealers = new MvxObservableCollection<Dealer>(await _dealerService.GetAll());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (_shipment.Dealer != null && _shipment.Dealer.Guid != Guid.Empty)
			{
				SelectedDealer = Dealers.Single(d => d.Guid == _shipment.Dealer.Guid);
			}
		}

		/// <summary>
		/// Подготавливает параметры подели.
		/// </summary>
		/// <param name="parameter">Отгрузка.</param>
		public override void Prepare(Shipment parameter)
		{
			_shipment = parameter;
		}
		#endregion

		#region Private
		/// <summary>
		/// Закрывает страницу.
		/// </summary>
		private void ClosePage()
		{
			_navigationService.Close(this, false);
		}

		/// <summary>
		/// Отправляет и создает отгрузку.
		/// </summary>
		private async void SendShipment()
		{
			var result = false;

			if (_shipment.Dealer == null)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					await Application.Current.MainPage.DisplayAlert(Strings.Alert, "Выберите дилера.", Strings.Ok);
				});
				return;
			}

			try
			{
				result = await _shipmentService.Store(_shipment);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (result)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					await Application.Current.MainPage.DisplayAlert(Strings.Alert, "Отгрузка создана.", Strings.Ok);
				});
				await _navigationService.Close(this, true);
				return;
			}

			Device.BeginInvokeOnMainThread(async () =>
			{
				await Application.Current.MainPage.DisplayAlert(Strings.Alert, "Не удалось создать отгрузку.", Strings.Ok);
			});
		}
		#endregion
	}
}
