using System;
using System.Windows.Forms;
using System.Diagnostics;
using CADC.Utils;
using CADC.Properties;

namespace CADC.SubForm
{
	public partial class DeviceSetForm : Form
    {
        private String[] comlist;

        public DeviceSetForm()
        {
            InitializeComponent();
        }

        private void DeviceSetForm_Load(object sender, EventArgs e)
        {
            InitKeyEvent();

            comlist = System.IO.Ports.SerialPort.GetPortNames();
            cbCom485.Items.AddRange(comlist);
            cbCom232.Items.AddRange(comlist);
            cbCom485.Text = Settings.Default.ComPort485;
            cbCom232.Text = Settings.Default.ComPort232;
			cbBR485.Text = string.Format("{0}", Settings.Default.baudrate485);
			cbBR232.Text = string.Format("{0}", Settings.Default.baudrate232);
			LoadUserSetting();
			SetUIUseDC();

			// 가상키보드 실행
			CommonUtil.ShowKeyboard();
        }

        private void InitKeyEvent()
        {
            txtDCID1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtDCID2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtDCID3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtDCID4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtIp1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtIp2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtIp3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtIp4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtSubnet1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtSubnet2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtSubnet3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtSubnet4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtGateway1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtGateway2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtGateway3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtGateway4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtBsDns1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtBsDns2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtBsDns3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtBsDns4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtSubDns1.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtSubDns2.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtSubDns3.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
            txtSubDns4.KeyPress += new KeyPressEventHandler(digitTextBox_KeyPress);
        }

        private void digitTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 양의 정수
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            this.Close();

			if(isSave)
				this.DialogResult = DialogResult.OK;
		}

