using System.Collections.Generic;
using BRIMS.EmailAlerts.BusinessLogic.Infrastructure;
using BRIMS.EmailAlerts.BusinessLogic.Data;
using System.Linq;

namespace BRIMS.EmailAlerts.BusinessLogic.Email
{
    public partial class MailService : IMailService
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
        public MailService(BRIMSObjectContext context)
        {
            this._context = context;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Gets a collection of Mail Recipients
        /// </summary>
        /// <returns>A collection of Mail Recipients</returns>
        public List<BRIMSMail> GetMailRecipients()
        {
            List<BRIMSMail> mailRecipients = _context.sp_GetEmailRecipient().ToList();

            return mailRecipients;
        }

        /// <summary>
        /// Updates the sent email alerts
        /// </summary>
        /// <param name="id">Email identifier</param>
        /// <param name="Item">Email Alert Item</param>
        /// <param name="OurBranchID">BranchIdentifier</param>
        public void UpdateSentEmailAlert(long id, string item, string ourBranchID)
        {
            _context.sp_UpdateSentEmailAlert(id, item, ourBranchID);
        }


        /// <summary>
        /// Updates the sent trx email alerts
        /// </summary>
        /// <param name="TrxIDS">TrxIDS</param>
        public void UpdateSentEmailAlert(string trxIDS)
        {
            _context.proc_UpdateUTTrxEmailSent(trxIDS);
        }
        #endregion
    }
}
