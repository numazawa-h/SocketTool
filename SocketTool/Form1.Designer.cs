namespace SocketTool
{
    partial class Form1
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRemotePort1 = new System.Windows.Forms.TextBox();
            this.txtRemoteAddress1 = new System.Windows.Forms.TextBox();
            this.txtSelfPort1 = new System.Windows.Forms.TextBox();
            this.txtSelfAddress1 = new System.Windows.Forms.TextBox();
            this.rtxMsgList = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbxRemort1 = new System.Windows.Forms.ComboBox();
            this.chkListen1 = new System.Windows.Forms.CheckBox();
            this.chkConnect1 = new System.Windows.Forms.CheckBox();
            this.cbxSelf1 = new System.Windows.Forms.ComboBox();
            this.grp1 = new System.Windows.Forms.GroupBox();
            this.lbl_Connect = new System.Windows.Forms.Label();
            this.lbl_Accept = new System.Windows.Forms.Label();
            this.grp1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRemotePort1
            // 
            this.txtRemotePort1.Location = new System.Drawing.Point(244, 165);
            this.txtRemotePort1.Margin = new System.Windows.Forms.Padding(6);
            this.txtRemotePort1.Name = "txtRemotePort1";
            this.txtRemotePort1.Size = new System.Drawing.Size(130, 31);
            this.txtRemotePort1.TabIndex = 9;
            // 
            // txtRemoteAddress1
            // 
            this.txtRemoteAddress1.Location = new System.Drawing.Point(20, 165);
            this.txtRemoteAddress1.Margin = new System.Windows.Forms.Padding(6);
            this.txtRemoteAddress1.Name = "txtRemoteAddress1";
            this.txtRemoteAddress1.Size = new System.Drawing.Size(212, 31);
            this.txtRemoteAddress1.TabIndex = 10;
            // 
            // txtSelfPort1
            // 
            this.txtSelfPort1.Location = new System.Drawing.Point(244, 71);
            this.txtSelfPort1.Margin = new System.Windows.Forms.Padding(6);
            this.txtSelfPort1.Name = "txtSelfPort1";
            this.txtSelfPort1.Size = new System.Drawing.Size(130, 31);
            this.txtSelfPort1.TabIndex = 6;
            // 
            // txtSelfAddress1
            // 
            this.txtSelfAddress1.Location = new System.Drawing.Point(20, 71);
            this.txtSelfAddress1.Margin = new System.Windows.Forms.Padding(6);
            this.txtSelfAddress1.Name = "txtSelfAddress1";
            this.txtSelfAddress1.Size = new System.Drawing.Size(212, 31);
            this.txtSelfAddress1.TabIndex = 7;
            // 
            // rtxMsgList
            // 
            this.rtxMsgList.Location = new System.Drawing.Point(33, 349);
            this.rtxMsgList.Name = "rtxMsgList";
            this.rtxMsgList.Size = new System.Drawing.Size(687, 237);
            this.rtxMsgList.TabIndex = 12;
            this.rtxMsgList.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1126, 445);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 53);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbxRemort1
            // 
            this.cbxRemort1.FormattingEnabled = true;
            this.cbxRemort1.Location = new System.Drawing.Point(20, 124);
            this.cbxRemort1.Name = "cbxRemort1";
            this.cbxRemort1.Size = new System.Drawing.Size(576, 32);
            this.cbxRemort1.TabIndex = 14;
            this.cbxRemort1.SelectedIndexChanged += new System.EventHandler(this.cbxRemort1_SelectedIndexChanged);
            // 
            // chkListen1
            // 
            this.chkListen1.AutoSize = true;
            this.chkListen1.Location = new System.Drawing.Point(501, 73);
            this.chkListen1.Name = "chkListen1";
            this.chkListen1.Size = new System.Drawing.Size(95, 28);
            this.chkListen1.TabIndex = 15;
            this.chkListen1.Text = "listen";
            this.chkListen1.UseVisualStyleBackColor = true;
            this.chkListen1.CheckedChanged += new System.EventHandler(this.chkListen1_CheckedChanged);
            // 
            // chkConnect1
            // 
            this.chkConnect1.AutoSize = true;
            this.chkConnect1.Location = new System.Drawing.Point(501, 165);
            this.chkConnect1.Name = "chkConnect1";
            this.chkConnect1.Size = new System.Drawing.Size(122, 28);
            this.chkConnect1.TabIndex = 16;
            this.chkConnect1.Text = "connect";
            this.chkConnect1.UseVisualStyleBackColor = true;
            this.chkConnect1.CheckedChanged += new System.EventHandler(this.chkConnect1_CheckedChanged);
            // 
            // cbxSelf1
            // 
            this.cbxSelf1.FormattingEnabled = true;
            this.cbxSelf1.Location = new System.Drawing.Point(20, 30);
            this.cbxSelf1.Name = "cbxSelf1";
            this.cbxSelf1.Size = new System.Drawing.Size(576, 32);
            this.cbxSelf1.TabIndex = 17;
            this.cbxSelf1.SelectedIndexChanged += new System.EventHandler(this.cbxSelf1_SelectedIndexChanged);
            // 
            // grp1
            // 
            this.grp1.Controls.Add(this.lbl_Connect);
            this.grp1.Controls.Add(this.lbl_Accept);
            this.grp1.Controls.Add(this.cbxSelf1);
            this.grp1.Controls.Add(this.chkConnect1);
            this.grp1.Controls.Add(this.txtSelfAddress1);
            this.grp1.Controls.Add(this.txtRemoteAddress1);
            this.grp1.Controls.Add(this.txtRemotePort1);
            this.grp1.Controls.Add(this.cbxRemort1);
            this.grp1.Controls.Add(this.chkListen1);
            this.grp1.Controls.Add(this.txtSelfPort1);
            this.grp1.Location = new System.Drawing.Point(33, 36);
            this.grp1.Name = "grp1";
            this.grp1.Size = new System.Drawing.Size(629, 224);
            this.grp1.TabIndex = 18;
            this.grp1.TabStop = false;
            this.grp1.Text = "１系";
            // 
            // lbl_Connect
            // 
            this.lbl_Connect.AutoSize = true;
            this.lbl_Connect.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Connect.Location = new System.Drawing.Point(385, 169);
            this.lbl_Connect.Name = "lbl_Connect";
            this.lbl_Connect.Size = new System.Drawing.Size(52, 21);
            this.lbl_Connect.TabIndex = 19;
            this.lbl_Connect.Text = "切断";
            // 
            // lbl_Accept
            // 
            this.lbl_Accept.AutoSize = true;
            this.lbl_Accept.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Accept.Location = new System.Drawing.Point(385, 77);
            this.lbl_Accept.Name = "lbl_Accept";
            this.lbl_Accept.Size = new System.Drawing.Size(52, 21);
            this.lbl_Accept.TabIndex = 18;
            this.lbl_Accept.Text = "切断";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1670, 762);
            this.Controls.Add(this.grp1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtxMsgList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grp1.ResumeLayout(false);
            this.grp1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtRemotePort1;
        private System.Windows.Forms.TextBox txtRemoteAddress1;
        private System.Windows.Forms.TextBox txtSelfPort1;
        private System.Windows.Forms.TextBox txtSelfAddress1;
        private System.Windows.Forms.RichTextBox rtxMsgList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbxRemort1;
        private System.Windows.Forms.CheckBox chkListen1;
        private System.Windows.Forms.CheckBox chkConnect1;
        private System.Windows.Forms.ComboBox cbxSelf1;
        private System.Windows.Forms.GroupBox grp1;
        private System.Windows.Forms.Label lbl_Connect;
        private System.Windows.Forms.Label lbl_Accept;
    }
}

