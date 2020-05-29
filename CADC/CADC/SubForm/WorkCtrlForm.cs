using CADC.Registers;
using CADC.CustomControl;
using CADC.Modbus;
using CADC.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CADC.SubForm
{
    public partial class WorkCtrlForm : Form
    {
        private RWData rwData;

        private int DCIndex;
        private ucPanel ucPnlCom1;
        private ucPanel ucPnlCom2;
        private ucPanel ucPnlTemp1;
        private ucPanel ucPnlTemp2;
        private ucPanel ucPnlIntervalLight;
        private ucPanel ucPnlIntervalHeater;
        private ucPanel ucPnlAlarmTemp;

        private ModbusRTUMaster m_rtu;
        public WorkCtrlForm(ModbusRTUMaster modbusRTUMaster, int selDCNo)
        {
            this.DCIndex = selDCNo - 1;
            InitializeComponent();
        }

        private void WorkingControlForm_Load(object sender, EventArgs e)
        {
            InitModbus();
            rwData = AppData.Instence.rwData[DCIndex];

            labDCNO.Text = (DCIndex + 1).ToString();

            ucPnlCom1 = new ucPanel("485 ID", "ID(4~10)", ucPanel.UNIT_NONE, 4, 10);
            ucPnlCom2 = new ucPanel("485 ID", "ID(4~10)", ucPanel.UNIT_NONE, 4, 10);
            ucPnlTemp1 = new ucPanel("온도제어", "희망온도", ucPanel.UNIT_TEMP, -20, 20);
            ucPnlTemp2 = new ucPanel("온도제어", "희망온도", ucPanel.UNIT_TEMP, -20, 20);
            ucPnlIntervalLight = new ucPanel("LIGHT 간격", "지연시간", ucPanel.UNIT_SEC);
            ucPnlIntervalHeater = new ucPanel("히터간격 간격", "지연시간", ucPanel.UNIT_SEC);
            ucPnlAlarmTemp = new ucPanel("TEMP 알람", "1 / 2 차이", ucPanel.UNIT_TEMP);

            pnlTop1.Controls.Add(ucPnlCom1);
            pnlTop2.Controls.Add(ucPnlTemp1);
            pnlTop3.Controls.Add(ucPnlCom2);
            pnlTop4.Controls.Add(ucPnlTemp2);
            pnlBottom1.Controls.Add(ucPnlIntervalLight);
            pnlBottom2.Controls.Add(ucPnlIntervalHeater);
            pnlBottom3.Controls.Add(ucPnlAlarmTemp);

            pnlTop1.Visible = false;
            pnlTop3.Visible = false;
            pnlBottom1.Visible = false;
            pnlBottom2.Visible = false;
            pnlBottom3.Visible = false;

            ReflashAppdata();
            //reflashTimer.Start();
        }

        // Event 등록 등 Modbus 설정
        private void InitModbus()
        {

        }

        private void WorkingControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAppData();
            //reflashTimer.Stop();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            // 설정화면을 들어갈때만 Password 폼을 띄운다
            if (!pnlTop1.Visible)
            {
                PasswordForm passwordForm = new PasswordForm();
                DialogResult result = passwordForm.ShowDialog();
                if (result != DialogResult.OK) return;
            }

            if(!pnlTop1.Visible)
            {
                pnlTop1.Visible = true;
                pnlTop3.Visible = true;
                pnlBottom1.Visible = true;
                pnlBottom2.Visible = true;
                pnlBottom3.Visible = true;
            }
            else
            {
                pnlTop1.Visible = false;
                pnlTop3.Visible = false;
                pnlBottom1.Visible = false;
                pnlBottom2.Visible = false;
                pnlBottom3.Visible = false;
            }
        }

        private void ReflashAppdata()
        {
            ucPnlCom1.panelValue = rwData.comID1;
            ucPnlCom2.panelValue = rwData.comID2;
            ucPnlTemp1.panelValue = rwData.setTemp1;
            ucPnlTemp2.panelValue = rwData.setTemp2;
            ucPnlIntervalLight.panelValue = rwData.intervalLight;
            ucPnlIntervalHeater.panelValue = rwData.intervalHeater;
            ucPnlAlarmTemp.panelValue = rwData.alarmTemp;
            labSenserTemp1.Text = rwData.senserTemp1.ToString() + "℃";
            labSenserTemp2.Text = rwData.senserTemp2.ToString() + "℃";
            labSenserTemp3.Text = rwData.senserTemp3.ToString() + "℃";
        }

        private void SaveAppData()
        {
            rwData.comID1 = ucPnlCom1.panelValue;
            rwData.comID2 = ucPnlCom2.panelValue;
            rwData.setTemp1 = ucPnlTemp1.panelValue;
            rwData.setTemp2 = ucPnlTemp2.panelValue;
            rwData.intervalLight = ucPnlIntervalLight.panelValue;
            rwData.intervalHeater = ucPnlIntervalHeater.panelValue;
            rwData.alarmTemp = ucPnlAlarmTemp.panelValue;
        }

        private int testTemp = -20;
        private int testCnt = 0;
        private void reflashTimer_Tick(object sender, EventArgs e)
        {
            if(testCnt++ % 20 == 0)
            {
                testTemp++;
                rwData.senserTemp1 = testTemp;
                rwData.senserTemp2 = testTemp;
                rwData.senserTemp3 = testTemp;
                ReflashAppdata();
            }
        }
    }
}
