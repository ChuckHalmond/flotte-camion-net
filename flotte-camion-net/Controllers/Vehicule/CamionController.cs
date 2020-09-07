using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TP3_NET.Dao.Commande;
using TP3_NET.Dao.Vehicule;
using TP3_NET.Models;
using TP3_NET.Models.Camion;
using TP3_NET.Models.Employe;

namespace TP3_NET.Controllers.Vehicule
{
    public class CamionController : Controller
    {
        private readonly CamionDao _camionDao = new CamionDao();

        // GET: Camion
        public ActionResult Liste()
        {
            var list = _camionDao.GetList();
            return View("Liste", null, list);
        }

        public ActionResult Creer()
        {
            return View("Creer");
        }

        [HttpPost]
        public ActionResult Creer(Camion camion)
        {
            if (!ModelState.IsValid) return Creer();
            try
            {
                _camionDao.Create(camion);
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
                var camion = _camionDao.FindById(id);
                if (camion == null)
                    throw new Exception("Impossible de trouver ce camion.");
                return View("Modifier", null, camion);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Liste();
            }
        }
        public ActionResult Consulter(int id)
        {
            try
            {
                var camion = _camionDao.FindById(id);
                if (camion == null)
                    throw new Exception("Impossible de trouver ce camion.");
                return View("Consulter", null, camion);
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
                if (!_camionDao.Remove(id))
                    throw new Exception("Impossible de supprimer ce camion.");
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("SuppressionMessage", e.Message);
                return Liste();
            }
        }

        [HttpPost]
        public ActionResult Modifier(Camion camion)
        {
            try
            {
                _camionDao.Update(_camionDao.FindById(camion.IdCamion).IdCamion, camion);
                ModelState.AddModelError("SuccessMessage", "Le camion a bien été mis à jour.");
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Modifier(camion.IdCamion);
            }
        }

        public ActionResult List(int id)
        {
            try
            {
                var camion = _camionDao.FindById(id);
                return View("Liste", null, (IEnumerable<Camion>) camion);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Liste();
            }
        }
    }
}
