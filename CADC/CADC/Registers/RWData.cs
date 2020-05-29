using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CADC.Utils;

namespace CADC.Registers
{
	// 집진기 설정 데이더
	class RWData
	{
		// 전압센서
		public RwSensor voltSensor;
		// 전류센서
		public RwSensor currSensor;
		// PH센서
		public RwSensor phSensor;
		// 온도센서
        public TempSenser tempSensor;
        // FAN Sensor(사용안함)
		//public RwSensor[] arrFanSensor;
		// 차압센서
		public RwSensor[] difSensor;
		// 예비센서
		public RwSensor spSensor;
		// 공란센서1,2
		public RwSensor sp2Sensor;
		public RwSensor sp3Sensor;
		// Fan 결선제어
		public FanBreakSensor FBSensor;
		// 센서 Limit
		public SensorLimit LimitVolt;
		public SensorLimit LimitCurr;
		public SensorLimit LimitPH;
		public SensorLimit LimitTemp;
		public SensorLimit LimitFan;
		public SensorLimit LimitDif1;
		public SensorLimit LimitDif2;
		public SensorLimit LimitDif3;
		public SensorLimit LimitDif4;
		public SensorLimit LimitSp;
		// FAN CURR 설정
		public FanCurrSet FanCurr1;
		public FanCurrSet FanCurr2;
		public FanCurrSet FanCurr3;
		public FanCurrSet FanCurr4;

		// 메인화면 Control
		// 42040 Start 별도 저장 없음
		// 42041 Door / Buzzer Mask
		public RWValue DrBzVal;
		// 42042 Control
		public RWValue ControlVal;
		// 42043 AIR Count
		public RWValue AirCountVal;
		// 42044 Status
		public RWValue StatusVal;
        // 42015~16 댐퍼설정
        public RW2Regi damperSet;
        // 42017~18 IoT 전송 선택
        public RW2Regi IoTSet;
        
		// 작업장제어 화면
		public int senserTemp1;
		public int senserTemp2;
		public int senserTemp3;
		public int comID1;
		public int comID2;
		public int setTemp1;
		public int setTemp2;
		public int intervalLight;
		public int intervalHeater;
		public int alarmTemp;
		public int intervalAircon;

		public RWData()
		{
			InitData();
		}

		public void InitData(bool bArrayInit = true)
		{
			int i = 0;
			if (bArrayInit)
			{
				voltSensor = new RwSensor();
				currSensor = new RwSensor();
				phSensor = new RwSensor();
				spSensor = new RwSensor();
				sp2Sensor = new RwSensor();
				sp3Sensor = new RwSensor();
				tempSensor = new TempSenser();
				FBSensor = new FanBreakSensor();
				LimitVolt = new SensorLimit();
				LimitCurr = new SensorLimit();
				LimitPH = new SensorLimit();
				LimitTemp = new SensorLimit();
				LimitFan = new SensorLimit();
				LimitDif1 = new SensorLimit();
				LimitDif2 = new SensorLimit();
				LimitDif3 = new SensorLimit();
				LimitDif4 = new SensorLimit();
				LimitSp = new SensorLimit();
				FanCurr1 = new FanCurrSet();
				FanCurr2 = new FanCurrSet();
				FanCurr3 = new FanCurrSet();
				FanCurr4 = new FanCurrSet();
				DrBzVal = new RWValue();
				ControlVal = new RWValue();
				AirCountVal = new RWValue();
				StatusVal = new RWValue();
				//arrFanSensor = new RwSensor[4];
				difSensor = new RwSensor[4];
                for (i = 0; i < 4; i++) difSensor[i] = new RwSensor();
                damperSet = new RW2Regi();
				IoTSet = new RW2Regi();
			}

		}

	}

    class TempSenser
    {
        public int hdrTemp { get; set; }
        public int hdrResi { get; set; }
        public int valKelv { get; set; }
        public float offset { get; set; }
        public bool mask { get; set; }

        public TempSenser()
        {
            hdrTemp = 0;        // 기준온도
            hdrResi = 0;      // 기준저항
            valKelv = 0;         // KELVIN(K)
            offset = 0;         // Offset
            mask = false;       // MASK
        }

        public int[] GetData()
        {
            int[] arrRet = new int[3];
            arrRet[0] = hdrTemp << 8 | hdrResi;
            arrRet[1] = valKelv;
            int tOffset = (int)(Math.Round(offset + 1.00, 2) * 100);
            int tMask = mask ? 0x01 : 0x00;
            arrRet[2] = tOffset << 8 | tMask;
            return arrRet;
        }

