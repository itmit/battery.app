﻿using System.Threading.Tasks;
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
		private Shipment _shipment;
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

			if (_shipment is Delivery delivery)
			{
				Batteries = new MvxObservableCollection<Models.Battery>(await _shipmentService.GetBatteryInDelivery(delivery));
			}
			else
			{
				Batteries = new MvxObservableCollection<Models.Battery>(await _shipmentService.GetBatteryInShipments(_shipment));
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
			_shipment = parameter;
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
