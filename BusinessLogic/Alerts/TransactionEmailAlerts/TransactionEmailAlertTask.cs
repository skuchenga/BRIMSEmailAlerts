using System.Xml;
using BRIMS.EmailAlerts.BusinessLogic.Infrastructure;
using BRIMS.EmailAlerts.BusinessLogic.Tasks;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.TransactionEmailAlerts
{
    public partial  class TransactionEmailAlertTask : ITask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        /// <param name="node">Xml node that represents a task description</param>
        public void Execute(XmlNode node)
        {
            IoC.Resolve<ITransactionEmailService>().ProcessEmailAlerts("001");
        }
    }
}