        public void SetData(UInt16 uHdr, UInt16 uVal, UInt16 uSub)
        {
            byte[] hdr, sub;
            hdr = BitConverter.GetBytes(uHdr);
            sub = BitConverter.GetBytes(uSub);
            hdrTemp = hdr[1];
            hdrResi = hdr[0];
            valKelv = uVal;
            int tOffset = sub[1];
            int tMask = sub[0];

            offset = (float)Math.Round((float)Math.Round(tOffset / 100f, 2) - 1f, 2);
            mask = tMask == 0 ? false : true;

        }
    }

	class RwSensor
	{
		public const int SENSOR_COMM = 0;		// 일반센서
		public const int SENSOR_DIFF = 1;		// 차압센서
		public const int SENSOR_SPAR = 2;		// 예비센서

		public float hdrMin { get; set; }		// 표시 Min
		public float hdrMax { get; set; }		// 표시 Max
        public float valCen { get; set; }	    // 전압 Center
        public float valMin { get; set; }		// 전압 Min
		public float valMax { get; set; }		// 전압 Max
		public float offset { get; set; }		// Offset
		public int maskVal { get; set; }		// 배수
		public bool mask { get; set; }			// MASK
		public int uMulti { get; set; }			// 차압센서에서만 사용하며 표시 Min, Max를 / 10 하는지 / 100 하는지
		//public bool isMinus { get; set; }		// 예비센서(온도센서)일 경우 dispMin이 무조건 "-"값 출력
		public int sensorType { get; set; }		// 센서타입 0 : 일반센서, 1 : 차압센서, 2 : 예비센서

		// 예비센서 Step 필요 -18 ~ 80 : 98 / 254 = 3.86
		public RwSensor()
		{
			hdrMin = 0;
			hdrMax = 0;
            valCen = 0;
			valMin = 0;
			valMax = 0;
			offset = 0;
			maskVal = 0;
			mask = false;
			uMulti = 0;
			sensorType = SENSOR_COMM;
			//isMinus = false;
		}

		// GUI -> Board
		// SENSOR_DIFF - 0 : (mmH2O 정수), 1 : (Kpa 소수점 1자리)
		public int[] GetData(int dPoint = 1)
		{
			int[] arrData = new int[3];
			int hi, lo;
			//int tPoint = CommonUtil.GetDecimalPoint(dispMin / 2);
			//int oPoint = CommonUtil.GetDecimalPoint(dispMax / 2);

			//if (isMinus && dispMin < 0) dispMin *= -1;  // "-" 값은 전송할땐 "+"로 변환해서 전송

			if(sensorType == SENSOR_COMM)
			{
				hi = (int)(Math.Round(hdrMin / 2, dPoint) * (int)Math.Pow(10, dPoint));   // HDR HI
				lo = (int)(Math.Round(hdrMax / 2, dPoint) * (int)Math.Pow(10, dPoint));   // HDR LO
				arrData[0] = hi << 8 | lo;
			}
			else if (sensorType == SENSOR_DIFF)
			{
				this.uMulti = dPoint;
				// uMulti = 0 : (Kpa 0.0 ~ 20.0)     전송값 hdrMax / 2 * 10
				// uMulti = 1 : (mmH2O 0 ~ 2000)     전송값 hdrMax / 2 * 0.1
                if(uMulti == 0)
				    hi = (int)Math.Round(hdrMax / 2 * 10, 0);   // HDR LO
                else
                    hi = (int)Math.Round(hdrMax / 2 * 0.1, 0);   // HDR LO
                lo = (int)Math.Round(valCen / 2 * 100, 0);   // HDR LO
				arrData[0] = hi << 8 | lo;
			}
			else if (sensorType == SENSOR_SPAR)
			{
                if (hdrMin < 0) hdrMin *= -1;  // "-" 값은 전송할땐 "+"로 변환해서 전송
                hi = (int)hdrMin;	// HDR HI
				lo = (int)hdrMax;	// HDR LO
				arrData[0] = hi << 8 | lo;
			}
			hi = (int)Math.Round(valMin / 2 * 100, 0);   // VAL HI
			lo = (int)Math.Round(valMax / 2 * 100, 0);   // VAL LO
			arrData[1] = hi << 8 | lo;

			hi = (int)(Math.Round(offset + 1.00, 2) * 100);     // SUB HI
            if (sensorType == SENSOR_DIFF)
            {
                if(uMulti == 0)
                    lo = (mask ? 1 : 0) * 100 + 1 * 10 + 1;   // SUB LO(111 or 011)
                else
                    lo = (mask ? 1 : 0) * 100 + 5 * 10 + 5;   // SUB LO(155 or 055)
            }
            else
            {
                lo = (mask ? 1 : 0) * 100 + dPoint * 10 + dPoint;   // SUB LO
            }
			arrData[2] = hi << 8 | lo;

			return arrData;
		}

