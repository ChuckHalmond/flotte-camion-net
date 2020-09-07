using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TP3_NET.Models.Employe;
using TP3_NET.Models.Livraison;
using TP3_NET.Models.Marchandise;

namespace TP3_NET.Models.Recherche
{
    public class Recherche
    {
        public string NomClient { get; set; }
        public DateTime DateCommande { get; set; }
        public string ImmatricultionRemorque { get; set; }
        public string ImmatricultionCamion { get; set; }
        public string NomConducteur { get; set; }
        public DateTime DateDepart { get; set; }
        public DateTime DateArrivee { get; set; }
        public string VilleDepart { get; set; }
        public string VilleArrivee{ get; set; }
        public TypeMarchandise TypeMarchandise { get; set; }
        public EtatLivraison EtatLivraison { get; set; }
    }
}