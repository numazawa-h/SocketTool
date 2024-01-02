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
            this.grp_Comm = new System.Windows.Forms.GroupBox();
            this.lbl_Remote_Status = new System.Windows.Forms.Label();
            this.lbl_Self_Status = new System.Windows.Forms.Label();
            this.cbx_Self_Machine = new System.Windows.Forms.ComboBox();
            this.chk_Remote_AutoConnect = new System.Windows.Forms.CheckBox();
            this.txt_Self_IpAddress = new System.Windows.Forms.TextBox();
            this.txt_Remote_IpAddress = new System.Windows.Forms.TextBox();
            this.txt_Remote_PortNo = new System.Windows.Forms.TextBox();
            this.cbx_Remorte_Machine = new System.Windows.Forms.ComboBox();
            this.chk_Self_AutoConnect = new System.Windows.Forms.CheckBox();
            this.txt_Self_PortNo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtx_MsgList = new System.Windows.Forms.RichTextBox();
            this.grp_Comm.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_Comm
            // 
            this.grp_Comm.Controls.Add(this.lbl_Remote_Status);
            this.grp_Comm.Controls.Add(this.lbl_Self_Status);
            this.grp_Comm.Controls.Add(this.cbx_Self_Machine);
            this.grp_Comm.Controls.Add(this.chk_Remote_AutoConnect);
            this.grp_Comm.Controls.Add(this.txt_Self_IpAddress);
            this.grp_Comm.Controls.Add(this.txt_Remote_IpAddress);
            this.grp_Comm.Controls.Add(this.txt_Remote_PortNo);
            this.grp_Comm.Controls.Add(this.cbx_Remorte_Machine);
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
            // cbx_Self_Machine
            // 
            this.cbx_Self_Machine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbx_Self_Machine.FormattingEnabled = true;
            this.cbx_Self_Machine.Location = new System.Drawing.Point(17, 30);
            this.cbx_Self_Machine.Name = "cbx_Self_Machine";
            this.cbx_Self_Machine.Size = new System.Drawing.Size(573, 32);
            this.cbx_Self_Machine.TabIndex = 17;
            this.cbx_Self_Machine.SelectedIndexChanged += new System.EventHandler(this.cbx_Self_Machine_SelectedIndexChanged);
            // 
            // chk_Remote_AutoConnect
            // 
            this.chk_Remote_AutoConnect.AutoSize = true;
            this.chk_Remote_AutoConnect.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Remote_AutoConnect.Location = new System.Drawing.Point(498, 165);
            this.chk_Remote_AutoConnect.Name = "chk_Remote_AutoConnect";
            this.chk_Remote_AutoConnect.Size = new System.Drawing.Size(86, 27);
            this.chk_Remote_AutoConnect.TabIndex = 16;
            this.chk_Remote_AutoConnect.Text = "接続";
            this.chk_Remote_AutoConnect.UseVisualStyleBackColor = true;
            this.chk_Remote_AutoConnect.CheckedChanged += new System.EventHandler(this.chk_Remote_AutoConnect_CheckedChanged);
            // 
            // txt_Self_IpAddress
            // 
            this.txt_Self_IpAddress.Location = new System.Drawing.Point(20, 71);
            this.txt_Self_IpAddress.Margin = new System.Windows.Forms.Padding(6);
            this.txt_Self_IpAddress.Name = "txt_Self_IpAddress";
            this.txt_Self_IpAddress.Size = new System.Drawing.Size(212, 31);
            this.txt_Self_IpAddress.TabIndex = 7;
            // 
            // txt_Remote_IpAddress
            // 
            this.txt_Remote_IpAddress.Location = new System.Drawing.Point(20, 165);
            this.txt_Remote_IpAddress.Margin = new System.Windows.Forms.Padding(6);
            this.txt_Remote_IpAddress.Name = "txt_Remote_IpAddress";
            this.txt_Remote_IpAddress.Size = new System.Drawing.Size(212, 31);
            this.txt_Remote_IpAddress.TabIndex = 10;
            // 
            // txt_Remote_PortNo
            // 
            this.txt_Remote_PortNo.Location = new System.Drawing.Point(244, 165);
            this.txt_Remote_PortNo.Margin = new System.Windows.Forms.Padding(6);
            this.txt_Remote_PortNo.Name = "txt_Remote_PortNo";
            this.txt_Remote_PortNo.Size = new System.Drawing.Size(130, 31);
            this.txt_Remote_PortNo.TabIndex = 9;
            // 
            // cbx_Remorte_Machine
            // 
            this.cbx_Remorte_Machine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbx_Remorte_Machine.FormattingEnabled = true;
            this.cbx_Remorte_Machine.Location = new System.Drawing.Point(17, 124);
            this.cbx_Remorte_Machine.Name = "cbx_Remorte_Machine";
            this.cbx_Remorte_Machine.Size = new System.Drawing.Size(576, 32);
            this.cbx_Remorte_Machine.TabIndex = 14;
            this.cbx_Remorte_Machine.SelectedIndexChanged += new System.EventHandler(this.cbx_Remorte_Machine_SelectedIndexChanged);
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
            this.panel2.Padding = new System.Windows.Forms.Padding(20, 5, 10, 10);
            this.panel2.Size = new System.Drawing.Size(620, 586);
            this.panel2.TabIndex = 21;
            // 
            // rtx_MsgList
            // 
            this.rtx_MsgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtx_MsgList.Location = new System.Drawing.Point(20, 5);
            this.rtx_MsgList.Name = "rtx_MsgList";
            this.rtx_MsgList.Size = new System.Drawing.Size(590, 571);
            this.rtx_MsgList.TabIndex = 13;
            this.rtx_MsgList.Text = "";
            // 
            // CommForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Comm;
        private System.Windows.Forms.Label lbl_Remote_Status;
        private System.Windows.Forms.Label lbl_Self_Status;
        private System.Windows.Forms.ComboBox cbx_Self_Machine;
        private System.Windows.Forms.CheckBox chk_Remote_AutoConnect;
        private System.Windows.Forms.TextBox txt_Self_IpAddress;
        private System.Windows.Forms.TextBox txt_Remote_IpAddress;
        private System.Windows.Forms.TextBox txt_Remote_PortNo;
        private System.Windows.Forms.ComboBox cbx_Remorte_Machine;
        private System.Windows.Forms.CheckBox chk_Self_AutoConnect;
        private System.Windows.Forms.TextBox txt_Self_PortNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtx_MsgList;
    }
}
