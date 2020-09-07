using System;
using System.Web.Mvc;
using TP3_NET.Dao.Client;
using TP3_NET.Models.Service;

namespace TP3_NET.Controllers.Client
{
    public class ClientController : Controller
    {
        private readonly ClientDao _clientDao = new ClientDao();

        public ActionResult Enregistrement()
        {
            return View("Enregistrement");
        }

        [HttpPost]
        public ActionResult Enregistrement(Models.Client.Client client)
        {
            if (!ModelState.IsValid) return Enregistrement();
            if (!Request.Form.Get("password").Equals(client.Compte.MotDePasse))
            {
                ModelState.AddModelError("password", "Les deux mots de passes ne correspondent pas");
                return Enregistrement();
            }

            try
            {
                client.Compte.MotDePasse = EncryptionService.EncryptSha256(client.Compte.MotDePasse);
                client = _clientDao.Create(client);
                Session["compteID"] = client.Compte.IdCompte;
                Session["typeCompte"] = client.Compte.TypeCompte;
                Session["messageBienvenue"] = "Bienvenue " + client.Prenom + " " + client.Nom;
                return RedirectToAction("Index", "Accueil");
            }
            catch (Exception)
            {
                ModelState.AddModelError("ActionMessage", "Adresse email déjà utilisée");
                return Enregistrement();
            }
        }

        public ActionResult Deconnexion()
        {
            Session["compteID"] = null;
            Session["typeCompte"] = null;
            return RedirectToAction("Connexion", "Compte");
        }

        public ActionResult Profil()
        {
            return View("Profil", null, _clientDao.FindByIdCompte(Convert.ToInt32(Session["compteID"])));
        }

        [HttpPost]
        public ActionResult Profil(Models.Client.Client client)
        {
            try
            {
                client.Compte.MotDePasse = EncryptionService.EncryptSha256(client.Compte.MotDePasse);
                var tempClient = _clientDao.FindByIdCompte(Convert.ToInt32(Session["compteID"]));
                client.IdClient = tempClient.IdClient;
                client.Compte.IdCompte = tempClient.Compte.IdCompte;
                client.Adresse.IdAdresse = tempClient.Adresse.IdAdresse;
                _clientDao.Update(client.IdClient, client);
                ModelState.AddModelError("SuccessMessage", "Votre compte a bien été mis à jour");
                return Profil();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ActionMessage", e.Message);
                return Profil();
            }
        }

        public ActionResult Supprimer()
        {
            try
            {
                if (!_clientDao.Remove(_clientDao.FindByIdCompte(Convert.ToInt32(Session["compteID"])).IdClient))
                    throw new Exception("Impossible de supprimer votre compte. Veuillez contacter un administrateur");
                Session["compteID"] = null;
                Session["typeCompte"] = null;
                return RedirectToAction("Index", "Accueil");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("SuppressionMessage", e.Message);
                return Profil();
            }
        }
    }
}