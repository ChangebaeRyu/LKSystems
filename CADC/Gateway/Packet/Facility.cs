using System;
using System.Collections.Generic;
using System.Linq;

namespace Gateway
{
	public enum FacilityType { Prevention = 0, Discharge = 1}
	/// <summary>
	/// 시설 Record
	/// </summary>
	class Facility
	{
		public FacilityType FacType { get; set; }                   // 시설구분(방지시설, 배출시설)
		public string FcCode { get; set; }							// 시설코드(방지시설, 배출시설)
		public List<MeasureItem> MeasureItems { get; set; }        // 항목 리스트
		public int Length { get; set; }
		public Facility(FacilityType facType)
		{
			FacType = facType;
			FcCode = "";
			MeasureItems = new List<MeasureItem>();
			Length = 0;
		}

		public byte[] GetBytes()
		{
			List<byte> arrData = new List<byte>();
			if(FacType == FacilityType.Prevention)
				arrData.AddRange(Utils.GetBytes(FcCode.PadRight(4, ' ')));
			else
				arrData.AddRange(Utils.GetBytes(FcCode.PadRight(8, ' ')));
			arrData.Add(Convert.ToByte(MeasureItems.Count));
			foreach (MeasureItem item in MeasureItems)
			{
				arrData.AddRange(item.GetBytes());
			}
			byte[] data = arrData.OfType<byte>().ToArray();
			Length = data.Length;
			return data;
		}
	}

}
