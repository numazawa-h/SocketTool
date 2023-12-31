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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtRemotePort = new System.Windows.Forms.TextBox();
            this.txtRemoteAddress = new System.Windows.Forms.TextBox();
            this.btnListen = new System.Windows.Forms.Button();
            this.txtSelfPort = new System.Windows.Forms.TextBox();
            this.txtSelfAddress = new System.Windows.Forms.TextBox();
            this.rtxMsgList = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(551, 174);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(162, 46);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtRemotePort
            // 
            this.txtRemotePort.Location = new System.Drawing.Point(289, 178);
            this.txtRemotePort.Margin = new System.Windows.Forms.Padding(6);
            this.txtRemotePort.Name = "txtRemotePort";
            this.txtRemotePort.Size = new System.Drawing.Size(212, 31);
            this.txtRemotePort.TabIndex = 9;
            // 
            // txtRemoteAddress
            // 
            this.txtRemoteAddress.Location = new System.Drawing.Point(33, 178);
            this.txtRemoteAddress.Margin = new System.Windows.Forms.Padding(6);
            this.txtRemoteAddress.Name = "txtRemoteAddress";
            this.txtRemoteAddress.Size = new System.Drawing.Size(212, 31);
            this.txtRemoteAddress.TabIndex = 10;
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(551, 124);
            this.btnListen.Margin = new System.Windows.Forms.Padding(6);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(162, 46);
            this.btnListen.TabIndex = 8;
            this.btnListen.Text = "listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // txtSelfPort
            // 
            this.txtSelfPort.Location = new System.Drawing.Point(289, 128);
            this.txtSelfPort.Margin = new System.Windows.Forms.Padding(6);
            this.txtSelfPort.Name = "txtSelfPort";
            this.txtSelfPort.Size = new System.Drawing.Size(212, 31);
            this.txtSelfPort.TabIndex = 6;
            // 
            // txtSelfAddress
            // 
            this.txtSelfAddress.Location = new System.Drawing.Point(33, 128);
            this.txtSelfAddress.Margin = new System.Windows.Forms.Padding(6);
            this.txtSelfAddress.Name = "txtSelfAddress";
            this.txtSelfAddress.Size = new System.Drawing.Size(212, 31);
            this.txtSelfAddress.TabIndex = 7;
            // 
            // rtxMsgList
            // 
            this.rtxMsgList.Location = new System.Drawing.Point(33, 243);
            this.rtxMsgList.Name = "rtxMsgList";
            this.rtxMsgList.Size = new System.Drawing.Size(687, 237);
            this.rtxMsgList.TabIndex = 12;
            this.rtxMsgList.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 548);
            this.Controls.Add(this.rtxMsgList);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtRemotePort);
            this.Controls.Add(this.txtRemoteAddress);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.txtSelfPort);
            this.Controls.Add(this.txtSelfAddress);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtRemotePort;
        private System.Windows.Forms.TextBox txtRemoteAddress;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.TextBox txtSelfPort;
        private System.Windows.Forms.TextBox txtSelfAddress;
        private System.Windows.Forms.RichTextBox rtxMsgList;
    }
}

