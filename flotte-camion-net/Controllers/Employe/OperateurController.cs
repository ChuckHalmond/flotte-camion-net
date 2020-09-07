using System;
using System.Web.Mvc;
using TP3_NET.Dao.Employe;
using TP3_NET.Models.Compte;
using TP3_NET.Models.Employe;
using TP3_NET.Models.Service;

namespace TP3_NET.Controllers.Employe
{
    public class OperateurController : Controller
    {
        private readonly OperateurDao _operateurDao = new OperateurDao();

        public ActionResult Creer()
        {
            return View("Creer");
        }

        [HttpPost]
        public ActionResult Creer(Operateur operateur)
        {
            if (!ModelState.IsValid) return Creer();
            try
            {
                operateur.Compte.MotDePasse = EncryptionService.EncryptSha256(operateur.Compte.MotDePasse);
                operateur.Compte.TypeCompte = TypeCompte.Operateur;
                operateur = _operateurDao.Create(operateur);
                Session["compteID"] = operateur.Compte.IdCompte;
                Session["typeCompte"] = operateur.Compte.TypeCompte;
                Session["messageBienvenue"] = "Bienvenue " + operateur.Prenom + " " + operateur.Nom;
                return RedirectToAction("Index", "Accueil");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ActionMessage", e.Message);
                return Creer();
            }
        }

        public ActionResult Liste()
        {
            var liste = _operateurDao.GetList();
            return View("Liste", null, liste);
        }
        
        public ActionResult Supprimer(int id)
        {
            try
            {
                _operateurDao.Remove(id);
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("SuppressionMessage", e.Message);
                return Liste();
            }
        }

        public ActionResult Modifier(int id)
        {
            try
            {
                var operateur = _operateurDao.FindById(id);
                if (operateur == null)
                    throw new Exception("Impossible de trouver ce compte. Veuillez contacter un administrateur");
                return View("Modifier", null, operateur);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Liste();
            }
        }

        [HttpPost]
        public ActionResult Modifier(Operateur operateur)
        {
            try
            {
                operateur.Compte.MotDePasse = EncryptionService.EncryptSha256(operateur.Compte.MotDePasse);
                _operateurDao.Update(operateur.IdEmploye, operateur);
                ModelState.AddModelError("SuccessMessage", "Votre compte a bien été mis à jour");
                return Liste();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Modifier(operateur.IdEmploye);
            }
        }

        public ActionResult Consulter(int id)
        {
            try
            {
                var oper = _operateurDao.FindById(id);
                return View("Consulter", null, oper);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Liste();
            }
        }

    }
}