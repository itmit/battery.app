using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace battery.app.Core.DTO
{
	public class ShipmentDto
	{
		[JsonProperty("serials")]
		public List<string> Serials
		{
			get;
			set;
		}

		[JsonProperty("whom")]
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
