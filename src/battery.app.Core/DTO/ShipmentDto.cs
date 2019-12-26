using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace battery.app.Core.DTO
{
	public class ShipmentDto
	{
		[JsonProperty("serial_numbers")]
		public List<string> GoodsCodes
		{
			get;
			set;
		}

		[JsonProperty("dealer_uuid")]
		public Guid Dealer
		{
			get;
			set;
		}
	}
}
