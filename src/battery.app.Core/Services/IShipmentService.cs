using System.Collections.Generic;
using System.Threading.Tasks;
using battery.app.Core.Models;

namespace battery.app.Core.Services
{
	public interface IShipmentService
	{
		Task<bool> Store(Shipment shipment);

		Task<Battery> CheckGoods(string code);

		Task<List<Shipment>> GetDeliveriesAndShipments();

		Task<List<Battery>> GetBatteryInShipments(Shipment shipment);

		Task<List<Battery>> GetBatteryInDelivery(Delivery delivery);
	}
}
