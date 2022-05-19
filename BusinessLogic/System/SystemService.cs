using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Configuration;
using BRIMS.EmailAlerts.BusinessLogic.Data;
using BRIMS.EmailAlerts.BusinessLogic.Infrastructure;


namespace BRIMS.EmailAlerts.BusinessLogic.System
{
    public partial class SystemService : ISystemService
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
        public SystemService(BRIMSObjectContext context)
        {
            this._context = context;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Processes email alerts
        /// </summary>
        public List<tSystem> GetAllBranches()
        {
            try
            {
               var tSystem = _context.proc_GetSystem_MailAlerts("").ToList();

                return tSystem;
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }

        /// <summary>
        /// Processes email alerts
        /// </summary>
        /// /// <param name="ourBranchId">Branch identifier</param>
        /// Returns t_system identified by the branch Id
        public tSystem GetSystemByBranchId(string ourBranchId)
        {
            if (string.IsNullOrEmpty(ourBranchId))
            {
                ourBranchId = "001";
            }

            var tSystem = _context.proc_GetSystem_MailAlerts(ourBranchId).SingleOrDefault();

            return tSystem;
        }

        #endregion



    }
}
