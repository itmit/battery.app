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

		[JsonProperty("delivery_note")]
		public string DeliveryNote
		{
			get;
			set;
		}

		[JsonProperty("SSCC")]
		public string Sscc
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

		[JsonProperty("CustomerOrderNumber")]
		public string CustomerOrderNumber
		{
			get;
			set;
		}

		[JsonProperty("Customer_Buyer")]
		public string CustomerBuyer
		{
			get;
			set;
		}

		[JsonProperty("Customer_Receiver")]
		public string CustomerReceiver
		{
			get;
			set;
		}
	}
}
