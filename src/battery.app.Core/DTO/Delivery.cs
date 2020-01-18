using System;
using Newtonsoft.Json;

namespace battery.app.Core.DTO
{
	public class Delivery
	{
		public Guid Uuid
		{
			get;
			set;
		}
		
		[JsonProperty("delivery_number")]
		public string Number
		{
			get;
			set;
		}

		[JsonProperty("dealer_uuid")]
		public Guid DealerUuid
		{
			get;
			set;
		}
	}
}
