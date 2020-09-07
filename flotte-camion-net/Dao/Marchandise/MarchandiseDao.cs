using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP3_NET.Models.Marchandise;

namespace TP3_NET.Dao.Marchandise
{
    public class MarchandiseDao : BaseDao<Models.Marchandise.Marchandise>
    {
        public override Models.Marchandise.Marchandise Create(Models.Marchandise.Marchandise entity)
        {
            entity = DatabaseContext.Marchandises.Add(entity);
            DatabaseContext.SaveChanges();

            return entity;
        }

        public override Models.Marchandise.Marchandise FindById(int id)
        {
            return DatabaseContext.Marchandises.Find(id);
        }

        public override List<Models.Marchandise.Marchandise> GetList()
        {
            return DatabaseContext.Marchandises.ToList();
        }

        public override bool Remove(int id)
        {
            var entity = FindById(id);
            if (entity == null)
            {
                return false;
            }

            DatabaseContext.Marchandises.Remove(entity);
            DatabaseContext.SaveChanges();

            return true;
        }

        public override Models.Marchandise.Marchandise Update(int id, Models.Marchandise.Marchandise entity)
        {
            var entityTemp = FindById(id);
            entity.IdMarchandise = id;

            DatabaseContext.Entry(entityTemp).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();

            return entity;
        }
    }
}