using System.Collections.Generic;

namespace BRIMS.EmailAlerts.BusinessLogic.System
{
    /// <summary>
    /// BRIMS System service interface
    /// </summary>
    public partial interface ISystemService
    {
        /// <summary>
        /// Processes email alerts
        /// </summary>
        List<tSystem> GetAllBranches();

        /// <summary>
        /// Processes email alerts
        /// </summary>
        /// /// <param name="ourBranchId">Branch identifier</param>
        /// Returns t_system identified by the branch Id
        tSystem GetSystemByBranchId(string ourBranchId);



    }
}
