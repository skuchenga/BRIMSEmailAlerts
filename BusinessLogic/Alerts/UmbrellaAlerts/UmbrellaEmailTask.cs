using System.Xml;
using BRIMS.EmailAlerts.BusinessLogic.Infrastructure;
using BRIMS.EmailAlerts.BusinessLogic.Tasks;

namespace BRIMS.EmailAlerts.BusinessLogic.Alerts.UmbrellaAlerts
{
    public partial class UmbrellaEmailTask : ITask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        /// <param name="node">Xml node that represents a task description</param>
        public void Execute(XmlNode node)
        {
            IoC.Resolve<IUmbrellaEmailService>().ProcessEmailAlerts("001");
        }
    }
}
