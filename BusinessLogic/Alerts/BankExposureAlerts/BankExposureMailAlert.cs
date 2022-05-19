namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.BankExposures
{
    public partial class BankExposureMailAlert : BaseEntity
    {

        #region Properties

        /// <summary>
        /// Gets or sets the email alert identifier
        /// </summary>
        public int ColumnID { get; set; }

        /// <summary>
        /// Gets or sets the Bank Name
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Gets or sets the Limit Type
        /// </summary>
        public string LimitType { get; set; }

        /// <summary>
        /// Gets or sets the Limit
        /// </summary>
        public decimal Limit { get; set; }

        /// <summary>
        /// Gets or sets the Deposits Held
        /// </summary>
        public decimal DepositsHeld { get; set; }


        /// <summary>
        /// Gets or sets the Variance
        /// </summary>
        public decimal Variance { get; set; }

        #endregion
    }
}