		// Board -> GUI
		public void SetData(UInt16 uHdr, UInt16 uVal, UInt16 uSub, int sensorType = 0)
		{
			this.sensorType = sensorType;
			//this.isMinus = isMinus;
			byte[] hdr, val, sub;
			hdr = BitConverter.GetBytes(uHdr);
			val = BitConverter.GetBytes(uVal);
			sub = BitConverter.GetBytes(uSub);
			// isMinus : 예비센서 tru 나머지는 false. 예비센서(온도센서) MIN 값은 무조건 "-" 표시
			//int tDispMin = isMinus ? hdr[1] * -1 : hdr[1];
			int tHdrHi = hdr[1];
			int tHdrLo = hdr[0];
			int tValHi = val[1];
			int tValLo = val[0];
			int tOffset = sub[1];
			int oPoint = sub[0];
			int tMask = oPoint / 100;       // 100의 자리 Mask
			oPoint -= tMask * 100;
			int tPoint = oPoint / 10;       // 10의 자리소수점(HDR_MIN 배수)
			oPoint -= tPoint * 10;          // 1의 자리소수점(HDR_MAX 배수)

			if (this.sensorType == SENSOR_COMM)
			{
                // (전압, 전류, PH, Spare) 센서
				this.hdrMin = (float)Math.Round(tHdrHi * 2 / Math.Pow(10, tPoint), tPoint);
				this.hdrMax = (float)Math.Round(tHdrLo * 2 / Math.Pow(10, oPoint), oPoint);
			}
			else if (this.sensorType == SENSOR_DIFF)
			{
				// 차압센서
				// 차압센서 표시 Min Max(단위가 Kpa면 / 10, mmH2O면 / 100)
                if (oPoint == 1)    // oPoint, tPoint = 1
                {
                    uMulti = 0;
                    this.hdrMax = (float)Math.Round(tHdrHi * 2 / 10f, 1);   // 소수점1자리
                }
                else    // oPoint, tPoint = 5
                {
                    uMulti = 1;
                    this.hdrMax = (int)Math.Round(tHdrHi * 2 / 0.1, 0);   // 정수
                }
                //this.hdrMax = tHdrHi * 2 / uMulti;
				this.hdrMin = hdrMax * -1;
                this.valCen = (float)Math.Round(tHdrLo * 2 / 100f, 2);
			}
			else if (this.sensorType == SENSOR_SPAR)
			{
				// 예비센서
				this.hdrMin = tHdrHi * -1;
				this.hdrMax = tHdrLo;
			}
			this.valMin = (float)Math.Round(tValHi * 2 / 100f, 2);
			this.valMax = (float)Math.Round(tValLo * 2 / 100f, 2);
			this.maskVal = sub[0];
			this.offset = (float)Math.Round((float)Math.Round(tOffset / 100f, 2) - 1f, 2);
			this.mask = tMask == 0 ? false : true;

			string strMaskVal = string.Format("{0:0}{1:0}{2:0}", mask ? 1 : 0, tPoint, oPoint);
			maskVal = CommonUtil.String2Int(strMaskVal);
		}
	}

	// 댐퍼설정, IoT 전송
	class RW2Regi
	{
		public RWValue hdr;
		public RWValue val;

		public RW2Regi()
		{
			hdr = new RWValue();
			val = new RWValue();
		}

		public int[] GetData()
		{
			int[] arrData = new int[2];
			arrData[0] = hdr.GetData();
			arrData[1] = val.GetData();

			return arrData;
		}

		public void SetData(int iHdr, int iVal)
		{
			hdr.SetData(iHdr);
			val.SetData(iVal);
		}
	}

	// FAN 결선제어
	class FanBreakSensor
	{
        public int hdrBlnk { get; set; }
		public bool hdrSngYD { get; set; }
		public int valYDDelay { get; set; }
		public int valFanDelay { get; set; }

