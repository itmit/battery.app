using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace battery.app.Core.DTO
{
	public class GoodsDto
	{
		public Guid Uuid
		{
			get;
			set;
		}

		public string Sscc
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		[JsonProperty("Description")]
		public string DescriptionCategory
		{
			get;
			set;
		}

		public string Article
		{
			get;
			set;
		}

		public string Type
		{
			get;
			set;
		}

		public string Serial
		{
			get;
			set;
		}

		[JsonProperty("SSCC_QUANTITY")]
		public float SsccQuantity
		{
			get;
			set;
		}

		public float Batch
		{
			get;
			set;
		}

		[JsonProperty("DESCRIPTION")]
		public string Description
		{
			get;
			set;
		}

		[JsonProperty("PACKING_DATE")]
		public DateTime PackingDate
		{
			get;
			set;
		}

		[JsonProperty("DISPATCH_DATE")]
		public DateTime DispatchDate
		{
			get;
			set;
		}

		[JsonProperty("Description_2")]
		public string Description2
		{
			get;
			set;
		}

		[JsonProperty("PAYER_CODE")]
		public string PayerCode
		{
			get;
			set;
		}

		[JsonProperty("PAYER_DESCRIPTION")]
		public string PayerDescription
		{
			get;
			set;
		}

		[JsonProperty("RECEIVER_CODE")]
		public string ReceiverCode
		{
			get;
			set;
		}

		[JsonProperty("RECEIVER_DESCRIPTION")]
		public string ReceiverDescription
		{
			get;
			set;
		}

		[JsonProperty("delivery_number")]
		public string DeliveryNumber
		{
			get;
			set;
		}

		[JsonProperty("din_marking")]
		public string DinMarking
		{
			get;
			set;
		}

		[JsonProperty("old_jis_marking")]
		public string OldJisMarking
		{
			get;
			set;
		}

		[JsonProperty("new_jis_marking")]
		public string NewJisMarking
		{
			get;
			set;
		}

		[JsonProperty("short_code")]
		public string ShortCode
		{
			get;
			set;
		}

		public string Ah
		{
			get;
			set;
		}

		public string Rc
		{
			get;
			set;
		}

		public string Box
		{
			get;
			set;
		}

		public string En
		{
			get;
			set;
		}

		[JsonProperty("l_w_h")]
		public string Lwh
		{
			get;
			set;
		}

		public string Bhd
		{
			get;
			set;
		}

		public string Layout
		{
			get;
			set;
		}

		[JsonProperty("weight_wet")]
		public float WeightWet
		{
			get;
			set;
		}

		[JsonProperty("pcs_pallet")]
		public string PcsPallet
		{
			get;
			set;
		}

		public string Remarks
		{
			get;
			set;
		}
	}
}
