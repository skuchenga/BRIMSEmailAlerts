using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using BRIMS.EmailAlerts.BusinessLogic.Data;
using BRIMS.EmailAlerts.BusinessLogic.Email;
using BRIMS.EmailAlerts.BusinessLogic.Infrastructure;
using BRIMS.EmailAlerts.BusinessLogic.Configuration;
using BRIMS.EmailAlerts.BusinessLogic.System;
using BRIMS.EmailAlerts.Common.Utils;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.TransactionEmailAlerts
{
    public partial  class TransactionEmailAlertService : ITransactionEmailService
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
        public TransactionEmailAlertService(BRIMSObjectContext context)
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
                List<TransactionEmailAlert> trxMailAlerts = GetMailAlerts(ourBranchId);
               
                SendPurchaseNoteEmail(ourBranchId, trxMailAlerts); 
            }
            catch (Exception excp)
            {
                CommonHelper.SendMessages(excp.Message + " : Transaction Emails Service");
            }
        }

        private List<TransactionEmailAlert> GetMailAlerts(string ourBranchId)
        {
            try
            {

                if (string.IsNullOrEmpty(ourBranchId))
                {
                    ourBranchId = "001";
                }

                var mailAlerts = _context.proc_UTTrxEmailAlerts(ourBranchId).ToList();

                return mailAlerts;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void SendPurchaseNoteEmail(string ourBranchID,List<TransactionEmailAlert> emailAlerts)
        { 
            string strSendMail = string.Empty;
            string strReportPath = string.Empty;
            string strPDFDirPath = string.Empty;
            string strAccountIDS = string.Empty;

            try
            {
                tSystem tSystem = IoC.Resolve<ISystemService>().GetSystemByBranchId(ourBranchID);

                if (tSystem != null)
                {
                    strReportPath = tSystem.ReportPath;
                }

                if (string.IsNullOrEmpty(strReportPath))
                {
                    _items.Add("Report Path error. Please check.");
                    return;
                }

                if (!strReportPath.EndsWith("\\"))
                {
                    strReportPath = strReportPath + "\\";
                }

                strPDFDirPath = strReportPath + "PDFReports";

                if (!Directory.Exists(strPDFDirPath))
                {
                    Directory.CreateDirectory(strPDFDirPath);
                }

                string[] conProperties = Config.ConnectionString.Split(';');


                string EmailBody = string.Empty;

                if (emailAlerts != null)
                {
                    if (emailAlerts.Count > 0)
                    {
                        foreach (TransactionEmailAlert row in emailAlerts)
                        {


                            ReportDocument rpt = new ReportDocument();
                            string filePath = strReportPath + "rpt_FundsPurchaseNoteHist.rpt";
                            rpt.Load(filePath);

                            rpt.Refresh();
                            rpt.SetParameterValue("@OurBranchID", "001");
                            rpt.SetParameterValue("@ClientID", row.ClientID);
                            rpt.SetParameterValue("@TranID", row.TranID);
                            
                            ConnectionInfo connectionInfo = new ConnectionInfo();

                            foreach (string conProp in conProperties)
                            {
                                if (conProp.Contains("Data Source="))
                                {
                                    connectionInfo.ServerName = conProp.Replace("Data Source=", "").ToString();
                                }
                                else if (conProp.Contains("Initial Catalog="))
                                {
                                    connectionInfo.DatabaseName = conProp.Replace("Initial Catalog=", "").ToString();
                                }
                                else if (conProp.Contains("User ID="))
                                {
                                    connectionInfo.UserID = conProp.Replace("User ID=", "").ToString();
                                }
                                else if (conProp.Contains("Password="))
                                {
                                    connectionInfo.Password = conProp.Replace("Password=", "").ToString();
                                }

                            }

                            connectionInfo.IntegratedSecurity = false;

                            SetDBLogonForReport(connectionInfo, rpt);

                            SetDBLogonForSubreports(connectionInfo, rpt);

                            string fileName = strPDFDirPath + "\\" + row.AccountID + "_" + row.TranID + ".pdf";

                            ExportOptions CrExportOptions;
                            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                            CrDiskFileDestinationOptions.DiskFileName = fileName;
                            CrExportOptions = rpt.ExportOptions;
                            {
                                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                                CrExportOptions.FormatOptions = CrFormatTypeOptions;
                            }
                            rpt.Export();

                            //Email the report

                            EmailBody = EmailBody + "";
                            EmailBody = EmailBody + "";
                            EmailBody = EmailBody + "";
                            EmailBody = EmailBody + "";
                            EmailBody = EmailBody + "";
                            EmailBody = EmailBody + "";

                            string strEmailSignature = CommonHelper.EmailSignature();

                            if (!string.IsNullOrEmpty(strEmailSignature))
                            {
                                EmailBody = EmailBody + strEmailSignature;
                            }

                            MailMessage message = new MailMessage();

                            message.From = new MailAddress(ConfigurationManager.AppSettings["SenderemailID"].ToString());
                            List<string> emailIDs = new List<string>();

                            emailIDs = row.UTCEmail.Split(new char[] { '/' }).ToList();

                            foreach (string emailID in emailIDs)
                            {
                                message.To.Add(emailID);
                            }

                            message.Body = EmailBody.ToString();

                            message.Subject = "BRIMS Transaction for Ac " + row.ClientID + " " + row.UTCFirstName + " " +row.UTCLastName ;

                            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OutgoingMailServer"], int.Parse(ConfigurationManager.AppSettings["OutgoingMailServerPort"]));
                            NetworkCredential SMTPUserInfo = new NetworkCredential(ConfigurationManager.AppSettings["SenderemailID"], ConfigurationManager.AppSettings["SenderPassword"]);
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = SMTPUserInfo;
                            if (ConfigurationManager.AppSettings["EnableSSL"].ToString() == "1")
                            {
                                smtp.EnableSsl = true;
                            }
                            message.IsBodyHtml = true;

                            try
                            {
                                Attachment attachFile = new Attachment(fileName);
                                message.Attachments.Add(attachFile);
                                smtp.Send(message);
                            }
                            catch (Exception exc)
                            {
                                throw exc;
                            }
                            finally
                            {
                                message.Dispose();
                            }

                            //Send sms


                            File.Delete(fileName);

                            CommonHelper.SendMessages("Transaction reprort for Account ID : " + row.AccountID + "  sent to Client ID : " + row.ClientID + "  on  " + DateTime.Now.ToString());
                            //CommonHelper.SendMessages("Transaction reprort for Account ID : " + row.AccountID + "  sent to Client ID : " + row.ClientID + "  on  " + DateTime.Now.ToString());

                            //if (string.IsNullOrEmpty(strAccountIDS))
                            //{
                            //    strAccountIDS = row.TranID.ToString();
                            //}
                            //else
                            //{
                            //    strAccountIDS = strAccountIDS + "," + row.TranID.ToString();
                            //}


                            IoC.Resolve<IMailService>().UpdateSentEmailAlert(row.TranID.ToString());
                            
                        }

                        if (strAccountIDS.EndsWith(","))
                        {
                            strAccountIDS = strAccountIDS.Substring(0, strAccountIDS.Length - 1);
                        }

                    }

                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (!string.IsNullOrEmpty(strAccountIDS))
                {
                    IoC.Resolve<IMailService>().UpdateSentEmailAlert(strAccountIDS);                    
                }
            }

            
        }


        private void SetDBLogonForReport(ConnectionInfo myConnectionInfo, ReportDocument myReportDocument)
        {
            try
            {
                Tables myTables = myReportDocument.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table myTable in myTables)
                {
                    TableLogOnInfo myTableLogonInfo = myTable.LogOnInfo;
                    myTableLogonInfo.ConnectionInfo = myConnectionInfo;
                    myTable.ApplyLogOnInfo(myTableLogonInfo);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        private void SetDBLogonForSubreports(ConnectionInfo myConnectionInfo, ReportDocument myReportDocument)
        {
            try
            {
                foreach (ReportDocument subReport in myReportDocument.Subreports)
                {
                    foreach (Table myTable in subReport.Database.Tables)
                    {
                        TableLogOnInfo myTableLogonInfo = myTable.LogOnInfo;
                        myTableLogonInfo.ConnectionInfo = myConnectionInfo;
                        myTable.ApplyLogOnInfo(myTableLogonInfo);
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
