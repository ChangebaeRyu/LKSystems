using CADC.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CADC.SubForm
{
    public partial class PasswordForm : Form
    {
        private const String password = "1111";
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            CommonUtil.ShowKeyboard();
            txtPassword.PasswordChar = '*';
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (password.Equals(txtPassword.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("비밀번호가 틀렸습니다. 다시 입력해 주세요\n 임시비번 : 1111","비밀번호 확인", MessageBoxButtons.OK);
                txtPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnConfirm.PerformClick();
            }
        }

        private void PasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 가상키보드 종료
            Process[] psList = Process.GetProcessesByName("osk");
            if (psList.Length > 0)
            {
                psList[0].Kill();
            }
        }
    }
}
