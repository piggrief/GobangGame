using MaterialSkin;
using MaterialSkin.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace Gobang
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Retract_button = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Start_button = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Situation = new System.Windows.Forms.Label();
            this.Computer_First_button = new System.Windows.Forms.Button();
            this.Player_First_button = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CK_Music = new MaterialSkin.Controls.MaterialCheckBox();
            this.VolumeBar = new MaterialSkin.Controls.MaterialProgressBar();
            this.Volume_a = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Volume_s = new MaterialSkin.Controls.MaterialRaisedButton();
            this.MediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.CK_AIdebug = new MaterialSkin.Controls.MaterialCheckBox();
            this.Ck_monitor = new MaterialSkin.Controls.MaterialCheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.SystemMonitor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(11, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 562);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // Retract_button
            // 
            this.Retract_button.AutoSize = true;
            this.Retract_button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Retract_button.Depth = 0;
            this.Retract_button.Enabled = false;
            this.Retract_button.Icon = null;
            this.Retract_button.Location = new System.Drawing.Point(423, 578);
            this.Retract_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Retract_button.MouseState = MaterialSkin.MouseState.HOVER;
            this.Retract_button.Name = "Retract_button";
            this.Retract_button.Primary = true;
            this.Retract_button.Size = new System.Drawing.Size(68, 36);
            this.Retract_button.TabIndex = 4;
            this.Retract_button.Text = "Undo";
            this.Retract_button.UseVisualStyleBackColor = true;
            this.Retract_button.Click += new System.EventHandler(this.Retract_button_Click);
            // 
            // Start_button
            // 
            this.Start_button.AutoSize = true;
            this.Start_button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Start_button.Depth = 0;
            this.Start_button.Icon = null;
            this.Start_button.Location = new System.Drawing.Point(505, 578);
            this.Start_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Start_button.MouseState = MaterialSkin.MouseState.HOVER;
            this.Start_button.Name = "Start_button";
            this.Start_button.Primary = true;
            this.Start_button.Size = new System.Drawing.Size(84, 36);
            this.Start_button.TabIndex = 6;
            this.Start_button.Text = "  Start";
            this.Start_button.UseVisualStyleBackColor = true;
            this.Start_button.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 78);
            this.materialTabSelector1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(624, 42);
            this.materialTabSelector1.TabIndex = 17;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 120);
            this.materialTabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(624, 672);
            this.materialTabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Situation);
            this.tabPage1.Controls.Add(this.Computer_First_button);
            this.tabPage1.Controls.Add(this.Player_First_button);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.Retract_button);
            this.tabPage1.Controls.Add(this.Start_button);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(616, 643);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Game";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Situation
            // 
            this.Situation.AutoSize = true;
            this.Situation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.Situation.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Situation.ForeColor = System.Drawing.Color.White;
            this.Situation.Location = new System.Drawing.Point(19, 625);
            this.Situation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Situation.Name = "Situation";
            this.Situation.Size = new System.Drawing.Size(201, 32);
            this.Situation.TabIndex = 20;
            this.Situation.Text = "AI is FIRST now.";
            // 
            // Computer_First_button
            // 
            this.Computer_First_button.BackColor = System.Drawing.Color.Teal;
            this.Computer_First_button.FlatAppearance.BorderSize = 0;
            this.Computer_First_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Computer_First_button.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Computer_First_button.ForeColor = System.Drawing.SystemColors.Control;
            this.Computer_First_button.Location = new System.Drawing.Point(116, 582);
            this.Computer_First_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Computer_First_button.Name = "Computer_First_button";
            this.Computer_First_button.Size = new System.Drawing.Size(97, 34);
            this.Computer_First_button.TabIndex = 9;
            this.Computer_First_button.Text = "AI";
            this.Computer_First_button.UseVisualStyleBackColor = false;
            this.Computer_First_button.Click += new System.EventHandler(this.Computer_First_button_Click);
            // 
            // Player_First_button
            // 
            this.Player_First_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.Player_First_button.FlatAppearance.BorderSize = 0;
            this.Player_First_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Player_First_button.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player_First_button.ForeColor = System.Drawing.SystemColors.Control;
            this.Player_First_button.Location = new System.Drawing.Point(17, 582);
            this.Player_First_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Player_First_button.Name = "Player_First_button";
            this.Player_First_button.Size = new System.Drawing.Size(97, 34);
            this.Player_First_button.TabIndex = 8;
            this.Player_First_button.Text = "PLAYER";
            this.Player_First_button.UseVisualStyleBackColor = false;
            this.Player_First_button.Click += new System.EventHandler(this.Player_First_button_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CK_Music);
            this.tabPage2.Controls.Add(this.VolumeBar);
            this.tabPage2.Controls.Add(this.Volume_a);
            this.tabPage2.Controls.Add(this.Volume_s);
            this.tabPage2.Controls.Add(this.MediaPlayer);
            this.tabPage2.Controls.Add(this.CK_AIdebug);
            this.tabPage2.Controls.Add(this.Ck_monitor);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(616, 643);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Setting";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CK_Music
            // 
            this.CK_Music.AutoSize = true;
            this.CK_Music.Checked = true;
            this.CK_Music.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CK_Music.Depth = 0;
            this.CK_Music.Font = new System.Drawing.Font("Roboto", 10F);
            this.CK_Music.ForeColor = System.Drawing.Color.White;
            this.CK_Music.Location = new System.Drawing.Point(11, 110);
            this.CK_Music.Margin = new System.Windows.Forms.Padding(0);
            this.CK_Music.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CK_Music.MouseState = MaterialSkin.MouseState.HOVER;
            this.CK_Music.Name = "CK_Music";
            this.CK_Music.Ripple = true;
            this.CK_Music.Size = new System.Drawing.Size(69, 30);
            this.CK_Music.TabIndex = 14;
            this.CK_Music.Text = "BGM";
            this.CK_Music.UseVisualStyleBackColor = true;
            this.CK_Music.CheckedChanged += new System.EventHandler(this.CK_Music_CheckedChanged);
            // 
            // VolumeBar
            // 
            this.VolumeBar.Depth = 0;
            this.VolumeBar.Location = new System.Drawing.Point(11, 162);
            this.VolumeBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VolumeBar.MouseState = MaterialSkin.MouseState.HOVER;
            this.VolumeBar.Name = "VolumeBar";
            this.VolumeBar.Size = new System.Drawing.Size(592, 5);
            this.VolumeBar.TabIndex = 13;
            this.VolumeBar.Value = 50;
            // 
            // Volume_a
            // 
            this.Volume_a.AutoSize = true;
            this.Volume_a.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Volume_a.Depth = 0;
            this.Volume_a.Icon = null;
            this.Volume_a.Location = new System.Drawing.Point(492, 181);
            this.Volume_a.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Volume_a.MouseState = MaterialSkin.MouseState.HOVER;
            this.Volume_a.Name = "Volume_a";
            this.Volume_a.Primary = true;
            this.Volume_a.Size = new System.Drawing.Size(100, 36);
            this.Volume_a.TabIndex = 12;
            this.Volume_a.Text = "Volume+";
            this.Volume_a.UseVisualStyleBackColor = true;
            this.Volume_a.Click += new System.EventHandler(this.Volume_a_Click);
            // 
            // Volume_s
            // 
            this.Volume_s.AutoSize = true;
            this.Volume_s.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Volume_s.Depth = 0;
            this.Volume_s.Icon = null;
            this.Volume_s.Location = new System.Drawing.Point(11, 181);
            this.Volume_s.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Volume_s.MouseState = MaterialSkin.MouseState.HOVER;
            this.Volume_s.Name = "Volume_s";
            this.Volume_s.Primary = true;
            this.Volume_s.Size = new System.Drawing.Size(96, 36);
            this.Volume_s.TabIndex = 11;
            this.Volume_s.Text = "Volume-";
            this.Volume_s.UseVisualStyleBackColor = true;
            this.Volume_s.Click += new System.EventHandler(this.Volume_s_Click);
            // 
            // MediaPlayer
            // 
            this.MediaPlayer.Enabled = true;
            this.MediaPlayer.Location = new System.Drawing.Point(65, 313);
            this.MediaPlayer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MediaPlayer.Name = "MediaPlayer";
            this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
            this.MediaPlayer.Size = new System.Drawing.Size(315, 66);
            this.MediaPlayer.TabIndex = 10;
            this.MediaPlayer.TabStop = false;
            this.MediaPlayer.Visible = false;
            // 
            // CK_AIdebug
            // 
            this.CK_AIdebug.AutoSize = true;
            this.CK_AIdebug.Depth = 0;
            this.CK_AIdebug.Font = new System.Drawing.Font("Roboto", 10F);
            this.CK_AIdebug.ForeColor = System.Drawing.Color.White;
            this.CK_AIdebug.Location = new System.Drawing.Point(11, 59);
            this.CK_AIdebug.Margin = new System.Windows.Forms.Padding(0);
            this.CK_AIdebug.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CK_AIdebug.MouseState = MaterialSkin.MouseState.HOVER;
            this.CK_AIdebug.Name = "CK_AIdebug";
            this.CK_AIdebug.Ripple = true;
            this.CK_AIdebug.Size = new System.Drawing.Size(158, 30);
            this.CK_AIdebug.TabIndex = 9;
            this.CK_AIdebug.Text = "AI DEBUG MODE";
            this.CK_AIdebug.UseVisualStyleBackColor = true;
            this.CK_AIdebug.CheckedChanged += new System.EventHandler(this.CK_AIdebug_CheckedChanged);
            // 
            // Ck_monitor
            // 
            this.Ck_monitor.AutoSize = true;
            this.Ck_monitor.Checked = true;
            this.Ck_monitor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ck_monitor.Depth = 0;
            this.Ck_monitor.Font = new System.Drawing.Font("Roboto", 10F);
            this.Ck_monitor.ForeColor = System.Drawing.Color.White;
            this.Ck_monitor.Location = new System.Drawing.Point(11, 8);
            this.Ck_monitor.Margin = new System.Windows.Forms.Padding(0);
            this.Ck_monitor.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Ck_monitor.MouseState = MaterialSkin.MouseState.HOVER;
            this.Ck_monitor.Name = "Ck_monitor";
            this.Ck_monitor.Ripple = true;
            this.Ck_monitor.Size = new System.Drawing.Size(173, 30);
            this.Ck_monitor.TabIndex = 8;
            this.Ck_monitor.Text = "RAM/CPU Monitor";
            this.Ck_monitor.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "玩家先手",
            "电脑先手"});
            this.comboBox1.Location = new System.Drawing.Point(448, 48);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(129, 23);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "选择谁先手";
            this.comboBox1.Visible = false;
            // 
            // Timer
            // 
            this.Timer.Interval = 50;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // SystemMonitor
            // 
            this.SystemMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SystemMonitor.AutoSize = true;
            this.SystemMonitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.SystemMonitor.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SystemMonitor.ForeColor = System.Drawing.Color.White;
            this.SystemMonitor.Location = new System.Drawing.Point(423, 35);
            this.SystemMonitor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SystemMonitor.Name = "SystemMonitor";
            this.SystemMonitor.Size = new System.Drawing.Size(52, 25);
            this.SystemMonitor.TabIndex = 10;
            this.SystemMonitor.Text = "监控";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 792);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.SystemMonitor);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Sizable = false;
            this.Text = "Gobang AI";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private MaterialTabSelector materialTabSelector1;
        private MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        public static System.Windows.Forms.TextBox debugtext;
        private MaterialRaisedButton Retract_button;
        private MaterialRaisedButton Start_button;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Label SystemMonitor;
        private Button Computer_First_button;
        private Button Player_First_button;
        private Label Situation;
        private MaterialSkin.Controls.MaterialCheckBox Ck_monitor;
        public MaterialCheckBox CK_AIdebug;
        private AxWMPLib.AxWindowsMediaPlayer MediaPlayer;
        private MaterialProgressBar VolumeBar;
        private MaterialRaisedButton Volume_a;
        private MaterialRaisedButton Volume_s;
        public MaterialCheckBox CK_Music;
    }
}

