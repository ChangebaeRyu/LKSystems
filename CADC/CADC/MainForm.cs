using CADC.Modbus;
using CADC.Properties;
using CADC.Registers;
using CADC.SubForm;
using CADC.Utils;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CADC
{
	public partial class MainForm : Form
	{
		//private const string VERSION = "Ver. 2004210900";
		//private const string VERSION = "Ver. 2004211546";
		//private const string VERSION = "Ver. 2004222200";
		//private const string VERSION = "Ver. 2004231100";
		//private const string VERSION = "Ver. 2004231300";
		//private const string VERSION = "Ver. 2004232100";
		//private const string VERSION = "Ver. 2004251000";
		//private const string VERSION = "Ver. 2004261600";
		//private const string VERSION = "Ver. 2004272300";
		//private const string VERSION = "Ver. 2004282200";
		//private const string VERSION = "Ver. 2004291900";
		//private const string VERSION = "Ver. 20043021";
		//private const string VERSION = "Ver. 20050110";
		//private const string VERSION = "Ver. 20050822";
		//private const string VERSION = "Ver. 20051009";
		//private const string VERSION = "Ver. 20051018";
		private const string VERSION = "Ver. 200523";
		public ModbusRTUMaster m_rtu;
        public DateTime m_rtu_lastrcv; //마지막 데이터 변경수신을 받은시간

        //public int m_step = 0;
        private int m_interval_rtu;
        //public const int BUF_SIZE = 1024 * 10;

        private AppData appData;
		private RWData rwData;
        private ROData roData;
        // 현재 선택된 집진기 No(1 ~ 4)
        private int selDCNo;
		// 집진기 타입 Single, Multi
        private int selDCType;
		// 현재 선택된 slaveid
		public int slaveId;
        // Settings.Default.DCType 가 설정된 count
        private int settingPageCnt;
		// Air 가동예약 시간
		private DateTime airRemTime;
		// Air 가동 남은 시간
		private int remTimer = 0;
		// Air 가동예약 실행 여부
		private bool isAirReserv;
		// 하단 페이지 선택 이미지
		private Image pageNoneSelImage;
        private Image pageSelImage;

        //private Image gifCap;
        private Image gifProp;
		private Image gifDoor;
		private Image gifAirFlow;
		private Image imgArrUp;
        private Image imgArrRight;

		private int[] arrAirReserv = { 0, 5, 10, 15, 20, 30, 60 };

		// 알람 변수
		// Emergency
		private int AlmEmg;
		// 경광등 상태
		private int AlmStat;
		// Volt Sensor
		private int AlmVolt;
		// 전류 Sensor
		private int AlmCurr;
		// PH Sensor
		private int AlmPH;
		// 예비 Sensor
		private int AlmSp;
		// 예비2 Sensor
		private int AlmSp2;
		// 예비3 Sensor
		private int AlmSp3;
		// 온도 Sensor
		private int AlmTemp;
		// Out Damper
		private int AlmOutDam;
		// In Damper
		private int AlmInDam;
		// 차압센서1
		private int AlmDiff1;
		// 차압센서2
		private int AlmDiff2;
		// 차압센서3
		private int AlmDiff3;
		// 차압센서4
		private int AlmDiff4;
		// 차압센서 알람 시나리오
		//1. 오토에서 메뉴얼 변경시 차압 알람 count 0으로 설정(알람 해제하고는 별개 입니다.)
		//2. 다시 오토로 바꾸면 다시 차압알람 count 
		//3. Stop 하면 차압알람 count가 0이 아닐때 AIR 명령
		private int AlmDiffCnt;
		// 차압센서 알람 시나리오관련
		//private bool isAlmDiff;

		// RO Control Value
		// 40027 doorVal            Hi : 0x00(사용안함), Lo - 0 : Close, 1 : Open
		// 40028 airCntVal          val4 : AIR 밸브 동작횟수, val3 : AIR 밸브 기준횟수, val2 : AIR 밸브 동작간격, val1 : 분진모터 동작시간
		// 40029 airTotVal  		전체 : AIR 동작 총누적건수
		// 40030 motTotVal   		전체 : 분진모터 총누적건수
		// 40031 statVal1			val4 : EMG SW 상태, val3 : 집진기 동작 상태, val2 : 사용안함, val1 : MOTOR 동작 상태
		// 40032 statVal2			val4 : FAN 제어상태, val3 : AUTO / MANU 상태, val2 : FAN 결선상태, val1 : 경광등 상태
		// 40033 statAirVal			val4 : AIR 밸브 동작 상태, val3 : AIR 밸브1 동작상태, val2 : AIR 밸브2 동작상태, val1 : AIR 밸브3 동작상태

		// RW Control Value
		// 42040 별도 저장없음       val1 - 0 : 집진기 Stop, 1 : 집진기 Start
		// 42041 DrBzVal            val4 : Buzzer Mask, val3 : DoorMask, val2 : Air 밸브 동작간격, val1 : 분진 배출 모터 가동시간
		// 42042 ControlVal         val4 : Air 밸브 동작 유지시간, val3 : AUTO/MANU 선택, val2 : 수동 분진 MOTOR SW, val1 : 수동 AIR SW
		// 42043 AirCountVal        val4 : 분진모터동작기준횟수, val3 : 분진모터총누적건수리셋, val2 : AIR 밸브 가준 횟수, val1 : Air 동작총누적건수리셋
		// 42044 StatusVal          val1 :  STATUS(아직 사용안함)

		public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
			CommonUtil.CreateShortCut();

			InitControl();
            LoadGif();
			ResetAlarm();

			appData = AppData.Instence;

            Debug.WriteLine(string.Format("isFirstInit : {0}", Settings.Default.IsFirstInit));
            Debug.WriteLine(string.Format("comPort485 : {0}", Settings.Default.ComPort485));
            Debug.WriteLine(string.Format("comPort232 : {0}", Settings.Default.ComPort232));
			
			if (Settings.Default.IsFirstInit)
                Connection_process();

			// AIR 가동 예약시간 선택
			comAirTime.SelectedIndex = Settings.Default.AirTimer;
			selDCNo = 1;
            changePage();
		}

		private void InitControl()
		{
			Point tPos = pnlMulti.Location;
			// pnlFanVolt Location 변경
			this.pnlFanVolt.Location = new System.Drawing.Point(tPos.X + 22, tPos.Y + 156);
			labVersion.Text = VERSION;
			pageNoneSelImage = picPage1.BackgroundImage;
			pageSelImage = picPage1.InitialImage;

			this.labVolt.MouseDown += new MouseEventHandler(this.label_MouseDown);
			this.labCurr.MouseDown += new MouseEventHandler(this.label_MouseDown);
			this.labVolt.MouseUp += new MouseEventHandler(this.label_MouseUp);
			this.labCurr.MouseUp += new MouseEventHandler(this.label_MouseUp);
		}

		private void LoadGif()
        {
            try
            {
				//gifCap = Image.FromFile("gif\\cap.gif");
				//gifProp = Image.FromFile("gif\\Prop.gif");
				//gifDoor = Image.FromFile("gif\\door.gif");
				object objCap = Resources.ResourceManager.GetObject("cap"); //Return an object from the image chan1.png in the project
				//gifCap = (Image)objCap;
				object objProp = Resources.ResourceManager.GetObject("prop"); //Return an object from the image chan1.png in the project
				gifProp = (Image)objProp;
				object objDoor = Resources.ResourceManager.GetObject("door"); //Return an object from the image chan1.png in the project
				gifDoor = (Image)objDoor;
				object objAir = Resources.ResourceManager.GetObject("air_flow_2"); //Return an object from the image chan1.png in the project
				gifAirFlow = (Image)objAir;
				imgArrUp = Resources.arr_up;
                imgArrRight = Resources.arr_right;

                //picCap.Image = gifCap;
                picSngFan1.Image = null;
                picSngMotor.Image = null;
                picSngFan1.BackgroundImage = gifProp;
                picSngMotor.BackgroundImage = gifProp;
				//picSngDamFanIn.BackgroundImage = gifProp;

				picMulFan1.Image = null;
                picMulFan2.Image = null;
                picMulFan3.Image = null;
                picMulFan4.Image = null;
                picMulMotor.Image = null;
                picMulFan1.BackgroundImage = gifProp;
                picMulFan2.BackgroundImage = gifProp;
                picMulFan3.BackgroundImage = gifProp;
                picMulFan4.BackgroundImage = gifProp;
                picMulMotor.BackgroundImage = gifProp;
				//picMulDamFanIn.BackgroundImage = gifProp;
				picMulAirFlow.BackgroundImage = null;
				picSngAirFlow.BackgroundImage = null;

			}
			catch (Exception)
            {
            }
        }

		private void ResetAlarm()
		{
			AlmEmg = AlmStat = AlmVolt = AlmCurr = AlmPH = 0x00;
			AlmSp = AlmSp2 = AlmSp3 = 0x00;
			AlmTemp = AlmOutDam = AlmInDam = 0x00;
			AlmDiff1 = AlmDiff2 = AlmDiff3 = AlmDiff4 = 0x00;
			//isAlmDiff = false;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Close_process();
			Application.Exit();
		}

		private Stopwatch sw = new Stopwatch();
		private void label_MouseDown(object sender, MouseEventArgs e)
		{
			sw.Start();
		}

		private void label_MouseUp(object sender, MouseEventArgs e)
		{
			sw.Stop();
			if (sw.ElapsedMilliseconds < 3000) return;
			if ((Control)sender == labVolt)
				WindowState = FormWindowState.Minimized;
			if ((Control)sender == labCurr)
			{
				SendCloseCmd();
			}
		}

		private System.Windows.Forms.Timer CommonTimer;
		// 종료 시 명령 전송 : Stop, Damper Close, Air Off, Moter Off
		private void SendCloseCmd()
		{
			if (m_rtu == null || m_rtu.ser == null || !m_rtu.ser.IsOpen)
			{
				Close();
				return;
			}
			// 집진기 Stop 명령
			if (roData.statVal1.val3 == 1)
			{
				SendModbusData(slaveId, (int)RwRegister.START_VAL, 0x00);
				//Thread.Sleep(100);
			}

			// Air Off, Moter Off
			// AirValve Stop 명령
			if (rwData.ControlVal.val1 == 1 || rwData.ControlVal.val2 == 1)
			{
				rwData.ControlVal.val1 = 0;
				rwData.ControlVal.val2 = 0;
				SendModbusData(slaveId, (int)RwRegister.CONTROL_VAL, rwData.ControlVal.GetData());
				//Thread.Sleep(100);
			}
			// Damper Close
			//if (roData.damStatVal.val1 == 1)
			{
				SendModbusData(slaveId, (int)RwRegister.DAM_HDR, 0xFF00);
				//Thread.Sleep(100);
			}
			CommonTimer = new System.Windows.Forms.Timer();
			CommonTimer.Interval = 1000;
			CommonTimer.Tick += new EventHandler(CloseTimer_Tick);
			CommonTimer.Start();
		}

		private void CloseTimer_Tick(object sender, EventArgs e)
		{
			CommonTimer.Tick -= new EventHandler(CloseTimer_Tick);
			CommonTimer.Stop();
			Close();
		}

		private void btnStart_Click(object sender, EventArgs e)
        {
			int cmd;
			if (roData.statVal1.val3 == 1) cmd = 0x00;
			else cmd = 0x01;
			SendModbusData(slaveId, (int)RwRegister.START_VAL, cmd);
			SendModbusData(slaveId, (int)RwRegister.DAM_HDR, 0xFF00 | cmd);

			if (AlmDiffCnt != 0 && cmd == 0x00)
			{
				rwData.ControlVal.val1 = 1;
				SendModbusData(slaveId, (int)RwRegister.CONTROL_VAL, rwData.ControlVal.GetData());
			}
			AlmDiffCnt = 0;
		}

		private void btnAutoMnl_Click(object sender, EventArgs e)
        {
			if (rwData.ControlVal.val3 == 0)	rwData.ControlVal.val3 = 1;
			else								rwData.ControlVal.val3 = 0;
			SendModbusData(slaveId, (int)RwRegister.CONTROL_VAL, rwData.ControlVal.GetData());
		}

		private void btnAir_Click(object sender, EventArgs e)
        {
			if (rwData.ControlVal.val1 == 0) rwData.ControlVal.val1 = 1;
			else rwData.ControlVal.val1 = 0;
			SendModbusData(slaveId, (int)RwRegister.CONTROL_VAL, rwData.ControlVal.GetData());
		}

		private void btnMotor_Click(object sender, EventArgs e)
        {
			if (rwData.ControlVal.val2 == 0) rwData.ControlVal.val2 = 1;
			else rwData.ControlVal.val2 = 0;
			SendModbusData(slaveId, (int)RwRegister.CONTROL_VAL, rwData.ControlVal.GetData());
		}

		private void btnPower_Click(object sender, EventArgs e)
		{
			PowerOffForm powerOff = new PowerOffForm();
			powerOff.DialogResult = powerOff.ShowDialog();
			if (powerOff.DialogResult == DialogResult.Cancel) return;

			SendCloseCmd();
			CommonUtil.shutdownCom();
		}

		// 장비설정
		private void btnDeviceSet_Click(object sender, EventArgs e)
        {
			bool isFirstInit = Settings.Default.IsFirstInit;
			DeviceSetForm deviceSetForm = new DeviceSetForm();
			deviceSetForm.StartPosition = FormStartPosition.Manual;
			deviceSetForm.Location = this.Location;
			DialogResult result = deviceSetForm.ShowDialog();

			if (result == DialogResult.OK)
			{
				if (m_rtu != null)
				{
					m_rtu.MonitorAddresses.Clear();

					//모니터하는 영역의 값이 변경시 발생하는 이벤트
					m_rtu.ValueChanged -= new EventHandler(Modbus_Rtu_ValueChanged);
					//모니터하는 영역의 값이 변경없을시 발생하는 이벤트
					m_rtu.ValueNonChaned -= new EventHandler(Modbus_Rtu_ValueNonChanged);
					Close_process();
				}

				CommonTimer = new System.Windows.Forms.Timer();
				CommonTimer.Interval = 1000;
				CommonTimer.Tick += new EventHandler(ResetTimer_Tick);
				CommonTimer.Start();
			}
			changePage();
        }

		private void ResetTimer_Tick(object sender, EventArgs e)
		{
			CommonTimer.Tick -= new EventHandler(ResetTimer_Tick);
			CommonTimer.Stop();
			if (Settings.Default.IsFirstInit)
			{
				Connection_process();
			}
		}

		// Setting
		private void btnSetting_Click(object sender, EventArgs e)
        {
			NSettingForm settingForm = new NSettingForm(m_rtu, selDCNo);
			settingForm.StartPosition = FormStartPosition.Manual;
			settingForm.Location = this.Location;
			settingForm.ShowDialog();

			changePage();
		}

		// 작업장제어
		private void btnControl_Click(object sender, EventArgs e)
        {
            WorkCtrlForm workForm = new WorkCtrlForm(m_rtu, selDCNo);
			workForm.StartPosition = FormStartPosition.Manual;
			workForm.Location = this.Location;
			workForm.ShowDialog();
            changePage();
        }

        // F/W
        private void btnFW_Click(object sender, EventArgs e)
        {
			//AutoUpdate autoUpdate = new AutoUpdate();
			//autoUpdate.CompareVersions();
			string path = "C:\\CADC\\Downloads\\AutoUpdater.exe";
			Process.Start(path, "PUPG");

			// 집진기 Controller 종료
			Close();
		}

		// 다음페이지
		private void btnNext_Click(object sender, EventArgs e)
        {
            if (selDCNo < settingPageCnt)
                selDCNo++;
            else
                selDCNo = 1;

            changePage();
        }

		// Reset Alarm
		private void btnAlmReset_Click(object sender, EventArgs e)
		{
			ResetAlarm();
		}

		// 하단 슬라이드 아이콘
		private void picPage1_Click(object sender, EventArgs e)
        {
            selDCNo = 1;
            changePage();
        }

        private void picPage2_Click(object sender, EventArgs e)
        {
            selDCNo = 2;
            changePage();
        }

        private void picPage3_Click(object sender, EventArgs e)
        {
            selDCNo = 3;
            changePage();
        }

        private void picPage4_Click(object sender, EventArgs e)
        {
            selDCNo = 4;
            changePage();
        }

		#region 현재페이지 데이터 변경
		private void changePage()
        {
            // 설정된 집진기 Count
            settingPageCnt = 1;
			if (Settings.Default.UseDC1 && Settings.Default.DCType1 != 0)
			{
				settingPageCnt = 1;
				picPage1.Visible = true;
				picPage2.Visible = false;
				picPage3.Visible = false;
				picPage4.Visible = false;
			}
			if (Settings.Default.UseDC2 && Settings.Default.DCType2 != 0)
			{
				settingPageCnt = 2;
				picPage1.Visible = true;
				picPage2.Visible = true;
				picPage3.Visible = false;
				picPage4.Visible = false;
			}
			if (Settings.Default.UseDC3 && Settings.Default.DCType3 != 0)
			{
				settingPageCnt = 3;
				picPage1.Visible = true;
				picPage2.Visible = true;
				picPage3.Visible = true;
				picPage4.Visible = false;
			}
			if (Settings.Default.UseDC4 && Settings.Default.DCType4 != 0)
			{
				settingPageCnt = 4;
				picPage1.Visible = true;
				picPage2.Visible = true;
				picPage3.Visible = true;
				picPage4.Visible = true;
			}

            btnState.Text = selDCNo + " CLEAN";
            picPage1.BackgroundImage = pageNoneSelImage;
            picPage2.BackgroundImage = pageNoneSelImage;
            picPage3.BackgroundImage = pageNoneSelImage;
            picPage4.BackgroundImage = pageNoneSelImage;

            switch (selDCNo)
            {
                case 1:
                    picPage1.BackgroundImage = pageSelImage;
                    SelectDCType(Settings.Default.DCType1);
					slaveId = Settings.Default.DCID1;
                    break;
                case 2:
                    picPage2.BackgroundImage = pageSelImage;
                    SelectDCType(Settings.Default.DCType2);
					slaveId = Settings.Default.DCID2;
					break;
                case 3:
                    picPage3.BackgroundImage = pageSelImage;
                    SelectDCType(Settings.Default.DCType3);
					slaveId = Settings.Default.DCID3;
					break;
                case 4:
                    picPage4.BackgroundImage = pageSelImage;
                    SelectDCType(Settings.Default.DCType4);
					slaveId = Settings.Default.DCID4;
					break;
            }
			rwData = appData.rwData[selDCNo - 1];
			roData = appData.roData[selDCNo - 1];

			if (selDCNo < settingPageCnt)
                labPageNo.Text = (selDCNo + 1) + " Page";
            else
                labPageNo.Text = "1 Page";

			ShowDisplay();
        }
		#endregion

		#region 선택된 집진기가 Single인지 Multi인지 설정
		private void SelectDCType(int dcType)
        {
            selDCType = dcType;
            if (dcType == 1)        // Single
            {
                pnlSingle.Visible = true;
                pnlMulti.Visible = false;
            }
            else if (dcType == 2)   // Multi
            {
                pnlSingle.Visible = false;
                pnlMulti.Visible = true;

            }
            else                    // None
            {
                pnlSingle.Visible = false;
                pnlMulti.Visible = false;
                // 첫 페이지에 아무 것도 선택되지 않은 경우 강제로 설정
                if (selDCNo == 1)
                {
                    pnlSingle.Visible = true;
                }
            }
        }
		#endregion

		public void Connection_process()
        {
            //string filepath = "";
            //m_bTx = false;
            //m_bRx = false;
            //m_step = 0;
            m_interval_rtu = 1500;
            //DataWarning.m_modem_normal_queue.Clear();
            //DataWarning.m_bEmerClose = false;
            //DataWarning.m_modem_emer_queue.Clear();
            //DataWarning.m_base64_queue.Clear();
            appData.InitData(false);

            if (Settings.Default.ComPort485.Replace(" ", "") == "")
            {
                MessageBox.Show("COMPORT 설정이 올바르지 않습니다.");
            }
            else
            {
				int baudrate = Settings.Default.baudrate485;
				if (baudrate == 0) baudrate = 9600;
                InitModbus(Settings.Default.ComPort485, baudrate, 200, m_interval_rtu);

                //filepath = Application.StartupPath + @"\connmethod.txt";
                //WriteFileOne(filepath, "0");
                //filepath = Application.StartupPath + @"\comport.txt";
                //WriteFileOne(filepath, cmbComport.Text);
                alarmTimer.Start();
            }
        }

        public void InitModbus(string comport = "COM10", int baudrate = 9600, int interval = 1000, int timeout = 1500)
        {
            //rtu 초기값
            m_rtu = new ModbusRTUMaster(this);
            m_rtu.PortName = comport;
            m_rtu.Baudrate = baudrate;
            m_rtu.Interval = interval; //100이면 약 0.5초정도
            m_rtu.Timeout = timeout;

            int slaveid = 0;
            if (Settings.Default.DCType1 != 0)
            {
                slaveid = Settings.Default.DCID1;
                //rtu 모니터링할 영역 지정 : new ModbusAddress(국번, 주소, 영역타입, 개수)
                m_rtu.MonitorAddresses.Add(new ModbusAddress(slaveid, 40001, ModbusAddressType.WORD, 50));
				m_rtu.MonitorAddresses.Add(new ModbusAddress(slaveid, 42001, ModbusAddressType.WORD, 83));
			}
			if (Settings.Default.DCType2 != 0)
            {
                slaveid = Settings.Default.DCID2;
                //rtu 모니터링할 영역 지정 : new ModbusAddress(국번, 주소, 영역타입, 개수)
                m_rtu.MonitorAddresses.Add(new ModbusAddress(slaveid, 40001, ModbusAddressType.WORD, 50));
				m_rtu.MonitorAddresses.Add(new ModbusAddress(slaveid, 42001, ModbusAddressType.WORD, 83));
			}
            if (Settings.Default.DCType3 != 0)
            {
                slaveid = Settings.Default.DCID3;
                //rtu 모니터링할 영역 지정 : new ModbusAddress(국번, 주소, 영역타입, 개수)
                m_rtu.MonitorAddresses.Add(new ModbusAddress(slaveid, 40001, ModbusAddressType.WORD, 50));
				m_rtu.MonitorAddresses.Add(new ModbusAddress(slaveid, 42001, ModbusAddressType.WORD, 83));
			}
            if (Settings.Default.DCType4 != 0)
            {
                slaveid = Settings.Default.DCID4;
                //rtu 모니터링할 영역 지정 : new ModbusAddress(국번, 주소, 영역타입, 개수)
                m_rtu.MonitorAddresses.Add(new ModbusAddress(slaveid, 40001, ModbusAddressType.WORD, 50));
				m_rtu.MonitorAddresses.Add(new ModbusAddress(slaveid, 42001, ModbusAddressType.WORD, 83));
			}

            //모니터하는 영역의 값이 변경시 발생하는 이벤트
            m_rtu.ValueChanged += new EventHandler(Modbus_Rtu_ValueChanged);
            //모니터하는 영역의 값이 변경없을시 발생하는 이벤트
            m_rtu.ValueNonChaned += new EventHandler(Modbus_Rtu_ValueNonChanged);

            //통신시작
            int testTimeOut = m_rtu.Timeout;
            m_rtu.Start();
        }

        public void Close_process()
        {
            if(alarmTimer.Enabled) alarmTimer.Stop();
			if (AirWindTimer.Enabled) AirWindTimer.Stop();
			if (m_rtu != null)
			{
				m_rtu.Stop();
			}
        }

		/// <summary>
		/// status에 따라 메인화면의 각종 이미지 상태 설정
		/// </summary>
		private void btnStatusImage()
		{
			// Emergency 상태
			if (roData.statVal1.val4 == 0x01)
				btnEmg.Visible = true;
			else
				btnEmg.Visible = false;

			// Single, Multi 공통. 집진기 가동 상태(RUN / STOP)
			if (roData.statVal1.val3 == 0x01)
			{
				btnStart.BackgroundImage = Resources.stop_on;
			}
			else
			{
				btnStart.BackgroundImage = Resources.start_on;
			}

			// Single, Multi 공통. AUTO / MANUAL(1 / 0)
			if (roData.statVal2.val3 == 0x01)
			{
				btnAutoMnl.BackgroundImage = Resources.auto;
				btnAir.Visible = false;
				btnMotor.Visible = false;
				pnlAirTimer.Visible = false;
			}
			else
			{
				btnAutoMnl.BackgroundImage = Resources.manual_off;
				btnAir.Visible = true;
				btnMotor.Visible = true;
				pnlAirTimer.Visible = true;
				AlmDiffCnt = 0;
			}

			// Single, Multi 공통. AIR 가동 상태(RUN / STOP)
			if (roData.statAirVal.val4 == 0x01)
				btnAir.BackgroundImage = Resources.air_on;
			else
				btnAir.BackgroundImage = Resources.air_off;

			// Single, Multi 공통. MOTOR 가동 상태(RUN / STOP)
			if (roData.statVal1.val1 == 0x01)
				btnMotor.BackgroundImage = Resources.motor_on;
			else
				btnMotor.BackgroundImage = Resources.motor_off;

		}

		private void ShowDisplay()
        {
			SetAirReserve();
			btnStatusImage();

			// btn 상태를 위한 Count 초기화
			if (roData.statVal1.val4 == 0) emgStep = 0;		// Emergency
			if (roData.statVal2.val1 == 0) wrnStep = 0;		// 경광등
			if (roData.statAirVal.val4 == 0)				// Air 밸브
			{
				windStep = 0;
				picMulWind.Visible = false;
				picSngWind.Visible = false;
				AirWindTimer.Stop();
			}
			else
			{
				picMulWind.Visible = true;
				picSngWind.Visible = true;
				if(AirWindTimer.Enabled == false)
					AirWindTimer.Start();
			}

			labFWVer.Text = string.Format("{0}", roData.FwVersion.valLo > 0x30 ? roData.FwVersion.valLo - 0x30 : roData.FwVersion.valLo);

			// 메인 전압, 전류 등
			labVolt.Text = roData.voltSensor.dispVal;
            labCurr.Text = roData.currSensor.dispVal;
            labPH.Text = roData.phSensor.dispVal;
            labSpare.Text = roData.spSensor.dispVal;

			pnlSp2.Visible = CommonUtil.IsNull(Settings.Default.sp2Name) ? false : true;
			pnlSp3.Visible = CommonUtil.IsNull(Settings.Default.sp3Name) ? false : true;

			if (pnlSp2.Visible)
			{
				labSp2Name.Text = Settings.Default.sp2Name;
				labSp2Val.Text = roData.sp2Sensor.dispVal;
				labSp2Unit.Text = Settings.Default.sp2HdrUnit;
			}
			if (pnlSp3.Visible)
			{
				labSp3Name.Text = Settings.Default.sp3Name;
				labSp3Val.Text = roData.sp3Sensor.dispVal;
				labSp3Unit.Text = Settings.Default.sp3HdrUnit;
			}

			// Damper Open/Close Animation
			if (roData.statVal1.val3 == 0x01 && roData.damStatVal.valLo != 0)
			{
				isOutDamperOpen = true;
				if (!outDamTimer.Enabled && outDamperAlgle == 0)
				{
					outDamperAlgle = 0;
					outDamTimer.Start();
				}
			}
			else
			{
				isOutDamperOpen = false;
				if (!outDamTimer.Enabled && outDamperAlgle != 0)
				{
					outDamperAlgle = roData.damStatVal.valLo;
					outDamTimer.Stop();
					outDamTimer.Start();
				}
			}

			labYD.Text = roData.statVal2.val2 == 0 ? "Single" : "Y / Delta";
			// Fan 소비 전력
			//roData.fanVoltVal.val = 0xffff;
			Int16 fanVolt = (Int16)roData.fanVoltVal.val;
			if (fanVolt < 0)
				labFanVolt.Text = string.Format("----");
			else if (fanVolt < 1000)
				labFanVolt.Text = string.Format("{0} W", fanVolt);
			else
				labFanVolt.Text = string.Format("{0:f1} Kw", fanVolt / 1000f);


			if (selDCType == (int)DCType.DC_SINGLE)
            {
				labSngDmpOut.Text = string.Format("{0} ˚", roData.damStatVal.valLo);
				labSngTemp.Text = roData.tempSensor.dispVal + (roData.tempSensor.val != 0xFFFF ? " ℃" : "");
                labSngCurr.Text = roData.fanSensor[0].dispVal + (roData.fanSensor[0].val != 0xFFFF ? " A" : "");
                byte[] bHdr = BitConverter.GetBytes((Int16)roData.difSensor[0].hdr);
				if (roData.difSensor[0].uMulti == 0x01)
					labSngDiff.Text = string.Format("{0} {1}", roData.difSensor[0].dispVal, "Kpa");
				else
					labSngDiff.Text = string.Format("{0} {1}", roData.difSensor[0].dispVal, "mmH\u00B2O");


				// Single 화면 적용
				labSngDoor.Text = roData.doorVal.valLo != 0 ? "OPEN" : "CLOSE";
				labSngSolValve.Text = string.Format("{0}", roData.airCntVal.val4);
				labSngAirTot.Text = string.Format("{0}", roData.airTotVal.val);
				labSngDustMot.Text = string.Format("{0}", roData.motTotVal.val);

				// Single, Multi 공통. damMonVal.valLo
				//if (roData.damMonVal.valLo == 0x01)
				// Door open
				if (roData.doorVal.valLo == 0x01)
                {
                    labSngDoor.Text = "OPEN";
                    picSngDoor.Image = gifDoor;
                    picSngDoor.BackgroundImage = null;
                }
                else
                {
                    labSngDoor.Text = "CLOSE";
                    picSngDoor.Image = null;
                    picSngDoor.BackgroundImage = Resources.door;
                }

                // statAirVal.val4 : AIR 밸브 RUN / STOP
                // *statAirVal.val3 : AIR 밸브 1 동작 상황
                // *statAirVal.val2 : AIR 밸브 2 동작 상황
                // *statAirVal.val1 : AIR 밸브 3 동작 상황
				// 밸브 1
				if (roData.statAirVal.val1 == 0x01)
                    picSngValve1.BackgroundImage = Resources.sw_on;
                else
                    picSngValve1.BackgroundImage = Resources.sw_off;
				// 밸브 2
				if (roData.statAirVal.val2 == 0x01)
                    picSngValve2.BackgroundImage = Resources.sw_on;
                else
                    picSngValve2.BackgroundImage = Resources.sw_off;
				// 밸브 3
				if (roData.statAirVal.val3 == 0x01)
                    picSngValve3.BackgroundImage = Resources.sw_on;
                else
                    picSngValve3.BackgroundImage = Resources.sw_off;

				if (roData.statVal2.val4 == 0x01)
				{
					if (picSngFan1.Image == null)
					{
						picSngFan1.Image = gifProp;
						picSngAirFlow.Image = gifAirFlow;
						picSngFan1.BackgroundImage = null;
						picSngAirFlow.BackgroundImage = null;
					}
				}
				else
				{
					if (picSngFan1.BackgroundImage == null)
					{
						picSngFan1.Image = null;
						picSngAirFlow.Image = null;
						picSngAirFlow.BackgroundImage = null;
						picSngFan1.BackgroundImage = Resources.prop;
					}
				}

				// Single, Multi 공통. MOTOR 가동 상태(RUN / STOP)
				if (roData.statVal1.val1 == 0x01)
				{
					if (picSngMotor.Image == null)
					{
						picSngMotor.Image = gifProp;
						picSngMotor.BackgroundImage = null;
					}
				}
				else
				{
					if (picSngMotor.BackgroundImage == null)
					{
						picSngMotor.Image = null;
						picSngMotor.BackgroundImage = Resources.prop;
					}
				}

				// Single, Multi 공통. 흡기 댐퍼 가동 상태
				//if (roData.damMonVal.valLo != 0x00)
				//{
				//	if (picSngDamFanIn.Image == null)
				//	{
				//		picSngDamFanIn.Image = gifProp;
				//		picSngDamFanIn.BackgroundImage = null;
				//	}
				//}
				//else
				//{
				//	if (picMulDamFanIn.BackgroundImage == null)
				//	{
				//		picMulDamFanIn.Image = null;
				//		picMulDamFanIn.BackgroundImage = Resources.prop;
				//	}
				//}

			}
			else if (selDCType == (int)DCType.DC_MULTI)
            {
				labMulDmpOut.Text = string.Format("{0} ˚", roData.damStatVal.valLo);
				labMulDmpIn.Text = string.Format("{0} ˚", roData.damMonVal.valLo);
				labMulTemp.Text = roData.tempSensor.dispVal + (roData.tempSensor.val != 0xFFFF ? " ℃" : "");
				labMulCurr1.Text = roData.fanSensor[0].dispVal + (roData.fanSensor[0].val != 0xFFFF ? " A" : "");
				labMulCurr2.Text = roData.fanSensor[1].dispVal + (roData.fanSensor[1].val != 0xFFFF ? " A" : "");
				labMulCurr3.Text = roData.fanSensor[2].dispVal + (roData.fanSensor[2].val != 0xFFFF ? " A" : "");
				labMulCurr4.Text = roData.fanSensor[3].dispVal + (roData.fanSensor[3].val != 0xFFFF ? " A" : "");

                byte[] bHdr = BitConverter.GetBytes((Int16)roData.difSensor[0].hdr);
                if (roData.difSensor[0].uMulti == 0x01)
                    labMulDiff1.Text = string.Format("{0} {1}", roData.difSensor[0].dispVal, "Kpa");
                else
                    labMulDiff1.Text = string.Format("{0} {1}", roData.difSensor[0].dispVal, "mmH\u00B2O");

                if (roData.difSensor[1].uMulti == 0x01)
                    labMulDiff2.Text = string.Format("{0} {1}", roData.difSensor[1].dispVal, "Kpa");
                else
                    labMulDiff2.Text = string.Format("{0} {1}", roData.difSensor[1].dispVal, "mmH\u00B2O");
                
                if (roData.difSensor[2].uMulti == 0x01)
					labMulDiff3.Text = string.Format("{0} {1}", roData.difSensor[2].dispVal, "Kpa");
                else
                    labMulDiff3.Text = string.Format("{0} {1}", roData.difSensor[2].dispVal, "mmH\u00B2O");
                
                if (roData.difSensor[3].uMulti == 0x01)
					labMulDiff4.Text = string.Format("{0} {1}", roData.difSensor[3].dispVal, "Kpa");
                else
                    labMulDiff4.Text = string.Format("{0} {1}", roData.difSensor[3].dispVal, "mmH\u00B2O");

				// Multi 화면 적용
				labMulSolValve.Text = string.Format("{0}", roData.airCntVal.val4);
				labMulAirTot.Text = string.Format("{0}", roData.airTotVal.val);
				labMulDustMot.Text = string.Format("{0}", roData.motTotVal.val);
                labYD.Text = roData.statVal2.val2 == 0 ? "Single" : "Y / Delta";

				// Single, Multi 공통. 집진기 가동 상태(RUN / STOP)
				//if (roData.damMonVal.valLo == 0x01)
				// Door open
				if (roData.doorVal.valLo == 0x01)
				{
					labMulDoor.Text = "OPEN";
					picMulDoor.Image = gifDoor;
					picMulDoor.BackgroundImage = null;
				}
				else
				{
					labMulDoor.Text = "CLOSE";
					picMulDoor.Image = null;
					picMulDoor.BackgroundImage = Resources.door;
				}

				// statAirVal.val4 : AIR 밸브 RUN / STOP
				// *statAirVal.val3 : AIR 밸브 1 동작 상황
				// *statAirVal.val2 : AIR 밸브 2 동작 상황
				// *statAirVal.val1 : AIR 밸브 3 동작 상황
				// 밸브 1
				if (roData.statAirVal.val1 == 0x01)
					picMulValve1.BackgroundImage = Resources.sw_on;
				else
					picMulValve1.BackgroundImage = Resources.sw_off;
				// 밸브 2
				if (roData.statAirVal.val2 == 0x01)
					picMulValve2.BackgroundImage = Resources.sw_on;
				else
					picMulValve2.BackgroundImage = Resources.sw_off;
				// 밸브 3
				if (roData.statAirVal.val3 == 0x01)
					picMulValve3.BackgroundImage = Resources.sw_on;
				else
					picMulValve3.BackgroundImage = Resources.sw_off;

				// *airCntVal.val4 : AIR 밸브 동작횟수(Sol Valve와 연동)
				// airCntVal.val3 : AIR 밸브 기준횟수
				// airCntVal.val2 : AIR 밸브 동작간격
				// airCntVal.val1 : 분진 모터 동작시간

				// statVal2.val4 : FAN 제어상태
				// statVal2.val3 : AUTO/MANU 상태
				// statVal2.val2 : FAN 결선상태
				// statVal2.val1 : 경광등 상태

				if (roData.statVal2.val4 == 0x01)
				{
					if (picMulFan1.Image == null)
					{
						picMulFan1.Image = gifProp;
						picMulFan2.Image = gifProp;
						picMulFan3.Image = gifProp;
						picMulFan4.Image = gifProp;
						picMulAirFlow.Image = gifAirFlow;
						picMulFan1.BackgroundImage = null;
						picMulFan2.BackgroundImage = null;
						picMulFan3.BackgroundImage = null;
						picMulFan4.BackgroundImage = null;
					}
				}
				else
				{
					if (picMulFan1.BackgroundImage == null)
					{
						picMulFan1.Image = null;
						picMulFan2.Image = null;
						picMulFan3.Image = null;
						picMulFan4.Image = null;
						picMulAirFlow.Image = null;
						picMulAirFlow.BackgroundImage = null;
						picMulFan1.BackgroundImage = Resources.prop;
						picMulFan2.BackgroundImage = Resources.prop;
						picMulFan3.BackgroundImage = Resources.prop;
						picMulFan4.BackgroundImage = Resources.prop;
					}
				}
				// Single, Multi 공통. MOTOR 가동 상태(RUN / STOP)
				if (roData.statVal1.val1 == 0x01)
				{
					if (picMulMotor.Image == null)
					{
						picMulMotor.Image = gifProp;
						picMulMotor.BackgroundImage = null;
					}
				}
				else
				{
					if (picMulMotor.BackgroundImage == null)
					{
						picMulMotor.Image = null;
						picMulMotor.BackgroundImage = Resources.prop;
					}
				}

				// Single, Multi 공통. 흡기 댐퍼 가동 상태
				//if (roData.damMonVal.valLo != 0x00)
				//{
				//	if (picMulDamFanIn.Image == null)
				//	{
				//		picMulDamFanIn.Image = gifProp;
				//		picMulDamFanIn.BackgroundImage = null;
				//	}
				//}
				//else
				//{
				//	if (picMulDamFanIn.BackgroundImage == null)
				//	{
				//		picMulDamFanIn.Image = null;
				//		picMulDamFanIn.BackgroundImage = Resources.prop;
				//	}
				//}

			}

		}

		public int ModbusData(int page, int position)
        {
            int ret = 0;
			try
			{
				ret = m_rtu.Words[page][position];
			} catch(Exception)
			{

			}
            return ret;
        }

        public void SendModbusData(int slaveid, int addr, int value)
        {
            if (m_rtu == null)
            {
				MessageBox.Show("장비설정이 되지 않았습니다. 장비설정을 먼저해 주시기 바랍니다.", "설정 오류");
				return;
            }
            m_rtu.SetWord(slaveid, addr, value);
        }

        private void Modbus_Rtu_ValueChanged(object sender, EventArgs e)
        {
			testCnt++;
			Debug.WriteLine("Modus RTU Value Change");
			ModbusDataRefresh();
            ShowDisplay();
		}

		public void ModbusDataRefresh(bool bTx = true, bool bRx = true)
        {
            m_rtu_lastrcv = DateTime.Now;

            // 1번 Slave를 읽는다
            if (Settings.Default.UseDC1 && Settings.Default.DCType1 != 0)
			{
                ReadRoData(Settings.Default.DCID1, 0);
				ReadRwData(Settings.Default.DCID1, 0);
			}
			// 2번 Slave를 읽는다
			if (Settings.Default.UseDC2 && Settings.Default.DCType2 != 0)
			{
				ReadRoData(Settings.Default.DCID2, 1);
				ReadRwData(Settings.Default.DCID2, 1);
			}
			// 3번 Slave를 읽는다
			if (Settings.Default.UseDC3 && Settings.Default.DCType3 != 0)
			{
				ReadRoData(Settings.Default.DCID2, 2);
				ReadRwData(Settings.Default.DCID2, 2);
			}
			// 4번 Slave를 읽는다
			if (Settings.Default.UseDC4 && Settings.Default.DCType4 != 0)
			{
				ReadRoData(Settings.Default.DCID2, 4);
				ReadRwData(Settings.Default.DCID2, 4);
			}

		}

		private int testCnt = 0;
        private void ReadRoData(int slaveid, int index)
        {
			Int16[] ro_data = new Int16[50];
			// read ro data
			int pos_modbus = 40001;
			int addr;

			for (addr = 0; addr < ro_data.Length; addr++)
			{
				ro_data[addr] = (Int16)ModbusData(slaveid, pos_modbus);
				//Debug.WriteLine(string.Format("RO DATA page : {0}, pos_modbus : {1}, hdr[{2}] : {3}",
				//    page, pos_modbus, addr, ro_data[addr]));
				pos_modbus++;
			}

			// ROData 설정
			roData = appData.roData[index];

			// 전압센서 설정(40001 ~ 40002)
			roData.voltSensor.SetData(ro_data[0], ro_data[1], true);
			// 전류센서 설정(40003 ~ 40004)
			roData.currSensor.SetData(ro_data[2], ro_data[3], true);
			// PH센서 설정(40005 ~ 40006)
			roData.phSensor.SetData(ro_data[4], ro_data[5], true);
			// 온도센서 설정(40007 ~ 40008)
			roData.tempSensor.SetData(ro_data[6], ro_data[7], true);
			// FAN1센서 설정(40009 ~ 40010)
			roData.fanSensor[0].SetData(ro_data[8], ro_data[9]);
			// FAN2센서 설정(40011 ~ 40012)
			roData.fanSensor[1].SetData(ro_data[10], ro_data[11]);
			// FAN3센서 설정(40013 ~ 40014)
			roData.fanSensor[2].SetData(ro_data[12], ro_data[13]);
			// FAN4센서 설정(40015 ~ 40016)
			roData.fanSensor[3].SetData(ro_data[14], ro_data[15]);
			// 차압센서1 설정(40017 ~ 40018)
            roData.difSensor[0].SetData(ro_data[16], ro_data[17]);
            // 차압센서2 설정(40019 ~ 40020)
			roData.difSensor[1].SetData(ro_data[18], ro_data[19]);
			// 차압센서3 설정(40021 ~ 40022)
			roData.difSensor[2].SetData(ro_data[20], ro_data[21]);
			// 차압센서4 설정(40023 ~ 40024)
			roData.difSensor[3].SetData(ro_data[22], ro_data[23]);
			// 예비센서(온도센서) 설정(40025 ~ 40026)
			roData.spSensor.SetData(ro_data[24], ro_data[25], true);
			// 예비센서2,3초기화
			roData.sp2Sensor.SetData(0, 0);
			roData.sp3Sensor.SetData(0, 0);
			// Door 상태(40027)
			roData.doorVal.SetData(ro_data[26]);
			// AIR Count(40028)
			roData.airCntVal.SetData(ro_data[27]);
			// AIR 총 누적(40029)
			roData.airTotVal.SetData(ro_data[28]);
			// 분진모터 배출횟수(40030)
			roData.motTotVal.SetData(ro_data[29]);
			// 상태표시1, 2(40031 ~ 32)
			roData.statVal1.SetData(ro_data[30]);
			roData.statVal2.SetData(ro_data[31]);
			// AIR 밸브 상태(40033)
			roData.statAirVal.SetData(ro_data[32]);
            // Damper 상태(40034)
            roData.damStatVal.SetData(ro_data[33]);
            // 흡기 댐퍼 모니터링(40035)
            roData.damMonVal.SetData(ro_data[34]);
            // Fan 소모 전력(40036)
            roData.fanVoltVal.SetData(ro_data[35]);
            // 예비(40037 ~ 40040)
            roData.FwVersion.SetData(ro_data[36]);
			roData.FwChkSum.SetData(ro_data[37]);
			roData.spVal7.SetData(ro_data[38]);
			roData.spVal8.SetData(ro_data[39]);
			roData.spVal9.SetData(ro_data[40]);
			roData.spVal10.SetData(ro_data[41]);
			roData.spVal11.SetData(ro_data[42]);
			roData.spVal12.SetData(ro_data[43]);
			roData.spVal13.SetData(ro_data[44]);
			roData.spVal14.SetData(ro_data[45]);
			roData.spVal15.SetData(ro_data[46]);
			roData.spVal16.SetData(ro_data[47]);
			roData.spVal17.SetData(ro_data[48]);
			roData.spVal18.SetData(ro_data[49]);

			// Emergency
			if (roData.statVal1.val4 != 0) AlmEmg = 0x01;
			// 경광등 상태
			if (roData.statVal2.val1 != 0) AlmStat = roData.statVal2.val1;
			// Volt Sensor
			if (roData.voltSensor.alarmOn) AlmVolt = 0x01;
			// 전류 Sensor 알람 조건 알람 & Start 상태일때
			if (roData.currSensor.alarmOn && roData.statVal1.val3 == 0x01) AlmCurr = 0x01;
			// PH Sensor
			if (roData.phSensor.alarmOn) AlmPH = 0x01;
			// 예비 Sensor
			if (roData.spSensor.alarmOn) AlmSp = 0x01;
			// 예비2 Sensor
			if (roData.sp2Sensor.alarmOn) AlmSp2 = 0x01;
			// 예비3 Sensor
			if (roData.sp3Sensor.alarmOn) AlmSp3 = 0x01;
			// 온도 Sensor
			if (roData.tempSensor.alarmOn) AlmTemp = 0x01;
			// Out Damper
			if (roData.damStatVal.valHi != 0x00) AlmOutDam = 0x01;
			// In Damper
			if (roData.damMonVal.valHi != 0x00) AlmInDam = 0x01;
			// 차압센서1 알람 조건 알람 & Start 상태일때
			if (roData.difSensor[0].alarmOn && roData.statVal1.val3 == 0x01)
			{
				AlmDiff1 = 0x01;
				// AUTO / MANUAL(1 / 0)
				if (roData.statVal2.val3 == 0x01)
					AlmDiffCnt++;
			}
			// 차압센서2 알람 조건 알람 & Start 상태일때
			if (roData.difSensor[1].alarmOn && roData.statVal1.val3 == 0x01)
			{
				AlmDiff2 = 0x01;
				if (roData.statVal2.val3 == 0x01)
					AlmDiffCnt++;
			}
			// 차압센서3 알람 조건 알람 & Start 상태일때
			if (roData.difSensor[2].alarmOn && roData.statVal1.val3 == 0x01)
			{
				AlmDiff3 = 0x01;
				if (roData.statVal2.val3 == 0x01)
					AlmDiffCnt++;
			}
			// 차압센서4 알람 조건 알람 & Start 상태일때
			if (roData.difSensor[3].alarmOn && roData.statVal1.val3 == 0x01)
			{
				AlmDiff4 = 0x01;
				if (roData.statVal2.val3 == 0x01)
					AlmDiffCnt++;
			}

			// 차압센서 알람 시나리오 적용
			//if((AlmDiff1 + AlmDiff2 + AlmDiff3 + AlmDiff4) != 0
			//	&& roData.statAirVal.val4 != 0)
			//{
			//	isAlmDiff = true;
			//}

		}

		private void ReadRwData(int slaveid, int index)
		{
			UInt16[] rw_data = new UInt16[83];
			// read ro data
			int pos_modbus = 42001;
			int addr;

			// read rw data
			for (addr = 0; addr < rw_data.Length; addr++)
			{
				rw_data[addr] = Convert.ToUInt16(ModbusData(slaveid, pos_modbus));
				pos_modbus++;
			}

            // RWData 설정
			rwData = appData.rwData[index];

			// 전압센서 설정(42001 ~ 42003) 0
			//rwData.voltSensor = MakeSenserData(rw_data[0], rw_data[1], rw_data[2]);
			rwData.voltSensor.SetData(rw_data[0], rw_data[1], rw_data[2]);
			// 전류센서 설정(42004 ~ 42006) 3
			rwData.currSensor.SetData(rw_data[3], rw_data[4], rw_data[5]);
			// PH센서 설정(42007 ~ 42009) 6
			rwData.phSensor.SetData(rw_data[6], rw_data[7], rw_data[8]);
			// 온도센서 설정(42010 ~ 42012) 9 TODO
			rwData.tempSensor.SetData(rw_data[9], rw_data[10], rw_data[11]);
			// FAN결선제어 설정(42013 ~ 42014) 12
			rwData.FBSensor.SetData(rw_data[12], rw_data[13]);
			// 댐퍼설정(42015 ~ 42016)
			rwData.damperSet.SetData(rw_data[14], rw_data[15]);
			// IoT 전송(42017 ~ 42018)
			rwData.IoTSet.SetData(rw_data[16], rw_data[17]);
			// 차압센서1 ~ 4 LIMIT(42019 ~ 42022)
			rwData.LimitDif1.SetData(rw_data[18]);
			rwData.LimitDif2.SetData(rw_data[19]);
			rwData.LimitDif3.SetData(rw_data[20]);
			rwData.LimitDif4.SetData(rw_data[21]);
			// rw_data[14]: 사용안함
			//// FAN2센서 설정(42016 ~ 42018) 15
			//         rwData.arrFanSensor[1].SetData(rw_data[15], rw_data[16], rw_data[17]);
			//// FAN2센서 설정(42019 ~ 42021) 18
			//         rwData.arrFanSensor[2].SetData(rw_data[18], rw_data[19], rw_data[20]);
			//// FAN3센서 설정(42022 ~ 42024) 21
			//         rwData.arrFanSensor[3].SetData(rw_data[21], rw_data[22], rw_data[23]);
			// DIF1센서 설정(42025 ~ 42027) 24
			rwData.difSensor[0].SetData(rw_data[24], rw_data[25], rw_data[26], RwSensor.SENSOR_DIFF);
			// DIF2센서 설정(42028 ~ 42030) 27
			rwData.difSensor[1].SetData(rw_data[27], rw_data[28], rw_data[29], RwSensor.SENSOR_DIFF);
			// DIF1센서 설정(42031 ~ 42033) 30
			rwData.difSensor[2].SetData(rw_data[30], rw_data[31], rw_data[32], RwSensor.SENSOR_DIFF);
			// DIF1센서 설정(42034 ~ 42036) 33
			rwData.difSensor[3].SetData(rw_data[33], rw_data[34], rw_data[35], RwSensor.SENSOR_DIFF);
			// 예비센서(온도센서) 설정(42037 ~ 42039) 36
			rwData.spSensor.SetData(rw_data[36], rw_data[37], rw_data[38], RwSensor.SENSOR_SPAR);
			// TODO 정리 필요
			// Start(42040) 39
			// Door/Buzzer Mask(42041) 40
			rwData.DrBzVal.SetData(rw_data[40]);
			// Control(42042) 41
			rwData.ControlVal.SetData(rw_data[41]);
			// Air Count(42043) 42
			rwData.AirCountVal.SetData(rw_data[42]);
			// Status(42044) 43
			rwData.StatusVal.SetData(rw_data[43]);
			// 전압센서 LIMIT(42045)
			rwData.LimitVolt.SetData(rw_data[44]);
			// 전류센서 LIMIT(42046)
			rwData.LimitCurr.SetData(rw_data[45]);
			// PH센서 LIMIT(42047)
			rwData.LimitPH.SetData(rw_data[46]);
			// 온도센서 LIMIT(42048)
			rwData.LimitTemp.SetData(rw_data[47]);
			// FAN 센서 LIMIT(42049)
			rwData.LimitFan.SetData(rw_data[48]);
			// 차압센서 LIMIT(42050)
			//rwData.LimitDif1.SetData(rw_data[49]);
			// 예비센서 LIMIT(42051)
			rwData.LimitSp.SetData(rw_data[50]);
			// FAN CURR1 설정(42052 ~ 42059)
			rwData.FanCurr1.SetData(rw_data[51], rw_data[52], rw_data[53], rw_data[54], rw_data[55], rw_data[56], rw_data[57], rw_data[58]);
            // FAN CURR2 설정(42060 ~ 42067)
			rwData.FanCurr2.SetData(rw_data[59], rw_data[60], rw_data[61], rw_data[62], rw_data[63], rw_data[64], rw_data[65], rw_data[66]);
            // FAN CURR3 설정(42068 ~ 42075)
			rwData.FanCurr3.SetData(rw_data[67], rw_data[68], rw_data[69], rw_data[70], rw_data[71], rw_data[72], rw_data[73], rw_data[74]);
            // FAN CURR4 설정(42076 ~ 42083)
			rwData.FanCurr4.SetData(rw_data[75], rw_data[76], rw_data[77], rw_data[78], rw_data[79], rw_data[80], rw_data[81], rw_data[82]);
		}

		private void Modbus_Rtu_ValueNonChanged(object sender, EventArgs e)
        {
			testCnt++;
			Debug.WriteLine("Modus RTU Value Non-Change");
			TimeSpan compTime = DateTime.Now - m_rtu_lastrcv;
        }

		private bool isBlink = false;
        private int windStep = 0;	// 40033.val4 Air 버튼 상태(Wind 상태)
        private int emgStep = 0;
		private int wrnStep = 0;   // 경광등 상태 count(Warning Light)
		private void alarmTimer_Tick(object sender, EventArgs e)
		{
            // TestCode
            //roData.difSensor[0].SetData(0, -256);
            //roData.difSensor[1].SetData(0, -512);
            //roData.difSensor[2].SetData(0, -1024);
            //roData.difSensor[3].SetData(0, -2048);
            //labMulDiff1.Text = roData.difSensor[0].dispVal + " Kpa";
            //labMulDiff2.Text = roData.difSensor[1].dispVal + " Kpa";
            //labMulDiff3.Text = roData.difSensor[2].dispVal + " Kpa";
            //labMulDiff4.Text = roData.difSensor[3].dispVal + " Kpa";

            if (m_rtu.TimeoutCnt > 5)
				labCon.Text = "Tx/Rx Fail!!";
			else
			{
				if (m_rtu != null && m_rtu.ser.IsOpen)
					labCon.Text = "Connected";
				else
					labCon.Text = "DisConnected";
			}

			if (isAirReserv)
			{
				// 예약시간 - 시스템 시간
				remTimer = (airRemTime.Hour - DateTime.Now.Hour) * 3600
					+ (airRemTime.Minute - DateTime.Now.Minute) * 60
					+ (airRemTime.Second - DateTime.Now.Second);
				int hh = remTimer / 3600;
				int mm = (remTimer - hh * 3600) / 60;
				int ss = remTimer - hh * 3600 - mm * 60;
				labRemTime.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", hh, mm, ss);
				if (remTimer == 0)
					StartAir();
			}
			else
			{
				labRemTime.Text = String.Format("00:00:00");
			}

			// 차압센서 알람 Clear
			//차압센서 알람(roData.difSensor[0~3].alarmOn == true) On 일때
			//AIR 동작(roData.statAirVal.val4)이 0 이 되면 차압센서 알람 Clear 시킴
			//if ((AlmDiff1 + AlmDiff2 + AlmDiff3 + AlmDiff4) != 0)
			//{
			//	if (isAlmDiff && roData.statAirVal.val4 == 0)	// AIR 밸브 동작 Off
			//	{
			//		AlmDiff1 = AlmDiff2 = AlmDiff3 = AlmDiff4 = 0;
			//		isAlmDiff = false;
			//	}
			//}

			// Emergency
			if (AlmEmg != 0x00)
            {
                if (emgStep++ % 2 == 0)
					btnEmg.BackgroundImage = Resources.emg_on;
                else
                    btnEmg.BackgroundImage = Resources.emg_off;
            }

			// 경광등 상태
			switch (AlmStat)
			{
				case 0:     // btnGreen 점등
					btnGrn.BackgroundImage = Resources.btnGrn_off;
					btnRed.BackgroundImage = Resources.btnRed_off;
					break;
				case 1:     // btnGreen 1초 깜빡임
					if(wrnStep++ % 2 == 0)
						btnGrn.BackgroundImage = Resources.btnGrn_on;
					else
						btnGrn.BackgroundImage = Resources.btnGrn_off;
					btnRed.BackgroundImage = Resources.btnRed_off;
					break;
				case 2:     // btnRed	1초 깜빡임
					btnGrn.BackgroundImage = Resources.btnGrn_off;
					if (wrnStep++ % 2 == 0)
						btnRed.BackgroundImage = Resources.btnRed_on;
					else
						btnRed.BackgroundImage = Resources.btnRed_off;
					break;
				case 3:     // btnRed	5초 켜짐 1초 꺼짐
					btnGrn.BackgroundImage = Resources.btnGrn_off;
					if (wrnStep++ < 5)
						btnRed.BackgroundImage = Resources.btnRed_on;
					else
					{
						wrnStep = 0;
						btnRed.BackgroundImage = Resources.btnRed_off;
					}
					break;
			}

			// 기타 알람
			isBlink = !isBlink;
			if (isBlink)
			{
                if (AlmVolt != 0x00)
                    labVolt.BackColor = Color.Red;
                else
                    labVolt.BackColor = Color.Transparent;

				if (AlmCurr != 0x00)
					labCurr.BackColor = Color.Red;
				else
                    labCurr.BackColor = Color.Transparent;

				if (AlmPH != 0x00)
					labPH.BackColor = Color.Red;
				else
                    labPH.BackColor = Color.Transparent;

                if (AlmSp != 0x00)
                    labSpare.BackColor = Color.Red;
                else
                    labSpare.BackColor = Color.Transparent;

				if (pnlSp2.Visible)
					if (AlmSp2 != 0x00)
						labSp2Val.BackColor = Color.Red;
					else
						labSp2Val.BackColor = Color.Transparent;

				if (pnlSp3.Visible)
					if (AlmSp3 != 0x00)
						labSp3Val.BackColor = Color.Red;
					else
						labSp3Val.BackColor = Color.Transparent;

				if (AlmTemp != 0x00)
                {
                    labSngTemp.BackColor = Color.Red;
                    labMulTemp.BackColor = Color.Red;
                }
                else
                {
                    labSngTemp.BackColor = Color.Transparent;
                    labMulTemp.BackColor = Color.Transparent;
                }

				if (AlmOutDam != 0x00)
				{
					labSngDmpOut.BackColor = Color.Red;
					labMulDmpOut.BackColor = Color.Red;
				}
				else
				{
					labSngDmpOut.BackColor = Color.Transparent;
					labMulDmpOut.BackColor = Color.Transparent;
				}

				if (AlmInDam != 0x00)
				{
					labSngDmpIn.BackColor = Color.Red;
					labMulDmpIn.BackColor = Color.Red;
				}
				else
				{
					labSngDmpIn.BackColor = Color.LightGray;
					labMulDmpIn.BackColor = Color.LightGray;
				}

				if (AlmDiff1 != 0x00)
				{
					labMulDiff1.BackColor = Color.Red;
					labSngDiff.BackColor = Color.Red;
				}
				else
				{
					labMulDiff1.BackColor = Color.Transparent;
					labSngDiff.BackColor = Color.Transparent;
				}

				if (AlmDiff2 != 0x00)
					labMulDiff2.BackColor = Color.Red;
				else
					labMulDiff2.BackColor = Color.Transparent;

				if (AlmDiff3 != 0x00)
					labMulDiff3.BackColor = Color.Red;
				else
					labMulDiff3.BackColor = Color.Transparent;

				if (AlmDiff4 != 0x00)
					labMulDiff4.BackColor = Color.Red;
				else
					labMulDiff4.BackColor = Color.Transparent;

			}
			else
			{
                labVolt.BackColor = Color.Transparent;
                labCurr.BackColor = Color.Transparent;
                labPH.BackColor = Color.Transparent;
                labSpare.BackColor = Color.Transparent;
				if (pnlSp2.Visible)
					labSp2Val.BackColor = Color.Transparent;
				if (pnlSp3.Visible)
					labSp3Val.BackColor = Color.Transparent;
				labSngTemp.BackColor = Color.Transparent;
                labMulTemp.BackColor = Color.Transparent;
				labSngDmpOut.BackColor = Color.Transparent;
				labMulDmpOut.BackColor = Color.Transparent;
				labSngDmpIn.BackColor = Color.LightGray;
				labMulDmpIn.BackColor = Color.LightGray;
				labMulDiff1.BackColor = Color.Transparent;
				labMulDiff2.BackColor = Color.Transparent;
				labMulDiff3.BackColor = Color.Transparent;
				labMulDiff4.BackColor = Color.Transparent;
				labSngDiff.BackColor = Color.Transparent;
			}

		}

		private void AirWindTimer_Tick(object sender, EventArgs e)
		{
			if (selDCType == (int)DCType.DC_SINGLE)
			{
				// 바람방향 표시
				if (windStep <= 1)
					picSngWind.BackgroundImage = imgArrUp;
				else
					picSngWind.BackgroundImage = imgArrRight;

				if (windStep == 0)
					picSngWind.Location = new Point(159, 310);
				else if (windStep == 1)
					picSngWind.Location = new Point(159, 260);
				else if (windStep == 2)
					picSngWind.Location = new Point(214, 240);
				else if (windStep == 3)
					picSngWind.Location = new Point(284, 240);
				else if (windStep == 4)
					picSngWind.Location = new Point(354, 240);
				else if (windStep == 5)
					picSngWind.Location = new Point(424, 240);
				else if (windStep == 6)
					picSngWind.Location = new Point(494, 240);
			}
			else if (selDCType == (int)DCType.DC_MULTI)
			{
				// 바람방향 표시
				if (windStep <= 1)
					picMulWind.BackgroundImage = imgArrUp;
				else
					picMulWind.BackgroundImage = imgArrRight;

				if (windStep == 0)
					picMulWind.Location = new Point(159, 310);
				else if (windStep == 1)
					picMulWind.Location = new Point(159, 260);
				else if (windStep == 2)
					picMulWind.Location = new Point(214, 240);
				else if (windStep == 3)
					picMulWind.Location = new Point(284, 240);
				else if (windStep == 4)
					picMulWind.Location = new Point(354, 240);
				else if (windStep == 5)
					picMulWind.Location = new Point(424, 240);
				else if (windStep == 6)
					picMulWind.Location = new Point(494, 240);
			}

			windStep = (windStep + 1) % 7;
		}

		private void btnSetting_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnSetting.BackgroundImage = Resources.setting_off;
        }

        private void btnSetting_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSetting.BackgroundImage = Resources.setting_on;
        }

        private void btnControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnControl.BackgroundImage = Resources.work_on;
        }

        private void btnControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnControl.BackgroundImage = Resources.work_off;
        }

        private void btnDeviceSet_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnDeviceSet.BackgroundImage = Resources.lot_on;
        }

        private void btnDeviceSet_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnDeviceSet.BackgroundImage = Resources.lot_off;
        }

        private void btnFW_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnFW.BackgroundImage = Resources.fw_on;
        }

        private void btnFW_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnFW.BackgroundImage = Resources.fw_off;
        }

        private void btnStart_MouseDown(object sender, MouseEventArgs e)
        {
			if(roData.statVal1.val3 == 0x00)
				this.btnStart.BackgroundImage = Resources.start_off;
			else
				this.btnStart.BackgroundImage = Resources.stop_off;
		}

		private void btnStart_MouseUp(object sender, MouseEventArgs e)
        {
			if (roData.statVal1.val3 == 0x00)
				this.btnStart.BackgroundImage = Resources.start_on;
			else
				this.btnStart.BackgroundImage = Resources.stop_on;
		}

		private void btnAutoMnl_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnAutoMnl.BackgroundImage = Resources.manual_on;
        }

        private void btnAutoMnl_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnAutoMnl.BackgroundImage = Resources.manual_off;
        }

        private void btnAir_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnAir.BackgroundImage = Resources.air_on;
        }

        private void btnAir_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnAir.BackgroundImage = Resources.air_off;
        }

        private void btnMotor_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnMotor.BackgroundImage = Resources.motor_on;
        }

        private void btnMotor_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnMotor.BackgroundImage = Resources.motor_off;
        }

		private void btnNext_MouseDown(object sender, MouseEventArgs e)
		{
			this.btnNext.BackgroundImage = Resources.RightArrow2;
		}

		private void btnNext_MouseUp(object sender, MouseEventArgs e)
		{
			this.btnNext.BackgroundImage = Resources.RightArrow;
		}

		private void btnPower_MouseDown(object sender, MouseEventArgs e)
		{
			this.btnPower.BackgroundImage = Resources.shut_on;
		}

		private void btnPower_MouseUp(object sender, MouseEventArgs e)
		{
			this.btnPower.BackgroundImage = Resources.shut_off;
		}

		private void comAirTime_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Settings.Default.AirTimer != comAirTime.SelectedIndex)
			{
				Settings.Default.AirTimer = comAirTime.SelectedIndex;
				Settings.Default.Save();
				isAirReserv = false;
				SetAirReserve();
			}
		}

		/// <summary>
		/// Air 가동 예약을 시작하거나 종료한다.
		/// </summary>
		private void SetAirReserve()
		{
			// 저장된 타임머 시간과 선택된 타임머 시간이 다르면 시간 변경후 Stop

			// STOP 상태이거나 AUTO 상태이면 Timer 종료
			if (roData.statVal1.val3 == 0 || roData.statVal2.val3 == 1 || roData.damStatVal.val4 == 1)
			{
				isAirReserv = false;
				return;
			}

			if (Settings.Default.AirTimer == 0)
			{
				isAirReserv = false;
				return;
			}

			if (isAirReserv == false)
			{
				//airRemTime = DateTime.Now.AddHours(Settings.Default.AirTimer);
				airRemTime = DateTime.Now.AddMinutes(arrAirReserv[Settings.Default.AirTimer]);
				isAirReserv = true;
				
			}
		}

		private void StartAir()
		{
			// Air 가동 명령 전송
			rwData.ControlVal.val1 = 1;
			SendModbusData(slaveId, (int)RwRegister.CONTROL_VAL, rwData.ControlVal.GetData());
			isAirReserv = false;
			//MessageBox.Show("AIR 가동 remTime : " + remTimer, "알림");
		}

		private bool isOutDamperOpen;
		private int outDamperAlgle;

		public object AutoUpdate { get; private set; }

		/// <summary>
		/// 배기 댐퍼 Open Image
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void outDamTimer_Tick(object sender, EventArgs e)
		{
			if (isOutDamperOpen)
				outDamperAlgle += 15;
			else
				outDamperAlgle -= 15;
			if (outDamperAlgle == 0)
			{
				picSngDmpOut.BackgroundImage = Resources.cap_0;
				picMulDmpOut.BackgroundImage = Resources.cap_0;
			}
			else if (outDamperAlgle == 15)
			{
				picSngDmpOut.BackgroundImage = Resources.cap_15;
				picMulDmpOut.BackgroundImage = Resources.cap_15;
			}
			else if (outDamperAlgle == 30)
			{
				picSngDmpOut.BackgroundImage = Resources.cap_30;
				picMulDmpOut.BackgroundImage = Resources.cap_30;
			}
			else if (outDamperAlgle == 45)
			{
				picSngDmpOut.BackgroundImage = Resources.cap_45;
				picMulDmpOut.BackgroundImage = Resources.cap_45;
			}
			else if (outDamperAlgle == 60)
			{
				picSngDmpOut.BackgroundImage = Resources.cap_60;
				picMulDmpOut.BackgroundImage = Resources.cap_60;
			}
			else if (outDamperAlgle == 75)
			{
				picSngDmpOut.BackgroundImage = Resources.cap_75;
				picMulDmpOut.BackgroundImage = Resources.cap_75;
			}
			else if (outDamperAlgle == 90)
			{
				picSngDmpOut.BackgroundImage = Resources.cap_90;
				picMulDmpOut.BackgroundImage = Resources.cap_90;
			}

			if (isOutDamperOpen && outDamperAlgle >= roData.damStatVal.valLo)
			{
				outDamTimer.Stop();
			}
			if (!isOutDamperOpen && outDamperAlgle <= 0)
			{
				outDamTimer.Stop();
			}
		}

	}
}
