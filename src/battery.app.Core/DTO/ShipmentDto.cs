using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace battery.app.Core.DTO
{
	public class ShipmentDto
	{
		public int? Id
		{
			get;
			set;
		}

		[JsonProperty("serials")]
		public List<string> Serials
		{
			get;
			set;
		}

		[JsonProperty("whom")]
		public Guid? WhomUuid
		{
			get;
			set;
		}

		[JsonProperty("from")]
		public Guid? FromUuid
		{
			get;
			set;
		}

		[JsonProperty("from_client_name")]
		public string FromClientName
		{
			get;
			set;
		}

		[JsonProperty("whom_client_name")]
		public string WhomClientName
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
