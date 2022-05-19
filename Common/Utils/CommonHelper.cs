using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Net.Configuration;
using System.Data.SqlClient; 

namespace BRIMS.EmailAlerts.Common.Utils
{
    public partial class CommonHelper
    {
        public static string dbServer { get; set; }
        public static string dbName { get; set; }
        public static string dbUser { get; set; }
        public static string dbPWD { get; set; }

        public string[] ConnectionProperties { get; set; }

        static List<string> _items = new List<string>();

        public static void SendMessages(string _item)
        {
            lock (_items)
            {
                _items.Add(_item);
            }
        }

        public static List<string> ReceiveMessages()
        {
            List<string> messages = new List<string>();
            lock (_items)
            {

                foreach (string msg in _items)
                {
                    messages.Add(msg);
                }
                _items.Clear();
            }

            return messages;
        }


        public static string GetConnectionString()
        {
            SetConnectionProperties();

            //string connString = "user id=" + dbUser + ";password=" + DecryptItem(dbPWD) + ";server=" + DecryptItem(dbServer) + ";Trusted_Connection=no;database=" + dbName + ";connection timeout=300";
            string connString = "Data Source=" + dbServer + "; Initial Catalog = " + dbName + ";User ID=" + dbUser + ";Password=" + DecryptItem(dbPWD);
            var builder = new SqlConnectionStringBuilder(connString);
            builder.PersistSecurityInfo = true;
            builder.ConnectTimeout = 360000;
            builder.MultipleActiveResultSets = true;
            builder.MinPoolSize = 5;
            builder.Pooling = true;

            return builder.ToString();
        }

        public static void SetConnectionProperties()
        {

            dbServer = ConfigurationManager.AppSettings["DBServer"];
            dbName = ConfigurationManager.AppSettings["DBName"];
            dbUser = ConfigurationManager.AppSettings["DBUserID"];
            dbPWD = ConfigurationManager.AppSettings["DBUserPWD"];
        }


         
        /// <summary>
        /// Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(email))
                return result;
            email = email.Trim();
            result = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }

        public static string DecryptItem(string StringToDecrypt)
        {
            string SeedValue = "0123456789ABCDEF";
            
            String Temp = "";

            int i, dblCountLength;
            int intLengthChar, intCountChar, intRandomSeed, intBeforeMulti, intAfterMulti, intSubNinetyNine, intInverseAsc;
            double dblCurrentChar;
            String strCurrentChar, Decrypt = "";

            if (SeedValue != "0123456789ABCDEF")
                return "";

            try
            {
                for (dblCountLength = 0; dblCountLength < StringToDecrypt.Length; dblCountLength++)
                {
                    intLengthChar = System.Convert.ToInt16(StringToDecrypt.Substring(dblCountLength, 1));
                    strCurrentChar = StringToDecrypt.Substring(dblCountLength + 1, intLengthChar);
                    dblCurrentChar = 0;

                    for (intCountChar = 0; intCountChar < strCurrentChar.Length; intCountChar++)
                    {
                        double x = 93, y = System.Convert.ToDouble(strCurrentChar.Length - intCountChar - 1);
                        dblCurrentChar = dblCurrentChar + (strCurrentChar[intCountChar] - 33) * (Math.Pow(x, y));
                    }
                    intRandomSeed = System.Convert.ToInt16(dblCurrentChar.ToString().Substring(2, 2));

                    intBeforeMulti = System.Convert.ToInt32(dblCurrentChar.ToString().Substring(0, 2) + dblCurrentChar.ToString().Substring(4, 2));
                    intAfterMulti = intBeforeMulti / intRandomSeed;
                    intSubNinetyNine = intAfterMulti - 99;
                    intInverseAsc = 256 - intSubNinetyNine;
                    Decrypt = Decrypt + System.Convert.ToChar(intInverseAsc).ToString();
                    dblCountLength = dblCountLength + intLengthChar;
                }

                for (i = 0; i < Decrypt.Length; i++)
                {
                    char c = (char)((int)(Decrypt[i] - (Decrypt.Length + i + 1)));
                    Temp = Temp + c.ToString();
                }
            }
            catch (Exception se)
            {
                throw se;
            }
            Decrypt = Temp;
            return Decrypt;
        }

