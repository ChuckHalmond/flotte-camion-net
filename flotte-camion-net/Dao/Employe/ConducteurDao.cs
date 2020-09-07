using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP3_NET.Models.Employe;

namespace TP3_NET.Dao.Employe
{
    public class ConducteurDao : BaseDao<Conducteur>
    {
        public override Conducteur Create(Conducteur entity)
        {
            entity = DatabaseContext.Conducteurs.Add(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override Conducteur FindById(int id)
        {
            return DatabaseContext.Conducteurs.Single(conducteur => conducteur.IdEmploye == id);
        }

        public override List<Conducteur> GetList()
        {
            return DatabaseContext.Conducteurs.ToList();
        }

        public override bool Remove(int id)
        {
            var conducteur = FindById(id);
            DatabaseContext.Conducteurs.Remove(conducteur);
            DatabaseContext.SaveChanges();
            return true;
        }

        public override Conducteur Update(int id, Conducteur entity)
        {
            entity.IdEmploye = id;
            var entityTemp = FindById(id);
            DatabaseContext.Entry(entityTemp).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }
    }
}