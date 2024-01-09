namespace SocketTool
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.commForm1 = new SocketTool.CommForm.CommForm();
            this.commForm2 = new SocketTool.CommForm.CommForm();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_auto_response = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_auto_send_interval = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_auto_send_startInterval = new System.Windows.Forms.TextBox();
            this.chk_auto_send = new System.Windows.Forms.CheckBox();
            this.cbx_Self_Machine = new System.Windows.Forms.ComboBox();
            this.cbx_Remort_Machine = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.Refresh_timer = new System.Windows.Forms.Timer(this.components);
            this.AutoSend_timer = new System.Windows.Forms.Timer(this.components);
            this.chk_Ack_Not_Display = new System.Windows.Forms.CheckBox();
            this.chk_Scroll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.splitContainer.MinimumSize = new System.Drawing.Size(554, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.commForm1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.commForm2);
            this.splitContainer.Size = new System.Drawing.Size(578, 378);
            this.splitContainer.SplitterDistance = 286;
            this.splitContainer.SplitterWidth = 2;
            this.splitContainer.TabIndex = 19;
            // 
            // commForm1
            // 
            this.commForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commForm1.Location = new System.Drawing.Point(0, 0);
            this.commForm1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.commForm1.MinimumSize = new System.Drawing.Size(286, 0);
            this.commForm1.Name = "commForm1";
            this.commForm1.Size = new System.Drawing.Size(286, 378);
            this.commForm1.TabIndex = 0;
            // 
            // commForm2
            // 
            this.commForm2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commForm2.Location = new System.Drawing.Point(0, 0);
            this.commForm2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.commForm2.MinimumSize = new System.Drawing.Size(286, 0);
            this.commForm2.Name = "commForm2";
            this.commForm2.Size = new System.Drawing.Size(290, 378);
            this.commForm2.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(588, 454);
            this.tabControl.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tabPage1.Size = new System.Drawing.Size(580, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "通信";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 48);
            this.panel2.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(578, 378);
            this.panel2.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chk_Scroll);
            this.panel1.Controls.Add(this.chk_Ack_Not_Display);
            this.panel1.Controls.Add(this.chk_auto_response);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_auto_send_interval);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_auto_send_startInterval);
            this.panel1.Controls.Add(this.chk_auto_send);
            this.panel1.Controls.Add(this.cbx_Self_Machine);
            this.panel1.Controls.Add(this.cbx_Remort_Machine);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 46);
            this.panel1.TabIndex = 1;
            // 
            // chk_auto_response
            // 
            this.chk_auto_response.AutoSize = true;
            this.chk_auto_response.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_auto_response.Location = new System.Drawing.Point(256, 4);
            this.chk_auto_response.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.chk_auto_response.Name = "chk_auto_response";
            this.chk_auto_response.Size = new System.Drawing.Size(68, 15);
            this.chk_auto_response.TabIndex = 32;
            this.chk_auto_response.Text = "自動応答";
            this.chk_auto_response.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(202, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 11);
            this.label1.TabIndex = 31;
            this.label1.Text = "msec";
            // 
            // txt_auto_send_interval
            // 
            this.txt_auto_send_interval.Location = new System.Drawing.Point(156, 1);
            this.txt_auto_send_interval.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txt_auto_send_interval.Name = "txt_auto_send_interval";
            this.txt_auto_send_interval.Size = new System.Drawing.Size(41, 19);
            this.txt_auto_send_interval.TabIndex = 30;
            this.txt_auto_send_interval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Numric_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(127, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 11);
            this.label2.TabIndex = 29;
            this.label2.Text = "msec";
            // 
            // txt_auto_send_startInterval
            // 
            this.txt_auto_send_startInterval.Location = new System.Drawing.Point(82, 1);
            this.txt_auto_send_startInterval.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txt_auto_send_startInterval.Name = "txt_auto_send_startInterval";
            this.txt_auto_send_startInterval.Size = new System.Drawing.Size(41, 19);
            this.txt_auto_send_startInterval.TabIndex = 28;
            this.txt_auto_send_startInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Numric_KeyPress);
            // 
            // chk_auto_send
            // 
            this.chk_auto_send.AutoSize = true;
            this.chk_auto_send.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_auto_send.Location = new System.Drawing.Point(14, 4);
            this.chk_auto_send.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.chk_auto_send.Name = "chk_auto_send";
            this.chk_auto_send.Size = new System.Drawing.Size(68, 15);
            this.chk_auto_send.TabIndex = 17;
            this.chk_auto_send.Text = "自動送信";
            this.chk_auto_send.UseVisualStyleBackColor = true;
            this.chk_auto_send.CheckedChanged += new System.EventHandler(this.chk_AutoSend_CheckedChanged);
            // 
            // cbx_Self_Machine
            // 
            this.cbx_Self_Machine.FormattingEnabled = true;
            this.cbx_Self_Machine.Location = new System.Drawing.Point(10, 20);
            this.cbx_Self_Machine.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.cbx_Self_Machine.Name = "cbx_Self_Machine";
            this.cbx_Self_Machine.Size = new System.Drawing.Size(271, 20);
            this.cbx_Self_Machine.TabIndex = 16;
            this.cbx_Self_Machine.SelectedIndexChanged += new System.EventHandler(this.cbx_Self_Machine_SelectedIndexChanged);
            // 
            // cbx_Remort_Machine
            // 
            this.cbx_Remort_Machine.FormattingEnabled = true;
            this.cbx_Remort_Machine.Location = new System.Drawing.Point(293, 20);
            this.cbx_Remort_Machine.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.cbx_Remort_Machine.Name = "cbx_Remort_Machine";
            this.cbx_Remort_Machine.Size = new System.Drawing.Size(281, 20);
            this.cbx_Remort_Machine.TabIndex = 15;
            this.cbx_Remort_Machine.SelectedIndexChanged += new System.EventHandler(this.cbx_Remort_Machine_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tabPage2.Size = new System.Drawing.Size(580, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "電文";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 88);
            this.button1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Refresh_timer
            // 
            this.Refresh_timer.Interval = 5000;
            // 
            // AutoSend_timer
            // 
            this.AutoSend_timer.Tick += new System.EventHandler(this.AutoSend_timer_Tick);
            // 
            // chk_Ack_Not_Display
            // 
            this.chk_Ack_Not_Display.AutoSize = true;
            this.chk_Ack_Not_Display.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Ack_Not_Display.Location = new System.Drawing.Point(384, 2);
            this.chk_Ack_Not_Display.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.chk_Ack_Not_Display.Name = "chk_Ack_Not_Display";
            this.chk_Ack_Not_Display.Size = new System.Drawing.Size(101, 15);
            this.chk_Ack_Not_Display.TabIndex = 33;
            this.chk_Ack_Not_Display.Text = "肯定応答非表示";
            this.chk_Ack_Not_Display.UseVisualStyleBackColor = true;
            this.chk_Ack_Not_Display.CheckedChanged += new System.EventHandler(this.chk_Ack_Not_Display_CheckedChanged);
            // 
            // chk_Scroll
            // 
            this.chk_Scroll.AutoSize = true;
            this.chk_Scroll.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chk_Scroll.Location = new System.Drawing.Point(496, 1);
            this.chk_Scroll.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.chk_Scroll.Name = "chk_Scroll";
            this.chk_Scroll.Size = new System.Drawing.Size(66, 15);
            this.chk_Scroll.TabIndex = 34;
            this.chk_Scroll.Text = "スクロール";
            this.chk_Scroll.UseVisualStyleBackColor = true;
            this.chk_Scroll.CheckedChanged += new System.EventHandler(this.chk_Scroll_CheckedChanged);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 454);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "FormMain";
            this.Text = "RESCOP対向シミュレータ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbx_Remort_Machine;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbx_Self_Machine;
        private CommForm.CommForm commForm1;
        private CommForm.CommForm commForm2;
        private System.Windows.Forms.Timer Refresh_timer;
        private System.Windows.Forms.CheckBox chk_auto_send;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_auto_send_startInterval;
        private System.Windows.Forms.Timer AutoSend_timer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_auto_send_interval;
        private System.Windows.Forms.CheckBox chk_auto_response;
        private System.Windows.Forms.CheckBox chk_Scroll;
        private System.Windows.Forms.CheckBox chk_Ack_Not_Display;
    }
}

