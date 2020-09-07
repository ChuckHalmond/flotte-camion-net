using System;
using System.Web.Mvc;
using TP3_NET.Dao.Employe;
using TP3_NET.Models.Employe;

namespace TP3_NET.Controllers.Employe
{
    public class ConducteurController : Controller
    {
        private readonly ConducteurDao _conducteurDao = new ConducteurDao(); // Accède à la page /Conducteur/Liste 

        public ActionResult Liste()
        {
            var list = _conducteurDao.GetList();
            return View("Liste", null, list);
        }

        public ActionResult Creer()
        {
            return View("Creer");
        }

        [HttpPost]
        public ActionResult Creer(Conducteur conducteur)
        {
            if (!ModelState.IsValid) return Creer();
            try
            {
                _conducteurDao.Create(conducteur);
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ActionMessage", e.Message);
                return Creer();
            }
        }

        public ActionResult Modifier(int id)
        {
            try
            {
                var conducteur = _conducteurDao.FindById(id);
                if (conducteur == null)
                    throw new Exception("Impossible de trouver ce conducteur. Veuillez contacter un administrateur");
                return View("Modifier", null, conducteur);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Liste();
            }
        }
        
        public ActionResult Delete(int id)
        {
            try
            {
                if (!_conducteurDao.Remove(id))
                    throw new Exception("Impossible de supprimer ce conducteur. Veuillez contacter un administrateur");
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("SuppressionMessage", e.Message);
                return Liste();
            }
        }

        [HttpPost]
        public ActionResult Modifier(Conducteur conducteur)
        {
            try
            {
                _conducteurDao.Update(_conducteurDao.FindById(conducteur.IdEmploye).IdEmploye, conducteur);
                ModelState.AddModelError("SuccessMessage", "Le conducteur a bien été mis à jour.");
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Modifier(conducteur.IdEmploye);
            }
        }

        public ActionResult Consulter(int id)
        {
            try
            {
                var conducteur = _conducteurDao.FindById(id);
                return View("Consulter", null, conducteur);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Liste();
            }
        }
    }
}