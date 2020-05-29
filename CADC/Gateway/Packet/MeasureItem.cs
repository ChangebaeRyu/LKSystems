using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gateway
{
	/// <summary>
	/// 측정 Record
	/// </summary>
	class MeasureItem
	{
		public string ItemCode { get; set; }            // 항목코드
		public float MeasureVal { get; set; }           // 측정값
		public byte Status { get; set; }                // 상태정보
		public int Length { get; set; }

		public MeasureItem()
		{
			ItemCode = "";
			MeasureVal = 0f;
			Status = 0x00;
			Length = 0;
		}

		public byte[] GetBytes()
		{
			List<byte> arrData = new List<byte>();
			arrData.AddRange(Utils.GetBytes(ItemCode.PadRight(4, ' ')));
			arrData.AddRange(Utils.GetBytes(string.Format("{0,6:f2}", MeasureVal)));
			arrData.Add(Status);
			byte[] data = arrData.OfType<byte>().ToArray();
			Length = data.Length;
			return data;
		}
	}
}
