using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.UmbrellaAlerts
{
    public partial class UmbrellaEmailAlert : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Transaction ID
        /// </summary>
        public decimal TransID { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Description
        /// </summary>
        public string TransDesc { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Date
        /// <summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the Subclient code
        /// <summary>
        public string SubClientCode { get; set; }

        /// <summary>
        /// Gets or sets the Client email ID
        /// <summary>
        public string ClientEmail { get; set; }

        /// <summary>
        /// Gets or sets the Client mobile number
        /// <summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Type
        /// </summary>
        public string SubTypeID { get; set; }
    }
}
