using System.Collections.Generic;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.BrokerExposureAlerts
{
    public partial interface IBrokerExposureService
    {
        /// <summary>
        /// Processes email alerts
        /// </summary>
        /// /// <param name="ourBranchId">Branch identifier</param>
        void ProcessEmailAlerts(string ourBranchId);

    }
}
