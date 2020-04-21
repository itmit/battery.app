using System;
using System.Threading.Tasks;
using battery.app.Core.Models;
using battery.app.Core.Repositories;
using battery.app.Core.Services;
using battery.app.Core.ViewModels.Battery;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels.ShipmentViewModels
{
	public class ShipmentDetailViewModel: MvxViewModel<Shipment>
	{
		private IMvxNavigationService _navigationService;

		public Shipment Shipment
		{
			get;
			private set;
		}

		private Dealer _dealer;
		private MvxObservableCollection<Models.Battery> _batteries;
		private IShipmentService _shipmentService;
		private Models.Battery _selectedBattery;
		private MvxCommand _closePageCommand;

		public ShipmentDetailViewModel(IMvxNavigationService navigationService, IShipmentService shipmentService)
		{
			_navigationService = navigationService;
			_shipmentService = shipmentService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				Batteries = new MvxObservableCollection<Models.Battery>(await _shipmentService.GetBatteryInShipments(Shipment.Id));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public Models.Battery SelectedBattery
		{
			get => _selectedBattery;
			set
			{
				if (value != null && SetProperty(ref _selectedBattery, value))
				{
					_navigationService.Navigate<BatteryDetailViewModel, Models.Battery>(value);
				}
			}
		}

		public override void Prepare(Shipment parameter)
		{
			Shipment = parameter;
		}

		public MvxCommand ClosePageCommand
		{
			get
			{
				_closePageCommand = _closePageCommand ?? new MvxCommand(() =>
				{
					_navigationService.Close(this);
				});
				return _closePageCommand;
			}
		}

		public MvxObservableCollection<Models.Battery> Batteries
		{
			get => _batteries;
			set => SetProperty(ref _batteries, value);
		}

		public Dealer Dealer
		{
			get => _dealer;
			private set => SetProperty(ref _dealer, value);
		}
	}
}
