using System;
using System.Threading.Tasks;
using System.Windows.Input;
using battery.app.Core.Models;
using battery.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels.ShipmentViewModels
{
	/// <summary>
	/// Представляет модель для страницы отгрузки.
	/// </summary>
	public class ShipmentViewModel : MvxNavigationViewModel
	{
		/// <summary>
		/// Команда для перехода на страницу создания отгрузки.
		/// </summary>
		private MvxCommand _openShippingCreatePageCommand;

		/// <summary>
		/// Сервис для работы с дилерами.
		/// </summary>
		private readonly IDealerService _dealerService;

		/// <summary>
		/// Дилеры.
		/// </summary>
		private MvxObservableCollection<Dealer> _dealers;

		/// <summary>
		/// Выбранной дилер.
		/// </summary>
		private Dealer _selectedDealer;

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ShipmentViewModel" />.
		/// </summary>
		/// <param name="logProvider">Провайдер логов.</param>
		/// <param name="navigationService">Сервис для навигации.</param>
		/// <param name="dealerService">Сервис для получения дилеров.</param>
		public ShipmentViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IDealerService dealerService)
			: base(logProvider, navigationService) =>
			_dealerService = dealerService;

		/// <summary>
		/// Инициализирует данные модели.
		/// </summary>
		/// <returns></returns>
		public override async Task Initialize()
		{
			await base.Initialize();
			try
			{
				Dealers = new MvxObservableCollection<Dealer>(await _dealerService.GetAll());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		/// <summary>
		/// Возвращает команду для перехода на страницу создания отгрузки.
		/// </summary>
		public ICommand OpenShippingCreatePageCommand
		{
			get
			{
				_openShippingCreatePageCommand = _openShippingCreatePageCommand ?? new MvxCommand(OpenShippingCreatePage);
				return _openShippingCreatePageCommand;
			}
		}

		/// <summary>
		/// Возвращает или устанавливает выбранного дилера.
		/// </summary>
		public Dealer SelectedDealer
		{
			get => _selectedDealer;
			set => SetProperty(ref _selectedDealer, value);
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
		/// Открывает страницу создания отгрузки.
		/// </summary>
		private async void OpenShippingCreatePage()
		{
			bool result = await NavigationService.Navigate<ShippingCreateViewModel, Dealer, bool>(SelectedDealer);
		}
	}
}
