using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP3_NET.Models.Marchandise;


namespace TP3_NET.Controllers.Marchandise
{
    public class MarchandiseController : Controller
    {
        public ActionResult ListeConsultable(IEnumerable<Models.Marchandise.Marchandise> marchandises)
        {
            Session["marchandises"] = new List<Models.Marchandise.Marchandise>(marchandises);

            return PartialView("_ListeConsultable", marchandises);
        }

        public ActionResult ListeModifiable(IEnumerable<Models.Marchandise.Marchandise> marchandises)
        {
            Session["marchandises"] = new List<Models.Marchandise.Marchandise>(marchandises);

            return PartialView("_ListeModifiable", marchandises);
        }

        public ActionResult Ajouter()
        {
            return PartialView("_Ajouter", new Models.Marchandise.Marchandise());
        }

        public ActionResult Modifier(Models.Marchandise.Marchandise marchandise, int idx)
        {
            return PartialView("_Modifier", new Models.Utilitaire.Paire<Models.Marchandise.Marchandise, int>(marchandise, idx));
        }

        public ActionResult Consulter(Models.Marchandise.Marchandise marchandise, int idx)
        {
            return PartialView("_Consulter", new Models.Utilitaire.Paire<Models.Marchandise.Marchandise, int>(marchandise, idx));
        }

        public ActionResult Supprimer(int idx)
        {
            return PartialView("_Supprimer", idx);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjouterPost(Models.Marchandise.Marchandise marchandise)
        {
            List<Models.Marchandise.Marchandise> marchandises;

            if (Session["marchandises"] is List<Models.Marchandise.Marchandise>)
            {
                marchandises = (List<Models.Marchandise.Marchandise>)Session["marchandises"];
            }
            else
            {
                marchandises = new List<Models.Marchandise.Marchandise>();
            }

            if (ModelState.IsValid)
            {
                marchandises.Add(marchandise);

                Session["marchandises"] = marchandises;
            }

            return PartialView("_ListeModifiable", marchandises);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierPost(Models.Utilitaire.Paire<Models.Marchandise.Marchandise, int> paireMarchandiseIdx)
        {
            List<Models.Marchandise.Marchandise> marchandises;

            if (Session["marchandises"] is List<Models.Marchandise.Marchandise>)
            {
                marchandises = (List<Models.Marchandise.Marchandise>)Session["marchandises"];
            }
            else
            {
                marchandises = new List<Models.Marchandise.Marchandise>();
            }

            if (ModelState.IsValid)
            {
                marchandises[paireMarchandiseIdx.Item2] = paireMarchandiseIdx.Item1;

                Session["marchandises"] = marchandises;
            }

            return PartialView("_ListeModifiable", marchandises);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupprimerPost(int idx)
        {
            List<Models.Marchandise.Marchandise> marchandises;

            if (Session["marchandises"] is List<Models.Marchandise.Marchandise>)
            {
                marchandises = (List<Models.Marchandise.Marchandise>)Session["marchandises"];
            }
            else
            {
                marchandises = new List<Models.Marchandise.Marchandise>();
            }

            if (idx >= 0 && idx < marchandises.Count)
            {
                marchandises.RemoveAt(idx);

                Session["marchandises"] = marchandises;
            }
            else
            {
                return new HttpNotFoundResult();
            }

            return PartialView("_ListeModifiable", marchandises);
        }
    }
}