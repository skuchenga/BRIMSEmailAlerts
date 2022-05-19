using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.TransactionEmailAlerts
{
    public partial class TransactionEmailAlert : BaseEntity
    {

        /// <summary>
        /// Gets or sets the Transaction ID
        /// </summary>
        public decimal TranID { get; set; }

        /// <summary>
        /// Gets or sets the Account ID
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// Gets or sets the Client ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Type
        /// </summary>
        public string TrxType { get; set; }

        /// <summary>
        /// Gets or sets the Client First Name
        /// </summary>
        public string UTCFirstName { get; set; }

        /// <summary>
        /// Gets or sets the Client Last Name
        /// </summary>
        public string UTCLastName { get; set; }

        /// <summary>
        /// Gets or sets the Client Email ID
        /// </summary>
        public string UTCEmail { get; set; }

        /// <summary>
        /// Gets or sets the Client Mobile number
        /// </summary>
        public string UTCMobile { get; set; }

                
    }
}
