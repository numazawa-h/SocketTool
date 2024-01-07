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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbx_Self_Machine = new System.Windows.Forms.ComboBox();
            this.cbx_Remort_Machine = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.Refresh_timer = new System.Windows.Forms.Timer(this.components);
            this.chkAutoSend = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_AutoSend_start_Interval = new System.Windows.Forms.TextBox();
            this.AutoSend_timer = new System.Windows.Forms.Timer(this.components);
            this.commForm1 = new SocketTool.CommForm.CommForm();
            this.commForm2 = new SocketTool.CommForm.CommForm();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_AutoSend_Interval = new System.Windows.Forms.TextBox();
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
            this.splitContainer.MinimumSize = new System.Drawing.Size(1200, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.commForm1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.commForm2);
            this.splitContainer.Size = new System.Drawing.Size(1251, 763);
            this.splitContainer.SplitterDistance = 620;
            this.splitContainer.TabIndex = 19;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1273, 907);
            this.tabControl.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1257, 860);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "通信";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1251, 763);
            this.panel2.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_AutoSend_Interval);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_AutoSend_start_Interval);
            this.panel1.Controls.Add(this.chkAutoSend);
            this.panel1.Controls.Add(this.cbx_Self_Machine);
            this.panel1.Controls.Add(this.cbx_Remort_Machine);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1251, 91);
            this.panel1.TabIndex = 1;
            // 
            // cbx_Self_Machine
            // 
            this.cbx_Self_Machine.FormattingEnabled = true;
            this.cbx_Self_Machine.Location = new System.Drawing.Point(21, 41);
            this.cbx_Self_Machine.Name = "cbx_Self_Machine";
            this.cbx_Self_Machine.Size = new System.Drawing.Size(583, 32);
            this.cbx_Self_Machine.TabIndex = 16;
            this.cbx_Self_Machine.SelectedIndexChanged += new System.EventHandler(this.cbx_Self_Machine_SelectedIndexChanged);
            // 
            // cbx_Remort_Machine
            // 
            this.cbx_Remort_Machine.FormattingEnabled = true;
            this.cbx_Remort_Machine.Location = new System.Drawing.Point(634, 41);
            this.cbx_Remort_Machine.Name = "cbx_Remort_Machine";
            this.cbx_Remort_Machine.Size = new System.Drawing.Size(604, 32);
            this.cbx_Remort_Machine.TabIndex = 15;
            this.cbx_Remort_Machine.SelectedIndexChanged += new System.EventHandler(this.cbx_Remort_Machine_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1257, 860);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "電文";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Refresh_timer
            // 
            this.Refresh_timer.Interval = 5000;
            this.Refresh_timer.Tick += new System.EventHandler(this.Refresh_timer_Tick);
            // 
            // chkAutoSend
            // 
            this.chkAutoSend.AutoSize = true;
            this.chkAutoSend.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAutoSend.Location = new System.Drawing.Point(31, 8);
            this.chkAutoSend.Name = "chkAutoSend";
            this.chkAutoSend.Size = new System.Drawing.Size(130, 27);
            this.chkAutoSend.TabIndex = 17;
            this.chkAutoSend.Text = "自動送信";
            this.chkAutoSend.UseVisualStyleBackColor = true;
            this.chkAutoSend.CheckedChanged += new System.EventHandler(this.chk_AutoSend_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(276, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 21);
            this.label2.TabIndex = 29;
            this.label2.Text = "msec";
            // 
            // txt_AutoSend_start_Interval
            // 
            this.txt_AutoSend_start_Interval.Location = new System.Drawing.Point(177, 3);
            this.txt_AutoSend_start_Interval.Name = "txt_AutoSend_start_Interval";
            this.txt_AutoSend_start_Interval.Size = new System.Drawing.Size(84, 31);
            this.txt_AutoSend_start_Interval.TabIndex = 28;
            this.txt_AutoSend_start_Interval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Numric_KeyPress);
            // 
            // AutoSend_timer
            // 
            this.AutoSend_timer.Tick += new System.EventHandler(this.AutoSend_timer_Tick);
            // 
            // commForm1
            // 
            this.commForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commForm1.Location = new System.Drawing.Point(0, 0);
            this.commForm1.MinimumSize = new System.Drawing.Size(620, 0);
            this.commForm1.Name = "commForm1";
            this.commForm1.Size = new System.Drawing.Size(620, 763);
            this.commForm1.TabIndex = 0;
            // 
            // commForm2
            // 
            this.commForm2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commForm2.Location = new System.Drawing.Point(0, 0);
            this.commForm2.MinimumSize = new System.Drawing.Size(620, 0);
            this.commForm2.Name = "commForm2";
            this.commForm2.Size = new System.Drawing.Size(627, 763);
            this.commForm2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(438, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 21);
            this.label1.TabIndex = 31;
            this.label1.Text = "msec";
            // 
            // txt_AutoSend_Interval
            // 
            this.txt_AutoSend_Interval.Location = new System.Drawing.Point(339, 3);
            this.txt_AutoSend_Interval.Name = "txt_AutoSend_Interval";
            this.txt_AutoSend_Interval.Size = new System.Drawing.Size(84, 31);
            this.txt_AutoSend_Interval.TabIndex = 30;
            this.txt_AutoSend_Interval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Numric_KeyPress);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 907);
            this.Controls.Add(this.tabControl);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
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
        private System.Windows.Forms.CheckBox chkAutoSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_AutoSend_start_Interval;
        private System.Windows.Forms.Timer AutoSend_timer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_AutoSend_Interval;
    }
}

