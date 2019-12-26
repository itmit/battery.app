using System.Collections.Generic;
using System.Threading.Tasks;
using battery.app.Core.Models;

namespace battery.app.Core.Services
{
	public interface IShipmentService
	{
		Task<bool> Store(Shipment shipment);

		Task<Goods> CheckGoods(string code);
	}
}
