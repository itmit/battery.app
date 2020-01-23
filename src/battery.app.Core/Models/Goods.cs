using System;

namespace battery.app.Core.Models
{
	public class Goods
	{
		public string SerialNumber
		{
			get;
			set;
		}
		
		public DateTime ProductionDate
		{
			get;
			set;
		}

		public DateTime DeliveryDate
		{
			get;
			set;
		}

		public string[] TabParams
		{
			get;
			set;
		}

		public string CustomerReceiver
		{
			get;
			set;
		}
	}
}
