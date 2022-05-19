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
using BRIMS.EmailAlerts.BusinessLogic.Alerts.UmbrellaAlerts;
using BRIMS.EmailAlerts.BusinessLogic.Email;
using BRIMS.EmailAlerts.BusinessLogic.Infrastructure;
using BRIMS.EmailAlerts.BusinessLogic.Configuration;
using BRIMS.EmailAlerts.BusinessLogic.System;
using BRIMS.EmailAlerts.Common.Utils;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.EntityModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.UmbrellaAlerts
{
    public partial class UmbrellaEmailService : IUmbrellaEmailService
	{
        #region Fields  

        /// <summary>
        /// Object Context
        /// </summary>
        private readonly BRIMSObjectContext _context;
        private List<string> _items = new List<string>();
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public UmbrellaEmailService(BRIMSObjectContext context)
        {
            this._context = context;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Process umbrella email alerts
        /// </summary>
        /// /// <param name="ourBranchID">Branch identifier</param>
        public void ProcessEmailAlerts(string ourBranchId)
        {
            try
            {
                List<BRIMS.EmailAlerts.BusinessLogic.Alerts.UmbrellaAlerts.UmbrellaEmailAlert>
                    UmbMailAlerts = GetMailAlerts(ourBranchId);
                SendUmbNoteEmail(ourBranchId, UmbMailAlerts);
            }
            catch (Exception excp)
            {
                CommonHelper.SendMessages(excp.Message + " : Umbrella Transaction Service");
            }
        }
        private List<UmbrellaEmailAlert> GetMailAlerts(string ourBranchId)
        {
            try
            {
                if (string.IsNullOrEmpty(ourBranchId))
                {
                    ourBranchId = "001";
                }
                

                string sp = "proc_UmbrellaAlerts";
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable d = new DataTable();

                cmd.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                cmd.CommandText = sp;
                cmd.Parameters.AddWithValue("@OurBranchId", "001");
                cmd.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = cmd;

                da.Fill(d);

                if (d.Rows.Count == 0)
                    return null;

                List<UmbrellaEmailAlert> umbList = new List<UmbrellaEmailAlert>();

                foreach (DataRow r in d.Rows)
                {
                    UmbrellaEmailAlert uAlert = new UmbrellaEmailAlert();

                    uAlert.ClientEmail = r["ClientEmail"].ToString();
                    uAlert.Date = DateTime.Parse(r["Date"].ToString());
                    uAlert.PhoneNumber = r["PhoneNumber"].ToString();
                    uAlert.SubClientCode = r["SubClientCode"].ToString();
                    uAlert.SubTypeID = r["SubTypeId"].ToString();
                    uAlert.TransDesc = r["TransDesc"].ToString();
                    uAlert.TransID = decimal.Parse(r["TransID"].ToString());

                    umbList.Add(uAlert);
                }


                /*var mailAlerts = _context.proc_UmbrellaAlerts(ourBranchId);                

                foreach (UmbrellaEmailAlert umbAlert in mailAlerts)
                {
                    umbList.Add(umbAlert);
                }*/

                return umbList;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        private void SendUmbNoteEmail(string OurBranchID, List<BRIMS.EmailAlerts.BusinessLogic.Alerts.UmbrellaAlerts.UmbrellaEmailAlert>
            umbrellaEmail)
        {
            string strSendMail = string.Empty;
            string strReportPath = string.Empty;
            string strPDFDirPath = string.Empty;
            string strAccountIDS = string.Empty;
    
            try
            {
                tSystem tSystem = IoC.Resolve<ISystemService>().GetSystemByBranchId(OurBranchID);
          
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

                if (umbrellaEmail != null)
                {
                    if (umbrellaEmail.Count > 0)
                    {
                        foreach (UmbrellaEmailAlert row in umbrellaEmail)
                        {
                            ReportDocument rpt = new ReportDocument();
                            string filepath = strReportPath + "rpt_UmbrellaNote.rpt";
                            rpt.Load(filepath);

                            rpt.Refresh();
                            rpt.SetParameterValue("@OurBranchID", "001");
                            rpt.SetParameterValue("@ClientID", row.SubClientCode);
                            rpt.SetParameterValue("@TransID", row.TransID);

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

                            string fileName = strPDFDirPath + "\\" + row.TransDesc + " " + row.TransID + ".pdf";

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

                            emailIDs = row.ClientEmail.Split(new char[] { '/' }).ToList();

                            foreach (string emailID in emailIDs)
                            {
                                message.To.Add(emailID);
                            }

                            message.Body = EmailBody.ToString();

                            message.Subject = "BRIMS Transaction for Ac " + row.TransDesc;

                            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["OutgoinMailServer"], int.Parse(ConfigurationManager.AppSettings["OutGoingMailServerPort"]));
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

                            //CommonHelper.SendMessages("Transaction reprort for Account ID : " + row.AccountID + "  sent to Client ID : " + row.ClientID + "  on  " + DateTime.Now.ToString());
                            //CommonHelper.SendMessages("Transaction reprort for Account ID : " + row.AccountID + "  sent to Client ID : " + row.ClientID + "  on  " + DateTime.Now.ToString());

                            //if (string.IsNullOrEmpty(strAccountIDS))
                            //{
                            //    strAccountIDS = row.TranID.ToString();
                            //}
                            //else
                            //{
                            //    strAccountIDS = strAccountIDS + "," + row.TranID.ToString();
                            //}

                            IoC.Resolve<IMailService>().UpdateSentEmailAlert(row.TransID.ToString());
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
		