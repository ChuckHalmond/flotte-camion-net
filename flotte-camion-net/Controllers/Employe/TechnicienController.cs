using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TP3_NET.Dao.Employe;
using TP3_NET.Models.Employe;

namespace TP3_NET.Controllers.Employe
{
    public class TechnicienController : Controller
    {
        private readonly TechnicienDao _technicienDao = new TechnicienDao();

        public ActionResult Liste()
        {
            var list = _technicienDao.GetList();
            return View("Liste", null, list);
        }

        public ActionResult Creer()
        {
            return View("Creer");
        }

        [HttpPost]
        public ActionResult Creer(Technicien technicien)
        {
            if (!ModelState.IsValid) return Creer();
            try
            {
                var tempTechnicien = (Technicien) Session["technicien"];
                technicien.Competences = tempTechnicien?.Competences ?? new List<Competence>();
                
                _technicienDao.Create(technicien);
                Session["technicien"] = null;
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ActionMessage", e.Message);
                return Creer();
            }
        }

        [HttpPost]
        public ActionResult AjouterCompetence()
        {
            var competence = Request.Form.Get("competence");
            var competenceClass = new Competence
            {
                TypeCompetence = Competence.Parse(competence)
            };
            var technicien = (Technicien) Session["technicien"] ?? new Technicien();
            technicien.AddCompetence(competenceClass);
            Session["technicien"] = technicien;
            return View("Creer", null, technicien);
        }
        
        public ActionResult Modifier(int id)
        {
            try
            {
                var technicien = _technicienDao.FindById(id);
                if (technicien == null)
                    throw new Exception("Impossible de trouver ce techncien. Veuillez contacter un administrateur");
                return View("Modifier", null, technicien);
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
                if (!_technicienDao.Remove(id))
                    throw new Exception("Impossible de supprimer ce technicien. Veuillez contacter un administrateur");
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("SuppressionMessage", e.Message);
                return Liste();
            }
        }

        [HttpPost]
        public ActionResult Modifier(Technicien technicien)
        {
            try
            {
                _technicienDao.Update(_technicienDao.FindById(technicien.IdEmploye).IdEmploye, technicien);
                ModelState.AddModelError("SuccessMessage", "Le conducteur a bien été mis à jour.");
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Modifier(technicien.IdEmploye);
            }
        }

        public ActionResult Consulter(int id)
        {
            try
            {
                var technicien = _technicienDao.FindById(id);
                return View("Consulter", null, technicien);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Liste();
            }
        }
    }
}