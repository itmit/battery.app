using System;
using System.Threading.Tasks;
using battery.app.Core.Models;
using battery.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels.ShipmentViewModels
{
	/// <summary>
	/// Представляет модель представления для страницы списка отгрузок.
	/// </summary>
	public class ShippingListViewModel : MvxNavigationViewModel
	{
		private IShipmentService _shipmentService;

		public User User { get; }

		private MvxObservableCollection<Shipment> _shipments;
		private Shipment _selectedShipment;
		private IMvxNavigationService _navigationService;

		public ShippingListViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IShipmentService shipmentService, IAuthService authService)
			: base(logProvider, navigationService)
		{
			_navigationService = navigationService;
			_shipmentService = shipmentService;
			User = authService.User;
			_shipmentService.CreatedShipment += async (sender, args) =>
			{
				try
				{
					Shipments = new MvxObservableCollection<Shipment>(
						await _shipmentService.GetShipments());
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			};
		}

		public override async Task Initialize()
		{
			await base.Initialize();
			try
			{
				Shipments = new MvxObservableCollection<Shipment>(
					await _shipmentService.GetShipments());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public MvxObservableCollection<Shipment> Shipments
		{
			get => _shipments;
			set => SetProperty(ref _shipments, value);
		}

		public Shipment SelectedShipment
		{
			get => _selectedShipment;
			set
			{
				SetProperty(ref _selectedShipment, value);
				if (value != null)
				{
					_navigationService.Navigate<ShipmentDetailViewModel, Shipment>(value);
				}
			}
		}
	}
}
