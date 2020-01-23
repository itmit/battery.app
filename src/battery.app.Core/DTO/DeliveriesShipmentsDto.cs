using System.Collections.Generic;

namespace battery.app.Core.DTO
{
	public class DeliveriesShipmentsDto
	{
		public List<ShipmentDto> Shipments
		{
			get;
			set;
		}
		public List<DeliveryDto> Deliveries
		{
			get;
			set;
		}
	}
}