		public FanBreakSensor()
		{
            hdrBlnk = 0xFF;
			hdrSngYD = false;
			valYDDelay = 0;
			valFanDelay = 0;
		}

		public void SetData(UInt16 uHdr, UInt16 uVal)
		{
			byte[] hdr, val;
			hdr = BitConverter.GetBytes(uHdr);
			val = BitConverter.GetBytes(uVal);
            hdrBlnk = hdr[1];
			hdrSngYD = hdr[0] != 0 ? true : false;
			valYDDelay = val[1];
			valFanDelay = val[0];
		}

		public int[] GetData()
		{
			int[] arrData = new int[2];
            arrData[0] = (hdrBlnk << 4) | (hdrSngYD ? 0x01 : 0x00);
			arrData[1] = valYDDelay << 8 | valFanDelay;
			return arrData;
		}
	}

	// FAN CURR 설정
	class FanCurrSet
	{
		public float hdrCurrMin { get; set; }
		public float hdrCurrMax { get; set; }
		public float valVoltMin { get; set; }
		public float valVoltMax { get; set; }
		public float crStep1 { get; set; }
		public float crStep2 { get; set; }
		public float crStep3 { get; set; }
		public float crStep4 { get; set; }
		public float crStep5 { get; set; }
		public float vtStep1 { get; set; }
		public float vtStep2 { get; set; }
		public float vtStep3 { get; set; }
		public float vtStep4 { get; set; }
		public float vtStep5 { get; set; }
		public float offset { get; set; }
		public bool mask { get; set; }

		public FanCurrSet()
		{
			hdrCurrMin = 0;
			hdrCurrMax = 0;
			valVoltMin = 0;
			valVoltMax = 0;
			crStep1 = 0;
			crStep2 = 0;
			crStep3 = 0;
			crStep4 = 0;
			crStep5 = 0;
			vtStep1 = 0;
			vtStep2 = 0;
			vtStep3 = 0;
			vtStep4 = 0;
			vtStep5 = 0;
			offset = 0;
			mask = false;
		}

		public void SetData(UInt16 uHdr, UInt16 uVal1, UInt16 uVal2, UInt16 uVal3, UInt16 uVal4, UInt16 uVal5, UInt16 uSub, UInt16 uVal6)
		{
            byte[] hdr, val1, val2, val3, val4, val5, sub, val6;
			hdr = BitConverter.GetBytes(uHdr);
			val1 = BitConverter.GetBytes(uVal1);
			val2 = BitConverter.GetBytes(uVal2);
			val3 = BitConverter.GetBytes(uVal3);
			val4 = BitConverter.GetBytes(uVal4);
			val5 = BitConverter.GetBytes(uVal5);
			sub = BitConverter.GetBytes(uSub);
			val6 = BitConverter.GetBytes(uVal6);
			int tOffset = sub[1];
			int oPoint = sub[0];
			int tMask = oPoint / 100;		// 100의 자리 Mask
			oPoint -= tMask * 100;
            int tPoint = oPoint / 10;       // 10의 자리소수점(HDR_MIN 배수)
            oPoint -= tPoint * 10;          // 1의 자리소수점(HDR_MAX 배수)

			this.mask = tMask != 0 ? true : false;
			this.hdrCurrMin = (float)Math.Round(hdr[1] * 2 / Math.Pow(10, tPoint), tPoint);
			this.hdrCurrMax = (float)Math.Round(hdr[0] * 2 / Math.Pow(10, tPoint), tPoint);
			this.crStep1 = (float)Math.Round(val1[1] * 2 / Math.Pow(10, tPoint), tPoint);
			this.crStep2 = (float)Math.Round(val2[1] * 2 / Math.Pow(10, tPoint), tPoint);
			this.crStep3 = (float)Math.Round(val3[1] * 2 / Math.Pow(10, tPoint), tPoint);
			this.crStep4 = (float)Math.Round(val4[1] * 2 / Math.Pow(10, tPoint), tPoint);
			this.crStep5 = (float)Math.Round(val5[1] * 2 / Math.Pow(10, tPoint), tPoint);
			this.vtStep1 = (float)Math.Round(val1[0] * 2 / 100f, 2);
			this.vtStep2 = (float)Math.Round(val2[0] * 2 / 100f, 2);
			this.vtStep3 = (float)Math.Round(val3[0] * 2 / 100f, 2);
			this.vtStep4 = (float)Math.Round(val4[0] * 2 / 100f, 2);
			this.vtStep5 = (float)Math.Round(val5[0] * 2 / 100f, 2);

			this.valVoltMin = (float)Math.Round(val6[1] * 2 / 100f, 2);
			this.valVoltMax = (float)Math.Round(val6[0] * 2 / 100f, 2);

			this.offset = (float)Math.Round((float)Math.Round(tOffset / 100f, 2) - 1f, 2);
		}

