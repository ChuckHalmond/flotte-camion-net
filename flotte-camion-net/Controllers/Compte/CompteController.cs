using System;
using System.Web.Mvc;
using TP3_NET.Dao.Client;
using TP3_NET.Dao.Compte;
using TP3_NET.Dao.Employe;
using TP3_NET.Models.Compte;
using TP3_NET.Models.Service;

namespace TP3_NET.Controllers.Compte
{
    public class CompteController : Controller
    {
        private readonly ClientDao _clientDao = new ClientDao();
        private readonly CompteDao _compteDao = new CompteDao();
        private readonly OperateurDao _operateurDao = new OperateurDao();

        public ActionResult Connexion()
        {
            return View("Connexion");
        }

        [HttpPost]
        public ActionResult Connexion(Models.Compte.Compte compte)
        {
            if (!ModelState.IsValid)
            {
                return Connexion();
            }

            compte.MotDePasse = EncryptionService.EncryptSha256(compte.MotDePasse);
            compte = _compteDao.FindByEmailAndPassword(compte.Email, compte.MotDePasse);

            if (compte == null)
            {
                ModelState.AddModelError("ActionMessage", "Adresse mail ou mot de passe incorrect");
                return Connexion();
            }

            Session["compteID"] = compte.IdCompte;
            Session["typeCompte"] = compte.TypeCompte;

            if (compte.TypeCompte.Equals(TypeCompte.Operateur))
            {
                var operateur = _operateurDao.FindByIdCompte(compte.IdCompte);
                Session["messageBienvenue"] = "Bienvenue " + operateur.Prenom + " " + operateur.Nom;
            }
            else if (compte.TypeCompte.Equals(TypeCompte.Client))
            {
                var client = _clientDao.FindByIdCompte(compte.IdCompte);
                Session["messageBienvenue"] = "Bienvenue " + client.Prenom + " " + client.Nom;
            }

            return RedirectToAction("Index", "Accueil");
        }
    }
}