using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TP3_NET.Dao.Livraison;
using TP3_NET.Dao.Vehicule;
using TP3_NET.Dao.Employe;
using System.Linq;
using TP3_NET.Dao.Client;

namespace TP3_NET.Controllers.Livraison
{
    public class LivraisonController : Controller
    {
        private readonly ClientDao _clientDao = new ClientDao();
        private readonly LivraisonDao _livraisonDao = new LivraisonDao();
        private readonly CamionDao _camionDao = new CamionDao();
        private readonly ConducteurDao _conducteurDao = new ConducteurDao();
        private readonly RemorqueDao _remorqueDao = new RemorqueDao();

        public ActionResult Liste()
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsOperateur(Session))
            {
                var livraisonsVM = new List<Models.Livraison.LivraisonViewModel>();

                foreach (Models.Livraison.Livraison livraison in _livraisonDao.GetList())
                {
                    livraisonsVM.Add(Models.Livraison.LivraisonViewModel.FromLivraison(livraison));
                }

                return View(livraisonsVM);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        public ActionResult MesLivraisons()
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsClient(Session))
            {
                var idClient = _clientDao.FindByIdCompte((int)Session["compteID"]).IdClient;
                var livraisonsVM = new List<Models.Livraison.LivraisonViewModel>();

                foreach (Models.Livraison.Livraison livraison in _livraisonDao.GetByClient(idClient))
                {
                    livraisonsVM.Add(Models.Livraison.LivraisonViewModel.FromLivraison(livraison));
                }

                return View(livraisonsVM);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        public ActionResult MaLivraison(int id)
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsClient(Session))
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var livraison = _livraisonDao.FindById(id);

                if (livraison == null)
                {
                    return HttpNotFound();
                }

                return View(Models.Livraison.LivraisonViewModel.FromLivraison(livraison));
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        public ActionResult Consulter(int id)
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsOperateur(Session))
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var livraison = _livraisonDao.FindById(id);

                if (livraison == null)
                {
                    return HttpNotFound();
                }

                return View(Models.Livraison.LivraisonViewModel.FromLivraison(livraison));
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        public ActionResult Confirmer(int id)
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsOperateur(Session))
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var livraison = _livraisonDao.FindById(id);
                var livraisonVM = Models.Livraison.LivraisonViewModel.FromLivraison(livraison);

                if (livraisonVM == null)
                {
                    return HttpNotFound();
                }

                livraisonVM.DateDepart = livraison.Commande.DepartSouhaitee;
                livraisonVM.DateArrivee = livraisonVM.DateDepart.AddDays(1);
                Session["MarchandisesLivraison"] = livraison.Marchandises;

                IEnumerable<SelectListItem> camionsItems = from camion in _camionDao.GetList()
                    select new SelectListItem
                    {
                        Value = camion.IdCamion.ToString(),
                        Text = "Camion n°" + camion.IdCamion.ToString()
                    };

                Session["CamionsItems"] = camionsItems;

                IEnumerable<SelectListItem> conducteursItems = from conducteur in _conducteurDao.GetList()
                    select new SelectListItem
                    {
                        Value = conducteur.IdEmploye.ToString(),
                        Text = "Employé n°" + conducteur.IdEmploye.ToString()
                    };

                Session["ConducteursItems"] = conducteursItems;

                IEnumerable<SelectListItem> remorquesItems = from remorque in _remorqueDao.GetList()
                    select new SelectListItem
                    {
                        Value = remorque.IdRemorque.ToString(),
                        Text = "Remorque n°" + remorque.IdRemorque.ToString()
                    };

                Session["RemorquesItems"] = remorquesItems;


                return View(livraisonVM);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmerPost(Models.Livraison.LivraisonViewModel livraisonVM)
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsOperateur(Session))
            {
                Models.Livraison.Livraison livraison = _livraisonDao.FindById(livraisonVM.IdLivraison);

                if (livraisonVM.DateDepart.Ticks < livraison.Commande.DepartSouhaitee.Ticks)
                {
                    ModelState.AddModelError("DateDepart", "La date de départ ne peut pas être antérieure à la date souhaitée par le client.");

                    return View("Confirmer", livraisonVM);
                }

                if (livraisonVM.DateArrivee.Ticks < livraisonVM.DateDepart.Ticks)
                {
                    ModelState.AddModelError("DateArrivee", "La date d'arrivée ne peut pas être antérieure à la date de départ.");

                    return View("Confirmer", livraisonVM);
                }

                livraison.Camion = _camionDao.FindById(livraisonVM.IdCamion);
                livraison.Conducteur = _conducteurDao.FindById(livraisonVM.IdConducteur);
                livraison.Remorque = _remorqueDao.FindById(livraisonVM.IdRemorque);
                livraison.DateArrivee = livraisonVM.DateArrivee;
                livraison.DateDepart = livraisonVM.DateDepart;
                livraison.EtatLivraison = Models.Livraison.EtatLivraison.EnCours;

                if (ModelState.IsValid)
                {
                    _livraisonDao.Update(livraison.IdLivraison, livraison);
                }

                return RedirectToAction("Liste");
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }
    }
}