		public int[] GetData()
		{
			int[] arrData = new int[8];
			int hi, lo;
			int dPoint = 1;		// 전압은 소수점 1자리까지만
			int tPoint = CommonUtil.GetDecimalPoint(hdrCurrMin);
			hi = (int)Math.Round(hdrCurrMin / 2 * 10, 1);
			lo = (int)Math.Round(hdrCurrMax / 2 * 10, 1);
			arrData[0] = hi << 8 | lo;

			hi = (int)Math.Round(crStep1 / 2 * 10, 1);
			lo = (int)Math.Round(vtStep1 / 2 * 100, 2);
			arrData[1] = hi << 8 | lo;

			hi = (int)Math.Round(crStep2 / 2 * 10, 1);
			lo = (int)(Math.Round(vtStep2 / 2 * 100, 2));
			arrData[2] = hi << 8 | lo;

			hi = (int)Math.Round(crStep3 / 2 * 10, 1);
			lo = (int)(Math.Round(vtStep3 / 2 * 100, 2));
			arrData[3] = hi << 8 | lo;

			hi = (int)Math.Round(crStep4 / 2 * 10, 1);
			lo = (int)(Math.Round(vtStep4 / 2 * 100, 2));
			arrData[4] = hi << 8 | lo;

			hi = (int)Math.Round(crStep5 / 2 * 10, 1);
			lo = (int)(Math.Round(vtStep5 / 2 * 100, 2));
			arrData[5] = hi << 8 | lo;

			hi = (int)(Math.Round(offset + 1.00, 2) * 100);
			lo = (mask ? 1 : 0) * 100 + dPoint * 10 + dPoint;
			arrData[6] = hi << 8 | lo;

			hi = (int)Math.Round(valVoltMin / 2 * 100, 2);
			lo = (int)Math.Round(valVoltMax / 2 * 100, 2);
			arrData[7] = hi << 8 | lo;

			return arrData;
		}
	}

	// 센서 Limit 설정
	class SensorLimit
	{
		public float valLimit { get; set; }
		public bool isLoHi { get; set; }      // 0 : 하한, 1 : 상한

		public SensorLimit()
		{
			valLimit = 0;
			isLoHi = false;	// lo : false, Hi : true;
		}

		public int GetData(int dPoint = 1)
		{
			int uVal = 0;
			int hi, lo;
			int oPoint = CommonUtil.GetDecimalPoint(valLimit);

			hi = (int)(Math.Round(valLimit / 2, dPoint) * Math.Pow(10, dPoint));
			lo = (isLoHi ? 1 : 0) * 100 + dPoint;
			uVal = hi << 8 | lo;

			return uVal;
		}

		public void SetData(UInt16 uVal)
		{
			byte[] val = BitConverter.GetBytes(uVal);
			int oPoint = val[0];
			int tLoHi = oPoint / 100;       // 100의 자리 Mask
			oPoint -= tLoHi * 100;
            int tPoint = oPoint / 10;       // 10의 자리소수점(HDR_MIN 배수)
            oPoint -= tPoint * 10;          // 1의 자리소수점(HDR_MAX 배수)

			this.valLimit = (float)Math.Round(val[1] * 2 / Math.Pow(10, oPoint), oPoint);
			this.isLoHi = tLoHi != 0 ? true : false;
		}

	}

	class RWValue
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
				val &= 0x00ff;			// 초기화
				val |= _valHi << 8;		// 설정
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
				val |= _valLo;			// 설정
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
				val |= _val4 << 12;		// 설정
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

		public RWValue()
		{
			val = 0;
		}
		public void SetData(int val)
		{
			this.val = val;
			UInt16 uVal = Convert.ToUInt16(val);
			byte[] bytes = BitConverter.GetBytes(val);
			valHi = bytes[1];
			valLo = bytes[0];
			val4 = bytes[1] >> 4;
			val3 = bytes[1] & 0x0f;
			val2 = bytes[0] >> 4;
			val1 = bytes[0] & 0x0f;
		}

		public int GetData()
		{
			return val;
		}
	}

}
