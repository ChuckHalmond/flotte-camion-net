using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TP3_NET.Models.Recherche;

namespace TP3_NET.Dao.Livraison
{
    public class LivraisonDao : BaseDao<Models.Livraison.Livraison>
    {
        public override Models.Livraison.Livraison Create(Models.Livraison.Livraison entity)
        {
            entity = DatabaseContext.Livraisons.Add(entity);

            DatabaseContext.SaveChanges();

            return entity;
        }

        public override Models.Livraison.Livraison Update(int id, Models.Livraison.Livraison entity)
        {
            var dbEntity = FindById(id);
            entity.IdLivraison = id;

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

            DatabaseContext.Livraisons.Remove(entity);
            DatabaseContext.SaveChanges();

            return true;
        }

        public override List<Models.Livraison.Livraison> GetList()
        {
            var dbList = DatabaseContext.Livraisons.Include(livraison => livraison.Commande)
            .Include(livraison => livraison.Client)
            .Include(livraison => livraison.Conducteur)
            .Include(livraison => livraison.Camion)
            .Include(livraison => livraison.Remorque)
            .Include(livraison => livraison.Itineraire)
            .Include(livraison => livraison.Itineraire.Adresses)
            .Include(livraison => livraison.Marchandises)
            .ToList();

            return dbList;
        }

        public override Models.Livraison.Livraison FindById(int id)
        {
            var dbLivraison = DatabaseContext.Livraisons.Include(livraison => livraison.Commande)
            .Include(livraison => livraison.Client)
            .Include(livraison => livraison.Conducteur)
            .Include(livraison => livraison.Camion)
            .Include(livraison => livraison.Remorque)
            .Include(livraison => livraison.Itineraire)
            .Include(livraison => livraison.Itineraire.Adresses)
            .Include(livraison => livraison.Marchandises)
            .Single(livraison => livraison.IdLivraison == id);

            return dbLivraison;
        }

        public List<Models.Livraison.Livraison> GetByClient(int idClient)
        {
            var dbList = DatabaseContext.Livraisons.Include(livraison => livraison.Commande)
            .Include(livraison => livraison.Client)
            .Include(livraison => livraison.Conducteur)
            .Include(livraison => livraison.Camion)
            .Include(livraison => livraison.Remorque)
            .Include(livraison => livraison.Itineraire)
            .Include(livraison => livraison.Itineraire.Adresses)
            .Include(livraison => livraison.Marchandises)
            .Where(livraison => livraison.Client.IdClient == idClient)
            .ToList();

            return dbList;
        }

        public IEnumerable<TP3_NET.Models.Livraison.Livraison> Search(Recherche recherche)
        {
            IQueryable<TP3_NET.Models.Livraison.Livraison> query = null;
            if (recherche.NomClient != null)
            {
                query = DatabaseContext.Livraisons.Include(livraison => livraison.Client.Nom == recherche.NomClient);
            }

            if (recherche.DateCommande != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(livraison =>
                        livraison.Commande.DateCommande == recherche.DateCommande)
                    : query.Include(livraison => livraison.Commande.DateCommande == recherche.DateCommande);
            }

            if (recherche.ImmatricultionCamion != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(livraison =>
                        livraison.Camion.Immatriculation == recherche.ImmatricultionCamion)
                    : query.Include(livraison => livraison.Camion.Immatriculation == recherche.ImmatricultionCamion);
            }

            if (recherche.ImmatricultionRemorque != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(livraison =>
                        livraison.Remorque.Immatriculation == recherche.ImmatricultionRemorque)
                    : query.Include(livraison =>
                        livraison.Remorque.Immatriculation == recherche.ImmatricultionRemorque);
            }

            if (recherche.NomConducteur != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(livraison =>
                        livraison.Conducteur.Nom == recherche.NomConducteur)
                    : query.Include(livraison => livraison.Conducteur.Nom == recherche.NomConducteur);
            }

            if (recherche.DateDepart != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(livraison => livraison.DateDepart == recherche.DateDepart)
                    : query.Include(livraison => livraison.DateDepart == recherche.DateDepart);
            }

            if (recherche.DateArrivee != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(livraison => livraison.DateArrivee == recherche.DateArrivee)
                    : query.Include(livraison => livraison.DateArrivee == recherche.DateArrivee);
            }

            if (recherche.VilleDepart != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(livraison =>
                        livraison.Commande.LieuDepart.Ville == recherche.VilleDepart)
                    : query.Include(livraison => livraison.Commande.LieuDepart.Ville == recherche.VilleDepart);
            }

            if (recherche.VilleArrivee != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(livraison =>
                        livraison.Commande.LieuArrivee.Ville == recherche.VilleArrivee)
                    : query.Include(livraison => livraison.Commande.LieuArrivee.Ville == recherche.VilleArrivee);
            }

            /*if (recherche.TypeMarchandise != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(livraison =>
                        livraison.Marchandises[0] == recherche.TypeMarchandise)
                    : query.Include(livraison => livraison.Marchandises[0] == recherche.TypeMarchandise);
            }*/
            if (recherche.EtatLivraison != null)
            {
                query = query == null
                    ? DatabaseContext.Livraisons.Include(
                        livraison => livraison.EtatLivraison == recherche.EtatLivraison)
                    : query.Include(livraison => livraison.EtatLivraison == recherche.EtatLivraison);
            }

            return query.AsEnumerable();
        }

        public Models.Livraison.Livraison FindByIdCommande(int entityIdCommande)
        {
            return DatabaseContext.Livraisons.Single(livraison => livraison.Commande.IdCommande == entityIdCommande);
        }
    }
}