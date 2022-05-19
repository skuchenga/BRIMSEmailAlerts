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

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.BankExposures
{
    public partial class BankExposureService : IBankExposureService
    {

        #region Fields

        /// <summary>
        /// Object context
        /// </summary>
        private readonly BRIMSObjectContext  _context;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public BankExposureService(BRIMSObjectContext context)
        {
            this._context = context;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Reindex tables
        /// </summary>
        public void ProcessEmailAlerts(string ourBranchId)
        {

            try
            {
                List<BankExposureMailAlert> bankExpMailAlerts = GetMailAlerts(ourBranchId);
                SendBankExposureMailAlerts(bankExpMailAlerts, ourBranchId);
            }
            catch (Exception excp)
            {
                CommonHelper.SendMessages(excp.Message + " : Bank Exposure Emails Service");
                
            }
        }


        private void SendBankExposureMailAlerts(List<BankExposureMailAlert> emailAlerts,string ourBranchID)
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
                        EmailBody = EmailBody + "<th style='width:30%'>";
                        EmailBody = EmailBody + "Bank  Name&nbsp;&nbsp;"; //Bank Name
                        EmailBody = EmailBody + "</th>";
                        EmailBody = EmailBody + "<th>";
                        EmailBody = EmailBody + "Limit Type"; //Limit Type
                        EmailBody = EmailBody + "</th>";
                        EmailBody = EmailBody + "<th>";
                        EmailBody = EmailBody + "Limit"; //Limit
                        EmailBody = EmailBody + "</TH>";
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "Deposits Amount"; //Deposits Amount
                        EmailBody = EmailBody + "</TH>";
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "Exposure(%)"; //Exposure
                        EmailBody = EmailBody + "</TH>";
                        EmailBody = EmailBody + "</TR>";

                        foreach (BankExposureMailAlert row in emailAlerts)
                        {

                            EmailBody = EmailBody + "<tr>"; //TR3
                            EmailBody = EmailBody + "<td style='padding-left:2px'>";
                            EmailBody = EmailBody + row.BankName.ToString();
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-left:2px'>";
                            EmailBody = EmailBody + row.LimitType.ToString();
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.Limit) > 0) { EmailBody = EmailBody + "&nbsp;"; }
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.Limit.ToString()));
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.DepositsHeld) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.DepositsHeld.ToString()));
                            EmailBody = EmailBody + "</td>";

                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.Variance) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.Variance.ToString()));
                            EmailBody = EmailBody + "</td>";


                            EmailBody = EmailBody + "</tr>";

                        }//for each loop



                        EmailBody = EmailBody + "</TABLE>";
                        EmailBody = EmailBody + "</BODY>";
                        EmailBody = EmailBody + "</HTML>";

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(ConfigurationManager.AppSettings["SenderemailID"].ToString());
                        message.Body = EmailBody.ToString();

                        message.Subject = "BRIMS email alert - Bank Exposure";

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
                        //message.To.Add("skuchenga@gmail.com");
                        //Get Mail Recepients
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
                        CommonHelper.SendMessages("Bank Exposure email alert, containing " + emailAlerts.Count.ToString() + " records sent on " + DateTime.Now.ToString());


                        IoC.Resolve<IMailService>().UpdateSentEmailAlert(0, "BANK", ourBranchID);
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private List<BankExposureMailAlert> GetMailAlerts(string ourBranchId)
        {
            try
            {
                if (string.IsNullOrEmpty(ourBranchId))
                {
                    ourBranchId = "001";
                }

                var mailAlerts = _context.proc_bankexposuremailalerts(ourBranchId).ToList();

                return mailAlerts;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion
    }
}
