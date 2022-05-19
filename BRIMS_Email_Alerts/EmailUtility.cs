using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using BRIMS.EmailAlerts.BusinessLogic.Configuration;
using BRIMS.EmailAlerts.BusinessLogic.Infrastructure;
using BRIMS.EmailAlerts.BusinessLogic.Tasks;
using BRIMS.EmailAlerts.Common.Utils;
using BRIMS.EmailAlerts.BusinessLogic.Alerts.BankExposures;
using BRIMS.EmailAlerts.BusinessLogic.Alerts.BrokerExposureAlerts;
using BRIMS.EmailAlerts.BusinessLogic.Alerts.RBALimitAlerts;
using BRIMS.EmailAlerts.BusinessLogic.Alerts.TransactionEmailAlerts;


namespace BRIMS_Email_Alerts
{
    public partial class EmailUtility : Form
    {

        static List<string> _items = new List<string>();
        private System.Timers.Timer tmrProcessAlerts = null;
        public delegate void ListBoxStringsConsumer(ListBox listbox, List<string> _List);  // defines a delegate type
        private bool _updateUI = false;


        public EmailUtility()
        {
            try
            {
                InitializeComponent();

                this.ControlBox = true;
                //this.MaximizeBox = false;

                LoadConfiguration();
                tabAppSettings.Select();
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }

        private void LoadConfiguration()
        {
            try
            {
                txtDatabaseServer.Text = ConfigurationManager.AppSettings["DBServer"];
                txtDatabaseName.Text = ConfigurationManager.AppSettings["DBName"];
                txtOutMailServer.Text = ConfigurationManager.AppSettings["OutgoingMailServer"];
                txtOutPort.Text = ConfigurationManager.AppSettings["OutgoingMailServerPort"];
                txtOutEmail.Text = ConfigurationManager.AppSettings["SenderemailID"];
                txtOutPassword.Text = ConfigurationManager.AppSettings["SenderPassword"];                
                chkUseSSL.Checked = false;

                if (ConfigurationManager.AppSettings["EnableSSL"].ToString() == "1")
                {
                    chkUseSSL.Checked = true;
                }
            }
            catch (Exception excp)
            {
                throw excp;
            }

        }

        private void StartApp()
        {
            try
            {

                _items.Add("Application started " + " on " + DateTime.Now.ToString());
                _items.Add("Waiting for the next available email alert....");
                lstLog.DataSource = null;
                lstLog.DataSource = _items;

                Config.Init();
                //initialize IoC
                IoC.InitializeWith(new DependencyResolverFactory());

                //initialize task manager
                TaskManager.Instance.Initialize(Config.ScheduleTasks);
                TaskManager.Instance.Start();

                SetTexts(lstLog, _items);

                tmrProcessAlerts = new System.Timers.Timer(100000);
                tmrProcessAlerts.SynchronizingObject = this;
                tmrProcessAlerts.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerEvent);
                tmrProcessAlerts.Start();

            }
            catch (Exception excp)
            {
                throw excp;
            }
        }

        public void SetTexts(ListBox listbox, List<string> texts)
        {
            try
            {
                if (listbox.InvokeRequired)
                {
                    listbox.Invoke(new ListBoxStringsConsumer(SetTexts), new object[] { listbox, texts });  // invoking itself
                }
                else
                {
                    if (!_updateUI)
                    {
                        _items[_items.Count - 1] = "Waiting for new email alert as at " + DateTime.Now.ToString();
                    }
                    else
                    {
                        _items.Add("Waiting for new email alert as at " + DateTime.Now.ToString());
                    }
                    listbox.DataSource = null;
                    listbox.DataSource = texts;
                    _updateUI = false;

                }
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {

            try
            {
                List<string> lstMessages = CommonHelper.ReceiveMessages();

                if (lstMessages.Count > 0)
                {
                    _updateUI = true;

                    foreach (string lItem in lstMessages)
                    {
                        _items.Add(lItem);
                    }
                }

                SetTexts(lstLog, _items);
            }
            catch (Exception excp)
            {
                throw excp;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtDatabaseServer.Text.Trim()))
                {

                    MessageBox.Show("Please enter the dabase server name");
                    return;
                }

                if (string.IsNullOrEmpty(txtDatabaseName.Text.Trim()))
                {

                    MessageBox.Show("Please enter the dabase name");
                    return;
                }
                if (string.IsNullOrEmpty(txtOutMailServer.Text.Trim()))
                {

                    MessageBox.Show("Please enter the outgoing server");
                    return;
                }
                if (string.IsNullOrEmpty(txtOutPort.Text.Trim()))
                {

                    MessageBox.Show("Please enter the Outgoing Mail Server Port");
                    return;
                }
                if (string.IsNullOrEmpty(txtOutEmail.Text.Trim()))
                {

                    MessageBox.Show("Please enter the Outgoing email ID.");
                    return;
                }
                if (string.IsNullOrEmpty(txtOutPassword.Text.Trim()))
                {

                    MessageBox.Show("Please enter the Outgoing email Password.");
                    return;
                }


                CommonHelper.UpdateSetting("DBServer", txtDatabaseServer.Text);
                CommonHelper.UpdateSetting("DBName", txtDatabaseName.Text);
                CommonHelper.UpdateSetting("OutgoingMailServer", txtOutMailServer.Text);
                CommonHelper.UpdateSetting("OutgoingMailServerPort", txtOutPort.Text);
                CommonHelper.UpdateSetting("SenderemailID", txtOutEmail.Text);
                CommonHelper.UpdateSetting("SenderPassword", txtOutPassword.Text);
                if (chkUseSSL.Checked)
                {
                    CommonHelper.UpdateSetting("EnableSSL", "1");
                }
                else
                {
                    CommonHelper.UpdateSetting("EnableSSL", "0");
                }


                //test db connection
                bool enableSSL = false;
                if (ConfigurationManager.AppSettings["EnableSSL"].ToString() == "1")
                {
                    enableSSL = true;   
                }
                string testEmail = CommonHelper.TestEmailConnection(ConfigurationManager.AppSettings["OutgoingMailServer"],
                        ConfigurationManager.AppSettings["SenderemailID"],
                        ConfigurationManager.AppSettings["OutgoingMailServerPort"],
                        ConfigurationManager.AppSettings["SenderPassword"],
                        "BRIMS Email Alert System",
                        "This is an automated email from the BRIMS email alert application", enableSSL);

                if (!string.IsNullOrEmpty(testEmail))
                {
                    MessageBox.Show(testEmail);
                    return;
                }

                //test db connection
                string strDBConnectionStatus = CommonHelper.CheckDbConnection();

                if (!string.IsNullOrEmpty(strDBConnectionStatus))
                {
                    MessageBox.Show(strDBConnectionStatus);
                    return;
                }

                tabAppEmailsAlerts.SelectTab(tabAppEmails);
                StartApp();


            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private void EmailUtility_Load(object sender, EventArgs e)
        {

        }

        private void EmailUtility_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                tabAppEmailsAlerts.Height = this.Height - panel1.Height - 50;
                this.Invalidate();
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                tabAppEmailsAlerts.Height = 368;
                this.Invalidate();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(lstLog.Items.Count > 0)
            {
                DialogResult d = MessageBox.Show("Are ou sure you want to clear all the available logs?",
                    "Confirm Clearance", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (d == DialogResult.Yes)
                {
                    lstLog.DataSource=null;
                }                
            }
        }

        private void txtOutPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtOutPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tabAppEmailsAlerts.SelectTab(tabAppEmails);
                StartApp();
            }
        }
    }
}
