using System;
using System.Windows.Forms;
using CADC.Registers;
using System.Diagnostics;
using CADC.Utils;
using CADC.Modbus;
using CADC.Properties;
using System.IO;

namespace CADC.SubForm
{
	public partial class NSettingForm : Form
    {
        private RWData rwData;
		private RWData tmpRwData;	// 파일에서 읽어온 자료 저장
		private RwSensor rwSensor;
		private FanCurrSet fanSet;

        private int currPage;
        private int selDCNo;
        private int DCIndex;
		private int slaveId;

		private ModbusRTUMaster m_rtu;

        public NSettingForm(ModbusRTUMaster mRtu, int selDCNo)
        {
			m_rtu = mRtu;
			//m_rtu.frm2 = this;
			this.selDCNo = selDCNo;
			InitializeComponent();
		}

		private void SettingForm_Load(object sender, EventArgs e)
        {
			this.DCIndex = selDCNo - 1;
			//rwData = AppData.Instence.rwData[DCIndex];

			if (selDCNo == 1)
				slaveId = Settings.Default.DCID1;
			else if (selDCNo == 2)
				slaveId = Settings.Default.DCID2;
			else if (selDCNo == 3)
				slaveId = Settings.Default.DCID3;
			else if (selDCNo == 4)
				slaveId = Settings.Default.DCID4;

			InitContorl();
			// 가상키보드 실행
			CommonUtil.ShowKeyboard();

			LoadAppData();

			SelectPage(tabControl1.SelectedIndex);
            labDCNO.Text = selDCNo.ToString();

        }

		private void NSettingForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// 가상키보드 종료
			Process[] psList = Process.GetProcessesByName("osk");
			if (psList.Length > 0)
			{
				psList[0].Kill();
			}
		}

