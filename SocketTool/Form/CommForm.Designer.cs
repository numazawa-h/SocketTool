namespace SocketTool.CommForm
{
    partial class CommForm
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grp_Comm = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Remort_Health_Interval = new System.Windows.Forms.TextBox();
            this.chk_Remort_Health = new System.Windows.Forms.CheckBox();
            this.chk_Remort_Ack = new System.Windows.Forms.CheckBox();
            this.chk_Self_Ack = new System.Windows.Forms.CheckBox();
            this.lbl_Remote_Status = new System.Windows.Forms.Label();
            this.lbl_Self_Status = new System.Windows.Forms.Label();
            this.chk_Remort_AutoConnect = new System.Windows.Forms.CheckBox();
            this.txt_Self_IpAddress = new System.Windows.Forms.TextBox();
            this.txt_Remort_IpAddress = new System.Windows.Forms.TextBox();
            this.txt_Remort_PortNo = new System.Windows.Forms.TextBox();
            this.chk_Self_AutoConnect = new System.Windows.Forms.CheckBox();
            this.txt_Self_PortNo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtx_MsgList = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer_health = new System.Windows.Forms.Timer(this.components);
            this.grp_Comm.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_Comm
            // 
            this.grp_Comm.Controls.Add(this.label4);
            this.grp_Comm.Controls.Add(this.label3);
            this.grp_Comm.Controls.Add(this.label2);
            this.grp_Comm.Controls.Add(this.txt_Remort_Health_Interval);
            this.grp_Comm.Controls.Add(this.chk_Remort_Health);
            this.grp_Comm.Controls.Add(this.chk_Remort_Ack);
            this.grp_Comm.Controls.Add(this.chk_Self_Ack);
            this.grp_Comm.Controls.Add(this.lbl_Remote_Status);
            this.grp_Comm.Controls.Add(this.lbl_Self_Status);
            this.grp_Comm.Controls.Add(this.chk_Remort_AutoConnect);
            this.grp_Comm.Controls.Add(this.txt_Self_IpAddress);
            this.grp_Comm.Controls.Add(this.txt_Remort_IpAddress);
            this.grp_Comm.Controls.Add(this.txt_Remort_PortNo);
            this.grp_Comm.Controls.Add(this.chk_Self_AutoConnect);
            this.grp_Comm.Controls.Add(this.txt_Self_PortNo);
            this.grp_Comm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Comm.Location = new System.Drawing.Point(0, 0);
            this.grp_Comm.Name = "grp_Comm";
            this.grp_Comm.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.grp_Comm.Size = new System.Drawing.Size(620, 207);
            this.grp_Comm.TabIndex = 19;
            this.grp_Comm.TabStop = false;
            this.grp_Comm.Text = "２系";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(16, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 21);
            this.label4.TabIndex = 29;
            this.label4.Text = "受信側";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(16, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 21);
            this.label3.TabIndex = 28;
            this.label3.Text = "送信側";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(571, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 21);
            this.label2.TabIndex = 27;
            this.label2.Text = "秒";
            // 
            // txt_Remort_Health_Interval
            // 
            this.txt_Remort_Health_Interval.Location = new System.Drawing.Point(498, 117);
            this.txt_Remort_Health_Interval.Name = "txt_Remort_Health_Interval";
            this.txt_Remort_Health_Interval.Size = new System.Drawing.Size(67, 31);
            this.txt_Remort_Health_Interval.TabIndex = 26;
            // 
            // chk_Remort_Health
            // 
            this.chk_Remort_Health.AutoSize = true;
            this.chk_Remort_Health.Font = new System.Drawing.Font("ＭＳ ゴシック", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Remort_Health.Location = new System.Drawing.Point(277, 127);
            this.chk_Remort_Health.Name = "chk_Remort_Health";
            this.chk_Remort_Health.Size = new System.Drawing.Size(196, 27);
            this.chk_Remort_Health.TabIndex = 25;
            this.chk_Remort_Health.Text = "ヘルスチェック";
            this.chk_Remort_Health.UseVisualStyleBackColor = true;
            this.chk_Remort_Health.CheckedChanged += new System.EventHandler(this.chk_Remort_Health_CheckedChanged);
            // 
            // chk_Remort_Ack
            // 
            this.chk_Remort_Ack.AutoSize = true;
            this.chk_Remort_Ack.Font = new System.Drawing.Font("ＭＳ ゴシック", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Remort_Ack.Location = new System.Drawing.Point(115, 129);
            this.chk_Remort_Ack.Name = "chk_Remort_Ack";
            this.chk_Remort_Ack.Size = new System.Drawing.Size(130, 27);
            this.chk_Remort_Ack.TabIndex = 24;
            this.chk_Remort_Ack.Text = "肯定応答";
            this.chk_Remort_Ack.UseVisualStyleBackColor = true;
            // 
            // chk_Self_Ack
            // 
            this.chk_Self_Ack.AutoSize = true;
            this.chk_Self_Ack.Font = new System.Drawing.Font("ＭＳ ゴシック", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Self_Ack.Location = new System.Drawing.Point(115, 31);
            this.chk_Self_Ack.Name = "chk_Self_Ack";
            this.chk_Self_Ack.Size = new System.Drawing.Size(130, 27);
            this.chk_Self_Ack.TabIndex = 20;
            this.chk_Self_Ack.Text = "肯定応答";
            this.chk_Self_Ack.UseVisualStyleBackColor = true;
            // 
            // lbl_Remote_Status
            // 
            this.lbl_Remote_Status.AutoSize = true;
            this.lbl_Remote_Status.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Remote_Status.Location = new System.Drawing.Point(382, 169);
            this.lbl_Remote_Status.Name = "lbl_Remote_Status";
            this.lbl_Remote_Status.Size = new System.Drawing.Size(52, 21);
            this.lbl_Remote_Status.TabIndex = 19;
            this.lbl_Remote_Status.Text = "切断";
            // 
            // lbl_Self_Status
            // 
            this.lbl_Self_Status.AutoSize = true;
            this.lbl_Self_Status.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Self_Status.Location = new System.Drawing.Point(382, 77);
            this.lbl_Self_Status.Name = "lbl_Self_Status";
            this.lbl_Self_Status.Size = new System.Drawing.Size(52, 21);
            this.lbl_Self_Status.TabIndex = 18;
            this.lbl_Self_Status.Text = "切断";
            // 
            // chk_Remort_AutoConnect
            // 
            this.chk_Remort_AutoConnect.AutoSize = true;
            this.chk_Remort_AutoConnect.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Remort_AutoConnect.Location = new System.Drawing.Point(498, 165);
            this.chk_Remort_AutoConnect.Name = "chk_Remort_AutoConnect";
            this.chk_Remort_AutoConnect.Size = new System.Drawing.Size(86, 27);
            this.chk_Remort_AutoConnect.TabIndex = 16;
            this.chk_Remort_AutoConnect.Text = "接続";
            this.chk_Remort_AutoConnect.UseVisualStyleBackColor = true;
            this.chk_Remort_AutoConnect.CheckedChanged += new System.EventHandler(this.chk_Remote_AutoConnect_CheckedChanged);
            // 
            // txt_Self_IpAddress
            // 
            this.txt_Self_IpAddress.Location = new System.Drawing.Point(20, 71);
            this.txt_Self_IpAddress.Margin = new System.Windows.Forms.Padding(6);
            this.txt_Self_IpAddress.Name = "txt_Self_IpAddress";
            this.txt_Self_IpAddress.Size = new System.Drawing.Size(212, 31);
            this.txt_Self_IpAddress.TabIndex = 7;
            // 
            // txt_Remort_IpAddress
            // 
            this.txt_Remort_IpAddress.Location = new System.Drawing.Point(20, 165);
            this.txt_Remort_IpAddress.Margin = new System.Windows.Forms.Padding(6);
            this.txt_Remort_IpAddress.Name = "txt_Remort_IpAddress";
            this.txt_Remort_IpAddress.Size = new System.Drawing.Size(212, 31);
            this.txt_Remort_IpAddress.TabIndex = 10;
            // 
            // txt_Remort_PortNo
            // 
            this.txt_Remort_PortNo.Location = new System.Drawing.Point(244, 165);
            this.txt_Remort_PortNo.Margin = new System.Windows.Forms.Padding(6);
            this.txt_Remort_PortNo.Name = "txt_Remort_PortNo";
            this.txt_Remort_PortNo.Size = new System.Drawing.Size(130, 31);
            this.txt_Remort_PortNo.TabIndex = 9;
            // 
            // chk_Self_AutoConnect
            // 
            this.chk_Self_AutoConnect.AutoSize = true;
            this.chk_Self_AutoConnect.Font = new System.Drawing.Font("ＭＳ ゴシック", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Self_AutoConnect.Location = new System.Drawing.Point(498, 73);
            this.chk_Self_AutoConnect.Name = "chk_Self_AutoConnect";
            this.chk_Self_AutoConnect.Size = new System.Drawing.Size(86, 27);
            this.chk_Self_AutoConnect.TabIndex = 15;
            this.chk_Self_AutoConnect.Text = "接続";
            this.chk_Self_AutoConnect.UseVisualStyleBackColor = true;
            this.chk_Self_AutoConnect.CheckedChanged += new System.EventHandler(this.chk_Self_AutoConnect_CheckedChanged);
            // 
            // txt_Self_PortNo
            // 
            this.txt_Self_PortNo.Location = new System.Drawing.Point(244, 71);
            this.txt_Self_PortNo.Margin = new System.Windows.Forms.Padding(6);
            this.txt_Self_PortNo.Name = "txt_Self_PortNo";
            this.txt_Self_PortNo.Size = new System.Drawing.Size(130, 31);
            this.txt_Self_PortNo.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grp_Comm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 207);
            this.panel1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtx_MsgList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 207);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(20, 5, 20, 10);
            this.panel2.Size = new System.Drawing.Size(620, 586);
            this.panel2.TabIndex = 21;
            // 
            // rtx_MsgList
            // 
            this.rtx_MsgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtx_MsgList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rtx_MsgList.Location = new System.Drawing.Point(20, 5);
            this.rtx_MsgList.Name = "rtx_MsgList";
            this.rtx_MsgList.Size = new System.Drawing.Size(580, 571);
            this.rtx_MsgList.TabIndex = 13;
            this.rtx_MsgList.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.statusStrip);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 746);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(620, 47);
            this.panel3.TabIndex = 23;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 25);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(620, 22);
            this.statusStrip.TabIndex = 23;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 12);
            // 
            // timer_health
            // 
            this.timer_health.Enabled = true;
            this.timer_health.Interval = 1000;
            this.timer_health.Tick += new System.EventHandler(this.OnHealthCheckTimer);
            // 
            // CommForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(620, 0);
            this.Name = "CommForm";
            this.Size = new System.Drawing.Size(620, 793);
            this.Load += new System.EventHandler(this.CommForm_Load);
            this.grp_Comm.ResumeLayout(false);
            this.grp_Comm.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Comm;
        private System.Windows.Forms.Label lbl_Remote_Status;
        private System.Windows.Forms.Label lbl_Self_Status;
        private System.Windows.Forms.CheckBox chk_Remort_AutoConnect;
        private System.Windows.Forms.TextBox txt_Self_IpAddress;
        private System.Windows.Forms.TextBox txt_Remort_IpAddress;
        private System.Windows.Forms.TextBox txt_Remort_PortNo;
        private System.Windows.Forms.CheckBox chk_Self_AutoConnect;
        private System.Windows.Forms.TextBox txt_Self_PortNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtx_MsgList;
        private System.Windows.Forms.CheckBox chk_Self_Ack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Remort_Health_Interval;
        private System.Windows.Forms.CheckBox chk_Remort_Health;
        private System.Windows.Forms.CheckBox chk_Remort_Ack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Timer timer_health;
    }
}
