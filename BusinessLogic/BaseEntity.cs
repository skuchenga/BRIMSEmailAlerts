using System.Collections.Generic;
using System.Data;



namespace BRIMS.EmailAlerts.BusinessLogic
{
    /// <summary>
    /// Provides a base class for entities
    /// </summary>
    public  abstract partial class BaseEntity
    {
        //protected IEnumerable<TEntity> ToList(IDbCommand command)
        //{
        //    using (var reader = command.ExecuteReader())
        //    {
        //        List<TEntity> items = new List<TEntity>();
        //        while (reader.Read())
        //        {
        //            var item = CreateEntity();
        //            Map(record, item);
        //            items.Add(item);
        //        }
        //        return items;
        //    }
        //}

        //protected abstract void Map(IDataRecord record, TEntity entity);
        //protected abstract TEntity CreateEntity();
    }
}
