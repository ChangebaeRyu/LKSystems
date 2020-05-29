using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CADC.CustomControl
{
    public partial class ucPanel : UserControl
    {
        public const int UNIT_NONE = 0;
        public const int UNIT_SEC = 1;
        public const int UNIT_TEMP = 2;

        public String PanelTitle { get; set; }
        public String LabelName { get; set; }
        public int LabelUnit { get; set; }
        public int minValue { get; set; }
        public int maxValue { get; set; }

        private int inValue;
        public int panelValue {
            get
            {
                return inValue;
            }
            set
            {
                inValue = value;
                setLabelValue();
            }
        }

        public ucPanel()
        {
            InitializeComponent();
        }

        public ucPanel(String title, String name, int unit = 0, int minValue = 0, int maxValue = 100)
        {
            PanelTitle = title;
            LabelName = name;
            LabelUnit = unit;
            this.minValue = minValue;
            this.maxValue = maxValue;
            InitializeComponent();
        }

        private void ucPanel_Load(object sender, EventArgs e)
        {
            InitData();

            btnTitle.Text = PanelTitle;
            labName.Text = LabelName;
            //panelValue = minValue;
            setLabelValue();
        }

        private void setLabelValue()
        {
            if (LabelUnit == UNIT_SEC)
            {
                labValue.Text = inValue + "초";
            }
            else if (LabelUnit == UNIT_TEMP)
            {
                labValue.Text = inValue + "℃";
            }
            else if(LabelUnit == UNIT_NONE)
            {
                labValue.Text = inValue.ToString();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if(inValue > minValue) inValue--;
            setLabelValue();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (inValue < maxValue) inValue++;
            setLabelValue();
        }

        private void InitData()
        {

        }

    }
}
