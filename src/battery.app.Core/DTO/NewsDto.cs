using System;
using Newtonsoft.Json;

namespace battery.app.Core.DTO
{
	public class NewsDto
	{
		public string Head
		{
			get;
			set;
		}

		public string Body
		{
			get;
			set;
		}

		public string Picture
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
