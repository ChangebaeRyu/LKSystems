using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gateway
{
    class PDUM
    {
        public string Cmd { get; set; }
        public string WorkPlaceCode { get; set; }       // 사업장코드
        public string ChimneyCode { get; set; }         // 굴뚝코드
        public int Length { get; set; }                 // 전체 길이
        public DateTime StartDate { get; set; }         // 시작일시
        public DateTime EndDate { get; set; }         // 종료일시
		public int Chkmum { get; set; }             // 오류 검증코드

        public bool SetBytes(byte[] data)
        {
            Cmd = Utils.GetString(data, 0, 4);
            WorkPlaceCode = Utils.GetString(data, 4, 7);
            ChimneyCode = Utils.GetString(data, 11, 4);
            Length = Convert.ToInt32(Utils.GetString(data, 15, 4));
            StartDate = DateTime.ParseExact(Utils.GetString(data, 19, 10), "yyMMddHHmm", null);
            EndDate = DateTime.ParseExact(Utils.GetString(data, 29, 10), "yyMMddHHmm", null);
            UInt32 chksum = BitConverter.ToUInt32(data, 39);

            byte[] chkData = new byte[data.Length - 4];
            Crc16Ccitt crc = new Crc16Ccitt(InitialCrcValue.Zeros);
            if (chksum == crc.ComputeChecksum(chkData))
                return true;
            else
                return false;
        }
    }
}
