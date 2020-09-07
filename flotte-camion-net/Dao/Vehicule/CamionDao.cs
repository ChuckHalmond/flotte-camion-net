using System.Collections.Generic;
using System.Linq;

namespace TP3_NET.Dao.Vehicule
{
    public class CamionDao : BaseDao <Models.Camion.Camion>
    {
        public override Models.Camion.Camion Create(Models.Camion.Camion entity)
        {
            entity = DatabaseContext.Camions.Add(entity);
            DatabaseContext.SaveChanges();

            return entity;
        }

        public override Models.Camion.Camion FindById(int id)
        {
            return DatabaseContext.Camions.Find(id);
        }

        public override List<Models.Camion.Camion> GetList()
        {
            return DatabaseContext.Camions.ToList();
        }

        public override bool Remove(int id)
        {
            var entity = FindById(id);
            if (entity == null)
            {
                return false;
            }

            DatabaseContext.Camions.Remove(entity);
            DatabaseContext.SaveChanges();

            return true;
        }

        public override Models.Camion.Camion Update(int id, Models.Camion.Camion entity)
        {
            var entityTemp = FindById(id);
            entity.IdCamion = id;

            DatabaseContext.Entry(entityTemp).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();

            return entity;
        }
    }
}