		#region textbox 형식, 최소값, 최대값 설정
		private void InitContorl()
        {
            // TextBox Tag 정의
            // P0;     // 양의 정수
            // N0;     // 음의 정수
            // P1;    // 양수 소수점 1자리
            // N1;    // 음수 소수점 1자리
            // P2;    // 양수 소수점 2자리
            // N2;    // 음수 소수점 2자리
            // 숫자 형식,최소값, 최대값
            // 전압센서
            this.txtVoltHdrMin.Tag = "P0, 0, 404";
            this.txtVoltHdrMax.Tag = "P0, 0, 404";
            this.txtVoltValMin.Tag = "P2, 0.00, 3.28";
            this.txtVoltValMax.Tag = "P2, 0.00, 3.28";
            this.txtVoltOffset.Tag = "N2, -1.00, 1.00";
            // 전류센서
            this.txtCurrHdrMin.Tag = "P1, 0.0, 50.8";
            this.txtCurrHdrMax.Tag = "P1, 0.0, 50.8";
            this.txtCurrValMin.Tag = "P2, 0.00, 3.28";
			this.txtCurrValMax.Tag = "P2, 0.00, 3.28";
			this.txtCurrOffset.Tag = "N2, -1.00, 1.00";
            // PH센서
            this.txtPhHdrMin.Tag = "P1, 0.0, 51.0";
            this.txtPhHdrMax.Tag = "P1, 0.0, 51.0";
            this.txtPhValMin.Tag = "P2, 0.00, 3.28";
			this.txtPhValMax.Tag = "P2, 0.00, 3.28";
			this.txtPhOffset.Tag = "N2, -1.00, 1.00";
            // 예비센서
            this.txtSpHdrMin.Tag = "N0, -18, 0";
            this.txtSpHdrMax.Tag = "P0, 0, 80";
            this.txtSpValMin.Tag = "P2, 0.00, 3.28";
			this.txtSpValMax.Tag = "P2, 0.00, 3.28";
			this.txtSpOffset.Tag = "N2, -1.00, 1.00";
			// 예비센서2
			this.txtSp2HdrMin.Tag = "P1, 0.0, 51.0";
			this.txtSp2HdrMax.Tag = "P1, 0.0, 51.0";
			this.txtSp2ValMin.Tag = "P2, 0.00, 3.28";
			this.txtSp2ValMax.Tag = "P2, 0.00, 3.28";
			this.txtSp2Offset.Tag = "N2, -1.00, 1.00";
			// 예비센서3
			this.txtSp3HdrMin.Tag = "P1, 0.0, 51.0";
			this.txtSp3HdrMax.Tag = "P1, 0.0, 51.0";
			this.txtSp3ValMin.Tag = "P2, 0.00, 3.28";
			this.txtSp3ValMax.Tag = "P2, 0.00, 3.28";
			this.txtSp3Offset.Tag = "N2, -1.00, 1.00";
			// 온도센서
			this.txtTempStdTemp.Tag = "P0, 0, 255";
			this.txtTempStdResist.Tag = "P0, 0, 255";
			this.txtTempKelvin.Tag = "P0, 0, 10000";
			this.txtTempOffset.Tag = "N2, -1.00, 1.00";
			// 차압센서
            this.txtDiff1MaxDisp.Tag = "P1, 0, 20.0";
            this.txtDiff1MinVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff1MaxVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff1CenVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff1Offset.Tag = "N2, -1.00, 1.00";

			this.txtDiff2MaxDisp.Tag = "P1, 0, 20.0";
			this.txtDiff2MinVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff2MaxVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff2CenVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff2Offset.Tag = "N2, -1.00, 1.00";

			this.txtDiff3MaxDisp.Tag = "P1, 0, 20.0";
			this.txtDiff3MinVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff3MaxVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff3CenVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff3Offset.Tag = "N2, -1.00, 1.00";

			this.txtDiff4MaxDisp.Tag = "P1, 0, 20.0";
			this.txtDiff4MinVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff4MaxVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff4CenVolt.Tag = "P2, 0.00, 3.28";
			this.txtDiff4Offset.Tag = "N2, -1.00, 1.00";

			// Fan 결선제어
			this.txtFBYDDelay.Tag = "P0, 0, 50";
			this.txtFBFanDelay.Tag = "P0, 0, 50";
			// 댐퍼 설정
            //this.txtDamHi.Tag = "P0, 0, 90";
            //this.txtDamLow.Tag = "P0, 0, 90";
			// Sensor LIMIT Value
			this.txtLmtVoltVal.Tag = "P0, 0, 500";
			this.txtLmtCurrVal.Tag = "P1, 0.0, 51.0";
			this.txtLmtPHVal.Tag = "P1, 0.0, 51.0";
			this.txtLmtTempVal.Tag = "P0, 0, 255";
			this.txtLmtFanVal.Tag = "P1, 0.0, 51.0";
			this.txtLmtDif1Val.Tag = "N0, -2000, 2000";
			this.txtLmtDif2Val.Tag = "N0, -2000, 2000";
			this.txtLmtDif3Val.Tag = "N0, -2000, 2000";
			this.txtLmtDif4Val.Tag = "N0, -2000, 2000";
			// Fan CURR 1 ~ 4 설정
			this.txtFan1DpMin.Tag = "P1, 0.0, 50.8";
            this.txtFan1DpMax.Tag = "P1, 0.0, 50.8";
			this.txtFan1VtMin.Tag = "P2, 0.00, 3.28";
			this.txtFan1VtMax.Tag = "P2, 0.00, 3.28";
			this.txtFan1Offset.Tag = "N2, -1.00, 1.00";
            this.txtFan2DpMin.Tag = "P1, 0.0, 50.8";
            this.txtFan2DpMax.Tag = "P1, 0.0, 50.8";
			this.txtFan2VtMin.Tag = "P2, 0.00, 3.28";
			this.txtFan2VtMax.Tag = "P2, 0.00, 3.28";
			this.txtFan2Offset.Tag = "N2, -1.00, 1.00";
            this.txtFan3DpMin.Tag = "P1, 0.0, 50.8";
            this.txtFan3DpMax.Tag = "P1, 0.0, 50.8";
			this.txtFan3VtMin.Tag = "P2, 0.00, 3.28";
			this.txtFan3VtMax.Tag = "P2, 0.00, 3.28";
			this.txtFan3Offset.Tag = "N2, -1.00, 1.00";
            this.txtFan4DpMin.Tag = "P1, 0.0, 50.8";
            this.txtFan4DpMax.Tag = "P1, 0.0, 50.8";
			this.txtFan4VtMin.Tag = "P2, 0.00, 3.28";
			this.txtFan4VtMax.Tag = "P2, 0.00, 3.28";
			this.txtFan4Offset.Tag = "N2, -1.00, 1.00";

            this.txtFan1DpStep1.Tag = "P1, 0.0, 50.8";
            this.txtFan1DpStep2.Tag = "P1, 0.0, 50.8";
            this.txtFan1DpStep3.Tag = "P1, 0.0, 50.8";
            this.txtFan1DpStep4.Tag = "P1, 0.0, 50.8";
            this.txtFan1DpStep5.Tag = "P1, 0.0, 50.8";
			this.txtFan1VtStep1.Tag = "P2, 0.00, 3.28";
			this.txtFan1VtStep2.Tag = "P2, 0.00, 3.28";
			this.txtFan1VtStep3.Tag = "P2, 0.00, 3.28";
			this.txtFan1VtStep4.Tag = "P2, 0.00, 3.28";
			this.txtFan1VtStep5.Tag = "P2, 0.00, 3.28";

            this.txtFan2DpStep1.Tag = "P1, 0.0, 50.8";
            this.txtFan2DpStep2.Tag = "P1, 0.0, 50.8";
            this.txtFan2DpStep3.Tag = "P1, 0.0, 50.8";
            this.txtFan2DpStep4.Tag = "P1, 0.0, 50.8";
            this.txtFan2DpStep5.Tag = "P1, 0.0, 50.8";
			this.txtFan2VtStep1.Tag = "P2, 0.00, 3.28";
			this.txtFan2VtStep2.Tag = "P2, 0.00, 3.28";
			this.txtFan2VtStep3.Tag = "P2, 0.00, 3.28";
			this.txtFan2VtStep4.Tag = "P2, 0.00, 3.28";
			this.txtFan2VtStep5.Tag = "P2, 0.00, 3.28";

            this.txtFan3DpStep1.Tag = "P1, 0.0, 50.8";
            this.txtFan3DpStep2.Tag = "P1, 0.0, 50.8";
            this.txtFan3DpStep3.Tag = "P1, 0.0, 50.8";
            this.txtFan3DpStep4.Tag = "P1, 0.0, 50.8";
            this.txtFan3DpStep5.Tag = "P1, 0.0, 50.8";
			this.txtFan3VtStep1.Tag = "P2, 0.00, 3.28";
			this.txtFan3VtStep2.Tag = "P2, 0.00, 3.28";
			this.txtFan3VtStep3.Tag = "P2, 0.00, 3.28";
			this.txtFan3VtStep4.Tag = "P2, 0.00, 3.28";
			this.txtFan3VtStep5.Tag = "P2, 0.00, 3.28";

            this.txtFan4DpStep1.Tag = "P1, 0.0, 50.8";
            this.txtFan4DpStep2.Tag = "P1, 0.0, 50.8";
            this.txtFan4DpStep3.Tag = "P1, 0.0, 50.8";
            this.txtFan4DpStep4.Tag = "P1, 0.0, 50.8";
            this.txtFan4DpStep5.Tag = "P1, 0.0, 50.8";
			this.txtFan4VtStep1.Tag = "P2, 0.00, 3.28";
			this.txtFan4VtStep2.Tag = "P2, 0.00, 3.28";
			this.txtFan4VtStep3.Tag = "P2, 0.00, 3.28";
			this.txtFan4VtStep4.Tag = "P2, 0.00, 3.28";
			this.txtFan4VtStep5.Tag = "P2, 0.00, 3.28";

			// KeyPress Event //////////////////////////////////////////////////////////////////
			this.txtVoltHdrMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtVoltHdrMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtVoltValMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtVoltValMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtVoltOffset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

            this.txtCurrHdrMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtCurrHdrMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtCurrValMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtCurrValMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtCurrOffset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

            this.txtPhHdrMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtPhHdrMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtPhValMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtPhValMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtPhOffset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

            this.txtSpHdrMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtSpHdrMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtSpValMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtSpValMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtSpOffset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

			this.txtSp2HdrMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtSp2HdrMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtSp2ValMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtSp2ValMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtSp2Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

			this.txtSp3HdrMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtSp3HdrMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtSp3ValMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtSp3ValMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtSp3Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

			this.txtTempStdTemp.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtTempStdResist.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtTempKelvin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtTempOffset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

            this.txtDiff1MaxDisp.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtDiff1MinVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtDiff1MaxVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtDiff1CenVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtDiff1Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

            this.txtDiff2MaxDisp.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtDiff2MinVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtDiff2MaxVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtDiff2CenVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtDiff2Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

            this.txtDiff3MaxDisp.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtDiff3MinVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtDiff3MaxVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtDiff3CenVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtDiff3Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

            this.txtDiff4MaxDisp.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtDiff4MinVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            this.txtDiff4MaxVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtDiff4CenVolt.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtDiff4Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

			// Fan 결선제어
			this.txtFBYDDelay.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFBFanDelay.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			// Sensor LIMIT Value
			this.txtLmtVoltVal.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtLmtCurrVal.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtLmtPHVal.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtLmtTempVal.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtLmtFanVal.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtLmtDif1Val.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtLmtDif2Val.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtLmtDif3Val.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtLmtDif4Val.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			// Fan CURR 1 ~ 4 설정
			this.txtFan1DpMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1DpMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2DpMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2DpMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3DpMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3DpMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4DpMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4DpMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1VtMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1VtMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2VtMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2VtMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3VtMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3VtMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4VtMin.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4VtMax.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4Offset.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

			this.txtFan1DpStep1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1DpStep2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1DpStep3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1DpStep4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1DpStep5.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1VtStep1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1VtStep2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1VtStep3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1VtStep4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan1VtStep5.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

			this.txtFan2DpStep1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2DpStep2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2DpStep3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2DpStep4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2DpStep5.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2VtStep1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2VtStep2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2VtStep3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2VtStep4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan2VtStep5.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

			this.txtFan3DpStep1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3DpStep2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3DpStep3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3DpStep4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3DpStep5.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3VtStep1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3VtStep2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3VtStep3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3VtStep4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan3VtStep5.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

			this.txtFan4DpStep1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4DpStep2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4DpStep3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4DpStep4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4DpStep5.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4VtStep1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4VtStep2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4VtStep3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4VtStep4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
			this.txtFan4VtStep5.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);

		}
		#endregion

		private void LoadAppData(bool isLoadFile = false)
        {
			if (isLoadFile == false)
				rwData = AppData.Instence.rwData[DCIndex];
			else
				rwData = tmpRwData;

			// 전압센서
			rwSensor = rwData.voltSensor;
            txtVoltHdrMin.Text = string.Format("{0}", (int)rwSensor.hdrMin);
            txtVoltHdrMax.Text = string.Format("{0}", (int)rwSensor.hdrMax);
            txtVoltValMin.Text = string.Format("{0:f2}", rwSensor.valMin);
            txtVoltValMax.Text = string.Format("{0:f2}", rwSensor.valMax);
            txtVoltOffset.Text = string.Format("{0:f2}", rwSensor.offset);
            txtVoltMulti.Text = string.Format("{0:D3}", rwSensor.maskVal);
            chkVoltMask.Checked = rwSensor.mask;
            // 전류센서
            rwSensor = rwData.currSensor;
            txtCurrHdrMin.Text = string.Format("{0:f1}", rwSensor.hdrMin);
            txtCurrHdrMax.Text = string.Format("{0:f1}", rwSensor.hdrMax);
            txtCurrValMin.Text = string.Format("{0:f2}", rwSensor.valMin);
            txtCurrValMax.Text = string.Format("{0:f2}", rwSensor.valMax);
            txtCurrOffset.Text = string.Format("{0:f2}", rwSensor.offset);
            txtCurrMulti.Text = string.Format("{0:D3}", rwSensor.maskVal);
            chkElecMask.Checked = rwSensor.mask;
            // PH센서
            rwSensor = rwData.phSensor;
            txtPhHdrMin.Text = string.Format("{0:f1}", rwSensor.hdrMin);
            txtPhHdrMax.Text = string.Format("{0:f1}", rwSensor.hdrMax);
            txtPhValMin.Text = string.Format("{0:f2}", rwSensor.valMin);
            txtPhValMax.Text = string.Format("{0:f2}", rwSensor.valMax);
            txtPhOffset.Text = string.Format("{0:f2}", rwSensor.offset);
            txtPHMulti.Text = string.Format("{0:D3}", rwSensor.maskVal);
            chkPHMask.Checked = rwSensor.mask;
			// 온도센서 항목 수정 필요
			txtTempStdTemp.Text = string.Format("{0}", rwData.tempSensor.hdrTemp);
            txtTempStdResist.Text = string.Format("{0}", rwData.tempSensor.hdrResi);
            txtTempKelvin.Text = string.Format("{0}", rwData.tempSensor.valKelv);
            txtTempOffset.Text = string.Format("{0:f1}", rwData.tempSensor.offset);
            chkTempMask.Checked = rwData.tempSensor.mask;
			// 예비센서(온도센서)     Min 값은 "-"값만 입력 가능하며 -10 을 입력하면 10으로 변환하여 전송한다.
			rwSensor = rwData.spSensor;
            txtSpHdrMin.Text = string.Format("{0:0}", rwSensor.hdrMin);
            txtSpHdrMax.Text = string.Format("{0:0}", rwSensor.hdrMax);
            txtSpValMin.Text = string.Format("{0:f2}", rwSensor.valMin);
            txtSpValMax.Text = string.Format("{0:f2}", rwSensor.valMax);
            txtSpOffset.Text = string.Format("{0:f2}", rwSensor.offset);
            txtSpMulti.Text = string.Format("{0:D3}", rwSensor.maskVal);
            chkSpMask.Checked = rwSensor.mask;
			// 예비센서2
			rwSensor = rwData.sp2Sensor;
			txtSp2HdrMin.Text = string.Format("{0:f1}", rwSensor.hdrMin);
			txtSp2HdrMax.Text = string.Format("{0:f1}", rwSensor.hdrMax);
			txtSp2ValMin.Text = string.Format("{0:f2}", rwSensor.valMin);
			txtSp2ValMax.Text = string.Format("{0:f2}", rwSensor.valMax);
			txtSp2Offset.Text = string.Format("{0:f2}", rwSensor.offset);
			txtSp2Multi.Text = string.Format("{0:D3}", rwSensor.maskVal);
			chkSp2Mask.Checked = rwSensor.mask;
			// 예비센서3
			rwSensor = rwData.sp3Sensor;
			txtSp3HdrMin.Text = string.Format("{0:f1}", rwSensor.hdrMin);
			txtSp3HdrMax.Text = string.Format("{0:f1}", rwSensor.hdrMax);
			txtSp3ValMin.Text = string.Format("{0:f2}", rwSensor.valMin);
			txtSp3ValMax.Text = string.Format("{0:f2}", rwSensor.valMax);
			txtSp3Offset.Text = string.Format("{0:f2}", rwSensor.offset);
			txtSp3Multi.Text = string.Format("{0:D3}", rwSensor.maskVal);
			chkSp2Mask.Checked = rwSensor.mask;
			// 공란센서2
			txtSp2Name.Text = Settings.Default.sp2Name;
            comSp2HdrUnit.Text = Settings.Default.sp2HdrUnit;
            labSp2HdrUnit.Text = Settings.Default.sp2HdrUnit;
			comSp2ValUnit.Text = Settings.Default.sp2ValUnit;
			labSp2ValUnit.Text = Settings.Default.sp2ValUnit;
			comSp2OffsetUnit.Text = Settings.Default.sp2OffsetUnit;
			// 공란센서3
			txtSp3Name.Text = Settings.Default.sp3Name;
			comSp3HdrUnit.Text = Settings.Default.sp3HdrUnit;
			labSp3HdrUnit.Text = Settings.Default.sp3HdrUnit;
			comSp3ValUnit.Text = Settings.Default.sp3ValUnit;
			labSp3ValUnit.Text = Settings.Default.sp3ValUnit;
			comSp3OffsetUnit.Text = Settings.Default.sp3OffsetUnit;
			// 기타 42041 ~ 42043(Door, Control, Air Count)
			bool isFail = false;

			if (rwData.DrBzVal.val4 <= 1)
				chkBuzzerMask.Checked = rwData.DrBzVal.val4 != 0 ? true : false;
			else
				isFail = true;
			if (rwData.DrBzVal.val3 <= 1)
				chkDoorMask.Checked = rwData.DrBzVal.val3 != 0 ? true : false;
			else
				isFail = true;
			if (rwData.DrBzVal.val2 < comAvoi.Items.Count)
				comAvoi.SelectedIndex = rwData.DrBzVal.val2;
			else
				isFail = true;
			if (rwData.DrBzVal.val1 < comDdmot.Items.Count)
				comDdmot.SelectedIndex = rwData.DrBzVal.val1;
			else
				isFail = true;
			if (isFail)
				MessageBox.Show(string.Format("42041 설정값 오류 Buzzer Mask : {0}, Door Mask : {1}, AIR 밸브 동작간격 : {2}, 분진배출모터동작시간 : {3}",
					rwData.DrBzVal.val4, rwData.DrBzVal.val3, rwData.DrBzVal.val2, rwData.DrBzVal.val1), "설정오류");

			comAvoht.SelectedIndex = rwData.ControlVal.val4;

			comDustCount.SelectedIndex = rwData.AirCountVal.val4;
			comAirCount.SelectedIndex = rwData.AirCountVal.val2;

			// Fan 결선제어
			txtFBYDDelay.Text = string.Format("{0:0}", rwData.FBSensor.valYDDelay);
			txtFBFanDelay.Text = string.Format("{0:0}", rwData.FBSensor.valFanDelay);
			if (rwData.FBSensor.hdrSngYD)
				chkFBYD.Checked = true;
			else
				chkFBSingle.Checked = true;

            // 댐퍼설정
            RW2Regi rw2Regi = rwData.damperSet;
            chkDamOn.Checked = rw2Regi.hdr.valLo != 0 ? true : false;
            if (chkDamOn.Checked)
            {
                comDamAng.Enabled = true;
            }
            else
            {
                rw2Regi.hdr.valHi = 0;
                comDamAng.Enabled = false;
            }
            comDamAng.Text = string.Format("{0}", rw2Regi.hdr.valHi);
            chkDamInhMsk.Checked = rw2Regi.val.valHi != 0 ? true : false;
            chkDamVenMsk.Checked = rw2Regi.val.valLo != 0 ? true : false;

            // IoT 전송
            rw2Regi = rwData.IoTSet;
            chkIot1.Checked = rw2Regi.hdr.val1 != 0 ? true : false;
            chkIot2.Checked = rw2Regi.hdr.val2 != 0 ? true : false;
            chkIot3.Checked = rw2Regi.hdr.val3 != 0 ? true : false;
            chkIot4.Checked = rw2Regi.hdr.val4 != 0 ? true : false;
            chkIot5.Checked = rw2Regi.val.val1 != 0 ? true : false;
            chkIot6.Checked = rw2Regi.val.val2 != 0 ? true : false;
            chkIot7.Checked = rw2Regi.val.val3 != 0 ? true : false;
            chkIot8.Checked = rw2Regi.val.val4 != 0 ? true : false;

            // Diff 차압센서
			rwSensor = rwData.difSensor[0];
			txtDiff1MinVolt.Text = string.Format("{0:f2}", rwSensor.valMin);
			txtDiff1MaxVolt.Text = string.Format("{0:f2}", rwSensor.valMax);
			txtDiff1CenVolt.Text = string.Format("{0:f2}", rwSensor.valCen);
			txtDiff1Offset.Text = string.Format("{0:f2}", rwSensor.offset);
			//txtDiff1Multi.Text = string.Format("{0:D3}", rwSensor.maskVal);
			comDiff1Unit.SelectedIndex = rwSensor.uMulti;
			labDiff1Unit.Text = comDiff1Unit.Text;
			if (comDiff1Unit.SelectedIndex == 0)
			{
				this.txtDiff1MaxDisp.Tag = "P1, 0.0, 20.0";
				txtDiff1MinDisp.Text = String.Format("{0:f1}", rwSensor.hdrMin);
				txtDiff1MaxDisp.Text = String.Format("{0:f1}", rwSensor.hdrMax);
			}
			else
			{
				this.txtDiff1MaxDisp.Tag = "P0, 0, 2000";
				txtDiff1MinDisp.Text = String.Format("{0:0}", rwSensor.hdrMin);
				txtDiff1MaxDisp.Text = String.Format("{0:0}", rwSensor.hdrMax);
			}
			chkDiff1Mask.Checked = rwSensor.mask;

			rwSensor = rwData.difSensor[1];
			txtDiff2MinVolt.Text = string.Format("{0:f2}", rwSensor.valMin);
			txtDiff2MaxVolt.Text = string.Format("{0:f2}", rwSensor.valMax);
			txtDiff2CenVolt.Text = string.Format("{0:f2}", rwSensor.valCen);
			txtDiff2Offset.Text = string.Format("{0:f2}", rwSensor.offset);
			//txtDiff2Multi.Text = string.Format("{0:D3}", rwSensor.maskVal);
			comDiff2Unit.SelectedIndex = rwSensor.uMulti;
			labDiff2Unit.Text = comDiff2Unit.Text;
			if (comDiff2Unit.SelectedIndex == 0)
			{
				this.txtDiff2MaxDisp.Tag = "P1, 0.0, 20.0";
				txtDiff2MinDisp.Text = String.Format("{0:f1}", rwSensor.hdrMin);
				txtDiff2MaxDisp.Text = String.Format("{0:f1}", rwSensor.hdrMax);
			}
			else
			{
				this.txtDiff2MaxDisp.Tag = "P0, 0, 200";
				txtDiff2MinDisp.Text = String.Format("{0:0}", rwSensor.hdrMin);
				txtDiff2MaxDisp.Text = String.Format("{0:0}", rwSensor.hdrMax);
			}
			chkDiff2Mask.Checked = rwSensor.mask;

			rwSensor = rwData.difSensor[2];
			txtDiff3MinVolt.Text = string.Format("{0:f2}", rwSensor.valMin);
			txtDiff3MaxVolt.Text = string.Format("{0:f2}", rwSensor.valMax);
			txtDiff3CenVolt.Text = string.Format("{0:f2}", rwSensor.valCen);
			txtDiff3Offset.Text = string.Format("{0:f2}", rwSensor.offset);
			//txtDiff3Multi.Text = string.Format("{0:D3}", rwSensor.maskVal);
			comDiff3Unit.SelectedIndex = rwSensor.uMulti;
			labDiff3Unit.Text = comDiff3Unit.Text;
			if (comDiff3Unit.SelectedIndex == 0)
			{
				this.txtDiff3MaxDisp.Tag = "P1, 0.0, 20.0";
				txtDiff3MinDisp.Text = String.Format("{0:f1}", rwSensor.hdrMin);
				txtDiff3MaxDisp.Text = String.Format("{0:f1}", rwSensor.hdrMax);
			}
			else
			{
				this.txtDiff3MaxDisp.Tag = "P0, 0, 200";
				txtDiff3MinDisp.Text = String.Format("{0:0}", rwSensor.hdrMin);
				txtDiff3MaxDisp.Text = String.Format("{0:0}", rwSensor.hdrMax);
			}
			chkDiff3Mask.Checked = rwSensor.mask;

			rwSensor = rwData.difSensor[3];
			txtDiff4MinVolt.Text = string.Format("{0:f2}", rwSensor.valMin);
			txtDiff4MaxVolt.Text = string.Format("{0:f2}", rwSensor.valMax);
			txtDiff4CenVolt.Text = string.Format("{0:f2}", rwSensor.valCen);
			txtDiff4Offset.Text = string.Format("{0:f2}", rwSensor.offset);
			//txtDiff4Multi.Text = string.Format("{0:D3}", rwSensor.maskVal);
			comDiff4Unit.SelectedIndex = rwSensor.uMulti;
			labDiff4Unit.Text = comDiff4Unit.Text;
			if (comDiff4Unit.SelectedIndex == 0)
			{
				this.txtDiff4MaxDisp.Tag = "P1, 0.0, 20.0";
				txtDiff4MinDisp.Text = String.Format("{0:f1}", rwSensor.hdrMin);
				txtDiff4MaxDisp.Text = String.Format("{0:f1}", rwSensor.hdrMax);
			}
			else
			{
				this.txtDiff4MaxDisp.Tag = "P0, 0, 200";
				txtDiff4MinDisp.Text = String.Format("{0:0}", rwSensor.hdrMin);
				txtDiff4MaxDisp.Text = String.Format("{0:0}", rwSensor.hdrMax);
			}
			chkDiff4Mask.Checked = rwSensor.mask;

			// 센서 LIMIT Value
			SensorLimit limit = rwData.LimitVolt;
			txtLmtVoltVal.Text = string.Format("{0:0}", limit.valLimit);
			if (limit.isLoHi) chkLmtVoltHi.Checked = true;
			else chkLmtVoltLo.Checked = true;

			limit = rwData.LimitCurr;
			txtLmtCurrVal.Text = string.Format("{0:f1}", limit.valLimit);
			if (limit.isLoHi) chkLmtCurrHi.Checked = true;
			else chkLmtCurrLo.Checked = true;

			limit = rwData.LimitPH;
			txtLmtPHVal.Text = string.Format("{0:f1}", limit.valLimit);
			if (limit.isLoHi) chkLmtPHHi.Checked = true;
			else chkLmtPHLo.Checked = true;

			limit = rwData.LimitTemp;
			txtLmtTempVal.Text = string.Format("{0:0}", limit.valLimit);
			if (limit.isLoHi) chkLmtTempHi.Checked = true;
			else chkLmtTempLo.Checked = true;

			limit = rwData.LimitFan;
			txtLmtFanVal.Text = string.Format("{0:f1}", limit.valLimit);
			if (limit.isLoHi) chkLmtFanHi.Checked = true;
			else chkLmtFanLo.Checked = true;

			limit = rwData.LimitDif1;
			txtLmtDif1Val.Text = string.Format("{0:0}", limit.valLimit);
			if (limit.isLoHi) chkLmtDif1Hi.Checked = true;
			else chkLmtDif1Lo.Checked = true;

			limit = rwData.LimitDif2;
			txtLmtDif2Val.Text = string.Format("{0:0}", limit.valLimit);
			if (limit.isLoHi) chkLmtDif2Hi.Checked = true;
			else chkLmtDif2Lo.Checked = true;

			limit = rwData.LimitDif3;
			txtLmtDif3Val.Text = string.Format("{0:0}", limit.valLimit);
			if (limit.isLoHi) chkLmtDif3Hi.Checked = true;
			else chkLmtDif3Lo.Checked = true;

			limit = rwData.LimitDif4;
			txtLmtDif4Val.Text = string.Format("{0:0}", limit.valLimit);
			if (limit.isLoHi) chkLmtDif4Hi.Checked = true;
			else chkLmtDif4Lo.Checked = true;

			limit = rwData.LimitSp;
			txtLmtSpVal.Text = string.Format("{0:f1}", limit.valLimit);
			if (limit.isLoHi) chkLmtSpHi.Checked = true;
			else chkLmtSpLo.Checked = true;

			// Fan CURR 설정
			fanSet = rwData.FanCurr1;
			txtFan1DpMin.Text = string.Format("{0:f1}", fanSet.hdrCurrMin);
			txtFan1DpMax.Text = string.Format("{0:f1}", fanSet.hdrCurrMax);
			txtFan1VtMin.Text = string.Format("{0:f2}", fanSet.valVoltMin);
			txtFan1VtMax.Text = string.Format("{0:f2}", fanSet.valVoltMax);
			txtFan1Offset.Text = string.Format("{0:f2}", fanSet.offset);
			chkFan1Mask.Checked = fanSet.mask;
			txtFan1DpStep1.Text = string.Format("{0:f1}", fanSet.crStep1);
			txtFan1DpStep2.Text = string.Format("{0:f1}", fanSet.crStep2);
			txtFan1DpStep3.Text = string.Format("{0:f1}", fanSet.crStep3);
			txtFan1DpStep4.Text = string.Format("{0:f1}", fanSet.crStep4);
			txtFan1DpStep5.Text = string.Format("{0:f1}", fanSet.crStep5);
			txtFan1VtStep1.Text = string.Format("{0:f2}", fanSet.vtStep1);
			txtFan1VtStep2.Text = string.Format("{0:f2}", fanSet.vtStep2);
			txtFan1VtStep3.Text = string.Format("{0:f2}", fanSet.vtStep3);
			txtFan1VtStep4.Text = string.Format("{0:f2}", fanSet.vtStep4);
			txtFan1VtStep5.Text = string.Format("{0:f2}", fanSet.vtStep5);

			fanSet = rwData.FanCurr2;
			txtFan2DpMin.Text = string.Format("{0:f1}", fanSet.hdrCurrMin);
			txtFan2DpMax.Text = string.Format("{0:f1}", fanSet.hdrCurrMax);
			txtFan2VtMin.Text = string.Format("{0:f2}", fanSet.valVoltMin);
			txtFan2VtMax.Text = string.Format("{0:f2}", fanSet.valVoltMax);
			txtFan2Offset.Text = string.Format("{0:f2}", fanSet.offset);
			chkFan2Mask.Checked = fanSet.mask;
			txtFan2DpStep1.Text = string.Format("{0:f1}", fanSet.crStep1);
			txtFan2DpStep2.Text = string.Format("{0:f1}", fanSet.crStep2);
			txtFan2DpStep3.Text = string.Format("{0:f1}", fanSet.crStep3);
			txtFan2DpStep4.Text = string.Format("{0:f1}", fanSet.crStep4);
			txtFan2DpStep5.Text = string.Format("{0:f1}", fanSet.crStep5);
			txtFan2VtStep1.Text = string.Format("{0:f2}", fanSet.vtStep1);
			txtFan2VtStep2.Text = string.Format("{0:f2}", fanSet.vtStep2);
			txtFan2VtStep3.Text = string.Format("{0:f2}", fanSet.vtStep3);
			txtFan2VtStep4.Text = string.Format("{0:f2}", fanSet.vtStep4);
			txtFan2VtStep5.Text = string.Format("{0:f2}", fanSet.vtStep5);

			fanSet = rwData.FanCurr3;
			txtFan3DpMin.Text = string.Format("{0:f1}", fanSet.hdrCurrMin);
			txtFan3DpMax.Text = string.Format("{0:f1}", fanSet.hdrCurrMax);
			txtFan3VtMin.Text = string.Format("{0:f2}", fanSet.valVoltMin);
			txtFan3VtMax.Text = string.Format("{0:f2}", fanSet.valVoltMax);
			txtFan3Offset.Text = string.Format("{0:f2}", fanSet.offset);
			chkFan3Mask.Checked = fanSet.mask;
			txtFan3DpStep1.Text = string.Format("{0:f1}", fanSet.crStep1);
			txtFan3DpStep2.Text = string.Format("{0:f1}", fanSet.crStep2);
			txtFan3DpStep3.Text = string.Format("{0:f1}", fanSet.crStep3);
			txtFan3DpStep4.Text = string.Format("{0:f1}", fanSet.crStep4);
			txtFan3DpStep5.Text = string.Format("{0:f1}", fanSet.crStep5);
			txtFan3VtStep1.Text = string.Format("{0:f2}", fanSet.vtStep1);
			txtFan3VtStep2.Text = string.Format("{0:f2}", fanSet.vtStep2);
			txtFan3VtStep3.Text = string.Format("{0:f2}", fanSet.vtStep3);
			txtFan3VtStep4.Text = string.Format("{0:f2}", fanSet.vtStep4);
			txtFan3VtStep5.Text = string.Format("{0:f2}", fanSet.vtStep5);

			fanSet = rwData.FanCurr4;
			txtFan4DpMin.Text = string.Format("{0:f1}", fanSet.hdrCurrMin);
			txtFan4DpMax.Text = string.Format("{0:f1}", fanSet.hdrCurrMax);
			txtFan4VtMin.Text = string.Format("{0:f2}", fanSet.valVoltMin);
			txtFan4VtMax.Text = string.Format("{0:f2}", fanSet.valVoltMax);
			txtFan4Offset.Text = string.Format("{0:f2}", fanSet.offset);
			chkFan4Mask.Checked = fanSet.mask;
			txtFan4DpStep1.Text = string.Format("{0:f1}", fanSet.crStep1);
			txtFan4DpStep2.Text = string.Format("{0:f1}", fanSet.crStep2);
			txtFan4DpStep3.Text = string.Format("{0:f1}", fanSet.crStep3);
			txtFan4DpStep4.Text = string.Format("{0:f1}", fanSet.crStep4);
			txtFan4DpStep5.Text = string.Format("{0:f1}", fanSet.crStep5);
			txtFan4VtStep1.Text = string.Format("{0:f2}", fanSet.vtStep1);
			txtFan4VtStep2.Text = string.Format("{0:f2}", fanSet.vtStep2);
			txtFan4VtStep3.Text = string.Format("{0:f2}", fanSet.vtStep3);
			txtFan4VtStep4.Text = string.Format("{0:f2}", fanSet.vtStep4);
			txtFan4VtStep5.Text = string.Format("{0:f2}", fanSet.vtStep5);

        }

		public int ModbusData(int page, int position)
		{
			int ret = 0;
			ret = m_rtu.Words[page][position];
			return ret;
		}


		private bool isValueCheck = true;
        private bool CheckInputValue(Control.ControlCollection conColl)
        {
            if (!isValueCheck) return isValueCheck;
            foreach(Control con in conColl)
            {
                if (con.Controls.Count > 0)
                    CheckInputValue(con.Controls);

                if (con is TextBox)
                {
                    TextBox tb = (TextBox)con;
                    if (tb.Tag == null) continue;
                    String tag = tb.Tag.ToString();
                    if (tag == null || tag.Equals("")) continue;

                    String[] args = tb.Tag.ToString().Split(',');
                    if (args.Length < 3) continue;
                    float min = CommonUtil.String2Float(args[1]);
                    float max = CommonUtil.String2Float(args[2]);
                    //Debug.WriteLine("tb Name : {0}, min : {1}, max : {2}", tb.Name, min, max);
                    if (CommonUtil.String2Float(tb.Text) < min || CommonUtil.String2Float(tb.Text) > max)
                    {
                        isValueCheck = false;
                        MessageBox.Show(string.Format("입력 범위를 벗어 났습니다. 입력범위 : {0} ~ {1}, {2} {3}", min, max, tb.Text, tb.Name), "입력오류");
                        tb.Focus();
                        break;
                    }
                }
            }

            return isValueCheck;
        }

        private void SaveAppData(bool doSaveFile = false)
        {
			int[] tmpRw = new int[83];
            // 전압센서
            rwSensor = rwData.voltSensor;
            rwSensor.hdrMin = CommonUtil.String2Int(txtVoltHdrMin.Text);
            rwSensor.hdrMax = CommonUtil.String2Int(txtVoltHdrMax.Text);
            rwSensor.valMin = CommonUtil.String2Float(txtVoltValMin.Text);
            rwSensor.valMax = CommonUtil.String2Float(txtVoltValMax.Text);
            rwSensor.offset = CommonUtil.String2Float(txtVoltOffset.Text);
            rwSensor.maskVal = CommonUtil.String2Int(txtVoltMulti.Text);
            rwSensor.mask = chkVoltMask.Checked;
			SaveTmpRw((int)RwRegister.VOLT_HDR, rwSensor.GetData(0), ref tmpRw);

			// 전류센서
			rwSensor = rwData.currSensor;
            rwSensor.hdrMin = CommonUtil.String2Float(txtCurrHdrMin.Text);
            rwSensor.hdrMax = CommonUtil.String2Float(txtCurrHdrMax.Text);
            rwSensor.valMin = CommonUtil.String2Float(txtCurrValMin.Text);
            rwSensor.valMax = CommonUtil.String2Float(txtCurrValMax.Text);
            rwSensor.offset = CommonUtil.String2Float(txtCurrOffset.Text);
            rwSensor.maskVal = CommonUtil.String2Int(txtCurrMulti.Text);
            rwSensor.mask = chkElecMask.Checked;
			SaveTmpRw((int)RwRegister.CURR_HDR, rwSensor.GetData(), ref tmpRw);

			// PH센서
			rwSensor = rwData.phSensor;
            rwSensor.hdrMin = CommonUtil.String2Float(txtPhHdrMin.Text);
            rwSensor.hdrMax = CommonUtil.String2Float(txtPhHdrMax.Text);
            rwSensor.valMin = CommonUtil.String2Float(txtPhValMin.Text);
            rwSensor.valMax = CommonUtil.String2Float(txtPhValMax.Text);
            rwSensor.offset = CommonUtil.String2Float(txtPhOffset.Text);
            rwSensor.maskVal = CommonUtil.String2Int(txtPHMulti.Text);
            rwSensor.mask = chkPHMask.Checked;
			SaveTmpRw((int)RwRegister.PH_HDR, rwSensor.GetData(), ref tmpRw);

			// 온도센서
			rwData.tempSensor.hdrTemp = CommonUtil.String2Int(txtTempStdTemp.Text);
            rwData.tempSensor.hdrResi = CommonUtil.String2Int(txtTempStdResist.Text);
            rwData.tempSensor.valKelv = CommonUtil.String2Int(txtTempKelvin.Text);
            rwData.tempSensor.offset = CommonUtil.String2Float(txtTempOffset.Text);
            rwData.tempSensor.mask = chkTempMask.Checked;
			SaveTmpRw((int)RwRegister.TEMP_HDR, rwData.tempSensor.GetData(), ref tmpRw);
			// 예비센서
			rwSensor = rwData.spSensor;
			rwSensor.sensorType = RwSensor.SENSOR_SPAR;   // 예비센서임을 정의(원칙은 modbus에서 읽을때 설정됨)
			rwSensor.hdrMin = CommonUtil.String2Int(txtSpHdrMin.Text) < 0 ? CommonUtil.String2Int(txtSpHdrMin.Text) : CommonUtil.String2Int(txtSpHdrMin.Text) * -1;
            rwSensor.hdrMax = CommonUtil.String2Int(txtSpHdrMax.Text);
            rwSensor.valMin = CommonUtil.String2Float(txtSpValMin.Text);
            rwSensor.valMax = CommonUtil.String2Float(txtSpValMax.Text);
            rwSensor.offset = CommonUtil.String2Float(txtSpOffset.Text);
            rwSensor.maskVal = CommonUtil.String2Int(txtSpMulti.Text);
            rwSensor.mask = chkSpMask.Checked;
			SaveTmpRw((int)RwRegister.SP_HDR, rwSensor.GetData(), ref tmpRw);
			// 예비센서2
			rwSensor = rwData.sp2Sensor;
			rwSensor.sensorType = RwSensor.SENSOR_COMM;   // 예비센서임을 정의(원칙은 modbus에서 읽을때 설정됨)
			rwSensor.hdrMin = CommonUtil.String2Float(txtSp2HdrMin.Text);
			rwSensor.hdrMax = CommonUtil.String2Float(txtSp2HdrMax.Text);
			rwSensor.valMin = CommonUtil.String2Float(txtSp2ValMin.Text);
			rwSensor.valMax = CommonUtil.String2Float(txtSp2ValMax.Text);
			rwSensor.offset = CommonUtil.String2Float(txtSp2Offset.Text);
			rwSensor.maskVal = CommonUtil.String2Int(txtSp2Multi.Text);
			rwSensor.mask = chkSpMask.Checked;
			//SaveTmpRw((int)RwRegister.SP_HDR, rwSensor.GetData(), ref tmpRw);
			// 예비센서3
			rwSensor = rwData.sp3Sensor;
			rwSensor.sensorType = RwSensor.SENSOR_COMM;   // 예비센서임을 정의(원칙은 modbus에서 읽을때 설정됨)
			rwSensor.hdrMin = CommonUtil.String2Float(txtSp3HdrMin.Text);
			rwSensor.hdrMax = CommonUtil.String2Float(txtSp3HdrMax.Text);
			rwSensor.valMin = CommonUtil.String2Float(txtSp3ValMin.Text);
			rwSensor.valMax = CommonUtil.String2Float(txtSp3ValMax.Text);
			rwSensor.offset = CommonUtil.String2Float(txtSp3Offset.Text);
			rwSensor.maskVal = CommonUtil.String2Int(txtSp3Multi.Text);
			rwSensor.mask = chkSpMask.Checked;
			//SaveTmpRw((int)RwRegister.SP_HDR, rwSensor.GetData(), ref tmpRw);
			// 공란센서1 단위
			Settings.Default.sp2Name = txtSp2Name.Text;
			Settings.Default.sp2HdrUnit = comSp2HdrUnit.Text;
            Settings.Default.sp2ValUnit = comSp2ValUnit.Text;
			Settings.Default.sp2OffsetUnit = comSp2OffsetUnit.Text;

			// 공란센서2 단위
			Settings.Default.sp3Name = txtSp3Name.Text;
			Settings.Default.sp3HdrUnit = comSp3HdrUnit.Text;
			Settings.Default.sp3ValUnit = comSp3ValUnit.Text;
			Settings.Default.sp3OffsetUnit = comSp3OffsetUnit.Text;
			Settings.Default.Save();

			// 기타 42041 ~ 42043(Door, Control, Air Count)
			rwData.DrBzVal.val4 = chkBuzzerMask.Checked ? 1 : 0;
            rwData.DrBzVal.val3 = chkDoorMask.Checked ? 1 : 0;
			rwData.DrBzVal.val2 = comAvoi.SelectedIndex;
			rwData.DrBzVal.val1 = comDdmot.SelectedIndex;
			tmpRw[(int)RwRegister.DOOR_VAL - 42001] = rwData.DrBzVal.GetData();

			rwData.ControlVal.val4 = comAvoht.SelectedIndex;
			tmpRw[(int)RwRegister.CONTROL_VAL - 42001] = rwData.ControlVal.GetData();

			rwData.AirCountVal.val4 = comDustCount.SelectedIndex;
			rwData.AirCountVal.val3 = 0;
			rwData.AirCountVal.val2 = comAirCount.SelectedIndex;
			rwData.AirCountVal.val1 = 0;
			tmpRw[(int)RwRegister.AIR_COUNT_VAL - 42001] = rwData.AirCountVal.GetData();

			// Fan 결선제어
			rwData.FBSensor.hdrSngYD = chkFBYD.Checked;
			rwData.FBSensor.valYDDelay = CommonUtil.String2Int(txtFBYDDelay.Text);
			rwData.FBSensor.valFanDelay = CommonUtil.String2Int(txtFBFanDelay.Text);
			SaveTmpRw((int)RwRegister.FB_HDR, rwData.FBSensor.GetData(), ref tmpRw);

            // 댐퍼설정
			RW2Regi rw2Regi = rwData.damperSet;
			rw2Regi.hdr.valLo = chkDamOn.Checked ? 0x01 : 0x00;
            if (!chkDamOn.Checked)
            {
                comDamAng.SelectedIndex = 0;
            }
            rw2Regi.hdr.valHi = CommonUtil.String2Int(comDamAng.Text);
            rw2Regi.val.valHi = chkDamInhMsk.Checked ? 0x01 : 0x00;
            rw2Regi.val.valLo = chkDamVenMsk.Checked ? 0x01 : 0x00;
            SaveTmpRw((int)RwRegister.DAM_HDR, rw2Regi.GetData(), ref tmpRw);

			// IoT 전송
			rw2Regi = rwData.IoTSet;
			rw2Regi.hdr.val1 = chkIot1.Checked ? 0x01 : 0x00;
			rw2Regi.hdr.val2 = chkIot2.Checked ? 0x01 : 0x00;
			rw2Regi.hdr.val3 = chkIot3.Checked ? 0x01 : 0x00;
			rw2Regi.hdr.val4 = chkIot4.Checked ? 0x01 : 0x00;
			rw2Regi.val.val1 = chkIot5.Checked ? 0x01 : 0x00;
			rw2Regi.val.val2 = chkIot6.Checked ? 0x01 : 0x00;
			rw2Regi.val.val3 = chkIot7.Checked ? 0x01 : 0x00;
			rw2Regi.val.val4 = chkIot8.Checked ? 0x01 : 0x00;
			SaveTmpRw((int)RwRegister.IOT_HDR, rw2Regi.GetData(), ref tmpRw);

			// 차압센서
			int uMulti = 0;
			rwSensor = rwData.difSensor[0];
			rwSensor.sensorType = RwSensor.SENSOR_DIFF;   // 차압센서임을 정의(원칙은 modbus에서 읽을때 설정됨)
			rwSensor.hdrMin = CommonUtil.String2Float(txtDiff1MinDisp.Text);
			rwSensor.hdrMax = CommonUtil.String2Float(txtDiff1MaxDisp.Text);
			rwSensor.valMin = CommonUtil.String2Float(txtDiff1MinVolt.Text);
			rwSensor.valMax = CommonUtil.String2Float(txtDiff1MaxVolt.Text);
			rwSensor.valCen = CommonUtil.String2Float(txtDiff1CenVolt.Text);
			rwSensor.offset = CommonUtil.String2Float(txtDiff1Offset.Text);
			//rwSensor.maskVal = CommonUtil.String2Int(txtDiff1Multi.Text);
			rwSensor.mask = chkDiff1Mask.Checked;
			uMulti = comDiff1Unit.SelectedIndex;    // 0 : Kpa, 1 : mmH2O
			SaveTmpRw((int)RwRegister.DIF1_HDR, rwSensor.GetData(uMulti), ref tmpRw);

			rwSensor = rwData.difSensor[1];
			rwSensor.sensorType = RwSensor.SENSOR_DIFF;   // 차압센서임을 정의(원칙은 modbus에서 읽을때 설정됨)
			rwSensor.hdrMin = CommonUtil.String2Float(txtDiff2MinDisp.Text);
			rwSensor.hdrMax = CommonUtil.String2Float(txtDiff2MaxDisp.Text);
			rwSensor.valMin = CommonUtil.String2Float(txtDiff2MinVolt.Text);
			rwSensor.valMax = CommonUtil.String2Float(txtDiff2MaxVolt.Text);
			rwSensor.valCen = CommonUtil.String2Float(txtDiff2CenVolt.Text);
			rwSensor.offset = CommonUtil.String2Float(txtDiff2Offset.Text);
			//rwSensor.maskVal = CommonUtil.String2Int(txtDiff2Multi.Text);
			rwSensor.mask = chkDiff2Mask.Checked;
			uMulti = comDiff2Unit.SelectedIndex;
			SaveTmpRw((int)RwRegister.DIF2_HDR, rwSensor.GetData(uMulti), ref tmpRw);

			rwSensor = rwData.difSensor[2];
			rwSensor.sensorType = RwSensor.SENSOR_DIFF;   // 차압센서임을 정의(원칙은 modbus에서 읽을때 설정됨)
			rwSensor.hdrMin = CommonUtil.String2Float(txtDiff3MinDisp.Text);
			rwSensor.hdrMax = CommonUtil.String2Float(txtDiff3MaxDisp.Text);
			rwSensor.valMin = CommonUtil.String2Float(txtDiff3MinVolt.Text);
			rwSensor.valMax = CommonUtil.String2Float(txtDiff3MaxVolt.Text);
			rwSensor.valCen = CommonUtil.String2Float(txtDiff3CenVolt.Text);
			rwSensor.offset = CommonUtil.String2Float(txtDiff3Offset.Text);
			//rwSensor.maskVal = CommonUtil.String2Int(txtDiff3Multi.Text);
			rwSensor.mask = chkDiff3Mask.Checked;
			uMulti = comDiff3Unit.SelectedIndex;
			SaveTmpRw((int)RwRegister.DIF3_HDR, rwSensor.GetData(uMulti), ref tmpRw);

			rwSensor = rwData.difSensor[3];
			rwSensor.sensorType = RwSensor.SENSOR_DIFF;   // 차압센서임을 정의(원칙은 modbus에서 읽을때 설정됨)
			rwSensor.hdrMin = CommonUtil.String2Float(txtDiff4MinDisp.Text);
			rwSensor.hdrMax = CommonUtil.String2Float(txtDiff4MaxDisp.Text);
			rwSensor.valMin = CommonUtil.String2Float(txtDiff4MinVolt.Text);
			rwSensor.valMax = CommonUtil.String2Float(txtDiff4MaxVolt.Text);
			rwSensor.valCen = CommonUtil.String2Float(txtDiff4CenVolt.Text);
			rwSensor.offset = CommonUtil.String2Float(txtDiff4Offset.Text);
			//rwSensor.maskVal = CommonUtil.String2Int(txtDiff4Multi.Text);
			rwSensor.mask = chkDiff4Mask.Checked;
			uMulti = comDiff4Unit.SelectedIndex;
			SaveTmpRw((int)RwRegister.DIF4_HDR, rwSensor.GetData(uMulti), ref tmpRw);

			// 센서 LIMIT Value
			SensorLimit limit = rwData.LimitVolt;
			limit.valLimit = CommonUtil.String2Float(txtLmtVoltVal.Text);
			limit.isLoHi = chkLmtVoltHi.Checked;
			tmpRw[(int)RwRegister.LMT_VOLT_VAL - 42001] = limit.GetData(0);

			limit = rwData.LimitCurr;
			limit.valLimit = CommonUtil.String2Float(txtLmtCurrVal.Text);
			limit.isLoHi = chkLmtCurrHi.Checked;
			tmpRw[(int)RwRegister.LMT_CURR_VAL - 42001] = limit.GetData();

			limit = rwData.LimitPH;
			limit.valLimit = CommonUtil.String2Float(txtLmtPHVal.Text);
			limit.isLoHi = chkLmtPHHi.Checked;
			tmpRw[(int)RwRegister.LMT_PH_VAL - 42001] = limit.GetData();

			limit = rwData.LimitTemp;
			limit.valLimit = CommonUtil.String2Float(txtLmtTempVal.Text);
			limit.isLoHi = chkLmtTempHi.Checked;
			tmpRw[(int)RwRegister.LMT_TEMP_VAL - 42001] = limit.GetData(0);

			limit = rwData.LimitFan;
			limit.valLimit = CommonUtil.String2Float(txtLmtFanVal.Text);
			limit.isLoHi = chkLmtFanHi.Checked;
			tmpRw[(int)RwRegister.LMT_FAN_VAL - 42001] = limit.GetData();

			limit = rwData.LimitDif1;
			limit.valLimit = CommonUtil.String2Float(txtLmtDif1Val.Text);
			limit.isLoHi = chkLmtDif1Hi.Checked;
			tmpRw[(int)RwRegister.LMT_DIF1_VAL - 42001] = limit.GetData(0);

			limit = rwData.LimitDif2;
			limit.valLimit = CommonUtil.String2Float(txtLmtDif2Val.Text);
			limit.isLoHi = chkLmtDif2Hi.Checked;
			tmpRw[(int)RwRegister.LMT_DIF2_VAL - 42001] = limit.GetData(0);

			limit = rwData.LimitDif3;
			limit.valLimit = CommonUtil.String2Float(txtLmtDif3Val.Text);
			limit.isLoHi = chkLmtDif3Hi.Checked;
			tmpRw[(int)RwRegister.LMT_DIF3_VAL - 42001] = limit.GetData(0);

			limit = rwData.LimitDif4;
			limit.valLimit = CommonUtil.String2Float(txtLmtDif4Val.Text);
			limit.isLoHi = chkLmtDif4Hi.Checked;
			tmpRw[(int)RwRegister.LMT_DIF4_VAL - 42001] = limit.GetData(0);

			limit = rwData.LimitSp;
			limit.valLimit = CommonUtil.String2Float(txtLmtSpVal.Text);
			limit.isLoHi = chkLmtSpHi.Checked;
			tmpRw[(int)RwRegister.LMT_SP_VAL - 42001] = limit.GetData();

			// Fan CURR 설정
			fanSet = rwData.FanCurr1;
			fanSet.hdrCurrMin = CommonUtil.String2Float(txtFan1DpMin.Text);
			fanSet.hdrCurrMax = CommonUtil.String2Float(txtFan1DpMax.Text);
			fanSet.valVoltMin = CommonUtil.String2Float(txtFan1VtMin.Text);
			fanSet.valVoltMax = CommonUtil.String2Float(txtFan1VtMax.Text);
			fanSet.crStep1 = CommonUtil.String2Float(txtFan1DpStep1.Text);
			fanSet.crStep2 = CommonUtil.String2Float(txtFan1DpStep2.Text);
			fanSet.crStep3 = CommonUtil.String2Float(txtFan1DpStep3.Text);
			fanSet.crStep4 = CommonUtil.String2Float(txtFan1DpStep4.Text);
			fanSet.crStep5 = CommonUtil.String2Float(txtFan1DpStep5.Text);
			fanSet.vtStep1 = CommonUtil.String2Float(txtFan1VtStep1.Text);
			fanSet.vtStep2 = CommonUtil.String2Float(txtFan1VtStep2.Text);
			fanSet.vtStep3 = CommonUtil.String2Float(txtFan1VtStep3.Text);
			fanSet.vtStep4 = CommonUtil.String2Float(txtFan1VtStep4.Text);
			fanSet.vtStep5 = CommonUtil.String2Float(txtFan1VtStep5.Text);
			fanSet.offset = CommonUtil.String2Float(txtFan1Offset.Text);
			fanSet.mask = chkFan1Mask.Checked;
			SaveTmpRw((int)RwRegister.FAN1_HDR, fanSet.GetData(), ref tmpRw);

			fanSet = rwData.FanCurr2;
			fanSet.hdrCurrMin = CommonUtil.String2Float(txtFan2DpMin.Text);
			fanSet.hdrCurrMax = CommonUtil.String2Float(txtFan2DpMax.Text);
			fanSet.valVoltMin = CommonUtil.String2Float(txtFan2VtMin.Text);
			fanSet.valVoltMax = CommonUtil.String2Float(txtFan2VtMax.Text);
			fanSet.crStep1 = CommonUtil.String2Float(txtFan2DpStep1.Text);
			fanSet.crStep2 = CommonUtil.String2Float(txtFan2DpStep2.Text);
			fanSet.crStep3 = CommonUtil.String2Float(txtFan2DpStep3.Text);
			fanSet.crStep4 = CommonUtil.String2Float(txtFan2DpStep4.Text);
			fanSet.crStep5 = CommonUtil.String2Float(txtFan2DpStep5.Text);
			fanSet.vtStep1 = CommonUtil.String2Float(txtFan2VtStep1.Text);
			fanSet.vtStep2 = CommonUtil.String2Float(txtFan2VtStep2.Text);
			fanSet.vtStep3 = CommonUtil.String2Float(txtFan2VtStep3.Text);
			fanSet.vtStep4 = CommonUtil.String2Float(txtFan2VtStep4.Text);
			fanSet.vtStep5 = CommonUtil.String2Float(txtFan2VtStep5.Text);
			fanSet.offset = CommonUtil.String2Float(txtFan2Offset.Text);
			fanSet.mask = chkFan2Mask.Checked;
			SaveTmpRw((int)RwRegister.FAN2_HDR, fanSet.GetData(), ref tmpRw);

			fanSet = rwData.FanCurr3;
			fanSet.hdrCurrMin = CommonUtil.String2Float(txtFan3DpMin.Text);
			fanSet.hdrCurrMax = CommonUtil.String2Float(txtFan3DpMax.Text);
			fanSet.valVoltMin = CommonUtil.String2Float(txtFan3VtMin.Text);
			fanSet.valVoltMax = CommonUtil.String2Float(txtFan3VtMax.Text);
			fanSet.crStep1 = CommonUtil.String2Float(txtFan3DpStep1.Text);
			fanSet.crStep2 = CommonUtil.String2Float(txtFan3DpStep2.Text);
			fanSet.crStep3 = CommonUtil.String2Float(txtFan3DpStep3.Text);
			fanSet.crStep4 = CommonUtil.String2Float(txtFan3DpStep4.Text);
			fanSet.crStep5 = CommonUtil.String2Float(txtFan3DpStep5.Text);
			fanSet.vtStep1 = CommonUtil.String2Float(txtFan3VtStep1.Text);
			fanSet.vtStep2 = CommonUtil.String2Float(txtFan3VtStep2.Text);
			fanSet.vtStep3 = CommonUtil.String2Float(txtFan3VtStep3.Text);
			fanSet.vtStep4 = CommonUtil.String2Float(txtFan3VtStep4.Text);
			fanSet.vtStep5 = CommonUtil.String2Float(txtFan3VtStep5.Text);
			fanSet.offset = CommonUtil.String2Float(txtFan3Offset.Text);
			fanSet.mask = chkFan3Mask.Checked;
			SaveTmpRw((int)RwRegister.FAN3_HDR, fanSet.GetData(), ref tmpRw);

			fanSet = rwData.FanCurr4;
			fanSet.hdrCurrMin = CommonUtil.String2Float(txtFan4DpMin.Text);
			fanSet.hdrCurrMax = CommonUtil.String2Float(txtFan4DpMax.Text);
			fanSet.valVoltMin = CommonUtil.String2Float(txtFan4VtMin.Text);
			fanSet.valVoltMax = CommonUtil.String2Float(txtFan4VtMax.Text);
			fanSet.crStep1 = CommonUtil.String2Float(txtFan4DpStep1.Text);
			fanSet.crStep2 = CommonUtil.String2Float(txtFan4DpStep2.Text);
			fanSet.crStep3 = CommonUtil.String2Float(txtFan4DpStep3.Text);
			fanSet.crStep4 = CommonUtil.String2Float(txtFan4DpStep4.Text);
			fanSet.crStep5 = CommonUtil.String2Float(txtFan4DpStep5.Text);
			fanSet.vtStep1 = CommonUtil.String2Float(txtFan4VtStep1.Text);
			fanSet.vtStep2 = CommonUtil.String2Float(txtFan4VtStep2.Text);
			fanSet.vtStep3 = CommonUtil.String2Float(txtFan4VtStep3.Text);
			fanSet.vtStep4 = CommonUtil.String2Float(txtFan4VtStep4.Text);
			fanSet.vtStep5 = CommonUtil.String2Float(txtFan4VtStep5.Text);
			fanSet.offset = CommonUtil.String2Float(txtFan4Offset.Text);
			fanSet.mask = chkFan4Mask.Checked;
			SaveTmpRw((int)RwRegister.FAN4_HDR, fanSet.GetData(), ref tmpRw);

			// RwData(83 Register = 2 * 83 = 166 byte) 한번에 보내기
			if (doSaveFile == false)
				SendModbusDataArray(slaveId, (int)RwRegister.VOLT_HDR, tmpRw);
			else
				Save2File(ref tmpRw);
		}

		private void Save2File(ref int[] tmpRw)
		{
			try
			{
				BinaryWriter bw = new BinaryWriter(new FileStream("RW.dat", FileMode.Create));

				for (int i = 0; i < tmpRw.Length; i++)
				{
					bw.Write((UInt16)tmpRw[i]);
				}

				bw.Close();
			} catch(IOException)
			{
				MessageBox.Show("다른 프로세스에서 파일을 사용중입니다.", "알림");
				return;
			} catch(Exception)
			{
				MessageBox.Show("파일저장에 실패했습니다.", "알림");
				return;
			}
			MessageBox.Show("파일저장이 완료되었습니다.", "알림");
		}

		private void Load4File()
		{
			try
			{
				FileInfo info = new FileInfo("RW.dat");
				if(!info.Exists)
				{
					MessageBox.Show("저장된 파일이 없습니다.", "알림");
					return;
				}
				int cnt = (int)info.Length / sizeof(UInt16);
				UInt16[] rw_data = new UInt16[cnt];

				BinaryReader br = new BinaryReader(new FileStream(info.Name, FileMode.Open));
				for (int i = 0; i < cnt; i++)
				{
					rw_data[i] = br.ReadUInt16();
				}
                br.Close();

				tmpRwData = new RWData();
				// 전압센서 설정(42001 ~ 42003) 0
				//rwData.voltSensor = MakeSenserData(rw_data[0], rw_data[1], rw_data[2]);
				tmpRwData.voltSensor.SetData(rw_data[0], rw_data[1], rw_data[2]);
				// 전류센서 설정(42004 ~ 42006) 3
				tmpRwData.currSensor.SetData(rw_data[3], rw_data[4], rw_data[5]);
				// PH센서 설정(42007 ~ 42009) 6
				tmpRwData.phSensor.SetData(rw_data[6], rw_data[7], rw_data[8]);
				// 온도센서 설정(42010 ~ 42012) 9 TODO
				tmpRwData.tempSensor.SetData(rw_data[9], rw_data[10], rw_data[11]);
				// FAN결선제어 설정(42013 ~ 42015) 12
				tmpRwData.FBSensor.SetData(rw_data[12], rw_data[13]);
				// 댐퍼설정(42015 ~ 42016)
				tmpRwData.damperSet.SetData(rw_data[14], rw_data[15]);
				// IoT 전송(42017 ~ 42018)
				tmpRwData.IoTSet.SetData(rw_data[16], rw_data[17]);
				// 차압센서1 ~ 4 LIMIT(42019 ~ 42022)
				tmpRwData.LimitDif1.SetData(rw_data[18]);
				tmpRwData.LimitDif2.SetData(rw_data[19]);
				tmpRwData.LimitDif3.SetData(rw_data[20]);
				tmpRwData.LimitDif4.SetData(rw_data[21]);
				// rw_data[14]: 사용안함
				//// FAN2센서 설정(42016 ~ 42018) 15
				//         rwData.arrFanSensor[1].SetData(rw_data[15], rw_data[16], rw_data[17]);
				//// FAN2센서 설정(42019 ~ 42021) 18
				//         rwData.arrFanSensor[2].SetData(rw_data[18], rw_data[19], rw_data[20]);
				//// FAN3센서 설정(42022 ~ 42024) 21
				//         rwData.arrFanSensor[3].SetData(rw_data[21], rw_data[22], rw_data[23]);
				// DIF1센서 설정(42025 ~ 42027) 24
				tmpRwData.difSensor[0].SetData(rw_data[24], rw_data[25], rw_data[26], RwSensor.SENSOR_DIFF);
                comDiff1Unit.SelectedIndex = tmpRwData.difSensor[0].uMulti;
				// DIF2센서 설정(42028 ~ 42030) 27
				tmpRwData.difSensor[1].SetData(rw_data[27], rw_data[28], rw_data[29], RwSensor.SENSOR_DIFF);
                comDiff1Unit.SelectedIndex = tmpRwData.difSensor[1].uMulti;
                // DIF1센서 설정(42031 ~ 42033) 30
				tmpRwData.difSensor[2].SetData(rw_data[30], rw_data[31], rw_data[32], RwSensor.SENSOR_DIFF);
                comDiff1Unit.SelectedIndex = tmpRwData.difSensor[2].uMulti;
                // DIF1센서 설정(42034 ~ 42036) 33
				tmpRwData.difSensor[3].SetData(rw_data[33], rw_data[34], rw_data[35], RwSensor.SENSOR_DIFF);
                comDiff1Unit.SelectedIndex = tmpRwData.difSensor[3].uMulti;
                // 예비센서(온도센서) 설정(42037 ~ 42039) 36
				tmpRwData.spSensor.SetData(rw_data[36], rw_data[37], rw_data[38], RwSensor.SENSOR_SPAR);
				// TODO 정리 필요
				// Start(42040) 39
				// Door/Buzzer Mask(42041) 40
				tmpRwData.DrBzVal.SetData(rw_data[40]);
				// Control(42042) 41
				tmpRwData.ControlVal.SetData(rw_data[41]);
				// Air Count(42043) 42
				tmpRwData.AirCountVal.SetData(rw_data[42]);
				// Status(42044) 43
				tmpRwData.StatusVal.SetData(rw_data[43]);
				// 전압센서 LIMIT(42045)
				tmpRwData.LimitVolt.SetData(rw_data[44]);
				// 전류센서 LIMIT(42046)
				tmpRwData.LimitCurr.SetData(rw_data[45]);
				// PH센서 LIMIT(42047)
				tmpRwData.LimitPH.SetData(rw_data[46]);
				// 온도센서 LIMIT(42048)
				tmpRwData.LimitTemp.SetData(rw_data[47]);
				// FAN 센서 LIMIT(42049)
				tmpRwData.LimitFan.SetData(rw_data[48]);
				// 차압센서 LIMIT(42050)
				//tmpRwData.LimitDif1.SetData(rw_data[49]);
				// 예비센서 LIMIT(42051)
				tmpRwData.LimitSp.SetData(rw_data[50]);
				// FAN CURR1 설정(42052 ~ 42059)
				tmpRwData.FanCurr1.SetData(rw_data[51], rw_data[52], rw_data[53], rw_data[54], rw_data[55], rw_data[56], rw_data[57], rw_data[58]);
				// FAN CURR2 설정(42060 ~ 42067)
				tmpRwData.FanCurr2.SetData(rw_data[59], rw_data[60], rw_data[61], rw_data[62], rw_data[63], rw_data[64], rw_data[65], rw_data[66]);
				// FAN CURR3 설정(42068 ~ 42075)
				tmpRwData.FanCurr3.SetData(rw_data[67], rw_data[68], rw_data[69], rw_data[70], rw_data[71], rw_data[72], rw_data[73], rw_data[74]);
				// FAN CURR4 설정(42076 ~ 42083)
				tmpRwData.FanCurr4.SetData(rw_data[75], rw_data[76], rw_data[77], rw_data[78], rw_data[79], rw_data[80], rw_data[81], rw_data[82]);

				LoadAppData(true);

				MessageBox.Show("파일불러오기가 완료되었습니다.", "오류");
			}
			catch (Exception)
			{
				MessageBox.Show("저장된 파일이 없습니다.", "오류");
			}
		}

		private void SendModbusData(int slaveid, int addr, int value)
        {
            m_rtu.SetWord(slaveid, addr, value);
        }

        private void SendModbusDataArray(int slaveid, int addr, int[] value)
        {
            m_rtu.SetWords(slaveid, addr, value);
        }

		private void SaveTmpRw(int addr, int[] value, ref int[] tmpRw)
		{
			int idx = addr - 42001;
			for(int i = 0; i < value.Length; i++)
			{
				tmpRw[idx++] = value[i];
			}
		}

        private void btnMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
			LoadAppData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
			// Test 종료 후 풀어줄것
            if (CheckValidate() == false)
            {
                tabControl1.SelectedIndex = tabControl1.TabCount - 1;
                return;
            }

			SaveAppData();

		}

		private void btnSave2File_Click(object sender, EventArgs e)
		{
			SaveAppData(true);
		}

		private void btnLoad4File_Click(object sender, EventArgs e)
		{
			Load4File();
		}

		#region Fan Curr 단계별 입력값 Check
		private bool CheckValidate()
		{
			// FAN CURR1 CHECK
			if (CommonUtil.String2Float(txtFan1DpStep1.Text) > CommonUtil.String2Float(txtFan1DpStep2.Text)
				|| CommonUtil.String2Float(txtFan1DpStep2.Text) > CommonUtil.String2Float(txtFan1DpStep3.Text)
				|| CommonUtil.String2Float(txtFan1DpStep3.Text) > CommonUtil.String2Float(txtFan1DpStep4.Text)
				|| CommonUtil.String2Float(txtFan1DpStep4.Text) > CommonUtil.String2Float(txtFan1DpStep5.Text))
			{
				MessageBox.Show("FAN CURR1 단계별 입력값이 잘못됐습니다.", "입력오류");
				txtFan1DpStep1.Focus();
				return false;
			}
			if (CommonUtil.String2Float(txtFan1VtStep1.Text) > CommonUtil.String2Float(txtFan1VtStep2.Text)
				|| CommonUtil.String2Float(txtFan1VtStep2.Text) > CommonUtil.String2Float(txtFan1VtStep3.Text)
				|| CommonUtil.String2Float(txtFan1VtStep3.Text) > CommonUtil.String2Float(txtFan1VtStep4.Text)
				|| CommonUtil.String2Float(txtFan1VtStep4.Text) > CommonUtil.String2Float(txtFan1VtStep5.Text))
			{
				MessageBox.Show("FAN CURR1 단계별 입력값이 잘못됐습니다.", "입력오류");
				txtFan1VtStep1.Focus();
				return false;
			}

			// FAN CURR2 CHECK
			if (CommonUtil.String2Float(txtFan1DpStep1.Text) > CommonUtil.String2Float(txtFan1DpStep2.Text)
				|| CommonUtil.String2Float(txtFan1DpStep2.Text) > CommonUtil.String2Float(txtFan1DpStep3.Text)
				|| CommonUtil.String2Float(txtFan1DpStep3.Text) > CommonUtil.String2Float(txtFan1DpStep4.Text)
				|| CommonUtil.String2Float(txtFan1DpStep4.Text) > CommonUtil.String2Float(txtFan1DpStep5.Text))
			{
				MessageBox.Show("FAN CURR2 단계별 입력값이 잘못됐습니다.", "입력오류");
				txtFan2DpStep1.Focus();
				return false;
			}
			if (CommonUtil.String2Float(txtFan2VtStep1.Text) > CommonUtil.String2Float(txtFan2VtStep2.Text)
				|| CommonUtil.String2Float(txtFan2VtStep2.Text) > CommonUtil.String2Float(txtFan2VtStep3.Text)
				|| CommonUtil.String2Float(txtFan2VtStep3.Text) > CommonUtil.String2Float(txtFan2VtStep4.Text)
				|| CommonUtil.String2Float(txtFan2VtStep4.Text) > CommonUtil.String2Float(txtFan2VtStep5.Text))
			{
				MessageBox.Show("FAN CURR2 단계별 입력값이 잘못됐습니다.", "입력오류");
				txtFan2VtStep1.Focus();
				return false;
			}

			// FAN CURR3 CHECK
			if (CommonUtil.String2Float(txtFan3DpStep1.Text) > CommonUtil.String2Float(txtFan3DpStep2.Text)
				|| CommonUtil.String2Float(txtFan3DpStep2.Text) > CommonUtil.String2Float(txtFan3DpStep3.Text)
				|| CommonUtil.String2Float(txtFan3DpStep3.Text) > CommonUtil.String2Float(txtFan3DpStep4.Text)
				|| CommonUtil.String2Float(txtFan3DpStep4.Text) > CommonUtil.String2Float(txtFan3DpStep5.Text))
			{
				MessageBox.Show("FAN CURR3 단계별 입력값이 잘못됐습니다.", "입력오류");
				txtFan3DpStep1.Focus();
				return false;
			}
			if (CommonUtil.String2Float(txtFan3VtStep1.Text) > CommonUtil.String2Float(txtFan3VtStep2.Text)
				|| CommonUtil.String2Float(txtFan3VtStep2.Text) > CommonUtil.String2Float(txtFan3VtStep3.Text)
				|| CommonUtil.String2Float(txtFan3VtStep3.Text) > CommonUtil.String2Float(txtFan3VtStep4.Text)
				|| CommonUtil.String2Float(txtFan3VtStep4.Text) > CommonUtil.String2Float(txtFan3VtStep5.Text))
			{
				MessageBox.Show("FAN CURR3 단계별 입력값이 잘못됐습니다.", "입력오류");
				txtFan3VtStep1.Focus();
				return false;
			}

			// FAN CURR4 CHECK
			if (CommonUtil.String2Float(txtFan4DpStep1.Text) > CommonUtil.String2Float(txtFan4DpStep2.Text)
				|| CommonUtil.String2Float(txtFan4DpStep2.Text) > CommonUtil.String2Float(txtFan4DpStep3.Text)
				|| CommonUtil.String2Float(txtFan4DpStep3.Text) > CommonUtil.String2Float(txtFan4DpStep4.Text)
				|| CommonUtil.String2Float(txtFan4DpStep4.Text) > CommonUtil.String2Float(txtFan4DpStep5.Text))
			{
				MessageBox.Show("FAN CURR4 단계별 입력값이 잘못됐습니다.", "입력오류");
				txtFan4DpStep1.Focus();
				return false;
			}
			if (CommonUtil.String2Float(txtFan4VtStep1.Text) > CommonUtil.String2Float(txtFan4VtStep2.Text)
				|| CommonUtil.String2Float(txtFan4VtStep2.Text) > CommonUtil.String2Float(txtFan4VtStep3.Text)
				|| CommonUtil.String2Float(txtFan4VtStep3.Text) > CommonUtil.String2Float(txtFan4VtStep4.Text)
				|| CommonUtil.String2Float(txtFan4VtStep4.Text) > CommonUtil.String2Float(txtFan4VtStep5.Text))
			{
				MessageBox.Show("FAN CURR4 단계별 입력값이 잘못됐습니다.", "입력오류");
				txtFan4VtStep1.Focus();
				return false;
			}

			return true;
		}
		#endregion

		private void btnNext_Click(object sender, EventArgs e)
        {
			tabControl1.SelectedIndex = (tabControl1.SelectedIndex + 1 < tabControl1.TabCount) ?
										 tabControl1.SelectedIndex + 1 : 0;
            SelectPage(tabControl1.SelectedIndex);
		}

		private void SelectPage(int pageNo)
		{
			currPage = pageNo;
			switch (currPage)
			{
				case 0:
					labTitPage.Text = "(1페이지)";
					labPage.Text = "2 페이지";
					txtVoltHdrMin.Focus();
					break;
				case 1:
					labTitPage.Text = "(2페이지)";
					labPage.Text = "3 페이지";
					txtDiff1MaxDisp.Focus();
					break;
				case 2:
					labTitPage.Text = "(3페이지)";
					labPage.Text = "1 페이지";
					txtFan1DpMin.Focus();
					break;
			}
		}

		private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 가상키보드 종료
            Process[] psList = Process.GetProcessesByName("osk");
            if(psList.Length > 0)
            {
                psList[0].Kill();
            }
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            // 가상키보드 실행
            CommonUtil.ShowKeyboard();
        }

        private void digitTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Tag == null) return;
            if (CommonUtil.IsNull(tb.Tag.ToString())) return;
            String[] strType = tb.Tag.ToString().Split(',');
            if (strType.Length < 1) return;

            if(strType[0].ToUpper().Equals("P0"))
            {
                // 양의 정수
                if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            else if (strType[0].Equals("P1") || strType[0].Equals("P2"))
            {
                // 양수 소수점 1, 2자리
                if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != '.' && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            else if (strType[0].Equals("N0"))
            {
                // 음의 정수
                if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            else if (strType[0].Equals("N1") || strType[0].Equals("N2"))
            {
                // 양수 소수점 1, 2자리
                if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            else
            {
                // 양수 소수점 1, 2자리
                if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != '.' && e.KeyChar != '-' && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }

        }

		private void chkFBSingle_CheckedChanged(object sender, EventArgs e)
		{
			chkFBYD.Checked = !((CheckBox)sender).Checked;
		}

		private void chkFBYD_CheckedChanged(object sender, EventArgs e)
		{
			chkFBSingle.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtVoltLo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtVoltHi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtVoltHi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtVoltLo.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtCurrLo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtCurrHi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtCurrHi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtCurrLo.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtPHLo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtPHHi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtPHHi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtPHLo.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtTempLo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtTempHi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtTempHi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtTempLo.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtFanLo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtFanHi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtFanHi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtFanLo.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtDifLo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtDif1Hi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtDifHi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtDif1Lo.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtDif2Lo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtDif2Hi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtDif2Hi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtDif2Lo.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtDif3Lo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtDif3Hi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtDif3Hi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtDif3Lo.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtDif4Lo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtDif4Hi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtDif4Hi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtDif4Lo.Checked = !((CheckBox)sender).Checked;
		}
		private void chkLmtSpLo_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtSpHi.Checked = !((CheckBox)sender).Checked;
		}

		private void chkLmtSpHi_CheckedChanged(object sender, EventArgs e)
		{
			chkLmtSpLo.Checked = !((CheckBox)sender).Checked;
		}

        private void chkDamOn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDamOn.Checked)
            {
                comDamAng.Enabled = true;
            }
            else
            {
                comDamAng.SelectedIndex = 0;
                comDamAng.Enabled = false;
            }
        }

        private void btnResetAir_Click(object sender, EventArgs e)
		{
			rwData.AirCountVal.val1 = 0x01;
			SendModbusData(slaveId, (int)RwRegister.AIR_COUNT_VAL, rwData.AirCountVal.GetData());
			rwData.AirCountVal.val1 = 0x00;
		}

		private void btnDustReset_Click(object sender, EventArgs e)
		{
			rwData.AirCountVal.val3 = 0x01;
			SendModbusData(slaveId, (int)RwRegister.AIR_COUNT_VAL, rwData.AirCountVal.GetData());
			rwData.AirCountVal.val3 = 0x00;
		}

		private void txtFan1Max_Leave(object sender, EventArgs e)
		{
			txtFan1DpStep5.Text = ((TextBox)sender).Text;
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectPage(tabControl1.SelectedIndex);
		}

		private void txtDiff1MaxDisp_Leave(object sender, EventArgs e)
		{
            string format = comDiff1Unit.SelectedIndex == 0 ? "{0:f1}" : "{0:0}";
            txtDiff1MinDisp.Text = string.Format(format, CommonUtil.String2Float(((Control)sender).Text) * -1);
		}

		private void txtDiff2MaxDisp_Leave(object sender, EventArgs e)
		{
            string format = comDiff2Unit.SelectedIndex == 0 ? "{0:f1}" : "{0:0}";
            txtDiff2MinDisp.Text = string.Format(format, CommonUtil.String2Float(((Control)sender).Text) * -1);
		}

		private void txtDiff3MaxDisp_Leave(object sender, EventArgs e)
		{
            string format = comDiff3Unit.SelectedIndex == 0 ? "{0:f1}" : "{0:0}";
            txtDiff3MinDisp.Text = string.Format(format, CommonUtil.String2Float(((Control)sender).Text) * -1);
		}

		private void txtDiff4MaxDisp_Leave(object sender, EventArgs e)
		{
            string format = comDiff4Unit.SelectedIndex == 0 ? "{0:f1}" : "{0:0}";
            txtDiff4MinDisp.Text = string.Format(format, CommonUtil.String2Float(((Control)sender).Text) * -1);
		}

		private void txtFan1DpMax_Leave(object sender, EventArgs e)
		{
			txtFan1DpStep5.Text = ((TextBox)sender).Text;
		}

		private void txtFan2DpMax_Leave(object sender, EventArgs e)
		{
			txtFan2DpStep5.Text = ((TextBox)sender).Text;
		}

		private void txtFan3DpMax_Leave(object sender, EventArgs e)
		{
			txtFan3DpStep5.Text = ((TextBox)sender).Text;
		}

		private void txtFan4DpMax_Leave(object sender, EventArgs e)
		{
			txtFan4DpStep5.Text = ((TextBox)sender).Text;
		}

		private void txtFan1VtMax_Leave(object sender, EventArgs e)
		{
			txtFan1VtStep5.Text = ((TextBox)sender).Text;
		}

		private void txtFan2VtMax_Leave(object sender, EventArgs e)
		{
			txtFan2VtStep5.Text = ((TextBox)sender).Text;
		}

		private void txtFan3VtMax_Leave(object sender, EventArgs e)
		{
			txtFan3VtStep5.Text = ((TextBox)sender).Text;
		}

		private void txtFan4VtMax_Leave(object sender, EventArgs e)
		{
			txtFan4VtStep5.Text = ((TextBox)sender).Text;
		}

		private void comSp2HdrUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
			labSp2HdrUnit.Text = comSp2HdrUnit.Text;
		}

		private void comSp2ValUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
			labSp2ValUnit.Text = comSp2ValUnit.Text;
		}

		private void comSp3HdrUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
			labSp3HdrUnit.Text = comSp3HdrUnit.Text;
		}

		private void comSp3ValUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
			labSp3ValUnit.Text = comSp3ValUnit.Text;
		}

		private void comDiff1Unit_SelectedIndexChanged(object sender, EventArgs e)
		{
			labDiff1Unit.Text = comDiff1Unit.Text;
			rwSensor = rwData.difSensor[0];
			if (comDiff1Unit.SelectedIndex == 0)
			{
				if (rwSensor.hdrMax > 20.0f)
				{
					rwSensor.hdrMax /= 100;
					rwSensor.hdrMin = rwSensor.hdrMax * -1;
				}
				this.txtDiff1MaxDisp.Tag = "P1, 0.0, 20.0";
				txtDiff1MinDisp.Text = String.Format("{0:f1}", rwSensor.hdrMin);
				txtDiff1MaxDisp.Text = String.Format("{0:f1}", rwSensor.hdrMax);
			}
			else
			{
				if (rwSensor.hdrMax < 100)
				{
					rwSensor.hdrMax *= 100;
					rwSensor.hdrMin = rwSensor.hdrMax * -1;
				}
				this.txtDiff1MaxDisp.Tag = "P0, 0, 2000";
				txtDiff1MinDisp.Text = String.Format("{0:0}", rwSensor.hdrMin);
				txtDiff1MaxDisp.Text = String.Format("{0:0}", rwSensor.hdrMax);
			}
		}

		private void comDiff2Unit_SelectedIndexChanged(object sender, EventArgs e)
		{
			labDiff2Unit.Text = comDiff2Unit.Text;
			rwSensor = rwData.difSensor[1];
			if (comDiff2Unit.SelectedIndex == 0)
			{
				if (rwSensor.hdrMax > 20.0f)
				{
					rwSensor.hdrMax /= 100;
					rwSensor.hdrMin = rwSensor.hdrMax * -1;
				}
				this.txtDiff2MaxDisp.Tag = "P1, 0.0, 20.0";
				txtDiff2MinDisp.Text = String.Format("{0:f1}", rwSensor.hdrMin);
				txtDiff2MaxDisp.Text = String.Format("{0:f1}", rwSensor.hdrMax);
			}
			else
			{
				if (rwSensor.hdrMax < 100)
				{
					rwSensor.hdrMax *= 100;
					rwSensor.hdrMin = rwSensor.hdrMax * -1;
				}
				this.txtDiff2MaxDisp.Tag = "P0, 0, 2000";
				txtDiff2MinDisp.Text = String.Format("{0:0}", rwSensor.hdrMin);
				txtDiff2MaxDisp.Text = String.Format("{0:0}", rwSensor.hdrMax);
			}
		}

		private void comDiff3Unit_SelectedIndexChanged(object sender, EventArgs e)
		{
			labDiff3Unit.Text = comDiff3Unit.Text;
			rwSensor = rwData.difSensor[2];
			if (comDiff3Unit.SelectedIndex == 0)
			{
				if (rwSensor.hdrMax > 20.0f)
				{
					rwSensor.hdrMax /= 100;
					rwSensor.hdrMin = rwSensor.hdrMax * -1;
				}
				this.txtDiff3MaxDisp.Tag = "P1, 0.0, 20.0";
				txtDiff3MinDisp.Text = String.Format("{0:f1}", rwSensor.hdrMin);
				txtDiff3MaxDisp.Text = String.Format("{0:f1}", rwSensor.hdrMax);
			}
			else
			{
				if (rwSensor.hdrMax < 100)
				{
					rwSensor.hdrMax *= 100;
					rwSensor.hdrMin = rwSensor.hdrMax * -1;
				}
				this.txtDiff3MaxDisp.Tag = "P0, 0, 2000";
				txtDiff3MinDisp.Text = String.Format("{0:0}", rwSensor.hdrMin);
				txtDiff3MaxDisp.Text = String.Format("{0:0}", rwSensor.hdrMax);
			}
		}

		private void comDiff4Unit_SelectedIndexChanged(object sender, EventArgs e)
		{
			labDiff4Unit.Text = comDiff4Unit.Text;
			rwSensor = rwData.difSensor[3];
			if (comDiff4Unit.SelectedIndex == 0)
			{
				if (rwSensor.hdrMax > 20.0f)
				{
					rwSensor.hdrMax /= 100;
					rwSensor.hdrMin = rwSensor.hdrMax * -1;
				}
				this.txtDiff4MaxDisp.Tag = "P1, 0.0, 20.0";
				txtDiff4MinDisp.Text = String.Format("{0:f1}", rwSensor.hdrMin);
				txtDiff4MaxDisp.Text = String.Format("{0:f1}", rwSensor.hdrMax);
			}
			else
			{
				if (rwSensor.hdrMax < 100)
				{
					rwSensor.hdrMax *= 100;
					rwSensor.hdrMin = rwSensor.hdrMax * -1;
				}
				this.txtDiff4MaxDisp.Tag = "P0, 0, 2000";
				txtDiff4MinDisp.Text = String.Format("{0:0}", rwSensor.hdrMin);
				txtDiff4MaxDisp.Text = String.Format("{0:0}", rwSensor.hdrMax);
			}
		}

	}
}
