using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.UmbrellaAlerts
{
    public partial interface IUmbrellaEmailService
    {
        /// <summary>
        /// Processes umbrella email alerts
        /// </summary>
        /// /// <param name="ourBranchId">Branch identifier</param>
        void ProcessEmailAlerts(string ourBranchId);

    }
}
