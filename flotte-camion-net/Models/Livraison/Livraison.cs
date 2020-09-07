using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP3_NET.Models.Camion;
using TP3_NET.Models.Employe;

namespace TP3_NET.Models.Livraison
{
    public class Livraison
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLivraison { get; set; }

        [ForeignKey("IdCommande")]
        public Commande.Commande Commande { get; set; }
        public int IdCommande { get; set; }

        public List<Marchandise.Marchandise> Marchandises { get; set; }

        [ForeignKey("IdClient")]
        public Client.Client Client { get; set; }
        public int IdClient { get; set; }

        public Conducteur Conducteur { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateDepart { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateArrivee { get; set; }

        public Camion.Camion Camion { get; set; }

        public Remorque Remorque { get; set; }

        public EtatLivraison EtatLivraison { get; set; } = EtatLivraison.EnPreparation;

        [ForeignKey("IdItineraire")]
        public Itineraire Itineraire { get; set; }
        public int IdItineraire { get; set; }
    }
}