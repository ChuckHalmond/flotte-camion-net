using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TP3_NET.Dao.Itineraire
{
    public class ItineraireDao : BaseDao<Models.Livraison.Itineraire>
    {
        public override Models.Livraison.Itineraire Create(Models.Livraison.Itineraire entity)
        {
            entity = DatabaseContext.Itineraires.Add(entity);

            DatabaseContext.SaveChanges();

            return entity;
        }

        public override Models.Livraison.Itineraire Update(int id, Models.Livraison.Itineraire entity)
        {
            var dbEntity = FindById(id);
            entity.IdItineraire = id;

            DatabaseContext.Entry(dbEntity).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();

            return dbEntity;
        }

        public override bool Remove(int id)
        {
            var entity = FindById(id);
            if (entity == null)
            {
                return false;
            }

            DatabaseContext.Itineraires.Remove(entity);
            DatabaseContext.SaveChanges();

            return true;
        }

        public override List<Models.Livraison.Itineraire> GetList()
        {
            return DatabaseContext.Itineraires.Include(livraison => livraison.Adresses).ToList();
        }

        public override Models.Livraison.Itineraire FindById(int id)
        {
            return DatabaseContext.Itineraires.Include(livraison => livraison.Adresses)
                .Single(livraison => livraison.IdItineraire == id);
        }
    }
}