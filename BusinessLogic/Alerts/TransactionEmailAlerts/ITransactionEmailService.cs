using System.Collections.Generic;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.TransactionEmailAlerts
{
    public partial interface ITransactionEmailService
    {
        /// <summary>
        /// Processes email alerts
        /// </summary>
        /// /// <param name="ourBranchId">Branch identifier</param>
        void ProcessEmailAlerts(string ourBranchId);
    }
}
