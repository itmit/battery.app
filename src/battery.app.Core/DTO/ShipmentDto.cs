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

		[JsonProperty("uuid")]
		public Guid Guid
		{
			get;
			set;
		}

		[JsonProperty("created_at")]
		public DateTime? CreatedAt
		{
			get;
			set;
		}
	}
}
