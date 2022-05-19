namespace BRIMS_Email_Alerts
{
    partial class EmailUtility
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblClose = new System.Windows.Forms.Label();
            this.tmrSP = new System.Windows.Forms.Timer(this.components);
            this.tabAppEmails = new System.Windows.Forms.TabPage();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.tabAppSettings = new System.Windows.Forms.TabPage();
            this.btnContinue = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkUseSSL = new System.Windows.Forms.CheckBox();
            this.txtOutPassword = new System.Windows.Forms.TextBox();
            this.txtOutEmail = new System.Windows.Forms.TextBox();
            this.txtOutPort = new System.Windows.Forms.TextBox();
            this.txtOutMailServer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.txtDatabaseServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabAppEmailsAlerts = new System.Windows.Forms.TabControl();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabAppEmails.SuspendLayout();
            this.tabAppSettings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabAppEmailsAlerts.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblClose
            // 
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblClose.Location = new System.Drawing.Point(566, -19);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(32, 21);
            this.lblClose.TabIndex = 5;
            // 
            // tmrSP
            // 
            this.tmrSP.Interval = 30000;
            // 
            // tabAppEmails
            // 
            this.tabAppEmails.Controls.Add(this.lstLog);
            this.tabAppEmails.Location = new System.Drawing.Point(4, 25);
            this.tabAppEmails.Name = "tabAppEmails";
            this.tabAppEmails.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppEmails.Size = new System.Drawing.Size(806, 339);
            this.tabAppEmails.TabIndex = 1;
            this.tabAppEmails.Text = "Email Alerts";
            this.tabAppEmails.UseVisualStyleBackColor = true;
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.BackColor = System.Drawing.Color.White;
            this.lstLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 16;
            this.lstLog.Location = new System.Drawing.Point(3, 0);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(800, 320);
            this.lstLog.TabIndex = 8;
            // 
            // tabAppSettings
            // 
            this.tabAppSettings.Controls.Add(this.btnContinue);
            this.tabAppSettings.Controls.Add(this.groupBox2);
            this.tabAppSettings.Controls.Add(this.groupBox1);
            this.tabAppSettings.Location = new System.Drawing.Point(4, 25);
            this.tabAppSettings.Name = "tabAppSettings";
            this.tabAppSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppSettings.Size = new System.Drawing.Size(806, 339);
            this.tabAppSettings.TabIndex = 0;
            this.tabAppSettings.Text = "App Settings";
            this.tabAppSettings.UseVisualStyleBackColor = true;
            // 
            // btnContinue
            // 
            this.btnContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.Location = new System.Drawing.Point(340, 295);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(107, 38);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "&Continue >>";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkUseSSL);
            this.groupBox2.Controls.Add(this.txtOutPassword);
            this.groupBox2.Controls.Add(this.txtOutEmail);
            this.groupBox2.Controls.Add(this.txtOutPort);
            this.groupBox2.Controls.Add(this.txtOutMailServer);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(790, 194);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Outgoing Email Settings";
            // 
            // chkUseSSL
            // 
            this.chkUseSSL.AutoSize = true;
            this.chkUseSSL.Location = new System.Drawing.Point(133, 130);
            this.chkUseSSL.Name = "chkUseSSL";
            this.chkUseSSL.Size = new System.Drawing.Size(104, 22);
            this.chkUseSSL.TabIndex = 8;
            this.chkUseSSL.Text = "Enable SSL";
            this.chkUseSSL.UseVisualStyleBackColor = true;
            // 
            // txtOutPassword
            // 
            this.txtOutPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutPassword.Location = new System.Drawing.Point(574, 83);
            this.txtOutPassword.Name = "txtOutPassword";
            this.txtOutPassword.PasswordChar = '*';
            this.txtOutPassword.Size = new System.Drawing.Size(202, 24);
            this.txtOutPassword.TabIndex = 7;
            this.txtOutPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOutPassword_KeyPress);
            this.txtOutPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOutPassword_KeyUp);
            // 
            // txtOutEmail
            // 
            this.txtOutEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutEmail.Location = new System.Drawing.Point(133, 83);
            this.txtOutEmail.Name = "txtOutEmail";
            this.txtOutEmail.Size = new System.Drawing.Size(294, 24);
            this.txtOutEmail.TabIndex = 6;
            // 
            // txtOutPort
            // 
            this.txtOutPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutPort.Location = new System.Drawing.Point(574, 25);
            this.txtOutPort.Name = "txtOutPort";
            this.txtOutPort.Size = new System.Drawing.Size(202, 24);
            this.txtOutPort.TabIndex = 5;
            // 
            // txtOutMailServer
            // 
            this.txtOutMailServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutMailServer.Location = new System.Drawing.Point(133, 25);
            this.txtOutMailServer.Name = "txtOutMailServer";
            this.txtOutMailServer.Size = new System.Drawing.Size(294, 24);
            this.txtOutMailServer.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(490, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 18);
            this.label6.TabIndex = 3;
            this.label6.Text = "Password :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "Email ID :";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(451, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Mail Server Port :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Mail Server: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDatabaseName);
            this.groupBox1.Controls.Add(this.txtDatabaseServer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(792, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Settings";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabaseName.Location = new System.Drawing.Point(576, 36);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(202, 24);
            this.txtDatabaseName.TabIndex = 3;
            // 
            // txtDatabaseServer
            // 
            this.txtDatabaseServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabaseServer.Location = new System.Drawing.Point(135, 33);
            this.txtDatabaseServer.Name = "txtDatabaseServer";
            this.txtDatabaseServer.Size = new System.Drawing.Size(294, 24);
            this.txtDatabaseServer.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(456, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database Server:";
            // 
            // tabAppEmailsAlerts
            // 
            this.tabAppEmailsAlerts.Controls.Add(this.tabAppSettings);
            this.tabAppEmailsAlerts.Controls.Add(this.tabAppEmails);
            this.tabAppEmailsAlerts.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabAppEmailsAlerts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabAppEmailsAlerts.Location = new System.Drawing.Point(0, 0);
            this.tabAppEmailsAlerts.Name = "tabAppEmailsAlerts";
            this.tabAppEmailsAlerts.SelectedIndex = 0;
            this.tabAppEmailsAlerts.Size = new System.Drawing.Size(814, 368);
            this.tabAppEmailsAlerts.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(702, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 33);
            this.button1.TabIndex = 9;
            this.button1.Text = "Clear Log";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 368);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 60);
            this.panel1.TabIndex = 10;
            // 
            // EmailUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 433);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabAppEmailsAlerts);
            this.Controls.Add(this.lblClose);
            this.Name = "EmailUtility";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BRIMS Email Utility";
            this.Load += new System.EventHandler(this.EmailUtility_Load);
            this.SizeChanged += new System.EventHandler(this.EmailUtility_SizeChanged);
            this.tabAppEmails.ResumeLayout(false);
            this.tabAppSettings.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabAppEmailsAlerts.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Timer tmrSP;
        private System.Windows.Forms.TabPage tabAppEmails;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.TabPage tabAppSettings;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkUseSSL;
        private System.Windows.Forms.TextBox txtOutPassword;
        private System.Windows.Forms.TextBox txtOutEmail;
        private System.Windows.Forms.TextBox txtOutPort;
        private System.Windows.Forms.TextBox txtOutMailServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.TextBox txtDatabaseServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabAppEmailsAlerts;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;


    }
}