		private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadUserSetting();
        }

		private bool isSave = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
			if(CheckValidate() == false)
			{
				MessageBox.Show("집진기 사용은 1번부터 순서대로 사용하실수 있습니다.", "집진기 설정오류");
				return;
			}
			SaveUserSetting();
			isSave = true;
        }

		/// <summary>
		/// 입력 적합성 검사
		/// </summary>
		/// <returns></returns>
		private bool CheckValidate()
		{
			// 집진기 설정시 1,3 설정이나 2, 3 설정등 중간이 빠지게 설정되는 것을 막는다.
			if(chkUseDC1.Checked == false)
			{
				if(chkUseDC2.Checked || chkUseDC3.Checked || chkUseDC4.Checked)
				{
					return false;
				}
			}
			if (chkUseDC2.Checked == false)
			{
				if (chkUseDC3.Checked || chkUseDC4.Checked)
				{
					return false;
				}
			}
			if (chkUseDC3.Checked == false)
			{
				if (chkUseDC4.Checked)
				{
					return false;
				}
			}

			return true;
		}

		private void chkSingleDC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDC1Single.Checked)
                chkDC1Multi.Checked = false;
            if (chkDC2Single.Checked)
                chkDC2Multi.Checked = false;
            if (chkDC3Single.Checked)
                chkDC3Multi.Checked = false;
            if (chkDC4Single.Checked)
                chkDC4Multi.Checked = false;
        }

        private void chkMultiDC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDC1Multi.Checked)
                chkDC1Single.Checked = false;
            if (chkDC2Multi.Checked)
                chkDC2Single.Checked = false;
            if (chkDC3Multi.Checked)
                chkDC3Single.Checked = false;
            if (chkDC4Multi.Checked)
                chkDC4Single.Checked = false;
        }

        private void LoadUserSetting()
        {
            // System Interface(Properties에 저장)
            cbCom232.Text = Settings.Default.ComPort232;
            cbCom485.Text = Settings.Default.ComPort485;
			// 집진기 Value
			chkUseDC1.Checked = Settings.Default.UseDC1;
            chkDC1Single.Checked = Settings.Default.DCType1 == 1 ? true : false;
            chkDC1Multi.Checked = Settings.Default.DCType1 == 2 ? true : false;
            txtDCID1.Text = Settings.Default.DCID1.ToString();

			chkUseDC2.Checked = Settings.Default.UseDC2;
			chkDC2Single.Checked = Settings.Default.DCType2 == 1 ? true : false;
            chkDC2Multi.Checked = Settings.Default.DCType2 == 2 ? true : false;
            txtDCID2.Text = Settings.Default.DCID2.ToString();

			chkUseDC3.Checked = Settings.Default.UseDC3;
			chkDC3Single.Checked = Settings.Default.DCType3 == 1 ? true : false;
            chkDC3Multi.Checked = Settings.Default.DCType3 == 2 ? true : false;
            txtDCID3.Text = Settings.Default.DCID3.ToString();

			chkUseDC4.Checked = Settings.Default.UseDC4;
			chkDC4Single.Checked = Settings.Default.DCType4 == 1 ? true : false;
            chkDC4Multi.Checked = Settings.Default.DCType4 == 2 ? true : false;
            txtDCID4.Text = Settings.Default.DCID4.ToString();

            // System Interface
            //cbCom485.Text = appData.comPort485;
            //cbCom232.Text = appData.comPort232;

            // IP Setting
            if (Settings.Default.IPType == 1) chkAuto.Checked = true;
            else if (Settings.Default.IPType == 2) chkManual.Checked = true;

            if(CommonUtil.IsNull(Settings.Default.IPAddress))
            {
                txtIp1.Text = txtIp2.Text = txtIp3.Text = txtIp4.Text = "";
            }
            else
            {
                String[] addrs = Settings.Default.IPAddress.Split('.');
                if(addrs.Length < 4)
                    txtIp1.Text = txtIp2.Text = txtIp3.Text = txtIp4.Text = "";
                txtIp1.Text = addrs[0];
                txtIp2.Text = addrs[1];
                txtIp3.Text = addrs[2];
                txtIp4.Text = addrs[3];
            }

            if (CommonUtil.IsNull(Settings.Default.Subnet))
            {
                txtSubnet1.Text = txtSubnet2.Text = txtSubnet3.Text = txtSubnet4.Text = "";
            }
            else
            {
                String[] addrs = Settings.Default.Subnet.Split('.');
                if (addrs.Length < 4)
                    txtSubnet1.Text = txtSubnet2.Text = txtSubnet3.Text = txtSubnet4.Text = "";
                txtSubnet1.Text = addrs[0];
                txtSubnet2.Text = addrs[1];
                txtSubnet3.Text = addrs[2];
                txtSubnet4.Text = addrs[3];
            }

            if (CommonUtil.IsNull(Settings.Default.Gateway))
            {
                txtGateway1.Text = txtGateway2.Text = txtGateway3.Text = txtGateway4.Text = "";
            }
            else
            {
                String[] addrs = Settings.Default.Gateway.Split('.');
                if (addrs.Length < 4)
                    txtGateway1.Text = txtGateway2.Text = txtGateway3.Text = txtGateway4.Text = "";
                txtGateway1.Text = addrs[0];
                txtGateway2.Text = addrs[1];
                txtGateway3.Text = addrs[2];
                txtGateway4.Text = addrs[3];
            }

            if (CommonUtil.IsNull(Settings.Default.BsDNS))
            {
                txtBsDns1.Text = txtBsDns2.Text = txtBsDns3.Text = txtBsDns4.Text = "";
            }
            else
            {
                String[] addrs = Settings.Default.BsDNS.Split('.');
                if (addrs.Length < 4)
                    txtBsDns1.Text = txtBsDns2.Text = txtBsDns3.Text = txtBsDns4.Text = "";
                txtBsDns1.Text = addrs[0];
                txtBsDns2.Text = addrs[1];
                txtBsDns3.Text = addrs[2];
                txtBsDns4.Text = addrs[3];
            }

            if (CommonUtil.IsNull(Settings.Default.SubDNS))
            {
                txtSubDns1.Text = txtSubDns2.Text = txtSubDns3.Text = txtSubDns4.Text = "";
            }
            else
            {
                String[] addrs = Settings.Default.SubDNS.Split('.');
                if (addrs.Length < 4)
                    txtSubDns1.Text = txtSubDns2.Text = txtSubDns3.Text = txtSubDns4.Text = "";
                txtSubDns1.Text = addrs[0];
                txtSubDns2.Text = addrs[1];
                txtSubDns3.Text = addrs[2];
                txtSubDns4.Text = addrs[3];
            }

            // VPN 설정
            txtNetworkName.Text = Settings.Default.VpnNetName;
            switch(Settings.Default.VpnType)
            {
                case 1:
                    chkVpnType1.Checked = true;
                    break;
                case 2:
                    chkVpnType2.Checked = true;
                    break;
                case 3:
                    chkVpnType3.Checked = true;
                    break;
                case 4:
                    chkVpnType4.Checked = true;
                    break;
                case 5:
                    chkVpnType5.Checked = true;
                    break;
                case 6:
                    chkVpnType6.Checked = true;
                    break;
                case 7:
                    chkVpnType7.Checked = true;
                    break;
            }
            chkMPPE.Checked = Settings.Default.VpnMppe;
            if (Settings.Default.VpnPloxy == 1) chkVpnProxyNone.Checked = true;
            else if (Settings.Default.VpnPloxy == 2) chkVpnProxyManual.Checked = true;
            txtVpnServerAddreaa.Text = Settings.Default.VpnSvrAddr;
            txtVpnDnsSearchDomain.Text = Settings.Default.VpnDnsSchDomain;
            txtVpnDnsServer.Text = Settings.Default.VpnDnsServer;
            txtRelayPath.Text = Settings.Default.VpnRelayPath;
            txtUserName.Text = Settings.Default.VpnUserName;
            txtPassword.Text = Settings.Default.VpnPassword;
            txtLogFolder.Text = Settings.Default.VpnLogFolder;
            txtLogFileName.Text = Settings.Default.VpnLogFileName;
            txtLogMaxSize.Text = Settings.Default.VpnLogMaxSize.ToString();
        }

        private void SaveUserSetting()
        {
            Settings.Default.IsFirstInit = true;
            Settings.Default.ComPort232 = cbCom232.Text;
            Settings.Default.ComPort485 = cbCom485.Text;
			Settings.Default.baudrate485 = CommonUtil.String2Int(cbBR485.Text);
			Settings.Default.baudrate232 = CommonUtil.String2Int(cbBR232.Text);
			// Save 집진기 Value
			Settings.Default.UseDC1 = chkUseDC1.Checked;
			Settings.Default.UseDC2 = chkUseDC2.Checked;
			Settings.Default.UseDC3 = chkUseDC3.Checked;
			Settings.Default.UseDC4 = chkUseDC4.Checked;
			if (chkUseDC1.Checked)
			{
				if (chkDC1Single.Checked) Settings.Default.DCType1 = 1;
				else if (chkDC1Multi.Checked) Settings.Default.DCType1 = 2;
				else Settings.Default.DCType1 = 0;
				Settings.Default.DCID1 = CommonUtil.String2Int(txtDCID1.Text);
			}
			else
			{
				Settings.Default.DCType1 = 0;
				Settings.Default.DCID1 = 0;
			}

			if (chkUseDC2.Checked)
			{
				if (chkDC2Single.Checked) Settings.Default.DCType2 = 1;
				else if (chkDC2Multi.Checked) Settings.Default.DCType2 = 2;
				else Settings.Default.DCType2 = 0;
				Settings.Default.DCID2 = CommonUtil.String2Int(txtDCID2.Text);
			}
			else
			{
				Settings.Default.DCType2 = 0;
				Settings.Default.DCID2 = 0;
			}

			if (chkUseDC3.Checked)
			{
				if (chkDC3Single.Checked) Settings.Default.DCType3 = 1;
				else if (chkDC3Multi.Checked) Settings.Default.DCType3 = 2;
				else Settings.Default.DCType3 = 0;
				Settings.Default.DCID3 = CommonUtil.String2Int(txtDCID3.Text);
			}
			else
			{
				Settings.Default.DCType3 = 0;
				Settings.Default.DCID3 = 0;
			}

			if (chkUseDC4.Checked)
			{
				if (chkDC4Single.Checked) Settings.Default.DCType4 = 1;
				else if (chkDC4Multi.Checked) Settings.Default.DCType4 = 2;
				else Settings.Default.DCType4 = 0;
				Settings.Default.DCID4 = CommonUtil.String2Int(txtDCID4.Text);
			}
			else
			{
				Settings.Default.DCType4 = 0;
				Settings.Default.DCID4 = 0;
			}


			// System Interface
			//appData.comPort485 = cbCom485.Text;
			//appData.comPort232 = cbCom232.Text;

			// IP Setting
			if (chkAuto.Checked) Settings.Default.IPType = 1;
            else if (chkManual.Checked) Settings.Default.IPType = 2;

            Settings.Default.IPAddress = CommonUtil.String2Int(txtIp1.Text)
                + "." + CommonUtil.String2Int(txtIp2.Text)
                + "." + CommonUtil.String2Int(txtIp3.Text)
                + "." + CommonUtil.String2Int(txtIp4.Text);
            Settings.Default.Subnet = CommonUtil.String2Int(txtSubnet1.Text)
                + "." + CommonUtil.String2Int(txtSubnet2.Text)
                + "." + CommonUtil.String2Int(txtSubnet3.Text)
                + "." + CommonUtil.String2Int(txtSubnet4.Text);
            Settings.Default.Gateway = CommonUtil.String2Int(txtGateway1.Text)
                + "." + CommonUtil.String2Int(txtGateway2.Text)
                + "." + CommonUtil.String2Int(txtGateway3.Text)
                + "." + CommonUtil.String2Int(txtGateway4.Text);
            Settings.Default.BsDNS = CommonUtil.String2Int(txtBsDns1.Text)
                + "." + CommonUtil.String2Int(txtBsDns2.Text)
                + "." + CommonUtil.String2Int(txtBsDns3.Text)
                + "." + CommonUtil.String2Int(txtBsDns4.Text);
            Settings.Default.SubDNS = CommonUtil.String2Int(txtSubDns1.Text)
                + "." + CommonUtil.String2Int(txtSubDns2.Text)
                + "." + CommonUtil.String2Int(txtSubDns3.Text)
                + "." + CommonUtil.String2Int(txtSubDns4.Text);

            // VPN 설정
            Settings.Default.VpnNetName = txtNetworkName.Text;
            if (chkVpnType1.Checked) Settings.Default.VpnType = 1;
            else if (chkVpnType2.Checked) Settings.Default.VpnType = 2;
            else if (chkVpnType3.Checked) Settings.Default.VpnType = 3;
            else if (chkVpnType4.Checked) Settings.Default.VpnType = 4;
            else if (chkVpnType5.Checked) Settings.Default.VpnType = 5;
            else if (chkVpnType6.Checked) Settings.Default.VpnType = 6;
            else if (chkVpnType7.Checked) Settings.Default.VpnType = 7;
            Settings.Default.VpnMppe = chkMPPE.Checked;
            if (chkVpnProxyNone.Checked) Settings.Default.VpnPloxy = 1;
            else if (chkVpnProxyManual.Checked) Settings.Default.VpnPloxy = 2;
            Settings.Default.VpnSvrAddr = txtVpnServerAddreaa.Text;
            Settings.Default.VpnDnsSchDomain = txtVpnDnsSearchDomain.Text;
            Settings.Default.VpnDnsServer = txtVpnDnsServer.Text;
            Settings.Default.VpnRelayPath = txtRelayPath.Text;
            Settings.Default.VpnUserName = txtUserName.Text;
            Settings.Default.VpnPassword = txtPassword.Text;
            Settings.Default.VpnLogFolder = txtLogFolder.Text;
            Settings.Default.VpnLogFileName = txtLogFileName.Text;
            Settings.Default.VpnLogMaxSize = CommonUtil.String2Int(txtLogMaxSize.Text);

            Settings.Default.Save();
        }

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAuto.Checked)
            {
                chkManual.Checked = false;
                txtIp1.Enabled = false;
                txtIp2.Enabled = false;
                txtIp3.Enabled = false;
                txtIp4.Enabled = false;
                txtSubnet1.Enabled = false;
                txtSubnet2.Enabled = false;
                txtSubnet3.Enabled = false;
                txtSubnet4.Enabled = false;
                txtGateway1.Enabled = false;
                txtGateway2.Enabled = false;
                txtGateway3.Enabled = false;
                txtGateway4.Enabled = false;
                txtBsDns1.Enabled = false;
                txtBsDns2.Enabled = false;
                txtBsDns3.Enabled = false;
                txtBsDns4.Enabled = false;
                txtSubDns1.Enabled = false;
                txtSubDns2.Enabled = false;
                txtSubDns3.Enabled = false;
                txtSubDns4.Enabled = false;
            }
        }

        private void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManual.Checked)
            {
                chkAuto.Checked = false;
                txtIp1.Enabled = true;
                txtIp2.Enabled = true;
                txtIp3.Enabled = true;
                txtIp4.Enabled = true;
                txtSubnet1.Enabled = true;
                txtSubnet2.Enabled = true;
                txtSubnet3.Enabled = true;
                txtSubnet4.Enabled = true;
                txtGateway1.Enabled = true;
                txtGateway2.Enabled = true;
                txtGateway3.Enabled = true;
                txtGateway4.Enabled = true;
                txtBsDns1.Enabled = true;
                txtBsDns2.Enabled = true;
                txtBsDns3.Enabled = true;
                txtBsDns4.Enabled = true;
                txtSubDns1.Enabled = true;
                txtSubDns2.Enabled = true;
                txtSubDns3.Enabled = true;
                txtSubDns4.Enabled = true;
            }
        }

        private void chkNetType1_CheckedChanged(object sender, EventArgs e)
        {
            if(chkVpnType1.Checked)
            {
                chkVpnType2.Checked = false;
                chkVpnType3.Checked = false;
                chkVpnType4.Checked = false;
                chkVpnType5.Checked = false;
                chkVpnType6.Checked = false;
                chkVpnType7.Checked = false;
            }
        }

        private void chkNetType2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVpnType2.Checked)
            {
                chkVpnType1.Checked = false;
                chkVpnType3.Checked = false;
                chkVpnType4.Checked = false;
                chkVpnType5.Checked = false;
                chkVpnType6.Checked = false;
                chkVpnType7.Checked = false;
            }
        }

        private void chkNetType3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVpnType3.Checked)
            {
                chkVpnType1.Checked = false;
                chkVpnType2.Checked = false;
                chkVpnType4.Checked = false;
                chkVpnType5.Checked = false;
                chkVpnType6.Checked = false;
                chkVpnType7.Checked = false;
            }
        }

        private void chkNetType4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVpnType4.Checked)
            {
                chkVpnType1.Checked = false;
                chkVpnType2.Checked = false;
                chkVpnType3.Checked = false;
                chkVpnType5.Checked = false;
                chkVpnType6.Checked = false;
                chkVpnType7.Checked = false;
            }

        }

        private void chkNetType5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVpnType5.Checked)
            {
                chkVpnType1.Checked = false;
                chkVpnType2.Checked = false;
                chkVpnType3.Checked = false;
                chkVpnType4.Checked = false;
                chkVpnType6.Checked = false;
                chkVpnType7.Checked = false;
            }

        }

        private void chkNetType6_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVpnType6.Checked)
            {
                chkVpnType1.Checked = false;
                chkVpnType2.Checked = false;
                chkVpnType3.Checked = false;
                chkVpnType4.Checked = false;
                chkVpnType5.Checked = false;
                chkVpnType7.Checked = false;
            }

        }

        private void chkNetType7_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVpnType7.Checked)
            {
                chkVpnType1.Checked = false;
                chkVpnType2.Checked = false;
                chkVpnType3.Checked = false;
                chkVpnType4.Checked = false;
                chkVpnType5.Checked = false;
                chkVpnType6.Checked = false;
            }

        }

        private void chkMPPE_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkProxyNone_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVpnProxyNone.Checked) chkVpnProxyManual.Checked = false;
            else chkVpnProxyManual.Checked = true;
        }

        private void chkProxyManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVpnProxyManual.Checked) chkVpnProxyNone.Checked = false;
            else chkVpnProxyNone.Checked = true;
        }

        private void DeviceSetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 가상키보드 종료
            Process[] psList = Process.GetProcessesByName("osk");
            if (psList.Length > 0)
            {
                psList[0].Kill();
            }
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            // 가상키보드 실행
            CommonUtil.ShowKeyboard();
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txtLogFolder.Text = dialog.SelectedPath;    //선택한 다이얼로그 경로 저장
        }

		private void chkUseDC_CheckedChanged(object sender, EventArgs e)
		{
			SetUIUseDC();
		}

		/// <summary>
		/// Settings.Default.UseDC 번호에 따라 집진기 사용여부를 설정한다
		/// </summary>
		private void SetUIUseDC()
		{
			// 집진기 1 사용여부
			if (chkUseDC1.Checked == false)
			{
				chkDC1Multi.Enabled = false;
				chkDC1Single.Enabled = false;
				txtDCID1.Enabled = false;
			}
			else
			{
				chkDC1Multi.Enabled = true;
				chkDC1Single.Enabled = true;
				txtDCID1.Enabled = true;
			}
			// 집진기 2 사용여부
			if (chkUseDC2.Checked == false)
			{
				chkDC2Multi.Enabled = false;
				chkDC2Single.Enabled = false;
				txtDCID2.Enabled = false;
			}
			else
			{
				chkDC2Multi.Enabled = true;
				chkDC2Single.Enabled = true;
				txtDCID2.Enabled = true;
			}
			// 집진기 3 사용여부
			if (chkUseDC3.Checked == false)
			{
				chkDC3Multi.Enabled = false;
				chkDC3Single.Enabled = false;
				txtDCID3.Enabled = false;
			}
			else
			{
				chkDC3Multi.Enabled = true;
				chkDC3Single.Enabled = true;
				txtDCID3.Enabled = true;
			}
			// 집진기 4 사용여부
			if (chkUseDC4.Checked == false)
			{
				chkDC4Multi.Enabled = false;
				chkDC4Single.Enabled = false;
				txtDCID4.Enabled = false;
			}
			else
			{
				chkDC4Multi.Enabled = true;
				chkDC4Single.Enabled = true;
				txtDCID4.Enabled = false;
			}
		}

	}
}
