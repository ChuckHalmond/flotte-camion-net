using System;
using System.Collections.Generic;
using System.Linq;
using TP3_NET.Models.Commun;

namespace TP3_NET.Dao.Commun
{
    public class AdresseDao : BaseDao<Adresse>
    {
        public override Adresse Create(Adresse entity)
        {
            entity = DatabaseContext.Adresses.Add(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override Adresse Update(int id, Adresse entity)
        {
            entity.IdAdresse = id;
            var entityTemp = FindById(id);
            DatabaseContext.Entry(entityTemp).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override bool Remove(int id)
        {
            DatabaseContext.Adresses.Remove(DatabaseContext.Adresses.Find(id) ??
                                            throw new ArgumentException("Impossible de supprimer l'adresse"));
            DatabaseContext.SaveChanges();
            return true;
        }

        public override List<Adresse> GetList()
        {
            return DatabaseContext.Adresses.ToList();
        }

        public override Adresse FindById(int id)
        {
            return DatabaseContext.Adresses.Find(id);
        }
    }
}