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


namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.RBALimitAlerts
{
    public partial class RBAMailAlertService : IRBALimitService
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
        public RBAMailAlertService(BRIMSObjectContext context)
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
                List<RBAMailAlert> rbaMailAlerts = GetMailAlerts(ourBranchId);
                SendRBAMailAlerts(rbaMailAlerts, ourBranchId);
            }
            catch (Exception excp)
            {
               CommonHelper.SendMessages(excp.Message + " : RBA Emails Service");
                
            }
        }

        private List<RBAMailAlert> GetMailAlerts(string ourBranchId)
        {
            try
            {
                if (string.IsNullOrEmpty(ourBranchId))
                {
                    ourBranchId = "001";
                }

                var mailAlerts = _context.Proc_MailAlerts(ourBranchId).ToList();

                return mailAlerts;
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        private void SendRBAMailAlerts(List<RBAMailAlert> emailAlerts, string ourBranchID)
        {

            try
            {

                string EmailBody = string.Empty;

                if (emailAlerts != null)
                {
                    if (emailAlerts.Count > 0)
                    {


                        EmailBody = EmailBody + "<HTML>";
                        EmailBody = EmailBody + "<BODY>";
                        EmailBody = EmailBody + "<TABLE width='100%' border='1' cellpadding='1' cellspacing='0' style='font-size:0.8em; font-family:Tahoma; vertical-align:top'>";

                        EmailBody = EmailBody + "<tr style=' background-color:Silver; font-weight: bold'>";
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "Client ID "; //Client ID 
                        EmailBody = EmailBody + "</TH>";
                        EmailBody = EmailBody + "<th align='left' style='width:20%; padding-left:5px'>";
                        EmailBody = EmailBody + "Client  Name"; //Client Name
                        EmailBody = EmailBody + "</th>";
                        EmailBody = EmailBody + "<th>";
                        EmailBody = EmailBody + " Asset Class"; //Asset Class
                        EmailBody = EmailBody + "</th>";
                        EmailBody = EmailBody + "<th>";
                        EmailBody = EmailBody + "Current Holding(%)"; //Current Holding
                        EmailBody = EmailBody + "</TH>";
                        //Client Limit
                        EmailBody = EmailBody + "<th colspan='2' style='width:12%'>";
                        EmailBody = EmailBody + "<table border='1' cellspacing='0' cellpadding='0px' style='font-size: inherit; width:100%; border-style: hidden;font-weight: bold; '>";
                        EmailBody = EmailBody + " <tr>";
                        EmailBody = EmailBody + "<td style='border-bottom-width: 1px;' colspan='2'>";
                        EmailBody = EmailBody + "<div style='width: 100%; padding-left: 5px; font-size: inherit; padding-bottom: 5px; text-align:center;'>";
                        EmailBody = EmailBody + "Client Limit(%)"; //Client Limit
                        EmailBody = EmailBody + "</div>";
                        EmailBody = EmailBody + "</td>";
                        EmailBody = EmailBody + "</tr>";
                        EmailBody = EmailBody + "<tr>";
                        EmailBody = EmailBody + "<td>Minimum&nbsp;</td>"; //Minimum
                        EmailBody = EmailBody + "<td>Maximum</td>"; //Maximum
                        EmailBody = EmailBody + "</tr>";
                        EmailBody = EmailBody + "</table>";
                        EmailBody = EmailBody + "</th>";

                        //Client Variance
                        EmailBody = EmailBody + "<th colspan='2' style='width:12%'>";
                        EmailBody = EmailBody + "<table border='1' cellspacing='0' cellpadding='0px' style='font-size: inherit; width:100%; border-style: hidden;font-weight: bold; '>";
                        EmailBody = EmailBody + " <tr>";
                        EmailBody = EmailBody + "<td style='border-bottom-width: 1px;' colspan='2'>";
                        EmailBody = EmailBody + "<div style='width: 100%; padding-left: 5px; font-size: inherit; padding-bottom: 5px; text-align:center;'>";
                        EmailBody = EmailBody + "Client Variance(%)"; //Client Variance
                        EmailBody = EmailBody + "</div>";
                        EmailBody = EmailBody + "</td>";
                        EmailBody = EmailBody + "</tr>";
                        EmailBody = EmailBody + "<tr>";
                        EmailBody = EmailBody + "<td>Minimum&nbsp;</td>"; //Minimum
                        EmailBody = EmailBody + "<td>Maximum</td>"; //Maximum
                        EmailBody = EmailBody + "</tr>";
                        EmailBody = EmailBody + "</table>";
                        EmailBody = EmailBody + "</th>";

                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "RBA Limit(%)"; //RBA Limit(%)
                        EmailBody = EmailBody + "</TH>";
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "RBA Variance(%)"; //RBA Variance(%)
                        EmailBody = EmailBody + "</TH>";
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "House View Allocation(%)"; //House View Allocation(%)
                        EmailBody = EmailBody + "</TH>";
                        EmailBody = EmailBody + "<TH>";
                        EmailBody = EmailBody + "House View Variance(%)"; //House View Variance(%)
                        EmailBody = EmailBody + "</TH>";
                        EmailBody = EmailBody + "</TR>";

                        foreach (RBAMailAlert row in emailAlerts)
                        {

                            EmailBody = EmailBody + "<tr>"; //TR3
                            EmailBody = EmailBody + "<td style='padding-left:2px'>";
                            EmailBody = EmailBody + row.ClientID;
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-left:5px'>";
                            EmailBody = EmailBody + row.ClientName;
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-left:5px'>";
                            EmailBody = EmailBody + row.SecurityName;
                            EmailBody = EmailBody + "</td>";
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            //Current Holding = FundPercentage
                            if (Convert.ToDouble(row.FundPercentage) > 0) { EmailBody = EmailBody + "&nbsp;"; }
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.FundPercentage.ToString()));
                            EmailBody = EmailBody + "</td>";
                            //Client Minimum Limit = MinLimit
                            EmailBody = EmailBody + "<td style='padding-right:2px' width: 6%' align='right'>";
                            if (Convert.ToDouble(row.MinLimit) > 0)
                            {
                                if (Convert.ToDouble(row.MinLimit) < 100)
                                {
                                    EmailBody = EmailBody + "&nbsp;&nbsp;";
                                }
                                else
                                {
                                    EmailBody = EmailBody + "&nbsp;";
                                }

                            }
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.MinLimit.ToString()));
                            EmailBody = EmailBody + "</td>";
                            //Client Maximum Limit = MaxLimit
                            EmailBody = EmailBody + "<td style='padding-right:2px' width: 6%' align='right'>";
                            if (Convert.ToDouble(row.MaxLimit) > 0)
                            {
                                if (Convert.ToDouble(row.MaxLimit) < 100)
                                {
                                    EmailBody = EmailBody + "&nbsp;&nbsp;";
                                }
                                else
                                {
                                    EmailBody = EmailBody + "&nbsp;";
                                }

                            }
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.MaxLimit.ToString()));
                            EmailBody = EmailBody + "</td>";

                            //Client Variance Minimum = MinLimitVariance
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.MinLimitVariance) > 0)
                            {
                                if (Convert.ToDouble(row.MinLimitVariance) < 100)
                                {
                                    EmailBody = EmailBody + "&nbsp;&nbsp;";
                                }
                                else
                                {
                                    EmailBody = EmailBody + "&nbsp;";
                                }

                            }
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.MinLimitVariance.ToString()));
                            EmailBody = EmailBody + "</td>";

                            //Client Variance Maximum = MaxLimitVariance
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.MaxLimitVariance) > 0)
                            {
                                if (Convert.ToDouble(row.MaxLimitVariance) < 100)
                                {
                                    EmailBody = EmailBody + "&nbsp;&nbsp;";
                                }
                                else
                                {
                                    EmailBody = EmailBody + "&nbsp;";
                                }

                            }
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.MaxLimitVariance.ToString()));
                            EmailBody = EmailBody + "</td>";

                            //RBA Limi = RBALimit
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.RBALimit) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.RBALimit.ToString()));
                            EmailBody = EmailBody + "</td>";


                            //RBA Limit variance = RBALimitVariance
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.RBALimitVariance) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.RBALimitVariance.ToString()));
                            EmailBody = EmailBody + "</td>";

                            //House view allocation = HVLimit
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.HVLimit) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.HVLimit.ToString()));
                            EmailBody = EmailBody + "</td>";

                            //House view variance = HVVariance
                            EmailBody = EmailBody + "<td style='padding-right:2px' align ='right'>";
                            if (Convert.ToDouble(row.HVVariance) > 0) { EmailBody = EmailBody + "&nbsp;"; };
                            EmailBody = EmailBody + string.Format("{0:0,0.00}", Convert.ToDouble(row.HVVariance.ToString()));
                            EmailBody = EmailBody + "</td>";

                            EmailBody = EmailBody + "</tr>";

                        }//for each loop



                        EmailBody = EmailBody + "</TABLE>";
                        EmailBody = EmailBody + "</BODY>";
                        EmailBody = EmailBody + "</HTML>";

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(ConfigurationManager.AppSettings["SenderemailID"].ToString());
                        message.Body = EmailBody.ToString();

                        message.Subject = "BRIMS email alert - RBA Variance";

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
                        CommonHelper.SendMessages("RBA Variance email alert, containing " + emailAlerts.Count.ToString() + " records sent on " + DateTime.Now.ToString());


                        IoC.Resolve<IMailService>().UpdateSentEmailAlert(0, "RBA", ourBranchID);


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
