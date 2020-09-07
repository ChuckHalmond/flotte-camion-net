using System;
using System.Collections.Generic;
using System.Linq;

namespace TP3_NET.Dao.Compte
{
    public class CompteDao : BaseDao<Models.Compte.Compte>
    {
        public override Models.Compte.Compte Create(Models.Compte.Compte entity)
        {
            entity = DatabaseContext.Comptes.Add(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override Models.Compte.Compte Update(int id, Models.Compte.Compte entity)
        {
            entity.IdCompte = id;
            var entityTemp = FindById(id);
            DatabaseContext.Entry(entityTemp).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override bool Remove(int id)
        {
            DatabaseContext.Comptes.Remove(DatabaseContext.Comptes.Find(id) ??
                                           throw new ArgumentException("Impossible de supprimer le compte"));
            DatabaseContext.SaveChanges();
            return true;
        }

        public override List<Models.Compte.Compte> GetList()
        {
            return DatabaseContext.Comptes.ToList();
        }

        public override Models.Compte.Compte FindById(int id)
        {
            return DatabaseContext.Comptes.Find(id);
        }

        public Models.Compte.Compte FindByEmailAndPassword(string email, string password)
        {
            return DatabaseContext.Comptes
                .Where(client => client.Email == email).Single(client => client.MotDePasse == password);
        }
    }
}