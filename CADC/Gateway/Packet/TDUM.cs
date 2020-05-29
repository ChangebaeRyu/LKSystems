using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gateway
{
	class TDUM
	{
		public string WorkPlaceCode { get; set; }       // 사업장코드
		public string ChimneyCode { get; set; }         // 굴뚝코드
		public int Length { get; set; }                 // 전체 길이
		public List<TDUMFac> ListPreFc { get; set; }       // 방지시설 목록
		public List<TDUMFac> ListDisFc { get; set; }       // 배출시설 목록

		public TDUM()
		{
			WorkPlaceCode = "";
			ChimneyCode = "";
			Length = 0;
			ListPreFc = new List<TDUMFac>();
			ListDisFc = new List<TDUMFac>();
		}

		public byte[] GetBytes()
		{
			List<byte> header = new List<byte>();
			List<byte> body = new List<byte>();
			List<byte> arrData = new List<byte>();

			header.AddRange(Utils.GetBytes(CommConst.CMD_TDUM));
			header.AddRange(Utils.GetBytes(CommConst.WORK_CODE));
			header.AddRange(Utils.GetBytes(CommConst.CHIM_CODE));
			header.AddRange(BitConverter.GetBytes(Length));    // 전체길이

			body.AddRange(BitConverter.GetBytes(Convert.ToUInt16(ListPreFc.Count)));
			foreach (TDUMFac preFc in ListPreFc)
			{
				body.AddRange(preFc.GetBytes());
			}
			body.AddRange(BitConverter.GetBytes(Convert.ToUInt16(ListDisFc.Count)));
			foreach (TDUMFac disFc in ListDisFc)
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

	/// <summary>
	/// 미전송 데이터 Body
	/// </summary>
	class TDUMFac
	{
		public DateTime MeasureDT { get; set; }       // 측정일시
		public List<Facility> ListPreFc { get; set; }   // 방지시설 리스트
		public List<Facility> ListDisFc { get; set; }   // 배출시설 리스트

		public TDUMFac()
		{
			MeasureDT = new DateTime();
			ListPreFc = new List<Facility>();
			ListDisFc = new List<Facility>();
		}

		public byte[] GetBytes()
		{
			List<byte> arrData = new List<byte>();
			arrData.AddRange(Utils.GetBytes(MeasureDT.ToString("yyMMddHHmm")));
			foreach(Facility fac in ListPreFc)
			{
				arrData.AddRange(fac.GetBytes());
			}

			byte[] data = arrData.OfType<byte>().ToArray();
			return data;
		}
	}

}
