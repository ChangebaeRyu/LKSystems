using CADC.Properties;
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
    public partial class PowerOffForm : Form
    {
        private const String password = "1111";
        public PowerOffForm()
        {
            InitializeComponent();
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnConfirm_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnConfirm.BackgroundImage = Resources.shut_on;
        }

        private void btnConfirm_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnConfirm.BackgroundImage = Resources.shut_off;
            DialogResult = DialogResult.OK;
        }
    }
}
