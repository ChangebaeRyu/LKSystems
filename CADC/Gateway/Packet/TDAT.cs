using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gateway
{
	class TDAT
	{
		public string WorkPlaceCode { get; set; }       // 사업장코드
		public string ChimneyCode { get; set; }         // 굴뚝코드
		public int Length { get; set; }                 // 전체 길이
		public DateTime MeasureTime { get; set; }       // 측정일시
		public List<Facility> ListPreFc { get; set; }   // 방지시설 리스트
		public List<Facility> ListDisFc { get; set; }   // 배출시설 리스트

		public TDAT()
		{
			WorkPlaceCode = "";
			ChimneyCode = "";
			Length = 0;
			MeasureTime = new DateTime();
			ListPreFc = new List<Facility>();
			ListDisFc = new List<Facility>();
		}

		public byte[] GetBytes()
		{
			List<byte> header = new List<byte>();
			List<byte> body = new List<byte>();
			List<byte> arrData = new List<byte>();

			header.AddRange(Utils.GetBytes(CommConst.CMD_TDAT));
			header.AddRange(Utils.GetBytes(CommConst.WORK_CODE));
			header.AddRange(Utils.GetBytes(CommConst.CHIM_CODE));
			header.AddRange(BitConverter.GetBytes(Length));    // 전체길이
			header.AddRange(Utils.GetBytes(MeasureTime.ToString("yyMMddHHmm")));

			body.AddRange(BitConverter.GetBytes(Convert.ToUInt16(ListPreFc.Count)));
			foreach (Facility preFc in ListPreFc)
			{
				body.AddRange(preFc.GetBytes());
			}
			body.AddRange(BitConverter.GetBytes(Convert.ToUInt16(ListDisFc.Count)));
			foreach (Facility disFc in ListDisFc)
			{
				body.AddRange(disFc.GetBytes());
			}

			Length = header.Count + body.Count + 4;		// Tailer 포함
			byte[] bCnt = BitConverter.GetBytes(Length);
			for (int i = 0; i < 4; i++)
			{
				header[i + 11] = bCnt[i];
			}

			// arrData Tailer에 chksum을 추가
			arrData.AddRange(header);
			arrData.AddRange(body);

			byte[] data = arrData.OfType<byte>().ToArray();

			Crc16Ccitt crc = new Crc16Ccitt(InitialCrcValue.Zeros);
			int chksum = crc.ComputeChecksum(data);
			arrData.AddRange(BitConverter.GetBytes(chksum));

			data = arrData.OfType<byte>().ToArray();

			return data;
		}
	}

}
