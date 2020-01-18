using System;
using Newtonsoft.Json;

namespace battery.app.Core.DTO
{
	public class GoodsDto
	{
		[JsonProperty("serial_number")]
		public string SerialNumber
		{
			get;
			set;
		}

		[JsonProperty("production_date")]
		public DateTime? ProductionDate
		{
			get;
			set;
		}

		[JsonProperty("delivery_date")]
		public DateTime? DeliveryDate
		{
			get;
			set;
		}

		[JsonProperty("TAB_description")]
		public string TabDescription
		{
			get;
			set;
		}
	}
}
