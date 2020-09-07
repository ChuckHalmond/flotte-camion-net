using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TP3_NET.Models.Employe;

namespace TP3_NET.Dao.Employe
{
    public class TechnicienDao : BaseDao<Technicien>
    {
        public override Technicien Create(Technicien entity)
        {
            entity = DatabaseContext.Techniciens.Add(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override Technicien Update(int id, Technicien entity)
        {
            entity.IdEmploye = id;
            var entityTemp = FindById(id);
            entity.Competences = entityTemp.Competences;
            DatabaseContext.Entry(entityTemp).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override bool Remove(int id)
        {
            var technicien = FindById(id);
            DatabaseContext.Techniciens.Remove(technicien);
            DatabaseContext.SaveChanges();
            return true;
        }

        public override List<Technicien> GetList()
        {
            return DatabaseContext.Techniciens.ToList();
        }

        public override Technicien FindById(int id)
        {
            return DatabaseContext.Techniciens.Include(technicien => technicien.Competences)
                .Single(technicien => technicien.IdEmploye == id);
        }
    }
}