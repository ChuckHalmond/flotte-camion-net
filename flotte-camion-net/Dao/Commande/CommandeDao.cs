using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP3_NET.Models.Commande;
using TP3_NET.Models.Marchandise;
using TP3_NET.Dao.Client;
using System.Data.Entity;
using TP3_NET.Dao.Commun;
using TP3_NET.Dao.Livraison;
using TP3_NET.Dao.Marchandise;
using TP3_NET.Models.Camion;
using TP3_NET.Models.Livraison;

namespace TP3_NET.Dao.Commande
{
    public class CommandeDao : BaseDao<Models.Commande.Commande>
    {
        private readonly AdresseDao _adresseDao = new AdresseDao();
        private readonly ClientDao _clientDao = new ClientDao();
        private readonly MarchandiseDao _marchandiseDao = new MarchandiseDao();
        private readonly LivraisonDao _livraisonDao = new LivraisonDao();

        public override Models.Commande.Commande Create(Models.Commande.Commande entity)
        {
            entity = DatabaseContext.Commandes.Add(entity);
            DatabaseContext.SaveChanges();

            return entity;
        }

        public List<Models.Commande.Commande> GetByClient(int id)
        {
            return DatabaseContext.Commandes.Include(commande => commande.LieuDepart)
                .Include(commande => commande.LieuArrivee)
                .Include(commande => commande.Client)
                .Include(commande => commande.Marchandises).Where(commande => commande.Client.IdClient == id).ToList();
        }

        public override Models.Commande.Commande FindById(int id)
        {
            return DatabaseContext.Commandes.Include(commande => commande.LieuDepart)
                .Include(commande => commande.LieuArrivee)
                .Include(commande => commande.Client)
                .Include(commande => commande.Marchandises)
                .Single(commande => commande.IdCommande == id);
        }

        public override List<Models.Commande.Commande> GetList()
        {
            return DatabaseContext.Commandes.Include(commande => commande.LieuDepart)
                .Include(commande => commande.LieuArrivee)
                .Include(commande => commande.Client)
                .Include(commande => commande.Marchandises).ToList();
        }

        public override bool Remove(int id)
        {
            var entity = FindById(id);
            if (entity == null)
            {
                return false;
            }

            var livraison = _livraisonDao.FindByIdCommande(entity.IdCommande);
            if (livraison != null && (livraison.EtatLivraison.Equals(EtatLivraison.EnCours) || livraison.EtatLivraison.Equals(EtatLivraison.Livree)))
            {
                throw new ArgumentException("Suppression impossible, une livraison est désormais associée à cette commande");
            }
                
            DatabaseContext.Commandes.Remove(entity);
            DatabaseContext.SaveChanges();

            return true;
        }

        public override Models.Commande.Commande Update(int id, Models.Commande.Commande entity)
        {
            var dbEntity = FindById(id);

            DatabaseContext.Entry(dbEntity).CurrentValues.SetValues(entity);

            // Lieu de départ

            var lieuDepart = entity.LieuDepart;
            if (lieuDepart != null)
            {
                var dbLieuDepart = _adresseDao.FindById(entity.LieuDepart.IdAdresse);
                if (dbLieuDepart != null)
                {
                    dbEntity.LieuDepart = _adresseDao.Update(lieuDepart.IdAdresse, lieuDepart);
                }
                else
                {
                    dbEntity.LieuDepart = _adresseDao.Create(lieuDepart);
                }
            }

            // Lieu d'arrivée

            var lieuArrivee = entity.LieuArrivee;
            if (lieuArrivee != null)
            {
                var dbLieuArrivee = _adresseDao.FindById(lieuArrivee.IdAdresse);

                if (dbLieuArrivee != null)
                {
                    dbEntity.LieuArrivee = _adresseDao.Update(lieuArrivee.IdAdresse, lieuArrivee);
                }
                else
                {
                    dbEntity.LieuArrivee = _adresseDao.Create(lieuArrivee);
                }
            }

            // Marchandises

            // Supprimer les anciennes marchandises.
            if (dbEntity.Marchandises != null)
            {
                for (int idx = 0; idx < dbEntity.Marchandises.Count; idx++)
                {
                    _marchandiseDao.Remove(dbEntity.Marchandises[0].IdMarchandise);
                }
            }

            if (entity.Marchandises != null)
            {
                for (int idx = 0; idx < entity.Marchandises.Count; idx++)
                {
                    var marchandise = _marchandiseDao.FindById(entity.Marchandises[idx].IdMarchandise);

                    if (marchandise != null)
                    {
                        _marchandiseDao.Update(marchandise.IdMarchandise, entity.Marchandises[idx]);
                    }
                    else
                    {
                        _marchandiseDao.Create(entity.Marchandises[idx]);
                    }
                }
            }

            DatabaseContext.SaveChanges();

            return dbEntity;
        }
    }
}