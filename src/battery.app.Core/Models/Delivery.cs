using System;

namespace battery.app.Core.Models
{
	public class Delivery : Shipment
	{
		public string Sscc
		{
			get;
			set;
		}

		public string Article
		{
			get;
			set;
		}

		public string Serial
		{
			get;
			set;
		}

		public double SsccQuantity
		{
			get;
			set;
		}

		public int Batch
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public DateTime PackingDate
		{
			get;
			set;
		}

		public DateTime DispatchDate
		{
			get;
			set;
		}

		public string Description2
		{
			get;
			set;
		}

		public string PayerCode
		{
			get;
			set;
		}

		public string PayerDescription
		{
			get;
			set;
		}

		public string ReceiverCode
		{
			get;
			set;
		}

		public string ReceiverDescription
		{
			get;
			set;
		}
	}
}
