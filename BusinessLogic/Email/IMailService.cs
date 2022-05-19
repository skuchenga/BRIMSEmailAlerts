using System.Collections.Generic;

namespace BRIMS.EmailAlerts.BusinessLogic.Email
{
    public partial interface IMailService
    {
        /// <summary>
        /// Gets a collection of Mail Recipients
        /// </summary>
        /// <returns>A collection of Mail Recipients</returns>
        List<BRIMSMail> GetMailRecipients();

        /// <summary>
        /// Updates the sent email alerts
        /// </summary>
        /// <param name="id">Email identifier</param>
        /// <param name="Item">Email Alert Item</param>
        /// <param name="OurBranchID">BranchIdentifier</param>
        void UpdateSentEmailAlert(long id,string item, string ourBranchID);

        /// <summary>
        /// Updates the sent trx email alerts
        /// </summary>
        /// <param name="TrxIDS">TrxIDS</param>
        void UpdateSentEmailAlert(string trxIDS);

        /// <summary>
        /// Updates the Umbrella email alerts
        /// </summary>
        /// <param name="

    }
}
