using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP3_NET.Dao.Livraison;
using TP3_NET.Models;

namespace TP3_NET.Controllers.Recherche
{
    public class RechercheController : Controller
    {
        
        private readonly LivraisonDao _livraisonDao = new LivraisonDao();
        
        public ActionResult Recherche()
        {
            return View("Recherche");
        }

        [HttpPost]
        public ActionResult Rechercher(Models.Recherche.Recherche recherche)
        {
            var livraisons = _livraisonDao.Search(recherche);
            return View("Resultats", null, livraisons);
        }
    }
}