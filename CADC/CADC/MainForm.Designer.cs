using System;
using System.Runtime.InteropServices;

namespace CADC
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnState = new System.Windows.Forms.Button();
            this.labPageNo = new System.Windows.Forms.Label();
            this.alarmTimer = new System.Windows.Forms.Timer(this.components);
            this.AirWindTimer = new System.Windows.Forms.Timer(this.components);
            this.labVersion = new System.Windows.Forms.Label();
            this.comAirTime = new System.Windows.Forms.ComboBox();
            this.pnlAirTimer = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labRemTime = new System.Windows.Forms.Label();
            this.labCon = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.picPage1 = new System.Windows.Forms.PictureBox();
            this.picPage2 = new System.Windows.Forms.PictureBox();
            this.picPage3 = new System.Windows.Forms.PictureBox();
            this.picPage4 = new System.Windows.Forms.PictureBox();
            this.btnDeviceSet = new System.Windows.Forms.Button();
            this.pnlSp3 = new System.Windows.Forms.Panel();
            this.labSp3Name = new System.Windows.Forms.Label();
            this.labSp3Val = new System.Windows.Forms.Label();
            this.labSp3Unit = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.labSpare = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlSp2 = new System.Windows.Forms.Panel();
            this.labSp2Name = new System.Windows.Forms.Label();
            this.labSp2Val = new System.Windows.Forms.Label();
            this.labSp2Unit = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labPH = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.labCurr = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.labVolt = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPower = new System.Windows.Forms.Button();
            this.btnControl = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnFW = new System.Windows.Forms.Button();
            this.btnMotor = new System.Windows.Forms.Button();
            this.btnAir = new System.Windows.Forms.Button();
            this.btnAutoMnl = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnRed = new System.Windows.Forms.Button();
            this.btnGrn = new System.Windows.Forms.Button();
            this.btnEmg = new System.Windows.Forms.Button();
            this.pnlMulti = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.labMulDmpIn = new System.Windows.Forms.Label();
            this.labMulDmpOut = new System.Windows.Forms.Label();
            this.picMulFan4 = new System.Windows.Forms.PictureBox();
            this.picMulFan3 = new System.Windows.Forms.PictureBox();
            this.picMulFan2 = new System.Windows.Forms.PictureBox();
            this.picMulWind = new System.Windows.Forms.PictureBox();
            this.labMulDoor = new System.Windows.Forms.Label();
            this.picMulDoor = new System.Windows.Forms.PictureBox();
            this.picMulValve1 = new System.Windows.Forms.PictureBox();
            this.picMulValve2 = new System.Windows.Forms.PictureBox();
            this.picMulValve3 = new System.Windows.Forms.PictureBox();
            this.picMulMotor = new System.Windows.Forms.PictureBox();
            this.picMulFan1 = new System.Windows.Forms.PictureBox();
            this.picMulDmpOut = new System.Windows.Forms.PictureBox();
            this.labMulDustMot = new System.Windows.Forms.Label();
            this.labMulAirTot = new System.Windows.Forms.Label();
            this.labMulSolValve = new System.Windows.Forms.Label();
            this.labMulDiff4 = new System.Windows.Forms.Label();
            this.labMulDiff3 = new System.Windows.Forms.Label();
            this.labMulDiff2 = new System.Windows.Forms.Label();
            this.labMulDiff1 = new System.Windows.Forms.Label();
            this.labMulCurr4 = new System.Windows.Forms.Label();
            this.labMulCurr3 = new System.Windows.Forms.Label();
            this.labMulCurr2 = new System.Windows.Forms.Label();
            this.labMulCurr1 = new System.Windows.Forms.Label();
            this.labMulTemp = new System.Windows.Forms.Label();
            this.picMulAirFlow = new System.Windows.Forms.PictureBox();
            this.pnlFanVolt = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.labFanVolt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labYD = new System.Windows.Forms.Label();
            this.pnlSingle = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.labSngDmpIn = new System.Windows.Forms.Label();
            this.picSngWind = new System.Windows.Forms.PictureBox();
            this.picSngDoor = new System.Windows.Forms.PictureBox();
            this.picSngValve3 = new System.Windows.Forms.PictureBox();
            this.picSngValve2 = new System.Windows.Forms.PictureBox();
            this.picSngValve1 = new System.Windows.Forms.PictureBox();
            this.picSngMotor = new System.Windows.Forms.PictureBox();
            this.picSngFan1 = new System.Windows.Forms.PictureBox();
            this.picSngDmpOut = new System.Windows.Forms.PictureBox();
            this.labSngAirTot = new System.Windows.Forms.Label();
            this.labSngDustMot = new System.Windows.Forms.Label();
            this.labSngSolValve = new System.Windows.Forms.Label();
            this.labSngDoor = new System.Windows.Forms.Label();
            this.labSngDiff = new System.Windows.Forms.Label();
            this.labSngCurr = new System.Windows.Forms.Label();
            this.labSngDmpOut = new System.Windows.Forms.Label();
            this.labSngTemp = new System.Windows.Forms.Label();
            this.picSngAirFlow = new System.Windows.Forms.PictureBox();
            this.outDamTimer = new System.Windows.Forms.Timer(this.components);
            this.btnAlmReset = new System.Windows.Forms.Button();
            this.labFWVer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlAirTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPage3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPage4)).BeginInit();
            this.pnlSp3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlSp2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMulti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMulFan4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulFan3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulFan2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulWind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulDoor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulValve1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulValve2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulValve3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulMotor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulFan1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulDmpOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulAirFlow)).BeginInit();
            this.pnlFanVolt.SuspendLayout();
            this.pnlSingle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSngWind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngDoor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngValve3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngValve2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngValve1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngMotor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngFan1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngDmpOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngAirFlow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnState
            // 
            this.btnState.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnState.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnState.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnState.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnState.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnState.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnState.Location = new System.Drawing.Point(554, 12);
            this.btnState.Name = "btnState";
            this.btnState.Size = new System.Drawing.Size(147, 47);
            this.btnState.TabIndex = 12;
            this.btnState.TabStop = false;
            this.btnState.Text = "1 CLEAN";
            this.btnState.UseVisualStyleBackColor = false;
            // 
            // labPageNo
            // 
            this.labPageNo.AutoSize = true;
            this.labPageNo.BackColor = System.Drawing.Color.Transparent;
            this.labPageNo.ForeColor = System.Drawing.Color.Red;
            this.labPageNo.Location = new System.Drawing.Point(1159, 375);
            this.labPageNo.Name = "labPageNo";
            this.labPageNo.Size = new System.Drawing.Size(44, 12);
            this.labPageNo.TabIndex = 25;
            this.labPageNo.Text = "2 Page";
            // 
            // alarmTimer
            // 
            this.alarmTimer.Interval = 1000;
            this.alarmTimer.Tick += new System.EventHandler(this.alarmTimer_Tick);
            // 
            // AirWindTimer
            // 
            this.AirWindTimer.Interval = 500;
            this.AirWindTimer.Tick += new System.EventHandler(this.AirWindTimer_Tick);
            // 
            // labVersion
            // 
            this.labVersion.AutoSize = true;
            this.labVersion.Location = new System.Drawing.Point(1063, 779);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(46, 12);
            this.labVersion.TabIndex = 35;
            this.labVersion.Text = "version";
            // 
            // comAirTime
            // 
            this.comAirTime.BackColor = System.Drawing.Color.White;
            this.comAirTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comAirTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comAirTime.FormattingEnabled = true;
            this.comAirTime.Items.AddRange(new object[] {
            "OFF",
            "5분",
            "10분",
            "15분",
            "20분",
            "30분",
            "60분"});
            this.comAirTime.Location = new System.Drawing.Point(15, 30);
            this.comAirTime.Name = "comAirTime";
            this.comAirTime.Size = new System.Drawing.Size(78, 20);
            this.comAirTime.TabIndex = 36;
            this.comAirTime.TabStop = false;
            this.comAirTime.SelectedIndexChanged += new System.EventHandler(this.comAirTime_SelectedIndexChanged);
            // 
            // pnlAirTimer
            // 
            this.pnlAirTimer.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAirTimer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAirTimer.Controls.Add(this.label1);
            this.pnlAirTimer.Controls.Add(this.labRemTime);
            this.pnlAirTimer.Controls.Add(this.comAirTime);
            this.pnlAirTimer.Location = new System.Drawing.Point(103, 414);
            this.pnlAirTimer.Name = "pnlAirTimer";
            this.pnlAirTimer.Size = new System.Drawing.Size(108, 85);
            this.pnlAirTimer.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 18);
            this.label1.TabIndex = 37;
            this.label1.Text = "AIR 가동예약";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labRemTime
            // 
            this.labRemTime.Location = new System.Drawing.Point(18, 58);
            this.labRemTime.Name = "labRemTime";
            this.labRemTime.Size = new System.Drawing.Size(72, 18);
            this.labRemTime.TabIndex = 37;
            this.labRemTime.Text = "00:00:00";
            this.labRemTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCon
            // 
            this.labCon.AutoSize = true;
            this.labCon.Location = new System.Drawing.Point(953, 779);
            this.labCon.Name = "labCon";
            this.labCon.Size = new System.Drawing.Size(82, 12);
            this.labCon.TabIndex = 38;
            this.labCon.Text = "Disconnected";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(886, 779);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 12);
            this.label7.TabIndex = 38;
            this.label7.Text = "통신상태 :";
            // 
            // picPage1
            // 
            this.picPage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPage1.BackgroundImage")));
            this.picPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPage1.InitialImage = ((System.Drawing.Image)(resources.GetObject("picPage1.InitialImage")));
            this.picPage1.Location = new System.Drawing.Point(585, 726);
            this.picPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picPage1.Name = "picPage1";
            this.picPage1.Size = new System.Drawing.Size(17, 14);
            this.picPage1.TabIndex = 34;
            this.picPage1.TabStop = false;
            this.picPage1.Click += new System.EventHandler(this.picPage1_Click);
            // 
            // picPage2
            // 
            this.picPage2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPage2.BackgroundImage")));
            this.picPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPage2.InitialImage = ((System.Drawing.Image)(resources.GetObject("picPage2.InitialImage")));
            this.picPage2.Location = new System.Drawing.Point(608, 726);
            this.picPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picPage2.Name = "picPage2";
            this.picPage2.Size = new System.Drawing.Size(17, 14);
            this.picPage2.TabIndex = 31;
            this.picPage2.TabStop = false;
            this.picPage2.Click += new System.EventHandler(this.picPage2_Click);
            // 
            // picPage3
            // 
            this.picPage3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPage3.BackgroundImage")));
            this.picPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPage3.InitialImage = ((System.Drawing.Image)(resources.GetObject("picPage3.InitialImage")));
            this.picPage3.Location = new System.Drawing.Point(631, 726);
            this.picPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picPage3.Name = "picPage3";
            this.picPage3.Size = new System.Drawing.Size(17, 14);
            this.picPage3.TabIndex = 32;
            this.picPage3.TabStop = false;
            this.picPage3.Click += new System.EventHandler(this.picPage3_Click);
            // 
            // picPage4
            // 
            this.picPage4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPage4.BackgroundImage")));
            this.picPage4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picPage4.InitialImage = ((System.Drawing.Image)(resources.GetObject("picPage4.InitialImage")));
            this.picPage4.Location = new System.Drawing.Point(654, 726);
            this.picPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picPage4.Name = "picPage4";
            this.picPage4.Size = new System.Drawing.Size(17, 14);
            this.picPage4.TabIndex = 33;
            this.picPage4.TabStop = false;
            this.picPage4.Click += new System.EventHandler(this.picPage4_Click);
            // 
            // btnDeviceSet
            // 
            this.btnDeviceSet.BackColor = System.Drawing.Color.Transparent;
            this.btnDeviceSet.BackgroundImage = global::CADC.Properties.Resources.lot_off;
            this.btnDeviceSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeviceSet.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnDeviceSet.FlatAppearance.BorderSize = 0;
            this.btnDeviceSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeviceSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeviceSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeviceSet.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDeviceSet.ForeColor = System.Drawing.Color.White;
            this.btnDeviceSet.Location = new System.Drawing.Point(1167, 563);
            this.btnDeviceSet.Name = "btnDeviceSet";
            this.btnDeviceSet.Size = new System.Drawing.Size(85, 85);
            this.btnDeviceSet.TabIndex = 30;
            this.btnDeviceSet.TabStop = false;
            this.btnDeviceSet.UseVisualStyleBackColor = false;
            this.btnDeviceSet.Click += new System.EventHandler(this.btnDeviceSet_Click);
            this.btnDeviceSet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDeviceSet_MouseDown);
            this.btnDeviceSet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDeviceSet_MouseUp);
            // 
            // pnlSp3
            // 
            this.pnlSp3.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pnlSp3.BackgroundImage = global::CADC.Properties.Resources.txt_bg;
            this.pnlSp3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSp3.Controls.Add(this.labSp3Name);
            this.pnlSp3.Controls.Add(this.labSp3Val);
            this.pnlSp3.Controls.Add(this.labSp3Unit);
            this.pnlSp3.Location = new System.Drawing.Point(151, 161);
            this.pnlSp3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSp3.Name = "pnlSp3";
            this.pnlSp3.Size = new System.Drawing.Size(120, 68);
            this.pnlSp3.TabIndex = 27;
            // 
            // labSp3Name
            // 
            this.labSp3Name.BackColor = System.Drawing.Color.Transparent;
            this.labSp3Name.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSp3Name.Location = new System.Drawing.Point(10, 6);
            this.labSp3Name.Name = "labSp3Name";
            this.labSp3Name.Size = new System.Drawing.Size(100, 15);
            this.labSp3Name.TabIndex = 30;
            this.labSp3Name.Text = "전류센서";
            this.labSp3Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSp3Val
            // 
            this.labSp3Val.BackColor = System.Drawing.Color.Transparent;
            this.labSp3Val.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSp3Val.ForeColor = System.Drawing.Color.Blue;
            this.labSp3Val.Location = new System.Drawing.Point(12, 30);
            this.labSp3Val.Name = "labSp3Val";
            this.labSp3Val.Size = new System.Drawing.Size(70, 22);
            this.labSp3Val.TabIndex = 32;
            this.labSp3Val.Text = "85.6";
            this.labSp3Val.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labSp3Unit
            // 
            this.labSp3Unit.BackColor = System.Drawing.Color.Transparent;
            this.labSp3Unit.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSp3Unit.Location = new System.Drawing.Point(89, 30);
            this.labSp3Unit.Name = "labSp3Unit";
            this.labSp3Unit.Size = new System.Drawing.Size(25, 21);
            this.labSp3Unit.TabIndex = 3;
            this.labSp3Unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel4.BackgroundImage = global::CADC.Properties.Resources.txt_bg;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.labSpare);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(151, 94);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 68);
            this.panel4.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(10, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 15);
            this.label12.TabIndex = 30;
            this.label12.Text = "PT100센서";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSpare
            // 
            this.labSpare.BackColor = System.Drawing.Color.Transparent;
            this.labSpare.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSpare.ForeColor = System.Drawing.Color.Blue;
            this.labSpare.Location = new System.Drawing.Point(12, 30);
            this.labSpare.Name = "labSpare";
            this.labSpare.Size = new System.Drawing.Size(70, 22);
            this.labSpare.TabIndex = 32;
            this.labSpare.Text = "85.6";
            this.labSpare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(89, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "℃";
            // 
            // pnlSp2
            // 
            this.pnlSp2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pnlSp2.BackgroundImage = global::CADC.Properties.Resources.txt_bg;
            this.pnlSp2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSp2.Controls.Add(this.labSp2Name);
            this.pnlSp2.Controls.Add(this.labSp2Val);
            this.pnlSp2.Controls.Add(this.labSp2Unit);
            this.pnlSp2.Location = new System.Drawing.Point(25, 161);
            this.pnlSp2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSp2.Name = "pnlSp2";
            this.pnlSp2.Size = new System.Drawing.Size(120, 68);
            this.pnlSp2.TabIndex = 26;
            // 
            // labSp2Name
            // 
            this.labSp2Name.BackColor = System.Drawing.Color.Transparent;
            this.labSp2Name.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSp2Name.Location = new System.Drawing.Point(10, 6);
            this.labSp2Name.Name = "labSp2Name";
            this.labSp2Name.Size = new System.Drawing.Size(100, 15);
            this.labSp2Name.TabIndex = 30;
            this.labSp2Name.Text = "전류센서";
            this.labSp2Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSp2Val
            // 
            this.labSp2Val.BackColor = System.Drawing.Color.Transparent;
            this.labSp2Val.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSp2Val.ForeColor = System.Drawing.Color.Blue;
            this.labSp2Val.Location = new System.Drawing.Point(12, 30);
            this.labSp2Val.Name = "labSp2Val";
            this.labSp2Val.Size = new System.Drawing.Size(70, 22);
            this.labSp2Val.TabIndex = 31;
            this.labSp2Val.Text = "32.4";
            this.labSp2Val.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labSp2Unit
            // 
            this.labSp2Unit.BackColor = System.Drawing.Color.Transparent;
            this.labSp2Unit.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSp2Unit.Location = new System.Drawing.Point(89, 30);
            this.labSp2Unit.Name = "labSp2Unit";
            this.labSp2Unit.Size = new System.Drawing.Size(25, 21);
            this.labSp2Unit.TabIndex = 3;
            this.labSp2Unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel3.BackgroundImage = global::CADC.Properties.Resources.txt_bg;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.labPH);
            this.panel3.Location = new System.Drawing.Point(25, 94);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(120, 68);
            this.panel3.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.Location = new System.Drawing.Point(10, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 15);
            this.label13.TabIndex = 30;
            this.label13.Text = "PH센서";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(83, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 35);
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // labPH
            // 
            this.labPH.BackColor = System.Drawing.Color.Transparent;
            this.labPH.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labPH.ForeColor = System.Drawing.Color.Blue;
            this.labPH.Location = new System.Drawing.Point(12, 30);
            this.labPH.Name = "labPH";
            this.labPH.Size = new System.Drawing.Size(70, 22);
            this.labPH.TabIndex = 31;
            this.labPH.Text = "32.4";
            this.labPH.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::CADC.Properties.Resources.txt_bg;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.labCurr);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(151, 27);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 68);
            this.panel2.TabIndex = 27;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(10, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 15);
            this.label11.TabIndex = 30;
            this.label11.Text = "전류센서";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labCurr
            // 
            this.labCurr.BackColor = System.Drawing.Color.Transparent;
            this.labCurr.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labCurr.ForeColor = System.Drawing.Color.Blue;
            this.labCurr.Location = new System.Drawing.Point(12, 30);
            this.labCurr.Name = "labCurr";
            this.labCurr.Size = new System.Drawing.Size(70, 22);
            this.labCurr.TabIndex = 30;
            this.labCurr.Text = "80.2";
            this.labCurr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(89, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 21);
            this.label6.TabIndex = 3;
            this.label6.Text = "A";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.BackgroundImage = global::CADC.Properties.Resources.txt_bg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.labVolt);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(25, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 68);
            this.panel1.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(10, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 15);
            this.label10.TabIndex = 30;
            this.label10.Text = "전압센서";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labVolt
            // 
            this.labVolt.BackColor = System.Drawing.Color.Transparent;
            this.labVolt.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labVolt.ForeColor = System.Drawing.Color.Blue;
            this.labVolt.Location = new System.Drawing.Point(12, 30);
            this.labVolt.Name = "labVolt";
            this.labVolt.Size = new System.Drawing.Size(70, 22);
            this.labVolt.TabIndex = 29;
            this.labVolt.Text = "380.0";
            this.labVolt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(89, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 21);
            this.label8.TabIndex = 3;
            this.label8.Text = "V";
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.BackgroundImage = global::CADC.Properties.Resources.RightArrow;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.Red;
            this.btnNext.Location = new System.Drawing.Point(1205, 362);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(47, 38);
            this.btnNext.TabIndex = 20;
            this.btnNext.TabStop = false;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNext_MouseDown);
            this.btnNext.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNext_MouseUp);
            // 
            // btnPower
            // 
            this.btnPower.BackColor = System.Drawing.Color.Transparent;
            this.btnPower.BackgroundImage = global::CADC.Properties.Resources.shut_off;
            this.btnPower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPower.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPower.FlatAppearance.BorderSize = 0;
            this.btnPower.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPower.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPower.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPower.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPower.ForeColor = System.Drawing.Color.White;
            this.btnPower.Location = new System.Drawing.Point(12, 703);
            this.btnPower.Name = "btnPower";
            this.btnPower.Size = new System.Drawing.Size(85, 85);
            this.btnPower.TabIndex = 19;
            this.btnPower.TabStop = false;
            this.btnPower.UseVisualStyleBackColor = false;
            this.btnPower.Click += new System.EventHandler(this.btnPower_Click);
            this.btnPower.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPower_MouseDown);
            this.btnPower.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPower_MouseUp);
            // 
            // btnControl
            // 
            this.btnControl.BackColor = System.Drawing.Color.Transparent;
            this.btnControl.BackgroundImage = global::CADC.Properties.Resources.work_off;
            this.btnControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnControl.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnControl.FlatAppearance.BorderSize = 0;
            this.btnControl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnControl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnControl.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnControl.ForeColor = System.Drawing.Color.White;
            this.btnControl.Location = new System.Drawing.Point(1142, 82);
            this.btnControl.Name = "btnControl";
            this.btnControl.Size = new System.Drawing.Size(110, 47);
            this.btnControl.TabIndex = 19;
            this.btnControl.TabStop = false;
            this.btnControl.UseVisualStyleBackColor = false;
            this.btnControl.Click += new System.EventHandler(this.btnControl_Click);
            this.btnControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnControl_MouseDown);
            this.btnControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnControl_MouseUp);
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.White;
            this.btnSetting.BackgroundImage = global::CADC.Properties.Resources.setting_off;
            this.btnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetting.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSetting.FlatAppearance.BorderSize = 0;
            this.btnSetting.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetting.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSetting.ForeColor = System.Drawing.Color.White;
            this.btnSetting.Location = new System.Drawing.Point(1142, 28);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(110, 44);
            this.btnSetting.TabIndex = 18;
            this.btnSetting.TabStop = false;
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            this.btnSetting.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSetting_MouseDown);
            this.btnSetting.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSetting_MouseUp);
            // 
            // btnFW
            // 
            this.btnFW.BackColor = System.Drawing.Color.Transparent;
            this.btnFW.BackgroundImage = global::CADC.Properties.Resources.fw_off;
            this.btnFW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFW.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnFW.FlatAppearance.BorderSize = 0;
            this.btnFW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFW.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFW.ForeColor = System.Drawing.Color.White;
            this.btnFW.Location = new System.Drawing.Point(1167, 654);
            this.btnFW.Name = "btnFW";
            this.btnFW.Size = new System.Drawing.Size(85, 85);
            this.btnFW.TabIndex = 14;
            this.btnFW.TabStop = false;
            this.btnFW.UseVisualStyleBackColor = false;
            this.btnFW.Click += new System.EventHandler(this.btnFW_Click);
            this.btnFW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnFW_MouseDown);
            this.btnFW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnFW_MouseUp);
            // 
            // btnMotor
            // 
            this.btnMotor.BackColor = System.Drawing.Color.Transparent;
            this.btnMotor.BackgroundImage = global::CADC.Properties.Resources.motor_off;
            this.btnMotor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMotor.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnMotor.FlatAppearance.BorderSize = 0;
            this.btnMotor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMotor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMotor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMotor.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMotor.ForeColor = System.Drawing.Color.White;
            this.btnMotor.Location = new System.Drawing.Point(12, 505);
            this.btnMotor.Name = "btnMotor";
            this.btnMotor.Size = new System.Drawing.Size(85, 85);
            this.btnMotor.TabIndex = 14;
            this.btnMotor.TabStop = false;
            this.btnMotor.UseVisualStyleBackColor = false;
            this.btnMotor.Click += new System.EventHandler(this.btnMotor_Click);
            this.btnMotor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMotor_MouseDown);
            this.btnMotor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMotor_MouseUp);
            // 
            // btnAir
            // 
            this.btnAir.BackColor = System.Drawing.Color.Transparent;
            this.btnAir.BackgroundImage = global::CADC.Properties.Resources.air_off;
            this.btnAir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAir.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnAir.FlatAppearance.BorderSize = 0;
            this.btnAir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAir.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAir.ForeColor = System.Drawing.Color.White;
            this.btnAir.Location = new System.Drawing.Point(12, 414);
            this.btnAir.Name = "btnAir";
            this.btnAir.Size = new System.Drawing.Size(85, 85);
            this.btnAir.TabIndex = 14;
            this.btnAir.TabStop = false;
            this.btnAir.UseVisualStyleBackColor = false;
            this.btnAir.Click += new System.EventHandler(this.btnAir_Click);
            this.btnAir.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAir_MouseDown);
            this.btnAir.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAir_MouseUp);
            // 
            // btnAutoMnl
            // 
            this.btnAutoMnl.BackColor = System.Drawing.Color.Transparent;
            this.btnAutoMnl.BackgroundImage = global::CADC.Properties.Resources.manual_off;
            this.btnAutoMnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoMnl.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnAutoMnl.FlatAppearance.BorderSize = 0;
            this.btnAutoMnl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAutoMnl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAutoMnl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoMnl.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAutoMnl.ForeColor = System.Drawing.Color.White;
            this.btnAutoMnl.Location = new System.Drawing.Point(12, 323);
            this.btnAutoMnl.Name = "btnAutoMnl";
            this.btnAutoMnl.Size = new System.Drawing.Size(85, 85);
            this.btnAutoMnl.TabIndex = 14;
            this.btnAutoMnl.TabStop = false;
            this.btnAutoMnl.UseVisualStyleBackColor = false;
            this.btnAutoMnl.Click += new System.EventHandler(this.btnAutoMnl_Click);
            this.btnAutoMnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAutoMnl_MouseDown);
            this.btnAutoMnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAutoMnl_MouseUp);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.BackgroundImage = global::CADC.Properties.Resources.start_off;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(12, 232);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 85);
            this.btnStart.TabIndex = 14;
            this.btnStart.TabStop = false;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseDown);
            this.btnStart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseUp);
            // 
            // btnRed
            // 
            this.btnRed.BackColor = System.Drawing.Color.White;
            this.btnRed.BackgroundImage = global::CADC.Properties.Resources.btnRed_off;
            this.btnRed.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnRed.FlatAppearance.BorderSize = 0;
            this.btnRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRed.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRed.ForeColor = System.Drawing.Color.Red;
            this.btnRed.Location = new System.Drawing.Point(814, 15);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(40, 40);
            this.btnRed.TabIndex = 12;
            this.btnRed.TabStop = false;
            this.btnRed.UseVisualStyleBackColor = false;
            // 
            // btnGrn
            // 
            this.btnGrn.BackColor = System.Drawing.Color.White;
            this.btnGrn.BackgroundImage = global::CADC.Properties.Resources.btnGrn_off;
            this.btnGrn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGrn.FlatAppearance.BorderSize = 0;
            this.btnGrn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrn.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGrn.ForeColor = System.Drawing.Color.Red;
            this.btnGrn.Location = new System.Drawing.Point(746, 15);
            this.btnGrn.Name = "btnGrn";
            this.btnGrn.Size = new System.Drawing.Size(40, 40);
            this.btnGrn.TabIndex = 12;
            this.btnGrn.TabStop = false;
            this.btnGrn.UseVisualStyleBackColor = false;
            // 
            // btnEmg
            // 
            this.btnEmg.BackColor = System.Drawing.Color.Transparent;
            this.btnEmg.BackgroundImage = global::CADC.Properties.Resources.emg_on;
            this.btnEmg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEmg.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnEmg.FlatAppearance.BorderSize = 0;
            this.btnEmg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmg.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnEmg.ForeColor = System.Drawing.Color.Red;
            this.btnEmg.Location = new System.Drawing.Point(411, 18);
            this.btnEmg.Name = "btnEmg";
            this.btnEmg.Size = new System.Drawing.Size(125, 35);
            this.btnEmg.TabIndex = 12;
            this.btnEmg.TabStop = false;
            this.btnEmg.UseVisualStyleBackColor = false;
            this.btnEmg.Visible = false;
            // 
            // pnlMulti
            // 
            this.pnlMulti.BackColor = System.Drawing.Color.Transparent;
            this.pnlMulti.BackgroundImage = global::CADC.Properties.Resources.cadc_multi;
            this.pnlMulti.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlMulti.Controls.Add(this.label14);
            this.pnlMulti.Controls.Add(this.labMulDmpIn);
            this.pnlMulti.Controls.Add(this.labMulDmpOut);
            this.pnlMulti.Controls.Add(this.picMulFan4);
            this.pnlMulti.Controls.Add(this.picMulFan3);
            this.pnlMulti.Controls.Add(this.picMulFan2);
            this.pnlMulti.Controls.Add(this.picMulWind);
            this.pnlMulti.Controls.Add(this.labMulDoor);
            this.pnlMulti.Controls.Add(this.picMulDoor);
            this.pnlMulti.Controls.Add(this.picMulValve1);
            this.pnlMulti.Controls.Add(this.picMulValve2);
            this.pnlMulti.Controls.Add(this.picMulValve3);
            this.pnlMulti.Controls.Add(this.picMulMotor);
            this.pnlMulti.Controls.Add(this.picMulFan1);
            this.pnlMulti.Controls.Add(this.picMulDmpOut);
            this.pnlMulti.Controls.Add(this.labMulDustMot);
            this.pnlMulti.Controls.Add(this.labMulAirTot);
            this.pnlMulti.Controls.Add(this.labMulSolValve);
            this.pnlMulti.Controls.Add(this.labMulDiff4);
            this.pnlMulti.Controls.Add(this.labMulDiff3);
            this.pnlMulti.Controls.Add(this.labMulDiff2);
            this.pnlMulti.Controls.Add(this.labMulDiff1);
            this.pnlMulti.Controls.Add(this.labMulCurr4);
            this.pnlMulti.Controls.Add(this.labMulCurr3);
            this.pnlMulti.Controls.Add(this.labMulCurr2);
            this.pnlMulti.Controls.Add(this.labMulCurr1);
            this.pnlMulti.Controls.Add(this.labMulTemp);
            this.pnlMulti.Controls.Add(this.picMulAirFlow);
            this.pnlMulti.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pnlMulti.Location = new System.Drawing.Point(280, 100);
            this.pnlMulti.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMulti.Name = "pnlMulti";
            this.pnlMulti.Size = new System.Drawing.Size(720, 609);
            this.pnlMulti.TabIndex = 28;
            this.pnlMulti.Visible = false;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.LightGray;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.Location = new System.Drawing.Point(24, 450);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label14.Size = new System.Drawing.Size(115, 30);
            this.label14.TabIndex = 111;
            this.label14.Text = "IN DAMPER";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulDmpIn
            // 
            this.labMulDmpIn.BackColor = System.Drawing.Color.LightGray;
            this.labMulDmpIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labMulDmpIn.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulDmpIn.Location = new System.Drawing.Point(24, 480);
            this.labMulDmpIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulDmpIn.Name = "labMulDmpIn";
            this.labMulDmpIn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labMulDmpIn.Size = new System.Drawing.Size(115, 30);
            this.labMulDmpIn.TabIndex = 112;
            this.labMulDmpIn.Text = "0 ˚";
            this.labMulDmpIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulDmpOut
            // 
            this.labMulDmpOut.BackColor = System.Drawing.Color.Transparent;
            this.labMulDmpOut.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulDmpOut.Location = new System.Drawing.Point(160, 77);
            this.labMulDmpOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulDmpOut.Name = "labMulDmpOut";
            this.labMulDmpOut.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labMulDmpOut.Size = new System.Drawing.Size(115, 20);
            this.labMulDmpOut.TabIndex = 103;
            this.labMulDmpOut.Text = "0 ˚";
            this.labMulDmpOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picMulFan4
            // 
            this.picMulFan4.BackColor = System.Drawing.Color.Transparent;
            this.picMulFan4.BackgroundImage = global::CADC.Properties.Resources.prop;
            this.picMulFan4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMulFan4.Location = new System.Drawing.Point(474, 149);
            this.picMulFan4.Name = "picMulFan4";
            this.picMulFan4.Size = new System.Drawing.Size(30, 30);
            this.picMulFan4.TabIndex = 102;
            this.picMulFan4.TabStop = false;
            // 
            // picMulFan3
            // 
            this.picMulFan3.BackColor = System.Drawing.Color.Transparent;
            this.picMulFan3.BackgroundImage = global::CADC.Properties.Resources.prop;
            this.picMulFan3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMulFan3.Location = new System.Drawing.Point(390, 149);
            this.picMulFan3.Name = "picMulFan3";
            this.picMulFan3.Size = new System.Drawing.Size(30, 30);
            this.picMulFan3.TabIndex = 101;
            this.picMulFan3.TabStop = false;
            // 
            // picMulFan2
            // 
            this.picMulFan2.BackColor = System.Drawing.Color.Transparent;
            this.picMulFan2.BackgroundImage = global::CADC.Properties.Resources.prop;
            this.picMulFan2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMulFan2.Location = new System.Drawing.Point(305, 149);
            this.picMulFan2.Name = "picMulFan2";
            this.picMulFan2.Size = new System.Drawing.Size(30, 30);
            this.picMulFan2.TabIndex = 100;
            this.picMulFan2.TabStop = false;
            // 
            // picMulWind
            // 
            this.picMulWind.BackColor = System.Drawing.Color.Transparent;
            this.picMulWind.BackgroundImage = global::CADC.Properties.Resources.arr_up;
            this.picMulWind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMulWind.Location = new System.Drawing.Point(159, 310);
            this.picMulWind.Name = "picMulWind";
            this.picMulWind.Size = new System.Drawing.Size(20, 20);
            this.picMulWind.TabIndex = 99;
            this.picMulWind.TabStop = false;
            // 
            // labMulDoor
            // 
            this.labMulDoor.BackColor = System.Drawing.Color.LightGray;
            this.labMulDoor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labMulDoor.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulDoor.Location = new System.Drawing.Point(157, 386);
            this.labMulDoor.Name = "labMulDoor";
            this.labMulDoor.Size = new System.Drawing.Size(73, 16);
            this.labMulDoor.TabIndex = 58;
            this.labMulDoor.Text = "CLOSE";
            this.labMulDoor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picMulDoor
            // 
            this.picMulDoor.BackColor = System.Drawing.Color.Transparent;
            this.picMulDoor.BackgroundImage = global::CADC.Properties.Resources.door;
            this.picMulDoor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMulDoor.Location = new System.Drawing.Point(145, 403);
            this.picMulDoor.Name = "picMulDoor";
            this.picMulDoor.Size = new System.Drawing.Size(98, 88);
            this.picMulDoor.TabIndex = 98;
            this.picMulDoor.TabStop = false;
            // 
            // picMulValve1
            // 
            this.picMulValve1.BackColor = System.Drawing.Color.Transparent;
            this.picMulValve1.BackgroundImage = global::CADC.Properties.Resources.sw_on;
            this.picMulValve1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMulValve1.Location = new System.Drawing.Point(365, 273);
            this.picMulValve1.Name = "picMulValve1";
            this.picMulValve1.Size = new System.Drawing.Size(75, 56);
            this.picMulValve1.TabIndex = 97;
            this.picMulValve1.TabStop = false;
            // 
            // picMulValve2
            // 
            this.picMulValve2.BackColor = System.Drawing.Color.Transparent;
            this.picMulValve2.BackgroundImage = global::CADC.Properties.Resources.sw_on;
            this.picMulValve2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMulValve2.Location = new System.Drawing.Point(286, 273);
            this.picMulValve2.Name = "picMulValve2";
            this.picMulValve2.Size = new System.Drawing.Size(75, 56);
            this.picMulValve2.TabIndex = 97;
            this.picMulValve2.TabStop = false;
            // 
            // picMulValve3
            // 
            this.picMulValve3.BackColor = System.Drawing.Color.Transparent;
            this.picMulValve3.BackgroundImage = global::CADC.Properties.Resources.sw_on;
            this.picMulValve3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMulValve3.Location = new System.Drawing.Point(205, 274);
            this.picMulValve3.Name = "picMulValve3";
            this.picMulValve3.Size = new System.Drawing.Size(75, 56);
            this.picMulValve3.TabIndex = 97;
            this.picMulValve3.TabStop = false;
            // 
            // picMulMotor
            // 
            this.picMulMotor.BackColor = System.Drawing.Color.Transparent;
            this.picMulMotor.BackgroundImage = global::CADC.Properties.Resources.prop;
            this.picMulMotor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMulMotor.Location = new System.Drawing.Point(414, 556);
            this.picMulMotor.Name = "picMulMotor";
            this.picMulMotor.Size = new System.Drawing.Size(30, 30);
            this.picMulMotor.TabIndex = 96;
            this.picMulMotor.TabStop = false;
            // 
            // picMulFan1
            // 
            this.picMulFan1.BackColor = System.Drawing.Color.Transparent;
            this.picMulFan1.BackgroundImage = global::CADC.Properties.Resources.prop;
            this.picMulFan1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMulFan1.Location = new System.Drawing.Point(219, 149);
            this.picMulFan1.Name = "picMulFan1";
            this.picMulFan1.Size = new System.Drawing.Size(30, 30);
            this.picMulFan1.TabIndex = 95;
            this.picMulFan1.TabStop = false;
            // 
            // picMulDmpOut
            // 
            this.picMulDmpOut.BackColor = System.Drawing.Color.Transparent;
            this.picMulDmpOut.BackgroundImage = global::CADC.Properties.Resources.cap_0;
            this.picMulDmpOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picMulDmpOut.ErrorImage = global::CADC.Properties.Resources.cap_0;
            this.picMulDmpOut.Location = new System.Drawing.Point(315, 10);
            this.picMulDmpOut.Name = "picMulDmpOut";
            this.picMulDmpOut.Size = new System.Drawing.Size(90, 50);
            this.picMulDmpOut.TabIndex = 94;
            this.picMulDmpOut.TabStop = false;
            // 
            // labMulDustMot
            // 
            this.labMulDustMot.BackColor = System.Drawing.Color.Transparent;
            this.labMulDustMot.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulDustMot.Location = new System.Drawing.Point(519, 569);
            this.labMulDustMot.Name = "labMulDustMot";
            this.labMulDustMot.Size = new System.Drawing.Size(40, 16);
            this.labMulDustMot.TabIndex = 91;
            this.labMulDustMot.Text = "255";
            this.labMulDustMot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulAirTot
            // 
            this.labMulAirTot.BackColor = System.Drawing.Color.Transparent;
            this.labMulAirTot.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulAirTot.Location = new System.Drawing.Point(440, 326);
            this.labMulAirTot.Name = "labMulAirTot";
            this.labMulAirTot.Size = new System.Drawing.Size(80, 18);
            this.labMulAirTot.TabIndex = 88;
            this.labMulAirTot.Text = "255";
            this.labMulAirTot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulSolValve
            // 
            this.labMulSolValve.BackColor = System.Drawing.Color.Transparent;
            this.labMulSolValve.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulSolValve.Location = new System.Drawing.Point(440, 282);
            this.labMulSolValve.Name = "labMulSolValve";
            this.labMulSolValve.Size = new System.Drawing.Size(80, 18);
            this.labMulSolValve.TabIndex = 89;
            this.labMulSolValve.Text = "255";
            this.labMulSolValve.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulDiff4
            // 
            this.labMulDiff4.BackColor = System.Drawing.Color.Transparent;
            this.labMulDiff4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulDiff4.Location = new System.Drawing.Point(577, 407);
            this.labMulDiff4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulDiff4.Name = "labMulDiff4";
            this.labMulDiff4.Size = new System.Drawing.Size(90, 22);
            this.labMulDiff4.TabIndex = 25;
            this.labMulDiff4.Text = "0.0";
            this.labMulDiff4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulDiff3
            // 
            this.labMulDiff3.BackColor = System.Drawing.Color.Transparent;
            this.labMulDiff3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulDiff3.Location = new System.Drawing.Point(577, 384);
            this.labMulDiff3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulDiff3.Name = "labMulDiff3";
            this.labMulDiff3.Size = new System.Drawing.Size(90, 22);
            this.labMulDiff3.TabIndex = 25;
            this.labMulDiff3.Text = "0.0";
            this.labMulDiff3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulDiff2
            // 
            this.labMulDiff2.BackColor = System.Drawing.Color.Transparent;
            this.labMulDiff2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulDiff2.Location = new System.Drawing.Point(577, 361);
            this.labMulDiff2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulDiff2.Name = "labMulDiff2";
            this.labMulDiff2.Size = new System.Drawing.Size(90, 22);
            this.labMulDiff2.TabIndex = 25;
            this.labMulDiff2.Text = "0.0";
            this.labMulDiff2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulDiff1
            // 
            this.labMulDiff1.BackColor = System.Drawing.Color.Transparent;
            this.labMulDiff1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulDiff1.Location = new System.Drawing.Point(577, 339);
            this.labMulDiff1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulDiff1.Name = "labMulDiff1";
            this.labMulDiff1.Size = new System.Drawing.Size(90, 20);
            this.labMulDiff1.TabIndex = 25;
            this.labMulDiff1.Text = "0.0";
            this.labMulDiff1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulCurr4
            // 
            this.labMulCurr4.BackColor = System.Drawing.Color.Transparent;
            this.labMulCurr4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labMulCurr4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulCurr4.Location = new System.Drawing.Point(460, 212);
            this.labMulCurr4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulCurr4.Name = "labMulCurr4";
            this.labMulCurr4.Size = new System.Drawing.Size(62, 16);
            this.labMulCurr4.TabIndex = 27;
            this.labMulCurr4.Text = "50.2 A";
            this.labMulCurr4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulCurr3
            // 
            this.labMulCurr3.BackColor = System.Drawing.Color.Transparent;
            this.labMulCurr3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labMulCurr3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulCurr3.Location = new System.Drawing.Point(375, 212);
            this.labMulCurr3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulCurr3.Name = "labMulCurr3";
            this.labMulCurr3.Size = new System.Drawing.Size(62, 16);
            this.labMulCurr3.TabIndex = 27;
            this.labMulCurr3.Text = "50.2 A";
            this.labMulCurr3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulCurr2
            // 
            this.labMulCurr2.BackColor = System.Drawing.Color.Transparent;
            this.labMulCurr2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labMulCurr2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulCurr2.Location = new System.Drawing.Point(289, 212);
            this.labMulCurr2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulCurr2.Name = "labMulCurr2";
            this.labMulCurr2.Size = new System.Drawing.Size(62, 16);
            this.labMulCurr2.TabIndex = 27;
            this.labMulCurr2.Text = "50.2 A";
            this.labMulCurr2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulCurr1
            // 
            this.labMulCurr1.BackColor = System.Drawing.Color.Transparent;
            this.labMulCurr1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulCurr1.Location = new System.Drawing.Point(204, 212);
            this.labMulCurr1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulCurr1.Name = "labMulCurr1";
            this.labMulCurr1.Size = new System.Drawing.Size(62, 16);
            this.labMulCurr1.TabIndex = 27;
            this.labMulCurr1.Text = "50.2 A";
            this.labMulCurr1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labMulTemp
            // 
            this.labMulTemp.BackColor = System.Drawing.Color.Transparent;
            this.labMulTemp.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labMulTemp.Location = new System.Drawing.Point(446, 77);
            this.labMulTemp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labMulTemp.Name = "labMulTemp";
            this.labMulTemp.Size = new System.Drawing.Size(115, 20);
            this.labMulTemp.TabIndex = 26;
            this.labMulTemp.Text = "-20 ℃";
            this.labMulTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picMulAirFlow
            // 
            this.picMulAirFlow.BackColor = System.Drawing.Color.Transparent;
            this.picMulAirFlow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picMulAirFlow.BackgroundImage")));
            this.picMulAirFlow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMulAirFlow.Location = new System.Drawing.Point(0, -20);
            this.picMulAirFlow.Name = "picMulAirFlow";
            this.picMulAirFlow.Size = new System.Drawing.Size(720, 600);
            this.picMulAirFlow.TabIndex = 106;
            this.picMulAirFlow.TabStop = false;
            // 
            // pnlFanVolt
            // 
            this.pnlFanVolt.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFanVolt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFanVolt.Controls.Add(this.label5);
            this.pnlFanVolt.Controls.Add(this.labFanVolt);
            this.pnlFanVolt.Controls.Add(this.label3);
            this.pnlFanVolt.Controls.Add(this.labYD);
            this.pnlFanVolt.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pnlFanVolt.Location = new System.Drawing.Point(280, 100);
            this.pnlFanVolt.Name = "pnlFanVolt";
            this.pnlFanVolt.Size = new System.Drawing.Size(150, 69);
            this.pnlFanVolt.TabIndex = 104;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 94;
            this.label5.Text = "Fan 전력";
            // 
            // labFanVolt
            // 
            this.labFanVolt.BackColor = System.Drawing.Color.LightGray;
            this.labFanVolt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labFanVolt.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labFanVolt.Location = new System.Drawing.Point(78, 7);
            this.labFanVolt.Name = "labFanVolt";
            this.labFanVolt.Size = new System.Drawing.Size(63, 25);
            this.labFanVolt.TabIndex = 93;
            this.labFanVolt.Text = "10.5 Kw";
            this.labFanVolt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 94;
            this.label3.Text = "Fan 결선";
            // 
            // labYD
            // 
            this.labYD.BackColor = System.Drawing.Color.LightGray;
            this.labYD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labYD.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labYD.Location = new System.Drawing.Point(78, 37);
            this.labYD.Name = "labYD";
            this.labYD.Size = new System.Drawing.Size(63, 25);
            this.labYD.TabIndex = 93;
            this.labYD.Text = "Y / Delta";
            this.labYD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSingle
            // 
            this.pnlSingle.BackColor = System.Drawing.Color.Transparent;
            this.pnlSingle.BackgroundImage = global::CADC.Properties.Resources.cadc_single;
            this.pnlSingle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlSingle.Controls.Add(this.label15);
            this.pnlSingle.Controls.Add(this.labSngDmpIn);
            this.pnlSingle.Controls.Add(this.picSngWind);
            this.pnlSingle.Controls.Add(this.picSngDoor);
            this.pnlSingle.Controls.Add(this.picSngValve3);
            this.pnlSingle.Controls.Add(this.picSngValve2);
            this.pnlSingle.Controls.Add(this.picSngValve1);
            this.pnlSingle.Controls.Add(this.picSngMotor);
            this.pnlSingle.Controls.Add(this.picSngFan1);
            this.pnlSingle.Controls.Add(this.picSngDmpOut);
            this.pnlSingle.Controls.Add(this.labSngAirTot);
            this.pnlSingle.Controls.Add(this.labSngDustMot);
            this.pnlSingle.Controls.Add(this.labSngSolValve);
            this.pnlSingle.Controls.Add(this.labSngDoor);
            this.pnlSingle.Controls.Add(this.labSngDiff);
            this.pnlSingle.Controls.Add(this.labSngCurr);
            this.pnlSingle.Controls.Add(this.labSngDmpOut);
            this.pnlSingle.Controls.Add(this.labSngTemp);
            this.pnlSingle.Controls.Add(this.picSngAirFlow);
            this.pnlSingle.Location = new System.Drawing.Point(280, 100);
            this.pnlSingle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSingle.Name = "pnlSingle";
            this.pnlSingle.Size = new System.Drawing.Size(720, 609);
            this.pnlSingle.TabIndex = 24;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.LightGray;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.Location = new System.Drawing.Point(24, 450);
            this.label15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label15.Name = "label15";
            this.label15.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label15.Size = new System.Drawing.Size(115, 30);
            this.label15.TabIndex = 110;
            this.label15.Text = "IN DAMPER";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSngDmpIn
            // 
            this.labSngDmpIn.BackColor = System.Drawing.Color.LightGray;
            this.labSngDmpIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labSngDmpIn.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSngDmpIn.Location = new System.Drawing.Point(24, 480);
            this.labSngDmpIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labSngDmpIn.Name = "labSngDmpIn";
            this.labSngDmpIn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labSngDmpIn.Size = new System.Drawing.Size(115, 30);
            this.labSngDmpIn.TabIndex = 110;
            this.labSngDmpIn.Text = "0 ˚";
            this.labSngDmpIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picSngWind
            // 
            this.picSngWind.BackColor = System.Drawing.Color.Transparent;
            this.picSngWind.BackgroundImage = global::CADC.Properties.Resources.arr_up;
            this.picSngWind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSngWind.Location = new System.Drawing.Point(159, 310);
            this.picSngWind.Name = "picSngWind";
            this.picSngWind.Size = new System.Drawing.Size(20, 20);
            this.picSngWind.TabIndex = 104;
            this.picSngWind.TabStop = false;
            // 
            // picSngDoor
            // 
            this.picSngDoor.BackColor = System.Drawing.Color.Transparent;
            this.picSngDoor.BackgroundImage = global::CADC.Properties.Resources.door;
            this.picSngDoor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSngDoor.Location = new System.Drawing.Point(145, 403);
            this.picSngDoor.Name = "picSngDoor";
            this.picSngDoor.Size = new System.Drawing.Size(98, 88);
            this.picSngDoor.TabIndex = 103;
            this.picSngDoor.TabStop = false;
            // 
            // picSngValve3
            // 
            this.picSngValve3.BackColor = System.Drawing.Color.Transparent;
            this.picSngValve3.BackgroundImage = global::CADC.Properties.Resources.sw_on;
            this.picSngValve3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSngValve3.Location = new System.Drawing.Point(365, 273);
            this.picSngValve3.Name = "picSngValve3";
            this.picSngValve3.Size = new System.Drawing.Size(75, 56);
            this.picSngValve3.TabIndex = 102;
            this.picSngValve3.TabStop = false;
            // 
            // picSngValve2
            // 
            this.picSngValve2.BackColor = System.Drawing.Color.Transparent;
            this.picSngValve2.BackgroundImage = global::CADC.Properties.Resources.sw_on;
            this.picSngValve2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSngValve2.Location = new System.Drawing.Point(286, 273);
            this.picSngValve2.Name = "picSngValve2";
            this.picSngValve2.Size = new System.Drawing.Size(75, 56);
            this.picSngValve2.TabIndex = 102;
            this.picSngValve2.TabStop = false;
            // 
            // picSngValve1
            // 
            this.picSngValve1.BackColor = System.Drawing.Color.Transparent;
            this.picSngValve1.BackgroundImage = global::CADC.Properties.Resources.sw_on;
            this.picSngValve1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSngValve1.Location = new System.Drawing.Point(205, 274);
            this.picSngValve1.Name = "picSngValve1";
            this.picSngValve1.Size = new System.Drawing.Size(75, 56);
            this.picSngValve1.TabIndex = 102;
            this.picSngValve1.TabStop = false;
            // 
            // picSngMotor
            // 
            this.picSngMotor.BackColor = System.Drawing.Color.Transparent;
            this.picSngMotor.BackgroundImage = global::CADC.Properties.Resources.prop;
            this.picSngMotor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSngMotor.Location = new System.Drawing.Point(414, 556);
            this.picSngMotor.Name = "picSngMotor";
            this.picSngMotor.Size = new System.Drawing.Size(30, 27);
            this.picSngMotor.TabIndex = 101;
            this.picSngMotor.TabStop = false;
            // 
            // picSngFan1
            // 
            this.picSngFan1.BackColor = System.Drawing.Color.Transparent;
            this.picSngFan1.BackgroundImage = global::CADC.Properties.Resources.prop;
            this.picSngFan1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSngFan1.Location = new System.Drawing.Point(345, 149);
            this.picSngFan1.Name = "picSngFan1";
            this.picSngFan1.Size = new System.Drawing.Size(30, 27);
            this.picSngFan1.TabIndex = 100;
            this.picSngFan1.TabStop = false;
            // 
            // picSngDmpOut
            // 
            this.picSngDmpOut.BackColor = System.Drawing.Color.Transparent;
            this.picSngDmpOut.BackgroundImage = global::CADC.Properties.Resources.cap_0;
            this.picSngDmpOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSngDmpOut.Location = new System.Drawing.Point(315, 10);
            this.picSngDmpOut.Name = "picSngDmpOut";
            this.picSngDmpOut.Size = new System.Drawing.Size(90, 50);
            this.picSngDmpOut.TabIndex = 95;
            this.picSngDmpOut.TabStop = false;
            // 
            // labSngAirTot
            // 
            this.labSngAirTot.BackColor = System.Drawing.Color.Transparent;
            this.labSngAirTot.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSngAirTot.Location = new System.Drawing.Point(440, 326);
            this.labSngAirTot.Name = "labSngAirTot";
            this.labSngAirTot.Size = new System.Drawing.Size(80, 20);
            this.labSngAirTot.TabIndex = 58;
            this.labSngAirTot.Text = "255";
            this.labSngAirTot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSngDustMot
            // 
            this.labSngDustMot.BackColor = System.Drawing.Color.Transparent;
            this.labSngDustMot.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSngDustMot.Location = new System.Drawing.Point(519, 569);
            this.labSngDustMot.Name = "labSngDustMot";
            this.labSngDustMot.Size = new System.Drawing.Size(46, 16);
            this.labSngDustMot.TabIndex = 58;
            this.labSngDustMot.Text = "255";
            this.labSngDustMot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSngSolValve
            // 
            this.labSngSolValve.BackColor = System.Drawing.Color.Transparent;
            this.labSngSolValve.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSngSolValve.Location = new System.Drawing.Point(440, 282);
            this.labSngSolValve.Name = "labSngSolValve";
            this.labSngSolValve.Size = new System.Drawing.Size(80, 20);
            this.labSngSolValve.TabIndex = 58;
            this.labSngSolValve.Text = "255";
            this.labSngSolValve.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSngDoor
            // 
            this.labSngDoor.BackColor = System.Drawing.Color.LightGray;
            this.labSngDoor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labSngDoor.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSngDoor.Location = new System.Drawing.Point(157, 386);
            this.labSngDoor.Name = "labSngDoor";
            this.labSngDoor.Size = new System.Drawing.Size(73, 16);
            this.labSngDoor.TabIndex = 29;
            this.labSngDoor.Text = "CLOSE";
            this.labSngDoor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSngDiff
            // 
            this.labSngDiff.BackColor = System.Drawing.Color.Transparent;
            this.labSngDiff.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSngDiff.Location = new System.Drawing.Point(577, 339);
            this.labSngDiff.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labSngDiff.Name = "labSngDiff";
            this.labSngDiff.Size = new System.Drawing.Size(92, 20);
            this.labSngDiff.TabIndex = 25;
            this.labSngDiff.Text = "0.0";
            this.labSngDiff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSngCurr
            // 
            this.labSngCurr.BackColor = System.Drawing.Color.Transparent;
            this.labSngCurr.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSngCurr.Location = new System.Drawing.Point(327, 212);
            this.labSngCurr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labSngCurr.Name = "labSngCurr";
            this.labSngCurr.Size = new System.Drawing.Size(65, 16);
            this.labSngCurr.TabIndex = 27;
            this.labSngCurr.Text = "50.2 A";
            this.labSngCurr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSngDmpOut
            // 
            this.labSngDmpOut.BackColor = System.Drawing.Color.Transparent;
            this.labSngDmpOut.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSngDmpOut.Location = new System.Drawing.Point(160, 77);
            this.labSngDmpOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labSngDmpOut.Name = "labSngDmpOut";
            this.labSngDmpOut.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labSngDmpOut.Size = new System.Drawing.Size(115, 20);
            this.labSngDmpOut.TabIndex = 26;
            this.labSngDmpOut.Text = "0 ˚";
            this.labSngDmpOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSngTemp
            // 
            this.labSngTemp.BackColor = System.Drawing.Color.Transparent;
            this.labSngTemp.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labSngTemp.Location = new System.Drawing.Point(446, 77);
            this.labSngTemp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labSngTemp.Name = "labSngTemp";
            this.labSngTemp.Size = new System.Drawing.Size(115, 20);
            this.labSngTemp.TabIndex = 26;
            this.labSngTemp.Text = "-10 ℃";
            this.labSngTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picSngAirFlow
            // 
            this.picSngAirFlow.BackColor = System.Drawing.Color.Transparent;
            this.picSngAirFlow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picSngAirFlow.BackgroundImage")));
            this.picSngAirFlow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSngAirFlow.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picSngAirFlow.ErrorImage")));
            this.picSngAirFlow.Location = new System.Drawing.Point(0, -20);
            this.picSngAirFlow.Name = "picSngAirFlow";
            this.picSngAirFlow.Size = new System.Drawing.Size(720, 600);
            this.picSngAirFlow.TabIndex = 107;
            this.picSngAirFlow.TabStop = false;
            // 
            // outDamTimer
            // 
            this.outDamTimer.Interval = 200;
            this.outDamTimer.Tick += new System.EventHandler(this.outDamTimer_Tick);
            // 
            // btnAlmReset
            // 
            this.btnAlmReset.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnAlmReset.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnAlmReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAlmReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAlmReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlmReset.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlmReset.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnAlmReset.Location = new System.Drawing.Point(900, 20);
            this.btnAlmReset.Name = "btnAlmReset";
            this.btnAlmReset.Size = new System.Drawing.Size(120, 30);
            this.btnAlmReset.TabIndex = 12;
            this.btnAlmReset.TabStop = false;
            this.btnAlmReset.Text = "Reset Alarm";
            this.btnAlmReset.UseVisualStyleBackColor = false;
            this.btnAlmReset.Click += new System.EventHandler(this.btnAlmReset_Click);
            // 
            // labFWVer
            // 
            this.labFWVer.AutoSize = true;
            this.labFWVer.Location = new System.Drawing.Point(1203, 779);
            this.labFWVer.Name = "labFWVer";
            this.labFWVer.Size = new System.Drawing.Size(46, 12);
            this.labFWVer.TabIndex = 35;
            this.labFWVer.Text = "version";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1157, 779);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "F/W : ";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labCon);
            this.Controls.Add(this.pnlAirTimer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labFWVer);
            this.Controls.Add(this.labVersion);
            this.Controls.Add(this.picPage1);
            this.Controls.Add(this.picPage2);
            this.Controls.Add(this.picPage3);
            this.Controls.Add(this.picPage4);
            this.Controls.Add(this.btnDeviceSet);
            this.Controls.Add(this.pnlSp3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlSp2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPower);
            this.Controls.Add(this.btnControl);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnFW);
            this.Controls.Add(this.btnMotor);
            this.Controls.Add(this.btnAir);
            this.Controls.Add(this.btnAutoMnl);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnRed);
            this.Controls.Add(this.btnGrn);
            this.Controls.Add(this.btnEmg);
            this.Controls.Add(this.btnAlmReset);
            this.Controls.Add(this.btnState);
            this.Controls.Add(this.labPageNo);
            this.Controls.Add(this.pnlFanVolt);
            this.Controls.Add(this.pnlMulti);
            this.Controls.Add(this.pnlSingle);
            this.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Clear Air Dust Controller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlAirTimer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPage3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPage4)).EndInit();
            this.pnlSp3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlSp2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMulti.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMulFan4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulFan3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulFan2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulWind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulDoor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulValve1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulValve2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulValve3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulMotor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulFan1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulDmpOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMulAirFlow)).EndInit();
            this.pnlFanVolt.ResumeLayout(false);
            this.pnlFanVolt.PerformLayout();
            this.pnlSingle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSngWind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngDoor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngValve3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngValve2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngValve1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngMotor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngFan1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngDmpOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSngAirFlow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnState;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnControl;
        private System.Windows.Forms.Button btnAutoMnl;
        private System.Windows.Forms.Button btnAir;
        private System.Windows.Forms.Button btnMotor;
        private System.Windows.Forms.Button btnFW;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label labSngTemp;
        private System.Windows.Forms.Label labSngCurr;
        private System.Windows.Forms.Label labSngDiff;
        private System.Windows.Forms.Panel pnlSingle;
        private System.Windows.Forms.Label labPageNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labVolt;
        private System.Windows.Forms.Label labCurr;
        private System.Windows.Forms.Label labPH;
        private System.Windows.Forms.Label labSpare;
        private System.Windows.Forms.Button btnDeviceSet;
        private System.Windows.Forms.PictureBox picPage1;
        private System.Windows.Forms.PictureBox picPage2;
        private System.Windows.Forms.PictureBox picPage3;
        private System.Windows.Forms.PictureBox picPage4;
        private System.Windows.Forms.Timer alarmTimer;
        private System.Windows.Forms.Label labSngDoor;
		private System.Windows.Forms.Label labSngAirTot;
		private System.Windows.Forms.Label labSngDustMot;
        private System.Windows.Forms.Label labSngSolValve;
		private System.Windows.Forms.Button btnEmg;
		private System.Windows.Forms.Button btnGrn;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labMulTemp;
        private System.Windows.Forms.Label labMulCurr1;
        private System.Windows.Forms.Label labMulCurr2;
        private System.Windows.Forms.Label labMulCurr3;
        private System.Windows.Forms.Label labMulCurr4;
        private System.Windows.Forms.Label labMulDiff1;
        private System.Windows.Forms.Label labMulDiff2;
        private System.Windows.Forms.Label labMulDiff3;
        private System.Windows.Forms.Label labMulDiff4;
        private System.Windows.Forms.Label labMulSolValve;
        private System.Windows.Forms.Label labMulAirTot;
        private System.Windows.Forms.Label labMulDustMot;
        private System.Windows.Forms.Label labYD;
        private System.Windows.Forms.PictureBox picMulFan1;
        private System.Windows.Forms.PictureBox picMulMotor;
        private System.Windows.Forms.PictureBox picMulValve3;
        private System.Windows.Forms.PictureBox picMulValve2;
        private System.Windows.Forms.PictureBox picMulValve1;
        private System.Windows.Forms.PictureBox picMulDoor;
        private System.Windows.Forms.Label labMulDoor;
        private System.Windows.Forms.PictureBox picMulWind;
        private System.Windows.Forms.PictureBox picMulFan2;
        private System.Windows.Forms.PictureBox picMulFan3;
        private System.Windows.Forms.PictureBox picMulFan4;
        private System.Windows.Forms.Panel pnlMulti;
        private System.Windows.Forms.PictureBox picSngDmpOut;
        private System.Windows.Forms.PictureBox picSngWind;
        private System.Windows.Forms.PictureBox picSngDoor;
        private System.Windows.Forms.PictureBox picSngValve3;
        private System.Windows.Forms.PictureBox picSngValve2;
        private System.Windows.Forms.PictureBox picSngValve1;
        private System.Windows.Forms.PictureBox picSngMotor;
        private System.Windows.Forms.PictureBox picSngFan1;
		private System.Windows.Forms.Label labSngDmpOut;
		private System.Windows.Forms.Label labMulDmpOut;
		private System.Windows.Forms.Timer AirWindTimer;
		private System.Windows.Forms.Label labVersion;
		private System.Windows.Forms.PictureBox picMulDmpOut;
		private System.Windows.Forms.Panel pnlSp2;
		private System.Windows.Forms.Label labSp2Val;
		private System.Windows.Forms.Label labSp2Unit;
		private System.Windows.Forms.Panel pnlSp3;
		private System.Windows.Forms.Label labSp3Val;
		private System.Windows.Forms.Label labSp3Unit;
		private System.Windows.Forms.ComboBox comAirTime;
		private System.Windows.Forms.Panel pnlAirTimer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labRemTime;
        private System.Windows.Forms.Label labCon;
		private System.Windows.Forms.Panel pnlFanVolt;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labFanVolt;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label labSp3Name;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label labSp2Name;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnPower;
		private System.Windows.Forms.PictureBox picMulAirFlow;
		private System.Windows.Forms.PictureBox picSngAirFlow;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label labMulDmpIn;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label labSngDmpIn;
		private System.Windows.Forms.Timer outDamTimer;
		private System.Windows.Forms.Button btnAlmReset;
        private System.Windows.Forms.Label labFWVer;
        private System.Windows.Forms.Label label4;
    }
}

