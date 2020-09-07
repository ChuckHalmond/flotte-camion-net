using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TP3_NET.Dao.Vehicule;
using TP3_NET.Models;
using TP3_NET.Models.Camion;

namespace TP3_NET.Controllers.Vehicule
{
    public class RemorqueController : Controller
    {
        private RemorqueDao _remorqueDao = new RemorqueDao();

        public ActionResult Liste()
        {
            var list = _remorqueDao.GetList();
            return View("Liste", null, list);
        }

        public ActionResult Creer()
        {
            return View("Creer");
        }

        [HttpPost]
        public ActionResult Creer(Remorque remorque)
        {
            if (!ModelState.IsValid) return Creer();
            try
            {
                _remorqueDao.Create(remorque);
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
                var remorque = _remorqueDao.FindById(id);
                if (remorque == null)
                    throw new Exception("Impossible de trouver ce conducteur. Veuillez contacter un administrateur");
                return View("Modifier", null, remorque);
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
                var remorque = _remorqueDao.FindById(id);
                if (remorque == null)
                    throw new Exception("Impossible de trouver ce conducteur. Veuillez contacter un administrateur");
                return View("Consulter", null, remorque);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Liste();
            }
        }
        
        public ActionResult Supprimer(int id)
        {
            try
            {
                if (!_remorqueDao.Remove(id))
                    throw new Exception("Impossible de supprimer cette remorque.");
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("SuppressionMessage", e.Message);
                return Liste();
            }
        }

        [HttpPost]
        public ActionResult Modifier(Remorque remorque)
        {
            try
            {
                _remorqueDao.Update(_remorqueDao.FindById(remorque.IdRemorque).IdRemorque, remorque);
                ModelState.AddModelError("SuccessMessage", "La remorque a bien été mis à jour.");
                return RedirectToAction("Liste");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Modifier(remorque.IdRemorque);
            }
        }
        

        public ActionResult List(int id)
        {
            try
            {
                var remorque = _remorqueDao.FindById(id);
                return View("Liste", null, (IEnumerable<Remorque>) remorque);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ModifierMessage", e.Message);
                return Liste();
            }
        }
    }
}