        /// <summary>
        /// Modifies query string
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="queryStringModification">Query string modification</param>
        /// <param name="targetLocationModification">Target location modification</param>
        /// <returns>New url</returns>
        public static string ModifyQueryString(string url, string queryStringModification, string targetLocationModification)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryStringModification == null)
                queryStringModification = string.Empty;
            queryStringModification = queryStringModification.ToLowerInvariant();

            if (targetLocationModification == null)
                targetLocationModification = string.Empty;
            targetLocationModification = targetLocationModification.ToLowerInvariant();


            string str = string.Empty;
            string str2 = string.Empty;
            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#") + 1);
                url = url.Substring(0, url.IndexOf("#"));
            }
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryStringModification))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new char[] { '=' });
                            if (strArray.Length == 2)
                            {
                                dictionary[strArray[0]] = strArray[1];
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    foreach (string str4 in queryStringModification.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str4))
                        {
                            string[] strArray2 = str4.Split(new char[] { '=' });
                            if (strArray2.Length == 2)
                            {
                                dictionary[strArray2[0]] = strArray2[1];
                            }
                            else
                            {
                                dictionary[str4] = null;
                            }
                        }
                    }
                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
                else
                {
                    str = queryStringModification;
                }
            }
            if (!string.IsNullOrEmpty(targetLocationModification))
            {
                str2 = targetLocationModification;
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();
        }

        public static string CheckFilePath(string fPath)
        {
            string myPath = fPath;

            if (string.IsNullOrEmpty(myPath))
            {
                return string.Empty;//string.IsNullOrEmpty();
            }

            if (!myPath.EndsWith("\\"))
            {
                myPath += "\\";
            }

            return myPath;
        }



        public static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static bool TestConnection(Configuration config)
        {
            MailSettingsSectionGroup mailSettings = config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            if (mailSettings == null)
            {
                throw new ConfigurationErrorsException("The system.net/mailSettings configuration section group could not be read.");
            }
            return TestConnection(mailSettings.Smtp.Network.Host, mailSettings.Smtp.Network.Port);
        }

        /// <summary>
        /// test the smtp connection by sending a HELO command
        /// </summary>
        /// <param name="smtpServerAddress"></param>
        /// <param name="port"></param>
        public static bool TestConnection(string smtpServerAddress, int port)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(smtpServerAddress);
            IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], port);
            using (Socket tcpSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                //try to connect and test the rsponse for code 220 = success
                tcpSocket.Connect(endPoint);
                if (!CheckResponse(tcpSocket, 220))
                {
                    return false;
                }

                // send HELO and test the response for code 250 = proper response
                SendData(tcpSocket, string.Format("HELO {0}\r\n", Dns.GetHostName()));
                if (!CheckResponse(tcpSocket, 250))
                {
                    return false;
                }

                // if we got here it's that we can connect to the smtp server
                return true;
            }
        }

        private static void SendData(Socket socket, string data)
        {
            byte[] dataArray = Encoding.ASCII.GetBytes(data);
            socket.Send(dataArray, 0, dataArray.Length, SocketFlags.None);
        }

        private static bool CheckResponse(Socket socket, int expectedCode)
        {
            while (socket.Available == 0)
            {
                System.Threading.Thread.Sleep(100);
            }
            byte[] responseArray = new byte[1024];
            socket.Receive(responseArray, 0, socket.Available, SocketFlags.None);
            string responseData = Encoding.ASCII.GetString(responseArray);
            int responseCode = Convert.ToInt32(responseData.Substring(0, 3));
            if (responseCode == expectedCode)
            {
                return true;
            }
            return false;
        }

        public static string TestEmailConnection(string OutgoingMailServer, string emailAddress, string port, string password, string subject, string message, bool enableSSL)
        {
            try
            {
                if (string.IsNullOrEmpty(port))
                {
                    return "Outgoing server port not found";
                }

                SmtpClient smtp = new SmtpClient(OutgoingMailServer, int.Parse(port));
                smtp.EnableSsl = enableSSL;
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(emailAddress, password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = SMTPUserInfo;
                smtp.Send(new MailMessage(emailAddress, emailAddress, subject, message));
                return string.Empty;
            }
            catch (SmtpFailedRecipientException)
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Format("SMTP server connection test failed: {0}", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public static string CheckDbConnection()
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.Message; // any error is considered as db connection error for now
            }
        }

        public static string EmailSignature()
        {
            //check signature
            
            string strSignature = "";
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "EmailSignatures/email_signature.htm")))
            {
                StreamReader RDR_SIGNATURE = new StreamReader(Path.Combine(Environment.CurrentDirectory, "EmailSignatures/email_signature.htm"));
                strSignature = RDR_SIGNATURE.ReadToEnd();
                RDR_SIGNATURE.Close();
            }

            return strSignature;
        }

    }
}
