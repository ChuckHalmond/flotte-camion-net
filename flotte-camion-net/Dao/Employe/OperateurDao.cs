using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using TP3_NET.Dao.Compte;

namespace TP3_NET.Dao.Employe
{
    public class OperateurDao : BaseDao<Models.Employe.Operateur>
    {
        private readonly CompteDao _compteDao = new CompteDao();

        public override Models.Employe.Operateur Create(Models.Employe.Operateur entity)
        {
            entity = DatabaseContext.Operateurs.Add(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override Models.Employe.Operateur Update(int id, Models.Employe.Operateur entity)
        {
            entity.IdEmploye = id;
            var entityTemp = FindById(id);
            entity.Compte = _compteDao.Update(entityTemp.Compte.IdCompte, entity.Compte);
            DatabaseContext.Entry(entityTemp).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override bool Remove(int id)
        {
            var operateur = FindById(id);
            _compteDao.Remove(operateur.Compte.IdCompte);
            DatabaseContext.Operateurs.Remove(operateur);
            DatabaseContext.SaveChanges();
            return true;
        }

        public override List<Models.Employe.Operateur> GetList()
        {
            return DatabaseContext.Operateurs.ToList();
        }

        public override Models.Employe.Operateur FindById(int id)
        {
            return DatabaseContext.Operateurs.Include(operateur => operateur.Compte)
                .Single(operateur => operateur.IdEmploye == id);
        }

        public Models.Employe.Operateur FindByIdCompte(int id)
        {
            return DatabaseContext.Operateurs.Single(operateur => operateur.Compte.IdCompte == id);
        }
    }
}