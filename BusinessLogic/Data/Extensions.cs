using System;
using System.Data;
using System.Data.Objects;
using System.Diagnostics;

namespace BRIMS.EmailAlerts.BusinessLogic.Data
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Determines whether record is attached
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        public static bool IsAttached(this ObjectContext context, object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            ObjectStateEntry entry;
            try
            {
                entry = context.ObjectStateManager.GetObjectStateEntry(entity);
                return (entry.State != EntityState.Detached);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.ToString());
            }
            return false;
        }
    }
}
