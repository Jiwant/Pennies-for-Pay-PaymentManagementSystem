using System;

namespace PenniesForPay
{
	[Serializable]
	class DayRec
	{
		public int DayNum { get; set; }
		public bool Pres { get; set; }
		public Decimal DayOvr { get; set; }
		public Decimal DaySal { get; set; }
		public Decimal DayCumSal =0;

		public DayRec(int d,bool p, int o, decimal ds, decimal dcs)
		{
			DayNum = d;
			Pres = p;
			DayOvr = o;
			DaySal = ds;
			DayCumSal = dcs;
		}

		public override String ToString()
		{
			return (DayNum.ToString() + " " +(Pres?"Present":"Absent")+ " " + DayOvr.ToString() + " " + DaySal.ToString() +  " " + DayCumSal.ToString());
		}
	}
}