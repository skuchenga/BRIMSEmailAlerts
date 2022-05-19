using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.RBALimitAlerts
{
    public partial class RBAMailAlert : BaseEntity
    {
        /// <summary>
        /// Gets or sets the email alert identifier
        /// </summary>
        public Int64  ID { get; set; }

        /// <summary>
        /// Gets or sets the Client ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Gets or sets the Client Name
        /// </summary>
        public string ClientName { get; set; }


        /// <summary>
        /// Gets or sets the Security Name
        /// </summary>
        public string SecurityName { get; set; }

        /// <summary>
        /// Gets or sets the Fund Percentage
        /// </summary>
        public decimal FundPercentage { get; set; }

        /// <summary>
        /// Gets or sets the Min Limit
        /// </summary>
        public decimal MinLimit { get; set; }


        /// <summary>
        /// Gets or sets the Max Limit
        /// </summary>
        public decimal MaxLimit { get; set; }

        /// <summary>
        /// Gets or sets the Min Limit variance
        /// </summary>
        public decimal MinLimitVariance { get; set; }

        /// <summary>
        /// Gets or sets the Max Limit variance
        /// </summary>
        public decimal MaxLimitVariance { get; set; }

        /// <summary>
        /// Gets or sets the RBA Limit
        /// </summary>
        public decimal RBALimit { get; set; }

        /// <summary>
        /// Gets or sets the RBA Limit Variance
        /// </summary>
        public decimal RBALimitVariance { get; set; }

        /// <summary>
        /// Gets or sets the HV Limit
        /// </summary>
        public decimal HVLimit { get; set; }

        /// <summary>
        /// Gets or sets the HV Variance
        /// </summary>
        public decimal HVVariance { get; set; }

    }
}
