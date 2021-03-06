using System.Collections.Generic;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.RBALimitAlerts
{
    public partial interface IRBALimitService
    {
        /// <summary>
        /// Processes email alerts
        /// </summary>
        /// /// <param name="ourBranchId">Branch identifier</param>
        void ProcessEmailAlerts(string ourBranchId);
    }
}
