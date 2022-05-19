using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using BRIMS.EmailAlerts.BusinessLogic.Data;
using BRIMS.EmailAlerts.BusinessLogic.Email;
using BRIMS.EmailAlerts.BusinessLogic.Infrastructure;
using BRIMS.EmailAlerts.Common.Utils;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.BrokerExposureAlerts
{
    public partial class BrokerExposureService : IBrokerExposureService
    {
         #region Fields

        /// <summary>
        /// Object context
        /// </summary>
        private readonly BRIMSObjectContext  _context;

        private List<string> _items = new List<string>();

        #endregion

         #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public BrokerExposureService(BRIMSObjectContext context)
        {
            this._context = context;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Processes email alerts
        /// </summary>
        /// /// <param name="ourBranchId">Branch identifier</param>
        public void ProcessEmailAlerts(string ourBranchId)
        {

            try
            {
                List<BrokerExposureMailAlert> bankExpMailAlerts = GetMailAlerts(ourBranchId);

                SendMailAlerts(bankExpMailAlerts, ourBranchId);
            }
            catch (Exception excp)
            {
                CommonHelper.SendMessages(excp.Message + " : Broker Exposure Emails Service");
                
            }

        }


        private List<BrokerExposureMailAlert> GetMailAlerts(string ourBranchId)
        {
            try
            {
                if (string.IsNullOrEmpty(ourBranchId))
                {
                    ourBranchId = "001";
                }

                var mailAlerts = _context.proc_brokerexposuremailalerts(ourBranchId).ToList();

                return mailAlerts;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void SendMailAlerts(List<BrokerExposureMailAlert> emailAlerts, string ourBranchID)
        {
            string EmailBody = string.Empty;

            try
            {
                if (emailAlerts != null)
                {
                    if (emailAlerts.Count > 0)
                    {
                        EmailBody = EmailBody + "<HTML>";
                        EmailBody = EmailBody + "<BODY>";
                        EmailBody = EmailBody + "<TABLE width='100%' border='1' cellpadding='1' cellspacing='0' style='font-size:0.8em; font-family:Tahoma; vertical-align:top'>";

                        EmailBody = EmailBody + "<TR style=' background-color:Silver'>";

                        //Broker Name
                        EmailBody = EmailBody + "<th style='width:30%'>";
                        EmailBody = EmailBody + "Broker  Name";
                        EmailBody = EmailBody + "</th>";

                        //Transaction Amount
                        EmailBody = EmailBody + "<th>";
                        EmailBody = EmailBody + "Transaction Amount";
                        EmailBody = EmailBody + "</th>";

                        //Commission
                        EmailBody = EmailBody + "<th>";
                        EmailBody = EmailBody + "Commission";
                        EmailBody = EmailBody + "</TH>";

                        //Total
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "Total"; //
                        EmailBody = EmailBody + "</TH>";

                        //Actual
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "Actual";
                        EmailBody = EmailBody + "</TH>";

                        //Lower Limit
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "Lower Limit";
                        EmailBody = EmailBody + "</TH>";

                        //Lower Variance
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "Lower Variance";
                        EmailBody = EmailBody + "</TH>";

                        //Upper Limit
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "Upper Limit";
                        EmailBody = EmailBody + "</TH>";

                        //Upper Variance
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "Upper Variance";
                        EmailBody = EmailBody + "</TH>";
                        EmailBody = EmailBody + "</TR>";

                        foreach (BrokerExposureMailAlert row in emailAlerts)
                        {

                            EmailBody = EmailBody + "<tr>"; //TR2
                            EmailBody = EmailBody + "<td style='padding-left:2px'>";
                            EmailBody = EmailBody + row.BrokerName;
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.TransactionAmount) > 0) { EmailBody = EmailBody + "&nbsp;"; }
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.TransactionAmount.ToString()));
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.Commission) > 0) { EmailBody = EmailBody + "&nbsp;"; }
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.Commission.ToString()));
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.BrokerAmount) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.BrokerAmount.ToString()));
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.ActualPercentage) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.ActualPercentage.ToString()));
                            EmailBody = EmailBody + "</td>";

                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.LowerLimit) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.LowerLimit.ToString()));
                            EmailBody = EmailBody + "</td>";

                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.LowerLimitVariancepercentage) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.LowerLimitVariancepercentage.ToString()));
                            EmailBody = EmailBody + "</td>";

                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.Limit) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.Limit.ToString()));
                            EmailBody = EmailBody + "</td>";

                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.VariancePercentage) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.VariancePercentage.ToString()));
                            EmailBody = EmailBody + "</td>";

                            EmailBody = EmailBody + "</tr>";

                        }//for each loop

                        EmailBody = EmailBody + "</TABLE>";
                        EmailBody = EmailBody + "</BODY>";
                        EmailBody = EmailBody + "</HTML>";

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(ConfigurationManager.AppSettings["SenderemailID"].ToString());
                        message.Body = EmailBody.ToString();

                        message.Subject = "BRIMS email alert - Broker Exposure";

                        SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OutgoingMailServer"], int.Parse(ConfigurationManager.AppSettings["OutgoingMailServerPort"]));
                        NetworkCredential SMTPUserInfo = new NetworkCredential(ConfigurationManager.AppSettings["SenderemailID"], ConfigurationManager.AppSettings["SenderPassword"]);
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = SMTPUserInfo;
                        if (ConfigurationManager.AppSettings["EnableSSL"].ToString() == "1")
                        {
                            smtp.EnableSsl = true;
                        }
                        message.IsBodyHtml = true;



                        //message.To.Add("stephen.kuchenga@craftsilicon.com");
                        List<BRIMSMail> emailRecipients = IoC.Resolve<IMailService>().GetMailRecipients();

                        if (emailRecipients != null)
                        {
                            foreach (BRIMSMail eAddress in emailRecipients)
                            {
                                message.To.Add(eAddress.EmailID);
                            }
                        }
                        smtp.Send(message);

                        //Updte UI
                        CommonHelper.SendMessages("Broker Exposure email alert, containing " + emailAlerts.Count.ToString() + " records sent on " + DateTime.Now.ToString());
                                                
                        //Update table
                        IoC.Resolve<IMailService>().UpdateSentEmailAlert(0, "BROKER", ourBranchID);
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion
    }
}
