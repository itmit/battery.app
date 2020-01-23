using System.Threading.Tasks;
using battery.app.Core.Models;
using battery.app.Core.Repositories;
using battery.app.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels.ShipmentViewModels
{
	public class ShipmentDetailViewModel: MvxViewModel<Shipment>
	{
		private IMvxNavigationService _navigationService;
		private Shipment _shipment;
		private Dealer _dealer;
		private MvxObservableCollection<Goods> _goods;
		private IShipmentService _shipmentService;
		private Goods _selectedGoods;

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
				Goods = new MvxObservableCollection<Goods>(await _shipmentService.GetBatteryInDelivery(delivery));
			}
			else
			{
				Goods = new MvxObservableCollection<Goods>(await _shipmentService.GetBatteryInShipments(_shipment));
			}
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

		public override void Prepare(Shipment parameter)
		{
			_shipment = parameter;
		}

		public MvxObservableCollection<Goods> Goods
		{
			get => _goods;
			set => SetProperty(ref _goods, value);
		}

		public Dealer Dealer
		{
			get => _dealer;
			private set => SetProperty(ref _dealer, value);
		}
	}
}
