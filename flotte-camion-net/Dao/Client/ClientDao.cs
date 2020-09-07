using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TP3_NET.Dao.Commun;
using TP3_NET.Dao.Compte;

namespace TP3_NET.Dao.Client
{
    public class ClientDao : BaseDao<Models.Client.Client>
    {
        private readonly AdresseDao _adresseDao = new AdresseDao();
        private readonly CompteDao _compteDao = new CompteDao();

        public override Models.Client.Client Create(Models.Client.Client entity)
        {
            entity = DatabaseContext.Clients.Add(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override Models.Client.Client Update(int id, Models.Client.Client entity)
        {
            entity.IdClient = id;
            var entityTemp = FindById(id);
            if (!entityTemp.Compte.MotDePasse.Equals(entity.Compte.MotDePasse))
                throw new ArgumentException("Mot de passe incorrect");
            entity.Compte = _compteDao.Update(entityTemp.Compte.IdCompte, entity.Compte);
            entity.Adresse = _adresseDao.Update(entityTemp.Adresse.IdAdresse, entity.Adresse);
            DatabaseContext.Entry(entityTemp).CurrentValues.SetValues(entity);
            DatabaseContext.SaveChanges();
            return entity;
        }

        public override bool Remove(int id)
        {
            var client = FindById(id);
            _compteDao.Remove(client.Compte.IdCompte);
            _adresseDao.Remove(client.Adresse.IdAdresse);
            DatabaseContext.Clients.Remove(client);
            DatabaseContext.SaveChanges();
            return true;
        }

        public override List<Models.Client.Client> GetList()
        {
            return DatabaseContext.Clients.ToList();
        }

        public override Models.Client.Client FindById(int id)
        {
            return DatabaseContext.Clients.Include(client => client.Adresse).Include(client => client.Compte)
                .Single(client => client.IdClient == id);
        }

        public Models.Client.Client FindByIdCompte(int id)
        {
            return DatabaseContext.Clients.Include(client => client.Compte).Include(client => client.Adresse)
                .Single(client => client.Compte.IdCompte == id);
        }
    }
}