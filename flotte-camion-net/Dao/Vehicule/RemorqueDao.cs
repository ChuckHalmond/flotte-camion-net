using System.Collections.Generic;
using System.Linq;

namespace TP3_NET.Dao.Vehicule
{
    public class RemorqueDao : BaseDao <Models.Camion.Remorque>
    {
        public override Models.Camion.Remorque Create(Models.Camion.Remorque entity)
        {
            entity = DatabaseContext.Remorques.Add(entity);
            DatabaseContext.SaveChanges();

            return entity;
           
        }

        public override Models.Camion.Remorque Update(int id, Models.Camion.Remorque entity)
        {
            var entityTemp = FindById(id);
            entity.IdRemorque = id;

            DatabaseContext.Entry(entityTemp).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();

            return entity;
        }

        public override bool Remove(int id)
        {
            var entity = FindById(id);
            if (entity == null)
            {
                return false;
            }

            DatabaseContext.Remorques.Remove(entity);
            DatabaseContext.SaveChanges();

            return true;
        }

        public override List<Models.Camion.Remorque> GetList()
        {
            return DatabaseContext.Remorques.ToList();
        }

        public override Models.Camion.Remorque FindById(int id)
        {
            return DatabaseContext.Remorques.Find(id);
        }

        public List<Models.Camion.Remorque> FindByTypeMarchandise(Models.Marchandise.TypeMarchandise typeMarchandise)
        {
            return DatabaseContext.Remorques
                .Where(remorque => remorque.TypeMarchandise == typeMarchandise)
                .ToList();
        }
    }
}