using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CADC.Registers
{
    class ROData
    {
        public ROSensor voltSensor { get; set; }	// 40001 ~ 40002
        public ROSensor currSensor { get; set; }	// 40003 ~ 40004
        public ROSensor phSensor { get; set; }		// 40005 ~ 40006
		public ROSensor tempSensor { get; set; }    // 40007 ~ 40008
		public ROSensor[] fanSensor { get; set; }   // 40009 ~ 40010, 11~12, 13~14, 15~16
		public ROSensor[] difSensor { get; set; }   // 40017 ~ 40018, 19~20, 21~22, 23~24
		public ROSensor spSensor { get; set; }      // 40025 ~ 40026
		public ROValue doorVal { get; set; }		// 40027
        public ROValue airCntVal { get; set; }      // 40028
		public ROValue airTotVal { get; set; }      // 40029
		public ROValue motTotVal { get; set; }     // 40030
		public ROValue statVal1 { get; set; }		// 40031
		public ROValue statVal2 { get; set; }		// 40032
		public ROValue statAirVal { get; set; }     // 40033
        public ROValue damStatVal { get; set; }     // 40034 Damper 상태
        public ROValue damMonVal { get; set; }      // 40035 흡기댐퍼 모니터링
        public ROValue fanVoltVal { get; set; }     // 40036 Fan 소모전력
        public ROValue FwVersion { get; set; }
        public ROValue FwChkSum { get; set; }
        public ROValue spVal7 { get; set; }
        public ROValue spVal8 { get; set; }
		public ROValue spVal9 { get; set; }
		public ROValue spVal10 { get; set; }
		public ROValue spVal11 { get; set; }
		public ROValue spVal12 { get; set; }
		public ROValue spVal13 { get; set; }
		public ROValue spVal14 { get; set; }
		public ROValue spVal15 { get; set; }
		public ROValue spVal16 { get; set; }
		public ROValue spVal17 { get; set; }
		public ROValue spVal18 { get; set; }
		public ROSensor sp2Sensor { get; set; }
		public ROSensor sp3Sensor { get; set; }

		public ROData()
        {
            voltSensor = new ROSensor();
            currSensor = new ROSensor();
            phSensor = new ROSensor();
            tempSensor = new ROSensor();
            fanSensor = new ROSensor[4];
            difSensor = new ROSensor[4];
            for (int i = 0; i < 4; i++) fanSensor[i] = new ROSensor();
            for (int i = 0; i < 4; i++) difSensor[i] = new ROSensor();
            spSensor = new ROSensor();
			sp2Sensor = new ROSensor();
			sp3Sensor = new ROSensor();
			doorVal = new ROValue();
            airCntVal = new ROValue();
            airTotVal = new ROValue();
            motTotVal = new ROValue();
            statVal1 = new ROValue();
            statVal2 = new ROValue();
            statAirVal = new ROValue();
            damStatVal = new ROValue();
            fanVoltVal = new ROValue();
			damMonVal = new ROValue();
            FwVersion = new ROValue();
            FwChkSum = new ROValue();
            spVal7 = new ROValue();
            spVal8 = new ROValue();
			spVal9 = new ROValue();
			spVal10 = new ROValue();
			spVal11 = new ROValue();
			spVal12 = new ROValue();
			spVal13 = new ROValue();
			spVal14 = new ROValue();
			spVal15 = new ROValue();
			spVal16 = new ROValue();
			spVal17 = new ROValue();
			spVal18 = new ROValue();
		}
	}

    class ROSensor
    {
        public int hdr { get; set; }
        public int val { get; set; }
		public bool isAlarm { get; set; }
		public bool isMask { get; set; }
        // 차압센서만 사용 함. (Kpa면 0x00, mmH2O이면 0x01 전송) --> GUI에서 Kpa면 /100을 mmH2o면 그냥 사용하면 됨.
        public byte uMulti { get; set; }
		public bool alarmOn { get; set; }
		public String dispVal { get; set; }

        public ROSensor()
        {
            hdr = 0;
            val = 0;
            uMulti = 0;
            isAlarm = false;
            isMask = false;
            alarmOn = false;
            dispVal = "0.0";
        }

        public void SetData(int hdr, int val, bool isDpNoConnect = false)
        {
			Int16 uHdr = (Int16)hdr;
			byte[] bHdr = BitConverter.GetBytes(uHdr);
			this.hdr = (Int16)hdr;
			this.val = (Int16)val;
			isAlarm = (bHdr[1] >> 4) != 0 ? true : false;
			isMask = (bHdr[1] & 0x0f) != 0 ? true : false;
            uMulti = bHdr[0];
            int dPoint = 2;
			alarmOn = isAlarm && !isMask;
            
            if (uMulti == 0x01)         // 차압센서 단위 Kpa
            {
                float calcVal = (float)Math.Round(this.val / 100f, 1);
                dispVal = String.Format("{0:0.0}", calcVal);
            }
            else if (uMulti == 0x05)    // 차압센서 단위 mmH2O
            {
                dispVal = String.Format("{0:0}", this.val);
            }
            else                        // 일반센서
            {
                float calcVal = (float)Math.Round(this.val / Math.Pow(10, dPoint), dPoint);
				if (isDpNoConnect && (this.val == 0xFFFF || this.val == -1))
				{
					dispVal = String.Format("{0}", "No Con");
					alarmOn = false;
				}
				else
					dispVal = String.Format("{0:0.0}", calcVal);
            }
			//switch(decimalPoint)
			//{
			//	case 0:
			//		dispVal = String.Format("{0:0}", calcVal);
			//		break;
			//	case 1:
			//		dispVal = String.Format("{0:0.0}", calcVal);
            //		break;
            //	case 2:
            //		dispVal = String.Format("{0:0.00}", calcVal);
            //		break;
            //	case 3:
            //		dispVal = String.Format("{0:0.000}", calcVal);
            //		break;
            //}
        }

    }

    class ROValue
    {
		private int _valHi;
		private int _valLo;
		private int _val1;
		private int _val2;
		private int _val3;
		private int _val4;

		public int val { get; set; }
		public int valHi
		{
			get
			{
				return _valHi;
			}
			set
			{
				_valHi = value;
				val &= 0x00ff;          // 초기화
				val |= _valHi << 8;     // 설정
			}
		}
		public int valLo
		{
			get
			{
				return _valLo;
			}
			set
			{
				_valLo = value;
				val &= 0xff00;          // 초기화
				val |= _valLo;          // 설정
			}
		}
		public int val4
		{
			get
			{
				return _val4;
			}
			set
			{
				_val4 = value;
				val &= 0x0fff;          // 초기화
				val |= _val4 << 12;     // 설정
			}
		}
		public int val3
		{
			get
			{
				return _val3;
			}
			set
			{
				_val3 = value;
				val &= 0xf0ff;          // 초기화
				val |= _val3 << 8;
			}
		}
		public int val2
		{
			get
			{
				return _val2;
			}
			set
			{
				_val2 = value;
				val &= 0xff0f;          // 초기화
				val |= _val2 << 4;
			}
		}
		public int val1
		{
			get
			{
				return _val1;
			}
			set
			{
				_val1 = value;
				val &= 0xfff0;          // 초기화
				val |= _val1;
			}
		}

		public ROValue()
        {
            val = 0;
        }
        public void SetData(int val)
        {
            this.val = (Int16)val;
			byte[] bytes = BitConverter.GetBytes(val);
			valHi = bytes[1];
			valLo = bytes[0];
			val4 = bytes[1] >> 4;
			val3 = bytes[1] & 0x0f;
			val2 = bytes[0] >> 4;
			val1 = bytes[0] & 0x0f;
		}
	}

}
