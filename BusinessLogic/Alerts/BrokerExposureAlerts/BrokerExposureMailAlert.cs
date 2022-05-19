namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.BrokerExposureAlerts
{
    public partial class BrokerExposureMailAlert : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email alert identifier
        /// </summary>
        public int ColumnID { get; set; }

        /// <summary>
        /// Gets or sets the Broker Name
        /// </summary>
        public string BrokerName { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Amount
        /// </summary>
        public decimal TransactionAmount { get; set; }

        /// <summary>
        /// Gets or sets the Commission
        /// </summary>
        public decimal Commission { get; set; }


        /// <summary>
        /// Gets or sets the Broker Amount
        /// </summary>
        public decimal BrokerAmount { get; set; }


        /// <summary>
        /// Gets or sets the Actual Percentage
        /// </summary>
        public decimal ActualPercentage { get; set; }

        /// <summary>
        /// Gets or sets the Lower Limit
        /// </summary>
        public decimal LowerLimit { get; set; }

        /// <summary>
        /// Gets or sets the Lower Limit Variance percentage
        /// </summary>
        public decimal LowerLimitVariancepercentage { get; set; }

        /// <summary>
        /// Gets or sets the Limit
        /// </summary>
        public decimal Limit { get; set; }

        /// <summary>
        /// Gets or sets the Variance Percentage
        /// </summary>
        public decimal VariancePercentage { get; set; }

        #endregion
    }
}
