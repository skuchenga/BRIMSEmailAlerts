using System.Collections.Generic;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.BankExposures
{
    /// <summary>
    /// Bank Exposure Email alert service interface
    /// </summary>
    public partial interface IBankExposureService
    {
        /// <summary>
        /// Processes email alerts
        /// </summary>
        /// /// <param name="ourBranchId">Branch identifier</param>
        void ProcessEmailAlerts(string ourBranchId);

    }
}
