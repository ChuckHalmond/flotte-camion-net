using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using TP3_NET.Dao.Client;
using TP3_NET.Dao.Commande;
using TP3_NET.Dao.Livraison;
using TP3_NET.Models.Commande;

namespace TP3_NET.Controllers.Commande
{
    public class CommandeController : Controller
    {
        private readonly CommandeDao _commandeDao = new CommandeDao();
        private readonly LivraisonDao _livraisonDao = new LivraisonDao();
        private readonly ClientDao _clientDao = new ClientDao();

        public ActionResult Liste()
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsOperateur(Session))
            {
                var commandesVM = new List<CommandeViewModel>();

                foreach (Models.Commande.Commande commande in _commandeDao.GetList())
                {
                    commandesVM.Add(CommandeViewModel.FromCommande(commande));
                }

                return View(commandesVM);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        public ActionResult MesCommandes()
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsClient(Session))
            {
                var idClient = _clientDao.FindByIdCompte((int)Session["compteID"]).IdClient;
                var commandesVM = new List<CommandeViewModel>();

                foreach (Models.Commande.Commande commande in _commandeDao.GetByClient(idClient))
                {
                    commandesVM.Add(CommandeViewModel.FromCommande(commande));
                }

                return View(commandesVM);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        public ActionResult MaCommande(int id)
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsClient(Session))
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var commandeVM = CommandeViewModel.FromCommande(_commandeDao.FindById(id));

                if (commandeVM == null)
                {
                    return HttpNotFound();
                }

                return View(commandeVM);
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

                var commandeVM = CommandeViewModel.FromCommande(_commandeDao.FindById(id));

                if (commandeVM == null)
                {
                    return HttpNotFound();
                }

                return View(commandeVM);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        public ActionResult Creer()
        {
            var commandeVM = CommandeViewModel.FromCommande(new Models.Commande.Commande());

            if (commandeVM == null)
            {
                return HttpNotFound();
            }

            commandeVM.DepartSouhaitee = DateTime.Today;

            return View(commandeVM);
        }

        public ActionResult Supprimer(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var commandeVM = CommandeViewModel.FromCommande(_commandeDao.FindById(id));

            if (commandeVM == null)
            {
                return HttpNotFound();
            }

            return View(commandeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreerPost(CommandeViewModel commandeVM)
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsIdentified(Session))
            {
                int compteId = (int)Session["compteID"];
                Models.Client.Client client = _clientDao.FindByIdCompte(compteId);

                commandeVM.Marchandises = (List<Models.Marchandise.Marchandise>)Session["marchandises"];

                if (commandeVM.DepartSouhaitee.Ticks < DateTime.Today.AddDays(1).Ticks)
                {
                    ModelState.AddModelError("DepartSouhaitee", "La date de départ ne peut pas être antérieure ou égale à aujourd'hui.");

                    return View("Creer", commandeVM);
                }

                if (commandeVM.Marchandises.Count < 1)
                {
                    ModelState.AddModelError("Marchandises", "Veuillez saisir au moins une marchandise.");

                    return View("Creer", commandeVM);
                }

                if (ModelState.IsValid)
                {
                    Models.Commande.Commande commande = commandeVM.ToCommande();

                    commande.DateCommande = DateTime.Now;
                    commande.Client = client;

                    foreach (Models.Marchandise.Marchandise marchandise in commande.Marchandises)
                    {
                        marchandise.IdCommande = commande.IdCommande;
                    }

                    commande = _commandeDao.Create(commande);

                    var marchandisesMaps = commandeVM.Marchandises
                        .GroupBy(
                            marchandise => marchandise.TypeMarchandise,
                            (typeMarchandise, marchandise) => new { TypeMarchandise = typeMarchandise, Marchandises = marchandise.ToList() }
                        );

                    Models.Livraison.Itineraire itineraire = new Models.Livraison.Itineraire();

                    itineraire.Adresses = new List<Models.Commun.Adresse>();
                    itineraire.Adresses.Add(commande.LieuDepart);
                    itineraire.Adresses.Add(commande.LieuArrivee);

                    foreach (var marchandiesMap in marchandisesMaps)
                    {
                        Models.Livraison.Livraison livraison = new Models.Livraison.Livraison();

                        livraison.Client = client;
                        livraison.Commande = commande;
                        livraison.Marchandises = new List<Models.Marchandise.Marchandise>();

                        foreach (Models.Marchandise.Marchandise marchandise in marchandiesMap.Marchandises)
                        {
                            marchandise.IdLivraison = livraison.IdLivraison;
                            livraison.Marchandises.Add(marchandise);
                        }

                        livraison.EtatLivraison = Models.Livraison.EtatLivraison.EnPreparation;
                        livraison.Itineraire = itineraire;

                        _livraisonDao.Create(livraison);
                    }
                }

                if (TP3_NET.Models.Service.IdentityService.SessionUserIsClient(Session))
                {
                    return RedirectToAction("MesCommandes");
                }
                else
                {
                    return RedirectToAction("Liste");
                }
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupprimerPost(int id)
        {
            if (TP3_NET.Models.Service.IdentityService.SessionUserIsIdentified(Session))
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                _commandeDao.Remove(id);

                if (TP3_NET.Models.Service.IdentityService.SessionUserIsClient(Session))
                {
                    return RedirectToAction("MesCommandes");
                }
                else
                {
                    return RedirectToAction("Liste");
                }
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }
    }
}